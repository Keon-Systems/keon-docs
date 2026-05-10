using System.IO.Compression;
using System.Text;
using System.Text.Json;
using Keon.Contracts;
using Keon.Contracts.Decision;
using Keon.Contracts.Execution;
using Keon.Contracts.Identifiers;
using Keon.Contracts.Memory;
using Keon.Contracts.Tracing;
using Keon.Control.GovernedExecution;
using Keon.Control.Compliance;
using Keon.Control.Evidence;
using Keon.Control.Observability;
using Keon.Control.Runtime;
using Keon.Core.Receipts;
using Keon.Runtime.Retention;
using Keon.Runtime.Retention.Observability;
using Keon.Runtime.Retention.SqlServer;
using Keon.Runtime.Decisions;
using Keon.Runtime.Decisions.Defaults;
using Keon.Runtime.Observability;
using Keon.Runtime.Observability.DependencyInjection;
using Keon.Runtime.Compliance;
using Keon.Runtime.Execution.Budget;
using Keon.Runtime.Execution.Variance;
using Keon.Runtime.Execution;
using Keon.Runtime.Tracing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeonReceiptGraph();
builder.Services.TryAddSingleton<ILegalHoldService, InMemoryLegalHoldService>();
builder.Services.AddSingleton<EvidencePackStore>();
builder.Services.AddSingleton<EvidencePackService>();
builder.Services.TryAddSingleton<IRetentionRunObserver, InMemoryRetentionRunObserver>();
builder.Services.TryAddSingleton<IGovernedExecutionClock, GovernedExecutionSystemClock>();
builder.Services.TryAddSingleton(sp =>
{
    var configured = builder.Configuration["GovernedExecution:Sqlite:ConnectionString"];
    var connection = string.IsNullOrWhiteSpace(configured)
        ? "Data Source=keon-governed-execution.db;Version=3;"
        : configured;

    return new GovernedExecutionStoreOptions
    {
        Enabled = true,
        ConnectionString = connection,
        AutoMigrate = true
    };
});
builder.Services.TryAddSingleton(sp =>
{
    var decisionTimeoutSeconds = builder.Configuration.GetValue("GovernedExecution:Processing:DecisionTimeoutSeconds", 10);
    var executionTimeoutSeconds = builder.Configuration.GetValue("GovernedExecution:Processing:ExecutionTimeoutSeconds", 10);

    return new GovernedExecutionProcessingOptions
    {
        DecisionTimeout = TimeSpan.FromSeconds(Math.Max(1, decisionTimeoutSeconds)),
        ExecutionTimeout = TimeSpan.FromSeconds(Math.Max(1, executionTimeoutSeconds))
    };
});
builder.Services.TryAddSingleton<IGovernedRunStore, SqliteGovernedRunStore>();
builder.Services.TryAddSingleton<IPolicyEvaluator, AllowAllPolicyEvaluator>();
builder.Services.TryAddSingleton<IAuthorityService, AllowAllAuthorityService>();
builder.Services.TryAddSingleton<Keon.Runtime.Decisions.ITraceSink, InMemoryTraceSink>();
builder.Services.TryAddSingleton<IDecisionEngine>(sp => new DecisionEngine(
    policy: sp.GetRequiredService<IPolicyEvaluator>(),
    authority: sp.GetRequiredService<IAuthorityService>(),
    traces: sp.GetRequiredService<Keon.Runtime.Decisions.ITraceSink>(),
    receiptStore: sp.GetRequiredService<IDecisionReceiptStore>()));
builder.Services.TryAddSingleton<IExecutionHandler, NoOpExecutionHandler>();
builder.Services.TryAddSingleton(sp => new ExecutionDispatcher(
    handlers: sp.GetServices<IExecutionHandler>(),
    traceSink: sp.GetRequiredService<IExecutionTraceStore>(),
    receiptStore: sp.GetRequiredService<IExecutionReceiptStore>(),
    decisionReceiptStore: sp.GetRequiredService<IDecisionReceiptStore>(),
    budgetEvaluator: new NoOpBudgetEvaluator(),
    varianceEvaluator: new NoOpVarianceEvaluator(),
    authoritativeOutbox: sp.GetRequiredService<Keon.Runtime.Observability.Outbox.IAuthoritativeReceiptOutbox>()));
builder.Services.TryAddSingleton<GovernedExecutionProcessor>();
builder.Services.TryAddSingleton<GovernedExecutionToolService>();
builder.Services.AddHostedService<GovernedExecutionWorker>();

var retentionOptions = new RetentionOptions
{
    Enabled = builder.Configuration.GetValue("Retention:Enabled", false),
    Interval = builder.Configuration.GetValue("Retention:Interval", TimeSpan.FromMinutes(15)),
    BatchSize = builder.Configuration.GetValue("Retention:BatchSize", 500),
    RequirePreviewBeforeEnforce = builder.Configuration.GetValue("Retention:RequirePreviewBeforeEnforce", true),
    PolicyRef = builder.Configuration.GetValue<string?>("Retention:PolicyRef", null) ?? string.Empty,
    StopOnError = builder.Configuration.GetValue("Retention:StopOnError", true)
};
retentionOptions.Validate();
builder.Services.AddSingleton(retentionOptions);
builder.Services.TryAddSingleton<IRetentionStore, RetentionStubStore>();
builder.Services.TryAddSingleton<IRetentionReceiptSink, RetentionStubReceiptSink>();
builder.Services.TryAddSingleton(_ => new RetentionEvaluator(RetentionPolicy.FromOptions(retentionOptions)));
builder.Services.TryAddSingleton<RetentionEnforcerRunner>();
builder.Services.TryAddSingleton<RetentionGateway>();

var retentionConnectionString = builder.Configuration["Retention:SqlServer:ConnectionString"];
if (retentionOptions.Enabled && string.IsNullOrWhiteSpace(retentionConnectionString))
{
    throw new InvalidOperationException(
        "Retention is enabled but Retention:SqlServer:ConnectionString is not configured.");
}

if (!string.IsNullOrWhiteSpace(retentionConnectionString))
{
    var sqlOptions = new SqlServerRetentionStoreOptions
    {
        ConnectionString = retentionConnectionString,
        CommandTimeoutSeconds = builder.Configuration.GetValue("Retention:SqlServer:CommandTimeoutSeconds", 30)
    };

    builder.Services.AddSingleton(sqlOptions);
    builder.Services.AddSingleton<IRetentionStore, SqlServerRetentionStore>();
    builder.Services.TryAddSingleton<IRetentionReceiptSink, NoOpRetentionReceiptSink>();
    builder.Services.AddSingleton(_ => new RetentionEvaluator(RetentionPolicy.FromOptions(retentionOptions)));
    builder.Services.AddSingleton<RetentionEnforcerRunner>();
    builder.Services.AddSingleton<RetentionGateway>();
    builder.Services.AddHostedService<RetentionEnforcerService>();
}

var app = builder.Build();

app.MapGet("/governance/receipts", async (
    string? tenantId,
    string? correlationId,
    string? kind,
    string? pageToken,
    int? limit,
    IReceiptGraph receiptGraph,
    CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(correlationId))
    {
        return Results.BadRequest(new
        {
            error = "correlationId is required for receipt listings until a receipt query service is available."
        });
    }

    var pageSize = ClampPageSize(limit);
    var offset = ParseOffset(pageToken);

    var spine = await GetNormalizedSpineAsync(receiptGraph, correlationId, ct);
    var envelopes = ExtractReceiptEnvelopes(spine);

    if (!string.IsNullOrWhiteSpace(kind))
    {
        envelopes = envelopes
            .Where(envelope => string.Equals(envelope.Kind, kind, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    if (!string.IsNullOrWhiteSpace(tenantId))
    {
        envelopes = envelopes
            .Where(envelope => string.Equals(envelope.TenantId, tenantId, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    var ordered = envelopes.OrderByDescending(r => r.TimestampUtc).ToList();
    var paged = ordered.Skip(offset).Take(pageSize).ToList();
    var nextToken = offset + pageSize < ordered.Count ? (offset + pageSize).ToString() : null;

    return Results.Json(new ReceiptPage(paged, nextToken));
});

app.MapGet("/governance/receipts/{receiptId}", async (
    string receiptId,
    string? correlationId,
    IReceiptGraph receiptGraph,
    CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(correlationId))
    {
        return Results.BadRequest(new
        {
            error = "correlationId is required to resolve a receipt via the receipt graph."
        });
    }

    var spine = await GetNormalizedSpineAsync(receiptGraph, correlationId, ct);
    var node = spine.Nodes.FirstOrDefault(n =>
        string.Equals(n.NodeId, receiptId, StringComparison.OrdinalIgnoreCase));

    var envelope = node is null ? null : MapReceiptNode(node);

    return envelope is null ? Results.NotFound() : Results.Json(envelope);
});

app.MapGet("/observability/spines/{correlationId}", async (
    string correlationId,
    IReceiptGraph receiptGraph,
    CancellationToken ct) =>
{
    var spine = await GetNormalizedSpineAsync(receiptGraph, correlationId, ct);
    return Results.Json(spine);
});

app.MapGet("/observability/correlations", async (
    string? tenantId,
    DateTimeOffset? from,
    DateTimeOffset? to,
    string? pageToken,
    ICorrelationSummaryQueryService queryService,
    CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(tenantId))
    {
        return Results.BadRequest(new { error = "tenantId is required" });
    }

    var pageSize = ClampPageSize(limit: null);
    var offset = ParseOffset(pageToken);
    var query = new CorrelationSummaryQuery(new TenantId(tenantId), from, to, offset, pageSize);
    var page = await queryService.QueryAsync(query, ct);

    return Results.Json(page);
});

app.MapGet("/observability/retention/runs", (
    int? max,
    IRetentionRunObserver observer) =>
{
    var limit = Math.Clamp(max ?? 50, 1, 200);
    var runs = observer.GetRecent(limit);
    return Results.Json(new { runs });
});

app.MapGet("/runtime/executions", async (
    string? tenantId,
    string? correlationId,
    string? pageToken,
    int? limit,
    IReceiptGraph receiptGraph,
    CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(correlationId))
    {
        return Results.BadRequest(new
        {
            error = "correlationId is required for execution listings until a receipt query service is available."
        });
    }

    var pageSize = ClampPageSize(limit);
    var offset = ParseOffset(pageToken);

    var spine = await GetNormalizedSpineAsync(receiptGraph, correlationId, ct);
    var executions = ExtractPayloads<ExecutionResult>(spine, ReceiptNodeType.Execution);

    if (!string.IsNullOrWhiteSpace(tenantId))
    {
        var tenant = new TenantId(tenantId);
        executions = executions
            .Where(e => e.Link.TenantId.Equals(tenant))
            .ToList();
    }

    var ordered = executions.OrderByDescending(e => e.Timing.StartedAt).ToList();
    var paged = ordered.Skip(offset).Take(pageSize).ToList();
    var nextToken = offset + pageSize < ordered.Count ? (offset + pageSize).ToString() : null;

    return Results.Json(new ExecutionPage(paged, nextToken));
});

app.MapGet("/runtime/executions/{executionId}", async (
    string executionId,
    string? correlationId,
    IReceiptGraph receiptGraph,
    CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(correlationId))
    {
        return Results.BadRequest(new
        {
            error = "correlationId is required to resolve an execution via the receipt graph."
        });
    }

    var spine = await GetNormalizedSpineAsync(receiptGraph, correlationId, ct);
    var execution = ExtractPayloads<ExecutionResult>(spine, ReceiptNodeType.Execution)
        .FirstOrDefault(e => e.ExecutionId.Value.Equals(executionId, StringComparison.OrdinalIgnoreCase));

    return execution is null ? Results.NotFound() : Results.Json(execution);
});

app.MapGet("/runtime/executions/{executionId}/trace", async (
    string executionId,
    string? correlationId,
    IReceiptGraph receiptGraph,
    CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(correlationId))
    {
        return Results.BadRequest(new
        {
            error = "correlationId is required to resolve traces via the receipt graph."
        });
    }

    var spine = await GetNormalizedSpineAsync(receiptGraph, correlationId, ct);
    var executionKey = new ExecutionId(executionId);
    var events = ExtractTraceEvents(spine)
        .Where(e => e.ExecutionId.Equals(executionKey))
        .OrderBy(e => e.Sequence)
        .ThenBy(e => e.Stage)
        .ToList();

    return events.Count == 0 ? Results.NotFound() : Results.Json(events);
});

app.MapPost("/compliance/evidence-packs", async (
    EvidencePackRequest request,
    EvidencePackService service,
    CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(request.TenantId) || string.IsNullOrWhiteSpace(request.CorrelationId))
    {
        return Results.BadRequest(new { error = "tenantId and correlationId are required" });
    }

    var record = await service.CreateAsync(request, ct);
    return Results.Json(record.Metadata);
});

app.MapPost("/compliance/legal-holds", async (
    LegalHoldCreateRequest request,
    ILegalHoldService legalHoldService,
    CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(request.TenantId) ||
        string.IsNullOrWhiteSpace(request.CorrelationId) ||
        string.IsNullOrWhiteSpace(request.Reason))
    {
        return Results.BadRequest(new { error = "tenantId, correlationId, and reason are required" });
    }

    var existing = await legalHoldService.GetHoldForCorrelationAsync(
        request.TenantId,
        request.CorrelationId,
        ct);

    if (existing is not null)
    {
        return Results.Json(existing);
    }

    var created = await legalHoldService.CreateHoldAsync(
        request.TenantId,
        request.CorrelationId,
        request.Reason,
        request.ExpiresAtUtc,
        ct);

    return Results.Json(created, statusCode: StatusCodes.Status201Created);
});

app.MapGet("/compliance/legal-holds", async (
    string? tenantId,
    bool? includeInactive,
    ILegalHoldService legalHoldService,
    CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(tenantId))
    {
        return Results.BadRequest(new { error = "tenantId is required" });
    }

    var activeOnly = !(includeInactive ?? false);
    var holds = await legalHoldService.ListHoldsAsync(tenantId, activeOnly, ct);
    return Results.Json(holds);
});

app.MapDelete("/compliance/legal-holds/{id}", async (
    string id,
    string? reason,
    ILegalHoldService legalHoldService,
    CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(id))
    {
        return Results.BadRequest(new { error = "id is required" });
    }

    var revoked = await legalHoldService.RevokeHoldAsync(id, reason, ct);
    return revoked is null ? Results.NoContent() : Results.Json(revoked);
});

app.MapGet("/compliance/evidence-packs/{packId}", (
    string packId,
    EvidencePackStore store) =>
{
    var record = store.Get(packId);
    return record is null ? Results.NotFound() : Results.Json(record.Metadata);
});

app.MapGet("/compliance/evidence-packs/{packId}/download", (
    string packId,
    EvidencePackStore store) =>
{
    var record = store.Get(packId);
    if (record is null)
    {
        return Results.NotFound();
    }

    var zipBytes = BuildZip(record);
    return Results.File(zipBytes, "application/zip", $"evidence-pack-{packId}.zip");
});

app.MapGet("/mcp/tools", (GovernedExecutionToolService tools) =>
{
    return Results.Json(new { tools = tools.ListTools() });
});

app.MapPost("/mcp/tools/call", async (
    HttpContext httpContext,
    McpToolCallRequest request,
    GovernedExecutionToolService tools,
    CancellationToken ct) =>
{
    try
    {
        object result;
        switch (request.Name)
        {
            case "keon.governed.execute":
            {
                var executeRequest = DeserializeStrict<GovernedExecuteToolRequest>(request.Arguments, "keon.governed.execute");
                var preflightFailure = TryCreateGovernedExecuteFailure(httpContext, executeRequest);
                if (preflightFailure is not null)
                {
                    return preflightFailure;
                }

                result = await tools.ExecuteAsync(executeRequest, ct);
                break;
            }
            case "keon.runs.get":
                result = await tools.GetRunAsync(
                    DeserializeStrict<RunsGetToolRequest>(request.Arguments, "keon.runs.get"), ct);
                break;
            case "keon.runs.stream":
                result = await tools.StreamAsync(
                    DeserializeStrict<RunsStreamToolRequest>(request.Arguments, "keon.runs.stream"), ct);
                break;
            case "keon.evidence.export":
                result = await tools.ExportEvidenceAsync(
                    DeserializeStrict<EvidenceExportToolRequest>(request.Arguments, "keon.evidence.export"), ct);
                break;
            case "keon.evidence.verify":
                result = await tools.VerifyEvidenceAsync(
                    DeserializeStrict<EvidenceVerifyToolRequest>(request.Arguments, "keon.evidence.verify"), ct);
                break;
            default:
                throw new InvalidOperationException($"Unknown tool '{request.Name}'.");
        }

        return Results.Json(new McpToolCallResponse(request.Name, result));
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new
        {
            error = "tool_call_failed",
            tool = request.Name,
            message = ex.Message
        });
    }
});

// Retention enforcement endpoints
app.MapRetentionEndpoints();

app.Run();

static ReceiptEnvelope MapDecisionReceipt(DecisionReceipt receipt)
    => new(
        receipt.ReceiptId.Value,
        "decision",
        receipt.Context.TenantId.Value,
        receipt.Context.CorrelationId.Value,
        receipt.DecidedAtUtc,
        receipt);

static ReceiptEnvelope MapExecutionReceipt(ExecutionResult receipt)
    => new(
        receipt.ExecutionId.Value,
        "execution",
        receipt.Link.TenantId.Value,
        receipt.Link.CorrelationId.Value,
        receipt.Timing.StartedAt,
        receipt);

static ReceiptEnvelope MapMemoryReceipt(MemoryReceipt receipt)
    => new(
        receipt.ReceiptId.ToString("D"),
        "memory",
        receipt.TenantId.Value.ToString("D"),
        receipt.CorrelationId.ToString("D"),
        receipt.Timestamp,
        receipt);

static int ParseOffset(string? token)
    => int.TryParse(token, out var offset) && offset >= 0 ? offset : 0;

static int ClampPageSize(int? limit)
    => Math.Clamp(limit ?? 50, 1, 200);

static async Task<ReceiptSpine> GetNormalizedSpineAsync(
    IReceiptGraph receiptGraph,
    string correlationId,
    CancellationToken ct)
{
    var spine = await receiptGraph.GetSpineAsync(CorrelationId.From(correlationId), ct);
    return ReceiptGraphNormalizer.Normalize(spine);
}

static List<ReceiptEnvelope> ExtractReceiptEnvelopes(ReceiptSpine spine)
{
    var envelopes = new List<ReceiptEnvelope>();
    foreach (var node in spine.Nodes)
    {
        var envelope = MapReceiptNode(node);
        if (envelope is not null)
        {
            envelopes.Add(envelope);
        }
    }

    return envelopes;
}

static ReceiptEnvelope? MapReceiptNode(ReceiptNode node)
{
    return node.NodeType switch
    {
        ReceiptNodeType.Decision when node.Payload is DecisionReceipt decision => MapDecisionReceipt(decision),
        ReceiptNodeType.Execution when node.Payload is ExecutionResult execution => MapExecutionReceipt(execution),
        ReceiptNodeType.Memory when node.Payload is MemoryReceipt memory => MapMemoryReceipt(memory),
        _ => null
    };
}

static List<T> ExtractPayloads<T>(ReceiptSpine spine, ReceiptNodeType nodeType)
    => spine.Nodes
        .Where(node => node.NodeType == nodeType)
        .Select(node => node.Payload)
        .OfType<T>()
        .ToList();

static List<ExecutionTraceEvent> ExtractTraceEvents(ReceiptSpine spine)
{
    var events = new List<ExecutionTraceEvent>();
    foreach (var node in spine.Nodes.Where(n => n.NodeType == ReceiptNodeType.Trace))
    {
        switch (node.Payload)
        {
            case ExecutionTraceEvent traceEvent:
                events.Add(traceEvent);
                break;
            case IEnumerable<ExecutionTraceEvent> traceEvents:
                events.AddRange(traceEvents);
                break;
        }
    }

    return events;
}

static byte[] BuildZip(EvidencePackRecord record)
{
    using var stream = new MemoryStream();
    using (var archive = new ZipArchive(stream, ZipArchiveMode.Create, leaveOpen: true))
    {
        foreach (var artifact in record.Artifacts.OrderBy(a => a.Name, StringComparer.Ordinal))
        {
            var entry = archive.CreateEntry(artifact.Name, CompressionLevel.Optimal);
            entry.LastWriteTime = artifact.CreatedAtUtc;
            entry.ExternalAttributes = 0;
            using var entryStream = entry.Open();
            entryStream.Write(artifact.Content, 0, artifact.Content.Length);
        }
    }

    return stream.ToArray();
}

static T DeserializeStrict<T>(System.Text.Json.JsonElement arguments, string toolName)
{
    var value = arguments.Deserialize<T>(GovernedExecutionSerializer.JsonOptions);
    return value ?? throw new InvalidOperationException($"Tool '{toolName}' arguments did not match the expected schema.");
}

static IResult? TryCreateGovernedExecuteFailure(HttpContext httpContext, GovernedExecuteToolRequest request)
{
    var hasAuthContext =
        httpContext.Request.Headers.ContainsKey("Authorization") ||
        httpContext.Request.Headers.ContainsKey("X-Keon-Tenant") ||
        httpContext.Request.Headers.ContainsKey("X-Keon-Actor");

    if (!hasAuthContext)
    {
        return null;
    }

    var correlationId = !string.IsNullOrWhiteSpace(request.Controls.CorrelationId)
        ? request.Controls.CorrelationId!
        : $"t:{request.Tenant.Id}|c:preflight-rejected";

    if (!httpContext.Request.Headers.TryGetValue("Authorization", out var authValues) ||
        string.IsNullOrWhiteSpace(authValues.ToString()))
    {
        return BuildGovernedExecuteFailure(
            StatusCodes.Status401Unauthorized,
            correlationId,
            "AUTH_REQUIRED",
            "Bearer authorization is required when Keon identity headers are supplied.");
    }

    var authorization = authValues.ToString();
    if (!authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
    {
        return BuildGovernedExecuteFailure(
            StatusCodes.Status401Unauthorized,
            correlationId,
            "AUTH_INVALID",
            "Authorization must use the Bearer scheme.");
    }

    if (!TryParseJwtClaims(authorization["Bearer ".Length..].Trim(), out var claims))
    {
        return BuildGovernedExecuteFailure(
            StatusCodes.Status401Unauthorized,
            correlationId,
            "AUTH_INVALID",
            "Bearer token could not be parsed.");
    }

    var claimTenant = FirstClaim(claims, "tenant_id", "tenant");
    var claimActor = FirstClaim(claims, "actor_id", "actor", "sub");
    var headerTenant = FirstHeader(httpContext, "X-Keon-Tenant");
    var headerActor = FirstHeader(httpContext, "X-Keon-Actor");

    if ((!string.IsNullOrWhiteSpace(headerTenant) && !string.Equals(headerTenant, claimTenant, StringComparison.Ordinal)) ||
        (!string.IsNullOrWhiteSpace(headerActor) && !string.Equals(headerActor, claimActor, StringComparison.Ordinal)))
    {
        return BuildGovernedExecuteFailure(
            StatusCodes.Status403Forbidden,
            correlationId,
            "IDENTITY_MISMATCH",
            "JWT claims do not match the supplied X-Keon identity headers.");
    }

    var scopes = ParseScopes(claims);
    var requiredScopes = new[]
    {
        "keon:execute",
        $"keon:tool:{request.Intent.Tool}"
    };

    var missingScopes = requiredScopes
        .Where(required => !scopes.Contains(required))
        .ToArray();

    if (missingScopes.Length > 0)
    {
        return BuildGovernedExecuteFailure(
            StatusCodes.Status403Forbidden,
            correlationId,
            "MISSING_SCOPES",
            "Bearer token is missing required execution scopes.",
            missingScopes);
    }

    return null;
}

static IResult BuildGovernedExecuteFailure(
    int statusCode,
    string correlationId,
    string code,
    string message,
    IReadOnlyList<string>? missingScopes = null)
{
    return Results.Json(
        new
        {
            error = code.ToLowerInvariant(),
            isError = true,
            content = new[] { message },
            structuredContent = new
            {
                ok = false,
                correlation_id = correlationId,
                status = "failed",
                stage = "failed",
                failure = new
                {
                    code,
                    message,
                    partial_execution = false,
                    missing_scopes = missingScopes
                }
            },
            receipts = Array.Empty<object>()
        },
        statusCode: statusCode);
}

static bool TryParseJwtClaims(string token, out Dictionary<string, JsonElement> claims)
{
    claims = new Dictionary<string, JsonElement>(StringComparer.Ordinal);
    var parts = token.Split('.');
    if (parts.Length < 2)
    {
        return false;
    }

    try
    {
        var payloadBytes = DecodeBase64Url(parts[1]);
        using var document = JsonDocument.Parse(payloadBytes);
        foreach (var property in document.RootElement.EnumerateObject())
        {
            claims[property.Name] = property.Value.Clone();
        }

        return true;
    }
    catch (Exception)
    {
        return false;
    }
}

static byte[] DecodeBase64Url(string value)
{
    var padded = value.Replace('-', '+').Replace('_', '/');
    padded = (padded.Length % 4) switch
    {
        2 => padded + "==",
        3 => padded + "=",
        _ => padded
    };

    return Convert.FromBase64String(padded);
}

static string? FirstClaim(IReadOnlyDictionary<string, JsonElement> claims, params string[] names)
{
    foreach (var name in names)
    {
        if (claims.TryGetValue(name, out var value) && value.ValueKind == JsonValueKind.String)
        {
            return value.GetString();
        }
    }

    return null;
}

static string? FirstHeader(HttpContext httpContext, string name)
    => httpContext.Request.Headers.TryGetValue(name, out var values) ? values.ToString() : null;

static HashSet<string> ParseScopes(IReadOnlyDictionary<string, JsonElement> claims)
{
    var scopes = new HashSet<string>(StringComparer.Ordinal);

    if (claims.TryGetValue("scope", out var scopeValue) && scopeValue.ValueKind == JsonValueKind.String)
    {
        foreach (var scope in scopeValue.GetString()!.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            scopes.Add(scope);
        }
    }

    if (claims.TryGetValue("scp", out var scpValue) && scpValue.ValueKind == JsonValueKind.Array)
    {
        foreach (var scope in scpValue.EnumerateArray().Where(s => s.ValueKind == JsonValueKind.String).Select(s => s.GetString()))
        {
            if (!string.IsNullOrWhiteSpace(scope))
            {
                scopes.Add(scope!);
            }
        }
    }

    return scopes;
}

public sealed record ReceiptEnvelope(
    string ReceiptId,
    string Kind,
    string TenantId,
    string CorrelationId,
    DateTimeOffset TimestampUtc,
    object Payload);

public sealed record ReceiptPage(IReadOnlyList<ReceiptEnvelope> Items, string? NextPageToken);

public sealed record ExecutionPage(IReadOnlyList<ExecutionResult> Items, string? NextPageToken);
public partial class Program { }

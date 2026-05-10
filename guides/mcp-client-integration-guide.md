# Keon MCP Client Integration Guide

This guide is for teams integrating any MCP-capable client with the Keon MCP Gateway.

The gateway exposes standard MCP tool semantics while enforcing Keon governance at the execution boundary:

```text
MCP client -> Keon MCP Gateway -> Decide -> Execute -> Receipts
```

Thought is free. Effects are governed. The client calls an MCP tool; Keon binds identity, evaluates policy, emits receipts, and only executes after approval.

## Integration contract

Use the MCP transport, not the legacy direct HTTP invoke surface.

| Purpose | Value |
| --- | --- |
| Remote transport | Streamable HTTP |
| MCP endpoint | `POST /mcp` |
| Local transport | stdio with `--stdio` |
| Primary tool | `keon.governed.execute.v1` |
| List tools method | `tools/list` |
| Call tool method | `tools/call` |
| Result data | MCP `result.structuredContent` |

Supported protocol versions:

- `2025-11-25`
- `2025-06-18`
- `2025-03-26`

Unless Keon provides a different tenant-specific URL, use the assigned gateway base URL and append `/mcp`.

## Required headers for HTTP MCP clients

Bearer-token mode:

```http
Authorization: Bearer <token>
MCP-Protocol-Version: 2025-06-18
Content-Type: application/json
```

API-key mode:

```http
X-Api-Key: <api-key>
X-Keon-Tenant-Id: <tenant-id>
X-Keon-Actor-Id: <actor-id>
MCP-Protocol-Version: 2025-06-18
Content-Type: application/json
```

Recommended optional headers:

```http
X-Correlation-Id: <stable-correlation-id>
X-Idempotency-Key: <stable-idempotency-key>
```

If `X-Idempotency-Key` is absent, the gateway may derive one from the MCP request id and correlation id.

## Required scopes

For tool discovery:

- `keon:mcp:list`

For governed execution:

- `keon:mcp:invoke`
- `keon:execute`

For hardening attestation tools, Keon may also require:

- `keon:attest`

The gateway fails closed if tenant, actor, key, token, quota, or scope binding cannot be verified.

## Initialize session

Every MCP client should initialize before listing or calling tools.

```json
{
  "jsonrpc": "2.0",
  "id": 1,
  "method": "initialize",
  "params": {
    "protocolVersion": "2025-06-18",
    "clientInfo": {
      "name": "your-client-name",
      "version": "1.0.0"
    },
    "capabilities": {}
  }
}
```

## List tools

```json
{
  "jsonrpc": "2.0",
  "id": "tools-list-1",
  "method": "tools/list",
  "params": {}
}
```

Expected response shape:

```json
{
  "jsonrpc": "2.0",
  "id": "tools-list-1",
  "result": {
    "tools": [
      {
        "name": "keon.governed.execute.v1",
        "description": "Universal governed execution adapter...",
        "inputSchema": {}
      }
    ]
  }
}
```

## Call `keon.governed.execute.v1`

Use MCP `tools/call` and put the governed execution payload in `params.arguments`.

```json
{
  "jsonrpc": "2.0",
  "id": "call-1",
  "method": "tools/call",
  "params": {
    "name": "keon.governed.execute.v1",
    "arguments": {
      "purpose": "Summarize recent sent emails for weekly status update",
      "action": "summarize",
      "resource": {
        "type": "email",
        "scope": "mailbox:sent"
      },
      "params": {
        "window_days": 7,
        "max_items": 25
      },
      "mode": "decide_then_execute"
    }
  }
}
```

### Governed execute arguments

| Field | Required | Description |
| --- | --- | --- |
| `action` | Yes | Requested effect, for example `summarize`, `retrieve`, `create`, `update`, `delete`, `send`, `classify`, or `execute`. |
| `resource.type` | Yes | Resource class, for example `email`, `file`, `ticket`, `crm_record`, or another agreed resource type. |
| `resource.id` | No | Stable resource identifier when available. |
| `resource.scope` | No | Resource scope, for example `mailbox:sent`, `tenant:<id>`, or `project:<id>`. |
| `resource.labels` | No | Classification or routing labels. |
| `params` | Yes | Action-specific parameters. |
| `purpose` | Recommended | Human-readable purpose; required by policy for high-risk effects. |
| `mode` | No | `decide_then_execute` by default. Use `decide_only` for policy preflight. |

## Result handling

`tools/call` returns a standard MCP tool result. Use:

- `result.content` for human-readable text
- `result.structuredContent` for Keon governance data
- `result.isError` to detect tool-level failure

Example success:

```json
{
  "jsonrpc": "2.0",
  "id": "call-1",
  "result": {
    "content": [
      {
        "type": "text",
        "text": "keon.governed.execute.v1 completed with decision approved. Receipts are available in structuredContent."
      }
    ],
    "structuredContent": {
      "correlation_id": "c01J9Z8Q6X4J5Y2P9H3K8M7N6",
      "tool": "keon.governed.execute.v1",
      "ok": true,
      "decision": {
        "status": "approved",
        "policy_hash": "sha256:..."
      },
      "result": {},
      "receipts": {
        "directive": "rcpt_dir_...",
        "intent": "rcpt_int_...",
        "request": "rcpt_req_...",
        "decision": "rcpt_dec_...",
        "execution": "rcpt_exe_...",
        "outcome": "rcpt_out_...",
        "evidence_pack": null
      }
    },
    "isError": false
  }
}
```

Example denial:

```json
{
  "result": {
    "structuredContent": {
      "ok": true,
      "decision": {
        "status": "denied",
        "policy_hash": "sha256:...",
        "reason_code": "POLICY_DENIED"
      },
      "result": null,
      "receipts": {
        "execution": null
      }
    },
    "isError": false
  }
}
```

A denied decision is a governed terminal outcome, not a transport failure. It should be handled as a successful MCP response with `decision.status = "denied"` and no execution receipt.

Example tool error:

```json
{
  "result": {
    "content": [
      {
        "type": "text",
        "text": "MCP_SCOPE_DENIED: Missing required scope: keon:execute"
      }
    ],
    "structuredContent": {
      "ok": false,
      "error": {
        "code": "MCP_SCOPE_DENIED",
        "message": "Missing required scope: keon:execute",
        "http_status": 403,
        "retryable": false
      }
    },
    "isError": true
  }
}
```

## stdio integration

For local MCP clients that launch a server process, run the gateway with `--stdio`.

```json
{
  "type": "stdio",
  "command": "dotnet",
  "args": [
    "<path-to>/Keon.McpGateway.dll",
    "--stdio"
  ],
  "env": {
    "KEON_MCP_BEARER_TOKEN": "<token>"
  }
}
```

Supported stdio environment variables:

- `KEON_MCP_BEARER_TOKEN`
- `KEON_MCP_API_KEY`
- `KEON_MCP_TENANT_ID`
- `KEON_MCP_ACTOR_ID`

Use API-key mode with tenant and actor environment variables when the MCP client cannot attach custom headers.

## Client obligations

Integrating clients must:

1. Call `initialize` before tool use.
2. Use `tools/list` to discover available tools.
3. Use `tools/call` for governed execution.
4. Preserve `correlation_id` and receipt ids for audit and support.
5. Treat `decision.status = "denied"` as a valid governed outcome.
6. Treat `isError = true` as a tool-level failure.
7. Retry only when the returned error marks `retryable = true` or the transport failure is clearly transient.

## What not to do

Do not bypass the MCP server by calling downstream runtime execution directly.

Do not interpret cognition or model output as execution authority. The only lawful effect boundary is:

```text
MCP Gateway -> Decide -> Execute -> Receipts
```

Do not rely on the legacy `/mcp/tools/invoke` endpoint for new MCP-client integrations unless Keon explicitly instructs you to use the compatibility surface.
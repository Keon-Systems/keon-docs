# SDK Method Parity Matrix

> *Tracking feature implementation across all Keon SDKs*

This document shows which core methods are implemented in each Keon SDK.

---

## Core Runtime Methods

| Method | Python | TypeScript | C# | Go | Description |
|--------|--------|------------|-----|-----|-------------|
| `decide()` | Yes | Yes | Yes | Yes | Request policy decision |
| `execute()` | Yes | Yes | Yes | Yes | Execute action with receipt |
| `decideAndExecute()` | Yes | Yes | Yes | Yes | Convenience: decide + execute |

All core runtime methods are fully implemented across all four SDKs.

---

## Retry Policies

| Feature | Python | TypeScript | C# | Go |
|---------|--------|------------|-----|-----|
| Default retry | Yes | Yes | Yes | Yes |
| Conservative retry | Yes | Yes | Yes | Yes |
| No retry | Yes | Yes | Yes | Yes |
| Custom retry | Yes | Yes | **No** | **No** |

**Note:** C# and Go SDKs intentionally restrict custom retry policies to prevent misconfiguration. Use the provided presets (`Default`, `Conservative`, `NoRetry`).

---

## Advanced Features

| Feature | Python | TypeScript | C# | Go | Status |
|---------|--------|------------|-----|-----|--------|
| Async/await | Yes | Yes | Yes | Context-based | Available |
| Batch operations | No | No | Yes | No | C# only |
| Receipt tracking | Yes | Yes | Yes | No | Partial |
| Custom gateway | Yes | Yes | Yes | Yes | Available |

---

## Planned Features (Coming Soon)

The following methods are planned for future releases:

| Method | Python | TypeScript | C# | Go | Target |
|--------|--------|------------|-----|-----|--------|
| `registerAsset()` | Planned | Planned | Planned | Planned | Q2 2026 |
| `verifyEvidence()` | Planned | Planned | Planned | Planned | Q2 2026 |
| `getReceipt()` | Planned | Planned | Planned | Planned | Q2 2026 |
| `listDecisions()` | Planned | Planned | Planned | Planned | Q3 2026 |

---

## Error Types

All SDKs implement the same error taxonomy:

| Error Type | Python | TypeScript | C# | Go |
|------------|--------|------------|-----|-----|
| `InvalidCorrelationIdError` | Yes | Yes | Yes | Yes |
| `MissingReceiptError` | Yes | Yes | Yes | Yes |
| `ExecutionDeniedError` | Yes | Yes | Yes | Yes |
| `NetworkError` | Yes | Yes | Yes | Yes |
| `ServerError` | Yes | Yes | Yes | Yes |
| `ValidationError` | Yes | Yes | Yes | Yes |
| `RetryExhaustedError` | Yes | Yes | Yes | Yes |

---

## Testing Support

| Feature | Python | TypeScript | C# | Go |
|---------|--------|------------|-----|-----|
| Mock gateway | Yes | Yes | Yes | Yes |
| In-memory runtime | No | No | Yes | No |
| Test fixtures | Yes | Yes | Yes | Yes |

---

## Version Compatibility

| SDK | Minimum Runtime | Package Version |
|-----|-----------------|-----------------|
| Python | Python 3.11+ | 1.0.0 |
| TypeScript | Node 18+ | 1.0.0 |
| C# | .NET 8.0+ | 1.0.0 |
| Go | Go 1.21+ | 1.0.0 |

---

*Last updated: 2026-02-13*

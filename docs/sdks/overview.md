# Keon SDK Overview

> *Governance-first SDKs for the Keon Platform*

The Keon SDKs provide safe-by-default clients for interacting with the Keon Runtime. All SDKs enforce the core governance principles:

- **Law before execution** - Every action requires a policy decision
- **Execute requires receipt** - Hard fail if `DecisionReceiptId` is absent
- **CorrelationId is mandatory** - Canonical format: `t:<TenantId>|c:<uuidv7>`
- **Bounded retries** - Automatic retries for transient failures with exponential backoff

---

## Supported Environments

| Language | Keon (Governance) | OMEGA (Intelligence) | Notes |
|----------|-------------------|----------------------|-------|
| **Python** | Yes | Yes | Full feature parity |
| **TypeScript** | Yes | Yes | Full feature parity |
| **C#/.NET** | Yes | Yes | Full feature parity |
| **Go** | Yes | **No** | Governance layer only |

### Why Go is Keon-Only

The Go SDK is specifically designed for the **governance layer** where performance and determinism are critical. Go's characteristics make it ideal for:

- Policy enforcement engines
- High-throughput decision services
- Infrastructure-level governance

OMEGA's intelligence layer focuses on different concerns (AI orchestration, model routing, adaptive execution) that are better served by Python, TypeScript, and C# ecosystems.

---

## SDK Repositories

| SDK | Repository | Package |
|-----|------------|---------|
| Python | [keon-sdk-python](https://github.com/Keon-Systems/keon-sdk-python) | `pip install keon-sdk` |
| TypeScript | [keon-sdk-ts](https://github.com/Keon-Systems/keon-sdk-ts) | `npm install @keon/sdk` |
| C# | [keon-sdk-cs](https://github.com/Keon-Systems/keon-sdk-cs) | `dotnet add package Keon.Sdk` |
| Go | [keon-sdk-go](https://github.com/Keon-Systems/keon-sdk-go) | `go get github.com/Keon-Systems/keon-sdk-go` |

---

## Getting Started

1. Choose the SDK for your language
2. Install the package using your language's package manager
3. Configure the client with your API credentials
4. Start with a `decide()` call before any `execute()` call

All SDKs follow the same pattern:

```
Decide → Receipt → Execute → Result
```

See individual SDK documentation for language-specific examples and API references.

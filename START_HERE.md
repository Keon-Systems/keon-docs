# START_HERE: Welcome to Keon Systems

**30-second summary**:
Keon is a governance substrate that proves AI decisions — before, during, and after execution. Policy enforced at the infrastructure level. Every action produces evidence.

---

## What Are You Here to Do?

Choose your path based on what matters most to you:

### 🔍 **I want to verify Keon works** (2–5 minutes)
→ [Evidence Pack Tour](https://keon-systems.vercel.app/evidence-pack-tour) (no signup required)

See real governance proof in action. Interactive, zero-friction verification of actual decisions made by OMEGA using Keon.

### 📚 **I want to understand how Keon works** (15–30 minutes)
→ [Whitepaper: Governed Execution for Operational AI](./whitepaper/INDEX.md)

Read the canonical design document. Explains the problem Keon solves, the architecture, and why this matters for enterprises deploying operational AI.

### 🛠️ **I want to build with Keon** (30+ minutes)
→ Choose your SDK:
- [C# SDK](https://github.com/m0r6aN/keon-sdk-cs) (production-ready, recommended)
- [Go SDK](https://github.com/m0r6aN/keon-sdk-go)
- [Python SDK](https://github.com/m0r6aN/keon-sdk-python)
- [TypeScript SDK](https://github.com/m0r6aN/keon-sdk-ts)

Each SDK includes examples and quick-start guides to integrate Keon into your applications.

### ✅ **I need to verify claims/audit** (1–2 hours)
→ [Claims Registry & Proof Map](#governance-claims-registry-below)

Organizations with compliance requirements. Every public claim about Keon is registered, versioned, and linked to verifiable proof artifacts.

---

## What Is Keon?

### The Problem
- AI has crossed from advisory to operational
- Enterprises deploy faster than they can control
- Existing governance patterns fail at the infrastructure level
- No reliable way to verify what AI actually did

### The Solution
A governance substrate that sits below the application layer and enforces policy at runtime:

```
┌─────────────────────────────────────────┐
│  Your Applications (Agents, Workflows)  │
├─────────────────────────────────────────┤
│  Keon Governance Layer (This)           │
│  • Policy enforcement                   │
│  • Decision gating                       │
│  • Evidence generation                  │
├─────────────────────────────────────────┤
│  Execution Substrate (Federation Core)  │
│  • Secure compute                       │
│  • Encrypted storage                    │
│  • Tamper-evident audit log            │
└─────────────────────────────────────────┘
```

### What Keon Produces
1. **Evidence Packs**: Tamper-evident bundles proving:
   - What policy governed a decision
   - Who had authority to approve it
   - What the decision was
   - What happened as a result

2. **Decision Receipts**: Cryptographic proof that a specific decision was made under specific policy

3. **Audit Trails**: Complete history of governance decisions, never rewritten

### Who It's For
- **Enterprises** deploying operational AI (agents, autonomous workflows)
- **Regulated industries** (financial, healthcare, government)
- **Teams that can't afford unverifiable AI**

---

## Core Concepts

### Evidence Pack
The fundamental unit of proof in Keon. A tamper-evident bundle containing:
- **Decision**: What the AI was asked to do
- **Policy**: The governance rules that applied
- **Authority**: Who approved the decision (ALPHA system)
- **Execution**: What actually happened
- **Proof**: Cryptographic receipt proving all of the above

Evidence Packs are human-readable, machine-verifiable, and immutable.

### ALPHA (Authority Layer for Policy Handling)
The human decision point in Keon. When a policy requires human approval:
1. AI submits a decision request
2. ALPHA presents the decision to authorized human(s)
3. Human approves or rejects with explicit consent
4. System records decision + human approval in Evidence Pack
5. Approved decision executes with full provenance

This ensures AI always operates under human authority, never autonomously overriding governance.

### Proof Map
Registry linking every claim Keon makes to verifiable artifacts:
- Claims about functionality → executable tests
- Claims about performance → benchmark results
- Claims about security → threat models and mitigations

If it's claimed, it can be proven. If it's not here, it's not claimed.

---

## Repository Structure

```
keon-docs/
├── START_HERE.md (you are here)
├── README.md (governance principles)
├── whitepaper/ (versioned design documents)
│   ├── INDEX.md (current version: v1.0)
│   └── v1.0/ (January 2026 snapshot)
├── canon/ (source of truth)
│   ├── CLAIMS_REGISTRY.yaml (all public claims)
│   ├── PROOF_MAP.yaml (how to verify each claim)
│   └── FEATURES_BY_PHASE.yaml (implementation timeline)
└── content/ (reconciliation ledger - drift tracking)
```

### Key Directories

**`whitepaper/`**: Versioned snapshots of Keon's design document
- v1.0 (January 2026, current)
- Future versions as they're released
- Immutable versioning: no edits, only new versions

**`canon/`**: Governance claims registry
- CLAIMS_REGISTRY.yaml: Every claim about Keon's capabilities, with proof requirements
- PROOF_MAP.yaml: Links each claim to verifiable artifacts (tests, code, examples)
- FEATURES_BY_PHASE.yaml: Implementation timeline (when features ship)

**`content/`**: Reconciliation ledger
- Tracks drift between claimed and actual state
- Never silently corrected; changes appended as new entries
- Demonstrates governance is real, not marketing fiction

---

## Getting Started: Multiple Paths

### Path 1: Verify It Works (Fastest — 2 minutes)

1. Go to [https://keon-systems.vercel.app/evidence-pack-tour](https://keon-systems.vercel.app/evidence-pack-tour)
2. No signup, no downloads
3. See real Evidence Pack from OMEGA
4. Verify cryptographic receipt
5. Done

**Result**: You've seen actual governance proof.

---

### Path 2: Understand the Architecture (15 minutes)

1. Read [Whitepaper: Governed Execution for Operational AI](./whitepaper/INDEX.md)
2. Focus on sections:
   - "Why Existing Patterns Fail" (understand the problem)
   - "The Governance Substrate" (how Keon works)
   - "Evidence Packs & Proof" (what you get)
3. Reference the [Stack Overview](#what-is-keon) above

**Result**: You understand why Keon exists and how it's different.

---

### Path 3: Build with Keon (30+ minutes)

#### Step 1: Choose Your Language & SDK

- **C# (Recommended)**: [keon-sdk-cs](https://github.com/m0r6aN/keon-sdk-cs)
  - Most complete implementation
  - Used in production by OMEGA
  - Rich examples included

- **Go**: [keon-sdk-go](https://github.com/m0r6aN/keon-sdk-go)
  - High-performance operations
  - Low latency governance gates

- **Python**: [keon-sdk-python](https://github.com/m0r6aN/keon-sdk-python)
  - Data science workflows
  - ML pipeline integration

- **TypeScript**: [keon-sdk-ts](https://github.com/m0r6aN/keon-sdk-ts)
  - Node.js and browser environments
  - Real-time agent orchestration

#### Step 2: Follow SDK Quick Start

Each SDK repo includes:
- `README.md` with setup instructions
- `examples/` directory with working code
- `tests/` with integration examples
- API documentation

#### Step 3: See It in Production

Check how OMEGA uses Keon:
- [OMEGA Repository](https://github.com/m0r6aN/OMEGA)
- Demonstrates real governance patterns
- Shows Evidence Pack creation in practice

---

### Path 4: Verify Claims & Audit (60+ minutes)

For compliance teams, security audits, or verification requirements:

#### Step 1: Understand the Claims
Review `canon/CLAIMS_REGISTRY.yaml`:
- Lists every claim Keon makes about its capabilities
- Each claim has an ID (e.g., `KS-DECIDE-001`)
- Links to where it's documented publicly

#### Step 2: Find the Proof
Review `canon/PROOF_MAP.yaml`:
- For each claim ID, shows exactly how to verify it
- Includes CLI commands to run
- Lists artifacts and repositories to inspect

#### Step 3: Verify Independently
```bash
# Example: Verify a Decision Receipt
keon verify-pack ./samples/decision-pack.json

# Example: Verify an Evidence Pack
keon verify-pack ./samples/evidence-pack.json
```

#### Step 4: Check Implementation Timeline
Review `canon/FEATURES_BY_PHASE.yaml`:
- Confirms when features were implemented
- Links to repositories and code paths
- Shows proof tags for traceability

**Result**: You can independently audit Keon's claims.

---

## Governance Claims Registry (Below)

Below is a snapshot of Keon's public claims registry. For the current version, see:
[canon/CLAIMS_REGISTRY.yaml](./canon/CLAIMS_REGISTRY.yaml)

### Active Claims

#### KS-DECIDE-001: Deterministic Decision Gateway
**What**: Keon provides a deterministic runtime decision gateway that emits cryptographically verifiable receipts for every decision.

**Proof**:
- Implementation: `keon-runtime/src/Keon.Runtime.Decide`
- Verification: Run `keon verify-pack ./samples/decision-pack.json`
- Evidence: `samples/decision-pack.json` + `receipts/receipt.003.decision.v1.json`
- Repositories: keon-runtime, keon-verify

#### KS-EVIDENCE-004: Evidence Pack Verification CLI
**What**: Users can independently verify Evidence Packs using the Keon CLI.

**Proof**:
- CLI Command: `keon verify-pack ./samples/evidence-pack.json`
- Implementation: `keon-cli/src/Commands/Verify`
- Evidence: See `samples/evidence-pack.json`
- Repositories: keon-cli, keon-verify

---

## Implementation Timeline

Below is a snapshot of Keon's implementation phases. For the current timeline, see:
[canon/FEATURES_BY_PHASE.yaml](./canon/FEATURES_BY_PHASE.yaml)

### Phase 4: Evidence Generation ✅
- Evidence Pack v1 (Implemented)
  - Repository: `keon-core`
  - Path: `src/Keon.Evidence`
  - Tags: `keon-phase4-evidence-pack-v1`

### Phase 5: Runtime Governance ✅
- Decision Gateway (Implemented)
  - Repository: `keon-runtime`
  - Path: `src/Keon.Runtime.Decide`
  - Tags: `keon-phase5-runtime-gateway-v1`

---

## Key Repositories

| Repo | Purpose | Audience |
|------|---------|----------|
| [keon-docs](https://github.com/m0r6aN/keon-docs) | Public claims, whitepaper, verification | Everyone |
| [keon-systems-web](https://github.com/m0r6aN/keon-systems-web) | Public website & Evidence Pack tour | Decision makers, technical buyers |
| [keon-sdk-cs](https://github.com/m0r6aN/keon-sdk-cs) | C# SDK (production-ready) | Engineers (C#) |
| [keon-sdk-go](https://github.com/m0r6aN/keon-sdk-go) | Go SDK | Engineers (Go) |
| [keon-sdk-python](https://github.com/m0r6aN/keon-sdk-python) | Python SDK | Engineers (Python/ML) |
| [keon-sdk-ts](https://github.com/m0r6aN/keon-sdk-ts) | TypeScript SDK | Engineers (Node/Browser) |
| [OMEGA](https://github.com/m0r6aN/OMEGA) | Agent orchestration (uses Keon) | Production example, integration patterns |
| [federation-core](https://github.com/m0r6aN/federation-core) | Secure execution substrate | Infrastructure engineers |

---

## FAQ

### Q: How is Keon different from audit logging?
**A**: Audit logging records what happened after the fact. Keon enforces policy before decisions execute and produces tamper-evident proof of the enforcement. It's policy-first, not logging-first.

### Q: Do I need to trust Keon's implementation?
**A**: No. Every claim Keon makes is in this repository (`keon-docs`), linked to verifiable proof. You can independently verify the claims using the Proof Map.

### Q: Can I use Keon with existing AI frameworks?
**A**: Yes. Keon's SDKs integrate into your existing code. Choose your language SDK and follow the quick-start guide.

### Q: What if I want to understand the implementation details?
**A**: The implementation is in keon-systems (private). But all public claims are verifiable through the Evidence Pack Tour or via the SDK examples.

### Q: How do I report a vulnerability?
**A**: See SECURITY.md in the root of this repository.

---

## Next Steps

1. **Quickest verification**: Visit [Evidence Pack Tour](https://keon-systems.vercel.app/evidence-pack-tour)
2. **Understand the why**: Read [Whitepaper](./whitepaper/INDEX.md)
3. **Start building**: Pick an [SDK](#path-3-build-with-keon-30-minutes) for your language
4. **Verify claims**: Review [canon/](./canon/) directory
5. **See it in action**: Explore [OMEGA](https://github.com/m0r6aN/OMEGA)

---

## Governance & Versioning

This document is part of the Keon Public Documentation repository.

- **Versioned**: Each public claim is versioned separately in `canon/`
- **Immutable**: Once published, claims cannot be edited (only new versions added)
- **Governed**: Changes are tracked in the reconciliation ledger (`content/`)
- **Verifiable**: Every claim links to proof artifacts

**Current version**: v1.0 (January 2026)

For questions or issues, open an issue in this repository. For security concerns, see SECURITY.md.

---

**Law before power. Proof before trust.**

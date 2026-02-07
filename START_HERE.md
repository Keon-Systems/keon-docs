# START_HERE: Welcome to Keon Systems

**30-second summary**: Keon is a governance substrate that proves AI decisions — before, during, and after execution. Policy is enforced at the infrastructure boundary. Every governed decision produces verifiable evidence.

---

## What Is Keon?

Keon is designed for governing AI and LLM-driven systems where decisions must be provable, auditable, and regulator-ready.

AI has crossed from advisory to operational. Enterprises deploy faster than they can control. Existing governance patterns fail at the infrastructure level.

Keon introduces a different model:

> **Execution proposes. Governance decides. Receipts prove.**
```
┌─────────────────────────────────────────┐
│  Your Applications (Agents, Workflows)  │
├─────────────────────────────────────────┤
│  Keon Governance Layer                  │
│  • Policy enforcement                   │
│  • Decision gating                      │
│  • Evidence generation                  │
├─────────────────────────────────────────┤
│  Execution Substrate (Federation Core)  │
│  • Secure compute                       │
│  • Encrypted storage                    │
│  • Tamper-evident audit log             │
└─────────────────────────────────────────┘
```

Keon does not attempt to "explain" LLM internals. Instead, it governs **decision boundaries** around AI execution and answers questions such as:

- Under which policy was this output allowed?
- Where did human approval intervene?
- What constraints shaped the outcome?
- What evidence proves the decision path?

This shifts explainability from model introspection to **governance accountability**.

---

## Key Surfaces

### Keon Control (Governance Surface)

**Definition**
- A human governance surface
- A cross-application view
- A forensic & audit tool
- A policy lifecycle manager
- A receipt and evidence explorer

**Target Audience**
- Enterprise administrators
- Compliance officers
- Security teams
- Legal / audit
- Platform owners

**Design Philosophy**
- Out-of-band by design
- Enterprise-facing
- Similar to AWS Console / Okta Admin
- Not embedded in SaaS apps

**The Iron Rule**
- Keon never executes. Keon decides.
- Keon is a governance authority surface, not an application feature.
- Applications observe governance, never exercise it.

---

## Choose Your Path

| Goal | Time | Start Here |
|------|------|------------|
| **Verify it works** | 2–5 min | [Evidence Pack Tour](https://keon-systems.vercel.app/evidence-pack-tour) — no signup required |
| **Understand the architecture** | 15–30 min | [Whitepaper: Governed Execution for Operational AI](./whitepaper/INDEX.md) |
| **Build with Keon** | 30+ min | [SDK Quick Start](#build-with-keon) |
| **Audit claims** | 1–2 hrs | [Claims Registry](#governance-claims-registry) |

---

## Core Concepts

### Evidence Pack

The fundamental unit of proof. A tamper-evident bundle containing:

- **Decision**: What the AI was asked to do
- **Policy**: The governance rules that applied
- **Authority**: Who approved the decision (ALPHA system)
- **Execution**: What actually happened
- **Proof**: Cryptographic receipt proving all of the above

Evidence Packs are human-readable, machine-verifiable, and immutable. Sealed packs are stored in the **[Keon Evidence Vault][vault]** (append-only, write-once, read-only).

### ALPHA (Authority Layer for Policy Handling)

The human decision point. When policy requires human approval:

1. AI submits a decision request
2. ALPHA presents the decision to authorized human(s)
3. Human approves or rejects with explicit consent
4. System records decision + approval in Evidence Pack
5. Approved decision executes with full provenance

AI always operates under human authority, never autonomously overriding governance.

---

## Build with Keon

### Choose Your SDK

| Language | Repository | Notes |
|----------|------------|-------|
| **C#** (recommended) | [keon-sdk-cs](https://github.com/m0r6aN/keon-sdk-cs) | Production-ready, used by OMEGA |
| **Go** | [keon-sdk-go](https://github.com/m0r6aN/keon-sdk-go) | High-performance, low latency |
| **Python** | [keon-sdk-python](https://github.com/m0r6aN/keon-sdk-python) | Data science, ML pipelines |
| **TypeScript** | [keon-sdk-ts](https://github.com/m0r6aN/keon-sdk-ts) | Node.js, browser, real-time agents |

Each SDK includes setup instructions, working examples, integration tests, and API documentation.

### See It in Production

[OMEGA](https://github.com/m0r6aN/OMEGA) demonstrates real governance patterns and Evidence Pack creation in practice.

---

## Governance Claims Registry

Every claim Keon makes is registered, versioned, and linked to verifiable proof.

### Active Claims

**KS-DECIDE-001: Deterministic Decision Gateway**
- Keon provides a deterministic runtime decision gateway emitting cryptographically verifiable receipts
- Implementation: `keon-runtime/src/Keon.Runtime.Decide`
- Verification: `keon verify-pack ./samples/decision-pack.json`

**KS-EVIDENCE-004: Evidence Pack Verification CLI**
- Users can independently verify Evidence Packs using the Keon CLI
- Implementation: `keon-cli/src/Commands/Verify`
- Verification: `keon verify-pack ./samples/evidence-pack.json`

Full registry: [canon/CLAIMS_REGISTRY.yaml](./canon/CLAIMS_REGISTRY.yaml)
Proof artifacts: [canon/PROOF_MAP.yaml](./canon/PROOF_MAP.yaml)

---

## Repository Structure
```
keon-docs/
├── START_HERE.md          # You are here
├── whitepaper/            # Versioned design documents (v1.0 current)
├── canon/
│   ├── CLAIMS_REGISTRY.yaml   # All public claims
│   ├── PROOF_MAP.yaml         # Verification artifacts
│   └── FEATURES_BY_PHASE.yaml # Implementation timeline
└── content/               # Reconciliation ledger (drift tracking)
```

### Key Repositories

| Repo | Purpose |
|------|---------|
| [keon-docs](https://github.com/m0r6aN/keon-docs) | Public claims, whitepaper, verification |
| [keon-systems-web](https://github.com/m0r6aN/keon-systems-web) | Website & Evidence Pack tour |
| [OMEGA](https://github.com/m0r6aN/OMEGA) | Production example using Keon |
| [federation-core](https://github.com/m0r6aN/federation-core) | Secure execution substrate |

---

## FAQ

**How is Keon different from audit logging?**
Audit logging records what happened after the fact. Keon enforces policy before decisions execute and produces tamper-evident proof of enforcement.

**Do I need to trust Keon's implementation?**
No. Every claim is linked to verifiable proof. You can independently verify using the Proof Map.

**Can I use Keon with existing AI frameworks?**
Yes. SDKs integrate into existing code. Choose your language and follow the quick-start.

---

## Governance & Versioning

- **Versioned**: Each public claim is versioned separately in `canon/`
- **Immutable**: Published claims cannot be edited, only superseded
- **Governed**: Changes tracked in reconciliation ledger
- **Verifiable**: Every claim links to proof artifacts

**Current version**: v1.0 (January 2026)

For questions, open an issue. For security concerns, see SECURITY.md.

[vault]: https://github.com/m0r6aN/keon-evidence-vault

---

**Law before power. Proof before trust.**

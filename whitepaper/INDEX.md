# Keon Systems Whitepaper — Canonical Index

## Current Version
**Cryptographically Governed AI Execution (CGAE) — v1.0.0**
Published: February 12, 2026
Status: Canonical
Distribution: Public

[Read CGAE v1.0.0 →](../docs/whitepapers/cgae/v1.0.0.md)

---

## Versioning Policy

This document maintains a canonical record of all published whitepaper versions.

**Version Format**: `MAJOR.MINOR`
- **MAJOR** increments indicate architectural changes or new core concepts
- **MINOR** increments indicate clarifications, addenda, or non-breaking refinements

**Immutability**: Once published, previous versions are never modified. Changes are tracked in [CHANGELOG.md](./CHANGELOG.md).

---

## Version History

### v1.0.0 — Cryptographically Governed AI Execution
**Published**: February 12, 2026
**Status**: Canonical
**Availability**: [docs/whitepapers/cgae/v1.0.0.md](../docs/whitepapers/cgae/v1.0.0.md)

Defines the new CGAE category: every AI-initiated action is evaluated against enforceable policy before execution, bound cryptographically to its governing decision, scoped to an explicit tenant & authority context, and emitted as deterministic evidence. This is the canonical reference for Keon’s governance substrate.

**Key Claims**:
- Policy is enforced at the substrate, not in application code, with required receipts before execution.
- Every action produces portable Evidence Packs with lineage, attribution, and cost.
- Human and machine authorities remain attributable via cryptographic receipts, and execution is verifiable long after the fact.

### v1.0 — Governed Execution Foundation
**Published**: January 2026
**Status**: Archived
**Availability**: [docs/whitepapers/archive/governed-execution-v1.0.md](../docs/whitepapers/archive/governed-execution-v1.0.md)
**PDF**: [governed_execution_whitepaper_v1.0.pdf](../whitepaper.pdf)
**Superseded by**: [CGAE v1.0.0](../docs/whitepapers/cgae/v1.0.0.md)

Defines the core thesis and architectural framework for governed execution:
- Why authorization, not intelligence, is the hard problem
- The liability shift from Read-Only to Read-Write AI
- Why existing patterns (logs, monitoring, API keys) fail
- The Keon Core → Runtime → Control architecture
- Evidence Packs and verifiable proof

**Key Claims**:
- Policy is enforced at the substrate, not in application code
- Every action produces receipts with lineage, attribution, and cost
- Humans approve, ratify, and intervene — with evidence

---

## Addenda

### v1.0 Addendum A — TrustOps & Revocation Semantics
**Published**: 2026-02-07
**Status**: Incorporated
**Reference**: [CHANGELOG.md](./CHANGELOG.md)

Clarifies:
- Decision Gateway receipt schema changes (Phase 5)
- TrustOps revocation semantics overview
- Non-breaking refinements to v1.0 claims

### v1.1 Addendum — Lifecycle Governance & Policy Automation
**Published**: 2026-02-08
**Status**: Current
**Reference**: [CHANGELOG.md](./CHANGELOG.md)

Extends the whitepaper with:
- **Lifecycle Governance** — Governed birth (PT-014), governed death (PT-016), and governed automation (PT-017)
- **Policy Automation** — Severity gradation, human gate enforcement, cooldown periods, fail-closed ambiguity
- **Constitutional Execution** — Birth, death, and automation are all governed, attributed, and provable
- **7 Sealed Proof Campaigns** — Independently verifiable evidence bundles with SHA256-hashed manifests

**Key Claims Added**:
- KS-LIFECYCLE-014: Governed Birth
- KS-LIFECYCLE-016: Governed Death
- KS-LIFECYCLE-017: Revocation Policy Automation
- KS-ROUTE-003: Agent Registry & Capability Routing
- KS-WORKFLOW-004: Workflow Orchestration Execution Spine
- KS-GATE-005: Human-in-the-Loop Gates
- KS-COLLAB-013: Multi-Titan Collaboration

---

## How to Reference

**For direct links**:
```
Canonical Whitepaper: https://github.com/keon-systems/keon-docs/blob/main/docs/whitepapers/cgae/v1.0.0.md
Current Version: https://github.com/keon-systems/keon-docs/blob/main/docs/whitepapers/cgae/v1.0.0.md
Archived — Governed Execution for Operational AI v1.1: https://github.com/keon-systems/keon-docs/blob/main/docs/whitepapers/archive/governed-execution-v1.0.md
Claims Registry: https://github.com/keon-systems/keon-docs/blob/main/canon/CLAIMS_REGISTRY.yaml
Proof Map: https://github.com/keon-systems/keon-docs/blob/main/canon/PROOF_MAP.yaml
```

**For citations**:
```
Keon Systems Technical Whitepaper v1.0.0
"Cryptographically Governed AI Execution (CGAE)"
February 2026
https://github.com/keon-systems/keon-docs/blob/main/docs/whitepapers/cgae/v1.0.0.md
```

---

## Verification

All claims in the whitepaper are subject to the governance rules defined in [canon/](../canon/).

If it's stated in the whitepaper, it must be provable. [KS-EVIDENCE-004] Run `npm run claims:drift` to verify claim integrity.

Full proof campaign evidence: [OMEGA Proof Campaign Status](https://github.com/m0r6aN/omega-docs/blob/main/REPORT/PROOFS/PROOF_CAMPAIGN_STATUS.md)

---

*Last Updated: 2026-02-12*

# Keon Public Documentation

This repository is the canonical source of truth for Keon's
public-facing documentation and claims.

## 🚀 Start Here

**[→ START_HERE.md](./START_HERE.md)** — 30-second summary + multiple entry points

Choose your path:
- **Verify in 2 minutes**: [Evidence Pack Tour](https://keon-systems.vercel.app/evidence-pack-tour)
- **Understand the design** (15 min): [Whitepaper](./whitepaper/INDEX.md)
- **Build with Keon** (30+ min): [Choose your SDK](#building-with-keon)
- **Audit claims** (60+ min): [Governance Registry](#governance-claims)

## Contents
- [What this repository is](#what-this-repository-is)
- [What this repository is not](#what-this-repository-is-not)
- [Governance principles](#governance-principles)
- [Repository layout](#repository-layout)
- [Verification](#verification)
- [License](#license)

## What This Repository Is

- **Registry**: All public claims made about Keon, registered with IDs
- **Proof Map**: Each claim linked to verifiable artifacts (tests, code, examples)
- **Ledger**: Reconciliation record tracking content drift over time
- **Whitepaper**: Versioned design documents (v1.0 current, January 2026)
- **Evidence Tours**: Interactive verification of real governance decisions

## What This Repository Is NOT

- Marketing copy or sales materials
- Product roadmap or future commitments
- Internal design or implementation archive
- Mirror of private implementation repositories (see [OMEGA](https://github.com/m0r6aN/OMEGA) for production example)

## Governance Principles

How we ensure trust through transparency:

- **No implicit claims**: Every public claim must be explicitly registered in `canon/CLAIMS_REGISTRY.yaml`
- **Proof-required**: Claims marked as verifiable must have linked proof in `canon/PROOF_MAP.yaml`
- **Immutable history**: Changes are never rewritten; all changes appended as new versions
- **Explicit drift**: Content drift is recorded openly in `content/` ledger, not silently corrected
- **Self-verifying**: If a claim is here, you can verify it. If it's not here, we don't claim it.

## Building with Keon

Choose your SDK by language:

- **[C# SDK](https://github.com/m0r6aN/keon-sdk-cs)** — Production-ready, used by OMEGA
- **[Go SDK](https://github.com/m0r6aN/keon-sdk-go)** — High-performance operations
- **[Python SDK](https://github.com/m0r6aN/keon-sdk-python)** — ML pipelines and data workflows
- **[TypeScript SDK](https://github.com/m0r6aN/keon-sdk-ts)** — Node.js and browser environments

Each SDK includes quick-start guides, examples, and integration patterns.

## Governance Claims

All public claims about Keon are registered in `canon/CLAIMS_REGISTRY.yaml` with:
- **Claim ID**: Unique identifier (e.g., `KS-DECIDE-001`)
- **Description**: What Keon claims to do
- **Proof**: How to verify the claim independently
- **Status**: Active, deprecated, or planned

[→ View Claims Registry](./canon/CLAIMS_REGISTRY.yaml)

### Verification

Automated checks ensure:
- ✓ Every claim is registered with an ID
- ✓ Every verifiable claim has linked proof
- ✓ Claim IDs are well-formed and consistent
- ✓ No circular references or inconsistencies

If it's stated here, it can be proven. If it's not here, it is not claimed.

## Whitepaper

The canonical Keon Systems Whitepaper is published and versioned in the `whitepaper/` directory.

**Current Version**: v1.0 — Governed Execution for Operational AI (January 2026)

[See Whitepaper Index →](./whitepaper/INDEX.md)

### Why a Versioned Whitepaper?

- **Immutability**: v1.0 is locked; future changes become v1.1, v2.0, etc.
- **Traceability**: You always know which version you're referencing
- **Addenda policy**: Corrections and clarifications tracked separately
- **Citation-safe**: You can reference a specific version with confidence

## Repository Layout

```
keon-docs/
├── START_HERE.md ..................... Canonical entry point (start here!)
├── README.md ......................... This file
├── canon/ ............................ Source of truth for governance
│   ├── CLAIMS_REGISTRY.yaml .......... All public claims with proof requirements
│   ├── PROOF_MAP.yaml ............... How to verify each claim
│   └── FEATURES_BY_PHASE.yaml ....... Implementation timeline
├── content/ .......................... Reconciliation ledger
│   └── *.md .......................... Drift tracking records
├── whitepaper/ ....................... Versioned design documents
│   ├── INDEX.md ...................... Version index and citation guide
│   └── v1.0/ ......................... January 2026 release
├── scripts/ .......................... Governance automation
│   └── claims/ ....................... Claim validation & linting
└── .github/ .......................... CI/CD workflows
    └── workflows/ ................... PR validation, claim checks
```

## License

This repository is licensed under the
Creative Commons Attribution–NoDerivatives 4.0 International License (CC BY-ND 4.0).

**You can**:
- Share the contents with attribution
- Reference specific versions in citations
- Link to claims and proof artifacts

**You cannot**:
- Modify and redistribute altered versions
- Present claims as your own
- Remove version history

---

## Contributing

This is a public documentation repository.

- **For claims updates**: See CLAIMS_PROCESS.md (if you find an error, open an issue)
- **For proof additions**: Link to your GitHub issue or commit
- **For security**: See SECURITY.md

All changes go through PR review to maintain governance integrity.

---

## Questions?

- **How do I verify a claim?** → See START_HERE.md → Path 4: Verify Claims & Audit
- **Where's the implementation?** → See linked repos in PROOF_MAP.yaml
- **Can I use this in production?** → Yes; choose your SDK in START_HERE.md
- **What if I find a vulnerability?** → See SECURITY.md

---

**Law before power. Proof before trust.**

Last updated: January 2026

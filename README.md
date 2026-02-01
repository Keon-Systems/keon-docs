# Keon Public Documentation

This repository is the canonical source of truth for Keon’s
public-facing documentation and claims.

## Contents
- [What this repository is](#what-this-repository-is)
- [What this repository is not](#what-this-repository-is-not)
- [Governance principles](#governance-principles)
- [Repository layout](#repository-layout)
- [Verification](#verification)
- [License](#license)

## What this repository is
- A registry of all public claims made about Keon
- A proof map linking each claim to verifiable artifacts
- A reconciliation ledger tracking content drift over time
- Versioned whitepaper snapshots and addenda

## What this repository is not
- Marketing copy
- A roadmap
- An internal design archive
- A mirror of private implementation repositories

## Governance principles
- Every public claim must be explicitly registered
- Claims marked as verifiable must have proof
- History is never rewritten; changes are appended
- Drift is recorded explicitly, not silently corrected

## Verification
Claims and documentation in this repository are governed by
automated checks that fail if:
- A claim is referenced but not registered
- A verifiable claim lacks proof
- Claim IDs are malformed or inconsistent

If it’s stated here, it can be proven.
If it’s not here, it is not claimed.

## Repository layout
- `canon/` — claims registry, proof map, phase inventory (source of truth)
- `content/` — reconciliation ledger (drift tracking)
- `whitepaper/` — versioned snapshots + addenda
- `scripts/claims/` — claim lint + drift checks (CI)
- `.github/` — CI workflow and PR template

## License

This repository is licensed under the
Creative Commons Attribution–NoDerivatives 4.0 International License (CC BY-ND 4.0).

You may share and reference the contents with attribution.
You may not modify and redistribute altered versions.

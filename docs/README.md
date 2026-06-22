# Keon Documentation — Canonical Index

> **Status:** Canonical documentation index. Created under Operation CLEAN EDGE **M3** (Documentation Clean Sweep).
> **Rule:** Every surviving doc maps to exactly one canonical **product surface** (`docs/strategy/CANONICAL-PRODUCT-SURFACES.md`), one **cross-surface process candidate** (`docs/strategy/E2E-PROCESS-CANDIDATES.md`), a **standards artifact / external obligation**, or a **still-valid invariant** not yet captured elsewhere.
> Paths are relative to the `keon-docs/` repository root. History lives in version control — there is no `/archive`, `/legacy`, `/old`, or `/deprecated`.

---

## 1 · CLEAN EDGE Canon (strategy)

The frozen strategic-reset canon. Source of truth for the thesis, surfaces, gates, and posture.

| Doc | Role |
| :--- | :--- |
| `docs/strategy/KEON-STRATEGIC-THESIS-vNEXT.md` | The canonical thesis (reasoning-to-action chain + audit-ready evidence) |
| `docs/strategy/CANONICAL-PRODUCT-SURFACES.md` | The five surfaces (Collective · Cortex · MCP Gateway · Runtime · Control) + CAES/CPP |
| `docs/strategy/THREE-PROOFS-FRAME.md` | Evidence model — Authority / Causation / Viability |
| `docs/strategy/DELIBERATION-EVIDENCE-MODEL.md` | Deliberation evidence (G0 Position A — product-only, Cortex/Evidence Pack) |
| `docs/strategy/TELEMETRY-PUBLIC-PROOF-POSTURE.md` | Buyer-reproducible telemetry posture (no fake numbers) |
| `docs/strategy/E2E-PROCESS-CANDIDATES.md` | The seven cross-surface process candidates (M4 will formalize) |
| `docs/strategy/DOCUMENTATION-CLEAN-SWEEP-POLICY.md` | The policy this index executes (keep/delete criteria + G-COVERAGE) |
| `docs/strategy/OPERATION-CLEAN-EDGE-Implementation-Plan-v0.2.md` | Controlling milestone sequencer (control-plane planning artifact) |

## 2 · Public Narrative

| Doc | Maps to |
| :--- | :--- |
| `whitepaper/WHITEPAPER_v2.0.md` | **Canonical whitepaper** (M1 re-headline) — all five surfaces + Three Proofs |
| `whitepaper/INDEX.md`, `whitepaper/CHANGELOG.md` | Whitepaper navigation / change history |
| `README.md` | Repository entry point / overview *(public messaging alignment is M5 scope)* |
| `START_HERE.md` | Onboarding overview *(messaging alignment is M5 scope)* |

## 3 · Standards Artifacts (never deletion candidates)

> Canonical CAES/CPP live in the **`caes-standards-org`** repo (designated canonical under M2). The copies here are standards artifacts / mirrors pending the M2 reconciliation items — they are out of scope for deletion.

| Doc | Role |
| :--- | :--- |
| `caes-standard-v0.2.0-draft.md` | CAES standard (mirror; canonical = `caes-standards-org/caes-standard-v0.2.0-draft.md`) |
| `CAES_v0.2.0_CHANGES.md` | CAES v0.2.0 changes & rationale |
| `standards/CAES-…-Draft.pdf`, `standards/CPP-…-Public-Draft.pdf` | Published standards PDFs (external-facing) |

## 4 · Surface- and Process-Mapped Supporting Docs

| Doc | Canonical surface | Process candidate |
| :--- | :--- | :--- |
| `adr/ADR-0005-tenant-data-model.md` | Cortex · Control | Tenant Sandbox Activation (#5) |
| `audits/receipt-schema-parity.md` | Runtime · Cortex | Audit Evidence Reconstruction (#3) |
| `audits/control-auth-fixture-remediation-plan.md` | Control | — (Control launch-readiness audit) |
| `audits/control-route-inventory-notes.md` | Control | — (Control surface inventory) |
| `canon/claims/README.md` | (Claims registry / governance) | Audit Evidence Reconstruction (#3) |
| `content/PT-013-PBWB/artifacts/policy_pack.md` | (Claims evidence — sealed) | Collective Reasoning-to-Execution (#2) |
| `content/PT-013-PBWB/artifacts/final_artifact.md` | (Claims evidence — sealed) | Collective Reasoning-to-Execution (#2) |
| `content/evidence-packs/v1/README.md` | Cortex (Evidence Pack) | Audit Evidence Reconstruction (#3) |
| `guides/mcp-client-integration-guide.md` | MCP Gateway | MCP Tool Governance (#4) |
| `sdks/overview.md` | (Integration / SDK surface) | MCP Tool Governance (#4) |
| `sdks/parity-matrix.md` | (Integration / SDK surface) | MCP Tool Governance (#4) |
| `ui/governed-execution-diagram.md` | Architecture (five surfaces) | Autonomous Action Governance (#1) |
| `ui/separation-of-powers.md` | Architecture (three planes) | Autonomous Action Governance (#1) |
| `ui/auditor-walkthrough.md` | Control | Audit Evidence Reconstruction (#3) |
| `ui/courtroom-ui.md` | Control | Audit Evidence Reconstruction (#3) |
| `ui/why-not-open-source.md` | Control (UI positioning) | — *(positioning; M5 may revisit)* |

## 5 · Blocked from Deletion (pending milestone disposition)

These docs are not part of the surviving canonical set, but were **not** deleted in M3. They are held for a later milestone to rewrite or retire.

| Doc | Status | Reason |
| :--- | :--- | :--- |
| `comparison/governance-models.md` | **Blocked pending M5 disposition** | Coverage of its invariants passes, but it may hold salvageable comparative/messaging material for website alignment. M5 will either rewrite it into aligned messaging or retire it. It references deprecated taxonomy (OMEGA, Workshop/Courtroom-as-primary) and must not be treated as current canon in the interim. |

## 6 · Out of Index (not product documentation)

- `AGENTS.md` — repository agent-tooling stub (not Keon product documentation).
- `LICENSE` — legal.
- Root build/working artifacts (`*.txt` logs, `*.cs` / `*.ts` / `*.js` scripts, `package*.json`, `pr-body.md`, etc.) — transient, not documentation. Recommended for a separate (non-M3) repo-hygiene cleanup.

---

*Operation CLEAN EDGE · M3 Documentation Clean Sweep · canonical index of surviving Keon documentation.*

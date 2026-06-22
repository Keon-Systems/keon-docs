# OPERATION CLEAN EDGE
## Keon Strategic Reset — Implementation Plan

| | |
| :--- | :--- |
| **Version** | v0.2 (Approved) |
| **Codename** | CLEAN EDGE |
| **Status** | Plan approved · M0 accepted · G0 approved · M1/M2 pending authorization |
| **Owner** | Clint (final authority) |
| **The Joint** | GPT (authoring) · Gemini (research) · Claude (review/tighten/lock) |
| **Date** | 2026-06-03 |

---

## 0. Preface

Keon is entering a necessary strategic reset. The market has validated the need for governed AI execution, but it has also compressed the value of several primitives Keon originally led with. Pre-execution authorization, signed receipts, policy checks, and fail-closed enforcement remain essential, but they are no longer enough by themselves to define Keon's public edge. This work sharpens Keon around the higher-value thesis: governing the full reasoning-to-action chain and producing audit-ready evidence enterprises can trust.

This reset is a correction in emphasis, not a retreat from the architecture. The strongest differentiation is the combination of **Collective, Cortex, MCP Gateway, Runtime, and Control**: cognition that can branch and challenge itself, execution that remains non-bypassable, causal truth that can be reconstructed, and operator-facing proof of what happened, why, what alternatives were considered, and what it cost operationally.

The purpose is to establish a clean canonical starting point before more implementation, documentation, or public messaging accumulates — so old positioning stops bleeding into new work.

---

## 1. Locked Decisions (Plan Premises)

These are the premises the entire plan is built on. **D1 and D3 are locked.** **D2 is now locked by G0 to Position A** (see structural note).

| ID | Decision | Status |
| :--- | :--- | :--- |
| **D1** | CAES remains an **independent, complementary standard** — not an implementation profile *of* external standards. It supplies the mechanical enforcement layer that frameworks like EU AI Act Art. 12 *require but do not specify*. Posture is "we enable / we map to," never "we are a profile of." | **Locked** |
| **D2** | **Deliberation evidence** ("what alternatives were considered") grows the evidence model to back the reasoning-to-action thesis. **Default home: Cortex / Evidence Pack** — not CAES — unless a branch itself crosses (or would have crossed) an Effect Boundary. CAES stays narrow and stable absent a specific standards-level reason to extend. | **Locked (G0 — Position A)** |
| **D3** | Degraded-mode behavior is bounded by CAES, not open. When Keon is degraded, effect-bound actions resolve to **Denied** or **RequiresHumanAuthorization** (suspend + escalate, timeout → Denied). Receiptless execution is never permitted under any degraded path. | **Locked** |

> **Structural note on D2 (resolve at G0):** CAES v0.2.0's scope clause states that internal computation and non-effecting reasoning are *out of scope unless they cross an Effect Boundary*. That clause argues that "alternatives considered" evidence belongs in **Keon Cortex + the Evidence Pack**, not as a new CAES primitive — and that CAES only grows if a *rejected branch would itself have crossed an effect boundary*. This keeps the standard narrow (your own instinct) while still delivering the proof. **Adopted working default (per Clint): deliberation evidence lives in Cortex/Evidence Pack; CAES grows only if a branch crosses or would have crossed an Effect Boundary.** **G0 has PASSED — this is now locked as Position A:** deliberation evidence is product-only in Cortex/Evidence Pack; CAES does not absorb alternatives-considered evidence; Effect-Boundary rejected branches remain in Cortex/Evidence Pack unless a future CAES amendment explicitly defines them (the Effect Boundary triggers heightened evidence handling, not CAES ownership). See `DELIBERATION-EVIDENCE-MODEL.md`.

---

## 2. The Unifying Frame — Three Proofs

Every public claim in this reset maps to exactly one of three proof types. This is the narrative spine for M1, M4, and M5.

| Proof | Question Answered | Mechanism | Standards Anchor |
| :--- | :--- | :--- | :--- |
| **Authority** | Was it allowed? | Decision Receipt · PolicyHash · fail-closed | CAES L1/L2, CPP |
| **Causation** | What happened, why, what else was considered? | Evidence Pack · causal spine · deliberation evidence | CAES L2 + Cortex (deliberation = product/Cortex per D2 note) |
| **Viability** | What did it cost? Did it stay up? | Telemetry · degraded-mode behavior · chaos attestation | CAES L3 ChaosTestAttestation (partial) + product telemetry |

> **Extended house line (optional, M1/M5):**
> *Execution proposes. Governance decides. Receipts prove. Telemetry attests.*

---

## 3. Operating Model

The directive's M0→M6 structure is preserved for continuity with prior work. CLEAN EDGE adds **four cross-cutting gates** and **one terminal milestone (M7)**.

### Sequential gates
**M0 → (M1 ∥ M2) → M3 → M4 → M5 → M6 → M7**

### Parallel lanes (after M0 / G0)
- **M1** (Whitepaper) ∥ **M2** (CAES/CPP posture) — *with strict ownership split (see §4)*
- **M2-DELIB** (deliberation evidence model) runs as a sub-lane off M2, gated by G0 fork
- **M4** (E2E Canon) may start once M1 is directionally stable; reconciles after M3
- **M3** (Clean Sweep) waits for M1 + M2
- **M5** (Website) waits for M1 + M4; prefer M3 merged
- **M6** (Code/Test Audit) waits for M4
- **M7** (Canon Lock) waits for everything

---

## 4. Cross-Cutting Gates (apply across all milestones)

These are the quality gates that prevent the failure modes specific to this reset. Each milestone's acceptance criteria inherits the relevant gates.

| Gate | Rule | Applies To | Owner |
| :--- | :--- | :--- | :--- |
| **G-FALSIFY** | Every headline claim ships with its falsification method — "here's how you'd catch us lying." No claim softer than the receipt-grade standard it replaces. | M1, M5 | Claude (review) |
| **G-TAG** | Every public claim is tagged **standards-backed** (in CAES/CPP) or **product-only** (Collective deliberation, telemetry, causal reconstruction depth). No claim implies standards backing it does not have. | M1, M4, M5 | Claude (review) |
| **G-COVERAGE** | No legacy doc is deleted until its still-valid invariants/schemas are confirmed present in a surviving canonical doc. Git history is not working canon. | M3 | GPT (author) / Claude (verify) |
| **G-DRIFT** | No downstream artifact may contradict or silently re-scope locked canon — including the verbatim thesis, the surface role lines, the G0 rulings, and the locked premises. Any drift must be surfaced as an explicit change request, not absorbed. A violation is a shipped artifact whose claim conflicts with a locked item and was not flagged. (Subsumes canon-to-canon consistency: whitepaper, E2E canon, website, and CAES/CPP posture must assert the same facts — verified, not assumed.) | All milestones; terminal sweep at M7 + spot-checks at each merge | Claude (sweep) |

**Ownership split for CAES/CPP language (prevents M1 ∥ M2 drift):** **M2 owns** the canonical CAES/CPP framing. **M1 references it as a stub** and never authors standards-posture language independently. One owner per surface.

---

## 5. Milestone Specifications

> Items marked **[+]** are CLEAN EDGE additions beyond the original directive.

### M0 — Strategic Reset Canon
**Objective:** Definitive canonical starting point. **Dependency:** none. **Primary:** GPT · **Review:** Claude · **Approve:** Clint (G0)

**Deliverables:**
```
docs/strategy/KEON-STRATEGIC-THESIS-vNEXT.md
docs/strategy/CANONICAL-PRODUCT-SURFACES.md
docs/strategy/DOCUMENTATION-CLEAN-SWEEP-POLICY.md
docs/strategy/TELEMETRY-PUBLIC-PROOF-POSTURE.md
docs/strategy/E2E-PROCESS-CANDIDATES.md
docs/strategy/DELIBERATION-EVIDENCE-MODEL.md        [+]
docs/strategy/THREE-PROOFS-FRAME.md                 [+]
```

**Thesis (canonical):**
> Keon governs the reasoning-to-action chain for autonomous AI systems and produces audit-ready evidence for consequential action.

**Bind "consequential action" to the locked Effect Boundary taxonomy** [+] — consequential = crosses a CAES Effect Boundary. New vocabulary inherits existing rigor.

**Product surfaces:** Collective (governs cognition before action) · Cortex (preserves causal truth + reconstructable evidence) · MCP Gateway (governs execution at the tool boundary) · Runtime (authority/decision/execution/receipt mechanics) · Control (operator/tenant/audit/evidence cockpit) · CAES/CPP (standards-aligned, per D1).

**Telemetry posture** must define (no fake numbers — metric names, meaning, source, publication rules only): latency added, reliability posture, success/deny/fail-closed rates, receipt persistence overhead, gateway overhead, evidence-pack generation overhead, degraded-mode behavior. **Frame telemetry as buyer-reproducible** [+] — publish methodology + a verification harness ("measure our overhead yourself"), mirroring the offline-verifiable Evidence Pack ethos, not marketing dashboards.

**Deliberation evidence model** [+] — define the considered-but-rejected-branch evidence object (candidate intents/justifications + selection disposition + reason), its home (Cortex/Evidence Pack per D2 note), and its relationship to the linear spine.

**Hard boundaries:** no whitepaper rewrite, no CAES/CPP edits, no doc deletion, no website/product code changes.

**G0 Approval Gate** [+] — **PASSED.** Clint confirmed (a) D2 fork → **Position A** (deliberation evidence in Cortex/Evidence Pack, product-only; CAES not extended); (b) thesis wording; (c) telemetry-harness approach. **M0 is accepted.** Downstream milestones (M1/M2) are released to begin **only on explicit Clint authorization** — they are not auto-started by G0 passing.

---

### M1 — Whitepaper Rewrite (Re-headline, not blank page) [+ framing]
**Objective:** Canonical public narrative on the new thesis. **Dependency:** M0 merged + G0. **Primary:** GPT · **Review:** Claude

**Core positioning:** *Keon governs autonomous AI from reasoning to action, producing audit-ready evidence before and after consequential effects.*

**Framing discipline** [+]: this is **re-sequencing and re-headlining**, not a blank-page rewrite. The hardest-to-fabricate content already exists — FP-01..10, failure scenarios, structural guarantees, regulatory mapping. **Lock what survives, then rebuild the frame around it.** Receipts, fail-closed, PolicyHash, append-only move from headline to **substrate** — demoted, never deleted.

**Required structure:** problem (autonomy crossing effect boundaries faster than enterprises can audit) → why logs/prompts/post-hoc fail → thesis → architecture (Collective/Cortex/Gateway/Runtime/Control) → evidence model (Three Proofs) → telemetry model → standards posture (CAES/CPP per D1, **stub owned by M2**) → why Keon ≠ gateway-only governance → regulated-enterprise wedge → operational proof obligations.

**Gates inherited:** G-FALSIFY, G-TAG.

**Hard boundaries:** no CAES/CPP edits (M2 owns), no deletions, no website code, no invented benchmarks.

---

### M2 — CAES/CPP Standards Posture Review
**Objective:** Make CAES/CPP support the new positioning without reading as rival standards or vendor-serving. **Dependency:** M0 + G0. Runs ∥ M1. **Primary:** GPT · **Review:** Claude

**Required outcome (per D1):** CAES is an **independent, complementary standard** that maps to external frameworks. Not a profile *of* them.

**Concrete edits identified** [+]:
- **Neutralize the CPP "Strategic Note" (CPP p10)** — "This locks the game / competitors…" is vendor-competitive language inside a normative spec. Strip or rewrite to neutral standards-body tone. *Highest-priority single edit.*
- **Harden self-attestation language** (CAES Conformance Claims) — keep self-attestation but strengthen third-party audit framing so the standard does not read as self-grading.
- **Preserve** the existing "reference designation does not imply commercial endorsement" line — already correct.
- **Extend the "we enable / we map to" mapping** using the existing whitepaper appendix model (CAES property → regulation article).

**M2-DELIB sub-lane** [+] (gated by G0 fork): if G0 routes deliberation into CAES, draft the minimal CAES vNEXT addition (only where a rejected branch crosses an effect boundary). If G0 routes to Cortex/Evidence Pack, CAES is untouched and this work moves to M4/Cortex docs.

**Hard boundaries:** do not weaken core primitives (fail-closed, receipt, policy, evidence). No Keon product positioning outside CAES/CPP docs. No website/code.

---

### M3 — Documentation Clean Sweep
**Objective:** Delete legacy docs that no longer support the thesis. **Dependency:** M1 + M2 merged. **Primary:** GPT · **Verify:** Claude

**Gate inherited:** **G-COVERAGE** [+] — pre-delete coverage check is mandatory and blocking.

**Rule:** delete docs that position Keon as generic middleware, lead with commoditized primitives as the differentiator, conflict with the new whitepaper, compete with external standards, describe deprecated surfaces (e.g., OMEGA taxonomy), or confuse the Systems/Cortex/Collective/Control/Runtime/Gateway split. Source control is the archive — **no `/archive`, `/legacy`, `/old`, `/deprecated` folders.**

**Deliverable:** `docs/README.md` as the canonical documentation index. PR summary lists every deleted doc and the reason.

---

### M4 — Canonical E2E Process Definitions
**Objective:** End-to-end processes that satisfy the messaging and become test targets. **Dependency:** M1 directionally stable; reconcile after M3. **Primary:** GPT · **Review:** Claude

**Deliverables:**
```
docs/canon/e2e/AUTONOMOUS-ACTION-GOVERNANCE.md
docs/canon/e2e/COLLECTIVE-REASONING-TO-EXECUTION-CANDIDATE.md
docs/canon/e2e/AUDIT-EVIDENCE-RECONSTRUCTION.md
docs/canon/e2e/MCP-TOOL-GOVERNANCE.md
docs/canon/e2e/TENANT-SANDBOX-ACTIVATION.md
docs/canon/e2e/FAILURE-DEGRADED-MODE.md
docs/canon/e2e/PUBLIC-TELEMETRY-PROOF.md
docs/canon/e2e/README.md
```

**Per-process format:** purpose · trigger · actors · preconditions · main flow · failure flow · required receipts/evidence · required telemetry · security invariants · audit questions answered · test obligations · out-of-scope.

**Elevation** [+]: `FAILURE-DEGRADED-MODE.md` is **first-tier**, not one-of-seven. It encodes D3 (deny vs. suspend-escalate, timeout → deny, never receiptless execution) — this is the real enterprise objection for the viability thesis.

**`COLLECTIVE-REASONING-TO-EXECUTION-CANDIDATE.md`** carries the deliberation-evidence E2E per the G0 fork.

`PUBLIC-TELEMETRY-PROOF.md` defines the full metric model (p50/p95/p99 decision/gateway/persistence latency, evidence-pack gen time, fail-closed/deny/approve rates, degraded-mode count, availability, sink health, eval error rate, binding-failure rate). **Metric definitions + reproduction method only — no fake numbers.**

---

### M5 — Website & Public Messaging Alignment
**Objective:** Align public surfaces to M1 + M4. **Dependency:** M1 + M4 merged; prefer M3. **Primary:** GPT · **Review:** Claude · **Approve:** Clint

**Messaging:** govern the reasoning-to-action chain · produce audit-ready evidence · prove performance and failure posture with public telemetry.

**Surfaces:** homepage, platform, proof, standards, whitepaper, get-access, Gateway/Runtime/Collective/Cortex references. Add `/proof/telemetry` (why telemetry matters · what Keon publishes · what each metric proves · how it complements receipts/evidence packs · reproduction method · no unsupported benchmarks).

**Gates inherited:** G-FALSIFY, G-TAG. **Hard boundaries:** no invented telemetry, no generic-gateway positioning, no receipts-as-sole-differentiator, no enterprise fluff.

---

### M6 — Code & Test Coverage Audit
**Objective:** Map codebase + test coverage against E2E canon. **Audit only.** **Dependency:** M4 merged; prefer M5. **Primary:** GPT · **Review:** Claude

**Deliverables:**
```
docs/audits/CODE-TO-E2E-CANON-MAP.md
docs/audits/TEST-COVERAGE-GAP-MAP.md
docs/audits/TELEMETRY-INSTRUMENTATION-GAP-MAP.md
docs/audits/DEAD-SURFACE-CANDIDATES.md
```
Map all seven E2E processes; identify telemetry emission points; propose follow-up PR lanes. **No implementation, no telemetry build, no deletion, no refactor.**

---

### M7 — Canon Lock (Drift Sweep) [+ new milestone]
**Objective:** Verify whitepaper, E2E canon, website, and CAES/CPP posture assert identical facts. **Dependency:** all prior milestones. **Primary:** Claude · **Approve:** Clint

**Deliverable:** `docs/audits/CANON-CONSISTENCY-REPORT.md` — every cross-surface assertion checked; every G-TAG claim verified consistent across whitepaper ↔ website ↔ E2E ↔ standards; drift items filed as blocking fixes before lock.

**Acceptance:** zero unresolved canon-to-canon drift. Reset is locked.

---

## 6. Dependency Graph

```
M0 ──G0──┬── M1 ──────────────┬── M3 ──┬── M4 ──┬── M5 ──┬── M6 ──┬── M7
         │                    │        │        │        │        │
         └── M2 ──────────────┘        │        │        │        │
              └── M2-DELIB (G0 fork) ──┘        │        │        │
                                                └────────┴────────┘
   (M4 may start when M1 is directionally stable; reconciles after M3)
```

---

## 7. The Joint — Assignments

| Member | Role |
| :--- | :--- |
| **GPT** | Primary authoring — research memos, spec drafts, milestone deliverables |
| **Gemini** | Research support — external standards landscape, regulatory citations, competitor posture |
| **Claude** | Architectural review, structural-gap + doctrinal-consistency detection, corrected/extended artifacts, G-FALSIFY / G-TAG enforcement, M7 drift sweep |
| **Clint** | Final authority — G0 and all approval/lock gates. Human primacy: every lock is a signed disposition, not a silent merge |

---

## 8. Conventions

| | |
| :--- | :--- |
| **Branch** | `reset/m{n}-{slug}` — e.g. `reset/m0-strategic-canon`, `reset/m1-whitepaper`, `reset/m2-standards-posture` |
| **Commit prefix** | `M{n}:` — e.g. `M0: add three-proofs frame`, `M2: neutralize CPP strategic note` |
| **Tags** | `keon-reset-m{n}` on each milestone lock; `keon-reset-locked` on M7 |
| **PR title** | `[CLEAN EDGE][M{n}] {summary}` |
| **Gate label** | PRs touching gated surfaces carry the gate label (`g-falsify`, `g-tag`, `g-coverage`, `g-drift`) |

---

## 9. Risk Register

| # | Risk | Mitigation | Gate/Milestone |
| :--- | :--- | :--- | :--- |
| R1 | New thesis softer/less falsifiable than the receipt claim it replaces | Every headline claim ships a falsification method | G-FALSIFY / M1 |
| R2 | "Standards-aligned" implied for claims CAES doesn't back | Tag every claim standards-backed vs product-only | G-TAG / M1·M4·M5 |
| R3 | M1 ∥ M2 author CAES/CPP framing independently → drift | M2 owns canonical language; M1 references stub | §4 / M1·M2 |
| R4 | Clean sweep amputates load-bearing invariants | Pre-delete coverage check before any deletion | G-COVERAGE / M3 |
| R5 | "Rewrite" regenerates locked, battle-tested content | Re-headline discipline; lock survivors first | M1 framing |
| R6 | Telemetry page reads as vaporware or over-commits to live numbers | Buyer-reproducible harness + methodology, no marketing numbers | M0·M4·M5 |
| R7 | D2 grows CAES past its own scope clause | G0 fork; default to Cortex/Evidence Pack home | G0 / M0·M2 |
| R8 | Degraded mode permits receiptless execution under load | D3 locked: deny or suspend-escalate, never bypass | M4 (FAILURE-DEGRADED-MODE) |
| R9 | Surfaces drift apart after parallel work | Terminal canon-to-canon sweep | G-DRIFT / M7 |
| R10 | CPP vendor-competitive language survives into standards-body materials | Neutralize Strategic Note + self-attestation framing | M2 |

---

## 10. Immediate Next Command (M0 kickoff — EXECUTED · M0 accepted · G0 passed)

> *Historical record of the M0 kickoff command as issued. M0 is complete and accepted; G0 has passed. Retained verbatim for traceability — it is no longer the active next command. M1/M2 await explicit Clint authorization.*

```text
Begin PR-M0 only — Operation CLEAN EDGE, Strategic Reset Canon.

Do not rewrite the whitepaper.
Do not edit CAES/CPP.
Do not delete documentation.
Do not modify website or product code.

Deliver:
- docs/strategy/KEON-STRATEGIC-THESIS-vNEXT.md
- docs/strategy/CANONICAL-PRODUCT-SURFACES.md
- docs/strategy/DOCUMENTATION-CLEAN-SWEEP-POLICY.md
- docs/strategy/TELEMETRY-PUBLIC-PROOF-POSTURE.md
- docs/strategy/E2E-PROCESS-CANDIDATES.md
- docs/strategy/DELIBERATION-EVIDENCE-MODEL.md
- docs/strategy/THREE-PROOFS-FRAME.md

Thesis:
Keon governs the reasoning-to-action chain for autonomous AI systems and produces audit-ready evidence for consequential action.

Locked premises:
- CAES remains an independent, complementary standard. It maps to external frameworks; it is not a subordinate implementation profile of them.
- Degraded mode resolves to Denied or RequiresHumanAuthorization / suspend-escalate. Receiptless execution is never permitted.
- Consequential action is bound to the CAES Effect Boundary taxonomy.

Working premise for G0:
- Deliberation evidence defaults to Cortex/Evidence Pack, not CAES, unless a branch itself crosses or would have crossed an Effect Boundary.
- CAES should stay narrow and stable unless there is a specific standards-level reason to extend it.

Telemetry posture:
- Telemetry is first-class public proof of operational viability.
- Receipts prove authority and outcome.
- Evidence packs prove causation and reconstruction.
- Telemetry proves latency, reliability, degraded-mode behavior, and production readiness.
- No fake benchmarks.
- Define metric names, sources, publication rules, and buyer-reproducible verification approach only.

Halt at G0 for Clint approval before any downstream milestone.
```

---

*Operation CLEAN EDGE · Keon Strategic Reset · Plan v0.2 · Approved · M0 accepted · G0 approved · M1/M2 pending authorization*

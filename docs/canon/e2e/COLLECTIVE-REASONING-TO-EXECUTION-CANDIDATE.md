# COLLECTIVE REASONING-TO-EXECUTION
### Canonical E2E Process — Candidate 2

> **Status:** Canonical E2E process definition. Authored under Operation CLEAN EDGE **M4**.
> **Boundary:** Strategy/governance canon. Defines a process; changes no code, website, standard, or product behavior.
> **Process candidate:** E2E candidate 2 — *Collective Reasoning-to-Execution* (`E2E-PROCESS-CANDIDATES.md`).
> **Primary surfaces:** Collective (cognition) · Cortex (deliberation home + causal record) · MCP Gateway · Runtime · Control.
> **G0 / Position A:** This process **carries deliberation evidence**. Deliberation evidence is **product-only**, lives in **Cortex / the Evidence Pack**, is **non-effecting until the Runtime decision**, and is **never** tagged standards-backed.

## Tag legend (G-TAG)

- **`[S]` standards-backed** — anchored in canonical CAES / CPP (CAES Working Group). Referenced, not authored here.
- **`[P]` product-only** — a Keon product property; real and falsifiable, not a standards claim.
- **`[G-FALSIFY: …]`** — the concrete test that would prove a headline claim false.

---

## Purpose

Carry multi-agent cognition from **contested reasoning in the Collective** through branch collapse to a **governed execution**, preserving the alternatives considered and rejected as **deliberation evidence**. This process specializes candidate 1 for Full Keon mode: cognition may branch, simulate, and challenge itself freely, but **no branch may cause an effect directly** — every effect still crosses the MCP Gateway and is decided by the Runtime. `[P]`

The differentiated claim this process backs: Keon governs the reasoning *before* the call — the alternatives weighed and rejected — not only the call itself. A gateway-only architecture cannot produce this. `[P]` `[G-FALSIFY: Show a gateway-only architecture that produces, for a consequential action, an offline-verifiable record of the alternatives considered and why the chosen path won. If a gateway alone can do that, this process's reason to exist is wrong.]`

---

## Trigger

A `CollectiveIntent` is admitted to the Collective Host (`ICollectiveHost`, the Pantheon orchestration seam) for cognition that is expected to terminate in at least one Effect-Boundary-crossing action. `[P]`

---

## Actors

| Actor | Role |
| :--- | :--- |
| **Collective** (Council / Guild / Worker archetypes) | Forms intents and justifications, materializes speculative branches, runs adversarial review, evaluates heat, collapses to a winner. Governs cognition **before** action; owns no effect authority. `[P]` |
| **Cortex** | Default home of deliberation evidence; builds the Collective Causal Record and Evidence Pack. `[P]` |
| **MCP Gateway** | Non-bypassable effect boundary; binds identity/scope; routes the collapsed, eligible request to the Runtime. `[P]` |
| **Runtime** | Owns authority, policy decision, execution mechanics, receipt emission. `[P]` |
| **Control** | Operator oversight (gradient modes) and post-hoc deliberation audit. `[P]` |
| **CAES / CPP** | Standard for the Authority/Causation proofs at the effect point — **not** for deliberation evidence. `[S]` |

---

## Preconditions

1. Tenant/actor scope is bound and inherited immutably into any downstream request (RB-4). `[S]` (Effect Boundary identity binding) / `[P]`
2. An oversight profile (`Strategic` · `Tactical` · `Surgical` · `Observational`) is explicit or inherited; reduced human depth triggers the **Compensation Rule** (more machine-side safeguards). `[P]`
3. The policy in force and its PolicyHash are available for the eventual Runtime decision. `[S]`
4. Deliberation evidence has a non-linear home (Cortex / Evidence Pack) — the linear spine cannot natively hold rejected branches. `[P]`

---

## Main flow

1. **Intent intake (Collective Host).** `CollectiveIntent` is accepted; lifecycle begins. `[P]`
2. **Branch materialization (Temporal Echo).** Speculative branches move `Proposed → Materialized → Simulating → Evaluated`. Branches are **non-effecting** throughout (TE-7). `[P]`
3. **Adversarial review.** Each effect-bearing branch must carry adversarial review state (X-4); findings may raise challenge/composite heat (CH-7). `[P]`
4. **Heat evaluation (Cognitive Heat).** Composite heat is compositional, not a sum (a swarm may be hot while each agent is cool). Critical heat can self-prune/pause/abort before any effect request (CH-4); heat may **not** authorize bypass of governed execution (CH-3). `[P]`
5. **Collapse (Temporal Echo).** An explicit collapse event selects exactly one winning branch and produces a `BranchCollapseRecord` (selected ref, candidate refs, rationale, comparative heat/utility, challenge summary, loser archival refs). Losers are archived or pruned **with rationale** — never silently discarded (TE-3). `[P]`
6. **Deliberation evidence capture (Cortex).** The candidate set, per-candidate **selection disposition**, and **reason** are recorded in Cortex / the Evidence Pack as deliberation evidence — **product-only**. `[P]`
7. **Reality Boundary submission.** Only a `CollapsedWinner` becomes `GovernedEffectRequestEligible` and is submitted as a `GovernedEffectRequest` referencing the winning branch, collapse proof, adversarial review, and heat profile (RB-1..3). No Cognition-Plane interface emits the effect payload directly (RB-6). `[S]` (effect boundary) / `[P]`
8. **Governed decision + execution.** From here the request runs candidate 1 (*Autonomous Action Governance*): Gateway admission → Runtime decision (disposition + PolicyHash) → ARO-verified receipt → spine append → execution → outcome. The action is **non-effecting until the Runtime decision**. `[S]` / `[P]`
9. **Collective Causal Record (Civilizational Truth).** Cortex anchors a `CollectiveCausalRecord` wrapping the gateway/decision/outcome receipts, branch lineage, heat profile, witness digest, and reconstructive anchor. Receipt-grounded truth always outranks witness meaning (CT-3); anchors never supersede receipts (CT-4). `[P]` (record) over `[S]` (receipt anchors)

---

## Failure flow

- **Loser branch escape attempt.** An entity submits a `CollapsedLoser` as an effect request → `IRealityBoundaryGuard` checks `selected_branch_ref` against the collapse record → RB-2 violated → fails closed → denial receipt minted, spine-appended, Collective Causal Record updated, adversarial finding elevated, heat updated (Scenario 09). `[S]` (effect boundary) / `[P]`
- **Uncollapsed / missing lineage.** Missing branch lineage, identity binding, or review state fails closed (RB-7, X-10). `[S]` / `[P]`
- **Critical heat.** Triggers self-prune, pause, or abort before any effect request (CH-4); never a bypass. `[P]`
- **Downstream governed-execution failures** (receipt, PolicyHash, spine, tenant, degraded/timeout) resolve exactly as in candidates 1 and 6. `[S]` / `[P]`
- **Oversight reduction.** Lower human depth must **not** reduce the governance floor (OE-2) and must produce compensating machine-side observability (OE-3). `[P]`

---

## Required receipts / evidence

| Artifact | Provenance | Tag |
| :--- | :--- | :--- |
| `BranchCollapseRecord` (candidates, selected, rationale, loser archival) | Collective / Cortex | `[P]` |
| **Deliberation evidence** (candidate intents/justifications + selection disposition + reason) | Cortex / Evidence Pack | `[P]` — **never `[S]`** |
| `CollectiveCausalRecord` (anchored to receipts) | Cortex | `[P]` (record) / `[S]` (anchors) |
| Decision/Denial/Outcome Receipts (at the effect point) | Runtime → ARO | `[S]` |
| Causal spine entry + Evidence Pack | Cortex | `[S]` |

> **Position A discipline:** A rejected branch that would itself have crossed an Effect Boundary **remains in Cortex / the Evidence Pack** under heightened evidence handling — it does **not** become a CAES-owned object absent a future CAES amendment that explicitly defines one. The Effect Boundary is a heightened-handling *trigger*, not a transfer of ownership to the standard. `[P]` (home) / `[S]` (boundary taxonomy only) `[G-FALSIFY: A shipped artifact tags deliberation/alternatives-considered evidence `[S]`, or routes an Effect-Boundary rejected branch into a CAES-owned object without such an amendment. Either is a G0 / G-DRIFT violation.]`

---

## Required telemetry

Definitions/reproduction only (candidate 7). This process additionally exposes, as product telemetry: branch count and collapse outcomes, adversarial-finding counts, heat profiles at submission, and oversight-mode distribution — all `[P]`. Effect-point telemetry (decision/gateway/persistence latency, dispositions) is shared with candidate 1. Chaos/degraded attestation partial `[S]`.

---

## Security invariants

1. No external effect without a Reality Boundary request (X-1); no Reality Boundary request without a selected winning branch (X-2); no winning branch without collapse lineage (X-3). `[S]` (effect boundary) / `[P]`
2. No effect-bearing branch proceeds without adversarial review state (X-4). `[P]`
3. Temporal Echoes remain non-effecting until elevated through collapse and the Reality Boundary (TE-7). `[P]`
4. Heat may influence pacing/escalation but must never authorize bypass of governed execution (CH-3). `[P]`
5. No causal record outranks its receipt anchors (X-6, CT-3/CT-4). `[P]` over `[S]`
6. Reduced human oversight may alter interaction style but may not reduce governance rigor (X-8, OE-2). `[P]`
7. Deliberation evidence is **product-only** and never represented as standards-backed. `[P]`

---

## Audit questions answered

- **What alternatives were considered, and why did the chosen path win?** → Deliberation evidence + `BranchCollapseRecord` (Causation, product-only). `[P]`
- **What risks were surfaced internally before the action?** → Adversarial review findings + heat profile at submission. `[P]`
- **Was the executed effect authorized, and is it reconstructable?** → Effect-point receipts + Evidence Pack + Collective Causal Record (Authority + Causation). `[S]`
- **Did reduced human oversight weaken governance?** → Oversight profile + compensating safeguards, queryable per effect-bearing record (OE-4). `[P]`

---

## Test obligations

1. Attempt to submit a `CollapsedLoser` as an effect request; assert RB-2 fail-closed and a recorded denial (Scenario 09). `[S]` / `[P]`
2. Assert every execution-eligible plan originates from an explicit collapse event identifying its alternatives (TE-1, TE-2). `[P]`
3. Assert loser branches are archived/pruned with rationale, never silently deleted (TE-3). `[P]`
4. Reconstruct, offline, the considered alternatives and the selection reason from the Evidence Pack (Causation falsification). `[P]`
5. Assert deliberation evidence is tagged product-only everywhere it appears (G-TAG). `[P]`
6. Reduce oversight depth and assert a compensating increase in machine-side observability (OE-3, Compensation Rule). `[P]`
7. Assert no Cognition-Plane interface can emit an effect payload directly (RB-6). `[S]` / `[P]`

---

## Out of scope

- The atomic governed-execution mechanics at the effect point → candidate 1.
- Auditor-initiated reconstruction workflow → candidate 3.
- Tool-boundary admission specifics → candidate 4.
- Tenant/sandbox provisioning → candidate 5.
- Degraded/timeout resolution detail → candidate 6.
- Telemetry metric model + harness → candidate 7.
- **Any extension of CAES to absorb deliberation evidence** (forbidden under Position A absent an explicit future CAES amendment), and any CAES/CPP normative authoring, whitepaper, website, or product code.

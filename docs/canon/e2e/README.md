# CANONICAL E2E PROCESS DEFINITIONS
### Operation CLEAN EDGE · Milestone M4

> **Status:** Canonical. Authored under Operation CLEAN EDGE **M4** (vNext strategy/governance track).
> **Boundary:** Strategy/governance canon only. Defines processes; changes no code, website, standard, whitepaper, or product behavior. No documents deleted.
> **Source candidates:** `../../strategy/E2E-PROCESS-CANDIDATES.md` (the 7).
> **Controlling sequencer:** `../../strategy/OPERATION-CLEAN-EDGE-Implementation-Plan-v0.2.md` (§5 M4).

## What this directory is

The seven end-to-end processes that cut across Keon's product surfaces, each formalized from a one-line M4 candidate into a full E2E specification and a test target. Together they are intended to be **complete**: any consequential, cross-surface process Keon performs should be representable as one of these seven, or a composition of them.

`[G-FALSIFY: A consequential, cross-surface process Keon performs cannot be represented as one of these seven (or a composition). If such a process exists, this set is incomplete and must be revised.]`

## Canonical thesis (verbatim)

> **Keon governs the reasoning-to-action chain for autonomous AI systems and produces audit-ready evidence for consequential action.**

## Tag legend (G-TAG)

- **`[S]` standards-backed** — anchored in canonical CAES / CPP (CAES Working Group). These docs *reference* the standard; they do not author it.
- **`[P]` product-only** — a Keon product property; real and falsifiable, not a standards claim.
- **`[G-FALSIFY: …]`** — the concrete test a skeptic runs to prove a headline claim false.

**Position A (locked):** deliberation / "alternatives-considered" evidence is **product-only**, lives in **Cortex / the Evidence Pack**, and is **never** tagged `[S]`.

## The five surfaces (+ standard)

| Surface | Canonical role |
| :--- | :--- |
| **Collective** | Governs cognition before action. `[P]` |
| **Cortex** | Preserves causal truth and reconstructable evidence; default home of deliberation evidence. `[P]` |
| **MCP Gateway** | Governs execution at the tool boundary: admission, identity/scope binding, non-bypassability; routes to Runtime. Does **not** own policy evaluation, authorization, execution mechanics, or receipt emission. `[P]` |
| **Runtime** | Owns authority, the policy decision, execution mechanics, and receipt emission. `[P]` |
| **Control** | Operator/tenant/audit/evidence cockpit; does not itself authorize actions. `[P]` |
| **CAES / CPP** | An independent, complementary standard — *we enable / we map to*, never *a profile of*. `[S]` |

## The seven processes

| # | Process | Candidate | Primary surfaces | One-line purpose |
| :--- | :--- | :--- | :--- | :--- |
| 1 | [AUTONOMOUS-ACTION-GOVERNANCE](AUTONOMOUS-ACTION-GOVERNANCE.md) | 1 | Gateway · Runtime · Cortex (Control retrieval; Collective upstream) | Govern one autonomous action from intent → authorized execution → signed receipt. |
| 2 | [COLLECTIVE-REASONING-TO-EXECUTION-CANDIDATE](COLLECTIVE-REASONING-TO-EXECUTION-CANDIDATE.md) | 2 | Collective · Cortex · Gateway · Runtime · Control | Carry contested multi-agent cognition to governed execution; **carries deliberation evidence** (product-only, non-effecting until the Runtime decision). |
| 3 | [AUDIT-EVIDENCE-RECONSTRUCTION](AUDIT-EVIDENCE-RECONSTRUCTION.md) | 3 | Control · Cortex · CAES/CPP | Reconstruct the full causal account of a consequential action from Cortex, offline, for an auditor. |
| 4 | [MCP-TOOL-GOVERNANCE](MCP-TOOL-GOVERNANCE.md) | 4 | MCP Gateway · Runtime · Cortex | Enforce policy at the tool boundary for every Effect-Boundary-crossing tool invocation. |
| 5 | [TENANT-SANDBOX-ACTIVATION](TENANT-SANDBOX-ACTIVATION.md) | 5 | Control · Runtime · Cortex · Gateway | Stand up and bind a governed tenant sandbox with its policy and evidence scope. |
| 6 | [FAILURE-DEGRADED-MODE](FAILURE-DEGRADED-MODE.md) **·first-tier** | 6 | Runtime · Gateway · Cortex · Control | Resolve degraded operation to `Denied` / `RequiresHumanAuthorization` (timeout → Denied); never receiptless. Encodes **D3**. |
| 7 | [PUBLIC-TELEMETRY-PROOF](PUBLIC-TELEMETRY-PROOF.md) | 7 | Runtime · Gateway · Cortex · Control · CAES/CPP (partial) | Produce buyer-reproducible telemetry evidence of viability — **definitions + reproduction method only, no numbers**. |

## The Three Proofs (mapping)

Every process contributes to one or more of the three proofs:

| Proof | Question | Backing | Carried primarily by |
| :--- | :--- | :--- | :--- |
| **Authority** | Was it allowed? | Receipt · PolicyHash · fail-closed — CAES L1/L2, CPP `[S]` | Processes 1, 4, 5 |
| **Causation** | What happened, why, what else was considered? | Causal spine + Evidence Pack `[S]` (CAES L2 + Cortex); deliberation evidence `[P]` | Processes 1, 2, 3 |
| **Viability** | What did it cost? Did it stay up? | Telemetry `[P]` + chaos/degraded attestation partial `[S]` (CAES L3) | Processes 6, 7 |

> **House line:** *Execution proposes. Governance decides. Receipts prove. Telemetry attests.*

## Per-process format

Every process file follows the same section order:

> Purpose · Trigger · Actors · Preconditions · Main flow · Failure flow · Required receipts/evidence · Required telemetry · Security invariants · Audit questions answered · Test obligations · Out of scope.

## Locked premises honored across all seven

- **Effect Boundary taxonomy** (`ExternalSideEffect`, `HumanFacingOutput`, `GovernanceRelevantState`, `SafetyCriticalActuation`, `WorkflowTransition`) defines what is consequential; micro-steps are not actions. `[S]`
- **Gateway/Runtime separation** (B1-corrected): the Gateway enforces the boundary; the Runtime owns authority, decision, execution mechanics, and receipt emission. `[P]`
- **Position A**: deliberation evidence is product-only, Cortex/Evidence Pack, never `[S]`; Effect-Boundary rejected branches stay in Cortex/Evidence Pack under heightened handling unless a future CAES amendment explicitly defines such an object. `[P]` / `[S]` (boundary only)
- **D3 degraded-mode law**: Denied or RequiresHumanAuthorization / suspend-escalate; timeout → Denied; receiptless execution never permitted. `[S]` / `[P]`
- **CAES independence (D1)**: independent + complementary; *we enable / we map to*, never *a profile of*. `[S]`
- **Telemetry**: buyer-reproducible — definitions, sources, reproduction method/harness only. No fake numbers, no benchmarks. `[P]`

## Scope boundaries (M4)

These documents define processes only. They do **not** edit CAES/CPP, the whitepaper, the website, product code, or the M0–M3 strategy docs; they delete nothing; and they start no later milestone (M5/M6/M7).

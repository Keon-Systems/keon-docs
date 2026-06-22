# AUTONOMOUS ACTION GOVERNANCE
### Canonical E2E Process — Candidate 1

> **Status:** Canonical E2E process definition. Authored under Operation CLEAN EDGE **M4**.
> **Boundary:** Strategy/governance canon. Defines a process; changes no code, website, standard, or product behavior.
> **Process candidate:** E2E candidate 1 — *Autonomous Action Governance* (`E2E-PROCESS-CANDIDATES.md`).
> **Primary surfaces:** MCP Gateway · Runtime · Cortex (Control for retrieval; Collective upstream when in Full Keon mode).

## Tag legend (G-TAG)

- **`[S]` standards-backed** — anchored in the canonical CAES / CPP standard (CAES Working Group). This document *references* the standard; it does not author it.
- **`[P]` product-only** — a property of the Keon product (Runtime/Gateway/Cortex mechanics, telemetry, deliberation depth). Real and falsifiable, but not a standards claim.
- **`[G-FALSIFY: …]`** — for a headline claim, the concrete test a skeptic runs to prove it false.

---

## Purpose

Govern a single autonomous action from **intent** through **authorized execution** to **signed receipt**, producing audit-ready evidence of authority, causation, and viability for that one action. This is the atomic unit of the thesis — *Keon governs the reasoning-to-action chain for autonomous AI systems and produces audit-ready evidence for consequential action.* Every other E2E process is a composition, specialization, or supporting case of this one. `[P]`

This process governs **one action that crosses an Effect Boundary**. Micro-steps of internal computation that do not cross a boundary are not actions and are out of scope (they are governed, where applicable, as cognition in candidate 2). `[S]` (Effect Boundary taxonomy is CAES-defined)

---

## Trigger

A proposed action reaches the **MCP Gateway** intending to cross exactly one **Effect Boundary**:

| Effect Boundary | Examples |
| :--- | :--- |
| `ExternalSideEffect` | Network calls, filesystem writes, external APIs |
| `HumanFacingOutput` | Messages, emails, tickets, notifications |
| `GovernanceRelevantState` | Policy changes, permission changes, memory writes |
| `SafetyCriticalActuation` | Actuation beyond configured safety thresholds |
| `WorkflowTransition` | Workflow completion, gate reached, run-state transition |

`[S]` (taxonomy) `[G-FALSIFY: An action with real-world consequence executes without being classifiable as a crossing of exactly one Effect Boundary in this taxonomy. If such an action exists, the trigger definition is incomplete.]`

---

## Actors

| Actor | Role in this process |
| :--- | :--- |
| **Proposing agent** (BYOAI model or Full-Keon Collective) | Forms the intent and proposes the action. Owns no authority. |
| **MCP Gateway** | Enforces boundary admission, binds tenant/actor identity and scope, guarantees non-bypassability; routes to Runtime for Decide/Execute. Does **not** own policy evaluation, authorization, execution mechanics, or receipt emission. `[P]` |
| **Runtime** | Owns authority, the policy decision, execution mechanics, and receipt emission. `[P]` |
| **Cortex** | Records the causal spine and assembles the Evidence Pack. `[P]` |
| **Control** | Operator/auditor retrieval surface (post-hoc). `[P]` |
| **CAES / CPP** | The independent standard against which the Authority proof is measured. `[S]` |

---

## Preconditions

1. The tenant and actor are provisioned and bound to a policy scope (see candidate 5, *Tenant Sandbox Activation*). `[P]`
2. A policy is in force; its deterministic **PolicyHash** is computable at decision time — `PolicyHash = SHA-256(canonical(policy_id, policy_version, policy_effect))`, JCS-canonicalized. `[S]`
3. The receipt sink and causal-spine store are available and integrity-checkable; if not, the process resolves under the degraded-mode law (candidate 6). `[P]` / partial `[S]`
4. The proposed action references a single Effect Boundary; multi-boundary proposals are decomposed before this process runs. `[S]`

---

## Main flow

1. **Admission (Gateway).** The Gateway receives the proposed action, binds tenant/actor identity and scope, and confirms non-bypassability. No effect may reach reality except through this boundary. `[P]`
2. **Route to decide (Gateway → Runtime).** The Gateway routes the proposal to the Runtime for the authority decision. The Gateway does not evaluate policy. `[P]`
3. **Policy decision (Runtime).** The Runtime evaluates the proposal against the policy in force and yields exactly one disposition: `Approve` · `Rewrite` · `Block` · `RequiresHuman`. `[P]` The decision binds the deterministic **PolicyHash** in force. `[S]`
4. **Receipt mint + persist (Runtime → ARO).** On a proceed disposition, the Runtime mints a signed **Decision Receipt** (Ed25519) carrying the PolicyHash, identifiers, and timestamps, and persists it through the **Authoritative Receipt Outbox**: `Pending → Persisted → Verified` with byte-level write-then-verify readback. Authority is granted only after `Verified`. `[S]`
5. **Spine append (Runtime → Cortex).** The causal spine is appended **before** execution, in non-negotiable order: receipt verified → spine append → trace projection → evidence materialization. The spine records `ITrigger → IIntent → IJustification → IDecision → IAction → IOutcome`. `[S]`
6. **Execute (Runtime).** Only after receipt verification and spine append does the Runtime drive execution mechanics across the Gateway boundary. `[P]`
7. **Outcome + evidence (Runtime → Cortex).** Exactly one terminal `IOutcome` is bound to the `IAction`; Cortex materializes a cryptographically sealed, offline-verifiable **Evidence Pack** (input, policy snapshot, decision receipt, execution trace, spine refs). `[S]`
8. **Availability for audit (Control).** The action's authority, causation, and viability evidence becomes retrievable through Control. `[P]`

> **House line:** *Execution proposes. Governance decides. Receipts prove. Telemetry attests.*
> `[G-FALSIFY: Exhibit a Keon-governed consequential action for which no offline-verifiable account of authority, causation, and outcome can be produced. One such case falsifies this process.]`

---

## Failure flow

- **Block disposition.** The action does not proceed. A **Denial Receipt** is serialized, hashed, signed, ARO-verified, and spine-appended — denial is affirmative evidence, not a log. `[S]`
- **RequiresHuman disposition.** Execution is suspended pending explicit human authorization (suspend + escalate). `[P]` / `[S]` (degraded bounding)
- **Receipt verification failure (liar-store).** Byte-level readback mismatch → `DECISION_RECEIPT_VERIFICATION_FAILED` → execution aborts, no partial state. `[S]`
- **PolicyHash mismatch / missing receipt.** Execution fails closed. Absence of receipt equals absence of authority (FP-07). `[S]`
- **Spine append failure.** Execution aborts before projection or evidence materialization — no action without a ledger record. `[S]`
- **Tenant/actor binding failure.** Boundary violation → denial receipt, never silent access. `[S]`
- **Degradation / timeout.** Resolves to `Denied` or `RequiresHumanAuthorization`; timeout → `Denied`. **Receiptless execution is never permitted.** Full treatment in candidate 6 (`FAILURE-DEGRADED-MODE.md`). `[S]` (degraded bounding) / `[P]`

---

## Required receipts / evidence

| Artifact | Provenance | Tag |
| :--- | :--- | :--- |
| Signed Decision Receipt (PolicyHash-bound, Ed25519) | Runtime → ARO | `[S]` |
| Denial Receipt (on Block / boundary violation) | Runtime → ARO | `[S]` |
| Causal spine entry (`ITrigger…IOutcome`, append-only) | Cortex | `[S]` |
| Evidence Pack (sealed, offline-verifiable, tamper-evident) | Cortex | `[S]` |
| Deliberation evidence (only in Full Keon mode; "what else was considered") | Cortex / Evidence Pack | `[P]` — **never `[S]`** |

> Deliberation evidence is product-only by G0 Position A and is **never** tagged standards-backed. See candidate 2 and `DELIBERATION-EVIDENCE-MODEL.md`.

---

## Required telemetry

Per `TELEMETRY-PUBLIC-PROOF-POSTURE.md` — **definitions and reproduction method only; no values in canon.** This process emits into: decision latency (p50/p95/p99), gateway latency, receipt-persistence latency, evidence-pack generation time, fail-closed/deny/approve rates, policy-eval error rate, tenant/actor binding-failure rate. `[P]` Chaos/degraded-mode attestation anchors partially to CAES L3 ChaosTestAttestation. `[S]` (partial)

---

## Security invariants

1. No execution without a verified Decision Receipt — not by omission, configuration, or accident. `[S]`
2. The Gateway is non-bypassable; every Effect-Boundary crossing passes through **both** Gateway enforcement **and** a Runtime decision. `[P]` `[G-FALSIFY: an Effect-Boundary invocation that executes without traversing both. If that path exists, non-bypassability is broken.]`
3. Gateway/Runtime separation holds: the Gateway never owns policy evaluation, authorization, execution mechanics, or receipt emission. `[P]`
4. PolicyHash is deterministic and recomputable; a mismatch proves post-hoc modification. `[S]`
5. The spine is append-only; revocation is an append, not a mutation. `[S]`
6. Uncertainty resolves to denial — no silent degradation paths. `[S]`

---

## Audit questions answered

- **Was it allowed?** → Decision Receipt + PolicyHash + fail-closed disposition (Authority proof). `[S]`
- **What happened and why?** → Causal spine + Evidence Pack (Causation proof). `[S]`
- **What else was considered?** → Deliberation evidence, if Full Keon mode (Causation proof, product-only). `[P]`
- **What did it cost / did it stay up?** → Telemetry + degraded-mode behavior (Viability proof). `[P]` / partial `[S]`

---

## Test obligations

1. Prove no execution path exists without a verified receipt (FP-01, FP-07; Scenario 04). `[S]`
2. Recompute PolicyHash from canonical inputs and match the receipt (FP-02; Scenario 03). `[S]`
3. Force a liar-store readback mismatch and assert abort (FP-03; Scenario 02). `[S]`
4. Attempt a spine reorder/edit and assert detection (FP-04; Scenario 05). `[S]`
5. Alter one Evidence Pack byte and assert seal invalidation, offline (FP-05). `[S]`
6. Force a Block and assert a signed, spine-appended Denial Receipt exists (FP-06; Scenario 01). `[S]`
7. Reconstruct directive→outcome offline for one consequential action (Causation). `[S]`

---

## Out of scope

- Multi-agent contested cognition prior to action → candidate 2 (`COLLECTIVE-REASONING-TO-EXECUTION-CANDIDATE.md`).
- Full auditor-driven reconstruction workflow → candidate 3 (`AUDIT-EVIDENCE-RECONSTRUCTION.md`).
- Tool-boundary policy specifics for every invocation → candidate 4 (`MCP-TOOL-GOVERNANCE.md`).
- Tenant/sandbox provisioning → candidate 5 (`TENANT-SANDBOX-ACTIVATION.md`).
- Degraded/failure resolution detail → candidate 6 (`FAILURE-DEGRADED-MODE.md`).
- Telemetry metric model and reproduction harness → candidate 7 (`PUBLIC-TELEMETRY-PROOF.md`).
- Any CAES/CPP normative definition (owned externally), whitepaper, website, or product code.

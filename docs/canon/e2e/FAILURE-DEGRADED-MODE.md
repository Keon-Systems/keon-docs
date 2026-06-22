# FAILURE / DEGRADED MODE
### Canonical E2E Process — Candidate 6 · **FIRST-TIER**

> **Status:** Canonical E2E process definition. Authored under Operation CLEAN EDGE **M4**.
> **Boundary:** Strategy/governance canon. Defines a process; changes no code, website, standard, or product behavior.
> **Process candidate:** E2E candidate 6 — *Failure / Degraded Mode* (`E2E-PROCESS-CANDIDATES.md`).
> **Elevation:** This is a **first-tier** process, not one-of-seven. It encodes locked premise **D3** — the real enterprise objection behind the Viability proof.
> **Primary surfaces:** Runtime (decision/fail-closed) · MCP Gateway (boundary) · Cortex (recorded failure) · Control (escalation/operator).

## Tag legend (G-TAG)

- **`[S]` standards-backed** — anchored in canonical CAES / CPP (CAES Working Group). Referenced, not authored here.
- **`[P]` product-only** — a Keon product property; real and falsifiable, not a standards claim.
- **`[G-FALSIFY: …]`** — the concrete test that would prove a headline claim false.

---

## Purpose

Resolve **degraded operation** to `Denied` or `RequiresHumanAuthorization` (suspend + escalate), with **timeout → Denied**, and **never** to receiptless execution. This process is the bounded behavior of every other process under failure: when any precondition for governed execution cannot be met, the system denies or escalates — it does not bypass. Degraded-mode behavior is itself the most-scrutinized property in regulated industries (e.g., safety-critical actuation), which is why it is first-tier. `[S]` (degraded bounding by CAES) / `[P]`

> **Degraded-mode law (D3, locked):** When Keon is degraded, an effect-bound action resolves to **Denied** or **RequiresHumanAuthorization** (suspend + escalate; **timeout → Denied**). **Receiptless execution is never permitted under any degraded path.** `[S]` / `[P]`

---

## Trigger

Any governed action (candidates 1–5) encounters a failure or degradation of a governance precondition: receipt sink unavailable or failing readback, spine append failure, PolicyHash unavailable/mismatched, tenant/actor binding failure, policy-eval error, correlation/causation drift, or a decision/persistence timeout. `[P]`

---

## Actors

| Actor | Role |
| :--- | :--- |
| **Runtime** | Detects the governance-precondition failure and resolves the disposition to `Denied` or `RequiresHumanAuthorization`; records the failure as an event. `[P]` |
| **MCP Gateway** | Holds the boundary closed; no effect crosses without a verified proceed disposition. `[P]` |
| **Cortex** | Records the failure/denial as a first-class evidentiary event (not a silent malfunction). `[P]` |
| **Control** | Receives the escalation for `RequiresHumanAuthorization`; surfaces the degraded-mode state to operators. `[P]` |
| **CAES / CPP** | Bounds degraded behavior; anchors chaos/degraded attestation (L3, partial). `[S]` |

---

## Preconditions

1. A governed action is in flight under one of candidates 1–5. `[P]`
2. The fail-closed invariant is active by default: uncertainty resolves to denial, never to execution. `[S]`
3. An escalation path to a human exists for `RequiresHumanAuthorization`. `[P]`
4. A bounded timeout is defined for suspended actions, resolving to `Denied` on expiry. `[S]` / `[P]`

---

## Main flow (resolution paths)

1. **Detect (Runtime).** A governance precondition cannot be satisfied (receipt, PolicyHash, spine, binding, policy-eval, correlation, or timeout). `[P]`
2. **Classify the disposition.**
   - **Denied** — the action is refused; a **Denial Receipt** is serialized, hashed, signed, ARO-verified, and spine-appended. Denial is affirmative proof the guardrail functioned. `[S]`
   - **RequiresHumanAuthorization** — the action is **suspended** and **escalated** to a human via Control; it remains non-effecting while suspended. `[P]` / `[S]` (disposition bounded by CAES)
3. **Bound the suspension (timeout).** If a suspended action is not explicitly authorized within its timeout, it resolves to **Denied** (timeout → Denied). `[S]` / `[P]`
4. **Hold the boundary (Gateway).** Throughout, no effect crosses the Effect Boundary without a verified proceed disposition — there is no fallback path. `[P]`
5. **Record (Cortex).** The failure/denial/escalation is recorded as a canonical event with its reason; degraded-mode entries are counted and paired with their resolution disposition. `[P]`
6. **Recover or stay closed.** When preconditions are restored, normal governed flow resumes; until then, the system stays closed. No silent degradation path exists. `[S]`

> `[G-FALSIFY: Produce a receiptless execution under degradation — an effect that crossed an Effect Boundary while a governance precondition was unmet and no verified receipt/disposition existed. One such case falsifies D3 and this process.]`

---

## Failure flow (failures *of* the degraded path itself)

- **Escalation channel down.** If `RequiresHumanAuthorization` cannot be delivered, the action does not proceed; it resolves to `Denied` on timeout — escalation failure never converts to execution. `[S]` / `[P]`
- **Denial-receipt persistence failure.** If the denial cannot be durably recorded, the action still does not execute; the system fails closed and the unrecorded-denial condition is itself a constrained-boundary event routed for evidence (DLQ). `[S]` / `[P]`
- **Ambiguous state.** Any ambiguity, partial failure, or missing receipt resolves to denial (ADDENDUM 06/13). `[S]`

---

## Required receipts / evidence

| Artifact | Provenance | Tag |
| :--- | :--- | :--- |
| Denial Receipt (signed, ARO-verified, spine-appended) | Runtime → ARO | `[S]` |
| Recorded failure event (constraint activation, with reason) | Cortex | `[P]` / `[S]` (denial-as-evidence) |
| Escalation record (`RequiresHumanAuthorization` → Control) | Runtime / Control | `[P]` |
| Timeout-to-Denied transition record | Runtime | `[S]` / `[P]` |
| Chaos / degraded-mode attestation (partial) | per CAES L3 ChaosTestAttestation | `[S]` (partial) |

---

## Required telemetry

Definitions/reproduction only (candidate 7). This process is the primary source for: **degraded-mode count** (paired with resolution disposition), **fail-closed / deny rates**, and the **degraded-mode behavior** family. `[P]` Chaos and degraded-mode attestation may anchor to **CAES L3 ChaosTestAttestation** — the operational counts remain product telemetry, the chaos attestation portion is `[S]` (partial). `[S]` / `[P]`

---

## Security invariants

1. **Receiptless execution is never permitted** under any degraded path — the central D3 invariant. `[S]` / `[P]`
2. Degradation resolves only to `Denied` or `RequiresHumanAuthorization`; **timeout → Denied**. `[S]` / `[P]`
3. Uncertainty is denial — no silent degradation paths exist (ADDENDUM 06, FP-09). `[S]`
4. Suspended actions are non-effecting until explicit human authorization. `[P]`
5. Failure is a recorded event, not a silent malfunction (FP-09). `[P]` / `[S]` (denial-as-evidence)
6. Reduced human oversight depth may not weaken this floor; it triggers compensating safeguards (OE-2/OE-3). `[P]`

---

## Audit questions answered

- **What did the system do when it could not fully govern?** → Denied or suspended-and-escalated; never executed receiptless. `[S]` / `[P]`
- **Is there proof the guardrail functioned?** → Signed, spine-appended Denial Receipt. `[S]`
- **What happens to a suspended action that is never authorized?** → It resolves to Denied at timeout. `[S]` / `[P]`
- **How often does the system degrade, and how does it resolve?** → Degraded-mode count paired with resolution disposition (telemetry). `[P]`
- **Is degraded behavior independently attestable?** → Partially, via CAES L3 ChaosTestAttestation. `[S]` (partial)

---

## Test obligations

1. Induce sink/spine/PolicyHash/binding failures and assert deny-or-suspend, **never** execution (Scenarios 02/04/06/07/08). `[S]`
2. Suspend an action for `RequiresHumanAuthorization`, let the timeout expire, assert resolution to `Denied`. `[S]` / `[P]`
3. Assert every denial produced a signed, ARO-verified, spine-appended receipt (FP-06). `[S]`
4. Sever the escalation channel and assert the suspended action does not execute. `[S]` / `[P]`
5. Drive chaos/degraded scenarios and map results to the CAES L3 ChaosTestAttestation criteria (partial). `[S]`
6. Assert degraded-mode count is emitted and paired with resolution disposition. `[P]`

---

## Out of scope

- Normal (non-degraded) governed execution → candidate 1.
- Multi-agent deliberation → candidate 2.
- Auditor reconstruction → candidate 3.
- Tool-boundary admission → candidate 4.
- Tenant/sandbox provisioning → candidate 5.
- Telemetry metric model + reproduction harness → candidate 7.
- Any CAES/CPP normative authoring (including any extension of degraded-mode normative text), whitepaper, website, or product code.

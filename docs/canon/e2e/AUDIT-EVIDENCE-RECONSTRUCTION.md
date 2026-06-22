# AUDIT EVIDENCE RECONSTRUCTION
### Canonical E2E Process — Candidate 3

> **Status:** Canonical E2E process definition. Authored under Operation CLEAN EDGE **M4**.
> **Boundary:** Strategy/governance canon. Defines a process; changes no code, website, standard, or product behavior.
> **Process candidate:** E2E candidate 3 — *Audit Evidence Reconstruction* (`E2E-PROCESS-CANDIDATES.md`).
> **Primary surfaces:** Control (auditor cockpit) · Cortex (causal truth + Evidence Pack) · CAES/CPP (verification standard).

## Tag legend (G-TAG)

- **`[S]` standards-backed** — anchored in canonical CAES / CPP (CAES Working Group). Referenced, not authored here.
- **`[P]` product-only** — a Keon product property; real and falsifiable, not a standards claim.
- **`[G-FALSIFY: …]`** — the concrete test that would prove a headline claim false.

---

## Purpose

Reconstruct the **full causal account** of a consequential action from Cortex for an auditor — **offline, without live-system access** — covering *was it allowed, what happened and why, what else was considered, and what it cost.* This process turns the evidence produced by candidates 1 and 2 into an auditor-verifiable answer. It is the demand side of the thesis: audit-ready evidence is only audit-ready if a third party can reconstruct it independently. `[P]`

> **If an action cannot be proven to an auditor, it is a liability.** The burden of proof shifts from "trust the operator" to "verify the artifact." `[S]` (Evidence Pack format/offline-verifiability) / `[P]`

---

## Trigger

An operator, auditor, regulator, or legal/discovery process requests the evidentiary account of one or more consequential actions through **Control** — by action, receipt, spine, intent, participant, or correlation lineage. `[P]`

---

## Actors

| Actor | Role |
| :--- | :--- |
| **Auditor / regulator / investigator** | Requests and independently verifies the account; needs no live Keon runtime. `[S]` (offline verification) |
| **Control** | The retrieval cockpit: locates records and returns the Evidence Pack(s) and causal record(s). Does not author or alter evidence. `[P]` |
| **Cortex** | System of record: causal spine, Evidence Pack, Collective Causal Record, deliberation evidence. `[P]` |
| **CAES / CPP** | The published criteria against which receipts, PolicyHash, and seal integrity are checked. `[S]` |
| **Runtime / Gateway** | Not in the reconstruction loop — their artifacts were produced upstream; reconstruction does not require them online. `[P]` |

---

## Preconditions

1. The action was governed by candidate 1 (and, in Full Keon mode, candidate 2): a verified receipt, an append-only spine entry, and a sealed Evidence Pack exist. `[S]`
2. Evidence Packs are self-contained (input, policy snapshot, decision receipt, execution trace, spine ids) and offline-verifiable. `[S]`
3. The causal record is queryable by intent, branch, receipt, participant, and correlation (CT-6). `[P]`
4. Deliberation evidence, where it exists, is retrievable from Cortex / the Evidence Pack as product-only evidence. `[P]`

---

## Main flow

1. **Locate (Control).** Resolve the request to one or more spine ids / receipt ids / causal-record ids via correlation lineage. `[P]`
2. **Retrieve (Cortex → Control).** Return the sealed Evidence Pack(s) and, in Full Keon mode, the `CollectiveCausalRecord` and deliberation evidence. `[P]`
3. **Verify authority offline (Auditor).** Recompute `PolicyHash = SHA-256(canonical(...))` from the policy snapshot and confirm it matches the Decision Receipt; confirm the Ed25519 signature and ARO-verified state. `[S]`
4. **Verify integrity offline (Auditor).** Confirm the Evidence Pack seal validates and that altering any single byte invalidates it; confirm spine ordering is intact and append-only. `[S]`
5. **Reconstruct causation (Auditor).** Walk `ITrigger → IIntent → IJustification → IDecision → IAction → IOutcome` from directive to outcome; confirm exactly one terminal `IOutcome` per `IAction`. `[S]`
6. **Reconstruct deliberation (Auditor, Full Keon only).** From deliberation evidence, rebuild the alternatives considered, their dispositions, and the reason the chosen path won — understood as **product-only** evidence. `[P]`
7. **Reconstruct viability (Auditor).** Retrieve the telemetry account and degraded-mode disposition for the action's window (definitions/method per candidate 7). `[P]` / partial `[S]`
8. **Conclude.** The three proofs (Authority, Causation, Viability) are independently established for the action. `[S]` / `[P]`

> `[G-FALSIFY: Hand an auditor a Keon-governed consequential action and have them attempt to rebuild the directive→outcome account from the Evidence Pack offline. If the account cannot be rebuilt — or if deliberation evidence is presented as standards-backed when it is product-only — this reconstruction process fails.]`

---

## Failure flow

- **Missing receipt.** Absence of receipt equals absence of authority (FP-07): the action cannot be proven authorized, and that absence is itself the finding. `[S]`
- **PolicyHash mismatch.** Recomputed hash ≠ receipt hash → post-hoc policy modification detected (Scenario 03). `[S]`
- **Seal invalidation.** A tampered Evidence Pack fails verification (FP-05; Scenario 05) — tamper is cryptographically evident, not silently accepted. `[S]`
- **Spine discontinuity.** A reorder/insert attempt breaks partition sequence and is detected (FP-04). `[S]`
- **Deliberation absent.** A Full-Keon action whose considered alternatives were silently discarded is a failure of candidate 2's guarantees (FP-11) — surfaced here, fixed upstream. `[P]`
- **Mis-tagging.** Any reconstruction that presents deliberation as `[S]` is a G-TAG / G-DRIFT violation. `[P]`

---

## Required receipts / evidence

| Artifact | Provenance | Tag |
| :--- | :--- | :--- |
| Sealed, offline-verifiable Evidence Pack | Cortex | `[S]` |
| Decision / Denial / Outcome Receipts (PolicyHash-bound, Ed25519) | Runtime → ARO (retrieved) | `[S]` |
| Append-only causal spine (`ITrigger…IOutcome`) | Cortex | `[S]` |
| `CollectiveCausalRecord` + reconstructive anchor (Full Keon) | Cortex | `[P]` / anchors `[S]` |
| Deliberation evidence | Cortex / Evidence Pack | `[P]` — **never `[S]`** |

---

## Required telemetry

Definitions/reproduction only (candidate 7). This process exposes: evidence-pack generation time and retrieval scope (action count/complexity) as product telemetry `[P]`. Reconstruction itself emits no Effect-Boundary action — it is read-only over canonical truth. `[P]`

---

## Security invariants

1. Reconstruction is **read-only**: it never mutates the spine, receipts, or Evidence Pack. `[P]`
2. Offline-verifiability holds — no live Keon runtime is required to verify authority, integrity, or causation. `[S]`
3. Receipt-grounded truth outranks any witness narrative or summary (CT-3); reconstructive anchors never supersede receipts (CT-4). `[P]` over `[S]`
4. The standard's criteria are applied independently of the Keon product — a CAES conformance claim is checkable against published CAES criteria without Keon. `[S]` `[G-FALSIFY: A CAES conformance claim cannot be checked against published CAES criteria independently of Keon's product.]`
5. Deliberation evidence is verifiable but product-only; it is never elevated to a standards claim during audit. `[P]`

---

## Audit questions answered

- **Was it allowed?** → Recomputed PolicyHash + verified signed receipt + fail-closed disposition. `[S]`
- **What happened and why?** → Reconstructed causal spine + Evidence Pack. `[S]`
- **What else was considered, and why this path?** → Deliberation evidence (product-only). `[P]`
- **What did it cost / did it stay up?** → Telemetry account + degraded-mode disposition for the window. `[P]` / partial `[S]`
- **Can a third party prove all of the above without us?** → Yes, offline, from the sealed artifact. `[S]`

---

## Test obligations

1. Reconstruct directive→outcome offline for a sampled consequential action; assert success (FP-04/05; Causation). `[S]`
2. Recompute PolicyHash and match the receipt; then alter the policy snapshot and assert mismatch detection (FP-02; Scenario 03). `[S]`
3. Flip one Evidence Pack byte; assert seal invalidation (FP-05). `[S]`
4. Query the causal record by each of intent/branch/receipt/participant/correlation and assert retrieval (CT-6). `[P]`
5. For a Full-Keon action, reconstruct the alternatives and selection reason; assert presence and product-only tagging (FP-11). `[P]`
6. Assert reconstruction performs no Effect-Boundary action and no mutation. `[P]`

---

## Out of scope

- Producing the evidence (that is candidates 1 and 2); this process consumes it.
- Tool-boundary admission → candidate 4.
- Tenant/sandbox provisioning and evidence-scope setup → candidate 5.
- Degraded/timeout resolution → candidate 6.
- Telemetry metric model + reproduction harness → candidate 7.
- Any CAES/CPP normative authoring, whitepaper, website, or product code.

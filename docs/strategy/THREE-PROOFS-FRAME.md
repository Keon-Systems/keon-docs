# THREE PROOFS FRAME

> **Status:** Canonical. Authored under PR-M0 (Strategic Reset Canon).
> **Boundary:** Strategy only. No code, website, whitepaper, CAES/CPP, or deletion changes.

## Tag conventions

- `[G-TAG:S]` standards-backed · `[G-TAG:P]` product-only · `[G-FALSIFY: …]` for headline claims.

## The frame

A consequential action satisfies the thesis only when **three independent proofs** hold. Each answers a different question and is backed by a different mix of standard and product.

> **Authority** — *was it allowed?*
> **Causation** — *what happened, why, and what else was considered?*
> **Viability** — *what did it cost?*

House line (optional): **"Execution proposes. Governance decides. Receipts prove. Telemetry attests."**

---

## Proof 1 — Authority (was it allowed?)

**Question:** Was this action permitted at the moment it executed?
**Evidence:** Signed receipts, PolicyHash binding, fail-closed disposition.
**Backing:** CAES **L1/L2** and **CPP**. `[G-TAG:S]`
**Product mechanics:** Runtime emits the receipt; Gateway enforces at the boundary. `[G-TAG:P]`

`[G-FALSIFY: Present an executed consequential action with no signed receipt bound to the policy that authorized it, or a receipt whose PolicyHash does not verify against the policy in force. Either breaks the Authority proof.]`

---

## Proof 2 — Causation (what happened, why, what else was considered?)

**Question:** What occurred, for what reason, and what alternatives were weighed?
**Evidence:** Evidence Pack + causal spine (`ITrigger→IIntent→IJustification→IDecision→IAction→IOutcome`) + **deliberation evidence**.
**Backing:**
- Causal spine + Evidence Pack reconstruction: CAES **L2** + Cortex. `[G-TAG:S]` (CAES L2 portion) / `[G-TAG:P]` (Cortex reconstruction)
- **Deliberation evidence ("what else was considered"): `[G-TAG:P]` — product-only pending the G0 ruling.** It is **not** CAES-backed today. See `DELIBERATION-EVIDENCE-MODEL.md`.

`[G-FALSIFY: Hand an auditor a consequential action and have them attempt to reconstruct the causal account from the Evidence Pack. If the account cannot be rebuilt, or if deliberation evidence is presented as standards-backed when it is product-only, the Causation proof fails.]`

> **G-TAG discipline note:** Do not imply Collective deliberation or alternatives-considered evidence is CAES-backed. Until G0 rules otherwise, it is `product-only`.

---

## Proof 3 — Viability (what did it cost?)

**Question:** Is governed operation operationally viable, and at what overhead?
**Evidence:** Telemetry (latency / availability / rate families), degraded-mode behavior, chaos attestation.
**Backing:**
- Chaos / degraded-mode attestation: CAES **L3 ChaosTestAttestation** — **partial**. `[G-TAG:S]`
- Latency / availability / rate families: product-only, buyer-reproducible. `[G-TAG:P]`

`[G-FALSIFY: Follow the published methodology and verification harness to reproduce the viability metrics against a Keon deployment. If they cannot be reproduced within stated tolerance, or any non-chaos metric is tagged standards-backed, the Viability proof fails.]` See `TELEMETRY-PUBLIC-PROOF-POSTURE.md`.

---

## G-TAG summary across the three proofs

| Proof | Standards-backed `[S]` | Product-only `[P]` |
|---|---|---|
| Authority | Receipts/PolicyHash/fail-closed → CAES L1/L2, CPP | Runtime/Gateway mechanics |
| Causation | Causal spine + Evidence Pack → CAES L2 + Cortex | Cortex reconstruction; **deliberation evidence (pending G0)** |
| Viability | Chaos attestation → CAES L3 (partial) | Latency/availability/rate telemetry |

`[G-TAG:P]` `[G-FALSIFY: Any cell above can be shown to mis-tag a claim — e.g., a product-only claim sold as standards-backed, or a CAES-backed claim demoted to product-only. One mis-tag falsifies the table.]`

## Definition of done (for this frame)

- Three proofs defined by question, evidence, and backing.
- Each proof carries a G-FALSIFY method.
- Standards-backed vs product-only marked per claim (G-TAG), with deliberation evidence explicitly product-only pending G0.
- House line included as optional.

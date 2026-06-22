# DELIBERATION EVIDENCE MODEL

> **Status:** Canonical framing. Authored under PR-M0 (Strategic Reset Canon).
> **Boundary:** This document frames the deliberation-evidence object and its open question. **The open question is now RESOLVED by G0 to Position A** (see below). The framing and proofs are retained; the resolution is recorded where the question was posed.

## Tag conventions

- `[G-TAG:S]` standards-backed · `[G-TAG:P]` product-only · `[G-FALSIFY: …]` for headline claims.

## What deliberation evidence is

Deliberation evidence answers: **"what alternatives were considered?"** It is the record of the reasoning that *did not* become the action. Its object has three parts:

1. **Candidate intents / justifications** — the alternatives that were generated and weighed (each with its own intent and justification).
2. **Selection disposition** — for each candidate, whether it was selected or rejected.
3. **Reason** — why the selected candidate was chosen and why the rejected ones were not.

This is distinct from the *chosen* path's evidence. The chosen path is already captured by the causal spine. Deliberation evidence is everything the spine *discarded*.

`[G-TAG:P]`

---

## Why the linear spine cannot natively hold it

The causal spine is linear:

```
ITrigger → IIntent → IJustification → IDecision → IAction → IOutcome
```

Each node has one successor. The spine records the path that **was taken**. By construction it has no slot for **rejected branches** — there is no `IIntent` node that leads to no `IDecision`, no `IJustification` that was overruled. The moment a candidate is rejected, it leaves the spine; the spine only ever holds the survivor.

So deliberation evidence — which is precisely the set of rejected `IIntent`/`IJustification` branches plus the disposition and reason — has **no native home** on the spine. Forcing it onto the spine would require either (a) inventing non-linear branch nodes the spine semantics do not support, or (b) collapsing rejected candidates into the chosen `IJustification`, which loses the disposition and reason structure.

`[G-TAG:P]` `[G-FALSIFY: Show a spine instance that natively stores a rejected IIntent branch with its disposition and reason, without adding non-spine structure. If the spine can do this unaided, this section is wrong.]`

---

## Default home (per locked premise)

Per the locked premises, deliberation evidence **lives in Cortex / the Evidence Pack**. When a branch crossed, or would have crossed, an Effect Boundary, that record **stays in Cortex / the Evidence Pack** but is subject to **heightened evidence handling** — the Effect Boundary is the carve-out *trigger*, not a relocation to a different (e.g. CAES-owned) home. The Evidence Pack is non-linear and can carry the candidate set, dispositions, and reasons alongside the spine without distorting the spine.

`[G-TAG:P]` (Cortex/Evidence Pack as home, including Effect-Boundary branches)
`[G-TAG:S]` (Effect Boundary as the heightened-handling trigger — bound to the CAES Effect Boundary taxonomy; trigger only, not ownership)

---

## The question (RESOLVED BY G0 — Position A)

> **Where does deliberation evidence canonically live, and what, if anything, does the CAES standard owe it?**

The two candidate positions that were put to G0:

- **Position A — Cortex / Evidence Pack only (product-only).**
  Deliberation evidence is a product feature of the Evidence Pack. CAES stays narrow and says nothing about alternatives-considered. Cleanest boundary; risk that a buyer/regulator expects a *standards* claim about deliberation and finds only a product claim.

- **Position B — CAES extension.**
  CAES is extended to define deliberation evidence as a standards-backed object (likely tied to L2 + the Effect Boundary). Stronger external claim; risk of widening CAES beyond authority/causation and diluting its narrow scope.

### G0 ruling — Position A is adopted

1. **Default home confirmed.** Deliberation evidence lives in **Cortex / the Evidence Pack by default, product-only**. `[G-TAG:P]`
2. **CAES does not absorb it.** CAES does **not** take on alternatives-considered / deliberation evidence; it stays narrow (authority/causation). There is no standards-backed claim about deliberation. `[G-TAG:P]`
3. **Effect-Boundary branches stay in Cortex / Evidence Pack.** A rejected branch that *would have crossed* an Effect Boundary **remains in Cortex / the Evidence Pack** — it does **not** become a CAES-owned object — **unless a future CAES amendment explicitly defines such an object.** Absent that amendment, its tag stays `[G-TAG:P]`.
4. **Effect Boundary = heightened-handling trigger, not CAES ownership.** Crossing (or would-have-crossed) an Effect Boundary is the carve-out trigger for **heightened evidence handling** of the deliberation record; it does **not** transfer ownership of that record to CAES. The `[G-TAG:S]` on the Effect Boundary names the boundary taxonomy, not standards-backing of the deliberation evidence itself.

`[G-TAG:P]` `[G-FALSIFY: A shipped artifact claims CAES standards-backing (`G-TAG:S`) for deliberation / alternatives-considered evidence, or routes an Effect-Boundary rejected branch out of Cortex/Evidence Pack into a CAES-owned object, without a future CAES amendment that explicitly defines it. Either is a G0 / G-DRIFT violation.]`

## Definition of done (for this model doc)

- Deliberation-evidence object defined (candidates + disposition + reason).
- Linear-spine incompatibility argued with a falsification method.
- Default home stated per locked premise (Cortex/Evidence Pack; Effect-Boundary carve-out).
- The G0 question framed precisely and **resolved to Position A** (product-only; CAES does not absorb; Effect-Boundary branches stay in Cortex/Evidence Pack absent an explicit future CAES amendment; Effect Boundary = heightened-handling trigger, not CAES ownership).

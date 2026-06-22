# DOCUMENTATION CLEAN-SWEEP POLICY

> **Status:** Canonical policy. Authored under PR-M0 (Strategic Reset Canon).
> **Boundary:** This document **defines rules only**. It deletes nothing, moves nothing, edits no doc.
> **Execution:** Milestone M3 will execute this policy in a later, separate PR. M3 may not begin until G0 passes.

## Tag conventions

- `[G-TAG:S]` standards-backed · `[G-TAG:P]` product-only · `[G-FALSIFY: …]` for headline claims.

## Intent

Establish the rules by which documentation is later consolidated to a single canonical set, so that implementation, docs, and public messaging do not accumulate from a polluted base. The policy is conservative by design: **no doc is deleted until its still-valid content is provably preserved elsewhere.**

---

## Keep-criteria (a doc survives if ANY hold)

1. It maps cleanly to exactly one canonical product surface role (see `CANONICAL-PRODUCT-SURFACES.md`) or to one cross-surface process candidate (see `E2E-PROCESS-CANDIDATES.md`).
2. It states a still-valid invariant, contract, interface, or premise not yet captured in a surviving canonical doc.
3. It is a standards artifact (CAES/CPP) or normatively references one. *(Standards docs are out of scope for deletion entirely — see Hard boundaries.)*
4. It is required by an external obligation (regulatory, contractual, legal hold).

## Delete-criteria (a doc is a deletion **candidate** only if ALL hold)

1. Its content is superseded by, or fully duplicated in, a surviving canonical doc.
2. It carries no unique still-valid invariant (verified by the coverage check below).
3. It maps to no canonical surface and no process candidate.
4. It is not a standards artifact and not required by an external obligation.

> "Deletion candidate" ≠ "deleted." Candidacy only makes a doc eligible for the coverage check. Deletion happens in M3, never here.

---

## Mandatory pre-delete coverage check (the gate)

**No document may be deleted until every still-valid invariant it contains has been shown to exist in a surviving canonical doc.**

Procedure (M3 executes; PR-M0 only specifies):
1. Extract the candidate doc's invariants, contracts, and premises into a checklist.
2. For each item, cite the surviving canonical doc + location that preserves it.
3. Any item without a citation **blocks deletion** of the entire doc until the invariant is migrated into a canonical doc.
4. Only when the checklist is fully cited may the doc be deleted.

`[G-TAG:P]` `[G-FALSIFY: After M3 runs, take any deleted doc from version history, list its invariants, and find one with no home in a surviving canonical doc. If found, the coverage check was violated.]`

---

## Structural rules

- **No `/archive`, `/legacy`, `/old`, or `/deprecated` folders.** Consolidation means content lives in canonical docs or is gone (after coverage check) — not parked in a graveyard. History lives in version control, not in folders.
  `[G-TAG:P]` `[G-FALSIFY: Any such folder exists in the docs tree after M3.]`
- **`docs/README.md` is the future canonical index.** M3 (not M0) creates/owns it as the single entry point mapping every surviving doc to its surface or process. PR-M0 does **not** create it.
- One canonical doc per concept. Duplicated concepts are consolidated, not cross-linked into ambiguity.

## Hard boundaries (inherited, absolute)

- Do not delete documentation in PR-M0 (this PR defines policy only).
- Do not edit CAES/CPP. Standards artifacts are never deletion candidates.
- Do not rewrite the whitepaper. Do not touch website or product code.

## Definition of done (for this policy doc)

- Keep- and delete-criteria are explicit and decidable.
- The coverage check is specified as a hard gate with a falsification method.
- The no-graveyard rule and the `docs/README.md` future-index rule are stated.
- Nothing is deleted, moved, or edited by this document.

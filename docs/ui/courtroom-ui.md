# Courtroom UI (keon-control)

## Mental Model

**The Courtroom is the sole human governance authority.**

This UI exists for one purpose:
> To make accountable, auditable decisions and emit immutable proof.

The Courtroom:
- accepts or rejects gated actions
- requires human rationale
- binds decisions to policy lineage
- emits receipts and evidence packs

It cannot:
- execute workflows
- modify running systems
- replay or alter past decisions

---

## What the Courtroom Is Allowed to Do

- Display pending decision cases
- Enforce rationale requirements
- Accept or reject proposed actions
- Bind decisions to explicit policy versions
- Generate immutable decision receipts
- Render and export full evidence packs

---

## What the Courtroom Is Forbidden From Doing

- Initiating execution
- Modifying workflows
- Editing recorded decisions
- Bypassing kernel verification
- Acting without policy lineage

All decisions are final once recorded.

---

## Core Screens

### Decision Queue

![Decision Queue Screenshot](../assets/ui/courtroom-decisions.png)

**Caption**
> The decision queue represents governance demand, not system urgency.
> No execution proceeds until a human decision is formally recorded.

---

### Decision Case View

![Decision Case Screenshot](../assets/ui/courtroom-decision-case.png)

**Caption**
> Each case binds human intent to policy, context, and consequence.
> Rationale is mandatory because accountability is non-optional.

---

### Evidence Pack Viewer

![Evidence Pack Viewer Screenshot](../assets/ui/courtroom-evidence.png)

**Caption**
> Evidence is rendered only where authority exists.
> Receipts, seals, and artifacts are verifiable and immutable.

---

## Policy Binding Visibility

Each decision displays:
- Policy identifier
- Version
- Lineage hash
- Effective timestamp

If policy lineage cannot be verified, the UI fails closed.

**Rationale:**
A decision without policy context is an opinion — not governance.

---

## Architectural Invariant

> **keon-control decides and proves.
> It never executes.**

This UI encodes legal, operational, and ethical boundaries directly into the user experience.

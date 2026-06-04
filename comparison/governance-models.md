# Governance Models: Governed vs. Conventional

## A Comparative Framework (Without Brand Names)

> **Rebased under Operation CLEAN EDGE M5** onto current canon: the reasoning-to-action thesis, the five canonical surfaces, and the Three Proofs. Prior OMEGA / Workshop–Courtroom framing is superseded and has been removed. This page describes architecture, not vendors.

Most agent platforms are optimized for speed, flexibility, or developer convenience. Keon is optimized for **governing the reasoning-to-action chain** under real-world constraints — and for producing **audit-ready evidence for consequential action**.

This page outlines the structural differences — without naming vendors — so systems can be evaluated on architecture, not claims.

### Tag legend

- **`[S]` standards-backed** — anchored in the independent, complementary CAES / CPP standard (CAES Working Group). Referenced, not authored here.
- **`[P]` product-only** — a property of the Keon product. Real and falsifiable, but not a standards claim.

---

## 1. Where authority lives

### Conventional agent platforms

- Decisions are often made inside agents, orchestration logic, or application code.
- Human approval, if present, is usually embedded in the same surface, optional, or bypassable under failure.

**Result:** authority and execution are co-located, and nothing behind the boundary owns *why* an action was allowed.

### Governed systems (Keon)

- The **Runtime** owns authority, the policy decision, execution mechanics, and receipt emission. The **MCP Gateway** is the non-bypassable tool boundary that binds identity and routes Decide-before-Execute to the Runtime — it does not own the decision. `[P]`
- Execution systems cannot approve themselves.

**Result:** authority is explicit, isolated in the Runtime, and provable. `[P]`

---

## 2. Separation of surfaces

### Conventional agent platforms

- A single surface often lets users trigger execution, approve outcomes, and view results.
- The same operator may initiate, approve, and justify actions.

**Risk:** no structural barrier prevents self-approval.

### Governed systems

- Responsibility is split across five surfaces with no overlap: **Collective** (governs cognition before action), **Cortex** (preserves causal truth and reconstructable evidence), **MCP Gateway** (governs execution at the tool boundary), **Runtime** (owns authority, decision, execution, receipt), **Control** (operator/audit cockpit — observes and initiates, but never authorizes). `[P]`
- No surface can both act and authorize. Control acts *through* the boundary, never around it.

**Guarantee:** cognition, enforcement, authority, evidence, and the operator view are distinct. `[P]`

---

## 3. Failure modes

### Conventional agent platforms

- On failure, systems often retry silently, degrade to partial results, or continue without approval.
- Logs may exist, but outcomes still occur.

**Risk:** failure produces ungoverned behavior.

### Governed systems

- When Keon is degraded, an effect-bound action resolves to **Denied** or **RequiresHumanAuthorization** (suspend + escalate; timeout → Denied). **Receiptless execution is never permitted.** `[P]` Degraded-mode behavior is bounded by CAES. `[S]`
- Uncertainty resolves to denial. No silent degradation path exists.

**Guarantee:** the system fails closed, not forward. `[P]`

---

## 4. Evidence and proof — the Three Proofs

### Conventional agent platforms

- Evidence is often logs, traces, or best-effort audit data — mutable, incomplete, or environment-dependent.

**Outcome:** audits rely on reconstruction and narrative.

### Governed systems

A consequential action satisfies the thesis only when three independent proofs hold:

| Proof | Question answered | Backing |
| :--- | :--- | :--- |
| **Authority** | Was it allowed? | Decision Receipt · PolicyHash · fail-closed — CAES L1/L2, CPP `[S]` |
| **Causation** | What happened, why, and what else was considered? | Causal spine + Evidence Pack `[S]` (CAES L2 + Cortex); deliberation evidence `[P]` |
| **Viability** | What did it cost? Did it stay up? | Buyer-reproducible telemetry `[P]`; chaos/degraded attestation partial `[S]` (CAES L3) |

The mechanical substrate that makes this provable — signed receipts, PolicyHash binding, append-only records, fail-closed enforcement — is **necessary, but it is the floor, not the headline.** `[P]`

**Outcome:** audits rely on an offline-verifiable account, not explanation. `[P]`

---

## 5. Human-in-the-loop semantics

### Conventional agent platforms

- "Human-in-the-loop" often means optional approval steps, prompts inside execution tools, or configurable bypasses.

**Reality:** human involvement is advisory, not authoritative.

### Governed systems

- Human decisions are mandatory when policy requires, require rationale, and are permanently recorded. Reduced oversight depth must trigger compensating machine-side safeguards, never a weaker governance floor. `[P]`
- No execution path exists around the human when one is required.

**Reality:** human authority is enforceable, not symbolic. `[P]`

---

## 6. Policy binding

### Conventional agent platforms

- Policies are often config files or runtime checks, loosely coupled to outcomes; changes may not invalidate prior decisions.

**Risk:** policy drift without accountability.

### Governed systems

- Every decision is bound to a specific policy and version through a deterministic **PolicyHash**; historical decisions remain independently evaluable. `[S]` (PolicyHash determinism, CAES) / `[P]`

**Guarantee:** policy context is inseparable from outcomes. `[P]`

---

## 7. What this means in practice

| Requirement | Conventional platforms | Governed systems |
| --- | --- | --- |
| Govern reasoning *before* the call (alternatives considered) | Absent | Native (Collective + Cortex) `[P]` |
| Separation of authority from execution | Weak or absent | Structural (Runtime owns authority) `[P]` |
| Fail-closed behavior | Rare | Default `[P]` |
| Offline-verifiable evidence | Inconsistent | Guaranteed (Evidence Pack) `[S]` |
| Reproducible viability proof | Marketing numbers | Buyer-runnable harness `[P]` |
| Audit without reconstruction | Difficult | Native (causal spine) `[S]` |

---

## Why this is not gateway-only governance

A tool gateway is necessary, and Keon has one. But a gateway alone sits at the tool boundary: it can admit or block a call and bind identity. It cannot, alone, govern the reasoning before the call, own the authority decision and the policy in force, reconstruct causation from directive to outcome, preserve why one path was chosen over others, or prove viability reproducibly. Keon's edge is the **combination** of all five surfaces governing the whole chain and producing all three proofs.

`[P]` `[G-FALSIFY: Show a gateway-only architecture that produces, for a consequential action, an offline-verifiable account of authority and causation and the alternatives considered. If a gateway alone can do that, this comparison is wrong.]`

---

## Final perspective

Most agent platforms are designed to **do things**. Governed systems are designed to **answer for things** — across the whole chain from reasoning to action.

If your environment requires accountability, auditability, regulatory defensibility, and clear human authority, then architecture matters more than features.

> **Governance is not a plugin. It is a system property.**
> **Execution proposes. Governance decides. Receipts prove. Telemetry attests.**

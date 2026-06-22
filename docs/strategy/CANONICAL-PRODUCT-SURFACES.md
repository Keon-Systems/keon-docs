# CANONICAL PRODUCT SURFACES

> **Status:** Canonical. Authored under PR-M0 (Strategic Reset Canon).
> **Boundary:** Strategy only. Defines roles; changes no code, website, or standard.

## Tag conventions

- `[G-TAG:S]` standards-backed · `[G-TAG:P]` product-only · `[G-FALSIFY: …]` falsification method for headline claims.

## Purpose

One canonical role line per surface. No overlap. No two surfaces may claim the same job. If a reader cannot tell two surfaces apart from their role lines, this document has failed.

---

## The surfaces

### Keon Collective
**Governs cognition before action.**
The multi-agent reasoning layer where intents and justifications are formed and contested *before* anything crosses an Effect Boundary. The Collective is where governance of the reasoning-to-action chain begins.
`[G-TAG:P]` `[G-FALSIFY: An action reaches an Effect Boundary with no governed cognition step (no intent/justification) recorded upstream in the Collective.]`

### Keon Cortex
**Preserves causal truth and reconstructable evidence.**
The system of record for what happened, why, and — by default — what alternatives were considered. Cortex holds the causal spine and the Evidence Pack and is the default home for deliberation evidence.
`[G-TAG:P]` `[G-FALSIFY: A consequential action's evidence cannot be reconstructed from Cortex into a coherent causal account end-to-end.]`

### Keon MCP Gateway
**Governs execution at the tool boundary.**
The non-bypassable boundary where a proposed action meets a tool. The Gateway enforces boundary admission, tenant/actor identity binding, scope checks, and non-bypassability, and may route to the Runtime for Decide/Execute. It does not own policy evaluation, authorization, execution mechanics, or receipt emission — the Runtime owns authority, the policy decision, execution mechanics, and receipt emission.
`[G-TAG:P]` `[G-FALSIFY: A tool invocation with Effect-Boundary impact executes without passing through both Gateway enforcement and a Runtime decision.]`

### Keon Runtime
**Owns authority, decision, execution, and receipt mechanics.**
The mechanism that binds authority to a decision, drives execution, and emits the signed receipt. Runtime is the engine; Gateway is its boundary, Cortex is its memory, Collective is its upstream.
`[G-TAG:P]` `[G-FALSIFY: A decision executes without a Runtime-emitted, verifiable receipt bound to the authorizing policy.]`

### Keon Control
**The operator, tenant, audit, and evidence cockpit.**
The human-facing surface for operators and auditors: tenant administration, policy operation, audit, and evidence retrieval. Control is where humans see and act; it does not itself authorize actions.
`[G-TAG:P]` `[G-FALSIFY: An operator or auditor cannot retrieve the authority, causation, and viability evidence for a given action from Control.]`

### CAES / CPP
**An independent, complementary standard.**
The standard against which Keon's authority and causation claims are measured. CAES maps to external frameworks; it is **not** a subordinate profile of them. Keon is one implementation; the standard stands on its own.
`[G-TAG:S]` `[G-FALSIFY: A CAES conformance claim cannot be checked against the published CAES criteria independently of Keon's product.]`

---

## No-overlap matrix

| Surface | Owns | Does NOT own |
|---|---|---|
| Collective | Cognition before action | Execution, enforcement, receipts |
| Cortex | Causal truth, evidence, deliberation home | Authorization, enforcement |
| MCP Gateway | Execution-boundary enforcement | Reasoning, evidence storage |
| Runtime | Authority→decision→execution→receipt | Reasoning, human UI |
| Control | Operator/tenant/audit/evidence UI | Authorizing actions |
| CAES / CPP | The standard | Any single product's implementation |

`[G-TAG:P]` `[G-FALSIFY: Any two rows can be shown to share an "Owns" entry, or a surface is shown performing a job listed under its "Does NOT own."]`

## Note for the clean sweep

These role lines are the **keep-criteria anchor** for `DOCUMENTATION-CLEAN-SWEEP-POLICY.md`: a surviving doc must map cleanly to exactly one surface's role (or to a cross-surface process in `E2E-PROCESS-CANDIDATES.md`).

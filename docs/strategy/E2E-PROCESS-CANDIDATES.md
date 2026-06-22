# E2E PROCESS CANDIDATES

> **Status:** Canonical candidate list. Authored under PR-M0 (Strategic Reset Canon).
> **Boundary:** Candidates only — one-line purpose each. **Not** full E2E specifications.
> **Execution:** Milestone M4 will formalize these into E2E specs in a later PR, after G0.

## Tag conventions

- `[G-TAG:S]` standards-backed · `[G-TAG:P]` product-only · `[G-FALSIFY: …]` for headline claims.

## Purpose

Name the end-to-end processes that cut across product surfaces and that M4 will later specify. This is a list, not a design. Each line states the candidate's purpose only; scope, steps, contracts, and acceptance criteria are deferred to M4.

---

## Candidates

1. **Autonomous Action Governance** — Govern a single autonomous action from intent through authorized execution to signed receipt.
2. **Collective Reasoning-to-Execution** — Carry multi-agent cognition from contested reasoning in the Collective to a governed execution.
3. **Audit Evidence Reconstruction** — Reconstruct the full causal account of a consequential action from Cortex for an auditor.
4. **MCP Tool Governance** — Enforce policy at the tool boundary for every Effect-Boundary-crossing tool invocation.
5. **Tenant Sandbox Activation** — Stand up and bind a governed tenant sandbox with its policy and evidence scope.
6. **Failure / Degraded Mode (first-tier)** — Resolve degraded operation to `Denied` or `RequiresHumanAuthorization` / suspend-escalate, never to receiptless execution.
7. **Public Telemetry Proof** — Produce buyer-reproducible telemetry evidence of operational viability per the telemetry posture.

`[G-TAG:P]` `[G-FALSIFY: A consequential, cross-surface process that Keon performs is not representable as one of these seven candidates (or a composition of them). If such a process exists, the candidate list is incomplete and must be revised before M4 formalizes it.]`

## Constraints carried into M4 (for reference, not specification)

- Candidate 6 is bound by the locked premise: degraded mode never permits receiptless execution.
- Candidate 4 is bound to the CAES Effect Boundary taxonomy for what counts as consequential. `[G-TAG:S]`
- Candidate 7 inherits the NO FAKE NUMBERS rule and buyer-reproducibility unit of proof.

## Definition of done (for this candidate list)

- Exactly the seven candidates above, each with a one-line purpose.
- No full specs, steps, or contracts (those belong to M4).
- Candidates traceable to product surfaces and locked premises.

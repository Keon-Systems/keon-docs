# MCP TOOL GOVERNANCE
### Canonical E2E Process — Candidate 4

> **Status:** Canonical E2E process definition. Authored under Operation CLEAN EDGE **M4**.
> **Boundary:** Strategy/governance canon. Defines a process; changes no code, website, standard, or product behavior.
> **Process candidate:** E2E candidate 4 — *MCP Tool Governance* (`E2E-PROCESS-CANDIDATES.md`).
> **Primary surfaces:** MCP Gateway (enforcement boundary) · Runtime (decision/receipt) · Cortex (evidence).

## Tag legend (G-TAG)

- **`[S]` standards-backed** — anchored in canonical CAES / CPP (CAES Working Group). Referenced, not authored here.
- **`[P]` product-only** — a Keon product property; real and falsifiable, not a standards claim.
- **`[G-FALSIFY: …]`** — the concrete test that would prove a headline claim false.

---

## Purpose

Enforce policy at the **tool boundary** for every **Effect-Boundary-crossing tool invocation**: bind tenant/actor identity and scope, guarantee non-bypassability, and route to the Runtime for the authority decision and receipt. This process isolates the **Gateway's** job and draws the load-bearing line between the Gateway and the Runtime that gateway-only systems blur. `[P]`

**What counts as consequential here is bound to the CAES Effect Boundary taxonomy** — a tool invocation is in scope when it crosses `ExternalSideEffect`, `HumanFacingOutput`, `GovernanceRelevantState`, `SafetyCriticalActuation`, or `WorkflowTransition`. `[S]`

---

## Trigger

A proposing agent (BYOAI model or Full-Keon Collective) invokes a tool whose effect would cross an Effect Boundary, reaching the MCP Gateway. `[S]` (taxonomy) / `[P]`

---

## Actors

| Actor | Role |
| :--- | :--- |
| **Proposing agent** | Requests the tool invocation. Holds no authority. `[P]` |
| **MCP Gateway** | **Owns:** boundary admission, tenant/actor identity binding, scope checks, non-bypassability; may route to Runtime for Decide/Execute. **Does NOT own:** policy evaluation, authorization, execution mechanics, receipt emission. `[P]` |
| **Runtime** | **Owns:** authority, the policy decision, execution mechanics, receipt emission. `[P]` |
| **Cortex** | Records spine + Evidence Pack for the invocation. `[P]` |
| **CAES / CPP** | Standard for the Authority proof and the Effect Boundary taxonomy. `[S]` |

> **The Gateway/Runtime separation is load-bearing (B1-corrected).** The Gateway admits the call and binds identity; the Runtime decides authority and emits the receipt. Conflating them is how gateway-only systems end up unable to prove *why* an invocation was allowed. `[P]`

---

## Preconditions

1. The tenant/actor is bound to a policy scope (candidate 5). `[P]`
2. A policy is in force and its PolicyHash is computable at decision time. `[S]`
3. The Gateway has a single, non-bypassable path to reality for this tenant — no side channel exists. `[P]`
4. The receipt sink and spine store are available, or the invocation resolves under the degraded-mode law (candidate 6). `[P]` / partial `[S]`

---

## Main flow

1. **Boundary admission (Gateway).** The Gateway intercepts the invocation, classifies the Effect Boundary, and confirms the call may be considered. `[S]` (taxonomy) / `[P]`
2. **Identity + scope binding (Gateway).** Tenant and actor identity are bound and immutable for the request; scope is checked against the bound policy scope. Missing/malformed/mismatched tenant identity fails immediately. `[P]` / `[S]` (tenant isolation)
3. **Non-bypassability assertion (Gateway).** The Gateway guarantees there is no path to the tool except through it. `[P]`
4. **Route to decide (Gateway → Runtime).** The Gateway hands the bound request to the Runtime. It does **not** evaluate policy or mint a receipt. `[P]`
5. **Authority decision (Runtime).** The Runtime evaluates policy, yields one disposition (`Approve` · `Rewrite` · `Block` · `RequiresHuman`), binds the PolicyHash, mints and ARO-verifies the receipt, and appends the spine — the candidate-1 mechanics. `[S]` / `[P]`
6. **Execute at the boundary (Runtime via Gateway).** On a proceed disposition, the Runtime drives execution mechanics; the effect crosses the Gateway boundary and nowhere else. `[P]`
7. **Evidence (Cortex).** Spine entry and Evidence Pack are materialized; the outcome is bound 1:1 to the action. `[S]`

> `[G-FALSIFY: An Effect-Boundary tool invocation executes without passing through BOTH Gateway enforcement AND a Runtime decision. If that path exists, non-bypassability is broken and this process is false.]`

---

## Failure flow

- **Scope/identity violation.** Tenant/actor mismatch or out-of-scope tool → denial receipt, spine-appended; never silent access (FP-08; Scenario 06). `[S]`
- **Block / RequiresHuman.** Decision returns Block (denial receipt) or RequiresHuman (suspend + escalate). `[S]` / `[P]`
- **Bypass attempt.** Any attempt to reach the tool outside the Gateway fails closed — governance is substrate-level, not API-level (ADDENDUM 07/14). `[S]` / `[P]`
- **Receipt/PolicyHash/spine failures.** Fail closed exactly as candidate 1 (Scenarios 02/04/08). `[S]`
- **Degradation/timeout.** Resolves to `Denied` or `RequiresHumanAuthorization`; timeout → `Denied`; never receiptless (candidate 6). `[S]` / `[P]`

---

## Required receipts / evidence

| Artifact | Provenance | Tag |
| :--- | :--- | :--- |
| Signed Decision Receipt (PolicyHash-bound) | Runtime → ARO | `[S]` |
| Denial Receipt (scope/identity/Block) | Runtime → ARO | `[S]` |
| Tenant/actor binding record | Gateway (carried into request) | `[P]` / `[S]` (isolation) |
| Causal spine entry + Evidence Pack | Cortex | `[S]` |

---

## Required telemetry

Definitions/reproduction only (candidate 7). This process is the primary source for: **gateway latency** (p50/p95/p99, reported as overhead vs. an ungoverned baseline), **tenant/actor binding-failure rate**, and **policy-eval error rate** (errors fail closed). `[P]` Decision/persistence latency and dispositions are shared with candidate 1. `[P]`

---

## Security invariants

1. The Gateway is the **sole lawful boundary** between cognition and external consequence, and it is non-bypassable. `[P]`
2. The Gateway never evaluates policy, authorizes, executes, or emits receipts — those are Runtime-owned. `[P]`
3. Every Effect-Boundary crossing traverses **both** Gateway enforcement and a Runtime decision. `[P]`
4. Tenant isolation is enforced at the boundary; cross-tenant access fails closed regardless of provider behavior (ADDENDUM 14). `[S]`
5. No actor controls whether governance is invoked — the platform decides. `[S]` / `[P]`
6. Governance invariants are identical across in-process, A2A, MCP, and remote-agent surfaces (ADDENDUM 07). `[S]` / `[P]`

---

## Audit questions answered

- **Did this tool call pass the boundary lawfully?** → Gateway admission + identity/scope binding record. `[P]`
- **Was it authorized, and under which policy?** → Runtime decision + PolicyHash-bound receipt. `[S]`
- **Could it have reached the tool any other way?** → No; non-bypassability assertion + bypass-attempt test. `[P]`
- **Was tenant isolation preserved?** → Binding record + denial-on-mismatch evidence. `[S]`

---

## Test obligations

1. Attempt to reach the tool outside the Gateway; assert fail-closed (ADDENDUM 14). `[S]` / `[P]`
2. Assert no execution path exists without **both** Gateway enforcement and a Runtime decision (non-bypassability). `[P]`
3. Submit a cross-tenant invocation; assert denial receipt and no access (FP-08; Scenario 06). `[S]`
4. Assert the Gateway emits no receipt and runs no policy evaluation (separation of duties). `[P]`
5. Exercise each Effect Boundary class and assert correct in-scope classification. `[S]`
6. Force a degraded sink and assert deny/suspend, never receiptless execution (candidate 6). `[S]` / `[P]`

---

## Out of scope

- Authority decision mechanics and receipt internals → candidate 1 (referenced, not redefined here).
- Multi-agent deliberation upstream of the call → candidate 2.
- Auditor reconstruction → candidate 3.
- Tenant/sandbox provisioning → candidate 5.
- Degraded/timeout resolution detail → candidate 6.
- Telemetry metric model + harness → candidate 7.
- Any CAES/CPP normative authoring, whitepaper, website, or product code.

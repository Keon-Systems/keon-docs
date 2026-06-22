# TENANT SANDBOX ACTIVATION
### Canonical E2E Process — Candidate 5

> **Status:** Canonical E2E process definition. Authored under Operation CLEAN EDGE **M4**.
> **Boundary:** Strategy/governance canon. Defines a process; changes no code, website, standard, or product behavior.
> **Process candidate:** E2E candidate 5 — *Tenant Sandbox Activation* (`E2E-PROCESS-CANDIDATES.md`).
> **Primary surfaces:** Control (tenant administration) · Runtime (authority/policy binding) · Cortex (tenant-isolated memory + evidence scope) · MCP Gateway (boundary scope).

## Tag legend (G-TAG)

- **`[S]` standards-backed** — anchored in canonical CAES / CPP (CAES Working Group). Referenced, not authored here.
- **`[P]` product-only** — a Keon product property; real and falsifiable, not a standards claim.
- **`[G-FALSIFY: …]`** — the concrete test that would prove a headline claim false.

---

## Purpose

Stand up and bind a governed **tenant sandbox** with its **policy scope** and **evidence scope** so that every subsequent action (candidates 1–4) is isolated to that tenant and reconstructable within it (candidate 3). This is the provisioning precondition the other processes assume: an action can only be governed if there is a tenant, an actor identity, a policy in force, and a tenant-scoped place for receipts, spine, and Evidence Packs to live. `[P]`

> Tenant isolation is not a configuration convenience — it is a security invariant enforced at the data layer and the boundary, regardless of provider behavior. `[S]`

---

## Trigger

An operator provisions (or re-binds) a tenant through **Control** — a new tenant, a new actor within a tenant, or a policy/scope change for an existing tenant. `[P]`

---

## Actors

| Actor | Role |
| :--- | :--- |
| **Operator** | Initiates provisioning through Control; defines policy intent. Does not author standards or authorize actions directly. `[P]` |
| **Control** | Tenant/operator administration cockpit; the human-facing surface for provisioning and scope. `[P]` |
| **Runtime** | Binds authority and the policy in force to the tenant; owns the decision path that downstream actions use. `[P]` |
| **Cortex** | Establishes tenant-isolated memory, causal-spine partition, and Evidence-Pack scope (two-gate tenant enforcement). `[P]` |
| **MCP Gateway** | Receives the tenant/actor scope it will bind and check at the boundary. `[P]` |
| **CAES / CPP** | Standard against which tenant-boundary and authority claims are measured. `[S]` |

---

## Preconditions

1. The operator is authorized to provision the tenant (Control-side authorization). `[P]`
2. A policy intent exists to bind; the resulting policy will be hashable (PolicyHash) at decision time. `[S]`
3. Storage for receipts, spine, and Evidence Packs is available and integrity-checkable, with tenant-scoped partitioning `(tenant_id, spine_id, actor_id)`. `[P]`
4. No cross-tenant default path exists — isolation is default-deny. `[S]`

---

## Main flow

1. **Define tenant + actors (Control).** Establish `tenant_id` and actor identities; capture the policy intent. `[P]`
2. **Bind policy scope (Runtime).** The policy in force is bound to the tenant; its PolicyHash will be computed deterministically at each decision. `[S]`
3. **Bind boundary scope (Gateway).** The Gateway is configured to bind and check this tenant/actor identity and scope on every Effect-Boundary invocation; non-bypassability is asserted for the tenant. `[P]`
4. **Establish evidence scope (Cortex).** Create the tenant-isolated memory substrate, the partition-scoped causal-spine sequence, and the Evidence-Pack scope. Two-gate tenant enforcement is active at the data layer. `[P]`
5. **Activation receipt.** Provisioning that changes governance-relevant state is itself an action crossing `GovernanceRelevantState`: it is decided by the Runtime, receipted, ARO-verified, and spine-appended. `[S]`
6. **Ready.** The sandbox is active: candidates 1–4 may now run within it, and candidate 3 can reconstruct within its evidence scope. `[P]`

> `[G-FALSIFY: After activation, an action attributed to this tenant reads or writes another tenant's memory, receipts, or Evidence Packs. One cross-tenant leak falsifies the activation's isolation guarantee.]`

---

## Failure flow

- **Cross-tenant binding attempt.** Any attempt to bind an actor or scope across tenant boundaries fails closed; the violation is recorded as a denial (FP-08; Scenario 06). `[S]`
- **Missing/malformed tenant id.** Immediate failure — no partial provisioning, no default-allow fallback. `[S]`
- **Policy not hashable / not in force.** Activation cannot complete; downstream decisions would fail closed, so activation fails closed first. `[S]`
- **Evidence scope unavailable.** Activation does not report ready; provisioning that touches governance-relevant state without a durable receipt is not permitted (degraded-mode law, candidate 6). `[S]` / `[P]`
- **Provisioning-receipt failure.** The provisioning action aborts like any other action whose receipt cannot be verified (Scenario 02). `[S]`

---

## Required receipts / evidence

| Artifact | Provenance | Tag |
| :--- | :--- | :--- |
| Activation/provisioning Decision Receipt (`GovernanceRelevantState`) | Runtime → ARO | `[S]` |
| Denial Receipt (cross-tenant / malformed id) | Runtime → ARO | `[S]` |
| Tenant policy-scope binding record | Runtime | `[P]` / `[S]` (PolicyHash) |
| Tenant evidence-scope + partition record | Cortex | `[P]` |

---

## Required telemetry

Definitions/reproduction only (candidate 7). This process exposes: **tenant/actor binding-failure rate** at provisioning, and provisioning **decision/persistence latency** as product telemetry. `[P]` It establishes the per-tenant measurement windows and denominators later telemetry will report against. `[P]`

---

## Security invariants

1. Tenant isolation is enforced at the data layer (two-gate) and the boundary; no cross-tenant memory access regardless of provider behavior. `[S]`
2. Provisioning that changes governance-relevant state is itself governed — receipted, ARO-verified, spine-appended. `[S]`
3. No memory write without a canonical, platform-assigned id; the actor never assigns spine join keys. `[S]`
4. Isolation is default-deny; absence of an explicit cross-tenant grant means no access. `[S]`
5. The operator defines policy intent; Keon enforces it mechanically — Keon does not determine policy morality (FP-10). `[S]` / `[P]`

---

## Audit questions answered

- **Who provisioned this tenant, under what authority, and when?** → Activation receipt + spine entry. `[S]`
- **What policy and scope are bound to this tenant?** → Policy-scope binding record + PolicyHash. `[S]`
- **Is this tenant's evidence isolated from every other tenant's?** → Evidence-scope/partition record + cross-tenant denial tests. `[S]`
- **Can this tenant's actions be reconstructed within its own scope?** → Yes, via candidate 3 over the tenant partition. `[P]`

---

## Test obligations

1. Provision a tenant, then attempt cross-tenant read/write; assert denial and no access (FP-08; Scenario 06). `[S]`
2. Submit a malformed/missing `tenant_id`; assert immediate fail-closed. `[S]`
3. Assert the activation itself produced a verified, spine-appended receipt for `GovernanceRelevantState`. `[S]`
4. Assert spine partitioning is tenant-scoped and platform-assigned (not actor-assigned). `[S]`
5. Verify two tenants' Evidence Packs are non-overlapping and independently reconstructable. `[P]`
6. Remove evidence-scope availability and assert activation fails closed (no receiptless provisioning). `[S]` / `[P]`

---

## Out of scope

- Per-action governance once the sandbox is live → candidate 1.
- Multi-agent deliberation within the tenant → candidate 2.
- Auditor reconstruction → candidate 3.
- Tool-boundary admission specifics → candidate 4.
- Degraded/timeout resolution detail → candidate 6.
- Telemetry metric model + harness → candidate 7.
- Any CAES/CPP normative authoring, whitepaper, website, or product code.

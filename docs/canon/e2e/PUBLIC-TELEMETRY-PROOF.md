# PUBLIC TELEMETRY PROOF
### Canonical E2E Process — Candidate 7

> **Status:** Canonical E2E process definition. Authored under Operation CLEAN EDGE **M4**.
> **Boundary:** Strategy/governance canon. **Metric definitions and reproduction method ONLY. NO NUMBERS appear in this document — by design.** Changes no code, dashboards, website, standard, or product behavior.
> **Process candidate:** E2E candidate 7 — *Public Telemetry Proof* (`E2E-PROCESS-CANDIDATES.md`).
> **Primary surfaces:** Runtime · MCP Gateway · Cortex (metric sources) · Control (retrieval) · CAES/CPP (partial chaos anchor).

## Tag legend (G-TAG)

- **`[S]` standards-backed** — anchored in canonical CAES / CPP (CAES Working Group). Referenced, not authored here.
- **`[P]` product-only** — a Keon product property; real and falsifiable, not a standards claim.
- **`[G-FALSIFY: …]`** — the concrete test that would prove a headline claim false.

---

## Purpose

Produce **buyer-reproducible** telemetry evidence of operational viability — the third proof. The unit of proof is **buyer reproducibility**: Keon publishes metric **definitions**, the measurement **methodology**, and a verification **harness** so a buyer can *measure Keon's overhead themselves*. Keon does **not** publish hero numbers and ask for trust. `[P]`

> **Absolute rule: NO FAKE NUMBERS.** No invented benchmarks, no illustrative-but-unlabeled figures, no aspirational latencies presented as measured. This canon document contains zero numbers; downstream artifacts publish only measured values with methodology and a buyer-runnable verification path attached. `[P]`

---

## Trigger

A buyer, auditor, or operator requests the viability account for a Keon deployment — or the publication discipline is exercised to produce/refresh a measured telemetry claim with its reproduction method. `[P]`

---

## Actors

| Actor | Role |
| :--- | :--- |
| **Buyer / auditor** | Runs the published harness against a deployment to reproduce the metric families within stated tolerance. `[P]` |
| **Runtime** | Source of decision/persistence latency, dispositions, degraded-mode counts, availability. `[P]` |
| **MCP Gateway** | Source of gateway latency (overhead vs. baseline) and availability. `[P]` |
| **Cortex** | Source of evidence-pack generation time and sink health. `[P]` |
| **Control** | Retrieval surface for the published metric definitions and harness. `[P]` |
| **CAES / CPP** | Partial anchor for chaos/degraded-mode attestation (L3). `[S]` (partial) |

---

## Preconditions

1. Every published value has a **measured source**, a **methodology reference**, and a **buyer-runnable verification path**. `[P]`
2. Overhead metrics are defined **against a stated ungoverned baseline**, not in isolation. `[P]`
3. Each value states its **measurement window** and **denominator/scope** (per-tenant windows established in candidate 5). `[P]`
4. Absence of a measurement is reportable as "not yet measured," **never estimated**. `[P]`

---

## Main flow

1. **Define families (not values).** The metric families below are defined by name, meaning, and source. No values appear in canon. `[P]`
2. **Attach methodology + harness.** Each published value carries its measurement methodology and a verification path the buyer can run. `[P]`
3. **State baseline + window + denominator.** Overhead is reported vs. a stated ungoverned baseline; every value carries its window and scope. `[P]`
4. **Tag provenance.** Latency/availability/rate families are product-only `[P]`; chaos/degraded-mode attestation is partially standards-backed `[S]` (CAES L3 ChaosTestAttestation) — and only that portion. `[S]` (partial) / `[P]`
5. **Publish reproducibly.** The buyer reproduces the family within stated tolerance using the harness. `[P]`

### Metric families (name · meaning · source · publication rule)

| Family | Meaning | Source | Publication rule | Tag |
| :--- | :--- | :--- | :--- | :--- |
| **Decision latency** (p50/p95/p99) | Time to render authorize/deny/escalate | Runtime decision path | Measured only; methodology + harness attached | `[P]` |
| **Gateway latency** (p50/p95/p99) | Enforcement overhead at the tool boundary | MCP Gateway | Measured only; reported vs. ungoverned baseline | `[P]` |
| **Receipt-persistence latency** (p50/p95/p99) | Time to durably persist a signed receipt | Runtime → receipt sink | Measured only; tied to sink-health | `[P]` |
| **Evidence-pack generation time** | Time to assemble a reconstructable Evidence Pack | Cortex | Measured only; report input scope | `[P]` |
| **Fail-closed / deny / approve rates** | Disposition distribution | Runtime | Measured only; report denominator + window | `[P]` |
| **Degraded-mode count** | Entries into degraded mode | Runtime | Measured only; pairs with resolution disposition | `[P]` |
| **Runtime availability** | Uptime of the decision/execution engine | Runtime | Measured only; state window | `[P]` |
| **Gateway availability** | Uptime of the enforcement boundary | MCP Gateway | Measured only; state window | `[P]` |
| **Receipt-sink health** | Availability/integrity of the receipt store | Receipt sink | Measured only; integrity-check method stated | `[P]` |
| **Policy-eval error rate** | Rate of policy evaluation errors | Runtime / Gateway | Measured only; errors fail closed, not open | `[P]` |
| **Tenant/actor binding-failure rate** | Rate of binding failures | Runtime | Measured only; binding failure = deny | `[P]` |

> `[G-FALSIFY: Run the published harness against a Keon deployment and obtain overhead materially worse than the methodology implies, or find a public Keon number you cannot reproduce within stated tolerance, or find a harness that is not actually buyer-runnable. Any one falsifies the viability posture.]`

---

## Failure flow

- **Unreproducible value.** A published value a buyer cannot reproduce within tolerance is a posture failure and must be withdrawn/corrected, not defended. `[P]`
- **Mis-tag.** Any non-chaos metric implying CAES backing, or a chaos-attestation claim not mappable to a specific CAES L3 criterion, is a G-TAG violation. `[S]` (boundary) / `[P]`
- **Estimated value.** A value published without a reproducible measured source violates the NO FAKE NUMBERS rule. `[P]`
- **Missing methodology/harness.** A value without an attached, runnable verification path is not publishable. `[P]`

---

## Required receipts / evidence

| Artifact | Provenance | Tag |
| :--- | :--- | :--- |
| Metric definition set (names, meanings, sources, rules) | This canon (definitions only) | `[P]` |
| Measurement methodology reference (per published value) | Published telemetry artifact | `[P]` |
| Buyer-runnable verification harness | Published telemetry artifact | `[P]` |
| Chaos / degraded-mode attestation (partial) | CAES L3 ChaosTestAttestation | `[S]` (partial) |

---

## Required telemetry

This **is** the telemetry process. It defines the metric model the other six processes emit into; it produces no Effect-Boundary action itself (read-only measurement + publication). `[P]`

---

## Security invariants

1. No value is published without a reproducible measured source. `[P]`
2. NO FAKE NUMBERS — no invented, illustrative-but-unlabeled, or aspirational figures presented as measured. `[P]`
3. Standards-backed vs product-only is tagged on **every** published claim; only the chaos-attestation portion may be `[S]`. `[S]` (boundary) / `[P]`
4. Overhead is always reported against a stated ungoverned baseline, with window and denominator. `[P]`
5. Measuring telemetry never weakens governance: telemetry is read-only over operational behavior and crosses no Effect Boundary. `[P]`

---

## Audit questions answered

- **What did governance cost?** → Latency families (decision/gateway/persistence), measured, vs. baseline. `[P]`
- **Did the substrate stay up under load?** → Availability + sink-health families. `[P]`
- **Are constraints active, not theoretical?** → Fail-closed/deny/approve rates and binding-failure rate. `[P]`
- **How does it behave under failure?** → Degraded-mode count + resolution disposition; chaos attestation partial. `[P]` / `[S]` (partial)
- **Can the buyer verify all of this without trusting us?** → Yes — methodology + harness, reproduced in their environment. `[P]`

---

## Test obligations

1. Run the published harness against a deployment and reproduce each family within stated tolerance. `[P]`
2. Assert every published value carries a methodology reference and a buyer-runnable verification path. `[P]`
3. Assert overhead values reference a stated ungoverned baseline and carry window + denominator. `[P]`
4. Assert no value lacks a measured source ("not yet measured" used instead of estimates). `[P]`
5. Map every `[S]`-tagged telemetry claim to a specific CAES L3 ChaosTestAttestation criterion; assert no non-chaos metric is `[S]`. `[S]` (boundary) / `[P]`
6. Assert this canon document contains zero numbers. `[P]`

---

## Out of scope

- Any benchmark **number** (forbidden in canon; published only with methodology downstream).
- Per-action governance mechanics → candidate 1.
- Multi-agent deliberation → candidate 2.
- Auditor reconstruction → candidate 3.
- Tool-boundary admission → candidate 4.
- Tenant/sandbox provisioning → candidate 5.
- Degraded/timeout resolution detail → candidate 6 (this process counts and attests it; it does not define the resolution law).
- Any CAES/CPP normative authoring, whitepaper, website, dashboards, or product code.

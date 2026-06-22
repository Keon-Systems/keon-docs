# TELEMETRY — PUBLIC PROOF POSTURE

> **Status:** Canonical posture. Authored under PR-M0 (Strategic Reset Canon).
> **Boundary:** Defines metric families and publication rules ONLY. **No numbers appear in this document.** No code, dashboards, or website.

## Tag conventions

- `[G-TAG:S]` standards-backed · `[G-TAG:P]` product-only · `[G-FALSIFY: …]` for headline claims.

## Posture statement

Telemetry is **first-class public proof of operational viability**, not marketing. The unit of proof is **buyer reproducibility**: we publish the metric definitions, the measurement methodology, and a verification harness so a buyer can **measure our overhead themselves**. We do not publish hero numbers and ask for trust.

`[G-TAG:P]` `[G-FALSIFY: A buyer follows the published methodology and harness against a Keon deployment and cannot reproduce the metric family within the stated tolerance, or the harness is not actually runnable by the buyer.]`

**Absolute rule: NO FAKE NUMBERS.** No invented benchmarks, no illustrative-but-unlabeled figures, no aspirational latencies presented as measured. This document contains zero numbers by design; downstream artifacts publish only measured values with methodology attached.

---

## Metric families (names · meaning · source · publication rule)

Each family below defines *what* is measured. **No values are stated here.** Publication of any value requires: a measured source, the methodology reference, and a buyer-runnable verification path.

| Family | Meaning | Source | Publication rule |
|---|---|---|---|
| **Decision latency** (p50/p95/p99) | Time to render an authorize/deny/escalate decision | Runtime decision path | Measured only; methodology + harness attached |
| **Gateway latency** (p50/p95/p99) | Time added at the tool boundary by enforcement | MCP Gateway | Measured only; reported as overhead vs. ungoverned baseline |
| **Receipt-persistence latency** (p50/p95/p99) | Time to durably persist a signed receipt | Runtime → receipt sink | Measured only; tied to sink-health family |
| **Evidence-pack generation time** | Time to assemble a reconstructable Evidence Pack | Cortex | Measured only; report input scope (action count/complexity) |
| **Fail-closed / deny / approve rates** | Disposition distribution of governed decisions | Runtime | Measured only; report denominator and window |
| **Degraded-mode count** | Count of entries into degraded mode | Runtime | Measured only; pairs with resolution disposition |
| **Runtime availability** | Uptime of the decision/execution engine | Runtime | Measured only; state measurement window |
| **Gateway availability** | Uptime of the enforcement boundary | MCP Gateway | Measured only; state measurement window |
| **Receipt-sink health** | Availability/integrity of the receipt store | Receipt sink | Measured only; integrity check method stated |
| **Policy-eval error rate** | Rate of policy evaluation errors | Runtime/Gateway | Measured only; errors fail closed, not open |
| **Tenant/actor binding-failure rate** | Rate of failures to bind action to authorized tenant/actor | Runtime | Measured only; binding failure = deny |

`[G-TAG:P]` for the metric mechanics above (these are product behaviors).

---

## Standards anchor (partial)

The viability proof anchors to **CAES L3 ChaosTestAttestation** for the chaos/degraded-behavior portion. This anchor is **partial**: only the chaos-attestation claims are standards-backed. Latency, availability, and rate families are **product-only** unless and until a standard covers them.

`[G-TAG:S]` (CAES L3 ChaosTestAttestation portion only)
`[G-TAG:P]` (all latency / availability / rate families)
`[G-FALSIFY: A published telemetry claim is tagged standards-backed but cannot be mapped to a specific CAES L3 ChaosTestAttestation criterion. Conversely, any non-chaos metric implying CAES backing is mis-tagged.]`

## Publication discipline (rules, not numbers)

1. Every published value carries its **methodology reference** and a **buyer-runnable verification path**.
2. Overhead metrics are reported **against a stated ungoverned baseline**, not in isolation.
3. Every value states its **measurement window** and **denominator/scope**.
4. No value is published without a reproducible source. Absence of a measurement is reported as "not yet measured," never estimated.
5. Standards-backed vs product-only is tagged on **every** published claim (G-TAG).

## Definition of done (for this posture doc)

- Metric families defined by name, meaning, source, and publication rule — no values present.
- Buyer-reproducibility framed as the unit of proof (methodology + harness).
- CAES L3 ChaosTestAttestation named as a **partial** anchor with explicit tagging.
- NO FAKE NUMBERS rule stated and self-enforced (this doc contains none).

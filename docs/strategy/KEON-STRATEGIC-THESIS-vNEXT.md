# KEON STRATEGIC THESIS — vNEXT

> **Status:** Canonical. Authored under PR-M0 (Strategic Reset Canon).
> **Controlling canon:** CLEAN EDGE Implementation Plan v0.2.
> **Boundary:** Strategy only. No code, no website, no whitepaper edits, no deletions.

## Tag conventions (apply throughout the strategy canon)

- `[G-TAG:S]` — **standards-backed** claim. Anchored in CAES/CPP or a named external standard.
- `[G-TAG:P]` — **product-only** claim. A property of the Keon implementation; not a standards claim.
- `[G-FALSIFY: …]` — for every **headline** claim: the method by which a skeptic catches us if the claim is false.

A claim may never imply standards backing it does not have. When in doubt, tag `P`.

---

## The thesis (canonical — stated verbatim)

> **Keon governs the reasoning-to-action chain for autonomous AI systems and produces audit-ready evidence for consequential action.**

This sentence is the headline. Everything below either supports it or scopes it.

`[G-TAG:P]` `[G-FALSIFY: A skeptic is handed a consequential action that occurred without a corresponding governed decision and signed receipt in the evidence record. If one exists, the thesis is false in practice.]`

---

## Emphasis correction (the reset)

The prior emphasis led with the primitives: pre-execution authorization, signed receipts, policy checks, fail-closed. Those remain true and load-bearing — but they are **substrate**, not the headline.

| Concept | Prior framing | Canonical framing |
|---|---|---|
| Pre-execution authorization | Headline feature | Substrate |
| Signed receipts | Headline feature | Substrate |
| Policy checks (PolicyHash) | Headline feature | Substrate |
| Fail-closed governance | Headline feature | Substrate |
| **Reasoning-to-action chain governance** | Implied | **Headline** |
| **Audit-ready evidence for consequential action** | Implied | **Headline** |

The substrate is *how* Keon keeps the headline promise. The buyer buys the headline. The auditor verifies the substrate.

`[G-TAG:P]` `[G-FALSIFY: If the substrate primitives can be removed without breaking the headline guarantee, the framing is wrong and they were never substrate. Removing receipt binding must break audit-readiness; removing fail-closed must break governance-before-action.]`

---

## What Keon IS

- A governance layer over the **full chain from reasoning to action** in autonomous AI systems — not a guardrail bolted onto a single tool call.
- A producer of **audit-ready evidence** for any **consequential action**, where "consequential" is bound to the CAES Effect Boundary taxonomy. `[G-TAG:S]`
- **Fail-closed by construction**: degraded mode resolves to `Denied` or `RequiresHumanAuthorization` / suspend-escalate. Receiptless execution is never permitted. `[G-TAG:P]`
- A system whose claims are **buyer-verifiable**, not asserted — every headline claim ships with its falsification method.

## What Keon IS NOT

- **Not** a model, an agent framework, or a replacement for the reasoning engine. Keon governs reasoning-to-action; it does not do the reasoning.
- **Not** a subordinate implementation profile of an external framework. CAES is an independent, complementary standard that *maps* to external frameworks; it is not derived from them. `[G-TAG:S]`
- **Not** a marketing-dashboard telemetry vendor. Telemetry is first-class public proof, reproducible by the buyer — see `TELEMETRY-PUBLIC-PROOF-POSTURE.md`.
- **Not** a best-effort observability tool. Observability describes what happened; Keon **authorizes** what is allowed to happen and **proves** it afterward.

---

## The wedge — most-exposed regulated industries

Keon enters where the cost of an ungoverned consequential action is highest and where audit-ready evidence is already a legal or regulatory expectation:

- **Financial services** — trades, transfers, credit decisions, market actions.
- **Health** — care decisions, ePHI-touching actions, clinical workflow automation.
- **Legal** — privileged actions, filings, obligations, retention.
- **Insurance** — underwriting, claims adjudication, payout authorization.
- **Critical infrastructure** — control-plane actions with physical or systemic effect.

These segments share three properties: high blast radius per action, an existing audit expectation, and a regulator who will ask "was it allowed, what happened and why, and can you prove it." Keon answers all three.

`[G-TAG:P]` `[G-FALSIFY: If a target segment can adopt fully autonomous consequential action with no governance-of-reasoning and no audit-ready evidence and incur no regulatory, legal, or contractual exposure, that segment is not in the wedge and the targeting is wrong.]`

---

## Relationship to the three proofs

The thesis decomposes into three independently verifiable proofs — Authority, Causation, Viability — formalized in `THREE-PROOFS-FRAME.md`. The thesis is satisfied only when all three hold for a given consequential action.

## Open question feeding G0

The thesis asserts audit-ready evidence for consequential action. One evidence class — **deliberation evidence** ("what alternatives were considered") — does not fit the linear causal spine natively. Its canonical home is framed but **not resolved** in `DELIBERATION-EVIDENCE-MODEL.md` and is surfaced to Clint at G0.

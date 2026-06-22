TECHNICAL WHITEPAPER · v2.1

# GOVERNING THE REASONING-TO-ACTION CHAIN
### Audit-ready evidence for consequential autonomous action

**Keon Systems** | 2026 · Distribution: Public

> **Keon governs the reasoning-to-action chain for autonomous AI systems and produces audit-ready evidence for consequential action.**

---

### How to read this paper — claim tagging and falsification

Every public claim in this paper is tagged for provenance, and every headline claim states how a skeptic would catch us if it were false.

- **`[S]` standards-backed** — the claim is anchored in an external, independently published standard (CAES / CPP, owned by the CAES Working Group). This paper *references* that standard; it does not author it.
- **`[P]` product-only** — the claim is a property of the Keon product (Collective deliberation, telemetry, causal-reconstruction depth). It is real and falsifiable, but it is not a standards claim. We never imply standards backing a claim does not have.
- **`Falsification:`** — for each headline claim, the concrete test a skeptic can run to prove us wrong. A claim with no falsification method is not a headline claim.

The mechanical substrate that earlier Keon material led with — decision receipts, fail-closed enforcement, PolicyHash binding, append-only records — is **not the headline of this paper. It is the substrate.** It is necessary, it is preserved in full in the Technical Addendum, and it is what makes the headline claims provable. But the differentiated claim is the one above: governance of the whole chain from reasoning to action, and audit-ready evidence of consequential action.

---

## ABSTRACT
### Executive Summary

Artificial Intelligence has crossed a threshold from advisory capabilities — summarization, search, content generation — to operational capabilities: transaction execution, code modification, data access, and autonomous decision-making at scale. This shift changes the question enterprises must answer. It is no longer *"what did the model say?"* It is *"what did the system decide, why, what else did it consider, and can we prove all of it after the fact?"*

> **Advisory AI incurs reputational risk. Operational AI incurs liability.** `[P]`

Autonomous systems now cross **Effect Boundaries** — the points where reasoning becomes consequence: an external call, a human-facing message, a state change, a safety-critical actuation — faster than any enterprise can audit by hand. The gap between *action* and *provable account of that action* is the exposure.

This paper defines how Keon closes that gap. Keon governs the **reasoning-to-action chain** — the path from what an agent intends, through what it considered and rejected, to what it was authorized to do and what it actually did — and produces **audit-ready evidence** for every consequential action. `[P]`

Keon operates in two modes. In **BYOAI mode**, any external AI — any model, any framework — is governed by the Keon enforcement layer. The intelligence is yours. The accountability is ours. In **Full Keon mode**, the Keon Collective provides the cognition layer: a governed cognitive civilization whose internal thought may scale, branch, and challenge itself freely, but whose effects may only enter reality through cryptographically authorized law.

> **Thought is free. Effects are governed.** `[P]`

The substrate that enforces this — pre-execution authorization, signed decision receipts, deterministic policy binding, fail-closed behavior, append-only records — is identical in both modes and is documented mechanically in the Technical Addendum. It is the floor, not the headline.

> **Within five years, deploying autonomous AI in regulated industries without an audit-ready account of the reasoning-to-action chain will be considered operationally negligent.** `[P]`
> **Falsification:** name a regulated-industry incident review, post-2026, that accepted "the logs show the action" in place of a reconstructable decision-and-authorization chain. If that becomes the norm, this prediction is wrong.

---

## 1 · The Problem: Autonomy Crosses Effect Boundaries Faster Than Enterprises Can Audit

For a decade, AI was "read-only." If a model hallucinated in a chat interface, the cost was wasted time. The human user was the operational firewall, evaluating output before acting on it.

We are now in the "read-write" era. Agents query production databases, commit code, authorize payments, and modify configuration directly. The latency between AI intent and real-world consequence has collapsed to milliseconds. The human firewall no longer exists at scale.

> **In this paradigm, the AI is no longer a tool. It is an actor.** `[P]`

The fundamental tension: AI models are **probabilistic**, while enterprise operations require **determinism and accountability**. A model may output a correct action 99 times and a destructive one the 100th, with no change in its permissions. Each time it acts, it crosses an **Effect Boundary** — the line where reasoning becomes consequence:

| Effect Boundary | Examples |
| :--- | :--- |
| `ExternalSideEffect` | Network calls, filesystem writes, external APIs |
| `HumanFacingOutput` | Messages, emails, tickets, notifications |
| `GovernanceRelevantState` | Policy changes, permission changes, memory writes |
| `SafetyCriticalActuation` | Actuation beyond configured safety thresholds |
| `WorkflowTransition` | Workflow completion, gate reached, run-state transition |

> **Micro-steps are not actions. Actions are consequences.** `[P]`

When a probabilistic actor crosses these boundaries at machine speed, three failure modes emerge — and all three are failures of *audit*, not just of control:

| UNDEFINED BEHAVIOR | AUDIT GAPS | LIABILITY DRIFT |
| :--- | :--- | :--- |
| Actions technically possible but policy-violating. | Inability to reconstruct *why* an action was taken — only *that* it happened. | Gradual expansion of an agent's scope beyond original design intent. |

The center column is the one that converts an engineering problem into a liability. The action happened in a millisecond; the obligation to *prove what happened and why* lasts for years.

> **The problem is not that AI systems are malicious. It is that they are architecturally incapable of proving they weren't.** `[P]`

---

## 2 · Why Logs, Prompts, and Post-Hoc Review Fail

Standard controls were built for humans and deterministic software. They fail when applied to probabilistic agents crossing Effect Boundaries autonomously.

**Logs are not evidence.** `[P]` Logs are mutable, non-standardized streams of text. They record *what* happened, after the fact, and rarely capture the *authorization logic* that permitted the event. A log shows a crash; it does not prove the brakes were applied. **Falsification:** take any log line for a consequential action and try to prove, offline, which policy version authorized it and that the log was not edited. If you can, logs were sufficient and Keon is unnecessary.

**Prompts are not policy.** `[P]` Prompt-level instructions ("only act within policy") are probabilistic guidance to the model, not a deterministic, evaluable constraint at the moment of action. A prompt cannot be hashed, bound to a decision, or replayed by an auditor. Prompt determinism is not execution determinism.

**Post-hoc review is not governance.** `[P]` Observability platforms alert *after* a threshold is breached. Reconstruction-after-the-fact assumes the record is complete, ordered, and unaltered — exactly the assumptions an autonomous probabilistic actor violates. Governance requires active, blocking interception *before* the effect, plus an account that survives adversarial scrutiny *after* it.

**API keys are not intent.** `[P]` An API key grants capability, not governance of intent. A key to the UserDB can read one record or dump the table. RBAC cannot inspect the semantic intent of a specific context-driven request.

Every layer of the current AI stack solves a real problem. None of them, alone, governs the reasoning-to-action chain:

| Layer | What It Solves | Where It Stops |
| :--- | :--- | :--- |
| **LLM APIs** | Text generation | No execution accountability |
| **Agent Frameworks** | Task delegation | No policy-bound decisions |
| **Orchestration** | Workflow routing | No forensic linkage |
| **Observability** | Performance logging | No authoritative ledger |
| **Gateway-only governance** | Tool-boundary admission | No reasoning, no causal reconstruction, no deliberation record (see §8) |

> **Governance of the reasoning-to-action chain begins where each of these stops.** `[P]`

---

## 3 · Thesis

> **Keon governs the reasoning-to-action chain for autonomous AI systems and produces audit-ready evidence for consequential action.** `[P]`

Two commitments, stated precisely:

1. **Govern the chain, not just the boundary.** Keon governs the full path — intent, the alternatives considered and rejected, the authorization decision, the policy in force, the execution, and the outcome — for any action that crosses an Effect Boundary. A tool gateway that only admits or blocks calls governs one point on that chain. Keon governs the chain.

2. **Produce audit-ready evidence.** For every consequential action, Keon produces an account that an auditor, a regulator, or a court can verify — offline, without access to the live system — covering *was it allowed, what happened and why, what else was considered, and what it cost operationally.*

The mechanical primitives that make this provable — pre-execution authorization, signed decision receipts, deterministic PolicyHash binding, fail-closed enforcement, append-only records — are **substrate**. They are necessary and non-negotiable (Technical Addendum, ADDENDUM 01–14), and no claim in this paper is softer than the primitive it rests on. But they are the floor beneath the thesis, not the thesis itself.

> **Thought is free. Effects are governed. And every governed effect leaves an account that survives scrutiny.** `[P]`
> **Falsification:** exhibit a consequential action governed by Keon for which no offline-verifiable account of authority, causation, and outcome can be produced. One such case falsifies the thesis.

---

## 4 · Architecture: Collective · Cortex · MCP Gateway · Runtime · Control

Keon governs the chain across five surfaces. Each has a defined role; none may absorb another's authority.

| Surface | Role | Governs |
| :--- | :--- | :--- |
| **Collective** | Cognition before action | Reasoning, branching, self-challenge, deliberation evidence (Full Keon mode) `[P]` |
| **Cortex** | Causal truth + reconstructable evidence | Memory substrate, causal spine, Evidence Pack, deliberation record `[P]` |
| **MCP Gateway** | Execution at the tool boundary | Boundary admission, identity/scope binding, non-bypassability |
| **Runtime** | Authority and mechanics | Policy decision, authority, execution mechanics, receipt emission |
| **Control** | Operator surface | Tenant / operator / audit / evidence cockpit |

**The Gateway and Runtime are distinct, and the distinction is load-bearing.** `[P]` The **MCP Gateway** is the sole lawful boundary between cognition and external consequence: it enforces boundary admission, binds identity and scope, and guarantees non-bypassability. It may call the Runtime to Decide and Execute — but it does **not** own policy evaluation or authorization. The **Runtime** owns authority, the policy decision, execution mechanics, and receipt emission. Conflating them is how "gateway-only" systems end up unable to prove *why* an action was allowed (see §8).

> **Gateway falsification:** an Effect-Boundary invocation that executes without passing through **both** Gateway enforcement **and** a Runtime decision. If that path exists, non-bypassability is broken.

**Three planes keep cognition and consequence separate.** Reasoning may explore freely; only one plane may cross an Effect Boundary; meaning is always anchored to receipt-grounded truth.

```
┌─────────────────────┬──────────────────────┬─────────────────────┐
│   COGNITION PLANE   │    REALITY PLANE     │   MEANING PLANE     │
│ Free to explore     │ Sole effect boundary │ Makes system        │
│ Branch · Simulate   │ Gateway → Runtime    │ legible             │
│ Challenge · Plan    │ Receipts · Spine     │ Witness Narratives  │
│ May not cause       │ No bypass permitted  │ Never contradicts   │
│ effects directly    │                      │ receipt truth       │
└─────────────────────┴──────────────────────┴─────────────────────┘
```

The substrate beneath these surfaces — the ALPHA authority handshake, the Authoritative Receipt Outbox, the KEON-SPINE-SPEC causal contract — is specified mechanically in the Technical Addendum. Architecturally, every external effect from any AI, in either mode, traverses the Gateway and is decided by the Runtime, and nowhere else.

> **Cognition may explore freely, but it may not directly cause effects.** `[P]`

---

## 5 · Evidence Model: The Three Proofs

Audit-ready evidence is not one artifact. It answers three distinct questions, each with its own mechanism and its own provenance. This is the narrative spine of every public claim Keon makes.

| Proof | Question Answered | Mechanism | Provenance |
| :--- | :--- | :--- | :--- |
| **Authority** | Was it allowed? | Decision Receipt · PolicyHash · fail-closed gating | `[S]` standards-backed (CAES L1/L2, CPP) |
| **Causation** | What happened, why, and what else was considered? | Evidence Pack · causal spine · deliberation evidence | Causal spine + Evidence Pack `[S]`; **deliberation evidence `[P]`** |
| **Viability** | What did it cost? Did it stay up? | Telemetry · degraded-mode behavior · chaos attestation | Product telemetry `[P]`; chaos/degraded-mode attestation partial `[S]` (CAES L3 ChaosTestAttestation) |

**Authority** is the floor: a signed Decision Receipt minted before execution, bound to the deterministic PolicyHash in force, with fail-closed denial on any gap. (Substrate: ADDENDUM 01–08, FP-01..07.)

**Causation** is reconstruction: the Evidence Pack and the append-only causal spine let an auditor rebuild the chain from directive to outcome — offline, without live-system access. (Substrate: §Evidence Packs below, FP-04/05, the failure-scenario walkthrough.)

**Deliberation evidence — "what alternatives were considered" — is part of Causation, and it is a product capability, not a standards claim.** `[P]` In Full Keon mode, the Collective Causal Record preserves the branches considered, the adversarial findings, the heat at submission, and the collapse rationale. This evidence lives in **Cortex / the Evidence Pack by default**. It is **not** a CAES primitive; CAES is not extended to absorb alternatives-considered. A rejected branch that would itself have crossed an Effect Boundary still resides in Cortex / the Evidence Pack unless a future CAES amendment explicitly defines such an object. Crossing an Effect Boundary triggers *heightened evidence handling*, not standards ownership.

> **We never tag deliberation evidence `[S]`.** It is real, auditable, and falsifiable — and it is product-only.
> **Falsification (Causation):** request the Evidence Pack for a Full-Keon consequential action and try to reconstruct what alternatives were evaluated and why the chosen path won, offline. If the alternatives are absent or unanchored to receipts, the Causation claim fails.

**Evidence Packs.** `[S]` (format/offline-verifiability) The Evidence Pack is a cryptographically sealed, portable artifact: self-contained (input, policy snapshot, decision receipt, execution trace), offline-verifiable (no live environment or Keon runtime required), and tamper-evident (one changed byte invalidates the seal). It shifts the burden of proof from "trust the operator" to "verify the artifact."

> **If an action cannot be proven to an auditor, it is a liability.** `[P]`

A worked example of all three proofs on a single regulated action — an autonomous credit decision — is preserved in the Technical Addendum's practice walkthrough and in the Failure Scenario section.

---

## 6 · Telemetry Model: Buyer-Reproducible Viability Proof

Viability is the third proof, and it is where most governance narratives quietly cheat — by publishing marketing numbers. Keon does not. **No benchmark number appears in this paper, and none will appear on any public Keon surface that the buyer cannot reproduce.** `[P]`

Keon's telemetry posture mirrors the offline-verifiable Evidence Pack: we publish **metric definitions, sources, and a reproduction method** — not dashboards. The metric families:

| Metric family | What it proves |
| :--- | :--- |
| Latency added (decision / gateway / persistence) | Governance overhead is bounded and measurable `[P]` |
| Reliability / availability posture | The substrate stays up under load `[P]` |
| Success / deny / fail-closed rates | Constraints are active, not theoretical `[P]` |
| Receipt-persistence overhead | Durable authority costs what we say it costs `[P]` |
| Gateway overhead | The boundary is not a hidden tax `[P]` |
| Evidence-pack generation overhead | Audit-readiness has a known price `[P]` |
| Degraded-mode behavior | Under failure, the system denies or escalates — never executes receiptless `[P]` / partial `[S]` |

**Buyer-reproducible means buyer-reproducible.** `[P]` Keon publishes the measurement methodology and a verification harness so a buyer can measure Keon's overhead in their own environment — *"measure our overhead yourself."* Chaos and degraded-mode attestation may additionally anchor to CAES L3 ChaosTestAttestation `[S]`; the operational numbers themselves remain product telemetry `[P]`.

> **Falsification:** run the published harness against a Keon deployment and obtain overhead materially worse than the methodology implies, or find a public Keon number you cannot reproduce. Either one falsifies the viability posture.
> **Degraded-mode law:** when Keon is degraded, an effect-bound action resolves to **Denied** or **RequiresHumanAuthorization** (suspend + escalate; timeout → Denied). Receiptless execution is never permitted. `[P]`

---

## 7 · Standards Posture (Reference)

> This section **references** the canonical standard. It does **not** author it. The Constitutional AI Execution Standard (CAES) and the Constitutional Policy Protocol (CPP) are maintained by the **CAES Working Group**; their canonical text is the authority for every `[S]` tag in this paper.

Three points, all owned by the standard, not by this paper:

- **CAES is an independent, complementary standard.** `[S]` It supplies the mechanical enforcement layer that external frameworks (e.g., EU AI Act Art. 12, SOC 2, ISO/IEC 27001) *require but do not specify*. The posture is **"we enable / we map to,"** never **"we are a profile of."** Keon is *a* reference implementation of CAES; reference designation does not imply commercial endorsement, and other implementations may independently conform.
- **CAES stays narrow.** `[S]` It does not absorb deliberation / alternatives-considered evidence (that is product-only — see §5). Degraded-mode behavior is bounded by CAES to Denied or RequiresHumanAuthorization; receiptless execution is never permitted.
- **The property → framework mapping is informative.** `[S]` Keon's Governed-Execution properties map to SOC 2, EU AI Act, and ISO/IEC 27001 controls; the mapping tables are preserved in the Regulatory Alignment appendix and are explicitly *informative* — Keon produces evidence frameworks require; it does not certify compliance.

For the normative definitions of receipts, PolicyHash, the governed spine, conformance levels, and conformance-claim rules, consult the canonical CAES/CPP specification directly. This paper does not restate or redefine them.

---

## 8 · Why Keon Is Not Gateway-Only Governance

A tool gateway is necessary and Keon has one (§4). But a gateway, alone, cannot govern the reasoning-to-action chain — and "gateway-only governance" is the most common way the market under-solves this problem.

A gateway sits at the **tool boundary**. It can admit or block a call, bind identity, and enforce non-bypassability. What it cannot do, alone:

| A gateway alone cannot… | Keon surface that does | Tag |
| :--- | :--- | :--- |
| Govern the reasoning *before* the call — the alternatives weighed and rejected | Collective + Cortex (deliberation evidence) | `[P]` |
| Own the authority decision and the policy in force at decision time | Runtime (decision, PolicyHash, receipt) | `[S]` |
| Reconstruct causation from directive to outcome, offline | Cortex (causal spine + Evidence Pack) | `[S]` |
| Preserve *why this path over others* for audit | Cortex (Collective Causal Record) | `[P]` |
| Prove operational viability reproducibly | Telemetry posture (§6) | `[P]` |

The Gateway/Runtime separation (§4) is precisely the line a gateway-only system blurs: it admits the call but cannot prove *why it was authorized*, because nothing behind it owns the decision and emits the receipt. Keon's edge is the **combination** — Collective, Cortex, Gateway, Runtime, Control — governing the whole chain and producing all three proofs. The gateway is one surface, the Effect Boundary. It is not the governance.

> **Falsification:** show a gateway-only architecture that produces, for a consequential action, an offline-verifiable account of authority **and** causation **and** the alternatives considered. If a gateway alone can do that, this section is wrong.

---

## 9 · The Regulated-Enterprise Wedge

Governed execution becomes inevitable first where consequential action already carries legal, financial, and audit obligation — the most-exposed regulated industries:

- **Financial services** — autonomous credit, payments, trading; Fair Lending and transaction-evidence obligations.
- **Healthcare** — clinical and access decisions; provenance and consent obligations.
- **Legal** — privileged, discoverable decisions that must be reconstructable.
- **Insurance** — automated underwriting and claims; explainability and audit obligations.
- **Critical infrastructure** — safety-critical actuation where degraded-mode behavior is itself regulated.

These are the buyers for whom the audit gap (§1) is not a future risk but a present obligation. Three forces converge here first: autonomy is escalating (agents act, not advise); liability is shifting (underwriters price AI operational risk, regulators demand evidence trails, discovery targets decision records); and trust has collapsed (black-box action is now a board-level risk). The first era of AI optimized for capability. The regulated wedge is where the second era — accountability — is already being priced.

> **Keon is not early. Keon is synchronized with where the obligation lands first.** `[P]`

---

## 10 · Operational Proof Obligations

This is the paper's contract with a skeptic. Each headline claim above rests on a mechanical guarantee that is enforced, not aspirational — and each comes with the test that would break it.

| Obligation | Mechanism (substrate) | How a skeptic falsifies it |
| :--- | :--- | :--- |
| No execution without authority | Verified Decision Receipt required pre-execution; fail-closed | Find an executed effect with no antecedent verified receipt |
| Policy state is provable | Deterministic PolicyHash (SHA-256 over canonical inputs, JCS) | Recompute the PolicyHash and get a different value than the receipt |
| Authority survives storage | Write-then-verify ARO (byte-level readback before authority) | Show an acknowledged-but-unpersisted receipt that still authorized execution |
| The record cannot be silently altered | Append-only causal spine; revocation is an append, not a mutation | Reorder or edit a spine entry without detection |
| Evidence verifies offline | Cryptographically sealed Evidence Pack | Alter one byte and have the seal still validate |
| Denial is evidence | DENY serialized, signed, ARO-verified, spine-appended | Find a constraint activation that left no recorded denial |
| Causation is reconstructable | Causal spine + Evidence Pack | Fail to rebuild directive→outcome offline for a consequential action |
| Deliberation is preserved `[P]` | Collective Causal Record (Cortex / Evidence Pack) | Find a Full-Keon action whose considered alternatives were silently discarded |
| Degraded mode never executes receiptless | Denied / RequiresHumanAuthorization; timeout → Denied | Produce a receiptless execution under degradation |
| Viability is reproducible `[P]` | Published methodology + verification harness | Publish-vs-measure mismatch you can demonstrate |

The full mechanical specification of these guarantees — the ALPHA authority handshake, the ARO state machine, the KEON-SPINE-SPEC causal contract, the Collective's Reality-Boundary / Temporal-Echo / Cognitive-Heat / Civilizational-Truth / Oversight contracts, the eleven Forensic Properties (FP-01..11), the nine adversarial Failure Scenarios, and the SOC 2 / EU AI Act / ISO 27001 mapping — follows in the Technical Addendum. **Engineers scan invariants. Auditors search for proofs. Executives skim vision. The substrate below is for the first two; the thesis above is for all three.**

> **Governed AI is not a feature. It is the substrate upon which autonomous systems must be built — and audit-ready evidence of the reasoning-to-action chain is what makes that substrate trustworthy.** `[P]`

---

***

# TECHNICAL ADDENDUM
v1.0 → v1.1 · March 2026

## Governed Execution — Mechanical Enforcement Guarantees
This addendum documents architectural enhancements implemented since v1.0. These updates convert the Governed Execution model from conceptual framework to mechanically enforced substrate.

### ADDENDUM 01
#### Authoritative Receipt Outbox (ARO)

v1.0 introduced the concept of a Decision Receipt. v1.1 introduces the **Authoritative Receipt Outbox (ARO)** — a write-then-verify enforcement layer that eliminates false acknowledgment risk.

**Problem Addressed**
A storage system may acknowledge a write before durable persistence. In governance systems, a "liar-store" creates a silent failure mode where authorization appears recorded but is not.

**Enforcement Guarantee (Immediate Mode)**
```text
1. Write receipt to store.
2. Receive acknowledgment.
3. Perform immediate readback.
4. Verify byte-level equality.
5. Fail closed on mismatch → DECISION_RECEIPT_VERIFICATION_FAILED
```

**ARO State Machine:**
`Pending` → `Persisted` → `Verified` → `Applied` → `Completed` *(or Failed - terminal)*

### ADDENDUM 02
#### PolicyHash as First-Class Invariant

v1.0 described policy evaluation. v1.1 formalizes **PolicyHash Binding**.
```text
PolicyHash = SHA-256(canonical(policy_id, policy_version, policy_effect))
# Deterministic · Lowercase hex · Computed at issuance · Immutable
```

The execution layer does **not** recompute policy. It consumes the stored `PolicyHash` embedded in the Decision Receipt. Execution fails closed if the receipt is missing, the PolicyHash mismatches, or the receipt has not passed ARO verification.

### ADDENDUM 03
#### Spine-First Ledger Enforcement

**Execution Order (Non-Negotiable)**
```text
1. Decision Receipt verified.
2. Spine append (authoritative ledger).
3. Trace projection.
4. Evidence materialization.

# If spine append fails: execution aborts — no projection — no partial state.
```

| Guarantee | Description |
| :--- | :--- |
| **Partition-scoped atomic sequence** | Events are ordered and isolated per partition. |
| **Causation chain integrity** | Causal linkage enforced across all events. |
| **Fail-closed on append errors** | Append failure halts execution unconditionally. |

> **The spine is the source of truth. Projections are derived, never authoritative.**


### ADDENDUM 04
#### Denial Receipts as First-Class Evidence

Every DENY decision is serialized, hashed, signed, passes through ARO, and appends to the authoritative spine.

> **A denial is not a log. It is proof that the guardrail functioned.**

High denial rates indicate policy refinement needs — not system instability.

### ADDENDUM 05
#### Execution Authorization Symmetry

Execution requires all of the following — with no exceptions:

- ✓ Receipt exists and is verified via ARO
- ✓ Stored PolicyHash matches evaluated policy
- ✓ Receipt ID is bound to execution scope
- ✓ Correlation and causation integrity intact

> **There is no execution path without a valid Decision Receipt.**
> **Not by omission. Not by configuration. Not by accident.**

### ADDENDUM 06
#### Fail-Closed as System Law

The system fails closed when any of the following conditions are detected:

| | |
| :--- | :--- |
| Receipt store verification fails | Missing spine ID |
| PolicyHash mismatches | Correlation drift detected |
| Spine append errors | Tenant boundary violation |

> **No silent degradation paths exist. Uncertainty is denial.**

### ADDENDUM 07
#### Governance Independent of Deployment Topology

Governance invariants are identical regardless of how the system is called:

| Invocation Surface | Governance Status |
| :--- | :--- |
| In-process invocation | **Fully governed** |
| A2A transport invocation | **Fully governed** |
| MCP capability exposure | **Fully governed** |
| Remote agent boundaries | **Fully governed** |

> **Governance is substrate-level, not API-level.**

### ADDENDUM 08
#### Mechanical, Not Interpretive

| | |
| :--- | :--- |
| **Deterministic Hashing** | SHA-256 over canonical policy inputs. Same inputs always produce the same PolicyHash. |
| **Write-Then-Verify Persistence** | Acknowledgment is not trust. Byte-level readback before authority is granted. |
| **Ordered Append-Only Ledger** | Events are causally chained. Reordering and retroactive modification are detectable. |
| **Explicit Execution Binding** | PolicyHash and Receipt ID bound to execution scope. No implicit authority paths. |
| **Cryptographic Denial Evidence** | DENY events are hashed, signed, and ledger-appended. Denial is affirmative proof. |
| **Fail-Closed Invariants** | Ambiguity, partial failure, and missing receipts all resolve to denial. |

> **Governance is no longer narrative.**
> **It is mechanical constraint.**

***

# TECHNICAL ADDENDUM
v1.1 → v1.2 · February 2026

## The Governed Memory Spine
### From Execution Enforcement to Ontological Correctness

This addendum introduces the **Keon Governed Memory Spine** — the architectural layer beneath Governed Execution that makes every event, decision, and action a first-class governed memory object.

---

### ADDENDUM 09
#### The Three-Layer Architecture

Keon is composed of three interlocking layers. Each addresses a distinct class of correctness problem.

| Layer | Component | Role |
| :--- | :--- | :--- |
| **Execution & Governance** | Keon Systems | Policy enforcement, execution receipts, ALPHA authority |
| **Memory Substrate** | Keon Cortex | Deterministic ingestion, tenant isolation, replay-safe indexing |
| **Causal Event Spine** | KEON-SPINE-SPEC v1.0 | Ontological contract binding all components |

**Division of Correctness Responsibility**

| Correctness Problem | Solved By |
| :--- | :--- |
| Was this action authorized before execution? | Keon Systems (ALPHA + ARO) |
| Is authorization evidence tamper-proof? | Keon Systems (Ed25519 + Evidence Pack) |
| Is memory correct under failure and restart? | Keon Cortex (Outbox + CQRS) |
| Is tenant isolation enforced at the data layer? | Keon Cortex (Two-gate enforcement) |
| Is the causal chain between events auditable? | KEON-SPINE-SPEC (IMemory + Spine) |
| Are actions bound to terminal outcomes? | KEON-SPINE-SPEC (1:1 IOutcome guardrail) |

### ADDENDUM 10
#### The Keon Spine — Causal Chain of Governed Memory

```
ITrigger → IIntent → IJustification → IDecision → IAction → IOutcome
```

**Spine Guardrails (Non-Negotiable)**

- Exactly one `ITrigger` per spine
- Exactly one terminal `IOutcome` per `IAction`
- Progress telemetry is NOT an Outcome
- No action executes without a preceding governance disposition
- No spine object is mutable after promotion

**IMemory Base Contract**

| Field | Type | Purpose |
| :--- | :--- | :--- |
| `memory_id` | UUIDv7 | Unique identity of this memory object |
| `event_id` | UUIDv7 | Identity of the source emission event |
| `spine_id` | UUIDv7 | Causal chain this object belongs to |
| `tenant_id` | string | Tenant scope — enforced at every layer |
| `actor_id` | string | Identity of the emitting actor |
| `hash_commitment` | hash | Canonical hash of the memory payload |
| `revocation_state` | enum | `active` · `revoked` · `tombstoned` + reason |
| `lineage_parent_id` | UUIDv7\|null | Causal parent in the spine |


### ADDENDUM 11
#### Canonical Event Envelope

Every event emitted into the Keon pipeline must carry a canonical envelope. No component may emit events outside this contract.

```json
{
  "event_id":       "uuidv7",
  "spine_id":       "uuidv7",
  "tenant_id":      "uuid|string",
  "actor_id":       "string|uuid",
  "correlation_id": "uuidv7",
  "causation_id":   "uuidv7|null",
  "interaction_id": "uuidv7|null",
  "occurred_at":    "utc-timestamp",
  "ingested_at":    "utc-timestamp|null",
  "sequence":       "int64|null",
  "event_type":     "string",
  "payload":        {},
  "metadata":       {}
}
```

`sequence` is monotonic per partition and assigned at ingestion — never by the actor. Legacy correlation identifiers must be stored under `metadata.legacy_correlation_id` and must not be used as spine join keys.

### ADDENDUM 12
#### Ordering, Partitioning, Replay, and Dead Letter

Ordering is guaranteed per partition, not globally.

| Partition Mode | Key |
| :--- | :--- |
| Default | `(tenant_id, spine_id, actor_id)` |
| Interaction-scoped | `(tenant_id, interaction_id)` |

A Dead Letter Queue must exist. Events route to the DLQ on schema violations, canonicalization failures, governance exceptions, and non-recoverable verification failures. The DLQ is not a discard bin — it is evidence of a constrained system encountering its boundaries.

The authoritative event log is append-only. Revocation and tombstoning are explicit append-only state transitions — not mutations.

### ADDENDUM 13
#### Governance Expanded — Dispositions, Conduct, and Fail-Closed

Every governance evaluation yields exactly one disposition:

| Disposition | Meaning |
| :--- | :--- |
| `Approve` | Action proceeds as proposed |
| `Rewrite` | Action proceeds with a platform-applied transform |
| `Block` | Action does not proceed |
| `RequiresHuman` | Action suspended pending explicit human authorization |

For actions that produce human-facing output, a Conduct Policy (`IConductPolicy`) applies pre-execution. Any missing or failed verification fails closed. JSON is canonicalized using JCS before hashing. Receipts are signed with Ed25519.

### ADDENDUM 14
#### What Cannot Be Bypassed

These invariants hold regardless of deployment topology, invocation surface, or runtime configuration.

- No execution without a verified Decision Receipt
- No cross-tenant memory access regardless of provider behavior
- No action without a preceding governance disposition
- No memory write without a canonical, platform-assigned ID
- No governance bypass through invocation surface changes
- No silent degradation — uncertainty resolves to denial
- No mutable canonical records — append-only is enforced, not assumed
- No actor control over whether governance is invoked — the platform decides

> **The spine does not trust its producers.**
> **Every emission is governed. Every promotion is attested. Every outcome is terminal.**

***

# TECHNICAL ADDENDUM
v1.2 → v1.3 · March 2026

## The Keon Collective — Governed Cognitive Civilization
### From Enforcement Substrate to Civilizational Architecture

This addendum documents the architectural specifications for the Keon Collective layer — the governed cognitive civilization that constitutes Full Keon mode. These specifications are grounded in the ratified Keon Collective Doctrine (KEON-COLLECTIVE-DOCTRINE-v0.3), the Canonical Contracts (KEON-COLLECTIVE-CONTRACTS-v0.1), and the Common Interfaces (KEON-COLLECTIVE-INTERFACES-v0.1).

---

### ADDENDUM 15
#### Collective Architecture Overview

The Keon Collective is organized across three planes (Section 10) and five primary contract domains.

**Entity Archetypes**

| Archetype | Role |
| :--- | :--- |
| **Council** | Strategic decomposition, governance oversight, swarm orchestration |
| **Guild** | Specialized domain knowledge, capability provision |
| **Worker** | Execution-oriented agents, task completion |

**Collective Host**

The `ICollectiveHost` is the Pantheon orchestration seam. It accepts a `CollectiveIntent` and returns a `CollectiveExecutionResult` containing the intent ID, selected branch ID, causal record ID, decision status, execution status, and summary. The host coordinates the full lifecycle: intent intake, branch materialization, adversarial review, heat evaluation, branch collapse, reality boundary submission, and causal record construction.

**Five Contract Domains**

1. **Reality Boundary** — sole lawful effect choke point (RB-1 through RB-7)
2. **Temporal Echo** — speculative branch lifecycle (TE-1 through TE-7)
3. **Cognitive Heat** — compositional risk thermodynamics (CH-1 through CH-7)
4. **Civilizational Truth** — causal record and reconstructive anchoring (CT-1 through CT-6)
5. **Oversight and Escalation** — gradient authority with compensation rule (OE-1 through OE-6)

---

### ADDENDUM 16
#### Reality Boundary Contract

The Reality Boundary is the single lawful choke point through which the Collective may request external effects. It enforces: the Law of Governed Reality, the Law of Inherited Identity, the Law of Receipt Preservation, and the Law of Separation.

**GovernedEffectRequest** (required fields)

```
request_id · tenant_context · actor_context · governance_context
originating_intent_ref · selected_branch_ref · branch_collapse_proof
adversarial_review_ref · heat_profile · requested_effect
requested_capability · input_payload · correlation_context
```

**GovernedEffectResult** (returned fields)

```
request_id · decision_status · execution_status
gateway_response_ref · decision_receipt_ref · outcome_receipt_ref
spine_refs[] · canonical_correlation_id · policy_binding_summary
result_payload_ref · denial_reason · returned_heat_adjustments
```

**Reality Boundary Invariants**

| ID | Invariant |
| :--- | :--- |
| RB-1 | Every effect-bound request must reference exactly one selected winning branch |
| RB-2 | No effect-bound request may originate from an uncollapsed speculative branch |
| RB-3 | Every effect-bound request must reference adversarial review output |
| RB-4 | Tenant and actor bindings must be inherited and immutable within the request |
| RB-5 | Denial outcomes are still canonical events and must return receipt anchors |
| RB-6 | No Cognition Plane interface may directly emit an external effect payload |
| RB-7 | Missing branch lineage, identity binding, or review state must fail closed |


### ADDENDUM 17
#### Temporal Echo Contract

The Temporal Echo Contract defines the lifecycle of speculative cognition from branch creation through evaluation, collapse, archival, and linkage to governed effect requests.

**Branch State Machine**

```
Proposed → Materialized → Simulating → Evaluated → CollapsedWinner → GovernedEffectRequestEligible
                                                 └→ CollapsedLoser → Archived
any nonterminal → Aborted
high-heat or invalid → Pruned
```

**Forbidden Transitions**
- Any loser branch directly to execution eligibility
- Any speculative branch directly to external effect
- Silent deletion of branch history after evaluation

**BranchCollapseRecord** contains: collapse ID, intent ref, candidate branch refs, selected branch ref, selection rationale, comparative heat summary, comparative utility summary, challenge summary, loser archival refs, witness summary ref, and timestamp.

**Temporal Echo Invariants**

| ID | Invariant |
| :--- | :--- |
| TE-1 | Every execution-eligible plan must originate from an explicit collapse event |
| TE-2 | Every collapse event must identify evaluated alternatives |
| TE-3 | Loser branches must not be silently discarded — archived, pruned with rationale, or aborted |
| TE-4 | Each branch must carry heat and challenge metadata for forensic interpretation |
| TE-5 | A winning branch must remain linked to sibling alternatives and parent intent lineage |
| TE-6 | Branches may be pruned automatically for heat, contradiction, or governance failure |
| TE-7 | Temporal Echoes remain non-effecting until elevated through collapse and the Reality Boundary |

---

### ADDENDUM 18
#### Cognitive Heat Contract

Cognitive Heat is a calculated, inspectable, compositional signal — not metaphor.

**Six Heat Dimensions**

| Dimension | Source |
| :--- | :--- |
| `entity_heat` | Local strain within a single agent |
| `branch_heat` | Instability within a speculative branch |
| `interaction_heat` | Instability from coordination across participants |
| `challenge_heat` | Risk surfaced by adversarial self-examination |
| `boundary_heat` | Proximity to sensitive governance boundaries |
| `swarm_heat` | Aggregate heat for the active swarm |

**Threshold States:** `Cool` · `Warm` · `Hot` · `Critical`

**Compositional Rule:** Composite heat is not the sum of local heats. A swarm may be hot even when each participant is individually cool. Coordination instability, contradiction, repeated failed simulation, adversarial findings, and boundary proximity may independently raise composite heat.

**Cognitive Heat Invariants**

| ID | Invariant |
| :--- | :--- |
| CH-1 | Every execution-eligible branch must carry a current heat profile |
| CH-3 | Heat thresholds may influence pacing and escalation but must not authorize bypass of governed execution |
| CH-4 | Critical heat must be capable of triggering self-prune, pause, or abort before effect request |
| CH-6 | Lower human oversight depth may not reduce heat sensitivity |
| CH-7 | Adversarial findings must be allowed to increase challenge heat and composite heat |


### ADDENDUM 19
#### Civilizational Truth Contract

The canonical truth unit for the Collective is the **Collective Causal Record** — the Collective's anchored wrapper around gateway receipts.

**CollectiveCausalRecord** contains:

```
causal_record_id · intent_ref · selected_branch_ref · branch_collapse_ref
adversarial_review_ref · governed_effect_request_ref
decision_receipt_ref · outcome_receipt_ref · spine_refs[]
participant_entities[] · heat_profile_ref · witness_digest_ref
reconstructive_anchor_ref · lineage_refs[] · fossil_candidate_flag
```

**ReconstructiveAnchor** contains the minimum symbolic state required to regenerate high-value understanding from canonical truth without treating volatile summaries as primary truth: spine refs, topology seed, decision invariant seed, heat snapshot, witness seed, lineage seed, integrity hash, and reconstruction version.

**Civilizational Truth Invariants**

| ID | Invariant |
| :--- | :--- |
| CT-1 | Every effect of consequence must be representable as a Collective Causal Record |
| CT-2 | Every Collective Causal Record must anchor to one or more canonical receipt references |
| CT-3 | If witness meaning conflicts with receipt-grounded truth, receipt-grounded truth wins |
| CT-4 | Reconstructive anchors must never supersede receipts |
| CT-5 | Lifecycle transitions must preserve lineage references sufficient for ancestry queries |
| CT-6 | A causal record must be queryable by intent, branch, receipt, participant, and correlation |

---

### ADDENDUM 20
#### Oversight and Escalation Contract

The Oversight Contract defines how human oversight depth changes the shape of interaction without weakening governance rigor.

**Canonical Oversight Modes:** `Strategic` · `Tactical` · `Surgical` · `Observational`

**What Oversight May Change:** pause thresholds, branch comparison visibility, witness narrative fidelity, heartbeat frequency, escalation verbosity, adversarial challenge depth, operator notification policies.

**What Oversight Must Never Change:** Reality Boundary bypass, gateway governance, inherited scope, receipt preservation, adversarial challenge elimination, canonical truth anchoring, direct effecting from cognition.

**The Compensation Rule**

When human oversight depth decreases, the system must automatically compensate by increasing non-human safeguards. At minimum, reduced human touchpoints must increase one or more of: witness narrative fidelity, heartbeat richness, adversarial challenge depth, heat-threshold sensitivity, or post-hoc legibility.

**Oversight and Escalation Invariants**

| ID | Invariant |
| :--- | :--- |
| OE-1 | Every active swarm must have an effective oversight profile, explicit or inherited |
| OE-2 | No oversight profile may reduce the immutable governance floor |
| OE-3 | Reduced operator touchpoints must produce compensating increases in machine-side observability |
| OE-4 | Oversight configuration must be queryable for any effect-bearing causal record |
| OE-5 | Sensitive domains may impose minimum oversight depth without weakening other doctrine laws |


### ADDENDUM 21
#### Cross-Contract Invariants and Collective Interface Seams

**Ten Cross-Contract Invariants**

| ID | Invariant |
| :--- | :--- |
| X-1 | No external effect may occur without a Reality Boundary request |
| X-2 | No Reality Boundary request may occur without a selected winning branch |
| X-3 | No selected winning branch may exist without collapse lineage |
| X-4 | No effect-bearing branch may proceed without adversarial review state |
| X-5 | No witness narrative of consequence may exist without causal anchoring |
| X-6 | No causal record may outrank its underlying receipt anchors |
| X-7 | No lifecycle transition may sever lineage |
| X-8 | Reduced human oversight may alter interaction style but may not reduce governance rigor |
| X-9 | All major consequential records must be queryable by correlation lineage |
| X-10 | Missing, inconsistent, or unverifiable causal anchors must fail closed for effect-bearing operations |

**Core Interface Seams (C# / KEON-COLLECTIVE-INTERFACES-v0.1)**

```
IRealityPlane                  — governed effect execution
IRealityBoundaryGuard          — pre-submission validation
ITemporalEchoPlanner           — branch materialization, evaluation, collapse
IBranchArchive                 — loser branch preservation
IAdversarialReviewEngine       — challenge before effect request
ICognitiveHeatCalculator       — compositional heat scoring
IHeatEscalationPolicy          — threshold-driven behavior decisions
IWitnessNarrativeEngine        — receipt-anchored operator narratives
ICausalRecordBuilder           — truth wrapper construction
ITruthReconstructionService    — causal record retrieval and reconstruction
IOversightProfileResolver      — gradient authority resolution
ICollectiveHost                — Pantheon orchestration seam
IHeartbeatService              — civilization vitals and operator visibility
ILineageService                — rebirth and lifecycle transition preservation
```

> **The doctrine gave us the laws. The contracts gave us the physics.**
> **The interfaces gave us the machine edges.**
> **No entity in the Collective may freestyle the substrate.**

***

# FORENSIC PROPERTIES
### Legal & Evidentiary Characteristics
**Governed Execution Under Adversarial Scrutiny**

#### FP-01: Chain of Authority
Every operational action executed under Keon is bound to a discrete authorization event traceable to the original input, the policy version in effect, the deterministic PolicyHash, the Decision Receipt, and the execution event appended to the authoritative spine. If no Decision Receipt exists, execution does not occur. This eliminates ghost execution.

#### FP-02: Deterministic Reproducibility
Each Decision Receipt contains canonicalized policy inputs, a deterministic PolicyHash, immutable identifiers, and timestamps recorded at issuance. An expert can independently recompute the PolicyHash and verify that the policy snapshot has not been altered.

#### FP-03: Write-Then-Verify Persistence Integrity
A receipt is written, acknowledged, immediately read back, byte-level equality is verified, and failure results in execution abort. The system can demonstrate that the receipt was not only acknowledged — it was durably stored and verified prior to execution.

#### FP-04: Append-Only Ledger with Ordered Causation
The authoritative spine is append-only, partition-scoped, strictly ordered, and fail-closed on append failure. Events cannot be retroactively inserted without detection. This establishes an unbroken chain of causation suitable for forensic reconstruction.

#### FP-05: Tamper-Evident Evidence Packs
An Evidence Pack contains the input proposal, policy snapshot, Decision Receipt, execution trace, and spine identifiers. The pack is cryptographically sealed. Changing any byte invalidates the signature. Verification can be performed offline without live system access.

#### FP-06: Denial as Affirmative Proof of Constraint
Denied actions are serialized, hashed, signed, and appended to the ledger. A denial is evidence of constraint functioning as designed.

#### FP-07: No Implicit Authority Paths
There is no silent bypass mechanism. Absence of receipt equals absence of authority. If execution occurred, a receipt exists. If a receipt cannot be produced, execution cannot be proven to have been authorized.

#### FP-08: Tenant Isolation and Boundary Enforcement
Missing, malformed, or mismatched tenant identifiers result in immediate failure. Boundary violations produce denial receipts, not silent access.

#### FP-09: Failure as Recorded Event
When the system fails closed, the failure is recorded. This creates an evidentiary record of constraint activation — not silent malfunction.

#### FP-10: Separation of Responsibility
Customer defines policy intent. Keon enforces policy mechanically and produces verifiable authorization artifacts. Auditor independently verifies cryptographic integrity. Keon does not determine policy morality. It enforces declared constraints and proves enforcement occurred.

#### FP-11: Collective Deliberation as Forensic Record `[P]` (product-only — not CAES-backed)
In Full Keon mode, the forensic record extends beyond the gateway. The Collective Causal Record — encompassing branch alternatives considered, adversarial review findings, heat profile at submission time, and collapse rationale — is available for operator audit. An investigator can reconstruct not only what the system did, but what alternatives were evaluated, what risks were surfaced internally, and why the selected path was chosen over others. Deliberation is evidence, not overhead.

This deliberation evidence is a **product capability of Cortex / the Evidence Pack**. It is **not** a CAES primitive: CAES is not extended to absorb alternatives-considered evidence, and a rejected branch that would itself have crossed an Effect Boundary remains in Cortex / the Evidence Pack unless a future CAES amendment explicitly defines such an object. This property is therefore tagged product-only `[P]`, never standards-backed.

> **Authority is provable. Causation is reconstructible.**
> **Policy state is verifiable. Tampering is detectable.**
> **In Full Keon mode: deliberation is auditable. Alternatives are preserved. Nothing is silently discarded.**

***

# FAILURE SCENARIO WALKTHROUGH
### Active Attack & Constraint Validation

#### SCENARIO 01: Policy Bypass Attempt
```text
Intercept → Proposed action captured before execution
Evaluate  → ALPHA evaluates against active policy
Result    → Policy returns DENY
Receipt   → Denial serialized, hashed, signed, ARO-verified, spine-appended
Gate      → No PASS receipt exists → Execution blocked
```
**Forensic Outcome:** Denial receipt exists. PolicyHash recorded. No mutation occurred.

#### SCENARIO 02: Liar-Store / Durability Attack
```text
Write     → Decision Receipt written to store
ACK       → Acknowledgment returned
Readback  → Immediate readback triggered
Verify    → Byte-level mismatch detected
Abort     → DECISION_RECEIPT_VERIFICATION_FAILED → Execution halted
```
**Forensic Outcome:** No execution proceeds. No partial state mutation.

#### SCENARIO 03: Policy Drift / Post-Hoc Manipulation
```text
Evaluation → Canonical policy snapshot hashed at evaluation time
Storage    → PolicyHash stored in Decision Receipt and spine
Audit      → SHA-256(canonical(policy_v3)) ≠ SHA-256(canonical(policy_v4))
Detection  → Mismatch proves post-hoc modification
```
**Forensic Outcome:** Historical authorization proof is immutable. Manipulation is detectable.

#### SCENARIO 04: Execution Without Receipt
```text
Gate check → Execution layer requires verified Decision Receipt
Missing    → Receipt absent → Execution fails closed immediately
No bypass  → No implicit authority, no fallback path
```
**Forensic Outcome:** Action does not execute. No backdoor execution path.

#### SCENARIO 05: Spine Tampering Attempt
```text
Append-only → Spine does not permit modification of existing entries
Sequence    → Ordered partition sequence breaks on reorder attempt
Seal check  → Evidence Pack signature invalidated by any modification
Detection   → Tamper is cryptographically evident
```
**Forensic Outcome:** Historical mutation cannot occur silently.

#### SCENARIO 06: Cross-Tenant Escalation
```text
Validate   → Tenant identifier validated at authorization layer
Mismatch   → Boundary violation detected immediately
Deny       → Denial receipt serialized and spine-appended
```
**Forensic Outcome:** No cross-tenant execution. Violation recorded as evidence.

#### SCENARIO 07: Correlation Drift / Causation Injection
```text
Validate   → Correlation and causation linkage validated
Scope      → Partition-scoped ordering enforced
Mismatch   → Scope binding violation detected
Abort      → Execution halted
```
**Forensic Outcome:** Receipts cannot be replayed in alternate contexts.

#### SCENARIO 08: Partial Failure During Execution
```text
Decision   → Receipt verified successfully
Spine      → Append attempted
Failure    → Append fails due to infrastructure instability
Abort      → Execution halted — trace and projection do not proceed
```
**Forensic Outcome:** No action occurs without ledger record.

#### SCENARIO 09: Collective Branch Escape Attempt
```text
Branch     → Speculative branch reaches CollapsedLoser state
Attempt    → Entity attempts to submit CollapsedLoser branch as effect request
Validate   → IRealityBoundaryGuard checks selected_branch_ref against collapse record
Reject     → RB-2 violated — branch is not CollapsedWinner → request fails closed
Receipt    → Denial receipt minted, spine-appended, causal record updated
```
**Forensic Outcome:** No loser branch executes. Violation recorded in Collective Causal Record. Adversarial review finding elevated. Heat profile updated.

> **If an unauthorized action occurs:**
> **Either a valid Decision Receipt exists and can be proven —**
> **Or system invariants were violated, in which case tampering is detectable.**
> **Governed Execution assumes scrutiny.**

***

# APPENDIX
### Regulatory Alignment & Control Mapping · Informative

## SOC 2 · EU AI Act · ISO/IEC 27001

### SOC 2: AICPA Trust Services Criteria

| Governed Execution Property | SOC 2 Criteria | Rationale |
| :--- | :--- | :--- |
| Decision Receipt (serialized, hashed, signed) | **CC6.1, CC6.6** | Demonstrates access decisions are formally evaluated and recorded before execution. |
| PolicyHash binding | **CC5.2** | Provides deterministic linkage to specific policy versions active at evaluation time. |
| ARO (write-then-verify) | **CC7.2, CC8.1** | Ensures authorization records are durably stored and verified prior to action. |
| Spine append-only ledger | **CC7.4** | Produces ordered, tamper-evident event record suitable for forensic reconstruction. |
| Denial receipts | **CC3.2, CC7.3** | Demonstrates guardrail enforcement and constraint functionality. |
| Tenant boundary enforcement | **CC6.6** | Prevents cross-boundary execution without explicit authorization. |
| Evidence Packs (offline-verifiable) | **CC7.5** | Enables independent verification of operational events without runtime access. |

### EU AI Act: High-Risk System Requirements

| Governed Execution Property | Article Alignment | Rationale |
| :--- | :--- | :--- |
| Decision as an event | **Art. 12** | Maintains machine-verifiable authorization records. |
| Evidence Packs | **Art. 12** | Enables reconstruction of decision logic and execution chain. |
| Policy snapshot + PolicyHash | **Art. 9** | Proves which risk rules were active at evaluation time. |
| Human cryptographic signing | **Art. 14** | Binds human authority to specific machine execution via signed delegation. |
| Denial receipts | **Art. 15** | Demonstrates that constraints are enforced, not theoretical. |
| Fail-closed invariants | **Art. 15** | Ensures uncertainty results in denial, not silent execution. |
| Ordered append-only ledger | **Art. 12** | Produces reconstructible causal sequence of events. |

### ISO/IEC 27001:2022: Information Security Management

| Governed Execution Property | Control Alignment | Rationale |
| :--- | :--- | :--- |
| Authorization evaluation per request | **A.5.15** | Ensures access decisions are context-aware and policy-bound. |
| Tenant scoping enforcement | **A.5.18** | Restricts system access to authorized domains. |
| Policy version binding (PolicyHash) | **A.8.32** | Links execution to defined and versioned policies. |
| Receipt verification before execution | **A.8.15** | Ensures audit records are created before operational effect. |
| Append-only spine ledger | **A.8.16** | Provides ordered, tamper-evident logging of operational events. |
| Evidence Pack sealing | **A.8.24** | Uses cryptographic mechanisms to protect integrity of records. |
| Fail-closed execution gating | **A.5.30** | Prevents unauthorized operation under partial system failure. |
| Denial recording | **A.5.7** | Captures attempted violations as security-relevant events. |

### Cross-Framework Convergence

Across SOC 2, EU AI Act, and ISO 27001, three architectural themes converge: traceability, authorization integrity, and tamper resistance. Governed Execution addresses all three through deterministic policy hashing, verified durable persistence, append-only ordered ledger, cryptographically sealed evidence artifacts, explicit denial proof, and fail-closed invariants.

These properties are architectural, not procedural. They operate regardless of deployment topology, hosting provider, or invocation surface.

### Important Clarification

Governed Execution does not certify compliance, does not replace organizational risk management programs, and does not substitute for governance policy definition. It provides the mechanical enforcement layer that allows organizations to prove that declared controls were applied at execution time. Compliance frameworks require evidence. Governed Execution produces it.

***


---

# DOCUMENT METADATA

**Document:** KEON WHITEPAPER v2.1 DRAFT
**Source:** Operation CLEAN EDGE — M1 re-headline / re-sequence of WHITEPAPER v2.0 (substrate preserved; narrative re-led on the reasoning-to-action thesis)
**Date:** June 2026 (M1); prior body March 2026
**Authority:** Keon Collective Doctrine (KEON-COLLECTIVE-DOCTRINE-v0.3), KEON-SPINE-SPEC v1.0, KEON-COLLECTIVE-CONTRACTS-v0.1, KEON-COLLECTIVE-INTERFACES-v0.1, CAES_INTERNAL_L3_SPEC_v1. Canonical standards posture is owned externally by the CAES Working Group (CAES / CPP); §7 references it as a stub and does not author it.

---

## CLEAN EDGE M1 Changes (v2.0 → v2.1)

| Area | Change |
| :--- | :--- |
| Title / headline | Re-led from "Governed Execution for Operational AI" to "Governing the Reasoning-to-Action Chain · Audit-ready evidence for consequential autonomous action" |
| Narrative | Re-sequenced into the 10-part structure (Problem → Why logs/prompts/post-hoc fail → Thesis → Architecture (5 surfaces) → Three Proofs → Telemetry → Standards stub → Why-not-gateway-only → Wedge → Proof obligations) |
| Substrate | Receipts, fail-closed, PolicyHash, append-only **demoted from headline to substrate**; all mechanical content preserved verbatim in the Technical Addendum |
| New sections | Three Proofs (Authority/Causation/Viability); buyer-reproducible Telemetry model; Gateway↔Runtime separation; Why-Keon-is-not-gateway-only; Regulated-enterprise wedge; Operational proof obligations |
| Standards | Former "Section 22 CAES Alignment" (which authored CAES three-primitives + conformance-level claims) **replaced by a reference stub (§7)** pointing to canonical CAES/CPP. No standards posture authored in the whitepaper. |
| Deliberation evidence | Tagged product-only `[P]` throughout (Position A); never implied CAES-backed |
| Gates applied | G-TAG (`[S]`/`[P]` legend + inline tags); G-FALSIFY (falsification method on every headline claim) |

---

## Sections Modified from v1.3

| Section | Change | Notes |
| :--- | :--- | :--- |
| Abstract | Expanded | Added BYOAI/Full Keon modes, core thesis, dual-mode framing |
| Section 08 | Expanded | Effect Boundaries table added, spine invariants clarified |
| Section 20 (was 14) | Fixed | Removed false identity claim. New accurate framing. |
| Section 21 (was 15) | Rebuilt | Four-layer ASCII diagram + three-plane governance model |
| Addendum 03 | Clarified | Spine enforcement order made explicit |
| Forensic Properties | Expanded | FP-11 added for Collective deliberation forensics |
| Failure Scenarios | Expanded | Scenario 09 added for Collective branch escape attempt |

## Sections Added in v2.0

| Section | Title | Type |
| :--- | :--- | :--- |
| Section 09 | Two Operating Modes | NEW |
| Section 10 | The Three Planes | NEW |
| Section 11 | The MCP Gateway | NEW |
| Section 12 | The Keon Collective | NEW |
| Section 13 | Self-Governance Before Execution | NEW |
| Section 14 | The Truth System | NEW |
| Section 22 | CAES Alignment | NEW |
| Section 23 | Why This Changes Everything | NEW |
| Addendum 15 | Collective Architecture Overview | NEW |
| Addendum 16 | Reality Boundary Contract | NEW |
| Addendum 17 | Temporal Echo Contract | NEW |
| Addendum 18 | Cognitive Heat Contract | NEW |
| Addendum 19 | Civilizational Truth Contract | NEW |
| Addendum 20 | Oversight and Escalation Contract | NEW |
| Addendum 21 | Cross-Contract Invariants and Interface Seams | NEW |

## Assumptions Requiring Verification Before External Publication

1. **Standards posture (was "Section 22 CAES Alignment")** — RESOLVED under CLEAN EDGE M1: the whitepaper no longer authors CAES three-primitives / L3-invariant / conformance-level prose. §7 is now a reference stub pointing to the canonical CAES/CPP specification (CAES Working Group). Any `[S]` claim must be verified against canonical CAES/CPP before publication — not re-authored here.

2. **CAES standard version** — referenced as CAES v0.1.1 in source documents. Confirm current version string before publishing.

3. **Addendum dating** — v1.1 dated March 2026, v1.2 dated February 2026 per source document headers. Verify intended publication sequence and dates.

4. **Collective architecture status** — interfaces document dated March 7, 2026 with "Draft for Pantheon Review" status. Confirm Collective sections are appropriate for public whitepaper vs. technical preview framing.

---

*Keon Systems · Governing the Reasoning-to-Action Chain · v2.1 (CLEAN EDGE M1) · 2026 Keon Systems. All rights reserved.*

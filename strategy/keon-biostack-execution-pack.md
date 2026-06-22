# Keon Systems + BioStack — 90-Day Execution Pack
**Version 1.0 — June 2026 | Solo founder, limited capital | Strategy locked per memo of record**

Clock starts Monday, June 15, 2026. Day 90 lands ~September 12, 2026 — the end of "Q1" in the locked plan (Jul–Sep 2026). This pack assumes ~15–20 founder hours/week. If actual capacity is lower, cut per the rules in §1.4 — do not stretch timelines silently.

---

## 1. 30/60/90-Day Execution Plan

**Standing rule: maximum 3 active workstreams at any time.**

- **WS-A: Clearance & Boundaries** (legal/IP/entity) — gates everything
- **WS-B: BioStack Beta** (the revenue and proof engine)
- **WS-C: Keon Spec & Writing** (category staking — *writing only*, no Keon code until Day 61+)

Anything not in these three does not exist for 90 days.

### 1.1 Days 1–30 (Jun 15 – Jul 12)

**Week 1 — Foundations or nothing.**
- WS-A: Submit written moonlighting/IP clearance request to employer (Day 1–2). Form the LLC (or confirm existing entity). Open separate bank account, separate GitHub org, separate cloud account, separate AI-tool subscriptions on personal payment. Personal hardware only from this day forward.
- WS-B: Lock the beta vertical (see §2.3). Write and freeze the Beta Scope Document — one page, what's in, what's out, signed by you, dated. Scope changes require a written reason.
- WS-C: Nothing yet. Resist.
- **Deliverables:** clearance request sent; entity + separated infrastructure live; scope freeze doc.

**Week 2 — Data model and corpus.**
- WS-B: BioStack data model (protocols, compounds, doses, evidence records, citations, evidence-tier enum). Begin curating the evidence corpus: target 50 compounds in the chosen vertical, each with tier label + 2–5 citations + a one-paragraph plain-language summary. This corpus IS the product; budget 40% of all Week 2–4 hours here.
- WS-B: Landing page + waitlist live (no claims beyond "evidence-graded protocol intelligence — coming soon").
- **Deliverables:** schema committed; 15 compounds graded; waitlist live.

**Week 3 — Core build.**
- WS-B: Auth, protocol portal, supplement/dose logging, weekly view, evidence labels rendered with tap-through citations, honest empty states.
- **Deliverables:** end-to-end flow works for one user (you); 30 compounds graded.

**Week 4 — Dogfood and recruit.**
- WS-B: You run your own stack through it for the full week. Fix what hurts. Recruit 20–25 beta candidates from 2–3 longevity/supplement communities (direct outreach, not broadcast).
- WS-C: Keon whitepaper *outline only* drafted (see §3.1) — 2–3 hours max.
- **Deliverables:** 50 compounds graded; 20+ committed beta users; whitepaper outline.

### 1.2 Days 31–60 (Jul 13 – Aug 9)

**Week 5 — Beta wave 1.**
- WS-B: Invite 25 users. Instrument activation (§2.6) and weekly retention. 15-minute feedback calls with 5 users.
- WS-C: Whitepaper v0.5 draft (definitions + proof primitives sections).

**Week 6 — Evidence intelligence deepens.**
- WS-B: Ship the evidence changelog ("what changed in the evidence for YOUR stack") and contradiction surfacing. These are the retention loop — they outrank every other feature request.
- **Gate check:** if Week-2 cohort retention <30%, stop building and run 10 user interviews before writing another line of code.

**Week 7 — Wave 2 + presale prep.**
- WS-B: Expand to 75–100 users. Build the Founding Member presale page (§2.8). Do not open it yet.
- WS-A: Confirm clearance status. **If clearance is not in writing by Day 49, escalate or get employment counsel — no public commercial activity without it.**

**Week 8 — First dollars + peer review.**
- WS-B: Open Founding Member presale to beta users only ($99/yr, cap 100, honest presale framing).
- WS-C: Whitepaper v1 complete; send to 2–3 trusted technical peers (outside your employer) for review.
- **Deliverables:** first revenue; whitepaper in review.

### 1.3 Days 61–90 (Aug 10 – Sep 12)

**Week 9 — Retention truth.**
- WS-B: Cohort analysis. Kill or fix the features nobody touches. Ship top retention-driving request only.
- WS-C: Incorporate whitepaper feedback.

**Week 10 — Keon goes public (words, not product).**
- WS-C: Publish whitepaper + Keon site v1 (spec, manifesto, "demo coming" — see §5.1). Publish essay #1 staking "Execution Assurance" language.
- WS-B: Maintenance + presale push to waitlist.

**Week 11 — The embed.**
- WS-C→B: Receipts/provenance primitives running inside BioStack's evidence pipeline (first real Keon code, running in production from day one). Every evidence grade now carries inspectable provenance.

**Week 12 — 90-day review.**
- All: Score against kill criteria (strategy §5). Decide BioStack public-launch timing and whether Keon reference implementation starts in Q2 as planned.
- **Deliverables:** written 90-day review; Q2 plan confirmed or amended.

### 1.4 What to cut if time gets tight (in order)
1. Keon essay cadence (keep the whitepaper, drop the blog).
2. Week 11 receipts-in-BioStack (slips to Q2 — painful but survivable).
3. Beta wave 2 size (75 → 40 users; signal quality > volume).
4. Evidence corpus breadth (50 → 35 compounds; depth per compound is non-negotiable).
5. **Never cut:** clearance work, scope freeze, activation/retention instrumentation, disclaimers/legal pages, evidence citation quality.

### 1.5 Hard gates before anything public or commercial
- [ ] Written employer clearance (or documented counsel opinion that activity is outside any IP/moonlighting restriction)
- [ ] Entity formed; finances, infra, accounts, and hardware fully separated
- [ ] Terms of Service, Privacy Policy, and wellness disclaimer live and conspicuous
- [ ] Evidence-grading methodology page published (the FTC-substantiation backbone)
- [ ] Every marketing claim passed through the §6.4 claims filter
- [ ] No employer code, patterns, documents, prompts, or materials anywhere in either codebase — verified by you, in writing, dated

---

## 2. BioStack Private Beta Scope

### 2.1 MVP feature list (the whole list)
1. Email/passkey auth
2. Single protocol portal (one active protocol per user)
3. Supplement + dose logging (fast entry; <10 seconds per log)
4. Weekly schedule view
5. **Evidence-graded compound library** — 50 compounds, 7-tier labels: Established / Plausible Theory / Early Evidence / Anecdotal Signal / Contradicted / Unsupported / Bro-Science
6. Tap-through citations on every grade (source, year, study type, plain-language summary)
7. **The Stack Report** — paste/enter your current stack, get it graded (the activation moment)
8. Evidence changelog — "what changed this week in the evidence behind your stack" (weekly digest)
9. Contradiction surfacing — when sources conflict, show both, say so plainly
10. Milestones (simple streaks/adherence)
11. Honest empty states everywhere
12. CSV export (data ownership from day one)

### 2.2 Explicitly excluded from beta
- Diet guidance engine · wearable/monitoring integrations · care-team messaging · practitioner tier · native mobile apps (responsive PWA only) · AI chat assistant · **hypothesis generation / personalized decision support** (regulatory-hot; deferred until post-beta with counsel review) · multi-protocol support · social/community features · payments beyond the presale page

If a beta user asks for any of these: "It's on the list — what would it change for you?" Log the answer. Build nothing.

### 2.3 First evidence-intelligence vertical
**Longevity & healthspan supplement stacks (OTC only).** NAD+ precursors, creatine, omega-3s, magnesium forms, berberine, glycine/NAC, etc. Prescription-adjacent compounds (rapamycin, metformin, peptides) are *covered editorially with evidence grades but never protocol-supported* in v1 — grade the discourse, don't structure the usage.

Why this vertical: highest bro-science density on the internet (the grading shines), motivated spenders ($75–300/mo on supplements), concentrated reachable communities, and an evidence spectrum wide enough to make the tier system visibly honest — creatine grades Established while NMN grades Early/Contested, and that contrast *is* the demo.

### 2.4 Beta user profile
30–55, spends $75+/month on supplements, active in longevity communities (forums, subreddits, Discords, podcast audiences), currently tracks in a spreadsheet or Notes app, has been burned by at least one hyped compound. Recruit by direct outreach with a personal ask, not broadcast posts.

### 2.5 Activation moment
**The Stack Report, first session.** User enters their current stack → sees every compound graded with citations → at least one grade surprises them. Target: <7 minutes from signup to graded stack. Activation metric: % of signups who grade ≥5 compounds in session one. If this doesn't make people screenshot it and post it, the product thesis needs work.

### 2.6 Retention loop
Weekly evidence digest (email + in-app): *your* adherence + *what changed in the evidence behind your specific stack* + one contradiction or grade-change spotlight. The loop is "the evidence moves, and this is the only place tracking it against MY stack." Retention metric: % of activated users who open the digest or return weekly at week 4.

### 2.7 Free-to-paid conversion trigger
Free: full logging, 10 evidence lookups/month, current grades only. Paid unlocks: unlimited lookups, evidence changelog history, contradiction alerts, personal response notes, export of evidence reports. **Trigger:** hitting the lookup cap or wanting the changelog history — i.e., the moment evidence intelligence becomes a habit. Tracking never paywalled (trust), intelligence is the product (revenue).

### 2.8 Founding Member offer
$99/year, capped at 100 members. Promise: full paid tier at launch, price locked for life, founding badge, direct roadmap input (monthly founder note + vote). Framing is explicitly a presale: "You're funding the build of the evidence engine. Here's exactly what exists today and what doesn't." Refund window: 60 days, no questions. Honest empty states apply to the offer too.

---

## 3. Keon Public Proof Package

### 3.1 Whitepaper outline — *"Execution Assurance for Agentic AI: Receipts Over Narratives"*
1. **The accountability gap** — agents are blocked from production because logs are narratives, not evidence; observability informs, it cannot testify
2. **Definitions** — governed execution, execution receipt, evidentiary memory, execution provenance, policy binding (precise enough that "we do that too" becomes checkable)
3. **The challenge model** — who challenges an AI execution (auditor, regulator, counsel, customer, incident review) and what a survivable answer requires
4. **Proof primitives** — signed, hash-chained execution receipts; memory provenance records; policy bindings; **Epoch Roots** (anchored commitments to memory + execution state over time); deterministic replay
5. **Gravitational Recall** — retrieval weighted by evidentiary relevance, temporal context, and trust weight (concepts and properties only — see §3.6)
6. **Trust-boundary architecture** — Runtime / Cortex / Collective / Control / MCP Gateway separation, and why the control surface must not pretend to be the governance engine
7. **Verification & replay protocol** — how a third party verifies receipts *without trusting Keon*
8. **Regulatory mapping** — receipts → NIST AI RMF, ISO/IEC 42001, EU AI Act record-keeping & traceability obligations
9. **Case study** — BioStack: evidence-graded AI in a high-distrust domain (added in Q2/Q3 when live)
10. **What Keon is not** — not an agent framework, not orchestration, not observability; frameworks are upstream integrations

Target: 12–18 pages, technical-credible, zero marketing fluff. The spec sections should be implementable by a stranger.

### 3.2 Public demo concept
A hosted page showing one completed agent execution (a deliberately mundane workflow — e.g., a document-triage decision). Three panes: the decision, the receipt chain, the verifier. Visitor needs zero setup.

### 3.3 "Challenge This Execution" demo flow
1. **Pick a decision** the agent made
2. **See what it knew** — memory provenance: which records influenced this step, with trust weights and timestamps
3. **See what bound it** — the policy in force, and the binding proof
4. **Replay it** — re-execute deterministically; verification passes, green across the chain
5. **Tamper with it** — visitor edits a memory record or a log line in-browser
6. **Watch it fail** — replay/verification fails loudly, pinpointing exactly what was altered
7. Closing line on screen: *"Your logs can't do step 6. That's the difference between a narrative and a receipt."*

Step 6 is the entire sales argument made unanswerable. Everything else is decoration.

### 3.4 Audit-pack sample outline
1. Executive summary (1 page, written for a risk officer)
2. System description & trust-boundary diagram
3. Execution inventory for the audit period
4. Sample receipts + step-by-step independent verification instructions (verifier CLI)
5. Memory provenance report
6. Policy conformance report (policies in force, bindings, exceptions)
7. Replay attestation
8. Framework mapping table (NIST AI RMF / ISO 42001 / EU AI Act articles)
9. Limitations & exceptions — stated plainly; an audit pack that admits nothing is not credible

### 3.5 Minimum proof primitives before Keon is sold (any of it, to anyone)
- [ ] Receipt schema published + open-source **verifier CLI** (third party verifies without trusting Keon — this is non-negotiable)
- [ ] Deterministic replay working for at least one workflow class
- [ ] Memory provenance on reads (what was retrieved, why, with what weight)
- [ ] Epoch Root anchoring implemented (even if the anchor target is simple in v1)
- [ ] Tamper-detection demo live and public
- [ ] Running in production inside BioStack

No pilots, no audits-as-a-service under the Keon name, until all six boxes check.

### 3.6 What remains private
Gravitational Recall scoring algorithms and weights · Epoch Root internal construction beyond the verification interface · implementation internals and stack choices · the product roadmap · BioStack coupling internals · anything resembling patterns from your day job (see §6.1 — when in doubt, it's private, and probably shouldn't exist in the codebase at all).

Publish interfaces and verification protocols; keep engines proprietary. The spec creates the category; the implementation is the moat.

---

## 4. First Revenue Assets (One-Pagers)

### 4.1 BioStack Founding Member Annual — $99/year (cap: 100)
- **Buyer:** Serious supplement/protocol users in longevity communities; spreadsheet-trackers tired of guessing which claims are real
- **Problem:** The supplement world is a fog of conflicting studies, influencer hype, and bro-science — and no tool separates what's proven from what's noise *for your specific stack*
- **Promise:** Every compound in your stack, graded across seven evidence tiers, cited, and tracked as the evidence changes — plus your own response data alongside it
- **Deliverables:** Full paid tier at launch · lifetime price lock · founding badge · monthly founder note with roadmap vote · 60-day no-questions refund
- **Price:** $99/year (will be $119+/yr at public launch)
- **Timeline:** Beta access now; paid features land across Q3–Q4 2026 (stated explicitly — this is a presale)
- **Proof required before selling:** Working Stack Report with 50 graded, cited compounds; published grading methodology; live disclaimers/ToS

### 4.2 Agentic AI Execution Audit — $9,500 fixed
- **Buyer:** Head of AI Platform / CAIO / VP Eng at a regulated or risk-sensitive company with agents stuck between "demo" and "approved for production"
- **Problem:** Risk and audit won't sign off on agent deployments, and nobody can articulate what "audit-ready" even means for agentic systems
- **Promise:** In three weeks, a severity-ranked assessment of whether your agent deployment could survive a challenge — provenance gaps, replayability gaps, policy-binding gaps — plus a remediation roadmap your risk team will accept
- **Deliverables:** Architecture & evidence review · gap analysis against the Execution Assurance model · severity-ranked findings report · remediation roadmap · 90-minute readout with your risk stakeholders
- **Price:** $9,500 fixed scope; one workflow/system
- **Timeline:** 2–3 weeks, remote
- **Proof required before selling:** Published whitepaper · documented assessment methodology · **written employer clearance (hard gate — this offer is closest to your day job)** · entity + basic contract/MSA + professional liability insurance

### 4.3 Keon Execution Assurance Pilot — $25,000 fixed, 12 weeks
- **Buyer:** Same persona as 4.2, one step further: they have a specific blocked workflow and budget to unblock it
- **Problem:** "Get this agent approved" has no playbook; logging and dashboards aren't satisfying audit, and rebuilding on a new framework is a non-starter
- **Promise:** Your existing workflow, instrumented with signed receipts, memory provenance, policy bindings, and deterministic replay — delivered as an audit pack your risk team can file, without rebuilding your stack
- **Deliverables:** Receipt instrumentation on one workflow · verifier handoff (their team verifies independently) · replay capability · complete audit pack (§3.4) mapped to NIST AI RMF / EU AI Act record-keeping · two readouts (engineering + risk)
- **Price:** $25,000 fixed; success criteria written into the SOW
- **Timeline:** 12 weeks; sold no earlier than Q3 (Jan 2027), gated on §3.5
- **Proof required before selling:** All six §3.5 primitives · BioStack case study live · the public tamper demo · at least three audit engagements (4.2) completed as pipeline and credibility

---

## 5. Homepage Messaging Hierarchy

### 5.1 Keon — above the fold
> **Prove what your AI did. Don't just log it.**
> Keon is execution assurance infrastructure for agentic AI: signed receipts, verifiable memory, policy-bound execution, and deterministic replay — so your AI systems can survive an audit, not just a demo.
> [Read the whitepaper] [Challenge a live execution]

Secondary line: *"Logs are narratives. Receipts are evidence. Your auditors know the difference."*

**Keon must NOT say:** "agent framework" · "build agents faster" · "observability platform" · "AI safety" (vague, wrong fight) · "powered by [any model vendor]" · anything about BioStack above the fold · "the first/only" claims you can't substantiate · any hint of your employer's domain language

**Three trust-building proof sections:**
1. **The live challenge demo** — tamper-and-fail, embedded on the homepage (§3.3)
2. **The spec** — whitepaper + receipt schema + open verifier CLI ("verify us without trusting us")
3. **Proven in a hostile domain** — the BioStack case study + regulatory mapping table (added Q2/Q3)

### 5.2 BioStack — above the fold
> **Know what's proven. Track what works for you.**
> BioStack grades every supplement and protocol claim across seven evidence tiers — established science to bro-science — with citations you can check, updates when the evidence moves, and your own response data alongside it.
> [Grade my stack — free]

Secondary line: *"Not medical advice. Better than guessing."*

**BioStack must NOT say:** treat / cure / prevent / diagnose · "AI health advisor" or "AI doctor" · "clinically proven results" · dosing recommendations as advice · outcome promises ("optimize your longevity") · any Keon/enterprise jargon ("governed execution" means nothing to this buyer) · "doctor-approved" or credential-borrowing

**Three trust-building proof sections:**
1. **The grading methodology page** — how tiers are assigned, what counts as evidence, who decides, how grades change (this doubles as FTC substantiation discipline)
2. **A live graded compound** — full public example (creatine vs. NMN side by side: Established vs. Early/Contested) showing citations and contradictions handled honestly
3. **The trust commitments** — honest empty states, no fake data, your data exports anytime, provenance on every claim ("tap any grade to see why")

---

## 6. Risk and Boundary Checklist

### 6.1 Employer/IP separation (existential — do first)
- [ ] Written moonlighting/side-project clearance requested Day 1; received before any commercial or public activity
- [ ] Review employment agreement IP-assignment clause with employment counsel (Georgia law; check scope of "related to employer's business" language — your domain overlap makes this the single biggest risk in the plan)
- [ ] Personal hardware, personal accounts, personal AI subscriptions, personal cloud — zero employer resources, ever, including incidental use
- [ ] Work only on personal time; keep a contemporaneous time log for the first 90 days (cheap insurance)
- [ ] Separate GitHub org, password manager vault, email domain, and billing
- [ ] No employer code, architecture docs, prompts, internal patterns, vendor materials, or customer information in any form — and no "reconstructing from memory" of employer-specific designs
- [ ] Document Keon's independent design provenance: dated design notes, decision records, public commits (Keon should be able to produce receipts about its own origins)
- [ ] No soliciting employer customers, partners, or colleagues
- [ ] If clearance is denied or restricted: stop Keon commercial plans; BioStack proceeds only if clearly outside any restriction; get counsel before improvising

### 6.2 BioStack wellness/compliance boundary
- [ ] Conspicuous wellness/education disclaimer; ToS + Privacy Policy live before first external user
- [ ] No diagnosis, treatment, prevention, or cure language anywhere — product, marketing, emails, social
- [ ] No dosing recommendations; present "commonly studied ranges" with citations; decisions stay with the user/practitioner
- [ ] Every marketing claim substantiated per the published methodology (FTC health-claim discipline — the nearer threat than FDA)
- [ ] Information-plus-provenance, human-decides design pattern everywhere (keeps clear of the FDA device/CDS line)
- [ ] Hypothesis-generation/decision-support features: counsel review before shipping, full stop
- [ ] Prescription compounds: editorial coverage with grades only; no protocol scaffolding in v1
- [ ] No sale or sharing of health data; minimal collection; export always available
- [ ] HIPAA posture documented as deliberately out of scope (consumer wellness, non-covered entity); revisit only with a clinical practitioner tier
- [ ] Refund policy honored fast and without friction (trust product = trust operations)

### 6.3 Keon enterprise credibility
- [ ] Entity, MSA/SOW templates, professional liability + cyber insurance before any paid engagement
- [ ] All six §3.5 proof primitives complete before the Keon name is sold
- [ ] Open verifier CLI published — "verify without trusting us" is the credibility cheat code for a solo vendor
- [ ] Key-person risk mitigated by design: fixed-scope, deliverable-based engagements; everything handed over verifiable
- [ ] Security posture one-pager honest about company stage; SOC 2 stated as roadmap, not implied as present
- [ ] Never claim production deployments, team size, or customers that don't exist — one inflated claim destroys a trust company permanently

### 6.4 Claims filter (run every public sentence through this)
Avoid: treat/cure/prevent/diagnose · "guaranteed" anything · "the only/first" without proof · "enterprise-grade" before an enterprise runs it · "audit-proof" (nothing is; say "audit-ready" or "challenge-survivable") · "compliant with EU AI Act/NIST" (say "maps to" / "supports") · customer logos without written permission · numbers you can't reproduce on demand

### 6.5 Public launch readiness criteria (both brands)
- [ ] All §1.5 hard gates green
- [ ] Activation + retention instrumentation live and verified
- [ ] Support channel exists and is answered (even if it's just you)
- [ ] Refund mechanics tested end-to-end
- [ ] A "what we don't do" page published per brand (boundary statements are trust assets)
- [ ] You can answer "who's behind this?" honestly and comfortably in public

---

## 7. Final Founder Recommendation

### Do this week (Jun 15–21)
1. **Send the clearance request. Monday. In writing.** Everything commercial routes through this gate, and the request itself is cheap. (2 hours)
2. **Separate everything** — entity confirmed, accounts, hardware, GitHub org, subscriptions. (4 hours)
3. **Lock the vertical and freeze the beta scope** — write the one-page scope doc, date it, sign it. (2 hours)
4. **Start the evidence corpus** — grade the first 10 compounds with citations. This is the slowest, most valuable asset in the whole plan; start the flywheel now. (6+ hours)

### Defer (on purpose, with dates)
Keon reference implementation (Day 61+) · receipts-in-BioStack (Week 11) · all enterprise outreach (Q3 2027 plan, i.e., Jan 2027) · practitioner tier (Q3) · BioStack public launch (post-Day-90 review) · essay cadence (after whitepaper ships)

### Kill (not defer — kill)
- **AI chat assistant in BioStack v1** — undifferentiated, regulatory-adjacent, and a time sink; the evidence engine is the product
- **Hypothesis-generation features pre-counsel-review** — the upside doesn't justify shipping into FDA-adjacent fog with no legal budget
- **Any Keon "platform" or self-serve ambitions through 2027** — sales-led pilots only
- **Native mobile apps** — PWA until revenue argues otherwise
- **More than one evidence vertical** — depth in longevity stacks or nothing

### The single most important metric for the next 90 days
**Week-4 evidence-engagement retention: the percentage of activated beta users who return in week 4 AND interact with evidence intelligence (grades, changelog, contradictions) — target ≥40%.**

Not signups, not waitlist size, not presale dollars. This one number tests the entire strategic thesis: that *evidence intelligence* — not tracking — retains. If it holds, BioStack monetizes, the Keon case study is real, and the whole 2026–27 sequence stands. If it doesn't, you've falsified the core assumption in 60 days for almost no money — which is exactly what a proof-obsessed company should want to know first.

Receipts over narratives. Including your own.

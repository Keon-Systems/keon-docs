# ADR-0006: BrowseAhead is a cognition-side inspection sensor with zero execution authority

## Status

Accepted (pre-canon design). Implementation of the MVP tool is authorized; canon lock pending review.

## Date

2026-06-21

## Deciders

AI security architecture review; Keon canon reviewer.

---

## Context

Agentic browsing exposes Keon-governed agents to **indirect / remote prompt injection**: adversary instructions embedded in external web content (rendered text, hidden DOM, CSS-invisible spans, HTML comments, metadata, markdown, obfuscated/encoded payloads) that an LLM-integrated agent retrieves and then *obeys* as if the page were a privileged speaker. OWASP catalogs this as remote/indirect prompt injection; NIST describes it as adversarial prompts injected into data likely to be retrieved by LLM applications; 2026 web-scale studies validated injection payloads across thousands of pages, frequently in non-rendered surfaces.

Keon canon separates **cognition from consequence**: internal cognition may explore, but external effects must be explicitly requested, policy-evaluated, cryptographically receipted, causally linked, operator-visible, and non-bypassable. The **MCP Gateway** is the lawful execution boundary (identity, tenant/actor fail-closed binding, scopes, Decide-before-Execute, durable ingress-spine receipts, governed envelope preserved in `structuredContent`). The **Runtime** owns Decide/Execute. The **Collective** (cognition plane) has no direct path to external effect across the Reality boundary.

We need a defense for browsed content **without** creating a new execution path or a governance bypass. The central design question: *is BrowseAhead an enforcer that can allow/deny agent actions, or a sensor that produces evidence?*

## Decision

**BrowseAhead is a cognition-side inspection sensor with ZERO execution authority.**

1. **It produces evidence, not verdicts.** Outputs: redacted findings, deterministic risk score, quarantine records, sanitized (data-only) content bundle, evidence-bundle hash, and a signed risk receipt. It emits no `decision`/`execution` spine receipt and never calls execute.
2. **It is reachable only as a governed Gateway tool** — `keon.browseahead.scan.v1` — inheriting identity, tenant/actor fail-closed binding, scopes, and ingress-spine receipts. The scan is a **non-mutating governed read** and requires **no Runtime Decide** (mirrors `keon.cortex.receipt.get.v1`).
3. **All effect-bound actions still flow through MCP Gateway → Runtime Decide → Execute.** The BrowseAhead risk receipt + `PolicySignalRef` are passed as *policy-relevant evidence* to Decide; downstream execution receipts must **cite** the BrowseAhead receipt when web content influenced the action, causally linking page-influence into the spine.
4. **Raw web content never becomes instruction authority.** The agent may ingest only the sanitized, data-only bundle; detected instructions are quoted/escaped as untrusted data, never re-emitted as executable framing.
5. **Agent-facing / forensic output split (correction, 2026-06-21).** Raw hostile excerpts (`raw_excerpt`, `decoded_excerpt`, verbatim attacker bytes) **must never** appear in agent-facing `structuredContent`. Agent-facing findings are **redacted summaries only** (`redacted_summary` + `excerpt_fingerprint` + length + decode-chain metadata). Verbatim payloads exist solely in operator-only quarantine/evidence storage and Control views, joined by `forensic_ref`/`excerpt_fingerprint`. Returning the raw payload to the agent would re-introduce the very injection BrowseAhead exists to neutralize.
6. **Fail-closed at high/critical.** `critical` risk blocks ingestion (`sanitized_bundle = null`) absent a governed, recorded override path.
7. **Determinism.** Same input bytes + same `policy_version` ⇒ same score/findings/receipt body. An optional advisory LLM classifier may annotate/escalate but can never be the sole basis for a block/allow outcome.

## Options Considered

### Option A — Cognition-side inspection sensor, no execution authority (Chosen)
Evidence producer behind the gateway; Runtime retains all authority. Aligns with cognition/consequence separation, non-bypassability, and the single lawful execution boundary. No new effect path.

### Option B — Inline enforcing firewall that allows/denies agent actions
BrowseAhead decides whether the agent may act on a page. **Rejected:** creates a second execution authority parallel to Runtime, violates "no direct cognition→effect path," and duplicates/forks Decide. The "firewall" framing is acceptable as *marketing metaphor only*, never as a contract.

### Option C — Client-side library the agent calls directly (outside the gateway)
A local scrubber with no governed envelope. **Rejected:** bypasses tenant/actor binding, scopes, and ingress-spine receipts; produces no inspectable, anchored evidence; non-auditable.

## Recommendation

Adopt **Option A**. Land the design as pre-canon in `keon-docs`, add a verification-canon stub encoding the invariants as `false_claim_gates`, then implement the deterministic, no-render, no-execute MVP `keon.browseahead.scan.v1` in `keon-mcp-gateway`.

## Consequences

### Positive
- No new execution authority; cognition/consequence separation preserved.
- Page-influence becomes causally traceable in the spine (receipt citation).
- Evidence is inspectable, anchored, and operator-reviewable; raw payloads are contained.
- Deterministic scoring makes scans reproducible and receipts defensible.

### Negative / costs
- Adds a preflight hop (latency) before browsed content reaches the agent.
- Agents must be integrated to ingest *only* sanitized content — a behavioral contract, not just an API.
- Redaction split adds a second storage sink (operator-only forensic store) and Control authz surface.

### Risks
- **Redaction leak** (raw payload reaching agent envelope) — mitigated by invariant 13 + a marker-payload redaction-leak test in CI.
- **Sanitizer over-strip** (losing benign content) — mitigated by golden benign fixtures.
- **`mode:raw` provenance** (caller-supplied bytes can't be vouched for) — restrict raw mode to inspect-only; see design §"Open questions".

## Implementation Notes

- Tool: `keon.browseahead.scan.v1`, scopes `keon:mcp:invoke` + `keon:browseahead:scan`, tier-gated.
- MVP analyzers: static HTML, header/metadata, declared-style hidden-content, obfuscation, link/form, cross-origin/redirect, instruction rule pack. **Excluded:** headless render, advisory LLM (both v0.2, default-off, advisory-only).
- Gate: golden fixture pack + determinism double-run must pass before merge.

## References
- `docs/design/KEON-BROWSEAHEAD-DESIGN-v0.1.md` (this ADR's full design)
- `canon/keon_browseahead_canon_v1.json` (verification-canon stub)
- `canon/keon_mcp_gateway_canon_v1_locked.json`, `canon/keon_runtime_canon_v1_locked.json`, `canon/keon_collective_canon_v1_locked.json`

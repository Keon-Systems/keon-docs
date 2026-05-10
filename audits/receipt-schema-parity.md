# Receipt Schema Parity Report

**Date:** May 8, 2026
**Status:** AUDIT COMPLETE — CRITICAL GAPS IDENTIFIED
**Author:** Keon Systems Architecture Team
**Classification:** Internal Audit Document

---

## Executive Summary

This audit examines schema parity across three critical receipt-handling systems in the Keon monorepo:
- **ProofReceipt** client implementation (`keon-systems-web/src/proof-types.ts`)
- **EVIDENCE_PACK_SCHEMA_V1_LOCKED** canonical definition
- **MCP Gateway** envelope specification (`mcp_gateway.v1.schema.json`)

**Key Findings:**
- ProofReceipt implementation contains only 5 fields vs. 80+ canonical fields
- 60+ critical fields exist only in canon, unimplemented in client
- MCP Gateway envelope provides 7 reference fields with limited field-level coverage
- **Zero field conflicts detected** — but **severe under-implementation gaps** present
- ProofReceipt client is ~94% incomplete relative to canon specification

**Architectural Note:** The three-layer model (client / canon / gateway) is intentional by design. The gateway emits receipt IDs; the verifier resolves those IDs to the Evidence Pack to access proof material. However, several gaps go beyond intentional abstraction and represent real blockers for compliance and auditability.

**Risk Level:** HIGH — Production deployments cannot prove receipt integrity or chain-of-custody in audit or legal contexts.

---

## Source Inventory

### Source 1: ProofReceipt Client Implementation
**Path:** `keon-systems-web/src/proof-types.ts`
**Description:** TypeScript interface defining ProofReceipt for web client consumption. Contains minimal field set (5 fields) for receipt handling in browser context.
**Scope:** Client-facing type definitions only; no server schema reference.

### Source 2: Evidence Pack Schema (Canonical — LOCKED)
**Path:** `EVIDENCE_PACK_SCHEMA_V1_LOCKED.json` (canonical registry)
**Description:** Locked canonical schema defining complete receipt structure with 14 top-level sections and ~80+ nested fields. Source-of-truth specification. Immutable — ratified 2026-01-25.
**Scope:** Complete end-to-end receipt representation including provenance, chain-of-custody, cryptographic attestation, and metadata.

### Source 3: MCP Gateway Receipt Envelope
**Path:** `keon-mcp-gateway/contracts/mcp_gateway.v1.schema.json`
**Description:** Message-based receipt envelope specification. Provides 7 reference/wrapper fields for receipt transit. Designed for inter-system messaging; references only, no proof material.
**Scope:** Receipt references and gateway-level envelope structure; not field-level receipt content.

---

## Field-Level Comparison Matrix

| Field | Type | Canon | ProofReceipt | MCP Gateway | Status | Notes |
|---|---|:---:|:---:|:---:|---|---|
| **RECEIPT IDENTITY** | | | | | | |
| `receiptId` | string | ✓ | ✓ | ✓ | **MATCH** | Consistent UUID pattern across all three |
| `receiptType` | enum | ✓ | ✓ | ✓ | **MATCH** | Type classification aligned |
| `receiptVersion` | string | ✓ | ✗ | ✗ | CANON-ONLY | Version tracking absent in implementations |
| `receiptFormat` | enum | ✓ | ✗ | ✗ | CANON-ONLY | Format spec (JSON/CBOR) not captured |
| **TEMPORAL** | | | | | | |
| `timestampUtc` | timestamp | ✓ | ✓ | ✗ | MATCH | Generalized timing — present in canon + client |
| `createdAtUtc` | timestamp | ✓ | ✗ | ✗ | CANON-ONLY | Creation timestamp unimplemented |
| `issuedAtUtc` | timestamp | ✓ | ✗ | ✗ | CANON-ONLY | Issuance timestamp unimplemented |
| `expiresAtUtc` | timestamp | ✓ | ✗ | ✗ | CANON-ONLY | Expiration tracking absent |
| `validFromUtc` | timestamp | ✓ | ✗ | ✗ | CANON-ONLY | Validity window start not captured |
| **CORRELATION & REFERENCE** | | | | | | |
| `correlationId` | string | ✓ | ✓ | ✓ | **MATCH** | Cross-system tracing aligned |
| `parentReceiptId` | string | ✓ | ✗ | ✗ | CANON-ONLY | Receipt lineage not tracked in client |
| `childReceiptIds` | string[] | ✓ | ✗ | ✗ | CANON-ONLY | Receipt tree unimplemented |
| `linkedReceiptIds` | string[] | ✓ | ✗ | ✗ | CANON-ONLY | Cross-references absent |
| `transactionId` | string | ✓ | ✗ | ✗ | CANON-ONLY | Transaction binding missing |
| `sessionId` | string | ✓ | ✗ | ✗ | CANON-ONLY | Session context not preserved |
| **PROVENANCE & CHAIN-OF-CUSTODY** | | | | | | |
| `originatingSystem` | string | ✓ | ✗ | ✗ | CANON-ONLY | Source system tracking absent |
| `originatingComponent` | string | ✓ | ✗ | ✗ | CANON-ONLY | Component attribution unimplemented |
| `issuingAuthority` | string | ✓ | ✗ | ✗ | CANON-ONLY | Authority identification missing |
| `custodyChain[]` | object[] | ✓ | ✗ | ✗ | CANON-ONLY | Full custody transfer history unimplemented |
| `custodyChain[].actor` | string | ✓ | ✗ | ✗ | CANON-ONLY | — |
| `custodyChain[].action` | enum | ✓ | ✗ | ✗ | CANON-ONLY | — |
| `custodyChain[].timestamp` | timestamp | ✓ | ✗ | ✗ | CANON-ONLY | — |
| `custodyChain[].location` | string | ✓ | ✗ | ✗ | CANON-ONLY | — |
| **CRYPTOGRAPHIC ATTESTATION** | | | | | | |
| `signatureAlgorithm` | enum | ✓ | ✗ | ✗ | CANON-ONLY | Ed25519/RS256; not captured |
| `signature` | string | ✓ | ✗ | ✗ | CANON-ONLY | Cryptographic signature absent |
| `publicKeyId` | string | ✓ | ✗ | ✗ | CANON-ONLY | Key material reference missing |
| `certificateChain` | string[] | ✓ | ✗ | ✗ | CANON-ONLY | Certificate chain validation absent |
| `hashAlgorithm` | enum | ✓ | ✗ | ✗ | CANON-ONLY | SHA-256; not captured |
| `contentHash` | string | ✓ | ✗ | ✗ | CANON-ONLY | Content integrity verification unimplemented |
| **PAYLOAD & CONTENT** | | | | | | |
| `payload` | object | ✓ | ✓ | ✗ | IMPL-ONLY | Generic object in client; canon specifies typed structure |
| `payloadSchema` | string | ✓ | ✗ | ✗ | CANON-ONLY | Schema version for payload missing |
| `payloadEncoding` | enum | ✓ | ✗ | ✗ | CANON-ONLY | Encoding method not specified |
| `payloadCompression` | enum | ✓ | ✗ | ✗ | CANON-ONLY | Compression type absent |
| **METADATA & CONTEXT** | | | | | | |
| `source` | string | ✓ | ✗ | ✗ | CANON-ONLY | Data source identification missing |
| `destination` | string | ✓ | ✗ | ✗ | CANON-ONLY | Target system reference absent |
| `priority` | enum | ✓ | ✗ | ✗ | CANON-ONLY | Processing priority not captured |
| `tags` | string[] | ✓ | ✗ | ✗ | CANON-ONLY | Metadata tagging unimplemented |
| `labels` | object | ✓ | ✗ | ✗ | CANON-ONLY | Custom labeling absent |
| **INTEGRITY & VALIDATION** | | | | | | |
| `integrityChecks` | object[] | ✓ | ✗ | ✗ | CANON-ONLY | Integrity verification suite missing |
| `validationStatus` | enum | ✓ | ✗ | ✗ | CANON-ONLY | Validation state tracking absent |
| `validationErrors` | string[] | ✓ | ✗ | ✗ | CANON-ONLY | Error collection not implemented |
| `warningsEncountered` | string[] | ✓ | ✗ | ✗ | CANON-ONLY | Warning aggregation absent |
| **COMPLIANCE & AUDIT** | | | | | | |
| `auditTrail` | object[] | ✓ | ✗ | ✗ | CANON-ONLY | Audit logging infrastructure absent |
| `complianceStatus` | enum | ✓ | ✗ | ✗ | CANON-ONLY | Compliance tracking unimplemented |
| `retentionPolicy` | string | ✓ | ✗ | ✗ | CANON-ONLY | Data retention specification missing |
| `privacyClassification` | enum | ✓ | ✗ | ✗ | CANON-ONLY | Data sensitivity classification absent |
| **EXTENSION & VERSIONING** | | | | | | |
| `extensionFields` | object | ✓ | ✗ | ✗ | CANON-ONLY | Custom extension mechanism unimplemented |
| `schemaVersion` | string | ✓ | ✗ | ✗ | CANON-ONLY | Schema evolution tracking missing |
| `deprecatedFields` | string[] | ✓ | ✗ | ✗ | CANON-ONLY | Deprecation tracking absent |
| **GATEWAY ENVELOPE FIELDS** | | | | | | |
| `directive` | ref | ✗ | ✗ | ✓ | IMPL-ONLY | Gateway receipt ID map — no canon equivalent |
| `intent` | ref | ✗ | ✗ | ✓ | IMPL-ONLY | — |
| `request` | ref | ✗ | ✗ | ✓ | IMPL-ONLY | — |
| `decision` | ref | ✗ | ✗ | ✓ | IMPL-ONLY | — |
| `execution` | ref | ✗ | ✗ | ✓ | IMPL-ONLY | — |
| `outcome` | ref | ✗ | ✗ | ✓ | IMPL-ONLY | — |
| `evidence_pack` | ref | ✗ | ✗ | ✓ | IMPL-ONLY | Links to EVIDENCE_PACK_SCHEMA_V1_LOCKED |

**Summary Statistics:**

| Source | Fields | Canon Coverage |
|---|---|---|
| Evidence Pack (canon) | 80+ | 100% (locked) |
| ProofReceipt client | 5 | ~6% |
| MCP Gateway envelope | 7 | ~9% |
| Exact field matches (all three) | 3 | receiptId, receiptType, correlationId |
| Conflicts | 0 | — |

---

## Architecture Analysis: Three-Layer Model

The gaps above are partially by design. The three layers serve distinct purposes:

```
CANON (80+ fields — locked, cryptographic proof)
    │
    ├─→ ProofReceipt (5 fields — browser display metadata)
    │   Intentional gap: crypto, custody, audit trail
    │   Unintentional gap: temporal lifecycle, lineage
    │
    ├─→ MCP Gateway (7 ref fields — opaque receipt ID map)
    │   Intentional gap: no proof material in transit
    │   Unintentional gap: no envelope versioning
    │
    └─→ Resolution path: gateway refs → verifier → Evidence Pack
```

**Layer 1 — ProofReceipt (client)**
Lightweight object for in-browser state. The 5 fields cover what the UI needs to display receipt status. Crypto and custody are intentionally server-side. However, temporal fields (`createdAtUtc`, `expiresAtUtc`) and lineage (`parentReceiptId`) should be present here.

**Layer 2 — Evidence Pack (canon, locked)**
Complete cryptographic proof container. Ed25519 signatures, SHA-256 hashes, before/after state snapshots, anomaly detection, escalation chain. This is the definitive record. Immutable — do not modify; create `keon_runtime_canon_v2.json` for additions (e.g., `evidence_refs[]` per ADR-0004).

**Layer 3 — MCP Gateway envelope**
Opaque reference map: 7 named slots (`directive`, `intent`, `request`, `decision`, `execution`, `outcome`, `evidence_pack`) holding receipt IDs. The verifier resolves these IDs to the Evidence Pack. No proof material crosses the wire.

---

## Blocker Analysis

### BLOCKER-1: Client lacks verifier-facing integrity summary and validation status
**Severity:** HIGH
**Affected:** ProofReceipt client
**Missing:** `verificationStatus`, `verifiedAtUtc`, `verifierRef`, `publicKeyId`, `evidencePackRef`, `contentHash`
**Impact:** The client cannot display whether a receipt has been verified or surface a link to the authoritative verifier result. Authoritative cryptographic verification must remain server-side or verifier-side — the browser should not independently validate all cryptographic material in v1.
**What the client SHOULD display:**
- `verificationStatus` — enum: `verified` / `unverified` / `failed` / `pending`
- `verifiedAtUtc` — when the server-side verifier ran
- `verifierRef` — opaque reference to the verifier run record
- `publicKeyId` — which key was used (display only)
- `evidencePackRef` — link to the canonical Evidence Pack for deep inspection
- `contentHash` — display only; the verifier owns the check

**What the client should NOT do in v1:** independently validate Ed25519 signatures or perform hash verification. The browser is not the authority.

**Remediation:** Add `verificationStatus` and `verifierRef` fields to `ProofReceiptSummary`. Wire client to a `/api/receipts/:id/verify` endpoint that returns a summary. Effort: 2–3 days.

### BLOCKER-2: No temporal lifecycle fields
**Severity:** HIGH
**Affected:** ProofReceipt client
**Missing:** `createdAtUtc`, `issuedAtUtc`, `expiresAtUtc`, `validFromUtc`
**Impact:** Cannot enforce SLA expiration, retention policy, or temporal validation at the UI layer.
**Remediation:** Add explicit creation/expiration timestamps to ProofReceipt. Add expiration check middleware. Effort: 2–3 days.

### BLOCKER-3: No receipt lineage / relationship tracking
**Severity:** HIGH
**Affected:** ProofReceipt client, MCP Gateway
**Missing:** `parentReceiptId`, `childReceiptIds`, `transactionId`, `sessionId`
**Impact:** Multi-receipt transactions are untrackable. Debugging and reconciliation fail for complex execution chains.
**Remediation:** Add optional lineage fields to ProofReceipt. Effort: 2–3 days.

### BLOCKER-4: No chain-of-custody events in any layer
**Severity:** MEDIUM (pre-enterprise)
**Affected:** ProofReceipt client, MCP Gateway
**Missing:** `custodyChain[]` with actor / action / timestamp / location
**Impact:** Cannot satisfy regulated-industry audit requirements. Provenance is server-only.
**Remediation:** Extend ProofReceipt with immutable custody event array. Append-only. Effort: 5–7 days.

### BLOCKER-5: No audit trail infrastructure
**Severity:** MEDIUM (pre-enterprise)
**Affected:** ProofReceipt client
**Missing:** `auditTrail[]`, `complianceStatus`, `retentionPolicy`
**Impact:** Regulatory non-compliance for audit export. All audit state is locked in the Evidence Pack with no accessible summary.
**Remediation:** Server-side audit log with retrieval API. Effort: 8–10 days.

---

## Recommended ProofReceiptSummary Shape

ProofReceipt should not grow toward the 80-field canon. It is a **projection / view model** — a client-facing summary, not a replication of the Evidence Pack. The recommended shape:

```typescript
/**
 * ProofReceiptSummary — client projection of a receipt.
 * Intentionally lightweight. Authoritative proof remains in the Evidence Pack.
 */
type ProofReceiptSummary = {
  // Identity
  receiptId: string
  receiptType: string
  correlationId: string
  tenantId: string
  actorId: string

  // Temporal lifecycle
  createdAtUtc: string
  issuedAtUtc: string
  expiresAtUtc?: string

  // Lineage (optional — present for child receipts in a transaction chain)
  parentReceiptId?: string
  transactionId?: string
  sessionId?: string

  // Verification summary (server/verifier populates these; client displays only)
  verificationStatus: 'verified' | 'unverified' | 'failed' | 'pending'
  verifiedAtUtc?: string
  verifierRef?: string
  publicKeyId?: string
  contentHash?: string

  // Canon references (links, not inlined proof material)
  evidencePackRef?: string
  spineRef?: string
  policyHash?: string

  // Evidence references summary (typed list; not inlined content)
  evidenceRefs?: EvidenceRef[]
}
```

**What this is not:** a replication of `EVIDENCE_PACK_SCHEMA_V1_LOCKED`. The Evidence Pack fields (custody chain, full crypto material, anomaly detection, compliance metadata) stay server-side. The client links to them via `evidencePackRef`.

---

## EvidenceRef Shape

The canonical name for evidence attachments in protocol and storage is `evidence_refs[]`, not `artifacts[]`. UI layers may label them "Artifacts" or "Supporting Evidence," but schema, API, and ADR language must use `evidence_refs`.

```typescript
type EvidenceRef = {
  ref_id: string
  kind:
    | 'document'
    | 'screenshot'
    | 'recording'
    | 'video'
    | 'log'
    | 'trace'
    | 'ticket'
    | 'pull_request'
    | 'commit'
    | 'external_url'
    | 'other'
  uri?: string
  title?: string
  description?: string
  source_system?: string
  content_hash?: string
  hash_algorithm?: 'sha256'
  captured_at_utc?: string
  visibility?: 'public' | 'tenant' | 'internal' | 'restricted'
  retention_policy_ref?: string
}
```

**Naming rationale:**
- `artifacts[]` is broad and product/UI-oriented — easily confused with CI build artifacts or other system outputs
- `evidence_refs[]` is narrower, audit-friendly, and semantically aligned with CAES/proof language
- A future ADR (ADR-0004 or ADR-0006) should ratify this shape as the canonical decision record

---

## Recommended Actions

### CRITICAL — Execute within this sprint

**CA-001: Migrate ProofReceipt → ProofReceiptSummary with verifier integration**
- Replace `ProofReceipt` in `proof-types.ts` with `ProofReceiptSummary` shape (see above)
- Add `verificationStatus`, `verifiedAtUtc`, `verifierRef`, `publicKeyId`, `evidencePackRef`, `contentHash`
- Implement `/api/receipts/:id/verify` endpoint returning server-side verification summary
- Client displays status only — no independent crypto validation
- Effort: 2–3 days | Blocker: BLOCKER-1

**CA-002: Add temporal lifecycle + lineage fields to ProofReceiptSummary**
- Add `createdAtUtc`, `issuedAtUtc`, `expiresAtUtc` (mandatory/optional per above shape)
- Add optional `parentReceiptId`, `transactionId`, `sessionId`
- Add expiration check in client (show expired badge if `expiresAtUtc` < now)
- Effort: 1 day | Blockers: BLOCKER-2, BLOCKER-3

**CA-003: Document the three-layer receipt model**
- Add `keon-mcp-gateway/contracts/README.md` explaining resolution path
- Prevents the next engineer from treating the intentional gap as a bug
- Effort: 0.5 days

**CA-004: Write ADR for `evidence_refs[]` shape**
- Ratify `EvidenceRef` type (see above) as canonical decision record
- File as ADR-0004 or ADR-0006 (check existing ADR numbering)
- All future references to evidence attachments use `evidence_refs[]`, not `artifacts[]`
- Effort: 0.5 days

### HIGH — Execute within 2 sprints

**CA-005: Extend MCP Gateway envelope with versioning**
- Add `envelopeVersion` field to `mcp_gateway.v1.schema.json`
- Enables schema evolution without breaking consumers
- Effort: 1 day

**CA-006: Wire `evidenceRefs` summary into ProofReceiptSummary**
- Once `evidence_refs[]` ADR is ratified, add typed `evidenceRefs?: EvidenceRef[]` to summary shape
- Effort: 1 day | Depends on CA-004

### MEDIUM — Post-launch / enterprise tier

**CA-007: Chain-of-custody event logging** — Effort: 5–7 days | Blocker: BLOCKER-4
**CA-008: Audit trail infrastructure** — Effort: 8–10 days | Blocker: BLOCKER-5
**CA-009: Payload schema versioning** — Effort: 3–5 days
**CA-010: Compliance status tracking** — Effort: 4–6 days

### DEFERRED — Do not implement in browser v1

- Client-side Ed25519 signature verification
- Full `custodyChain[]` display
- Complete `auditTrail[]` in client
- Enterprise retention / legal hold UI (unless required for demo)

---

## Appendix: Source File Paths

| Source | Path |
|---|---|
| ProofReceipt type | `keon-systems-web/src/proof-types.ts` |
| Evidence Pack canon | `EVIDENCE_PACK_SCHEMA_V1_LOCKED.json` |
| MCP Gateway schema | `keon-mcp-gateway/contracts/mcp_gateway.v1.schema.json` |
| Canon v2 (planned) | `keon-docs/canon/keon_runtime_canon_v2.json` |
| evidence_refs ADR | `keon-docs/adr/ADR-0004-evidence-refs-schema.md` |

---

*End of Report*

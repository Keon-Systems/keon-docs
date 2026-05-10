# ADR-0005: Tenant Data Model

## Status
Proposed

## Date
2026-05-08

## Deciders
Clint Morgan (@morganclint76)

---

## Context

### Current State

The Keon Systems monorepo is building a governed execution platform with multi-tenant isolation requirements. The current codebase has activation flows, state machines, and receipt schemas, but **no persistent tenant/user/membership data model**.

**What exists today:**

**Frontend (`keon-systems-web`):**
- Access request flow (`/get-access`) collects: name, email, organization, audience (`builder` / `architect` / `operator` / `enterprise`), and intended use
- Two activation modes: `test` (internal sandbox) and `invite` (prepared workspace)
- Activation tokens enable access to `/activate?token=X`
- Email notifications sent to requestor and admin (hardcoded `morganclint76@gmail.com`)
- No persistent storage of access requests; flow is email-only

**API types (`keon-systems`):**
- Basic `Tenant` interface: `{ id, name, status: 'active' | 'inactive' | 'suspended', createdAt }`
- `ReceiptEnvelope` includes `tenantId` on all receipt types
- `ExecutionLink`, `CorrelationSummary`, `LegalHold` all reference `tenantId` for isolation

**Provisioning state machine (`keon.control.website`):**
- 7-state activation state machine: `invite_validating` → `tenant_resolving` → `tenant_creating` → `membership_binding` → `workspace_bootstrapping` → `provisioning_complete` / `provisioning_failed`
- Stores activation context: `mode`, `source`, `tenantId`, `tenantName`, `workspaceId`, `workspaceName`, `environment`
- In-process `Map`-backed state; no persistence

**Receipt persistence (from ADR-0004):**
- All receipts include `TenantId` as primary isolation key
- Tables include `TenantId` in composite primary keys: `(TenantId, CorrelationId)`
- Schema uses `TEXT` types for IDs; numeric tenant IDs not supported

### What Is Missing

1. No persistent access request storage — requests flow email-only; no database record
2. No invite/membership model — activation is token-only; no `user ↔ tenant` binding
3. No workspace/environment schema — provisioning mentions `workspaceId` but no definition
4. No retention policy binding — retention records reference tenants but no relationship table
5. No authentication/authorization model — stubs exist but no backing data
6. No tenant lifecycle management — no create/update/suspend/delete endpoints
7. No multi-user support — single activation per tenant assumed; no members table

### Problem This ADR Solves

Keon needs a durable, multi-tenant data model to support:

- **Provisioning workflows** that persist access requests, track approval state, and link to created tenants
- **User identity** that persists authenticated users and their role within each tenant
- **Workspace isolation** that binds users, tenants, and resources into coherent operational units
- **Invite lifecycle** that allows existing tenant admins to invite new users with email validation
- **Audit trail** that records who provisioned what, when, and with what approval chain
- **Enterprise features** (future) like SSO, team hierarchies, and advanced access controls

---

## Decision

### Core Schema (Prisma + PostgreSQL)

```prisma
// ─────────────────────────────────────────────────────────────────────────────
// ACCESS REQUESTS — Pre-tenant form submissions (not yet a tenant)
// A submitted request is not a tenant. Keeping these separate avoids polluting
// the tenant table with unreviewed submissions and blurring approval semantics.
// ─────────────────────────────────────────────────────────────────────────────

model AccessRequest {
  id           String              @id @default(cuid())
  email        String
  name         String?
  organization String?
  audience     AccessAudience      @default(BUILDER)
  intendedUse  String?
  status       AccessRequestStatus @default(RECEIVED)

  // Review trail
  reviewedByEmail String?
  reviewedAt      DateTime?
  decisionReason  String?

  // Temporal
  createdAt    DateTime @default(now())
  updatedAt    DateTime @updatedAt

  // Link to resulting tenant (null until PROVISIONED)
  resultingTenantId String? @unique
  resultingTenant   Tenant? @relation("AccessRequestToTenant", fields: [resultingTenantId], references: [id])

  @@index([email])
  @@index([status])
  @@index([createdAt])
}

enum AccessRequestStatus {
  RECEIVED     // Form submitted; not yet reviewed
  REVIEWING    // Under active review
  APPROVED     // Approved; provisioning queued
  REJECTED     // Declined; decisionReason set
  PROVISIONED  // Tenant workspace created and linked
}

enum AccessAudience {
  BUILDER
  ARCHITECT
  OPERATOR
  ENTERPRISE
}

// ─────────────────────────────────────────────────────────────────────────────
// TENANTS — Governed execution boundary & billing unit
// Tenant links back to its originating AccessRequest rather than duplicating
// request fields. Tenant.name and Tenant.externalId are the only required
// identity fields; everything else lives in AccessRequest.
// ─────────────────────────────────────────────────────────────────────────────

model Tenant {
  id         String       @id @default(cuid())
  externalId String       @unique  // Slug-like, globally unique
  name       String
  status     TenantStatus @default(PROVISIONING)

  // Temporal
  createdAt     DateTime @default(now())
  updatedAt     DateTime @updatedAt
  provisionedAt DateTime?
  suspendedAt   DateTime?

  // Extensible config (absorbs tenant-specific settings without schema churn)
  metadata Json @default("{}")

  // Back-reference to originating access request (null for system-created tenants)
  accessRequest AccessRequest? @relation("AccessRequestToTenant")

  // Relations
  workspaces        Workspace[]
  memberships       TenantMembership[]
  invitations       InviteToken[]
  provisioningRuns  ProvisioningRun[]
  retentionPolicies RetentionPolicy[]
  legalHolds        LegalHold[]

  @@index([status])
  @@index([createdAt])
}

enum TenantStatus {
  PROVISIONING    // Tenant record created; workspace not yet ready
  PROVISIONED     // Workspace ready; awaiting first activation
  ACTIVE          // User activated; workspace live
  SUSPENDED       // Admin suspended (legal hold, abuse)
  DEPROVISIONING  // Scheduled for deletion
  DEPROVISIONED   // Soft-deleted
}

// ─────────────────────────────────────────────────────────────────────────────
// WORKSPACES — Operational unit within a tenant (future: multi-workspace)
//
// ENVIRONMENT DISTINCTION (enforced by business logic, not just enum):
//   SANDBOX  — visibly labeled in all UI surfaces; must never claim live status
//   PRODUCTION — must never be activated via a TEST token
//   DEMO_FIXTURE — must carry visible label; must never appear as live production
//
// Provisioning complete ≠ onboarding complete. onboardingStatus tracks the
// post-provisioning setup flow separately.
// ─────────────────────────────────────────────────────────────────────────────

model Workspace {
  id          String               @id @default(cuid())
  externalId  String               // Unique within tenant
  name        String
  environment WorkspaceEnvironment @default(SANDBOX)

  // Onboarding (provisioning complete does not mean onboarding complete)
  onboardingStatus      WorkspaceOnboardingStatus @default(NOT_STARTED)
  onboardingCompletedAt DateTime?
  onboardingVersion     String?   // Version of the onboarding flow completed
  onboardingState       Json      @default("{}")  // Checkpoint state for resumable flows

  createdAt   DateTime @default(now())
  updatedAt   DateTime @updatedAt

  tenantId    String
  tenant      Tenant   @relation(fields: [tenantId], references: [id], onDelete: Cascade)

  @@unique([tenantId, externalId])
  @@index([tenantId])
  @@index([onboardingStatus])
}

enum WorkspaceEnvironment {
  SANDBOX       // Test/trial — must be visibly labeled
  PRODUCTION    // Live governed execution — must not use test tokens
  DEMO_FIXTURE  // Fixture demo — must be visibly labeled; never claim live status
}

enum WorkspaceOnboardingStatus {
  NOT_STARTED
  IN_PROGRESS
  COMPLETED
  SKIPPED  // Admin bypassed onboarding (internal/dev tenants only)
}

// ─────────────────────────────────────────────────────────────────────────────
// USERS — Authenticated identities; email is the stable key
// ─────────────────────────────────────────────────────────────────────────────

model User {
  id            String             @id @default(cuid())
  email         String             @unique
  name          String?
  emailVerified DateTime?

  createdAt     DateTime           @default(now())
  updatedAt     DateTime           @updatedAt
  lastSignInAt  DateTime?

  memberships   TenantMembership[]
  sentInvites   InviteToken[]      @relation("InviteTokenInvitedBy")
  identities    UserIdentity[]     // External auth provider links

  @@index([email])
}

// ─────────────────────────────────────────────────────────────────────────────
// USER IDENTITY — External auth provider bindings
// Reserved shape so we don't paint ourselves into an email-only corner.
// Supports Auth0 / Clerk / Azure AD / NextAuth / any OIDC provider.
// A user can have multiple identities (e.g. email + GitHub + Google).
// ─────────────────────────────────────────────────────────────────────────────

model UserIdentity {
  id          String @id @default(cuid())
  userId      String
  provider    String  // e.g. "email", "github", "google", "azure-ad", "auth0"
  providerSub String  // Subject claim from the provider's ID token
  createdAt   DateTime @default(now())

  user User @relation(fields: [userId], references: [id], onDelete: Cascade)

  @@unique([provider, providerSub])
  @@index([userId])
}

// ─────────────────────────────────────────────────────────────────────────────
// TENANT MEMBERSHIP — User's role and access within a tenant
// ─────────────────────────────────────────────────────────────────────────────

model TenantMembership {
  id       String           @id @default(cuid())
  tenantId String
  userId   String
  tenant   Tenant           @relation(fields: [tenantId], references: [id], onDelete: Cascade)
  user     User             @relation(fields: [userId], references: [id], onDelete: Cascade)

  role     MembershipRole   @default(MEMBER)
  status   MembershipStatus @default(ACTIVE)

  createdAt DateTime        @default(now())
  updatedAt DateTime        @updatedAt

  @@unique([tenantId, userId])
  @@index([tenantId])
  @@index([userId])
  @@index([status])
}

enum MembershipRole {
  OWNER    // Created the tenant
  ADMIN    // Can manage team, invitations, settings
  MEMBER   // Standard access
  READONLY // View-only (future)
}

enum MembershipStatus {
  ACTIVE    // Member is active
  INVITED   // Invitation sent, pending acceptance
  SUSPENDED // Admin suspended this member
  INACTIVE  // Departed or soft-deleted
}

// ─────────────────────────────────────────────────────────────────────────────
// INVITE TOKENS — Email-based invitations for new users
// ─────────────────────────────────────────────────────────────────────────────

model InviteToken {
  id              String           @id @default(cuid())
  token           String           @unique @db.Char(32)  // Signed JWT or random hex

  // Who created this invite. Nullable because the first owner may be invited
  // by the system before any user record exists.
  createdByKind   InviteCreatorKind @default(SYSTEM)
  invitedByUserId String?           // null when createdByKind = SYSTEM or ADMIN
  invitedByUser   User?             @relation("InviteTokenInvitedBy", fields: [invitedByUserId], references: [id])

  tenantId        String
  tenant          Tenant           @relation(fields: [tenantId], references: [id], onDelete: Cascade)

  inviteeEmail    String
  inviteeName     String?
  roleToGrant     MembershipRole   @default(MEMBER)

  createdAt       DateTime       @default(now())
  updatedAt       DateTime       @updatedAt
  expiresAt       DateTime       // 7–30 days
  acceptedAt      DateTime?
  acceptedByUserId String?

  maxUses         Int            @default(1)
  usesRemaining   Int            // usesRemaining <= maxUses; set to 0 on first claim

  @@index([tenantId])
  @@index([inviteeEmail])
  @@index([expiresAt])
}

enum InviteCreatorKind {
  SYSTEM  // Generated by provisioning pipeline; no user exists yet
  USER    // Created by an existing tenant member
  ADMIN   // Created by a Keon internal admin
}

// ─────────────────────────────────────────────────────────────────────────────
// PROVISIONING RUNS — Durable audit trail of tenant setup operations
// ─────────────────────────────────────────────────────────────────────────────

model ProvisioningRun {
  id               String             @id @default(cuid())
  tenantId         String
  tenant           Tenant             @relation(fields: [tenantId], references: [id], onDelete: Cascade)

  activationMode   ActivationMode     @default(INVITE)
  activationSource ActivationSource

  state            ProvisioningState  @default(INVITE_VALIDATING)
  stateHistory     Json               @default("[]")  // { state, timestamp, error? }[]

  startedByEmail   String
  startedByName    String?

  status           ProvisioningStatus @default(IN_PROGRESS)
  completedAt      DateTime?
  failedAt         DateTime?
  failureCode      String?
  failureMessage   String?

  createdAt        DateTime           @default(now())
  updatedAt        DateTime           @updatedAt

  @@index([tenantId])
  @@index([status])
  @@index([createdAt])
}

enum ActivationMode   { INVITE TEST }
enum ActivationSource { INVITE_TOKEN TEST_TOKEN }

enum ProvisioningState {
  INVITE_VALIDATING
  TENANT_RESOLVING
  TENANT_CREATING
  MEMBERSHIP_BINDING
  WORKSPACE_BOOTSTRAPPING
  PROVISIONING_COMPLETE
  PROVISIONING_FAILED
}

enum ProvisioningStatus { IN_PROGRESS COMPLETED FAILED }

// ─────────────────────────────────────────────────────────────────────────────
// RETENTION POLICIES — Tenant-level data retention config
// ─────────────────────────────────────────────────────────────────────────────

model RetentionPolicy {
  id             String      @id @default(cuid())
  tenantId       String
  tenant         Tenant      @relation(fields: [tenantId], references: [id], onDelete: Cascade)

  name           String
  description    String?
  retentionDays  Int         // Days before receipt becomes eligible for deletion
  deleteAfterDays Int?       // Days after eligible when auto-delete occurs; null = manual only
  receiptKind    ReceiptKind? // null = apply to all kinds

  createdAt      DateTime    @default(now())
  updatedAt      DateTime    @updatedAt
  deactivatedAt  DateTime?

  @@unique([tenantId, name])
  @@index([tenantId])
}

enum ReceiptKind { DECISION EXECUTION MEMORY }

// ─────────────────────────────────────────────────────────────────────────────
// LEGAL HOLDS — Prevent deletion for compliance / litigation
// ─────────────────────────────────────────────────────────────────────────────

model LegalHold {
  id              String   @id @default(cuid())
  tenantId        String
  tenant          Tenant   @relation(fields: [tenantId], references: [id], onDelete: Cascade)

  correlationId   String?  // Scoped to specific correlation if provided
  reason          String
  createdByEmail  String
  createdByName   String?

  createdAt       DateTime @default(now())
  expiresAt       DateTime?
  revokedAt       DateTime?
  revokedByEmail  String?
  revokedReason   String?
  isActive        Boolean  @default(true)

  @@index([tenantId])
  @@index([isActive])
  @@index([expiresAt])
}
```

### Schema Principles

1. **Multi-tenant by design** — `TenantId` required on all relational tables. Aligns with ADR-0004 receipt persistence.
2. **AccessRequest ≠ Tenant** — A form submission is not a tenant. `AccessRequest` tracks request state; `Tenant` is only created when provisioning begins. This keeps the tenant table clean and approval semantics clear.
3. **Soft deletes only** — No hard deletes for audit trail. Use `status` / `isActive` / `deactivatedAt`.
4. **CUID primary keys** — Compatible with existing `correlationId` format (TEXT, not numeric).
5. **Temporal on everything** — All entities record `createdAt` / `updatedAt`.
6. **Email as identity anchor** — User authentication starts with email; `UserIdentity` reserves the shape for future OIDC/SSO providers without breaking email-first flows.
7. **Extensible metadata** — JSON columns on `Tenant` avoid schema migrations for tenant-specific settings.
8. **Append-only state history** — `ProvisioningRun.stateHistory` is a JSON array; transitions appended, never overwritten.
9. **Provisioning ≠ Onboarding** — `ProvisioningRun` tracks the infrastructure setup state machine. `Workspace.onboardingStatus` tracks the product onboarding flow separately. These must not be conflated.
10. **Environment distinction is enforced, not just labeled** — Business logic must prevent `SANDBOX` workspaces from claiming live status, `PRODUCTION` workspaces from being activated by test tokens, and `DEMO_FIXTURE` workspaces from appearing as real tenant data.

### ⚠️ Prisma Middleware Warning

The tenant isolation middleware pattern (injecting `WHERE tenantId = X` on queries) is a useful safety net but is **not sufficient as the sole guard**. Known failure modes:

- **Raw queries** (`prisma.$queryRaw`) bypass middleware entirely
- **Nested writes** (e.g. `create` with nested `connect`) may not have the right context injected
- **Admin operations** that legitimately need cross-tenant access must explicitly opt out
- **Middleware order** matters; a second middleware can override the isolation injection

**Required additional layers:**
- Explicit service-layer helpers (`getTenantOrThrow(tenantId, userId)`, `requireMembership(...)`)
- Integration tests that verify cross-tenant isolation at the API layer
- A lint rule or code review checklist flag for any `prisma.$queryRaw` usage
- Postgres row-level security (RLS) as a defense-in-depth layer post-launch

---

## Options Considered

### Option A — Single `tenantId` column on all tables (Chosen)

Every tenant-scoped table carries a `tenantId` column and a composite index.

**Pros:** Simple, predictable querying (`WHERE tenant_id = X`). Mirrors ADR-0004 receipt schema. Enables future PostgreSQL row-level security (RLS). Clear isolation boundary.

**Cons:** Larger composite keys; more index space; verbose queries always include `tenantId`.

---

### Option B — Global user table + tenant-scoped sessions

Users exist globally; tenants are scoped to sessions.

**Pros:** Smaller User table; natural for cross-tenant features (e.g., user audit history).

**Cons:** More complex join logic; harder to enforce isolation at query layer; session state fragmentation.

**Decision:** Rejected. Keon's multi-tenancy is hard (no cross-tenant user access by default) and admin-controlled.

---

### Option C — JSON-based tenant hierarchies (orgs → teams → projects)

Flexible nesting encoded in JSONB columns.

**Pros:** Future-proof for complex structures; flexible nesting.

**Cons:** Queryability suffers; harder to index; version management overhead.

**Decision:** Deferred to Phase 4. Start simple: Tenant → Workspace → Membership.

---

## Recommendation

**Adopt Option A.**

Rationale:
- Aligns with ADR-0004 receipt persistence schema (consistent `TenantId` isolation key)
- Supports existing activation flow (token → tenant → user) without rearchitecting
- Email-first identity matches current `/get-access` and `/activate` flows
- Simple to evolve — JSON `metadata` columns absorb tenant-specific settings without migrations
- PostgreSQL + Prisma stack is lowest-friction for the current team

---

## Consequences

### Positive

- Full audit trail: every provisioning step is recorded in `ProvisioningRun`
- Clear isolation: `TenantId` on every query prevents accidental cross-tenant leaks
- Extensible roles: `MembershipRole` enum covers OWNER → READONLY; enterprise tiers extend cleanly
- Invite lifecycle: `InviteToken` supports time-bounded, use-limited invitations with expiry
- Receipt persistence alignment: `RetentionPolicy` and `LegalHold` tables bind directly to ADR-0004

### Negative

- Composite indexes required for high-traffic queries (plan indexes carefully)
- Soft deletes complicate cleanup and require explicit null-safety logic in application code
- Invite expiry requires a background job (cron / pg_cron) to mark expired tokens
- Orphaned users require explicit cleanup if a user departs all tenants

### Risks

| Risk | Mitigation |
|---|---|
| Cross-tenant data leak if `tenantId` WHERE clause omitted | Prisma middleware enforcement; optional RLS in Postgres |
| Token reuse if invite not invalidated | Set `usesRemaining = 0` on first claim; hard `expiresAt` deadline |
| Orphaned users on tenant delete | `TenantMembership.status = INACTIVE` on user account deletion; soft-delete `User` record |
| Cascading receipt orphans if tenant deleted | ADR-0004 receipt tables have no FK to `Tenant`; receipts are append-only, never cascade-deleted |

---

## Implementation Notes

### Migration Path

**Phase 1 — Minimum durable tenant path (this sprint)**
`AccessRequest`, `Tenant`, `User`, `UserIdentity`, `TenantMembership`, `Workspace`, `ProvisioningRun`, `InviteToken`

Rationale: these eight tables form an inseparable unit. You can't persist a provisioning run without a tenant, can't bind a membership without a user, and can't issue the first invite without `InviteCreatorKind.SYSTEM`. Ship them together.

**Phase 2 — Retention integration (next sprint)**
`RetentionPolicy`, `LegalHold` — link to ADR-0004 receipt persistence

**Phase 3 — Enterprise (post-launch)**
SSO/SCIM via `UserIdentity` provider expansion, advanced roles, team hierarchies, multi-workspace, Postgres RLS

---

### Files to Create / Modify

| File | Action |
|---|---|
| `prisma/schema.prisma` | Create with all Phase 1 models |
| `prisma/migrations/001_initial_tenant_schema.sql` | Auto-generated by `prisma migrate dev` |
| `src/server/db/client.ts` | Prisma client singleton |
| `src/server/db/tenant.ts` | `getTenantOrThrow()`, `requireMembership()` — explicit service-layer guards (see middleware warning above) |
| `src/server/db/access-request.ts` | `createAccessRequest()`, `approveAccessRequest()`, `rejectAccessRequest()` |
| `src/api/access-request/route.ts` | Persist form submission to `AccessRequest` (currently email-only) |
| `src/api/activation/route.ts` | Replace in-process `Map` with `ProvisioningRun` persistence |
| `src/lib/provisioning/index.ts` | `startProvisioning()`, `resolveActivationToken()`, `bindFirstUser()` |
| `src/lib/provisioning/invite.ts` | `issueSystemInvite()` — creates `InviteToken` with `createdByKind: SYSTEM` |
| `keon-docs/adr/ADR-0005-tenant-data-model.md` | This file |

---

### State Machine Integration

The existing `ProvisioningStateMachine` drives `ProvisioningRun.state`. On each transition:

```typescript
async function advanceProvisioning(
  run: ProvisioningRun,
  newState: ProvisioningState
): Promise<ProvisioningRun> {
  const history = (run.stateHistory as StateEvent[]);
  history.push({ state: run.state, timestamp: new Date().toISOString() });

  try {
    switch (newState) {
      case 'TENANT_CREATING': {
        const tenant = await db.tenant.create({
          data: {
            externalId: generateExternalId(),
            name: run.startedByEmail.split('@')[0],
            requestedByEmail: run.startedByEmail,
            status: 'PROVISIONING',
          },
        });
        // store tenantId back on run
        break;
      }
      case 'MEMBERSHIP_BINDING': {
        const user = await getOrCreateUser(run.startedByEmail, run.startedByName);
        await db.tenantMembership.create({
          data: { tenantId: run.tenantId, userId: user.id, role: 'OWNER' },
        });
        break;
      }
      case 'WORKSPACE_BOOTSTRAPPING': {
        await db.workspace.create({
          data: {
            tenantId: run.tenantId,
            externalId: generateExternalId(),
            name: 'default',
            environment: run.activationMode === 'TEST' ? 'SANDBOX' : 'PRODUCTION',
          },
        });
        break;
      }
      case 'PROVISIONING_COMPLETE': {
        return db.provisioningRun.update({
          where: { id: run.id },
          data: { state: newState, status: 'COMPLETED', completedAt: new Date(), stateHistory: history },
        });
      }
    }

    return db.provisioningRun.update({
      where: { id: run.id },
      data: { state: newState, stateHistory: history },
    });

  } catch (err: unknown) {
    const e = err as Error & { code?: string };
    return db.provisioningRun.update({
      where: { id: run.id },
      data: {
        state: 'PROVISIONING_FAILED',
        status: 'FAILED',
        failedAt: new Date(),
        failureCode: e.code ?? 'UNKNOWN',
        failureMessage: e.message,
        stateHistory: history,
      },
    });
  }
}
```

---

### Tenant Isolation Middleware (Prisma)

```typescript
// prisma/middleware/tenant-isolation.ts
const TENANT_SCOPED_MODELS = [
  'TenantMembership', 'Workspace', 'InviteToken',
  'ProvisioningRun', 'RetentionPolicy', 'LegalHold',
] as const;

prisma.$use(async (params, next) => {
  const tenantId = getCurrentTenantId(); // from request context / session
  if (!tenantId) return next(params);

  const model = params.model as string;
  if (TENANT_SCOPED_MODELS.includes(model as any)) {
    if (['findUnique', 'findFirst', 'findMany', 'count'].includes(params.action)) {
      params.args ??= {};
      params.args.where ??= {};
      if (!params.args.where.tenantId) {
        params.args.where.tenantId = tenantId;
      }
    }
  }

  return next(params);
});
```

---

### Testing Strategy

**Unit tests (Prisma models)**
- Create tenant → create user → create membership → verify isolation
- Invite token expiry and reuse prevention (`usesRemaining` enforcement)
- Soft delete behavior — deactivated memberships excluded from active queries

**Integration tests (API layer)**
- `POST /get-access` → email receipt (existing) + `ProvisioningRun` record created
- `GET /activate?token=X` → state machine advances through all 7 states → `TenantMembership` created
- `POST /api/invites/:token/accept` → `InviteToken.usesRemaining` decremented; `TenantMembership` created

**Security tests**
- Omitting `tenantId` in query returns no cross-tenant rows
- User cannot list members of a tenant they don't belong to
- Invite tokens cannot be reused after `usesRemaining = 0`
- Expired tokens (`expiresAt < now`) are rejected

---

## References

- **ADR-0001** — Backing Store (Postgres + Redis)
- **ADR-0002** — Auth Surface (hardened invite-token + session cookie)
- **ADR-0004** — Receipt Persistence & Forensic Storage
- **Activation types** — `keon.control.website/src/lib/activation/types.ts`
- **Access request flow** — `keon-systems-web/src/app/get-access/page.tsx`
- **Prisma docs** — https://www.prisma.io/docs/orm/prisma-schema/overview

---

*End of ADR-0005*

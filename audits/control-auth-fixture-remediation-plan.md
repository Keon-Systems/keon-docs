# Keon Control — Auth Gate & Fixture Honesty Remediation Plan

**Date:** 2026-05-08
**Status:** ✅ CC-01 through CC-12 COMPLETE — LB-1 through LB-4 RESOLVED (verified 2026-05-09)
**Repo:** `keon-systems-pr/src/Keon.Control.Website`
**Trigger:** C.01 route inventory revealed zero authentication and 7 §1.3 Fixture Honesty Rule violations

**Verification results (2026-05-09):**
- `next build` — EXIT 0 ✅ (Compiled successfully, TypeScript clean, all 19 pages/routes generated)
- `vitest run` — EXIT 0 ✅ (63/63 tests passing across 4 test files)
- `tsc --noEmit` — EXIT 0 ✅ (no type errors)
- Lint — EXIT 1 (pre-existing on `main`: 2× `no-explicit-any` in `line-chart.tsx` lines 51+61, React compiler memoization in `keyboard.ts` line 99 — confirmed via `git show main:...` before this branch existed; zero new lint failures introduced by CC-01 through CC-12)

---

## Launch Blockers (No Deploy Until Resolved)

| # | Blocker | Severity | Blocks |
|---|---|---|---|
| LB-1 | No authentication on any route | ✅ RESOLVED | CC-01–CC-04: proxy.ts auth gate, iron-session, public route allowlist |
| LB-2 | `/evidence/source/[slug]` reads filesystem without auth | ✅ RESOLVED | CC-05–CC-06: path sandbox + ENABLE_GOLDEN_PATH env flag |
| LB-3 | 7 fixture-backed routes display no fixture label | ✅ RESOLVED | CC-07–CC-08: global FixtureModeBanner + per-page FixtureLabel |
| LB-4 | `/ui-demo` reachable in production | ✅ RESOLVED | CC-09: notFound() gate in production |
| LB-5 | `/incident-mode` publicly triggerable | 🟡 MEDIUM | ALPHA launch |
| LB-6 | `/settings` save is a no-op; no session context | 🟡 MEDIUM | ALPHA launch |

---

## Route Protection Matrix

| Route | Current | Target | Notes |
|---|---|---|---|
| `/` (dashboard) | Public | Protected | Fixture-backed; add fixture banner |
| `/alerts` | Public | Protected | Fixture-backed; add fixture banner |
| `/evidence` | Public | Protected | Launchable once auth is in place |
| `/evidence/verify` | Public | Protected | Launchable once auth is in place |
| `/evidence/pack` | Public | Protected | Launchable once auth is in place |
| `/evidence/pack/[pack_hash]` | Public | Protected | Launchable once auth is in place |
| `/evidence/golden-path` | Public | Protected + gated | Internal sales tool; hide from nav; require `ENABLE_GOLDEN_PATH=true` |
| `/evidence/source/[slug]` | Public | Protected + sandboxed | Filesystem exposure; auth + path check required |
| `/governance/receipts` | Public | Protected | Fixture-backed; API client ready to wire |
| `/incident-mode` | Public | Protected + role-gated | Not in nav; require explicit operator role |
| `/observability/traces` | Public | Protected | Fixture-backed; API client ready to wire |
| `/policies` | Public | Protected | Fixture-backed; backend stub |
| `/runtime/executions` | Public | Protected | Fixture-backed; API client ready to wire |
| `/settings` | Public | Protected | Hide before launch or disable save |
| `/tenants` | Public | Protected | Fixture-backed; backend stub |
| `/ui-demo` | Public | Dev-only (404 in prod) | Remove from prod build |

**No Control routes should be publicly accessible without an authenticated session.**
There are no legitimate public routes in the Control dashboard — no marketing pages, no landing pages, no public proof verification (that lives in `keon-systems-web`).

---

## 1. Auth Gate — Implementation Plan

### 1.1 Recommended Auth Strategy

**Package.json has no auth library.** Options evaluated:

| Option | Install Cost | Infrastructure | Launch Timeline | Post-Launch Path |
|---|---|---|---|---|
| **A. Iron Session (recommended for launch)** | `iron-session` | Cookie only; no DB | 1 day | Replace with Auth.js post-launch |
| B. Auth.js (NextAuth v5) | `next-auth` + DB adapter | DB required (Prisma/ADR-0005) | 3–5 days | Canonical long-term choice |
| C. Clerk | `@clerk/nextjs` | Hosted; zero DB | 0.5 day | Vendor lock; requires paid plan |
| D. Shared-secret env gate | None | Env var only | 2 hours | Not real auth; preview-only |

**Recommendation: Ship with Iron Session for launch. Plan Auth.js migration for ALPHA → GA.**

Rationale:
- ADR-0002 states: "hardened invite-token + session cookie now; Auth.js or Clerk post-launch"
- Iron Session is stateless JWT cookies — no database, no provider, zero infrastructure
- Session token issued from the existing `/activate` flow in `keon-systems-web`
- `UserIdentity` table (ADR-0005) reserves the OIDC shape for Auth.js migration
- Immediate security lift with a single middleware file

### 1.2 Iron Session Auth Flow

```
keon-systems-web /activate?token=X
    │
    ▼  (token validated, tenant provisioned)
    POST /api/auth/session  ← new endpoint in Control app
    Body: { activationToken, tenantId, userId }
    │
    ▼  iron-session creates signed cookie
    Set-Cookie: keon_control_session=<signed-jwt>; HttpOnly; Secure; SameSite=Strict
    │
    ▼  redirect to /  (dashboard)
    Middleware checks cookie on every request
```

For the pre-launch/internal preview window, a simpler gate is acceptable:

```
CONTROL_PREVIEW_TOKEN=<env var>
POST /api/auth/preview  { token: CONTROL_PREVIEW_TOKEN }
→ sets session cookie
```

This gives a deployable, auth-gated dashboard before the full activation flow is wired.

### 1.3 Middleware Implementation Plan

**File to create:** `src/middleware.ts` (project root, next to `src/app/`)

```typescript
// src/middleware.ts

import { NextResponse } from 'next/server'
import type { NextRequest } from 'next/server'
import { getIronSession } from 'iron-session'

// Routes that are explicitly allowed without a session
const PUBLIC_ROUTES = [
  '/api/auth/session',   // Session creation endpoint
  '/api/auth/preview',   // Preview token gate
  '/api/health',         // Health check (for infra)
  '/login',              // Login page (to be created)
]

// Routes that should 404 in production (never reach the handler)
const PRODUCTION_BLOCKED = [
  '/ui-demo',
]

export async function middleware(request: NextRequest) {
  const { pathname } = request.nextUrl

  // 1. Hard-block internal-only routes in production
  if (
    process.env.NODE_ENV === 'production' &&
    PRODUCTION_BLOCKED.some(route => pathname.startsWith(route))
  ) {
    return new NextResponse(null, { status: 404 })
  }

  // 2. Allow explicitly public routes
  if (PUBLIC_ROUTES.some(route => pathname.startsWith(route))) {
    return NextResponse.next()
  }

  // 3. Check for valid session
  const session = await getIronSession(request, NextResponse.next(), {
    password: process.env.SESSION_SECRET!,
    cookieName: 'keon_control_session',
    cookieOptions: { secure: process.env.NODE_ENV === 'production' },
  })

  if (!session.userId) {
    const loginUrl = new URL('/login', request.url)
    loginUrl.searchParams.set('next', pathname)
    return NextResponse.redirect(loginUrl)
  }

  // 4. Role gate for sensitive routes
  if (pathname.startsWith('/incident-mode') && session.role !== 'operator') {
    return new NextResponse(null, { status: 403 })
  }

  return NextResponse.next()
}

export const config = {
  matcher: [
    // Match all routes except Next.js internals and static files
    '/((?!_next/static|_next/image|favicon.ico|.*\\.(?:svg|png|jpg|ico|css|js)$).*)',
  ],
}
```

**Session shape:**
```typescript
// src/lib/session.ts
import { SessionOptions } from 'iron-session'

export interface KeonControlSession {
  userId: string
  tenantId: string
  role: 'owner' | 'admin' | 'member' | 'operator'
  email: string
  issuedAt: number
}

export const sessionOptions: SessionOptions = {
  password: process.env.SESSION_SECRET!,
  cookieName: 'keon_control_session',
  cookieOptions: {
    secure: process.env.NODE_ENV === 'production',
    httpOnly: true,
    sameSite: 'strict',
  },
}
```

### 1.4 Login Page Plan

**File to create:** `src/app/login/page.tsx`

For launch: a simple email + preview token form. No OAuth flow yet.

```
/login
  ├── Preview mode: "Enter your access code" → POST /api/auth/preview
  └── Activation mode: "Enter your activation token" → POST /api/auth/session
      (wired to keon-systems-web activation token validation)
```

The login page is the only public route in the Control app.

### 1.5 Required Env Vars

```bash
# Required for auth — must be set in every deployment
SESSION_SECRET=<random 32+ char string>          # iron-session encryption key

# Preview gate — used before full activation flow
CONTROL_PREVIEW_TOKEN=<random token>             # for internal preview access

# Existing — keep as-is
NEXT_PUBLIC_API_BASE_URL=...
NEXT_PUBLIC_API_LIVE_MODE=false
```

**No deployment should proceed without `SESSION_SECRET` set.** Fail fast: add a startup check.

---

## 2. Filesystem Route Lockdown — `/evidence/source/[slug]`

### 2.1 Current Vulnerability

```typescript
// CURRENT CODE — evidence-docs.ts
const absolutePath = path.join(process.cwd(), doc.filePath)
const content = await fs.readFile(absolutePath, "utf-8")
```

The `getEvidenceDoc(slug)` registry lookup protects against arbitrary slug injection (only 5 registered slugs). However:

1. **No auth gate** — these internal docs are publicly readable right now
2. **Path boundary not enforced** — if a bad registry entry is added, there is no path sandbox check
3. **Internal docs exposed** — `docs/BUYER_WOW.md`, `samples/buyer-wow/run.ps1` are sales enablement tools, not product features. Exposing them to any unauthenticated visitor is a data hygiene failure.
4. **Pattern is dangerous** — the code as written will be copied/adapted; the next developer may make `filePath` user-controlled

### 2.2 Mitigation Plan

**Immediate (Day 1):**
Auth gate from Step 1 blocks unauthenticated access entirely. This is the highest-impact fix.

**Code hardening (Day 1–2):**

```typescript
// HARDENED evidence-docs.ts

import path from 'path'

// Sandbox: all evidence docs must resolve within this directory
const EVIDENCE_DOCS_ROOT = path.resolve(process.cwd(), 'docs')
const SAMPLES_ROOT = path.resolve(process.cwd(), 'samples')
const ALLOWED_ROOTS = [EVIDENCE_DOCS_ROOT, SAMPLES_ROOT]

export function getEvidenceDoc(slug: string): EvidenceDoc | null {
  // Slug allowlist — never derive from user input beyond this lookup
  const doc = registry.find(d => d.slug === slug) ?? null
  if (!doc) return null

  // Validate resolved path stays within allowed roots
  const resolved = path.resolve(process.cwd(), doc.filePath)
  const isAllowed = ALLOWED_ROOTS.some(root => resolved.startsWith(root + path.sep))
  if (!isAllowed) {
    console.error('[security] Evidence doc path escaped sandbox:', resolved)
    return null
  }

  return doc
}
```

**Page component changes:**
- Replace raw `path.join(process.cwd(), doc.filePath)` with a call to a validated helper
- Use the sandbox-validated path from `getEvidenceDoc` — do not re-derive the path in the page
- Add explicit error boundary for file read failures

**Launch recommendation:**
Unless `/evidence/golden-path` + `/evidence/source/[slug]` are required for the v1 product experience, **hide them behind `ENABLE_GOLDEN_PATH=true` env flag** before launch. They are sales tooling, not product features. The auth gate covers the worst case, but removing the surface is safer.

### 2.3 Tests Required

```
describe('evidence-docs path sandbox', () => {
  it('resolves valid slug to allowed path')
  it('rejects slug not in registry')
  it('rejects registry entry with path traversal (../../etc/passwd)')
  it('rejects registry entry pointing outside docs/ or samples/')
  it('handles null slug gracefully')
})
```

---

## 3. Fixture Honesty Sweep

### 3.1 Strategy

Two layers needed:

**Layer 1 — Global banner (layout level)**
Add a persistent `FixtureModeAlert` to the Control app layout that renders when `NEXT_PUBLIC_API_LIVE_MODE !== 'true'`. This catches every page automatically, including future pages.

**Layer 2 — Route-local fixture labels**
For the 7 identified routes, add in-page fixture context: a subtle but clear "Demo data" indicator near the data being displayed (table header, metric card, chart title).

### 3.2 Global Banner Implementation

**File to modify:** `src/app/layout.tsx`

```tsx
// Add to layout.tsx

const IS_FIXTURE_MODE = process.env.NEXT_PUBLIC_API_LIVE_MODE !== 'true'

// Inside RootLayout, above {children}:
{IS_FIXTURE_MODE && (
  <div className="sticky top-0 z-50 flex items-center justify-center gap-3 bg-amber-500/10 border-b border-amber-500/30 px-4 py-2 text-xs font-mono">
    <span className="text-amber-400 tracking-widest uppercase font-semibold">
      ⚠ Demo Mode
    </span>
    <span className="text-amber-400/70">
      All data shown is fixture data. Not connected to live tenant.
    </span>
  </div>
)}
```

This banner is:
- Sticky (always visible, cannot be scrolled past)
- High z-index (cannot be obscured)
- Amber/warning color (visually distinct from the dark dashboard UI)
- Not dismissible (fixture state persists until env var changes)

### 3.3 Route-Local Fixture Label Component

**File to create:** `src/components/fixture-label.tsx`

```tsx
export function FixtureLabel({ label = 'Demo data' }: { label?: string }) {
  if (process.env.NEXT_PUBLIC_API_LIVE_MODE === 'true') return null
  return (
    <span className="ml-2 inline-flex items-center rounded border border-amber-500/30 bg-amber-500/10 px-1.5 py-0.5 text-[10px] font-mono uppercase tracking-widest text-amber-400">
      {label}
    </span>
  )
}
```

Apply to each of the 7 fixture routes — adjacent to data source labels, table headers, and metric titles.

### 3.4 Fixture Routes Requiring Labels

All 7 routes require both the global banner (automatic via layout) and at least one in-page label:

| Route | In-page label location | Fixture source to annotate |
|---|---|---|
| `/` | Dashboard header, each metric card | "Trust Score", "Active Agents", "Avg Latency" |
| `/alerts` | Alerts table header | "Recent Alerts" |
| `/governance/receipts` | Receipts table header | "Recent Receipts" |
| `/runtime/executions` | Executions table header | "Recent Executions" |
| `/observability/traces` | Traces table header | "Active Traces" |
| `/policies` | Policies list header | "Active Policies" |
| `/tenants` | Tenants table header | "Tenant Directory" |

### 3.5 Tenant Fixture Names

`/tenants` displays `Acme Corporation`, `Globex Industries`, `Initech` as tenant names. These are Simpsons-universe names — fine for internal development, not acceptable in any customer demo without a fixture label. Once the fixture label is applied, these names are acceptable for demo purposes. Replacing them with neutral demo names (`Demo Tenant A`, `Demo Tenant B`) would be cleaner but is lower priority than the label itself.

---

## 4. Hide Internal-Only Routes

### 4.1 `/ui-demo` — Remove from production

**File to modify:** `src/app/ui-demo/page.tsx`

Add at the top of the page component:

```typescript
import { notFound } from 'next/navigation'

// ...inside component:
if (process.env.NODE_ENV === 'production') {
  notFound()
}
```

This renders a 404 in production. In development, the component showcase remains accessible.

Also: confirm `/ui-demo` is not linked from any nav component. The C.01 audit found it is not in the sidebar — verify no other link exists.

### 4.2 `/incident-mode` — Auth gate + role gate

The middleware plan (Step 1) already adds a role check:

```typescript
if (pathname.startsWith('/incident-mode') && session.role !== 'operator') {
  return new NextResponse(null, { status: 403 })
}
```

Additionally, modify the `/incident-mode` page to:
- Replace `Date.now()` as incident ID with a proper generated ID
- Replace `"manual-override"` and `"all"` hardcodes with explicit form inputs
- Add a confirmation dialog before activation ("This will trigger a critical incident response. Confirm?")

These are polish items after the auth gate is in place.

### 4.3 `/evidence/golden-path` and `/evidence/source/[slug]` — Env flag

Add env flag check to both pages:

```typescript
if (!process.env.ENABLE_GOLDEN_PATH) {
  notFound()
}
```

Set `ENABLE_GOLDEN_PATH=true` only in internal/sales demo deployments. Never in customer-facing production.

---

## 5. API Client Rewire Plan

**Pre-condition:** Auth gate must be in place before wiring live API routes. No point wiring real data into unauthenticated pages.

### 5.1 Ready to wire (API clients implemented in `lib/api/`)

| Route | API Client | Estimated effort |
|---|---|---|
| `/governance/receipts` | `lib/api/receipts.ts` | 1 day |
| `/runtime/executions` | `lib/api/executions.ts` | 1 day |
| `/observability/traces` | `lib/api/traces.ts` | 1 day |

Pattern for each: replace `const mockX = [...]` with `const data = await apiClient.getX()`. Keep mock as fallback when `liveMode === false`.

### 5.2 Backend stubs (cannot wire until backend ships)

| Route | Stub | Blocker |
|---|---|---|
| `/alerts` | `lib/api/alerts.ts` — "Backend endpoint not implemented yet" | Backend API |
| `/policies` | `lib/api/policies.ts` — "Backend endpoint not implemented yet" | Backend API |
| `/tenants` | `lib/api/tenants.ts` — "Backend endpoint not implemented yet" | ADR-0005 schema + backend API |

These 3 stay fixture-backed until the backend ships the corresponding endpoints. The fixture labels (Step 3) cover them in the meantime.

---

## 6. Test Plan

### Auth Gate Tests
```
describe('middleware auth gate', () => {
  it('redirects unauthenticated request on protected route to /login')
  it('preserves next= param in redirect URL')
  it('allows authenticated request through')
  it('returns 404 for /ui-demo in production')
  it('returns 403 for /incident-mode when role is not operator')
  it('allows /api/auth/* without session')
  it('allows /api/health without session')
})
```

### Session API Tests
```
describe('/api/auth/preview', () => {
  it('returns 200 and sets session cookie for valid CONTROL_PREVIEW_TOKEN')
  it('returns 401 for invalid preview token')
  it('returns 400 for missing token')
  it('sets HttpOnly, Secure, SameSite=Strict cookie attributes')
})
```

### Filesystem Sandbox Tests
```
describe('getEvidenceDoc path sandbox', () => {
  it('returns doc for valid registered slug')
  it('returns null for unknown slug')
  it('returns null and logs error if registered filePath escapes sandbox')
  it('handles slug with path separators (traversal attempt via slug)')
  it('handles empty/null slug')
})
```

### Fixture Mode Tests
```
describe('fixture mode banner', () => {
  it('renders fixture banner when NEXT_PUBLIC_API_LIVE_MODE is false')
  it('does not render fixture banner when NEXT_PUBLIC_API_LIVE_MODE is true')
  it('FixtureLabel returns null in live mode')
  it('FixtureLabel renders with default label in fixture mode')
})
```

---

## 7. Implementation Ticket Sequence

Execute in order — each ticket unblocks the next.

| Ticket | Title | Files | Effort | Unblocks |
|---|---|---|---|---|
| **CC-01** | Install iron-session; add session types | `package.json`, `src/lib/session.ts` | 0.5d | All auth work |
| **CC-02** | Create `src/middleware.ts` with route protection | `src/middleware.ts` | 1d | All protected routes |
| **CC-03** | Create `/login` page + `/api/auth/preview` endpoint | `src/app/login/page.tsx`, `src/app/api/auth/preview/route.ts` | 1d | Manual testing |
| **CC-04** | Add `SESSION_SECRET` env check on startup | `src/app/api/health/route.ts` or startup check | 0.5d | Safe deployment |
| **CC-05** | Harden `evidence-docs.ts` with path sandbox | `src/lib/evidence-docs.ts` | 0.5d | LB-2 cleared |
| **CC-06** | Add `ENABLE_GOLDEN_PATH` env flag to golden-path + source routes | `src/app/evidence/golden-path/page.tsx`, `src/app/evidence/source/[slug]/page.tsx` | 0.5d | LB-2 cleared |
| **CC-07** | Add global fixture mode banner to layout | `src/app/layout.tsx`, `src/components/fixture-mode-banner.tsx` | 0.5d | LB-3 cleared |
| **CC-08** | Add in-page fixture labels to 7 fixture routes | 7 `page.tsx` files | 1d | LB-3 cleared |
| **CC-09** | Gate `/ui-demo` with `notFound()` in production | `src/app/ui-demo/page.tsx` | 0.5h | LB-4 cleared |
| **CC-10** | Add role gate + confirmation dialog to `/incident-mode` | `src/app/incident-mode/page.tsx` | 1d | LB-5 cleared |
| **CC-11** | Wire receipts/executions/traces pages to existing API clients | 3 `page.tsx` files | 3d | Post-auth |
| **CC-12** | Write auth middleware + filesystem sandbox tests | `src/__tests__/` | 2d | ✅ DONE — 63/63 passing, build clean |

**Total critical-path to LB-1 through LB-4 cleared:** ~5 days (CC-01 through CC-09).

---

## 8. Risk If Deployed As-Is

| Risk | Likelihood | Impact |
|---|---|---|
| Any user discovers Control dashboard URL → full unauthenticated access | HIGH if URL is shared/indexed | CRITICAL — exposes operational UI, demo tenant data, incident activation |
| `/evidence/source/[slug]` exposes internal docs | HIGH — 5 paths accessible | HIGH — BUYER_WOW.md, sales scripts, API surface snapshots visible to anyone |
| Fixture dashboard presented without labels → customer believes data is real | HIGH — happens on every demo with `liveMode=false` | HIGH — false impression of live system, violates §1.3 |
| `/incident-mode` triggered by unauthorized user | LOW if URL is not shared | MEDIUM — triggers "critical" incident state in UI, no backend impact yet (fixture) |
| `/ui-demo` component arsenal indexed by search engines | LOW | LOW — embarrassing but not dangerous |

**Bottom line: Do not expose the Control dashboard on any externally-reachable URL until CC-01 through CC-04 are complete. The zero-auth state is the only launch-blocking risk with immediate external exposure consequences.**

---

## 9. Env Requirements for Safe Deployment

```bash
# REQUIRED — deployment must fail without these
SESSION_SECRET=                  # 32+ random chars; iron-session encryption key

# REQUIRED for launch-gate (before full activation flow is wired)
CONTROL_PREVIEW_TOKEN=           # shared internally; change before ALPHA

# OPTIONAL — default is mock mode; set true only when backend API is running
NEXT_PUBLIC_API_LIVE_MODE=false
NEXT_PUBLIC_API_BASE_URL=https://api.keon.systems

# OPTIONAL — set true only for internal sales demo deployments
ENABLE_GOLDEN_PATH=
```

---

*End of plan — awaiting approval before implementation*

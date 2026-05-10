# Keon Control Route Inventory — Audit Notes

**Date:** 2026-05-08
**Audited repo:** `keon-systems-pr/src/Keon.Control.Website` (package: `keon-command-center`)
**Note on repo location:** The Control dashboard was NOT found at `keon-control/` as expected. It lives nested inside `keon-systems-pr/src/Keon.Control.Website`. A second copy exists at `keon-systems-proof-pr/src/Keon.Control.Website` with an identical file structure — treat as a branch snapshot.

---

## Summary

| Classification | Count | Routes |
|---|---|---|
| Launchable | 4 | `/evidence`, `/evidence/verify`, `/evidence/pack`, `/evidence/pack/[pack_hash]` |
| Fixture-backed (label required) | 7 | `/`, `/alerts`, `/governance/receipts`, `/runtime/executions`, `/observability/traces`, `/policies`, `/tenants` |
| Hide before launch | 4 | `/settings`, `/evidence/golden-path`, `/evidence/source/[slug]`, `/incident-mode` |
| Internal-only | 1 | `/ui-demo` |
| Needs copy polish | 0 | — |
| Broken | 0 | — |
| Placeholder | 0 | — |
| **Total** | **16** | |

---

## 🚨 Critical Finding: Zero Authentication

**There is no authentication anywhere in this application.**

- No `middleware.ts` exists at the project root or `src/` level
- Root `layout.tsx` wraps only `AppStateProvider` and `IncidentModeProvider` — no session check, no redirect to login
- No login page exists among the 16 routes
- No auth library (NextAuth, Clerk, Auth.js, Azure AD) in `package.json`

**Every route is publicly accessible without login.**

Routes of specific concern before any deployment:

| Route | Risk |
|---|---|
| `/evidence/source/[slug]` | Reads repo files from disk via `fs.readFile` — only gated by an in-code registry lookup, no auth |
| `/incident-mode` | Allows triggering a "critical" incident affecting "all" components — publicly accessible |
| `/tenants` | Exposes tenant names, IDs, user counts, plan tiers (fixture now, but sets a dangerous pattern) |
| `/settings` | Presents account management UI with no session context |

**Required before any external exposure:** Add Next.js middleware auth gate. Until then, deploy behind IP allowlist or VPN only.

---

## Fixture-backed Routes — §1.3 Fixture Honesty Rule Violations

All 7 fixture-backed routes violate the Fixture Honesty Rule. None display a visible "Demo" or "Fixture" label. The default environment ships as mock mode (`NEXT_PUBLIC_API_LIVE_MODE=false` in `.env.example`) — meaning any deployment without an explicit `.env.local` override runs entirely on fixture data and looks live.

**Immediate action required:** Add visible fixture banner to all 7 routes before any demo or launch. Reviewer flag: `fixture-honesty`. Cannot pass `Needs Review` while this flag is open.

---

## API Client vs. Page Mismatch

Three routes have working API clients in `src/lib/api/` but the page components re-declare their own inline mocks instead of calling them:

| Route | API Client | Action |
|---|---|---|
| `/governance/receipts` | `lib/api/receipts.ts` — implemented | Wire page to API client; remove inline mock |
| `/runtime/executions` | `lib/api/executions.ts` — implemented | Wire page to API client; remove inline mock |
| `/observability/traces` | `lib/api/observability.ts` — implemented | Wire page to API client; remove inline mock |

Three other routes have backend stubs that are not yet implemented:

| Route | API Stub | Status |
|---|---|---|
| `/alerts` | `lib/api/alerts.ts` | "Backend endpoint not implemented yet" |
| `/policies` | `lib/api/policies.ts` | "Backend endpoint not implemented yet" |
| `/tenants` | `lib/api/tenants.ts` | "Backend endpoint not implemented yet" |

---

## Hide-before-launch Detail

### `/evidence/source/[slug]`
Server component that reads arbitrary files from the repo filesystem at runtime via `fs.readFile(process.cwd() + doc.filePath)`. Only gated by an in-code `evidenceDocRegistry` lookup — no auth, no path sandboxing. A misconfigured registry entry or crafted slug could expose unintended files. **Do not deploy to any customer-reachable environment without path sandboxing and auth.**

### `/evidence/golden-path`
References internal script paths (`samples/buyer-wow/run.ps1`, `samples/golden-path/verify-pack.ps1`) and internal docs (`START_HERE.md`, `BUYER_WOW.md`). Appears to be a buyer/sales enablement tool, not a general product feature. Should be gated behind a feature flag or moved to an internal tooling surface.

### `/incident-mode`
Not linked in sidebar. "Activate Incident Mode" button uses `Date.now()` as the incident ID and hardcodes `"manual-override"` as root subsystem and `"all"` as impacted components. Source comment explicitly says "for dev/demo purposes." Not production-safe as a public URL.

### `/settings`
UI is complete (profile, notifications, appearance, danger zone) but all fields are hardcoded defaults (`"Admin User"`, `"admin@example.com"`). Save button is a no-op. Would actively mislead customers into thinking preferences are persisted.

---

## Structural Notes

1. **Two parallel repos** — `keon-systems-pr` and `keon-systems-proof-pr` both contain `src/Keon.Control.Website` with identical page structures. Confirm which is the active branch before launch.

2. **Default env is mock mode** — `.env.example` ships with `NEXT_PUBLIC_API_LIVE_MODE=false`. Any deployment without an explicit override runs entirely on fixture data.

3. **Evidence routes are the strongest cluster** — `/evidence`, `/evidence/verify`, `/evidence/pack`, and `/evidence/pack/[pack_hash]` are all genuinely launchable. The hash verifier is real, functional, and requires no backend.

4. **`/ui-demo` must not reach production** — It's a developer component showcase. Gate it behind `NODE_ENV === 'development'` or remove it from the production build entirely.

---

## Recommended Next Steps (C.01 follow-on)

1. **Auth gate first** — No external deployment until middleware auth is in place
2. **Add fixture banners** — All 7 fixture routes need visible "Demo Data" labels (§1.3)
3. **Wire the 3 ready API clients** — receipts, executions, traces are a 1-day lift each
4. **Sandbox `/evidence/source/[slug]`** — Path validation + auth gate before any exposure
5. **Gate `/ui-demo`** — Remove from prod build or env-flag
6. **Decide on `/evidence/golden-path`** — Sales tool or product feature? Scope accordingly

---

*End of C.01 audit notes*

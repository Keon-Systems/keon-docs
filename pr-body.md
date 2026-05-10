## Summary

This PR resolves the critical Control launch exposure found in C.01:

- Adds launch-safe auth gate across all Control routes via `proxy.ts` + iron-session v8
- Adds preview-token login path (`/api/auth/preview` + `/login` page)
- Blocks internal/demo-only routes in production (`/ui-demo` -> 404, `/incident-mode` -> 403 without operator role)
- Locks down filesystem-backed evidence source access with allowlist + path sandbox
- Adds `ENABLE_GOLDEN_PATH` env flag to gate internal sales route
- Adds global `FixtureModeBanner` and route-local `FixtureLabel` across all 7 fixture-backed routes
- Adds verification tests for auth middleware, preview login, fixture mode, and evidence path sandboxing

## Launch blockers cleared

| # | Blocker | Resolution |
|---|---|---|
| LB-1 | No authentication on any route | `proxy.ts` auth gate with iron-session, public route allowlist, session cookie |
| LB-2 | `/evidence/source/[slug]` filesystem exposure | Allowlisted slug map + `isPathInSandbox()` path traversal guard |
| LB-3 | Fixture-backed routes display no fixture label | Global `FixtureModeBanner` + per-page `FixtureLabel` on 7 routes |
| LB-4 | `/ui-demo` reachable in production | `notFound()` gate in production, pass-through in dev/test |

## Verification

| Check | Result |
|---|---|
| `npm install` | EXIT 0 |
| `tsc --noEmit` | EXIT 0 -- no type errors |
| `vitest run` | EXIT 0 -- 63/63 passing (4 test files) |
| `next build` | EXIT 0 -- compiled, TypeScript clean, 19 routes generated |
| `eslint src` | EXIT 1 -- pre-existing failures on main, zero new failures introduced |

## Lint baseline note

Failures confirmed pre-existing on `main` via `git show main:<path>`:

- `src/components/charts/line-chart.tsx:51,61` -- `no-explicit-any` (identical on `main`)
- `src/lib/keyboard.ts:99` -- React compiler memoization issue (same `useCallback` on `main`)

Zero new lint failures introduced by this branch.

## Test coverage added (CC-12)

- `middleware.test.ts` -- 17 cases: public passthrough, fail-closed on missing secret, unauthenticated redirect with next= preservation, tampered cookie clearing, valid session passthrough, `/incident-mode` operator role gate, `/ui-demo` production 404, matcher pattern validation
- `preview-route.test.ts` -- 13 cases: fail-closed on missing env, request validation, token validation, successful auth (session fields, save(), cookie options)
- `evidence-docs.test.ts` -- 17 cases: slug allowlist, path traversal rejection (unix/windows/UNC/null-bytes/siblings), sandbox path validation
- `fixture.test.tsx` -- 16 cases: FixtureLabel render/hide, FixtureModeBanner render/hide/a11y, ENABLE_GOLDEN_PATH exact-match gate

## Follow-on (not blocking this PR)

- CC-10: Incident mode role-gate polish and confirmation dialog
- CC-11: Rewire `/governance/receipts`, `/runtime/executions`, `/observability/traces` to existing API clients
- Lint cleanup branch for pre-existing baseline failures

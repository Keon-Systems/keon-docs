# Keon Claims Canon Registry

This directory contains the claim/proof registry set for Keon Docs.

## Files

| File | Purpose |
|---|---|
| `CLAIMS_REGISTRY.yaml` | What Keon is allowed to claim, including status, products, strength, and proof requirement. |
| `CAPABILITY_REGISTRY.yaml` | Renamed from `FEATURES_BY_PHASE.yaml`. Tracks implemented/planned capabilities and their repo/path/proof linkage. |
| `PROOF_MAP.yaml` | Claim-to-proof routing table: artifact paths, verification commands, and evidence quality. |
| `PACKAGING_REGISTRY.yaml` | Renamed from `service_packages.md`. Defines adoption ladder, sellable packages, and product dependency rules. |
| `CAES_CERTIFICATE_MAP.yaml` | Maps CAES Collective certificate assertions to claims and proof entries. |
| `schemas/*.schema.json` | Lightweight schema guards for registry validation. |

## Canon rule

No public, sales, or certification claim should be made unless it appears in `CLAIMS_REGISTRY.yaml` and either has a mapped proof in `PROOF_MAP.yaml` or is explicitly marked `proposed`, `draft`, or `not_implemented`.

Receipts win. Canon serves the proof spine.

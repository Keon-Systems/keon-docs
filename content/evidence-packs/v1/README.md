# Evidence Pack Samples (v1)

Canonical downloadable sample Evidence Packs for the website tour.

- `evidence-pack.v1.latest.zip`: website download target (always current)
- `evidence-pack.v1.YYYY-MM-DD.zip`: immutable dated snapshot(s)
- `manifest.json`: mirror of the downloadable evidence-pack manifest (`manifestVersion = "1"`)
- `checksums.txt`: mirror of generated downloadable artifact checksums
- `signer-public-key.b64`: public key used to verify pack attestations
- `trust-bundle.json`: trust bundle used for AuthorizationValid verification

Notes:
- The ZIP sample is generated from `EvidencePackService.CreateAsync(...)`.
- It is wrapped in the minimal legacy attested manifest shape (`version = "v1"`) so the current verifier can validate it end-to-end.
- `manifestVersion = "1"` remains present in the verifier-visible `manifest.json` as the downloadable evidence-pack schema signal.

Last refreshed: 2026-02-26
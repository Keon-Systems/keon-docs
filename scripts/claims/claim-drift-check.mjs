// scripts/claims/claim-drift-check.mjs
import fg from "fast-glob";
import yaml from "js-yaml";
import fs from "node:fs";
import path from "node:path";
import process from "node:process";

const ROOT = process.cwd();

const DEFAULTS = {
  claimsRegistryPath: "canon/CLAIMS_REGISTRY.yaml",
  proofMapPath: "canon/PROOF_MAP.yaml",
  markdownGlobs: ["canon/**/*.md", "content/**/*.md", "whitepaper/**/*.md"],
  // Exclude common noise
  ignoreGlobs: ["**/node_modules/**", "**/.next/**", "**/dist/**", "**/build/**", "**/.git/**"],
};

function readYaml(filePath) {
  const abs = path.resolve(ROOT, filePath);
  if (!fs.existsSync(abs)) throw new Error(`Missing YAML file: ${filePath}`);
  return yaml.load(fs.readFileSync(abs, "utf8"));
}

function loadClaimsRegistry(registryPath) {
  const doc = readYaml(registryPath);
  const claims = doc?.claims;
  if (!Array.isArray(claims)) {
    throw new Error(`Invalid registry format: expected "claims: []" in ${registryPath}`);
  }
  const map = new Map();
  for (const c of claims) {
    if (!c?.id) throw new Error(`Claim missing "id" in ${registryPath}`);
    if (map.has(c.id)) throw new Error(`Duplicate claim id "${c.id}" in ${registryPath}`);
    map.set(c.id, c);
  }
  return map;
}

function loadProofMap(proofPath) {
  const doc = readYaml(proofPath);
  // expected:
  // proofs:
  //   KS-XXXX-###:
  //     ...
  const proofs = doc?.proofs ?? {};
  if (typeof proofs !== "object" || Array.isArray(proofs)) {
    throw new Error(`Invalid proof map format: expected "proofs: { ... }" in ${proofPath}`);
  }
  return proofs;
}

function extractClaimIdsFromText(text) {
  // canonical claim id format: KS-ABCD-001
  // (allow multiple word segments like KS-TRUSTOPS-010)
  const re = /\bKS-[A-Z0-9]{2,32}-\d{3}\b/g;
  return new Set(text.match(re) ?? []);
}

function scanMarkdownFiles(globs, ignores) {
  return fg.sync(globs, {
    cwd: ROOT,
    ignore: ignores,
    onlyFiles: true,
    unique: true,
    dot: true,
  });
}

function main() {
  const claimsRegistryPath = process.env.CLAIMS_REGISTRY_PATH || DEFAULTS.claimsRegistryPath;
  const proofMapPath = process.env.PROOF_MAP_PATH || DEFAULTS.proofMapPath;

  const claims = loadClaimsRegistry(claimsRegistryPath);
  const proofs = loadProofMap(proofMapPath);

  // allow override globs if needed
  const globs = process.env.MD_GLOBS
    ? process.env.MD_GLOBS.split(",").map((s) => s.trim()).filter(Boolean)
    : DEFAULTS.markdownGlobs;

  const files = scanMarkdownFiles(globs, DEFAULTS.ignoreGlobs);

  const used = new Map(); // claimId -> Set(filepaths)

  for (const file of files) {
    const abs = path.resolve(ROOT, file);
    const text = fs.readFileSync(abs, "utf8");
    const ids = extractClaimIdsFromText(text);
    if (ids.size === 0) continue;

    for (const id of ids) {
      if (!used.has(id)) used.set(id, new Set());
      used.get(id).add(file);
    }
  }

  const errors = [];

  // 1) referenced claim must exist in registry
  for (const [id, where] of used.entries()) {
    if (!claims.has(id)) {
      errors.push({
        type: "UNKNOWN_CLAIM_ID",
        id,
        where: Array.from(where).sort(),
        msg: `Claim referenced in markdown but missing from registry: ${id}`,
      });
    }
  }

  // 2) if claim exists and requires proof, it must exist in PROOF_MAP
  for (const [id, claim] of claims.entries()) {
    const proofRequired = !!claim.proof_required;
    if (!proofRequired) continue;

    const hasProof = Object.prototype.hasOwnProperty.call(proofs, id);
    if (!hasProof) {
      errors.push({
        type: "MISSING_PROOF_MAP_ENTRY",
        id,
        where: used.has(id) ? Array.from(used.get(id)).sort() : [],
        msg: `Claim requires proof but has no entry in PROOF_MAP: ${id}`,
      });
    }
  }

  // 3) optional: fail if proof exists for a claim not in registry (stale proofs)
  for (const id of Object.keys(proofs)) {
    if (!claims.has(id)) {
      errors.push({
        type: "PROOF_FOR_UNKNOWN_CLAIM",
        id,
        where: [],
        msg: `PROOF_MAP contains an entry for a claim not in registry: ${id}`,
      });
    }
  }

  if (errors.length > 0) {
    console.error("\n❌ CLAIM DRIFT CHECK FAILED\n");

    for (const e of errors) {
      console.error(`- [${e.type}] ${e.msg}`);
      if (e.where?.length) {
        for (const f of e.where) console.error(`    ↳ ${f}`);
      }
    }

    console.error("\nFix by updating:");
    console.error(`- ${claimsRegistryPath}`);
    console.error(`- ${proofMapPath}`);
    console.error("…or removing bad claim references from markdown.\n");

    process.exit(1);
  }

  const countUsed = used.size;
  console.log(`✅ Claim drift check passed. (${countUsed} claim IDs referenced in markdown)`);
}

main();

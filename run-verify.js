const { spawnSync } = require('child_process');
const path = require('path');
const fs = require('fs');

const CWD = 'D:\\Repos\\keon-omega\\keon-systems-pr\\src\\Keon.Control.Website';
const OUT = 'D:\\Repos\\keon-omega\\keon-docs\\verify-output.txt';
const BIN = 'D:\\nvm\\v24.7.0';
const LOCAL_BIN = path.join(CWD, 'node_modules', '.bin');

const NPM = `"${path.join(BIN, 'npm.cmd')}"`;
const NPX = `"${path.join(BIN, 'npx.cmd')}"`;

const baseEnv = {
  ...process.env,
  PATH: `${BIN};${LOCAL_BIN};${process.env.PATH || ''}`,
  PATHEXT: '.COM;.EXE;.BAT;.CMD;.VBS;.JS;.PS1',
};

// dev env: keeps devDependencies installed, used for install/typecheck/lint/test
const devEnv = { ...baseEnv, NODE_ENV: 'development' };
// prod env: correct for next build
const prodEnv = { ...baseEnv, NODE_ENV: 'production' };

const out = [];
out.push('STARTED: ' + new Date().toISOString());

function run(label, cmd, env) {
  out.push('');
  out.push('='.repeat(60));
  out.push(`[${label}] ${cmd}`);
  out.push('='.repeat(60));
  const r = spawnSync(cmd, [], {
    cwd: CWD, env, encoding: 'utf8',
    shell: true,
    maxBuffer: 10 * 1024 * 1024,
  });
  if (r.stdout) out.push(r.stdout);
  if (r.stderr) out.push('STDERR: ' + r.stderr);
  out.push(`EXIT: ${r.status ?? 'null'}`);
  if (r.error) out.push('ERROR: ' + r.error.message);
  return r.status;
}

// 1. npm install (dev env keeps devDependencies)
run('npm install', `${NPM} install`, devEnv);

// 2. Check packages
out.push('');
out.push(`vitest installed: ${fs.existsSync(path.join(CWD, 'node_modules', 'vitest'))}`);
out.push(`@testing-library: ${fs.existsSync(path.join(CWD, 'node_modules', '@testing-library'))}`);

// 3. typecheck  (tsc is in node_modules/.bin)
run('typecheck', `${NPX} --no-install tsc --noEmit`, devEnv);

// 4. lint
run('lint', `${NPX} --no-install eslint src --max-warnings=0`, devEnv);

// 5. tests
run('test', `${NPX} --no-install vitest run --reporter=verbose`, devEnv);

// 6. build (production env required by Next.js)
run('build', `${NPM} run build`, prodEnv);

const content = out.join('\n');
fs.writeFileSync(OUT, content, 'utf8');
console.log('Done. Written to verify-output.txt');

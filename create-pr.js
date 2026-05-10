const { spawnSync } = require('child_process');
const fs = require('fs');

const body = fs.readFileSync('D:\\Repos\\keon-omega\\keon-docs\\pr-body.md', 'utf8');

const result = spawnSync('C:\\Program Files\\GitHub CLI\\gh.exe', [
  'pr', 'create',
  '--base', 'main',
  '--head', 'augment/pt-be-armor-hardening',
  '--title', 'feat(control): auth gate, fixture honesty, evidence sandbox - LB-1 through LB-4 cleared',
  '--body-file', 'D:\\Repos\\keon-omega\\keon-docs\\pr-body.md',
], {
  cwd: 'D:\\Repos\\keon-omega\\keon-systems-pr',
  encoding: 'utf8',
  shell: false,
});

console.log('STDOUT:', result.stdout);
console.log('STDERR:', result.stderr);
console.log('EXIT:', result.status);
if (result.error) console.log('ERROR:', result.error.message);

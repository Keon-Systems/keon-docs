const fs = require('fs');
const { spawnSync } = require('child_process');

fs.writeFileSync('D:\\Repos\\keon-omega\\keon-docs\\node-test-out.txt', 'STARTED\n', 'utf8');

const r = spawnSync('D:\\nvm\\v24.7.0\\npm.cmd', ['--version'], {
  cwd: 'D:\\Repos\\keon-omega\\keon-systems-pr\\src\\Keon.Control.Website',
  encoding: 'utf8',
  env: { ...process.env, PATH: 'D:\\nvm\\v24.7.0;' + (process.env.PATH || '') }
});

fs.appendFileSync('D:\\Repos\\keon-omega\\keon-docs\\node-test-out.txt',
  `stdout: ${r.stdout}\nstderr: ${r.stderr}\nstatus: ${r.status}\nerror: ${r.error}\n`);

console.log('Done');

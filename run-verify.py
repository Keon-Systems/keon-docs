import subprocess
import os
import sys

CWD = r"D:\Repos\keon-omega\keon-systems-pr\src\Keon.Control.Website"
OUT = r"D:\Repos\keon-omega\keon-docs\verify-output.txt"

lines = []

def run(label, cmd):
    lines.append(f"\n{'='*60}")
    lines.append(f"[{label}] {cmd}")
    lines.append('='*60)
    try:
        r = subprocess.run(
            cmd, shell=True, cwd=CWD,
            capture_output=True, text=True, timeout=300,
            env={**os.environ, "PATH": os.environ.get("PATH", "") + r";D:\Repos\keon-omega\keon-systems-pr\src\Keon.Control.Website\node_modules\.bin"}
        )
        lines.append(r.stdout or "(no stdout)")
        if r.stderr:
            lines.append("STDERR: " + r.stderr)
        lines.append(f"EXIT: {r.returncode}")
        return r.returncode
    except subprocess.TimeoutExpired:
        lines.append("TIMEOUT")
        return -1
    except Exception as e:
        lines.append(f"ERROR: {e}")
        return -1

# 1. Install
run("npm install", "npm install")

# 2. Check installed
v = os.path.exists(os.path.join(CWD, "node_modules", "vitest"))
tl = os.path.exists(os.path.join(CWD, "node_modules", "@testing-library"))
lines.append(f"\nvitest in node_modules: {v}")
lines.append(f"@testing-library in node_modules: {tl}")

# 3. TypeScript
run("typecheck", "npx --no-install tsc --noEmit")

# 4. Lint
run("lint", "npx --no-install eslint src --max-warnings=0")

# 5. Tests
run("test", "npx --no-install vitest run --reporter=verbose")

# 6. Build
run("build", "npx --no-install next build")

output = "\n".join(lines)
with open(OUT, "w", encoding="utf-8") as f:
    f.write(output)
print(f"Written to {OUT}")
print(output[-3000:])

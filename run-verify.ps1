$CWD = "D:\Repos\keon-omega\keon-systems-pr\src\Keon.Control.Website"
$OUT = "D:\Repos\keon-omega\keon-docs\verify-output.txt"
$lines = @()

function Run-Step($label, $cmd) {
    $lines += ""
    $lines += "=" * 60
    $lines += "[$label] $cmd"
    $lines += "=" * 60
    $result = Invoke-Expression "cd '$CWD'; $cmd 2>&1" | Out-String
    $lines += $result
    $lines += "EXIT: $LASTEXITCODE"
    return $LASTEXITCODE
}

Push-Location $CWD

# 1. Install
$lines += ""; $lines += "=" * 60; $lines += "[npm install] npm install"; $lines += "=" * 60
$out = npm install 2>&1 | Out-String
$lines += $out; $lines += "EXIT: $LASTEXITCODE"

# 2. Check installed
$vt = Test-Path (Join-Path $CWD "node_modules\vitest")
$tl = Test-Path (Join-Path $CWD "node_modules\@testing-library")
$lines += "vitest in node_modules: $vt"
$lines += "@testing-library in node_modules: $tl"

# 3. TypeScript
$lines += ""; $lines += "=" * 60; $lines += "[typecheck] npx tsc --noEmit"; $lines += "=" * 60
$out = npx --no-install tsc --noEmit 2>&1 | Out-String
$lines += $out; $lines += "EXIT: $LASTEXITCODE"

# 4. Lint
$lines += ""; $lines += "=" * 60; $lines += "[lint] npx eslint src --max-warnings=0"; $lines += "=" * 60
$out = npx --no-install eslint src --max-warnings=0 2>&1 | Out-String
$lines += $out; $lines += "EXIT: $LASTEXITCODE"

# 5. Tests
$lines += ""; $lines += "=" * 60; $lines += "[test] npx vitest run --reporter=verbose"; $lines += "=" * 60
$out = npx --no-install vitest run --reporter=verbose 2>&1 | Out-String
$lines += $out; $lines += "EXIT: $LASTEXITCODE"

# 6. Build
$lines += ""; $lines += "=" * 60; $lines += "[build] npm run build"; $lines += "=" * 60
$out = npm run build 2>&1 | Out-String
$lines += $out; $lines += "EXIT: $LASTEXITCODE"

Pop-Location

$content = $lines -join "`n"
$content | Out-File -FilePath $OUT -Encoding utf8
Write-Host "Written to $OUT"
Write-Host ($content[-3000..-1] -join "")

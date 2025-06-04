Write-Host ""
Write-Host "----------------------------" -ForegroundColor Cyan
Write-Host "PAPI SDK Copy Preview Script" -ForegroundColor Cyan
Write-Host "----------------------------" -ForegroundColor Cyan
Write-Host ""

$ErrorActionPreference = "Stop"

# Define paths
$root = Split-Path -Parent $MyInvocation.MyCommand.Definition
$previewRoot = Join-Path $root "fern\.preview"
$previewSrc = Join-Path $previewRoot "fern-csharp-sdk\src"
$targetSrc = Join-Path $root "src"
$fernignorePath = Join-Path $root ".fernignore"

# Clean start: remove the whole .preview folder (just in case)
if (Test-Path $previewRoot) {
    Write-Host "Removing existing .preview folder: $previewRoot"
    Remove-Item -Recurse -Force $previewRoot
}

Write-Host "Running Fern CLI to generate preview SDK..." -ForegroundColor Yellow

# Correctly invoke Fern CLI
& fern generate --group csharp-sdk --api api --preview

if ($LASTEXITCODE -ne 0) {
    Write-Error "Fern generation failed. Exiting script."
    exit 1
}

Write-Host "Fern CLI generation complete." -ForegroundColor Green

# Parse .fernignore
$ignoredPaths = @()
if (Test-Path $fernignorePath) {
    $ignoredPaths = Get-Content $fernignorePath | Where-Object {
        $_ -and -not $_.Trim().StartsWith("#")
    } | ForEach-Object {
        ($_ -replace "/", "\").Trim()
    }
}

# Define subfolders to sync
$foldersToSync = @("Payroc", "Payroc.Test")

foreach ($folder in $foldersToSync) {
    $targetFolder = Join-Path $targetSrc $folder
    $sourceFolder = Join-Path $previewSrc $folder

    if (-Not (Test-Path $sourceFolder)) {
        Write-Error "Source folder does not exist: $sourceFolder"
        continue
    }

    # Delete target contents EXCEPT ignored paths
    if (Test-Path $targetFolder) {
        Write-Host "Cleaning target folder: $targetFolder"

        Get-ChildItem -Recurse -Force $targetFolder | ForEach-Object {
            $relativePath = $_.FullName.Substring($root.Length + 1)
            if ($ignoredPaths -notcontains $relativePath) {
                Remove-Item -Recurse -Force $_.FullName -ErrorAction SilentlyContinue
            }
        }
    }

    # Copy contents of source folder EXCEPT ignored files
    Write-Host "Copying contents of $sourceFolder to $targetFolder"
    Get-ChildItem -Path $sourceFolder -Recurse -Force | ForEach-Object {
        $relativePath = $_.FullName.Substring($previewSrc.Length + 1)
        $targetPath = Join-Path $targetSrc $relativePath

        $shouldIgnore = $ignoredPaths | Where-Object { $relativePath -like "$_" }
        if (-not $shouldIgnore) {
            $targetDir = Split-Path $targetPath
            if (-not (Test-Path $targetDir)) {
                New-Item -ItemType Directory -Path $targetDir -Force | Out-Null
            }

            Copy-Item -Path $_.FullName -Destination $targetPath -Force
        }
    }
}

# Cleanup: remove the whole .preview folder
if (Test-Path $previewRoot) {
    Write-Host "Removing .preview folder after copy: $previewRoot"
    Remove-Item -Recurse -Force $previewRoot
}

Write-Host "`n✅ Sync complete."

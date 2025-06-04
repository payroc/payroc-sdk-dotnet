$ErrorActionPreference = "Stop"

# Define paths
$root = Split-Path -Parent $MyInvocation.MyCommand.Definition
$previewRoot = Join-Path $root "fern\.preview"
$previewSrc = Join-Path $previewRoot "fern-csharp-sdk\src"
$targetSrc = Join-Path $root "src"

# Define subfolders to sync
$foldersToSync = @("Payroc", "Payroc.Test")

foreach ($folder in $foldersToSync) {
    $targetFolder = Join-Path $targetSrc $folder
    $sourceFolder = Join-Path $previewSrc $folder

    if (-Not (Test-Path $sourceFolder)) {
        Write-Error "Source folder does not exist: $sourceFolder"
        continue
    }

    if (Test-Path $targetFolder) {
        Write-Host "Removing old target folder: $targetFolder"
        Remove-Item -Recurse -Force $targetFolder
    }

    Write-Host "Copying $sourceFolder to $targetFolder"
    Copy-Item -Recurse -Force $sourceFolder $targetFolder
}

# Cleanup: remove the whole .preview folder
if (Test-Path $previewRoot) {
    Write-Host "Removing entire .preview folder: $previewRoot"
    Remove-Item -Recurse -Force $previewRoot
}

Write-Host "`n✅ Sync complete."

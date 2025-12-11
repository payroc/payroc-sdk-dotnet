# Setup VS Code Extensions for Payroc SDK Development (Windows PowerShell)
# This script installs the recommended extensions for developing the Payroc .NET SDK

Write-Host "Installing VS Code extensions for Payroc SDK development..." -ForegroundColor Green

# Check if code command is available
$codeExists = Get-Command code -ErrorAction SilentlyContinue
if (-not $codeExists) {
    Write-Host "Error: VS Code command code not found in PATH." -ForegroundColor Red
    Write-Host "Please ensure VS Code is installed and the code command is available." -ForegroundColor Yellow
    Write-Host "You can add it to PATH by opening VS Code and running Shell Command: Install code command in PATH from the command palette." -ForegroundColor Yellow
    exit 1
}

# Install C# extension
Write-Host "Installing C# extension..." -ForegroundColor Cyan
code --install-extension ms-dotnettools.csharp --force
if ($LASTEXITCODE -eq 0) {
    Write-Host "C# extension installed" -ForegroundColor Green
} else {
    Write-Host "Failed to install C# extension" -ForegroundColor Red
}

# Install C# Dev Kit
Write-Host "Installing C# Dev Kit..." -ForegroundColor Cyan
code --install-extension ms-dotnettools.csdevkit --force
if ($LASTEXITCODE -eq 0) {
    Write-Host "C# Dev Kit installed" -ForegroundColor Green
} else {
    Write-Host "Failed to install C# Dev Kit" -ForegroundColor Red
}

Write-Host ""
Write-Host "Extensions installation complete!" -ForegroundColor Green
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "1. Reload VS Code (Ctrl+Shift+P > Reload Window)" -ForegroundColor White
Write-Host "2. Run tests using the Test Explorer in the sidebar" -ForegroundColor White
Write-Host ""
Write-Host "For more information, see CONTRIBUTORS.md" -ForegroundColor White

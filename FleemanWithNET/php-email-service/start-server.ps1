# PowerShell script to start PHP Email Service
Write-Host "Starting PHP Email Service..." -ForegroundColor Green
Write-Host ""

# Check if PHP is installed
try {
    $phpVersion = php -v 2>&1 | Select-Object -First 1
    Write-Host "PHP Version: $phpVersion" -ForegroundColor Yellow
} catch {
    Write-Host "Error: PHP is not installed or not in PATH" -ForegroundColor Red
    Write-Host "Please install PHP and add it to your system PATH" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

# Change to script directory
Set-Location $PSScriptRoot

Write-Host "Starting server on http://localhost:8000" -ForegroundColor Cyan
Write-Host "Press Ctrl+C to stop the server" -ForegroundColor Yellow
Write-Host ""

try {
    php -S localhost:8000
} catch {
    Write-Host "Error starting PHP server: $($_.Exception.Message)" -ForegroundColor Red
    Read-Host "Press Enter to exit"
}

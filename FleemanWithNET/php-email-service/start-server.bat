@echo off
echo Starting PHP Email Service...
echo.
echo This will start the PHP development server on http://localhost:8000
echo Press Ctrl+C to stop the server
echo.
cd /d "%~dp0"
php -S localhost:8000
pause

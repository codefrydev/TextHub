#!/bin/bash

# Bash script to run TextHub UI tests with Playwright

echo "TextHub UI Tests with Playwright"
echo "================================="

# Check if .NET is installed
if ! command -v dotnet &> /dev/null; then
    echo "Error: .NET is not installed or not in PATH"
    exit 1
fi

echo "Using .NET version: $(dotnet --version)"

# Build the test project
echo ""
echo "Building test project..."
dotnet build

if [ $? -ne 0 ]; then
    echo "Build failed!"
    exit 1
fi

# Install Playwright browsers
echo ""
echo "Installing Playwright browsers..."
pwsh bin/Debug/net9.0/playwright.ps1 install

if [ $? -ne 0 ]; then
    echo "Playwright browser installation failed!"
    exit 1
fi

# Check if TextHub is running
echo ""
echo "Checking if TextHub is running..."
if curl -s -k https://localhost:5001 > /dev/null; then
    echo "TextHub is running!"
else
    echo "TextHub is not running. Please start it with 'dotnet run' in the TextHub directory."
    echo "Then run this script again."
    exit 1
fi

# Run tests
echo ""
echo "Running UI tests..."
dotnet test --logger "console;verbosity=normal"

if [ $? -eq 0 ]; then
    echo ""
    echo "All tests passed!"
else
    echo ""
    echo "Some tests failed. Check the output above for details."
fi

echo ""
echo "Test results and screenshots are available in the 'test-results' directory."

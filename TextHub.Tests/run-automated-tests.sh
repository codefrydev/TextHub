#!/bin/bash

# Bash script to run tests with automated server startup and visible browser

echo "TextHub Automated UI Tests with Playwright"
echo "==========================================="

# Set environment variables for visible testing
export HEADLESS=false
export TEST_ENVIRONMENT=local

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

# Run tests (the GlobalSetup will automatically start the TextHub server)
echo ""
echo "Running automated UI tests..."
echo "The TextHub server will be started automatically!"
echo "You should see browser windows open during the tests!"

if [ $# -gt 0 ]; then
    # Run specific test if filter provided
    filter=$1
    echo "Running tests matching: $filter"
    dotnet test --filter "$filter" --logger "console;verbosity=normal"
else
    # Run all tests
    echo "Running all tests..."
    dotnet test --logger "console;verbosity=normal"
fi

if [ $? -eq 0 ]; then
    echo ""
    echo "All tests passed!"
else
    echo ""
    echo "Some tests failed. Check the output above for details."
fi

echo ""
echo "Test run completed!"
echo "Test results and screenshots are available in the 'test-results' directory."

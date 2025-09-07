#!/bin/bash

# TextHub UI Test Runner Script
# This script runs the comprehensive UI test suite

set -e

echo "🚀 Starting TextHub UI Tests..."
echo "=================================="

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Configuration
BASE_URL="http://localhost:5099"
TEST_PROJECT="TextHub.Tests.csproj"
SCREENSHOTS_DIR="screenshots"

# Create screenshots directory
mkdir -p "$SCREENSHOTS_DIR"

echo -e "${BLUE}📋 Test Configuration:${NC}"
echo "  • Base URL: $BASE_URL"
echo "  • Test Project: $TEST_PROJECT"
echo "  • Screenshots: $SCREENSHOTS_DIR"
echo ""

# Check if the application is running
echo -e "${YELLOW}🔍 Checking if TextHub is running...${NC}"
if curl -s --head "$BASE_URL" | head -n 1 | grep -q "200 OK"; then
    echo -e "${GREEN}✅ TextHub is running at $BASE_URL${NC}"
else
    echo -e "${RED}❌ TextHub is not running at $BASE_URL${NC}"
    echo -e "${YELLOW}💡 Please start the application first:${NC}"
    echo "   cd TextHub && dotnet run"
    exit 1
fi

# Install Playwright browsers if not already installed
echo -e "${YELLOW}🎭 Checking Playwright browsers...${NC}"
if ! dotnet run --project . -- --help > /dev/null 2>&1; then
    echo -e "${YELLOW}📦 Installing Playwright browsers...${NC}"
    pwsh -Command "dotnet build && dotnet test --list-tests | Out-Null; pwsh -Command '& {dotnet run --project . -- --help}' | Out-Null"
fi

# Run different test categories
echo ""
echo -e "${BLUE}🧪 Running UI Test Suite...${NC}"
echo "=================================="

# Function to run test category
run_test_category() {
    local category=$1
    local description=$2
    
    echo -e "${YELLOW}📝 Running $description...${NC}"
    
    if dotnet test --filter "FullyQualifiedName~$category" --logger "console;verbosity=normal" --collect:"XPlat Code Coverage"; then
        echo -e "${GREEN}✅ $description completed successfully${NC}"
    else
        echo -e "${RED}❌ $description failed${NC}"
        return 1
    fi
    echo ""
}

# Run all test categories
run_test_category "HomePageUITests" "Home Page UI Tests"
run_test_category "TextCaseToolsUITests" "Text Case Tools UI Tests"
run_test_category "TextAnalysisToolsUITests" "Text Analysis Tools UI Tests"
run_test_category "NavigationAndResponsiveUITests" "Navigation and Responsive UI Tests"
run_test_category "AccessibilityAndPerformanceUITests" "Accessibility and Performance Tests"

# Run all tests together
echo -e "${YELLOW}🎯 Running All UI Tests Together...${NC}"
if dotnet test --logger "console;verbosity=normal" --collect:"XPlat Code Coverage"; then
    echo -e "${GREEN}🎉 All UI tests completed successfully!${NC}"
else
    echo -e "${RED}💥 Some tests failed${NC}"
    exit 1
fi

echo ""
echo -e "${BLUE}📊 Test Summary:${NC}"
echo "=================================="
echo -e "${GREEN}✅ Test execution completed${NC}"
echo -e "${BLUE}📸 Screenshots saved to: $SCREENSHOTS_DIR${NC}"
echo -e "${BLUE}📈 Code coverage reports generated${NC}"

# Show screenshots if any were created
if [ -d "$SCREENSHOTS_DIR" ] && [ "$(ls -A $SCREENSHOTS_DIR)" ]; then
    echo ""
    echo -e "${YELLOW}📸 Generated Screenshots:${NC}"
    ls -la "$SCREENSHOTS_DIR"/*.png 2>/dev/null || echo "No screenshots found"
fi

echo ""
echo -e "${GREEN}🎊 UI Testing Complete!${NC}"

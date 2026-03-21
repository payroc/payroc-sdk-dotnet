# Contributing to Payroc API .NET SDK

## Getting Started

### Prerequisites

To build and test this source code, you'll need:

- **.NET SDK 8.0 or higher** - [Download](https://dotnet.microsoft.com/download)
- **Git** - [Download](https://git-scm.com/)
- A code editor or IDE (e.g., Visual Studio, Visual Studio Code, or JetBrains Rider)

### Building

Clone the repository and build the solution:

```bash
git clone https://github.com/payroc/payroc-sdk-dotnet.git
cd payroc-sdk-dotnet
dotnet build
```

### Project Structure

- `src/Payroc/` - Main SDK library
- `src/Payroc.Test/` - Unit tests with mocked dependencies
- `src/Payroc.TestCommon/` - Shared test infrastructure and test definitions
- `src/Payroc.TestSmoke/` - Single smoke test for quick API validation
- `src/Payroc.TestFunctional/` - Comprehensive integration tests (~125 files)
- `src/Payroc.TestHarness/` - Test harness for debugging

#### Test Organization

The test suite uses a **file linking architecture** to maintain a single source of truth:

```
src/
├── Payroc.TestCommon/          # Shared test infrastructure
│   ├── Tests/
│   │   ├── CardPayments/Refunds/CreateTests.cs  # Contains SmokeTest() method
│   │   └── [All other tests]   # ~125 test files total
│   ├── Factories/              # Test data factories
│   ├── TestData/               # JSON test data files
│   ├── GlobalFixture.cs
│   ├── TestClients.cs
│   └── Data.cs
│
├── Payroc.TestSmoke/           # Links to single test file
└── Payroc.TestFunctional/      # Links to all test files
```

**Benefits:**
- ✅ Single source of truth for all test code
- ✅ No code duplication
- ✅ Easy maintenance - update once in `TestCommon`
- ✅ Ultra-fast smoke test for quick validation
- ✅ Comprehensive functional tests for thorough testing

## Testing

### Prerequisites for Running Tests in VS Code

To run and debug tests directly in VS Code, install the following extensions:

- **[C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)** - Base C# language support and IntelliSense
- **[C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)** - Enhanced development tools with integrated test explorer

#### Quick Setup

We've provided setup scripts to automatically install these extensions. Choose the appropriate script for your operating system:

**Windows (PowerShell):**
```powershell
.\vscode-scripts\setup-extensions.ps1
```

**macOS/Linux (Bash):**
```bash
bash vscode-scripts/setup-extensions.sh
```

After running the script, reload VS Code (`Ctrl+Shift+P` > `Reload Window`) to activate the extensions.

### Test Framework

This project uses **NUnit** as the testing framework, with the following test runners and utilities:

- **Microsoft.NET.Test.Sdk** - Test runner
- **NUnit3TestAdapter** - NUnit test adapter
- **coverlet.collector** - Code coverage collection

### Running Tests

Execute all tests:

```bash
dotnet test
```

Run only unit tests:

```bash
dotnet test src/Payroc.Test/Payroc.Test.csproj
```

Run smoke test (ultra-fast):

```bash
dotnet test src/Payroc.TestSmoke/Payroc.TestSmoke.csproj
```

Run functional tests:

```bash
dotnet test src/Payroc.TestFunctional/Payroc.TestFunctional.csproj
```

Run tests with coverage:

```bash
dotnet test /p:CollectCoverage=true
```

### Test Categories

#### Unit Tests (`Payroc.Test`)
- **Purpose**: Fast, isolated tests for individual components
- **Dependencies**: Uses WireMock for HTTP mocking
- **Location**: `src/Payroc.Test/`
- **Examples**: JSON serialization, pagination, query builders, error handling

#### Smoke Test (`Payroc.TestSmoke`)
- **Purpose**: Ultra-fast validation that core API functionality works
- **Location**: Links to `TestCommon/Tests/CardPayments/Refunds/CreateTests.cs`
- **Count**: 1 test method (`SmokeTest()` in CreateTests.cs)
- **Test**: Creates an unreferenced refund to validate basic API connectivity
- **Environment**: Makes real API calls to UAT
- **Why this test**: Exercises multiple critical paths in one call:
  - API authentication (OAuth token retrieval)
  - Request serialization (JSON encoding)
  - HTTP communication (POST request)
  - Payment processing (UAT backend)
  - Response deserialization (JSON decoding)
  - Transaction status validation (business logic)
- **Based on**: Ruby SDK smoke test at `test/integration/card_payments/refunds/create_test.rb`
- **Note**: This same test is also included in TestFunctional (duplicated via file linking)

#### Functional Tests (`Payroc.TestFunctional`)
- **Purpose**: Comprehensive testing of all API endpoints, scenarios, and workflows
- **Location**: Links to `TestCommon/Tests/` (all test files)
- **Count**: ~126 test files covering all API endpoints (includes the smoke test)
- **Environment**: Makes real API calls to UAT
- **Coverage**:
  - All API endpoint operations (Create, Retrieve, Update, Delete, List)
  - Payment processing scenarios (approvals, declines, refunds, captures)
  - Different payment methods (card, bank transfer, tokens)
  - Boarding and merchant management
  - Funding and settlement operations
  - Event subscriptions and notifications
  - Payment links and repeat payments
  - Includes the smoke test for complete coverage

#### Test Harness (`Payroc.TestHarness`)
- **Purpose**: Debugging and manual testing utility
- **Location**: `src/Payroc.TestHarness/`

### Environment Configuration

Integration tests (Smoke and Functional) require the following environment variables:

- `PAYROC_API_KEY_GENERIC` or `PAYROC_API_KEY` - API key for generic operations
- `PAYROC_API_KEY_PAYMENTS` - API key for payment operations
- `TERMINAL_ID_AVS` - Terminal ID with AVS enabled
- `TERMINAL_ID_NO_AVS` - Terminal ID without AVS
- `TERMINAL_ID_AVS_PAYMENTS_BANK_TRANSFER` - Bank transfer terminal ID
- `TERMINAL_ID_AVS_PAYMENTS_BANK_TRANSFER_PAD` - Bank transfer PAD terminal ID

**Test Execution Details:**
- **Environment**: UAT (User Acceptance Testing)
- **Test Framework**: NUnit 4.4.0
- **Target Framework**: .NET 8.0
- **Parallelization**: Enabled at fixture scope
- **Test Type**: Integration tests making real API calls to UAT

### Adding New Tests

**Note**: The smoke test is the `SmokeTest()` method in `CardPayments/Refunds/CreateTests.cs`. This test is included in both TestSmoke (for fast validation) and TestFunctional (for comprehensive coverage). All new tests should be functional tests added to the Tests folder.

#### Adding a Functional Test
1. Create test file in `TestCommon/Tests/[Category]/[Feature]/`
2. Use descriptive test method names (e.g., `Payments_Create_Retrieve_Decline()`)
3. Test automatically included in `Payroc.TestFunctional`

#### Adding a Unit Test
1. Create test file in `Payroc.Test/[Category]/`
2. Use WireMock to mock HTTP responses
3. Follow existing patterns for test organization

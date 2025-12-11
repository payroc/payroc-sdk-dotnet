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
git clone https://github.com/payroc/papi-sdk-dotnet.git
cd papi-sdk-dotnet
dotnet build
```

### Project Structure

- `src/Payroc/` - Main SDK library
- `src/Payroc.Test/` - Unit tests
- `src/Payroc.TestFunctional/` - Functional tests
- `src/Payroc.TestHarness/` - Test harness for debugging

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

Run only functional tests:

```bash
dotnet test src/Payroc.TestFunctional/Payroc.TestFunctional.csproj
```

Run tests with coverage:

```bash
dotnet test /p:CollectCoverage=true
```

### Test Categories

- **Unit Tests** (`src/Payroc.Test/`) - Fast, isolated tests for individual components
- **Functional Tests** (`src/Payroc.TestFunctional/`) - Integration tests that validate end-to-end functionality
- **Test Harness** (`src/Payroc.TestHarness/`) - Debugging and manual testing utility

# Payroc SDK NuGet Package Tests

This project is for **exploratory testing against the real, published NuGet package** `Payroc`, rather than the local SDK source code.

## How It Works

- This project references `Payroc.TestCommon` (project reference), which provides shared factories, test data, and helpers.
- `Payroc.TestCommon` declares a **NuGet PackageReference** to `Payroc`. There is **no project reference** to `Payroc.csproj` anywhere in this project's dependency chain.
- This means all tests run against the published NuGet package — not the local build.

## How to Verify

Run the following command to confirm that `Payroc` is resolved as a NuGet package:

```bash
dotnet list src/Payroc.TestNuget package
```

You should see `Payroc` listed as a top-level or transitive NuGet package — **not** as a project reference.

## How to Update the NuGet Version

Edit the `<PackageReference Include="Payroc" Version="..." />` in `src/Payroc.TestCommon/Payroc.TestCommon.csproj` to the version you want to test, then restore:

```bash
dotnet restore src/Payroc.TestNuget/Payroc.TestNuget.csproj
```

## Comparison with TestFunctional

| Project | Payroc Source | Purpose |
|---|---|---|
| `Payroc.TestFunctional` | Local project reference (`Payroc.csproj`) | CI / regression tests against local build |
| `Payroc.TestNuget` | NuGet package (`Payroc`) | Exploratory testing against published package |

Both share the same factories, test data, and helpers via `Payroc.TestCommon`.

## Environment Setup

The following environment variables must be set (same as `Payroc.TestFunctional`):

- `PAYROC_API_KEY_GENERIC`
- `PAYROC_API_KEY_PAYMENTS`
- `PAYROC_API_KEY_PAYMENTS_BANK_TRANSFER`
- `TERMINAL_ID_AVS`
- `TERMINAL_ID_NO_AVS`
- `TERMINAL_ID_AVS_PAYMENTS_BANK_TRANSFER`

## Running

```bash
dotnet test src/Payroc.TestNuget/Payroc.TestNuget.csproj
```

using Payroc;

namespace Payroc.TestCommon;

public static class TestClients
{
    public static readonly PayrocClient Generic;
    public static readonly PayrocClient Payments;
    public static readonly string TerminalIdAvs;
    public static readonly string TerminalIdNoAvs;
    public static readonly string TerminalIdBank;
    public static readonly string TerminalIdBankPad;

    static TestClients()
    {
        Generic = CreateClient(GetEnvWithFallback("PAYROC_API_KEY_GENERIC"));
        Payments = CreateClient(GetEnvWithFallback("PAYROC_API_KEY_PAYMENTS"));
        TerminalIdAvs = GetEnv("TERMINAL_ID_AVS");
        TerminalIdNoAvs = GetEnv("TERMINAL_ID_NO_AVS");
        TerminalIdBank = GetEnv("TERMINAL_ID_AVS_PAYMENTS_BANK_TRANSFER");
        TerminalIdBankPad = GetEnv("TERMINAL_ID_AVS_PAYMENTS_BANK_TRANSFER_PAD");
    }

    private static PayrocClient CreateClient(string apiKey)
    {
        var environment = GetCustomEnvironment();
        return new PayrocClient(apiKey, new ClientOptions { Environment = environment });
    }

    private static PayrocEnvironment GetCustomEnvironment()
    {
        var apiBaseUrl = Environment.GetEnvironmentVariable("PAYROC_API_BASE_URL");
        var identityBaseUrl = Environment.GetEnvironmentVariable("PAYROC_IDENTITY_BASE_URL");

        // If custom URLs are provided, use them
        if (!string.IsNullOrEmpty(apiBaseUrl) && !string.IsNullOrEmpty(identityBaseUrl))
        {
            return new PayrocEnvironment
            {
                Api = apiBaseUrl,
                Identity = identityBaseUrl
            };
        }

        // Otherwise, fall back to UAT
        return PayrocEnvironment.Uat;
    }

    private static string GetEnv(string name)
        => Environment.GetEnvironmentVariable(name)
            ?? throw new InvalidOperationException($"Environment variable '{name}' is not set.");

    private static string GetEnvWithFallback(string name)
        => Environment.GetEnvironmentVariable(name)
            ?? Environment.GetEnvironmentVariable("PAYROC_API_KEY")
            ?? throw new InvalidOperationException($"Environment variable '{name}' and fallback 'PAYROC_API_KEY' are not set.");
}

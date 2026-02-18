using Payroc;

[SetUpFixture]
public class GlobalFixture
{
    public static readonly PayrocClient Generic;
    public static readonly PayrocClient Payments;
    public static readonly PayrocClient PaymentsBank;
    public static readonly string TerminalIdAvs;
    public static readonly string TerminalIdNoAvs;
    public static readonly string TerminalIdBank;

    static GlobalFixture()
    {
        Generic = CreateClient(GetEnv("PAYROC_API_KEY_GENERIC"));
        Payments = CreateClient(GetEnv("PAYROC_API_KEY_PAYMENTS"));
        PaymentsBank = CreateClient(GetEnv("PAYROC_API_KEY_PAYMENTS_BANK_TRANSFER"));
        TerminalIdAvs = GetEnv("TERMINAL_ID_AVS");
        TerminalIdNoAvs = GetEnv("TERMINAL_ID_NO_AVS");
        TerminalIdBank = GetEnv("TERMINAL_ID_AVS_PAYMENTS_BANK_TRANSFER");
    }

    private static PayrocClient CreateClient(string apiKey)
        => new(apiKey, new ClientOptions { Environment = PayrocEnvironment.Uat });

    private static string GetEnv(string name)
        => Environment.GetEnvironmentVariable(name)
            ?? throw new InvalidOperationException($"Environment variable '{name}' is not set.");
}

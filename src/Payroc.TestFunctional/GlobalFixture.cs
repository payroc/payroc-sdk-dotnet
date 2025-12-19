using Payroc;

[SetUpFixture]
public class GlobalFixture
{
    public static readonly PayrocClient Payments;
    public static readonly PayrocClient Generic;
    public static readonly string TerminalIdAvs;
    public static readonly string TerminalIdNoAvs;

    static GlobalFixture()
    {
        Generic = CreateClient(GetEnv("PAYROC_API_KEY_GENERIC"));
        Payments = CreateClient(GetEnv("PAYROC_API_KEY_PAYMENTS"));
        TerminalIdAvs = GetEnv("TERMINAL_ID_AVS");
        TerminalIdNoAvs = GetEnv("TERMINAL_ID_NO_AVS");
    }

    private static PayrocClient CreateClient(string apiKey) 
        => new(apiKey, new ClientOptions { Environment = PayrocEnvironment.Uat });

    private static string GetEnv(string name) 
        => Environment.GetEnvironmentVariable(name) 
            ?? throw new InvalidOperationException($"Environment variable '{name}' is not set.");
}

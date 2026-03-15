using Payroc;

[SetUpFixture]
public class GlobalFixture
{
    public static PayrocClient Generic => TestClients.Generic;
    public static PayrocClient Payments => TestClients.Payments;
    public static PayrocClient PaymentsBank => TestClients.PaymentsBank;
    public static string TerminalIdAvs => TestClients.TerminalIdAvs;
    public static string TerminalIdNoAvs => TestClients.TerminalIdNoAvs;
    public static string TerminalIdBank => TestClients.TerminalIdBank;
    public static string TerminalIdBankPad => TestClients.TerminalIdBankPad;
}

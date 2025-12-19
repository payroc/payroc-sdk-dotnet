using Payroc.Core;

namespace Payroc.BankTransferPayments;

public partial class BankTransferPaymentsClient
{
    private RawClient _client;

    internal BankTransferPaymentsClient(RawClient client)
    {
        try
        {
            _client = client;
            Payments = new Payroc.BankTransferPayments.Payments.PaymentsClient(_client);
            Refunds = new Payroc.BankTransferPayments.Refunds.RefundsClient(_client);
        }
        catch (Exception ex)
        {
            client.Options.ExceptionHandler?.CaptureException(ex);
            throw;
        }
    }

    public Payroc.BankTransferPayments.Payments.PaymentsClient Payments { get; }

    public Payroc.BankTransferPayments.Refunds.RefundsClient Refunds { get; }
}

using Payroc.Core;

namespace Payroc.CardPayments;

public partial class CardPaymentsClient
{
    private RawClient _client;

    internal CardPaymentsClient(RawClient client)
    {
        try
        {
            _client = client;
            Payments = new Payroc.CardPayments.Payments.PaymentsClient(_client);
            Refunds = new Payroc.CardPayments.Refunds.RefundsClient(_client);
        }
        catch (Exception ex)
        {
            client.Options.ExceptionHandler?.CaptureException(ex);
            throw;
        }
    }

    public Payroc.CardPayments.Payments.PaymentsClient Payments { get; }

    public Payroc.CardPayments.Refunds.RefundsClient Refunds { get; }
}

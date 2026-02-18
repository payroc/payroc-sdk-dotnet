using Payroc.Core;

namespace Payroc.CardPayments;

public partial class CardPaymentsClient : ICardPaymentsClient
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

    public Payroc.CardPayments.Payments.IPaymentsClient Payments { get; }

    public Payroc.CardPayments.Refunds.IRefundsClient Refunds { get; }
}

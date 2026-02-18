using Payroc.Core;
using Payroc.RepeatPayments.PaymentPlans;
using Payroc.RepeatPayments.Subscriptions;

namespace Payroc.RepeatPayments;

public partial class RepeatPaymentsClient : IRepeatPaymentsClient
{
    private RawClient _client;

    internal RepeatPaymentsClient(RawClient client)
    {
        try
        {
            _client = client;
            PaymentPlans = new PaymentPlansClient(_client);
            Subscriptions = new SubscriptionsClient(_client);
        }
        catch (Exception ex)
        {
            client.Options.ExceptionHandler?.CaptureException(ex);
            throw;
        }
    }

    public IPaymentPlansClient PaymentPlans { get; }

    public ISubscriptionsClient Subscriptions { get; }
}

using Payroc.RepeatPayments.PaymentPlans;
using Payroc.RepeatPayments.Subscriptions;

namespace Payroc.RepeatPayments;

public partial interface IRepeatPaymentsClient
{
    public IPaymentPlansClient PaymentPlans { get; }
    public ISubscriptionsClient Subscriptions { get; }
}

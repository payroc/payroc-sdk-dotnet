namespace Payroc.CardPayments;

public partial interface ICardPaymentsClient
{
    public Payroc.CardPayments.Payments.IPaymentsClient Payments { get; }
    public Payroc.CardPayments.Refunds.IRefundsClient Refunds { get; }
}

namespace Payroc.BankTransferPayments;

public partial interface IBankTransferPaymentsClient
{
    public Payroc.BankTransferPayments.Payments.IPaymentsClient Payments { get; }
    public Payroc.BankTransferPayments.Refunds.IRefundsClient Refunds { get; }
}

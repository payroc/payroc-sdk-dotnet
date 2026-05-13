using Payroc;
using Payroc.BankTransferPayments.Payments;

namespace Payroc.TestCommon.HelperMethods.BankTransfer;

internal static class PadPaymentMethodFactory
{
    // PAD account details known to be accepted by the UAT API.
    // Expressed here rather than relying on BankTransferPaymentRequest.json so the payment
    // method type is always PAD regardless of what the TestData refresh writes to that file.
    internal static BankTransferPaymentRequestPaymentMethod Create() =>
        new BankTransferPaymentRequestPaymentMethod.Pad(
            new PadPayload
            {
                NameOnAccount = "Sarah Hazel Hopper",
                AccountNumber = "1234567890",
                TransitNumber = "76543",
                InstitutionNumber = "543",
                AccountType = PadPayloadAccountType.Checking
            }
        );
}

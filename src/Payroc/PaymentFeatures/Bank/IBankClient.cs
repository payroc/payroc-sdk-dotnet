using Payroc;

namespace Payroc.PaymentFeatures.Bank;

public partial interface IBankClient
{
    /// <summary>
    /// Use this method to verify a customer's bank account details.
    ///
    /// In the request, send the customer's bank account details. Our gateway can verify the following types of bank details:
    /// - Automated Clearing House (ACH) details
    /// - Pre-Authorized Debit (PAD) details
    ///
    /// In the response, our gateway indicates if the account details are valid and if you should use them in follow-on actions.
    /// </summary>
    WithRawResponseTask<BankAccountVerificationResult> VerifyAsync(
        BankAccountVerificationRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

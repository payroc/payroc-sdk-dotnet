using Payroc;

namespace Payroc.ApplePaySessions;

public partial interface IApplePaySessionsClient
{
    /// <summary>
    /// Use this method to start an Apple Pay session for your merchant.
    ///
    /// In the response, we return the startSessionObject that you send to Apple when you retrieve the cardholder's encrypted payment details.
    ///
    /// **Note:** For more information about how to integrate with Apple Pay, go to [Apple Pay](https://docs.payroc.com/guides/take-payments/apple-pay).
    /// </summary>
    WithRawResponseTask<ApplePayResponseSession> CreateAsync(
        ApplePaySessions request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

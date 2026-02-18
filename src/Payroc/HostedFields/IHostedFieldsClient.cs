using Payroc;

namespace Payroc.HostedFields;

public partial interface IHostedFieldsClient
{
    /// <summary>
    /// Use this method to create a Hosted Fields session token. You need to generate a new session token each time you load Hosted Fields on a webpage.
    ///
    /// In your request, you need to indicate whether the merchant is using Hosted Fields to run a sale, save payment details, or update saved payment details.
    ///
    /// In the response, our gateway returns the session token and the time that it expires. You need the session token when you configure the JavaScript for Hosted Fields.
    ///
    /// For more information about adding Hosted Fields to a webpage, go to [Hosted Fields](https://docs.payroc.com/guides/take-payments/hosted-fields).
    /// </summary>
    WithRawResponseTask<HostedFieldsCreateSessionResponse> CreateAsync(
        HostedFieldsCreateSessionRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

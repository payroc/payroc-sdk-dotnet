using Payroc;
using Payroc.Core;

namespace Payroc.Tokenization.SecureTokens;

public partial interface ISecureTokensClient
{
    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of secure tokens.
    ///
    /// **Note:** If you want to view the details of a specific secure token and you have its secureTokenId, use our [Retrieve Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/retrieve) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for secure tokens by customer or by the first four digits of a card number.
    ///
    /// Our gateway returns information about the following for each secure token in the list:
    ///
    ///   -	Payment details that the secure token represents.
    ///   -	Customer details, including shipping and billing addresses.
    ///   -	Secure token that you can use to carry out transactions.
    ///
    ///   For each secure token, we also return the secureTokenId, which you can use to perform follow-on actions.
    /// </summary>
    Task<PayrocPager<SecureTokenWithAccountType>> ListAsync(
        ListSecureTokensRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to create a secure token that represents a customer's payment details.
    ///
    /// When you create a secure token, you need to generate and provide a secureTokenId that you use to run follow-on actions:
    /// - [Retrieve Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/retrieve) – View the details of the secure token.
    /// - [Delete Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/delete) – Delete the secure token.
    /// - [Update Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/partially-update) – Update the details of the secure token.
    /// - [Update Account Details](https://docs.payroc.com/api/schema/tokenization/secure-tokens/update-account) – Update the secure token with the details from a single-use token.
    ///
    /// **Note:** If you don't generate a secureTokenId to identify the token, our gateway generates a unique identifier and returns it in the response.
    ///
    /// If the request is successful, our gateway returns a token that the merchant can use in transactions instead of the customer's sensitive payment details, for example, when they [run a sale](https://docs.payroc.com/api/schema/card-payments/payments/create).
    /// </summary>
    WithRawResponseTask<SecureToken> CreateAsync(
        TokenizationRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a secure token.
    ///
    /// To retrieve a secure token, you need its secureTokenID, which you sent in the request of the [Create Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/create) method.
    ///
    /// **Note:** If you don't have the secureTokenId, use our [List Secure Tokens](https://docs.payroc.com/api/schema/tokenization/secure-tokens/list) method to search for the secure token.
    ///
    /// Our gateway returns the following information about the secure token:
    ///
    ///   -	Payment details that the secure token represents.
    ///   -	Customer details, including shipping and billing addresses.
    ///   -	Secure token that you can use to carry out transactions.
    /// </summary>
    WithRawResponseTask<SecureTokenWithAccountType> RetrieveAsync(
        RetrieveSecureTokensRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to delete a secure token and its related payment details from our vault.
    ///
    /// To delete a secure token, you need its secureTokenId, which you sent in the request of the [Create Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/create) method.
    ///
    /// **Note:** If you don’t have the secureTokenId, use our [List Secure Tokens](https://docs.payroc.com/api/schema/tokenization/secure-tokens/list) method to search for the secure token.
    ///
    /// When you delete a secure token, you can’t recover it, and you can’t reuse its identifier for a new token.
    /// </summary>
    Task DeleteAsync(
        DeleteSecureTokensRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to partially update a secure token. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.
    ///
    /// To update a secure token, you need its secureTokenId, which you sent in the request of the [Create Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/create) method.
    ///
    /// **Note:** If you don't have the secureTokenId, use our [List Secure Tokens](https://docs.payroc.com/api/schema/tokenization/secure-tokens/list) method to search  for the payment.
    ///
    /// You can update all of the properties of the secure token, except the following:
    /// - processingTerminalId
    /// - type
    /// - token
    /// - status
    /// - source/Card
    ///   - type
    ///   - cardNumber
    ///   - cardType
    ///   - currency
    ///   - debit
    ///   - surcharging
    /// - source/ACH account
    ///   - accountNumber
    ///   - routingNumber
    /// - source/PAD account
    ///   - type
    ///   - accountNumber
    ///   - transitNumber
    /// </summary>
    WithRawResponseTask<SecureToken> PartiallyUpdateAsync(
        PartiallyUpdateSecureTokensRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to update a secure token if you have a single-use token from Hosted Fields.
    ///
    /// **Note:** If you don't have a single-use token, you can update saved payment details with our [Update Secure Token](https://docs.payroc.com/api/resources#updateSecureToken) method. For more information about our two options to update a secure token, go to [Update saved payment details](https://docs.payroc.com/guides/take-payments/update-saved-payment-details).
    /// </summary>
    WithRawResponseTask<SecureToken> UpdateAccountAsync(
        UpdateAccountSecureTokensRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

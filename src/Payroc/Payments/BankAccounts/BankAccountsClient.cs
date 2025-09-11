using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.BankAccounts;

public partial class BankAccountsClient
{
    private RawClient _client;

    internal BankAccountsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Use this method to verify a customer's bank account details.
    ///
    /// In the request, send the customer's bank account details. Our gateway can verify the following types of bank details:
    /// - Automated Clearing House (ACH) details
    /// - Pre-Authorized Debit (PAD) details
    ///
    /// In the response, our gateway indicates if the account details are valid and if you should use them in follow-on actions.
    /// </summary>
    /// <example><code>
    /// await client.Payments.BankAccounts.VerifyAsync(
    ///     new BankAccountVerificationRequest
    ///     {
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         ProcessingTerminalId = "1234001",
    ///         BankAccount = new BankAccountVerificationRequestBankAccount(
    ///             new BankAccountVerificationRequestBankAccount.Pad(
    ///                 new PadPayload
    ///                 {
    ///                     NameOnAccount = "Sarah Hazel Hopper",
    ///                     AccountNumber = "1234567890",
    ///                     TransitNumber = "76543",
    ///                     InstitutionNumber = "543",
    ///                 }
    ///             )
    ///         ),
    ///     }
    /// );
    /// </code></example>
    public async Task<BankAccountVerificationResult> VerifyAsync(
        BankAccountVerificationRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _headers = new Headers(
                    new Dictionary<string, string>()
                    {
                        { "Idempotency-Key", request.IdempotencyKey },
                    }
                );
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Post,
                            Path = "bank-accounts/verify",
                            Body = request,
                            Headers = _headers,
                            ContentType = "application/json",
                            Options = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
                if (response.StatusCode is >= 200 and < 400)
                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        return JsonUtils.Deserialize<BankAccountVerificationResult>(responseBody)!;
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocException("Failed to deserialize response", e);
                    }
                }

                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        switch (response.StatusCode)
                        {
                            case 400:
                                throw new BadRequestError(
                                    JsonUtils.Deserialize<FourHundred>(responseBody)
                                );
                            case 401:
                                throw new UnauthorizedError(
                                    JsonUtils.Deserialize<FourHundredOne>(responseBody)
                                );
                            case 403:
                                throw new ForbiddenError(
                                    JsonUtils.Deserialize<FourHundredThree>(responseBody)
                                );
                            case 404:
                                throw new NotFoundError(
                                    JsonUtils.Deserialize<FourHundredFour>(responseBody)
                                );
                            case 406:
                                throw new NotAcceptableError(
                                    JsonUtils.Deserialize<FourHundredSix>(responseBody)
                                );
                            case 409:
                                throw new ConflictError(
                                    JsonUtils.Deserialize<FourHundredNine>(responseBody)
                                );
                            case 415:
                                throw new UnsupportedMediaTypeError(
                                    JsonUtils.Deserialize<FourHundredFifteen>(responseBody)
                                );
                            case 500:
                                throw new InternalServerError(
                                    JsonUtils.Deserialize<FiveHundred>(responseBody)
                                );
                        }
                    }
                    catch (JsonException)
                    {
                        // unable to map error response, throwing generic error
                    }
                    throw new PayrocApiException(
                        $"Error with status code {response.StatusCode}",
                        response.StatusCode,
                        responseBody
                    );
                }
            })
            .ConfigureAwait(false);
    }
}

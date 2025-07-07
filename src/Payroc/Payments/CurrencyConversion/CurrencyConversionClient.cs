using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.CurrencyConversion;

public partial class CurrencyConversionClient
{
    private RawClient _client;

    internal CurrencyConversionClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Check if a customer’s card is eligible for Dynamic Currency Conversion (DCC).
    /// If the card is eligible for DCC, offer currency conversion to the customer during a transaction.
    /// **Note:** We offer this through the DCC service, which gives customers a choice to pay in the local currency or their own currency.
    /// </summary>
    /// <example><code>
    /// await client.Payments.CurrencyConversion.RetrieveFxRatesAsync(
    ///     new FxRateInquiry
    ///     {
    ///         Channel = FxRateInquiryChannel.Web,
    ///         ProcessingTerminalId = "1234001",
    ///         Operator = "Jane",
    ///         BaseAmount = 10000,
    ///         BaseCurrency = Currency.Usd,
    ///         PaymentMethod = new FxRateInquiryPaymentMethod(
    ///             new FxRateInquiryPaymentMethod.Card(
    ///                 new CardPayload
    ///                 {
    ///                     CardDetails = new CardPayloadCardDetails(
    ///                         new CardPayloadCardDetails.Raw(
    ///                             new RawCardDetails
    ///                             {
    ///                                 Device = new Device
    ///                                 {
    ///                                     Model = DeviceModel.BbposChp,
    ///                                     SerialNumber = "1850010868",
    ///                                 },
    ///                                 RawData =
    ///                                     "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
    ///                             }
    ///                         )
    ///                     ),
    ///                 }
    ///             )
    ///         ),
    ///     }
    /// );
    /// </code></example>
    public async Task<FxRate> RetrieveFxRatesAsync(
        FxRateInquiry request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Post,
                            Path = "fx-rates",
                            Body = request,
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
                        return JsonUtils.Deserialize<FxRate>(responseBody)!;
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
                                    JsonUtils.Deserialize<object>(responseBody)
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

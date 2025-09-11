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
    /// &gt; **Important:** There are restrictions on which merchants can use this method. For more information, go to [Dynamic Currency Conversion](https://docs.payroc.com/knowledge/card-payments/dynamic-currency-conversion).
    ///
    /// Use this method to check if a card is eligible for Dynamic Currency Conversion (DCC) and to retrieve the conversion rate for a transaction amount. DCC provides a customer with the option to use their card's currency instead of the merchant's currency, for example, in Ireland, an American customer can pay in US dollars instead of Euros.
    ///
    /// The request includes the following:
    ///
    /// - **Payment method** - Card information, a secure token, or digital wallet.
    /// - **Transaction information** - Amount and currency of the transaction in the merchant's currency.
    ///
    /// If the card is eligible for DCC, our gateway returns the transaction amount in the card's currency and a dccOffer object that contains information about the conversion rate. The dccOffer object contains the following fields that you need when you [run a sale](https://docs.payroc.com/api/schema/payments/create) or [unreferenced refund](https://docs.payroc.com/api/schema/payments/refunds/create) with DCC:
    /// - fxAmount
    /// - fxCurrency
    /// - fxRate
    /// - markup
    /// - accepted
    /// - offerReference
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

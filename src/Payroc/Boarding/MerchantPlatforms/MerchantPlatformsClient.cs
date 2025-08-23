using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.MerchantPlatforms;

public partial class MerchantPlatformsClient
{
    private RawClient _client;

    internal MerchantPlatformsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Use this method to retrieve a [paginated](https://docs.payroc.com/api/pagination) list of merchant platforms that are linked to your ISV account.
    ///
    /// **Note**: If you want to view the details of a specific merchant platform and you have its merchantPlatformId, use our [Retrieve Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/retrieve) method.
    ///
    /// Our gateway returns the following information about each merchant platform in the list:
    /// - Legal information, including its legal name and address.
    /// - Contact information, including the email address for the business.
    /// - Processing  account information, including the processingAccountId and status of each processing account that's linked to the merchant platform.
    ///
    /// For each merchant platform, we also return its merchantPlatformId and its linked processingAccountIds, which you can use to perform follow-on actions.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.MerchantPlatforms.ListAsync(new ListMerchantPlatformsRequest());
    /// </code></example>
    public async Task<PayrocPager<MerchantPlatform>> ListAsync(
        ListMerchantPlatformsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _query = new Dictionary<string, object>();
                if (request.Before != null)
                {
                    _query["before"] = request.Before;
                }
                if (request.After != null)
                {
                    _query["after"] = request.After;
                }
                if (request.Limit != null)
                {
                    _query["limit"] = request.Limit.Value.ToString();
                }
                var httpRequest = _client.CreateHttpRequest(
                    new JsonRequest
                    {
                        BaseUrl = _client.Options.Environment.Api,
                        Method = HttpMethod.Get,
                        Path = "merchant-platforms",
                        Query = _query,
                        Options = options,
                    }
                );
                var sendRequest = async (
                    HttpRequestMessage httpRequest,
                    CancellationToken cancellationToken
                ) =>
                {
                    var response = await _client
                        .SendRequestAsync(httpRequest, options, cancellationToken)
                        .ConfigureAwait(false);
                    if (response.StatusCode is >= 200 and < 400)
                    {
                        return response.Raw;
                    }

                    {
                        var responseBody = await response.Raw.Content.ReadAsStringAsync();
                        try
                        {
                            switch (response.StatusCode)
                            {
                                case 401:
                                    throw new UnauthorizedError(
                                        JsonUtils.Deserialize<FourHundredOne>(responseBody)
                                    );
                                case 403:
                                    throw new ForbiddenError(
                                        JsonUtils.Deserialize<object>(responseBody)
                                    );
                                case 406:
                                    throw new NotAcceptableError(
                                        JsonUtils.Deserialize<FourHundredSix>(responseBody)
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
                };
                return await PayrocPagerFactory
                    .CreateAsync<MerchantPlatform>(
                        new PayrocPagerContext()
                        {
                            SendRequest = sendRequest,
                            InitialHttpRequest = httpRequest,
                            ClientOptions = _client.Options,
                            RequestOptions = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Use this method to board a merchant with Payroc.
    ///
    /// **Note**: This method is part of our Boarding solution. To help you understand how this method works with other Boarding methods, go to [Board a Merchant](https://docs.payroc.com/guides/integrate/boarding).
    ///
    /// In the request, include the following information:
    /// - Legal information, including its legal name and address.
    /// - Contact information, including the email address for the business.
    /// - Processing account information, including the pricing model, owners, and contacts for the processing account.
    ///
    /// When you send a successful request, we review the merchant's information. After we complete our review and approve the merchant, we assign:
    /// - **merchantPlatformId** - Unique identifier for the merchant platform.
    /// - **processingAccountId** - Unique identifier for each processing account linked to the merchant platform.
    ///
    /// You need to keep these to perform follow-on actions, for example, you need the processingAccountId to [order terminals](https://docs.payroc.com/api/schema/boarding/processing-accounts/create-terminal-order) for the processing account.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.MerchantPlatforms.CreateAsync(
    ///     new CreateMerchantAccount
    ///     {
    ///         IdempotencyKey = "Idempotency-Key",
    ///         Business = new Business
    ///         {
    ///             Name = "Example Corp",
    ///             TaxId = "12-3456789",
    ///             OrganizationType = BusinessOrganizationType.PrivateCorporation,
    ///             CountryOfOperation = "US",
    ///             Addresses = new List&lt;LegalAddress&gt;()
    ///             {
    ///                 new LegalAddress
    ///                 {
    ///                     Address1 = "1 Example Ave.",
    ///                     Address2 = "Example Address Line 2",
    ///                     Address3 = "Example Address Line 3",
    ///                     City = "Chicago",
    ///                     State = "Illinois",
    ///                     Country = "US",
    ///                     PostalCode = "60056",
    ///                     Type = "legalAddress",
    ///                 },
    ///             },
    ///             ContactMethods = new List&lt;ContactMethod&gt;()
    ///             {
    ///                 new ContactMethod(
    ///                     new ContactMethod.Email(
    ///                         new ContactMethodEmail { Value = "jane.doe@example.com" }
    ///                     )
    ///                 ),
    ///             },
    ///         },
    ///         ProcessingAccounts = new List&lt;CreateProcessingAccount&gt;()
    ///         {
    ///             new CreateProcessingAccount
    ///             {
    ///                 DoingBusinessAs = "Pizza Doe",
    ///                 Owners = new List&lt;Owner&gt;()
    ///                 {
    ///                     new Owner
    ///                     {
    ///                         FirstName = "Jane",
    ///                         MiddleName = "Helen",
    ///                         LastName = "Doe",
    ///                         DateOfBirth = new DateOnly(1964, 3, 22),
    ///                         Address = new Address
    ///                         {
    ///                             Address1 = "1 Example Ave.",
    ///                             Address2 = "Example Address Line 2",
    ///                             Address3 = "Example Address Line 3",
    ///                             City = "Chicago",
    ///                             State = "Illinois",
    ///                             Country = "US",
    ///                             PostalCode = "60056",
    ///                         },
    ///                         Identifiers = new List&lt;Identifier&gt;()
    ///                         {
    ///                             new Identifier { Type = "nationalId", Value = "000-00-4320" },
    ///                         },
    ///                         ContactMethods = new List&lt;ContactMethod&gt;()
    ///                         {
    ///                             new ContactMethod(
    ///                                 new ContactMethod.Email(
    ///                                     new ContactMethodEmail { Value = "jane.doe@example.com" }
    ///                                 )
    ///                             ),
    ///                         },
    ///                         Relationship = new OwnerRelationship
    ///                         {
    ///                             EquityPercentage = 48.5f,
    ///                             Title = "CFO",
    ///                             IsControlProng = true,
    ///                             IsAuthorizedSignatory = false,
    ///                         },
    ///                     },
    ///                 },
    ///                 Website = "www.example.com",
    ///                 BusinessType = CreateProcessingAccountBusinessType.Restaurant,
    ///                 CategoryCode = 5999,
    ///                 MerchandiseOrServiceSold = "Pizza",
    ///                 BusinessStartDate = new DateOnly(2020, 1, 1),
    ///                 Timezone = Timezone.AmericaChicago,
    ///                 Address = new Address
    ///                 {
    ///                     Address1 = "1 Example Ave.",
    ///                     Address2 = "Example Address Line 2",
    ///                     Address3 = "Example Address Line 3",
    ///                     City = "Chicago",
    ///                     State = "Illinois",
    ///                     Country = "US",
    ///                     PostalCode = "60056",
    ///                 },
    ///                 ContactMethods = new List&lt;ContactMethod&gt;()
    ///                 {
    ///                     new ContactMethod(
    ///                         new ContactMethod.Email(
    ///                             new ContactMethodEmail { Value = "jane.doe@example.com" }
    ///                         )
    ///                     ),
    ///                 },
    ///                 Processing = new Processing
    ///                 {
    ///                     TransactionAmounts = new ProcessingTransactionAmounts
    ///                     {
    ///                         Average = 5000,
    ///                         Highest = 10000,
    ///                     },
    ///                     MonthlyAmounts = new ProcessingMonthlyAmounts
    ///                     {
    ///                         Average = 50000,
    ///                         Highest = 100000,
    ///                     },
    ///                     VolumeBreakdown = new ProcessingVolumeBreakdown
    ///                     {
    ///                         CardPresentKeyed = 47,
    ///                         CardPresentSwiped = 30,
    ///                         MailOrTelephone = 3,
    ///                         Ecommerce = 20,
    ///                     },
    ///                     IsSeasonal = true,
    ///                     MonthsOfOperation = new List&lt;ProcessingMonthsOfOperationItem&gt;()
    ///                     {
    ///                         ProcessingMonthsOfOperationItem.Jan,
    ///                         ProcessingMonthsOfOperationItem.Feb,
    ///                     },
    ///                     Ach = new ProcessingAch
    ///                     {
    ///                         Naics = "5812",
    ///                         PreviouslyTerminatedForAch = false,
    ///                         Refunds = new ProcessingAchRefunds
    ///                         {
    ///                             WrittenRefundPolicy = true,
    ///                             RefundPolicyUrl = "www.example.com/refund-poilcy-url",
    ///                         },
    ///                         EstimatedMonthlyTransactions = 3000,
    ///                         Limits = new ProcessingAchLimits
    ///                         {
    ///                             SingleTransaction = 10000,
    ///                             DailyDeposit = 200000,
    ///                             MonthlyDeposit = 6000000,
    ///                         },
    ///                         TransactionTypes = new List&lt;ProcessingAchTransactionTypesItem&gt;()
    ///                         {
    ///                             ProcessingAchTransactionTypesItem.PrearrangedPayment,
    ///                             ProcessingAchTransactionTypesItem.Other,
    ///                         },
    ///                         TransactionTypesOther = "anotherTransactionType",
    ///                     },
    ///                     CardAcceptance = new ProcessingCardAcceptance
    ///                     {
    ///                         DebitOnly = false,
    ///                         HsaFsa = false,
    ///                         CardsAccepted = new List&lt;ProcessingCardAcceptanceCardsAcceptedItem&gt;()
    ///                         {
    ///                             ProcessingCardAcceptanceCardsAcceptedItem.Visa,
    ///                             ProcessingCardAcceptanceCardsAcceptedItem.Mastercard,
    ///                         },
    ///                         SpecialityCards = new ProcessingCardAcceptanceSpecialityCards
    ///                         {
    ///                             AmericanExpressDirect =
    ///                                 new ProcessingCardAcceptanceSpecialityCardsAmericanExpressDirect
    ///                                 {
    ///                                     Enabled = true,
    ///                                     MerchantNumber = "abc1234567",
    ///                                 },
    ///                             ElectronicBenefitsTransfer =
    ///                                 new ProcessingCardAcceptanceSpecialityCardsElectronicBenefitsTransfer
    ///                                 {
    ///                                     Enabled = true,
    ///                                     FnsNumber = "6789012",
    ///                                 },
    ///                             Other = new ProcessingCardAcceptanceSpecialityCardsOther
    ///                             {
    ///                                 WexMerchantNumber = "abc1234567",
    ///                                 VoyagerMerchantId = "abc1234567",
    ///                                 FleetMerchantId = "abc1234567",
    ///                             },
    ///                         },
    ///                     },
    ///                 },
    ///                 Funding = new CreateFunding
    ///                 {
    ///                     FundingSchedule = CommonFundingFundingSchedule.Nextday,
    ///                     AcceleratedFundingFee = 1999,
    ///                     DailyDiscount = false,
    ///                     FundingAccounts = new List&lt;FundingAccount&gt;()
    ///                     {
    ///                         new FundingAccount
    ///                         {
    ///                             Type = FundingAccountType.Checking,
    ///                             Use = FundingAccountUse.CreditAndDebit,
    ///                             NameOnAccount = "Jane Doe",
    ///                             PaymentMethods = new List&lt;PaymentMethodsItem&gt;()
    ///                             {
    ///                                 new PaymentMethodsItem(
    ///                                     new PaymentMethodsItem.Ach(new PaymentMethodAch())
    ///                                 ),
    ///                             },
    ///                             Metadata = new Dictionary&lt;string, string&gt;()
    ///                             {
    ///                                 { "yourCustomField", "abc123" },
    ///                             },
    ///                         },
    ///                     },
    ///                 },
    ///                 Pricing = new Pricing(
    ///                     new Pricing.Intent(new PricingTemplate { PricingIntentId = 6123 })
    ///                 ),
    ///                 Signature = new Signature(
    ///                     new Signature.RequestedViaDirectLink(new SignatureByDirectLink())
    ///                 ),
    ///                 Contacts = new List&lt;Contact&gt;()
    ///                 {
    ///                     new Contact
    ///                     {
    ///                         Type = ContactType.Manager,
    ///                         FirstName = "Jane",
    ///                         MiddleName = "Helen",
    ///                         LastName = "Doe",
    ///                         Identifiers = new List&lt;Identifier&gt;()
    ///                         {
    ///                             new Identifier { Type = "nationalId", Value = "000-00-4320" },
    ///                         },
    ///                         ContactMethods = new List&lt;ContactMethod&gt;()
    ///                         {
    ///                             new ContactMethod(
    ///                                 new ContactMethod.Email(
    ///                                     new ContactMethodEmail { Value = "jane.doe@example.com" }
    ///                                 )
    ///                             ),
    ///                         },
    ///                     },
    ///                 },
    ///                 Metadata = new Dictionary&lt;string, string&gt;() { { "customerId", "2345" } },
    ///             },
    ///         },
    ///         Metadata = new Dictionary&lt;string, string&gt;() { { "customerId", "2345" } },
    ///     }
    /// );
    /// </code></example>
    public async Task<MerchantPlatform> CreateAsync(
        CreateMerchantAccount request,
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
                            Path = "merchant-platforms",
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
                        return JsonUtils.Deserialize<MerchantPlatform>(responseBody)!;
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
                            case 406:
                                throw new NotAcceptableError(
                                    JsonUtils.Deserialize<FourHundredSix>(responseBody)
                                );
                            case 409:
                                throw new ConflictError(
                                    JsonUtils.Deserialize<FourHundredNine>(responseBody)
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

    /// <summary>
    /// Use this method to retrieve information about a merchant platform.
    ///
    /// To retrieve a merchant platform, you need its merchantPlatformId. Our gateway returned the merchantPlatformId in the response of the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method.
    ///
    /// **Note:** If you don't have the merchantPlatformId, use our [List Merchant Platforms](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list) method to search for the merchant platform.
    ///
    /// Our gateway returns the following information about the merchant platform:
    /// -	Legal information, including its legal name and address.
    /// -	Contact information, including the email address for the business.
    /// -	Processing account information, including the processingAccountId and status of each processing account that's linked to the merchant platform.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.MerchantPlatforms.RetrieveAsync(
    ///     new RetrieveMerchantPlatformsRequest { MerchantPlatformId = "merchantPlatformId" }
    /// );
    /// </code></example>
    public async Task<MerchantPlatform> RetrieveAsync(
        RetrieveMerchantPlatformsRequest request,
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
                            Method = HttpMethod.Get,
                            Path = string.Format(
                                "merchant-platforms/{0}",
                                ValueConvert.ToPathParameterString(request.MerchantPlatformId)
                            ),
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
                        return JsonUtils.Deserialize<MerchantPlatform>(responseBody)!;
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

    /// <summary>
    /// Use this method to retrieve a [paginated](https://docs.payroc.com/api/pagination) list of processing accounts linked to a merchant platform.
    ///
    /// **Note**: If you want to view the details of a specific processing account and you have its processingAccountId, use our [Retrieve Processing Account](https://docs.payroc.com/api/schema/boarding/processing-accounts/retrieve) method.
    ///
    /// Use the query parameters to filter the list of results that we return, for example, to search for only closed processing accounts.
    ///
    /// To list the processing accounts for a merchant platform, you need its merchantPlatformId. If you don't have the merchantPlatformId, use our [List Merchant Platforms](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list) method to search for the merchant platform.
    ///
    /// Our gateway returns the following information about eahc processing account in the list:
    /// - Business details, including its status, time zone, and address.
    /// - Owners' details, including their contact details.
    /// - Funding, pricing, and processing information, including its pricing model and funding accounts.
    ///
    /// For each processing account, we also return its processingAccountId, which you can use to perform follow-on actions.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.MerchantPlatforms.ListProcessingAccountsAsync(
    ///     new ListBoardingMerchantPlatformProcessingAccountsRequest
    ///     {
    ///         MerchantPlatformId = "merchantPlatformId",
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<ProcessingAccount>> ListProcessingAccountsAsync(
        ListBoardingMerchantPlatformProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _query = new Dictionary<string, object>();
                if (request.Before != null)
                {
                    _query["before"] = request.Before;
                }
                if (request.After != null)
                {
                    _query["after"] = request.After;
                }
                if (request.Limit != null)
                {
                    _query["limit"] = request.Limit.Value.ToString();
                }
                if (request.IncludeClosed != null)
                {
                    _query["includeClosed"] = JsonUtils.Serialize(request.IncludeClosed.Value);
                }
                var httpRequest = _client.CreateHttpRequest(
                    new JsonRequest
                    {
                        BaseUrl = _client.Options.Environment.Api,
                        Method = HttpMethod.Get,
                        Path = string.Format(
                            "merchant-platforms/{0}/processing-accounts",
                            ValueConvert.ToPathParameterString(request.MerchantPlatformId)
                        ),
                        Query = _query,
                        Options = options,
                    }
                );
                var sendRequest = async (
                    HttpRequestMessage httpRequest,
                    CancellationToken cancellationToken
                ) =>
                {
                    var response = await _client
                        .SendRequestAsync(httpRequest, options, cancellationToken)
                        .ConfigureAwait(false);
                    if (response.StatusCode is >= 200 and < 400)
                    {
                        return response.Raw;
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
                                case 406:
                                    throw new NotAcceptableError(
                                        JsonUtils.Deserialize<FourHundredSix>(responseBody)
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
                };
                return await PayrocPagerFactory
                    .CreateAsync<ProcessingAccount>(
                        new PayrocPagerContext()
                        {
                            SendRequest = sendRequest,
                            InitialHttpRequest = httpRequest,
                            ClientOptions = _client.Options,
                            RequestOptions = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Use this method to add an additional processing account to a merchant platform.
    ///
    /// To add a processing account to a merchant platform, you need the merchantPlatformId. Our gateway returned the merchantPlatformId in the response of the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method.
    ///
    /// **Note**: If you don't have the merchantPlatformId, use our [List Merchant Platforms](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list) method to search for the merchant platform.
    ///
    /// In the request, include the following information:
    /// - Business details, including its business type, time zone, and address.
    /// - Owners' details, including their contact details.
    /// - Funding, pricing, and processing information, including its pricing model and funding accounts.
    ///
    /// When you send a successful request, we review the information about the processing account. After we complete our review and approve the processing account, we assign a processingAccountId, which you need to perform follow-on actions.
    ///
    /// **Note**: You can subscribe to our processingAccount.status.changed event to get notifications when we update the status of a processing account. For more information about how to subscribe to events, go to [Events List](https://docs.payroc.com/knowledge/events/events-list).
    /// </summary>
    /// <example><code>
    /// await client.Boarding.MerchantPlatforms.CreateProcessingAccountAsync(
    ///     new CreateProcessingAccountMerchantPlatformsRequest
    ///     {
    ///         MerchantPlatformId = "merchantPlatformId",
    ///         IdempotencyKey = "Idempotency-Key",
    ///         Body = new CreateProcessingAccount
    ///         {
    ///             DoingBusinessAs = "Pizza Doe",
    ///             Owners = new List&lt;Owner&gt;()
    ///             {
    ///                 new Owner
    ///                 {
    ///                     FirstName = "Jane",
    ///                     MiddleName = "Helen",
    ///                     LastName = "Doe",
    ///                     DateOfBirth = new DateOnly(1964, 3, 22),
    ///                     Address = new Address
    ///                     {
    ///                         Address1 = "1 Example Ave.",
    ///                         Address2 = "Example Address Line 2",
    ///                         Address3 = "Example Address Line 3",
    ///                         City = "Chicago",
    ///                         State = "Illinois",
    ///                         Country = "US",
    ///                         PostalCode = "60056",
    ///                     },
    ///                     Identifiers = new List&lt;Identifier&gt;()
    ///                     {
    ///                         new Identifier { Type = "nationalId", Value = "000-00-4320" },
    ///                     },
    ///                     ContactMethods = new List&lt;ContactMethod&gt;()
    ///                     {
    ///                         new ContactMethod(
    ///                             new ContactMethod.Email(
    ///                                 new ContactMethodEmail { Value = "jane.doe@example.com" }
    ///                             )
    ///                         ),
    ///                     },
    ///                     Relationship = new OwnerRelationship
    ///                     {
    ///                         EquityPercentage = 51.5f,
    ///                         Title = "CFO",
    ///                         IsControlProng = true,
    ///                         IsAuthorizedSignatory = false,
    ///                     },
    ///                 },
    ///             },
    ///             Website = "www.example.com",
    ///             BusinessType = CreateProcessingAccountBusinessType.Restaurant,
    ///             CategoryCode = 5999,
    ///             MerchandiseOrServiceSold = "Pizza",
    ///             BusinessStartDate = new DateOnly(2020, 1, 1),
    ///             Timezone = Timezone.AmericaChicago,
    ///             Address = new Address
    ///             {
    ///                 Address1 = "1 Example Ave.",
    ///                 Address2 = "Example Address Line 2",
    ///                 Address3 = "Example Address Line 3",
    ///                 City = "Chicago",
    ///                 State = "Illinois",
    ///                 Country = "US",
    ///                 PostalCode = "60056",
    ///             },
    ///             ContactMethods = new List&lt;ContactMethod&gt;()
    ///             {
    ///                 new ContactMethod(
    ///                     new ContactMethod.Email(
    ///                         new ContactMethodEmail { Value = "jane.doe@example.com" }
    ///                     )
    ///                 ),
    ///             },
    ///             Processing = new Processing
    ///             {
    ///                 TransactionAmounts = new ProcessingTransactionAmounts
    ///                 {
    ///                     Average = 5000,
    ///                     Highest = 10000,
    ///                 },
    ///                 MonthlyAmounts = new ProcessingMonthlyAmounts { Average = 50000, Highest = 100000 },
    ///                 VolumeBreakdown = new ProcessingVolumeBreakdown
    ///                 {
    ///                     CardPresentKeyed = 47,
    ///                     CardPresentSwiped = 30,
    ///                     MailOrTelephone = 3,
    ///                     Ecommerce = 20,
    ///                 },
    ///                 IsSeasonal = true,
    ///                 MonthsOfOperation = new List&lt;ProcessingMonthsOfOperationItem&gt;()
    ///                 {
    ///                     ProcessingMonthsOfOperationItem.Jan,
    ///                     ProcessingMonthsOfOperationItem.Feb,
    ///                 },
    ///                 Ach = new ProcessingAch
    ///                 {
    ///                     Naics = "5812",
    ///                     PreviouslyTerminatedForAch = false,
    ///                     Refunds = new ProcessingAchRefunds
    ///                     {
    ///                         WrittenRefundPolicy = true,
    ///                         RefundPolicyUrl = "www.example.com/refund-poilcy-url",
    ///                     },
    ///                     EstimatedMonthlyTransactions = 3000,
    ///                     Limits = new ProcessingAchLimits
    ///                     {
    ///                         SingleTransaction = 10000,
    ///                         DailyDeposit = 200000,
    ///                         MonthlyDeposit = 6000000,
    ///                     },
    ///                     TransactionTypes = new List&lt;ProcessingAchTransactionTypesItem&gt;()
    ///                     {
    ///                         ProcessingAchTransactionTypesItem.PrearrangedPayment,
    ///                         ProcessingAchTransactionTypesItem.Other,
    ///                     },
    ///                     TransactionTypesOther = "anotherTransactionType",
    ///                 },
    ///                 CardAcceptance = new ProcessingCardAcceptance
    ///                 {
    ///                     DebitOnly = false,
    ///                     HsaFsa = false,
    ///                     CardsAccepted = new List&lt;ProcessingCardAcceptanceCardsAcceptedItem&gt;()
    ///                     {
    ///                         ProcessingCardAcceptanceCardsAcceptedItem.Visa,
    ///                         ProcessingCardAcceptanceCardsAcceptedItem.Mastercard,
    ///                     },
    ///                     SpecialityCards = new ProcessingCardAcceptanceSpecialityCards
    ///                     {
    ///                         AmericanExpressDirect =
    ///                             new ProcessingCardAcceptanceSpecialityCardsAmericanExpressDirect
    ///                             {
    ///                                 Enabled = true,
    ///                                 MerchantNumber = "abc1234567",
    ///                             },
    ///                         ElectronicBenefitsTransfer =
    ///                             new ProcessingCardAcceptanceSpecialityCardsElectronicBenefitsTransfer
    ///                             {
    ///                                 Enabled = true,
    ///                                 FnsNumber = "6789012",
    ///                             },
    ///                         Other = new ProcessingCardAcceptanceSpecialityCardsOther
    ///                         {
    ///                             WexMerchantNumber = "abc1234567",
    ///                             VoyagerMerchantId = "abc1234567",
    ///                             FleetMerchantId = "abc1234567",
    ///                         },
    ///                     },
    ///                 },
    ///             },
    ///             Funding = new CreateFunding
    ///             {
    ///                 FundingSchedule = CommonFundingFundingSchedule.Nextday,
    ///                 AcceleratedFundingFee = 1999,
    ///                 DailyDiscount = false,
    ///                 FundingAccounts = new List&lt;FundingAccount&gt;()
    ///                 {
    ///                     new FundingAccount
    ///                     {
    ///                         Type = FundingAccountType.Checking,
    ///                         Use = FundingAccountUse.CreditAndDebit,
    ///                         NameOnAccount = "Jane Doe",
    ///                         PaymentMethods = new List&lt;PaymentMethodsItem&gt;()
    ///                         {
    ///                             new PaymentMethodsItem(
    ///                                 new PaymentMethodsItem.Ach(new PaymentMethodAch())
    ///                             ),
    ///                         },
    ///                         Metadata = new Dictionary&lt;string, string&gt;()
    ///                         {
    ///                             { "yourCustomField", "abc123" },
    ///                         },
    ///                     },
    ///                 },
    ///             },
    ///             Pricing = new Pricing(
    ///                 new Pricing.Intent(new PricingTemplate { PricingIntentId = 6123 })
    ///             ),
    ///             Signature = new Signature(
    ///                 new Signature.RequestedViaDirectLink(new SignatureByDirectLink())
    ///             ),
    ///             Contacts = new List&lt;Contact&gt;()
    ///             {
    ///                 new Contact
    ///                 {
    ///                     Type = ContactType.Manager,
    ///                     FirstName = "Jane",
    ///                     MiddleName = "Helen",
    ///                     LastName = "Doe",
    ///                     Identifiers = new List&lt;Identifier&gt;()
    ///                     {
    ///                         new Identifier { Type = "nationalId", Value = "000-00-4320" },
    ///                     },
    ///                     ContactMethods = new List&lt;ContactMethod&gt;()
    ///                     {
    ///                         new ContactMethod(
    ///                             new ContactMethod.Email(
    ///                                 new ContactMethodEmail { Value = "jane.doe@example.com" }
    ///                             )
    ///                         ),
    ///                     },
    ///                 },
    ///             },
    ///             Metadata = new Dictionary&lt;string, string&gt;() { { "customerId", "2345" } },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task<ProcessingAccount> CreateProcessingAccountAsync(
        CreateProcessingAccountMerchantPlatformsRequest request,
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
                            Path = string.Format(
                                "merchant-platforms/{0}/processing-accounts",
                                ValueConvert.ToPathParameterString(request.MerchantPlatformId)
                            ),
                            Body = request.Body,
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
                        return JsonUtils.Deserialize<ProcessingAccount>(responseBody)!;
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

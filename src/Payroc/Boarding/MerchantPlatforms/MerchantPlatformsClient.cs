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
    /// Use this method to retrieve a [paginated](/api/pagination) list of the merchant platforms that are linked to the ISV's account.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.MerchantPlatforms.ListAsync(
    ///     new ListMerchantPlatformsRequest { Before = "2571", After = "8516" }
    /// );
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
                    .CreateAsync<MerchantPlatform>(sendRequest, httpRequest, cancellationToken)
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Use this method to create the entity that represents a business, including its legal information and all its processing accounts.
    ///
    /// &gt; **Note**: To add a processing account to an existing merchant platform, go to [Create a processing account](#createProcessingAccount).
    ///
    /// The response contains some fields that we require for other methods:
    /// - **merchantPlatformId** - Unique identifier that we assign to the merchant platform. Use the merchantPlatformId to retrieve and update information about the merchant platform.
    ///
    /// - **processingAccountId**- Unique identifier that we assign to each processing account. Use the processingAccountId to retrieve and update information about the processing account.
    ///   &lt;br/&gt;
    /// For more information about how to create a merchant platform, go to [Create a merchant platform.](/guides/integrate/boarding/merchant-platform)
    /// </summary>
    /// <example><code>
    /// await client.Boarding.MerchantPlatforms.CreateAsync(
    ///     new CreateMerchantAccount
    ///     {
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
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
    ///                         DateOfBirth = "1964-03-22",
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
    ///                 BusinessStartDate = "2020-01-01",
    ///                 Timezone = CreateProcessingAccountTimezone.AmericaChicago,
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
    ///                 Signature = CreateProcessingAccountSignature.RequestedViaDirectLink,
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
    /// Use this method to retrieve information about a merchant platform, including its legal information and processing accounts.
    ///
    /// Include the merchantPlatformId that we sent you when you created the merchant platform.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.MerchantPlatforms.GetAsync(
    ///     new GetMerchantPlatformsRequest { MerchantPlatformId = "12345" }
    /// );
    /// </code></example>
    public async Task<MerchantPlatform> GetAsync(
        GetMerchantPlatformsRequest request,
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
    /// Use this method to retrieve a paginated list of processing accounts associated with a merchant platform.
    ///
    /// When you created the merchant platform, we sent you its merchantPlatformId in the response. Send this merchantPlatformId as a path parameter in your endpoint.
    ///
    /// &gt; **Note**: By default, we return only open processing accounts. To include closed processing accounts, send a value of `true` for the includeClosed query parameter.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.MerchantPlatforms.ListProcessingAccountsAsync(
    ///     new ListBoardingMerchantPlatformProcessingAccountsRequest
    ///     {
    ///         MerchantPlatformId = "12345",
    ///         Before = "2571",
    ///         After = "8516",
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
                    .CreateAsync<ProcessingAccount>(sendRequest, httpRequest, cancellationToken)
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Use this method to create a processing account and add it to a merchant platform.
    ///     &gt; **Note**: You can create and add a processing account only to an existing merchant platform. If you have not already created a merchant platform, go to [Create a merchant platform.](#createMerchant)
    ///
    /// In the response we return a processingAccountId for the processing account, which you need for the following methods.
    /// - [Retrieve processing account](#getProcessingAcccounts)
    /// - [List processing account's funding accounts](#listProcessingAccountsFundingAccounts)
    /// - [List contacts](#listProcessingAccountContacts)
    /// - [Get a processing account pricing agreement](#retrieveProcessingAccountPricing)
    /// - [List owners](#listMerchantOwners)
    /// - [Create reminder for processing account](#createReminder)
    /// </summary>
    /// <example><code>
    /// await client.Boarding.MerchantPlatforms.CreateProcessingAccountAsync(
    ///     new CreateProcessingAccountMerchantPlatformsRequest
    ///     {
    ///         MerchantPlatformId = "12345",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
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
    ///                     DateOfBirth = "1964-03-22",
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
    ///             BusinessStartDate = "2020-01-01",
    ///             Timezone = CreateProcessingAccountTimezone.AmericaChicago,
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
    ///             Signature = CreateProcessingAccountSignature.RequestedViaDirectLink,
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

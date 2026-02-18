using System.Text.Json;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.Owners;

public partial class OwnersClient : IOwnersClient
{
    private RawClient _client;

    internal OwnersClient(RawClient client)
    {
        try
        {
            _client = client;
        }
        catch (Exception ex)
        {
            client.Options.ExceptionHandler?.CaptureException(ex);
            throw;
        }
    }

    private async Task<WithRawResponse<Owner>> RetrieveAsyncCore(
        RetrieveOwnersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _headers = await new Payroc.Core.HeadersBuilder.Builder()
                    .Add(_client.Options.Headers)
                    .Add(_client.Options.AdditionalHeaders)
                    .Add(options?.AdditionalHeaders)
                    .BuildAsync()
                    .ConfigureAwait(false);
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Get,
                            Path = string.Format(
                                "owners/{0}",
                                ValueConvert.ToPathParameterString(request.OwnerId)
                            ),
                            Headers = _headers,
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
                        var responseData = JsonUtils.Deserialize<Owner>(responseBody)!;
                        return new WithRawResponse<Owner>()
                        {
                            Data = responseData,
                            RawResponse = new RawResponse()
                            {
                                StatusCode = response.Raw.StatusCode,
                                Url =
                                    response.Raw.RequestMessage?.RequestUri
                                    ?? new Uri("about:blank"),
                                Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                            },
                        };
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocApiException(
                            "Failed to deserialize response",
                            response.StatusCode,
                            responseBody,
                            e
                        );
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
    /// Use this method to retrieve details about an owner of a processing account or an owner associated with a funding recipient.
    ///
    /// To retrieve an owner, you need their ownerId. Our gateway returned the ownerId in the response of the [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method or the [Create Funding Recipient Owner](https://docs.payroc.com/api/schema/funding/funding-recipients/create-owner) method.
    ///
    /// **Note:** If you don't have the ownerId, use the [Retrieve Processing Account](https://docs.payroc.com/api/schema/boarding/processing-accounts/retrieve) method if you are searching for a processing account owner, or use the [List Funding Recipient Owners](https://docs.payroc.com/api/schema/funding/funding-recipients/list-owners) method if you are searching for a funding recipient owner.
    ///
    /// Our gateway returns the following information about an owner:
    /// - Name, date of birth, and address.
    /// - Contact details, including their email address.
    /// - Relationship to the business, including whether they are a control prong or authorized signatory, and their equity stake in the business.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.Owners.RetrieveAsync(new RetrieveOwnersRequest { OwnerId = 1 });
    /// </code></example>
    public WithRawResponseTask<Owner> RetrieveAsync(
        RetrieveOwnersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<Owner>(
            RetrieveAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// &gt; **Important:** You can't update the details of an owner of a processing account.
    ///
    /// Use this method to update the details of an owner associated with a funding recipient.
    ///
    /// To update an owner, you need their ownerId. Our gateway returned the ownerId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method and the [Create Funding Recipient Owner](https://docs.payroc.com/api/schema/funding/funding-recipients/create-owner) method.
    ///
    /// **Note:** If you don't have the ownerId, use the [List Funding Recipient Owners](https://docs.payroc.com/api/schema/funding/funding-recipients/list-owners) method, the [Retrieve Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/retrieve) method, or the [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient owner.
    ///
    /// You can update the following details about an owner:
    ///
    /// - Personal details, including their name, date of birth, and address.
    /// - Identification details, including their identification type and number.
    /// - Contact details, including their email address.
    /// - Relationship to the business, including whether they are a control prong.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.Owners.UpdateAsync(
    ///     new UpdateOwnersRequest
    ///     {
    ///         OwnerId = 1,
    ///         Body = new Owner
    ///         {
    ///             FirstName = "Jane",
    ///             MiddleName = "Helen",
    ///             LastName = "Doe",
    ///             DateOfBirth = new DateOnly(1964, 3, 22),
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
    ///             Identifiers = new List&lt;Identifier&gt;()
    ///             {
    ///                 new Identifier { Type = IdentifierType.NationalId, Value = "000-00-4320" },
    ///             },
    ///             ContactMethods = new List&lt;ContactMethod&gt;()
    ///             {
    ///                 new ContactMethod(
    ///                     new ContactMethod.Email(
    ///                         new ContactMethodEmail { Value = "jane.doe@example.com" }
    ///                     )
    ///                 ),
    ///             },
    ///             Relationship = new OwnerRelationship
    ///             {
    ///                 EquityPercentage = 48.5f,
    ///                 Title = "CFO",
    ///                 IsControlProng = true,
    ///                 IsAuthorizedSignatory = false,
    ///             },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task UpdateAsync(
        UpdateOwnersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _headers = await new Payroc.Core.HeadersBuilder.Builder()
                    .Add(_client.Options.Headers)
                    .Add(_client.Options.AdditionalHeaders)
                    .Add(options?.AdditionalHeaders)
                    .BuildAsync()
                    .ConfigureAwait(false);
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Put,
                            Path = string.Format(
                                "owners/{0}",
                                ValueConvert.ToPathParameterString(request.OwnerId)
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
                    return;
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
    /// &gt; **Important:** You can't delete an owner of a processing account.
    ///
    /// Use this method to delete an owner associated with a funding recipient. You can delete an owner only if the funding recipient has more than one owner.
    ///
    /// To delete an owner, you need their ownerId. Our gateway returned the ownerId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method and the [Create Funding Recipient Owner](https://docs.payroc.com/api/schema/funding/funding-recipients/create-owner) method.
    ///
    /// **Note:** If you don't have the ownerId, use the [List Funding Recipient Owners](https://docs.payroc.com/api/schema/funding/funding-recipients/list-owners) method, the [Retrieve Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/retrieve) method, or the [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient owner.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.Owners.DeleteAsync(new DeleteOwnersRequest { OwnerId = 1 });
    /// </code></example>
    public async Task DeleteAsync(
        DeleteOwnersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _headers = await new Payroc.Core.HeadersBuilder.Builder()
                    .Add(_client.Options.Headers)
                    .Add(_client.Options.AdditionalHeaders)
                    .Add(options?.AdditionalHeaders)
                    .BuildAsync()
                    .ConfigureAwait(false);
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Delete,
                            Path = string.Format(
                                "owners/{0}",
                                ValueConvert.ToPathParameterString(request.OwnerId)
                            ),
                            Headers = _headers,
                            Options = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
                if (response.StatusCode is >= 200 and < 400)
                {
                    return;
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

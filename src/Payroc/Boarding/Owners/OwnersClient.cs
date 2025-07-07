using System.Net.Http;
using System.Text.Json;
using System.Threading;
using global::System.Threading.Tasks;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.Owners;

public partial class OwnersClient
{
    private RawClient _client;

    internal OwnersClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// If you have the ownerId, you can use this method to retrieve the details of the owner. The response includes the owner's contact details and information about the owner's relationship to the business.
    ///
    /// If you don't have the ownerId, you can use the following methods to retrieve the owner's information:
    ///   - **Owners of processing accounts** - Use the HATEOAS links that we send you when you [Retrieve a processing account](/api/schema/boarding/processing-accounts/get) or [List merchant platform's processing accounts](/api/schema/boarding/merchant-platforms/list-processing-accounts).
    ///   - **Owners of funding recipients** - Use the [List funding recipient owners](/api/schema/funding/funding-recipients/list-owners) method.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.Owners.RetrieveAsync(new RetrieveOwnersRequest { OwnerId = 1 });
    /// </code></example>
    public async Task<Owner> RetrieveAsync(
        RetrieveOwnersRequest request,
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
                                "owners/{0}",
                                ValueConvert.ToPathParameterString(request.OwnerId)
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
                        return JsonUtils.Deserialize<Owner>(responseBody)!;
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
    /// &gt; **Important:** You cannot update the owner of a processing account.
    ///
    /// Use this method to update the details of a funding recipient owner such as their contact details and their relationship to the business.
    ///
    /// Include the ownerId in the path parameter of your request. If you don’t know the ownerId, you can [Retrieve the funding recipient](/api/schema/funding/funding-recipients/get), [List funding recipients](/api/schema/funding/funding-recipients/list), or [List funding recipient owners](/api/schema/funding/funding-recipients/list-owners) to locate the ownerId.
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
    ///                 new Identifier { Type = "nationalId", Value = "000-00-4320" },
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
    public async global::System.Threading.Tasks.Task UpdateAsync(
        UpdateOwnersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
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
    /// &gt; **Important:** You cannot delete the owner of a processing account.
    ///
    /// Use this method to delete an owner from a funding recipient. You can delete an owner only if the funding recipient has more than one owner.
    ///
    /// Include the ownerId in the path parameter of your request. If you don’t know the ownerId, you can [Retrieve the funding recipient](/api/schema/funding/funding-recipients/get), [List funding recipients](/api/schema/funding/funding-recipients/list), or [List funding recipient owners](/api/schema/funding/funding-recipients/list-owners) to locate the ownerId.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.Owners.DeleteAsync(new DeleteOwnersRequest { OwnerId = 1 });
    /// </code></example>
    public async global::System.Threading.Tasks.Task DeleteAsync(
        DeleteOwnersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
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

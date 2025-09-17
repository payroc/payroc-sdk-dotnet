using System.Net.Http;
using System.Text.Json;
using System.Threading;
using global::System.Threading.Tasks;
using Payroc;
using Payroc.Core;

namespace Payroc.Notifications.EventSubscriptions;

public partial class EventSubscriptionsClient
{
    private RawClient _client;

    internal EventSubscriptionsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of event subscriptions that are linked to your ISV account.
    ///
    /// **Note:** If you want to view the details of a specific event subscription and you have its id, use our [Retrieve Event Subscription](https://docs.payroc.com/api/schema/notifications/event-subscriptions/retrieve) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for subscriptions with a specific status or an event type.
    ///
    /// Our gateway returns the following information about each subscription in the list:
    /// - Event types that you have subscribed to.
    /// - Whether you have enabled notifications for the subscription.
    /// - How we contact you when an event occurs, including the endpoint that send notifications to.
    /// - If there are any issues when we try to send you a notification, for example, if we can't contact your endpoint.
    ///
    /// For each event subscription, we also return its id, which you can use to perform follow-on actions.
    /// </summary>
    /// <example><code>
    /// await client.Notifications.EventSubscriptions.ListAsync(
    ///     new ListEventSubscriptionsRequest { Event = "processingAccount.status.changed" }
    /// );
    /// </code></example>
    public async Task<PayrocPager<EventSubscription>> ListAsync(
        ListEventSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _query = new Dictionary<string, object>();
                if (request.Status != null)
                {
                    _query["status"] = request.Status.Value.Stringify();
                }
                if (request.Event != null)
                {
                    _query["event"] = request.Event;
                }
                var httpRequest = _client.CreateHttpRequest(
                    new JsonRequest
                    {
                        BaseUrl = _client.Options.Environment.Api,
                        Method = HttpMethod.Get,
                        Path = "event-subscriptions",
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
                };
                return await PayrocPagerFactory
                    .CreateAsync<EventSubscription>(
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
    /// Use this method to create an event subscription that we use to notify you when an event occurs, for example, when we change the status of a processing account.
    ///
    /// In the request, include the events that you want to subscribe to and the public endpoint that we send event notifications to. For a complete list of events that you can subscribe to, go to [Events List](https://docs.payroc.com/knowledge/events/events-list).
    ///
    /// In the response, our gateway returns the id of the event subscription, which you can use to perform follow-on actions.
    /// </summary>
    /// <example><code>
    /// await client.Notifications.EventSubscriptions.CreateAsync(
    ///     new CreateEventSubscriptionsRequest
    ///     {
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Body = new EventSubscription
    ///         {
    ///             Enabled = true,
    ///             EventTypes = new List&lt;string&gt;() { "processingAccount.status.changed" },
    ///             Notifications = new List&lt;Notification&gt;()
    ///             {
    ///                 new Notification(
    ///                     new Notification.Webhook(
    ///                         new Webhook
    ///                         {
    ///                             Uri = "https://my-server/notification/endpoint",
    ///                             Secret = "aBcD1234eFgH5678iJkL9012mNoP3456",
    ///                             SupportEmailAddress = "supportEmailAddress",
    ///                         }
    ///                     )
    ///                 ),
    ///             },
    ///             Metadata = new Dictionary&lt;string, object&gt;() { { "yourCustomField", "abc123" } },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task<EventSubscription> CreateAsync(
        CreateEventSubscriptionsRequest request,
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
                            Path = "event-subscriptions",
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
                        return JsonUtils.Deserialize<EventSubscription>(responseBody)!;
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

    /// <summary>
    /// Use this method to retrieve the details of an event subscription.
    ///
    /// In your request, include the subscriptionId that we sent to you when we created the event subscription.
    ///
    /// **Note:** If you don't know the subscriptionId of the event subscription, go to [List event subscriptions](#listEventSubscriptions).
    /// </summary>
    /// <example><code>
    /// await client.Notifications.EventSubscriptions.RetrieveAsync(
    ///     new RetrieveEventSubscriptionsRequest { SubscriptionId = 1 }
    /// );
    /// </code></example>
    public async Task<EventSubscription> RetrieveAsync(
        RetrieveEventSubscriptionsRequest request,
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
                                "event-subscriptions/{0}",
                                ValueConvert.ToPathParameterString(request.SubscriptionId)
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
                        return JsonUtils.Deserialize<EventSubscription>(responseBody)!;
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
    /// Use this method to update the details of an event subscription.
    ///
    /// To update an event subscription, you need its subscriptionId. Our gateway returned the subscriptionId in the response of the [Create Event Subscription](https://docs.payroc.com/api/schema/notifications/event-subscriptions/create) method.
    ///
    /// **Note:** If you don’t have the subscriptionId, use our [List Event Subscriptions](https://docs.payroc.com/api/schema/notifications/event-subscriptions/list) method to search for the event subscription.
    ///
    /// You can update the following details about an event subscription:
    ///
    /// - Status of the event subscription.
    /// - Events that you have subscribed to. For a list of events that you can subscribe to, go to [Events list](https://docs.payroc.com/knowledge/events/events-list).
    /// - Information about how we contact you when an event occurs.
    /// </summary>
    /// <example><code>
    /// await client.Notifications.EventSubscriptions.UpdateAsync(
    ///     new UpdateEventSubscriptionsRequest
    ///     {
    ///         SubscriptionId = 1,
    ///         Body = new EventSubscription
    ///         {
    ///             Enabled = true,
    ///             EventTypes = new List&lt;string&gt;() { "processingAccount.status.changed" },
    ///             Notifications = new List&lt;Notification&gt;()
    ///             {
    ///                 new Notification(
    ///                     new Notification.Webhook(
    ///                         new Webhook
    ///                         {
    ///                             Uri = "https://my-server/notification/endpoint",
    ///                             Secret = "aBcD1234eFgH5678iJkL9012mNoP3456",
    ///                             SupportEmailAddress = "supportEmailAddress",
    ///                         }
    ///                     )
    ///                 ),
    ///             },
    ///             Metadata = new Dictionary&lt;string, object&gt;() { { "yourCustomField", "abc123" } },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async global::System.Threading.Tasks.Task UpdateAsync(
        UpdateEventSubscriptionsRequest request,
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
                                "event-subscriptions/{0}",
                                ValueConvert.ToPathParameterString(request.SubscriptionId)
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
    /// Use this method to delete an event subscription.
    ///
    /// &gt; **Important:** After you delete an event subscription, you can’t recover it. You won't receive event notifications from the event subscription.
    ///
    /// To delete an event subscription, you need its subscriptionId. Our gateway returned the subscriptionId in the response of the [Create Event Subscription](https://docs.payroc.com/api/schema/notifications/event-subscriptions/create) method.
    ///
    /// If you want to stop receiving event notifications but don't want to delete the event subscription, use our [Update Event Subscription](https://docs.payroc.com/api/schema/notifications/event-subscriptions/update) method to deactivate it.
    /// </summary>
    /// <example><code>
    /// await client.Notifications.EventSubscriptions.DeleteAsync(
    ///     new DeleteEventSubscriptionsRequest { SubscriptionId = 1 }
    /// );
    /// </code></example>
    public async global::System.Threading.Tasks.Task DeleteAsync(
        DeleteEventSubscriptionsRequest request,
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
                                "event-subscriptions/{0}",
                                ValueConvert.ToPathParameterString(request.SubscriptionId)
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
    /// Use this method to partially update an event subscription. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.
    ///
    /// To update an event subscription, you need its subscriptionId. Our gateway returned the subscriptionId in the id field in the response of the [Create Event Subscription](https://docs.payroc.com/api/schema/notifications/event-subscriptions/create) method.
    ///
    /// **Note:** If you don't have the subscriptionId, use our [List Event Subscriptions](https://docs.payroc.com/api/schema/notifications/event-subscriptions/list) method to search for the subscription.
    ///
    /// You can update the following properties of an event subscription:
    /// - **eventTypes** - Subscribe to new events or remove events that you are subscribed to.
    /// - **notifications** - Information about your endpoint and who we email if we can't contact your endpoint.
    /// - **enabled** - Turn on or turn off notifications for the subscription.
    /// </summary>
    /// <example><code>
    /// await client.Notifications.EventSubscriptions.PartiallyUpdateAsync(
    ///     new PartiallyUpdateEventSubscriptionsRequest
    ///     {
    ///         SubscriptionId = 1,
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Body = new List&lt;PatchDocument&gt;()
    ///         {
    ///             new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task<EventSubscription> PartiallyUpdateAsync(
        PartiallyUpdateEventSubscriptionsRequest request,
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
                            Method = HttpMethodExtensions.Patch,
                            Path = string.Format(
                                "event-subscriptions/{0}",
                                ValueConvert.ToPathParameterString(request.SubscriptionId)
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
                        return JsonUtils.Deserialize<EventSubscription>(responseBody)!;
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

using Payroc;
using Payroc.Core;

namespace Payroc.Notifications.EventSubscriptions;

public partial interface IEventSubscriptionsClient
{
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
    Task<PayrocPager<EventSubscription>> ListAsync(
        ListEventSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to create an event subscription that we use to notify you when an event occurs, for example, when we change the status of a processing account.
    ///
    /// In the request, include the events that you want to subscribe to and the public endpoint that we send event notifications to. For a complete list of events that you can subscribe to, go to [Events List](https://docs.payroc.com/knowledge/events/events-list).
    ///
    /// In the response, our gateway returns the id of the event subscription, which you can use to perform follow-on actions.
    /// </summary>
    WithRawResponseTask<EventSubscription> CreateAsync(
        CreateEventSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve the details of an event subscription.
    ///
    /// In your request, include the subscriptionId that we sent to you when we created the event subscription.
    ///
    /// **Note:** If you don't know the subscriptionId of the event subscription, go to [List event subscriptions](https://docs.payroc.com/api/schema/notifications/event-subscriptions/list).
    /// </summary>
    WithRawResponseTask<EventSubscription> RetrieveAsync(
        RetrieveEventSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

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
    Task UpdateAsync(
        UpdateEventSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to delete an event subscription.
    ///
    /// &gt; **Important:** After you delete an event subscription, you can’t recover it. You won't receive event notifications from the event subscription.
    ///
    /// To delete an event subscription, you need its subscriptionId. Our gateway returned the subscriptionId in the response of the [Create Event Subscription](https://docs.payroc.com/api/schema/notifications/event-subscriptions/create) method.
    ///
    /// If you want to stop receiving event notifications but don't want to delete the event subscription, use our [Update Event Subscription](https://docs.payroc.com/api/schema/notifications/event-subscriptions/update) method to deactivate it.
    /// </summary>
    Task DeleteAsync(
        DeleteEventSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

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
    WithRawResponseTask<EventSubscription> PartiallyUpdateAsync(
        PartiallyUpdateEventSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

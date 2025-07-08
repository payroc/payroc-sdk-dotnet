using Payroc.Core;
using Payroc.Notifications.EventSubscriptions;

namespace Payroc.Notifications;

public partial class NotificationsClient
{
    private RawClient _client;

    internal NotificationsClient(RawClient client)
    {
        _client = client;
        EventSubscriptions = new EventSubscriptionsClient(_client);
    }

    public EventSubscriptionsClient EventSubscriptions { get; }
}

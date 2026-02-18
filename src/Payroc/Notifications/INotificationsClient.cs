using Payroc.Notifications.EventSubscriptions;

namespace Payroc.Notifications;

public partial interface INotificationsClient
{
    public IEventSubscriptionsClient EventSubscriptions { get; }
}

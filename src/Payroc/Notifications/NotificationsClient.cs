using Payroc.Core;
using Payroc.Notifications.EventSubscriptions;

namespace Payroc.Notifications;

public partial class NotificationsClient
{
    private RawClient _client;

    internal NotificationsClient(RawClient client)
    {
        try
        {
            _client = client;
            EventSubscriptions = new EventSubscriptionsClient(_client);
        }
        catch (Exception ex)
        {
            client.Options.ExceptionHandler?.CaptureException(ex);
            throw;
        }
    }

    public EventSubscriptionsClient EventSubscriptions { get; }
}

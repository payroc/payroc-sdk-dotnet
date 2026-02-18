using Payroc;

namespace Payroc.Boarding.TerminalOrders;

public partial interface ITerminalOrdersClient
{
    /// <summary>
    /// Use this method to retrieve information about a terminal order.
    ///
    /// To retrieve a terminal order, you need it's terminalOrderId. Our gateway returned the terminalOrderId in the response of the [Create Terminal Order](https://docs.payroc.com/api/schema/boarding/processing-accounts/create-terminal-order) method.
    ///
    /// **Note**: If you don't have the terminalOrderId, use our [List Terminal Orders](https://docs.payroc.com/api/schema/boarding/processing-accounts/list-terminal-orders) method to search for the terminal order.
    ///
    /// Our gateway returns the following information about the terminal order:
    /// - Status of the order
    /// - Items in the order
    /// - Training provider
    /// - Shipping information
    ///
    /// **Note**: You can subscribe to our terminalOrder.status.changed event to get notifications when we update the status of a terminal order. For more information about how to subscribe to events, go to [Events Subscriptions](https://docs.payroc.com/guides/board-merchants/event-subscriptions).
    /// </summary>
    WithRawResponseTask<TerminalOrder> RetrieveAsync(
        RetrieveTerminalOrdersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

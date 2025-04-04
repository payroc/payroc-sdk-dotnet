using Payroc.Core;
using Payroc.PayrocCloud.PaymentInstructions;
using Payroc.PayrocCloud.RefundInstructions;

namespace Payroc.PayrocCloud;

public partial class PayrocCloudClient
{
    private RawClient _client;

    internal PayrocCloudClient(RawClient client)
    {
        _client = client;
        PaymentInstructions = new PaymentInstructionsClient(_client);
        RefundInstructions = new RefundInstructionsClient(_client);
    }

    public PaymentInstructionsClient PaymentInstructions { get; }

    public RefundInstructionsClient RefundInstructions { get; }
}

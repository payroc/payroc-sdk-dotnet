using Payroc.Core;
using Payroc.PayrocCloud.PaymentInstructions;
using Payroc.PayrocCloud.RefundInstructions;
using Payroc.PayrocCloud.SignatureInstructions;
using Payroc.PayrocCloud.Signatures;

namespace Payroc.PayrocCloud;

public partial class PayrocCloudClient : IPayrocCloudClient
{
    private RawClient _client;

    internal PayrocCloudClient(RawClient client)
    {
        try
        {
            _client = client;
            PaymentInstructions = new PaymentInstructionsClient(_client);
            RefundInstructions = new RefundInstructionsClient(_client);
            SignatureInstructions = new SignatureInstructionsClient(_client);
            Signatures = new SignaturesClient(_client);
        }
        catch (Exception ex)
        {
            client.Options.ExceptionHandler?.CaptureException(ex);
            throw;
        }
    }

    public IPaymentInstructionsClient PaymentInstructions { get; }

    public IRefundInstructionsClient RefundInstructions { get; }

    public ISignatureInstructionsClient SignatureInstructions { get; }

    public ISignaturesClient Signatures { get; }
}

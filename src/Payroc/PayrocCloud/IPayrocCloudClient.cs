using Payroc.PayrocCloud.PaymentInstructions;
using Payroc.PayrocCloud.RefundInstructions;
using Payroc.PayrocCloud.SignatureInstructions;
using Payroc.PayrocCloud.Signatures;

namespace Payroc.PayrocCloud;

public partial interface IPayrocCloudClient
{
    public IPaymentInstructionsClient PaymentInstructions { get; }
    public IRefundInstructionsClient RefundInstructions { get; }
    public ISignatureInstructionsClient SignatureInstructions { get; }
    public ISignaturesClient Signatures { get; }
}

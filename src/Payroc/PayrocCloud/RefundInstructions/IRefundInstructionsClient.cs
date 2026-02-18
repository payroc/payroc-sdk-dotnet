using Payroc;

namespace Payroc.PayrocCloud.RefundInstructions;

public partial interface IRefundInstructionsClient
{
    /// <summary>
    /// Use this method to submit an instruction request to initiate a refund on a payment device.
    ///
    /// In the request, include the refund amount and currency.
    ///
    /// If the request is successful, our gateway returns information about the refund instruction and a refundInstructionId, which you need for the following methods:
    /// - [Retrieve refund instruction](https://docs.payroc.com/api/schema/payroc-cloud/refund-instructions/retrieve) - View the details of the refund instruction.
    /// - [Cancel refund instruction](https://docs.payroc.com/api/schema/payroc-cloud/refund-instructions/delete) - Cancel the refund instruction.
    /// </summary>
    WithRawResponseTask<RefundInstruction> SubmitAsync(
        RefundInstructionRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a refund instruction.
    ///
    /// To retrieve a refund instruction, you need its refundInstructionId. Our gateway returned the refundInstructionId in the response of the [Submit Refund Instruction](https://docs.payroc.com/api/schema/payroc-cloud/refund-instructions/submit) method.
    ///
    /// Our gateway returns the status of the refund instruction. If the payment device completed the refund instruction, the response also includes a link to the refund.
    /// </summary>
    WithRawResponseTask<RefundInstruction> RetrieveAsync(
        RetrieveRefundInstructionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to cancel a refund instruction.
    ///
    /// You can cancel a refund instruction only if its status is `inProgress`. To retrieve the status of a refund instruction, use our [Retrieve Refund Instruction](https://docs.payroc.com/api/schema/payroc-cloud/refund-instructions/retrieve) method.
    ///
    /// To cancel a refund instruction, you need its refundInstructionId. Our gateway returned the refundInstructionId in the response of the [Submit Refund Instruction](https://docs.payroc.com/api/schema/payroc-cloud/refund-instructions/submit) method.
    /// </summary>
    Task DeleteAsync(
        DeleteRefundInstructionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

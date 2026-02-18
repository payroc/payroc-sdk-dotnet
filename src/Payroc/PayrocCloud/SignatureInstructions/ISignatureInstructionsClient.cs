using Payroc;

namespace Payroc.PayrocCloud.SignatureInstructions;

public partial interface ISignatureInstructionsClient
{
    /// <summary>
    /// Use this method to submit an instruction to capture a customer's signature on a payment device.
    ///
    /// Our gateway returns information about the signature instruction and a signatureInstructionId, which you need for the following methods:
    /// - [Retrieve signature instruction](https://docs.payroc.com/api/schema/payroc-cloud/signature-instructions/retrieve) - View the details of the signature instruction.
    /// - [Cancel signature instruction](https://docs.payroc.com/api/schema/payroc-cloud/signature-instructions/delete) - Cancel the signature instruction.
    /// </summary>
    WithRawResponseTask<SignatureInstruction> SubmitAsync(
        SignatureInstructionRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a signature instruction.
    ///
    /// To retrieve a signature instruction, you need its signatureInstructionId. Our gateway returned the signatureInstructionId in the response of the [Submit Signature Instruction](https://docs.payroc.com/api/schema/payroc-cloud/signature-instructions/submit) method.
    ///
    /// Our gateway returns the status of the instruction. If the payment device completed the instruction, the response also includes a link to retrieve the signature.
    /// </summary>
    WithRawResponseTask<SignatureInstruction> RetrieveAsync(
        RetrieveSignatureInstructionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to cancel a signature instruction.
    ///
    /// To cancel a signature instruction, you need its signatureInstructionId. Our gateway returned the signatureInstructionId in the response of the [Submit signature instruction](https://docs.payroc.com/api/schema/payroc-cloud/signature-instructions/submit) method.
    /// </summary>
    Task DeleteAsync(
        DeleteSignatureInstructionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

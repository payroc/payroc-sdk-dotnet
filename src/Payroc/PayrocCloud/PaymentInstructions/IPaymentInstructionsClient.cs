using Payroc;

namespace Payroc.PayrocCloud.PaymentInstructions;

public partial interface IPaymentInstructionsClient
{
    /// <summary>
    /// Use this method to submit an instruction request to initiate a sale on a payment device.
    ///
    /// In the request, include the order amount and currency.
    ///
    /// When you send a successful request, our gateway returns information about the payment instruction and a paymentInstructionId, which you need for the following methods:
    /// - [Retrieve payment instruction](https://docs.payroc.com/api/schema/payroc-cloud/payment-instructions/retrieve) - View the details of the payment instruction.
    /// - [Cancel payment instruction](https://docs.payroc.com/api/schema/payroc-cloud/payment-instructions/delete) - Cancel the payment instruction.
    /// </summary>
    WithRawResponseTask<PaymentInstruction> SubmitAsync(
        PaymentInstructionRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a payment instruction.
    ///
    /// To retrieve a payment instruction, you need its paymentInstructionId. Our gateway returned the paymentInstructionId in the response of the [Submit Payment Instruction](https://docs.payroc.com/api/schema/payroc-cloud/payment-instructions/submit) method.
    ///
    /// Our gateway returns the status of the payment instruction. If the payment device completed the payment instruction, the response also includes a link to the payment.
    /// </summary>
    WithRawResponseTask<PaymentInstruction> RetrieveAsync(
        RetrievePaymentInstructionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to cancel a payment instruction.
    ///
    /// You can cancel a payment instruction only if its status is `inProgress`. To retrieve the status of a payment instruction, use our [Retrieve Payment Instruction](https://docs.payroc.com/api/schema/payroc-cloud/payment-instructions/retrieve) method.
    ///
    /// To cancel a payment instruction, you need its paymentInstructionId. Our gateway returned the paymentInstructionId in the response of the [Submit Payment Instruction](https://docs.payroc.com/api/schema/payroc-cloud/payment-instructions/submit) method.
    /// </summary>
    Task DeleteAsync(
        DeletePaymentInstructionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

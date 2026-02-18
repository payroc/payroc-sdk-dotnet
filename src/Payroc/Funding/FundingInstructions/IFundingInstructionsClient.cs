using Payroc;
using Payroc.Core;

namespace Payroc.Funding.FundingInstructions;

public partial interface IFundingInstructionsClient
{
    /// <summary>
    /// &gt; Important: You can return a list of funding instructions from only the previous two years. If you want to view a funding instruction from more than two years ago and you have its instructionId, use our [Retrieve Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/retrieve) method.
    ///
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of funding instructions within a specific date range.
    ///
    /// **Note:** If you want to view the details of a specific funding instruction and you have its instructionId, use our [Retrieve Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/retrieve) method.
    ///
    /// Our gateway returns the following information for each instruction in the list:
    /// -	Status of the funding instruction.
    /// -	Funding information, including which merchant's funding balance we distribute and the funding account that we send the balance to.
    ///
    /// For each funding instruction, we also return the instructionId, which you can use to perform follow-on actions.
    /// </summary>
    Task<PayrocPager<ListFundingInstructionsResponseDataItem>> ListAsync(
        ListFundingInstructionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to create a funding instruction that tells us how to distribute the funds from your merchants' transactions.
    ///
    /// **Note:** Before you create a funding instruction, you can use our [List Funding Balances](https://docs.payroc.com/api/schema/funding/funding-activity/retrieve-balance) method to view the amount of available funds that a merchant has.
    ///
    /// In your request, include an array of merchantInstruction objects. Each merchantInstruction object contains the following:
    /// -	Merchant ID (MID) of the merchant whose funding balance you want to distribute.
    /// -	Funding account that you want to send funds to.
    /// -	Amount that you want to send to the funding account.
    ///
    /// Our gateway returns the instructionId, which you can use to run follow-on actions.
    /// </summary>
    WithRawResponseTask<Instruction> CreateAsync(
        CreateFundingInstructionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a funding instruction.
    ///
    /// To retrieve a funding instruction, you need its instructionId. Our gateway returned the instructionId in the response of the [Create Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/create) method.
    ///
    /// **Note:** If you don't have the instructionId, use our [List Funding Instructions](https://docs.payroc.com/api/schema/funding/funding-instructions/list) method to search for the funding instruction.
    ///
    /// Our gateway returns the following information about the funding instruction:
    /// -	Status of the funding instruction.
    /// -	Funding information, including which merchant's funding balance we distribute and the funding account that we send the balance to.
    /// </summary>
    WithRawResponseTask<Instruction> RetrieveAsync(
        RetrieveFundingInstructionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// &gt; **Important:** You can update a funding instruction only if its status is `accepted`. To view the status of a funding instruction, use our [Retrieve Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/retrieve) method.
    ///
    /// Use this method to update the details of a funding instruction.
    ///
    /// To update a funding instruction, you need its instructionId. Our gateway returned the instructionId in the response of the [Create Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/create) method.
    ///
    /// **Note:** If you don't have the fundingInstructionId, use our [List Funding Instructions](https://docs.payroc.com/api/schema/funding/funding-instructions/list) method to search for the funding instruction.
    ///
    /// You can modify the following information for the funding instruction:
    /// -	Merchant ID (MID) of the merchant whose funding balance you want to distribute.
    /// -	Funding account that you want to send funds to.
    /// -	Amount that you want to send to the funding account.
    /// </summary>
    Task UpdateAsync(
        UpdateFundingInstructionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// &gt; **Important:** You can delete a funding instruction only if its status is `accepted`. To view the status of a funding instruction, use our [Retrieve Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/retrieve) method.
    ///
    /// Use this method to delete a funding instruction.
    ///
    /// To delete a funding instruction, you need its instructionId. Our gateway returned the instructionId in the response of the [Create Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/create) method.
    ///
    /// **Note:** If you don't have the instructionId, use our [List Funding Instructions](https://docs.payroc.com/api/schema/funding/funding-instructions/list) method to search for the funding instruction.
    /// </summary>
    Task DeleteAsync(
        DeleteFundingInstructionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

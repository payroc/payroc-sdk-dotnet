using Payroc;

namespace Payroc.Boarding.Owners;

public partial interface IOwnersClient
{
    /// <summary>
    /// Use this method to retrieve details about an owner of a processing account or an owner associated with a funding recipient.
    ///
    /// To retrieve an owner, you need their ownerId. Our gateway returned the ownerId in the response of the [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method or the [Create Funding Recipient Owner](https://docs.payroc.com/api/schema/funding/funding-recipients/create-owner) method.
    ///
    /// **Note:** If you don't have the ownerId, use the [Retrieve Processing Account](https://docs.payroc.com/api/schema/boarding/processing-accounts/retrieve) method if you are searching for a processing account owner, or use the [List Funding Recipient Owners](https://docs.payroc.com/api/schema/funding/funding-recipients/list-owners) method if you are searching for a funding recipient owner.
    ///
    /// Our gateway returns the following information about an owner:
    /// - Name, date of birth, and address.
    /// - Contact details, including their email address.
    /// - Relationship to the business, including whether they are a control prong or authorized signatory, and their equity stake in the business.
    /// </summary>
    WithRawResponseTask<Owner> RetrieveAsync(
        RetrieveOwnersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// &gt; **Important:** You can't update the details of an owner of a processing account.
    ///
    /// Use this method to update the details of an owner associated with a funding recipient.
    ///
    /// To update an owner, you need their ownerId. Our gateway returned the ownerId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method and the [Create Funding Recipient Owner](https://docs.payroc.com/api/schema/funding/funding-recipients/create-owner) method.
    ///
    /// **Note:** If you don't have the ownerId, use the [List Funding Recipient Owners](https://docs.payroc.com/api/schema/funding/funding-recipients/list-owners) method, the [Retrieve Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/retrieve) method, or the [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient owner.
    ///
    /// You can update the following details about an owner:
    ///
    /// - Personal details, including their name, date of birth, and address.
    /// - Identification details, including their identification type and number.
    /// - Contact details, including their email address.
    /// - Relationship to the business, including whether they are a control prong.
    /// </summary>
    Task UpdateAsync(
        UpdateOwnersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// &gt; **Important:** You can't delete an owner of a processing account.
    ///
    /// Use this method to delete an owner associated with a funding recipient. You can delete an owner only if the funding recipient has more than one owner.
    ///
    /// To delete an owner, you need their ownerId. Our gateway returned the ownerId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method and the [Create Funding Recipient Owner](https://docs.payroc.com/api/schema/funding/funding-recipients/create-owner) method.
    ///
    /// **Note:** If you don't have the ownerId, use the [List Funding Recipient Owners](https://docs.payroc.com/api/schema/funding/funding-recipients/list-owners) method, the [Retrieve Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/retrieve) method, or the [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient owner.
    /// </summary>
    Task DeleteAsync(
        DeleteOwnersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

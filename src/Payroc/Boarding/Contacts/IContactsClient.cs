using Payroc;

namespace Payroc.Boarding.Contacts;

public partial interface IContactsClient
{
    /// <summary>
    /// Use this method to retrieve details about a contact.
    ///
    /// To retrieve a contact, you need its contactId. Our gateway returned the contactId in the [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method.
    ///
    /// **Note:** If you don't have the contactId, use the [List Contacts](https://docs.payroc.com/api/schema/boarding/processing-accounts/list-contacts) method to search for the contact.
    ///
    /// Our gateway returns the following information about a contact:
    ///
    /// -	Name and contact method, including their phone number or mobile number.
    /// -	Role within the business, for example, if they are a manager.
    /// </summary>
    WithRawResponseTask<Contact> RetrieveAsync(
        RetrieveContactsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to update a contact of a processing account.
    ///
    /// To update a contact, you need its contactId. Our gateway returned the contactId in the response of the [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method.
    ///
    /// **Note:** If you don't have the contactId, use our [List Contacts](https://docs.payroc.com/api/schema/boarding/processing-accounts/list-contacts) method to search for the contact.
    ///
    /// You can update the following details about a contact:
    ///
    /// -	First name and last name.
    /// -	Contact details, including their phone number or mobile number.
    /// -	Identification details, including their identification type and number.
    /// -	Role within the business, for example, if they are a manager.
    /// </summary>
    Task UpdateAsync(
        UpdateContactsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to delete a contact associated with a processing account.
    ///
    /// To delete a contact, you need their contactId. Our gateway returned the contactId in the response of the [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method.
    ///
    /// **Note:** If you don’t have the contactId, use our [Retrieve Processing Account](https://docs.payroc.com/api/schema/boarding/processing-accounts/retrieve) method or our [List Contacts](https://docs.payroc.com/api/schema/boarding/processing-accounts/list-contacts) method to search for the contact.
    /// </summary>
    Task DeleteAsync(
        DeleteContactsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

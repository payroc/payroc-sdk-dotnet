namespace Payroc.TestFunctional.Factories.Funding.RequestBodies;

public class FundingRecipientFactory
{
    public static FundingRecipient Create() => new()
    {
        RecipientType = FundingRecipientRecipientType.PrivateCorporation,
        TaxId = "123456789",
        DoingBusinessAs = "Pizza Doe",
        Address = Factories.AddressFactory.CreateAddress(),
        ContactMethods =  [
            new ContactMethod.Email(new() { Value = "jane.doe@example.com" })
        ]
    };
}
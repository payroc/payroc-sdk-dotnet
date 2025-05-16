namespace Payroc.TestHarness.Factory;

public class CreateProcessingAccountFactory
{
    public static CreateProcessingAccount Create()
        => new()
        {
            Owners =
            [
                new()
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    DateOfBirth = "1964-03-22",
                    Address = AddressFactory.CreateAddress(),
                    Identifiers =
                    [
                        new()
                        {
                            Type = "nationalId",
                            Value = "xxxxx4320"
                        }
                    ],
                    Relationship = new()
                    {
                        IsControlProng = true
                    },
                }
            ],
            ContactMethods =
            [
                new ContactMethod.Email(new() { Value = "jane.doe@example.com" })
            ],
            DoingBusinessAs = "Pizza Doe",
            BusinessType = CreateProcessingAccountBusinessType.Restaurant,
            CategoryCode = 123,
            MerchandiseOrServiceSold = "Pizza",
            BusinessStartDate = "05/05/2025",
            Timezone = CreateProcessingAccountTimezone.AmericaChicago,
            Address = AddressFactory.CreateAddress(),
            Processing = new()
            {
                TransactionAmounts = new() { Average = 5000, Highest = 10000 },
                MonthlyAmounts = new() { Average = 50000, Highest = 100000 },
                VolumeBreakdown = new() { CardPresentKeyed = 47, CardPresentSwiped = 30, MailOrTelephone = 3, Ecommerce = 20 }
            },
            Funding = new() { },
            Pricing = new(new Pricing.Intent(new() { PricingIntentId = 6123 })) { },
            Signature = CreateProcessingAccountSignature.RequestedViaEmail
        };
}

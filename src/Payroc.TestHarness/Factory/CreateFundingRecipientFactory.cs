using Payroc.Funding.FundingRecipients;

namespace Payroc.TestHarness.Factory;

public class CreateFundingRecipientFactory
{
    public static CreateFundingRecipient Create()
        => new()
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            RecipientType = CreateFundingRecipientRecipientType.PrivateLlc,
            TaxId = "123456789",
            CharityId = "123456789",
            DoingBusinessAs = "Pizza Doe",
            Address = AddressFactory.CreateAddress(),
            ContactMethods =
            [
                new ContactMethod.Email(new() { Value = "jane.doe@example.com" }),
                new ContactMethod.Phone(new() { Value = "123-45-6789" }),
            ],
            Metadata = new()
            {
                ["customField1"] = "customValue1",
                ["customField2"] = "customValue2"
            },
            Owners =
            [
                new()
                {
                    FirstName = "Mark",
                      LastName= "FirstPerson4101",
                      DateOfBirth= new (1964, 03, 22),
                      Address= new()
                      {
                        Address1= "1 Person Ln.",
                        Address2= null,
                        Address3= null,
                        City= "Mount Primary",
                        State= "Texas",
                        Country= "US",
                        PostalCode= "77477"
                      },
                      Identifiers=
                      [
                        new(){
                          Type= "nationalId",
                          Value= "111-11-1111"
                        }
                      ],
                    ContactMethods =
                    [
                        new ContactMethod.Email(new() { Value = "jane.doe@example.com" })
                    ],
                    Relationship=new()
                    {
                        EquityPercentage= 50.0f,
                        Title= "string",
                        IsControlProng= true
                    }
                }
            ],
            FundingAccounts =
            [
                new FundingAccount
                {
                    Type  = FundingAccountType.Checking,
                    Use  = FundingAccountUse.Credit,
                    NameOnAccount  = "Jane Doe",
                    PaymentMethods  =
                    [
                        new (new PaymentMethodsItem.Ach(new(){ Value = new(){ RoutingNumber="063100277", AccountNumber="321831591" } })){ }
                    ]
                },
                new FundingAccount
                {
                    Type  = FundingAccountType.Checking,
                    Use  = FundingAccountUse.Credit, // "CreditAndDebit is not a valid use"
                    NameOnAccount  = "John Doe",
                    PaymentMethods  =
                    [
                        new (new PaymentMethodsItem.Ach(new(){ Value = new(){ RoutingNumber="063100277", AccountNumber="321831592" } })){ }
                    ]
                }
            ]
        };
}

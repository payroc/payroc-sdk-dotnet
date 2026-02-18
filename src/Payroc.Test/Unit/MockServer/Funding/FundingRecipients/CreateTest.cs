using NUnit.Framework;
using Payroc;
using Payroc.Funding.FundingRecipients;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Funding.FundingRecipients;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "recipientType": "privateCorporation",
              "taxId": "12-3456789",
              "doingBusinessAs": "Pizza Doe",
              "address": {
                "address1": "1 Example Ave.",
                "address2": "Example Address Line 2",
                "address3": "Example Address Line 3",
                "city": "Chicago",
                "state": "Illinois",
                "country": "US",
                "postalCode": "60056"
              },
              "contactMethods": [
                {
                  "type": "email",
                  "value": "jane.doe@example.com"
                },
                {
                  "type": "phone",
                  "value": "2025550164"
                }
              ],
              "metadata": {
                "yourCustomField": "abc123"
              },
              "owners": [
                {
                  "firstName": "Jane",
                  "middleName": "Helen",
                  "lastName": "Doe",
                  "dateOfBirth": "1964-03-22",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "identifiers": [
                    {
                      "type": "nationalId",
                      "value": "000-00-4320"
                    }
                  ],
                  "contactMethods": [
                    {
                      "type": "email",
                      "value": "jane.doe@example.com"
                    },
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "relationship": {
                    "equityPercentage": 48.5,
                    "title": "CFO",
                    "isControlProng": true,
                    "isAuthorizedSignatory": false
                  }
                }
              ],
              "fundingAccounts": [
                {
                  "type": "checking",
                  "use": "credit",
                  "nameOnAccount": "Jane Doe",
                  "paymentMethods": [
                    {
                      "type": "ach"
                    }
                  ]
                }
              ]
            }
            """;

        const string mockResponse = """
            {
              "recipientType": "privateCorporation",
              "taxId": "123456789",
              "charityId": "charityId",
              "doingBusinessAs": "doingBusinessAs",
              "address": {
                "address1": "1 Example Ave.",
                "address2": "Example Address Line 2",
                "address3": "Example Address Line 3",
                "city": "Chicago",
                "state": "Illinois",
                "country": "US",
                "postalCode": "60056"
              },
              "contactMethods": [
                {
                  "type": "email",
                  "value": "jane.doe@example.com"
                }
              ],
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-recipients")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Funding.FundingRecipients.CreateAsync(
            new CreateFundingRecipient
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                RecipientType = CreateFundingRecipientRecipientType.PrivateCorporation,
                TaxId = "12-3456789",
                DoingBusinessAs = "Pizza Doe",
                Address = new Address
                {
                    Address1 = "1 Example Ave.",
                    Address2 = "Example Address Line 2",
                    Address3 = "Example Address Line 3",
                    City = "Chicago",
                    State = "Illinois",
                    Country = "US",
                    PostalCode = "60056",
                },
                ContactMethods = new List<ContactMethod>()
                {
                    new ContactMethod(
                        new ContactMethod.Email(
                            new ContactMethodEmail { Value = "jane.doe@example.com" }
                        )
                    ),
                    new ContactMethod(
                        new ContactMethod.Phone(new ContactMethodPhone { Value = "2025550164" })
                    ),
                },
                Metadata = new Dictionary<string, string>() { { "yourCustomField", "abc123" } },
                Owners = new List<Owner>()
                {
                    new Owner
                    {
                        FirstName = "Jane",
                        MiddleName = "Helen",
                        LastName = "Doe",
                        DateOfBirth = new DateOnly(1964, 3, 22),
                        Address = new Address
                        {
                            Address1 = "1 Example Ave.",
                            City = "Chicago",
                            State = "Illinois",
                            Country = "US",
                            PostalCode = "60056",
                        },
                        Identifiers = new List<Identifier>()
                        {
                            new Identifier
                            {
                                Type = IdentifierType.NationalId,
                                Value = "000-00-4320",
                            },
                        },
                        ContactMethods = new List<ContactMethod>()
                        {
                            new ContactMethod(
                                new ContactMethod.Email(
                                    new ContactMethodEmail { Value = "jane.doe@example.com" }
                                )
                            ),
                            new ContactMethod(
                                new ContactMethod.Phone(
                                    new ContactMethodPhone { Value = "2025550164" }
                                )
                            ),
                        },
                        Relationship = new OwnerRelationship
                        {
                            EquityPercentage = 48.5f,
                            Title = "CFO",
                            IsControlProng = true,
                            IsAuthorizedSignatory = false,
                        },
                    },
                },
                FundingAccounts = new List<FundingAccount>()
                {
                    new FundingAccount
                    {
                        Type = FundingAccountType.Checking,
                        Use = FundingAccountUse.Credit,
                        NameOnAccount = "Jane Doe",
                        PaymentMethods = new List<PaymentMethodsItem>()
                        {
                            new PaymentMethodsItem(
                                new PaymentMethodsItem.Ach(new PaymentMethodAch())
                            ),
                        },
                    },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}

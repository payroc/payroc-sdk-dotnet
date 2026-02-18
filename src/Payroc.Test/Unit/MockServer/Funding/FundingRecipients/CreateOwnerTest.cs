using NUnit.Framework;
using Payroc;
using Payroc.Funding.FundingRecipients;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Funding.FundingRecipients;

[TestFixture]
public class CreateOwnerTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "firstName": "Fred",
              "middleName": "Jim",
              "lastName": "Nerk",
              "dateOfBirth": "1980-01-19",
              "address": {
                "address1": "2 Example Ave.",
                "city": "Chicago",
                "state": "Illinois",
                "country": "US",
                "postalCode": "60056"
              },
              "identifiers": [
                {
                  "type": "nationalId",
                  "value": "000-00-9876"
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
                "equityPercentage": 51.5,
                "title": "CEO",
                "isControlProng": false,
                "isAuthorizedSignatory": true
              }
            }
            """;

        const string mockResponse = """
            {
              "firstName": "Jane",
              "middleName": "Helen",
              "lastName": "Doe",
              "dateOfBirth": "1964-03-22",
              "address": {
                "address1": "1 Example Ave.",
                "address2": "Example Address Line 2",
                "address3": "Example Address Line 3",
                "city": "Chicago",
                "state": "Illinois",
                "country": "US",
                "postalCode": "60056"
              },
              "identifiers": [
                {
                  "type": "nationalId",
                  "value": "xxxxx4320"
                }
              ],
              "contactMethods": [
                {
                  "type": "email",
                  "value": "jane.doe@example.com"
                }
              ],
              "relationship": {
                "equityPercentage": 48.5,
                "title": "CFO",
                "isControlProng": true,
                "isAuthorizedSignatory": false
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-recipients/1/owners")
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

        var response = await Client.Funding.FundingRecipients.CreateOwnerAsync(
            new CreateOwnerFundingRecipientsRequest
            {
                RecipientId = 1,
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new Owner
                {
                    FirstName = "Fred",
                    MiddleName = "Jim",
                    LastName = "Nerk",
                    DateOfBirth = new DateOnly(1980, 1, 19),
                    Address = new Address
                    {
                        Address1 = "2 Example Ave.",
                        City = "Chicago",
                        State = "Illinois",
                        Country = "US",
                        PostalCode = "60056",
                    },
                    Identifiers = new List<Identifier>()
                    {
                        new Identifier { Type = IdentifierType.NationalId, Value = "000-00-9876" },
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
                    Relationship = new OwnerRelationship
                    {
                        EquityPercentage = 51.5f,
                        Title = "CEO",
                        IsControlProng = false,
                        IsAuthorizedSignatory = true,
                    },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}

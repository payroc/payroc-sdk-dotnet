using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Funding.FundingRecipients;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Funding.FundingRecipients;

[TestFixture]
public class CreateOwnerTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "firstName": "Jane",
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
                  "value": "xxxxx4320"
                }
              ],
              "contactMethods": [
                {
                  "value": "jane.doe@example.com",
                  "type": "email"
                }
              ],
              "relationship": {
                "isControlProng": true
              }
            }
            """;

        const string mockResponse = """
            {
              "ownerId": 4564,
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
                  "value": "jane.doe@example.com",
                  "type": "email"
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
                    .WithHeader("Idempotency-Key", "Idempotency-Key")
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
                IdempotencyKey = "Idempotency-Key",
                Body = new Owner
                {
                    FirstName = "Jane",
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
                        new Identifier { Type = "nationalId", Value = "xxxxx4320" },
                    },
                    ContactMethods = new List<ContactMethod>()
                    {
                        new ContactMethod(
                            new ContactMethod.Email(
                                new ContactMethodEmail { Value = "jane.doe@example.com" }
                            )
                        ),
                    },
                    Relationship = new OwnerRelationship { IsControlProng = true },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Owner>(mockResponse)).UsingDefaults()
        );
    }
}

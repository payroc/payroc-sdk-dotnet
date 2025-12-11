using NUnit.Framework;
using Payroc;
using Payroc.Boarding.Owners;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.Owners;

[TestFixture]
public class UpdateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public void MockServerTest()
    {
        const string requestJson = """
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
                  "value": "000-00-4320"
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
                    .WithPath("/owners/1")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPut()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.Boarding.Owners.UpdateAsync(
                new UpdateOwnersRequest
                {
                    OwnerId = 1,
                    Body = new Owner
                    {
                        FirstName = "Jane",
                        MiddleName = "Helen",
                        LastName = "Doe",
                        DateOfBirth = new DateOnly(1964, 3, 22),
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
                        },
                        Relationship = new OwnerRelationship
                        {
                            EquityPercentage = 48.5f,
                            Title = "CFO",
                            IsControlProng = true,
                            IsAuthorizedSignatory = false,
                        },
                    },
                }
            )
        );
    }
}

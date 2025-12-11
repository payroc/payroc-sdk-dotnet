using NUnit.Framework;
using Payroc;
using Payroc.Boarding.Owners;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.Owners;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
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
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/owners/1").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.Owners.RetrieveAsync(
            new RetrieveOwnersRequest { OwnerId = 1 }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Owner>(mockResponse)).UsingDefaults()
        );
    }
}

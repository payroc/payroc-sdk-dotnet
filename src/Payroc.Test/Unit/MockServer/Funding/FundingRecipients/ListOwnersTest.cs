using NUnit.Framework;
using Payroc.Funding.FundingRecipients;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Funding.FundingRecipients;

[TestFixture]
public class ListOwnersTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            [
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
            ]
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-recipients/1/owners")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Funding.FundingRecipients.ListOwnersAsync(
            new ListFundingRecipientOwnersRequest { RecipientId = 1 }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}

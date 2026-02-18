using NUnit.Framework;
using Payroc.Boarding.Contacts;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Boarding.Contacts;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "type": "manager",
              "firstName": "Jane",
              "middleName": "Helen",
              "lastName": "Doe",
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
                },
                {
                  "type": "phone",
                  "value": "2025550164"
                },
                {
                  "type": "mobile",
                  "value": "8445557624"
                },
                {
                  "type": "fax",
                  "value": "2025550110"
                }
              ]
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/contacts/1").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.Contacts.RetrieveAsync(
            new RetrieveContactsRequest { ContactId = 1 }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}

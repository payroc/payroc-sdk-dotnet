using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Boarding.Contacts;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.Contacts;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "contactId": 1543,
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
                  "value": "jane.doe@example.com",
                  "type": "email"
                },
                {
                  "value": "2025550164",
                  "type": "phone"
                },
                {
                  "value": "8445557624",
                  "type": "mobile"
                },
                {
                  "value": "2025550110",
                  "type": "fax"
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
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Contact>(mockResponse)).UsingDefaults()
        );
    }
}

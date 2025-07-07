using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.Core;

namespace Payroc.Test.Unit.MockServer;

[TestFixture]
public class ListContactsTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
    {
        const string mockResponse = """
            {
              "limit": 2,
              "count": 2,
              "hasMore": true,
              "links": [
                {
                  "rel": "previous",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/processing-accounts/38765/contacts?before=1543&limit=2"
                },
                {
                  "rel": "next",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/processing-accounts/38765/contacts?after=87926&limit=2"
                }
              ],
              "data": [
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
                      "value": "2025550164",
                      "type": "phone"
                    },
                    {
                      "value": "8445557624",
                      "type": "mobile"
                    },
                    {
                      "value": "jane.doe@example.com",
                      "type": "email"
                    }
                  ]
                },
                {
                  "contactId": 87926,
                  "type": "representative",
                  "firstName": "Fred",
                  "middleName": "Jim",
                  "lastName": "Nerk",
                  "identifiers": [
                    {
                      "type": "nationalId",
                      "value": "xxxxx9876"
                    }
                  ],
                  "contactMethods": [
                    {
                      "value": "2025550164",
                      "type": "phone"
                    },
                    {
                      "value": "8445557624",
                      "type": "mobile"
                    },
                    {
                      "value": "jane.doe@example.com",
                      "type": "email"
                    }
                  ]
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-accounts/38765/contacts")
                    .WithParam("before", "2571")
                    .WithParam("after", "8516")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.ProcessingAccounts.ListContactsAsync(
            new ListContactsProcessingAccountsRequest
            {
                ProcessingAccountId = "38765",
                Before = "2571",
                After = "8516",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PaginatedContacts>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_2()
    {
        const string mockResponse = """
            {
              "limit": 10,
              "count": 0,
              "hasMore": false,
              "links": [
                {
                  "rel": "previous",
                  "method": "get",
                  "href": "<uri>"
                },
                {
                  "rel": "next",
                  "method": "get",
                  "href": "<uri>"
                }
              ],
              "data": [
                {
                  "contactId": 1543,
                  "type": "manager",
                  "firstName": "Jane",
                  "middleName": "Helen",
                  "lastName": "Doe",
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
                  ]
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-accounts/38765/contacts")
                    .WithParam("before", "2571")
                    .WithParam("after", "8516")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.ProcessingAccounts.ListContactsAsync(
            new ListContactsProcessingAccountsRequest
            {
                ProcessingAccountId = "38765",
                Before = "2571",
                After = "8516",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PaginatedContacts>(mockResponse)).UsingDefaults()
        );
    }
}

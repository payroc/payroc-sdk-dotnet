using NUnit.Framework;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Boarding.ProcessingAccounts;

[TestFixture]
public class ListContactsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest_1()
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
                      "type": "phone",
                      "value": "2025550164"
                    },
                    {
                      "type": "mobile",
                      "value": "8445557624"
                    },
                    {
                      "type": "email",
                      "value": "jane.doe@example.com"
                    }
                  ]
                },
                {
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
                      "type": "phone",
                      "value": "2025550164"
                    },
                    {
                      "type": "mobile",
                      "value": "8445557624"
                    },
                    {
                      "type": "email",
                      "value": "jane.doe@example.com"
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
                    .WithParam("limit", "1")
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
                Limit = 1,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_2()
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
                      "type": "email",
                      "value": "jane.doe@example.com"
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
                    .WithParam("limit", "1")
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
                Limit = 1,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}

using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.ProcessingAccounts;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "processingAccountId": "38765",
              "createdDate": "2024-07-02T12:00:00.000Z",
              "lastModifiedDate": "2024-07-02T12:00:00.000Z",
              "status": "approved",
              "doingBusinessAs": "Pizza Doe",
              "owners": [
                {
                  "ownerId": 4564,
                  "firstName": "Jane",
                  "lastName": "Doe",
                  "link": {
                    "rel": "owner",
                    "href": "https://api.payroc.com/v1/owners/1543",
                    "method": "get"
                  }
                }
              ],
              "website": "www.example.com",
              "businessType": "restaurant",
              "categoryCode": 5999,
              "merchandiseOrServiceSold": "Pizza",
              "businessStartDate": "2020-01-01",
              "timezone": "America/Chicago",
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
                  "value": "jane.doe@example.com",
                  "type": "email"
                }
              ],
              "processing": {
                "merchantId": "444412365478965",
                "transactionAmounts": {
                  "average": 5000,
                  "highest": 10000
                },
                "monthlyAmounts": {
                  "average": 50000,
                  "highest": 100000
                },
                "volumeBreakdown": {
                  "cardPresentKeyed": 47,
                  "cardPresentSwiped": 30,
                  "mailOrTelephone": 3,
                  "ecommerce": 20
                },
                "isSeasonal": true,
                "monthsOfOperation": [
                  "jan",
                  "feb"
                ],
                "ach": {
                  "naics": "5812",
                  "previouslyTerminatedForAch": false,
                  "refunds": {
                    "writtenRefundPolicy": true,
                    "refundPolicyUrl": "www.example.com/refund-poilcy-url"
                  },
                  "estimatedMonthlyTransactions": 3000,
                  "limits": {
                    "singleTransaction": 10000,
                    "dailyDeposit": 200000,
                    "monthlyDeposit": 6000000
                  },
                  "transactionTypes": [
                    "prearrangedPayment",
                    "other"
                  ],
                  "transactionTypesOther": "anotherTransactionType"
                },
                "cardAcceptance": {
                  "debitOnly": false,
                  "hsaFsa": false,
                  "cardsAccepted": [
                    "visa",
                    "mastercard"
                  ],
                  "specialityCards": {
                    "americanExpressDirect": {
                      "enabled": true,
                      "merchantNumber": "abc1234567"
                    },
                    "electronicBenefitsTransfer": {
                      "enabled": true,
                      "fnsNumber": "6789012"
                    },
                    "other": {
                      "wexMerchantNumber": "abc1234567",
                      "voyagerMerchantId": "abc1234567",
                      "fleetMerchantId": "abc1234567"
                    }
                  }
                }
              },
              "funding": {
                "status": "enabled",
                "fundingSchedule": "nextday",
                "acceleratedFundingFee": 1999,
                "dailyDiscount": false,
                "fundingAccounts": [
                  {
                    "fundingAccountId": 123,
                    "status": "pending",
                    "link": {
                      "rel": "fundingAccount",
                      "method": "get",
                      "href": "https://api.payroc.com/v1/funding-accounts/123"
                    }
                  }
                ]
              },
              "pricing": {
                "link": {
                  "rel": "pricing",
                  "href": "https://api.payroc.com/v1/processing-accounts/38765/pricing",
                  "method": "get"
                }
              },
              "contacts": [
                {
                  "contactId": 1543,
                  "firstName": "Jane",
                  "lastName": "Doe",
                  "link": {
                    "rel": "contact",
                    "href": "https://api.payroc.com/v1/contacts/1543",
                    "method": "get"
                  }
                }
              ],
              "signature": {
                "link": {
                  "rel": "agreement",
                  "method": "get",
                  "href": "https://us.agreementexpress.net/mv2/viewer2.jsp?docId=00000000-0000-0000-0000-000000000000"
                },
                "type": "requestedViaDirectLink"
              },
              "metadata": {
                "customerId": "2345"
              },
              "links": [
                {
                  "rel": "previous",
                  "method": "get",
                  "href": "<uri>"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-accounts/38765")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.ProcessingAccounts.RetrieveAsync(
            new RetrieveProcessingAccountsRequest { ProcessingAccountId = "38765" }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<ProcessingAccount>(mockResponse)).UsingDefaults()
        );
    }
}

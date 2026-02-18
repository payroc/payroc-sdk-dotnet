using NUnit.Framework;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Boarding.ProcessingAccounts;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "doingBusinessAs": "Pizza Doe",
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
                  "type": "email",
                  "value": "jane.doe@example.com"
                }
              ],
              "processing": {
                "transactionAmounts": {
                  "average": 5000,
                  "highest": 10000
                },
                "monthlyAmounts": {
                  "average": 50000,
                  "highest": 100000
                },
                "volumeBreakdown": {
                  "cardPresent": 77,
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
                "fundingSchedule": "nextday",
                "acceleratedFundingFee": 1999,
                "dailyDiscount": false
              },
              "pricing": {
                "link": {
                  "rel": "pricing",
                  "href": "https://api.payroc.com/v1/processing-accounts/38765/pricing",
                  "method": "get"
                }
              },
              "signature": {
                "type": "requestedViaDirectLink",
                "link": {
                  "rel": "previous",
                  "method": "get",
                  "href": "<uri>"
                }
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
        JsonAssert.AreEqual(response, mockResponse);
    }
}

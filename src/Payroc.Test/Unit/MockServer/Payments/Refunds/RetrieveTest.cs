using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Payments.Refunds;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Payments.Refunds;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "refundId": "CD3HN88U9F",
              "processingTerminalId": "1234001",
              "operator": "Jane",
              "order": {
                "orderId": "OrderRef6543",
                "dateTime": "2024-07-02T15:30:00.000Z",
                "description": "Refund for order OrderRef6543",
                "amount": 4999,
                "currency": "USD",
                "dccOffer": {
                  "accepted": true,
                  "offerReference": "DCC123456789",
                  "fxAmount": 3955,
                  "fxCurrency": "AED",
                  "fxCurrencyCode": "CAD",
                  "fxCurrencyExponent": 2,
                  "fxRate": 1.37,
                  "markup": 3.5,
                  "markupText": "3.5% mark-up applied.",
                  "provider": "DCC Provider Inc.",
                  "source": "European Central Bank"
                }
              },
              "customer": {
                "firstName": "Sarah",
                "lastName": "Hopper",
                "dateOfBirth": "1990-07-15",
                "referenceNumber": "CustomerCode234567",
                "billingAddress": {
                  "address1": "1 Example Ave.",
                  "address2": "Example Address Line 2",
                  "address3": "Example Address Line 3",
                  "city": "Chicago",
                  "state": "Illinois",
                  "country": "US",
                  "postalCode": "60056"
                },
                "shippingAddress": {
                  "recipientName": "Sarah Hopper",
                  "address": {
                    "address1": "1 Example Ave.",
                    "address2": "Example Address Line 2",
                    "address3": "Example Address Line 3",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  }
                },
                "contactMethods": [
                  {
                    "value": "jane.doe@example.com",
                    "type": "email"
                  }
                ],
                "notificationLanguage": "en"
              },
              "card": {
                "type": "Visa Credit",
                "entryMethod": "keyed",
                "cardholderName": "Sarah Hazel Hopper",
                "cardholderSignature": "a1b1c012345678a000b000c0012345d0e0f010g10061a031i001j071k0a1b0c1d0e1234567890120f1g0h1i0j1k0a1b0123451c012d0e1f0g1h0i1j123k1a1b1c1d1e1f1g123h1i1j1k1a1b1c1d1e1f1g123h123i1j123k12340a120a12345b012c0123012d0d1e0f1g0h1i123j123k10000",
                "cardNumber": "453985******7062",
                "expiryDate": "1225",
                "secureToken": {
                  "secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
                  "customerName": "Sarah Hazel Hopper",
                  "token": "296753123456",
                  "status": "notValidated",
                  "link": {
                    "rel": "previous",
                    "method": "get",
                    "href": "<uri>"
                  }
                },
                "securityChecks": {
                  "cvvResult": "M",
                  "avsResult": "Y"
                },
                "emvTags": [
                  {
                    "hex": "9F36",
                    "value": "001234"
                  },
                  {
                    "hex": "5F2A",
                    "value": "0840"
                  }
                ],
                "balances": [
                  {
                    "benefitCategory": "cash",
                    "amount": 50000,
                    "currency": "USD"
                  },
                  {
                    "benefitCategory": "foodStamp",
                    "amount": 10000,
                    "currency": "USD"
                  }
                ]
              },
              "payment": {
                "paymentId": "M2MJOG6O2Y",
                "dateTime": "2024-07-02T15:30:00.000Z",
                "currency": "AED",
                "amount": 4999,
                "status": "ready",
                "responseCode": "A",
                "responseMessage": "Transaction approved",
                "link": {
                  "rel": "previous",
                  "method": "get",
                  "href": "<uri>"
                }
              },
              "supportedOperations": [
                "capture",
                "refund",
                "fullyReverse",
                "partiallyReverse",
                "incrementAuthorization",
                "adjustTip",
                "addSignature",
                "setAsReady",
                "setAsPending"
              ],
              "transactionResult": {
                "type": "refund",
                "ebtType": "cashPurchase",
                "status": "ready",
                "approvalCode": "000000",
                "authorizedAmount": -4999,
                "currency": "USD",
                "responseCode": "A",
                "responseMessage": "OK5",
                "processorResponseCode": "processorResponseCode",
                "cardSchemeReferenceId": "cardSchemeReferenceId"
              },
              "customFields": [
                {
                  "name": "yourCustomField",
                  "value": "abc123"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock.RequestBuilders.Request.Create().WithPath("/refunds/refundId").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Payments.Refunds.RetrieveAsync(
            new RetrieveRefundsRequest { RefundId = "refundId" }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Refund>(mockResponse)).UsingDefaults()
        );
    }
}

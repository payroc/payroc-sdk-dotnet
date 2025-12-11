using NUnit.Framework;
using Payroc;
using Payroc.CardPayments.Payments;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.CardPayments.Payments;

[TestFixture]
public class AdjustTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "adjustments": [
                {
                  "type": "customer"
                },
                {
                  "amount": 4999,
                  "type": "order"
                }
              ]
            }
            """;

        const string mockResponse = """
            {
              "paymentId": "M2MJOG6O2Y",
              "processingTerminalId": "1234001",
              "operator": "Jane",
              "order": {
                "orderId": "OrderRef6543",
                "dateTime": "2024-07-02T15:30:00.000Z",
                "description": "Example payment",
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
                },
                "standingInstructions": {
                  "sequence": "first",
                  "processingModel": "unscheduled",
                  "referenceDataOfFirstTxn": {
                    "paymentId": "M2MJOG6O2Y",
                    "cardSchemeReferenceId": "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
                  }
                },
                "breakdown": {
                  "subtotal": 2899,
                  "cashbackAmount": 0,
                  "tip": {
                    "type": "percentage",
                    "amount": 500,
                    "percentage": 10
                  },
                  "surcharge": {
                    "bypass": false,
                    "amount": 50,
                    "percentage": 2
                  },
                  "dualPricing": {
                    "offered": false,
                    "choiceRate": {
                      "applied": true,
                      "rate": 2.5,
                      "amount": 75
                    }
                  },
                  "taxes": [
                    {
                      "name": "Sales Tax",
                      "rate": 7,
                      "amount": 190
                    }
                  ],
                  "dutyAmount": 0,
                  "freightAmount": 0,
                  "convenienceFee": {
                    "amount": 25
                  },
                  "items": [
                    {
                      "commodityCode": "5812-0111",
                      "productCode": "PZA-001-LG",
                      "description": "Large Pepperoni Pizza",
                      "unitPrice": 2709,
                      "quantity": 1,
                      "discountRate": 5,
                      "taxes": [
                        {
                          "name": "Sales Tax",
                          "rate": 7,
                          "amount": 190
                        }
                      ]
                    }
                  ]
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
                "type": "MasterCard",
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
              "refunds": [
                {
                  "refundId": "CD3HN88U9F",
                  "dateTime": "2024-07-14T12:25:00.000Z",
                  "currency": "AED",
                  "amount": 4999,
                  "status": "ready",
                  "responseCode": "A",
                  "responseMessage": "Transaction refunded",
                  "link": {
                    "rel": "previous",
                    "method": "get",
                    "href": "<uri>"
                  }
                }
              ],
              "supportedOperations": [
                "capture",
                "fullyReverse",
                "partiallyReverse",
                "incrementAuthorization",
                "adjustTip",
                "setAsPending"
              ],
              "transactionResult": {
                "type": "sale",
                "ebtType": "cashPurchase",
                "status": "ready",
                "approvalCode": "OK6",
                "authorizedAmount": 4999,
                "currency": "USD",
                "responseCode": "A",
                "responseMessage": "OK6",
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
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/payments/M2MJOG6O2Y/adjust")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.CardPayments.Payments.AdjustAsync(
            new PaymentAdjustment
            {
                PaymentId = "M2MJOG6O2Y",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Adjustments = new List<PaymentAdjustmentAdjustmentsItem>()
                {
                    new PaymentAdjustmentAdjustmentsItem(
                        new Payroc.CardPayments.Payments.PaymentAdjustmentAdjustmentsItem.Customer(
                            new CustomerAdjustment()
                        )
                    ),
                    new PaymentAdjustmentAdjustmentsItem(
                        new Payroc.CardPayments.Payments.PaymentAdjustmentAdjustmentsItem.Order(
                            new OrderAdjustment { Amount = 4999 }
                        )
                    ),
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Payment>(mockResponse)).UsingDefaults()
        );
    }
}

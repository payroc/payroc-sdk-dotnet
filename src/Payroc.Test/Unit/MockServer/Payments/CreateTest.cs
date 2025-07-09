using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Payments;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Payments;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
    {
        const string requestJson = """
            {
              "channel": "web",
              "processingTerminalId": "1234001",
              "operator": "Jane",
              "order": {
                "orderId": "OrderRef6543",
                "description": "Large Pepperoni Pizza",
                "amount": 4999,
                "currency": "USD"
              },
              "customer": {
                "firstName": "Sarah",
                "lastName": "Hopper",
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
                }
              },
              "paymentMethod": {
                "cardDetails": {
                  "device": {
                    "model": "bbposChp",
                    "serialNumber": "1850010868"
                  },
                  "rawData": "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                  "entryMethod": "raw"
                },
                "type": "card"
              },
              "customFields": [
                {
                  "name": "yourCustomField",
                  "value": "abc123"
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
                "description": "Large Pepperoni Pizza",
                "amount": 4999,
                "currency": "USD",
                "breakdown": {
                  "subtotal": 2899,
                  "cashbackAmount": 0,
                  "tip": {
                    "type": "percentage"
                  },
                  "taxes": [
                    {
                      "name": "Sales Tax",
                      "rate": 7,
                      "amount": 190
                    }
                  ],
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
                },
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
                "approvalCode": "OK3",
                "authorizedAmount": 4999,
                "currency": "USD",
                "responseCode": "A",
                "responseMessage": "OK3",
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
                    .WithPath("/payments")
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

        var response = await Client.Payments.CreateAsync(
            new PaymentRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Channel = PaymentRequestChannel.Web,
                ProcessingTerminalId = "1234001",
                Operator = "Jane",
                Order = new PaymentOrder
                {
                    OrderId = "OrderRef6543",
                    Description = "Large Pepperoni Pizza",
                    Amount = 4999,
                    Currency = Currency.Usd,
                },
                Customer = new Customer
                {
                    FirstName = "Sarah",
                    LastName = "Hopper",
                    BillingAddress = new Address
                    {
                        Address1 = "1 Example Ave.",
                        Address2 = "Example Address Line 2",
                        Address3 = "Example Address Line 3",
                        City = "Chicago",
                        State = "Illinois",
                        Country = "US",
                        PostalCode = "60056",
                    },
                    ShippingAddress = new Shipping
                    {
                        RecipientName = "Sarah Hopper",
                        Address = new Address
                        {
                            Address1 = "1 Example Ave.",
                            Address2 = "Example Address Line 2",
                            Address3 = "Example Address Line 3",
                            City = "Chicago",
                            State = "Illinois",
                            Country = "US",
                            PostalCode = "60056",
                        },
                    },
                },
                PaymentMethod = new PaymentRequestPaymentMethod(
                    new PaymentRequestPaymentMethod.Card(
                        new CardPayload
                        {
                            CardDetails = new CardPayloadCardDetails(
                                new CardPayloadCardDetails.Raw(
                                    new RawCardDetails
                                    {
                                        Device = new Device
                                        {
                                            Model = DeviceModel.BbposChp,
                                            SerialNumber = "1850010868",
                                        },
                                        RawData =
                                            "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                                    }
                                )
                            ),
                        }
                    )
                ),
                CustomFields = new List<CustomField>()
                {
                    new CustomField { Name = "yourCustomField", Value = "abc123" },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Payment>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_2()
    {
        const string requestJson = """
            {
              "channel": "web",
              "processingTerminalId": "1234001",
              "operator": "Jane",
              "order": {
                "orderId": "1234567890W",
                "description": "Card Transaction (APPLE)",
                "amount": 4999,
                "currency": "USD"
              },
              "paymentMethod": {
                "serviceProvider": "apple",
                "encryptedData": "encryptedData",
                "type": "digitalWallet"
              }
            }
            """;

        const string mockResponse = """
            {
              "paymentId": "J9VULKIKFP",
              "processingTerminalId": "1234001",
              "operator": "Jane",
              "order": {
                "orderId": "1234567890W",
                "dateTime": "2024-07-02T15:30:00.000Z",
                "description": "Card Transaction (APPLE)",
                "amount": 4999,
                "currency": "USD",
                "breakdown": {
                  "subtotal": 2899,
                  "cashbackAmount": 0,
                  "tip": {
                    "type": "percentage"
                  },
                  "taxes": [
                    {
                      "name": "Sales Tax",
                      "rate": 7,
                      "amount": 190
                    }
                  ],
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
                },
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
                "cardNumber": "500165******0000",
                "expiryDate": "0328",
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
                  "avsResult": "U"
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
                "approvalCode": "OK3",
                "authorizedAmount": 4999,
                "currency": "USD",
                "responseCode": "A",
                "responseMessage": "APPROVAL",
                "processorResponseCode": "00",
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
                    .WithPath("/payments")
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

        var response = await Client.Payments.CreateAsync(
            new PaymentRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Channel = PaymentRequestChannel.Web,
                ProcessingTerminalId = "1234001",
                Operator = "Jane",
                Order = new PaymentOrder
                {
                    OrderId = "1234567890W",
                    Description = "Card Transaction (APPLE)",
                    Amount = 4999,
                    Currency = Currency.Usd,
                },
                PaymentMethod = new PaymentRequestPaymentMethod(
                    new PaymentRequestPaymentMethod.DigitalWallet(
                        new DigitalWalletPayload
                        {
                            ServiceProvider = DigitalWalletPayloadServiceProvider.Apple,
                            EncryptedData = "encryptedData",
                        }
                    )
                ),
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Payment>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_3()
    {
        const string requestJson = """
            {
              "channel": "web",
              "processingTerminalId": "1234001",
              "operator": "Jane",
              "order": {
                "orderId": "OrderRef6543",
                "description": "Large Pepperoni Pizza",
                "amount": 4999,
                "currency": "USD"
              },
              "customer": {
                "firstName": "Sarah",
                "lastName": "Hopper",
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
                }
              },
              "paymentMethod": {
                "cardDetails": {
                  "device": {
                    "model": "bbposChp",
                    "serialNumber": "1850010868"
                  },
                  "rawData": "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                  "entryMethod": "raw"
                },
                "type": "card"
              },
              "customFields": [
                {
                  "name": "yourCustomField",
                  "value": "abc123"
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
                "description": "Large Pepperoni Pizza",
                "amount": 4999,
                "currency": "USD",
                "breakdown": {
                  "subtotal": 2899,
                  "cashbackAmount": 0,
                  "tip": {
                    "type": "percentage"
                  },
                  "taxes": [
                    {
                      "name": "Sales Tax",
                      "rate": 7,
                      "amount": 190
                    }
                  ],
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
                },
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
                "approvalCode": "OK3",
                "authorizedAmount": 4999,
                "currency": "USD",
                "responseCode": "A",
                "responseMessage": "OK3",
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
                    .WithPath("/payments")
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

        var response = await Client.Payments.CreateAsync(
            new PaymentRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Channel = PaymentRequestChannel.Web,
                ProcessingTerminalId = "1234001",
                Operator = "Jane",
                Order = new PaymentOrder
                {
                    OrderId = "OrderRef6543",
                    Description = "Large Pepperoni Pizza",
                    Amount = 4999,
                    Currency = Currency.Usd,
                },
                Customer = new Customer
                {
                    FirstName = "Sarah",
                    LastName = "Hopper",
                    BillingAddress = new Address
                    {
                        Address1 = "1 Example Ave.",
                        Address2 = "Example Address Line 2",
                        Address3 = "Example Address Line 3",
                        City = "Chicago",
                        State = "Illinois",
                        Country = "US",
                        PostalCode = "60056",
                    },
                    ShippingAddress = new Shipping
                    {
                        RecipientName = "Sarah Hopper",
                        Address = new Address
                        {
                            Address1 = "1 Example Ave.",
                            Address2 = "Example Address Line 2",
                            Address3 = "Example Address Line 3",
                            City = "Chicago",
                            State = "Illinois",
                            Country = "US",
                            PostalCode = "60056",
                        },
                    },
                },
                PaymentMethod = new PaymentRequestPaymentMethod(
                    new PaymentRequestPaymentMethod.Card(
                        new CardPayload
                        {
                            CardDetails = new CardPayloadCardDetails(
                                new CardPayloadCardDetails.Raw(
                                    new RawCardDetails
                                    {
                                        Device = new Device
                                        {
                                            Model = DeviceModel.BbposChp,
                                            SerialNumber = "1850010868",
                                        },
                                        RawData =
                                            "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                                    }
                                )
                            ),
                        }
                    )
                ),
                CustomFields = new List<CustomField>()
                {
                    new CustomField { Name = "yourCustomField", Value = "abc123" },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Payment>(mockResponse)).UsingDefaults()
        );
    }
}

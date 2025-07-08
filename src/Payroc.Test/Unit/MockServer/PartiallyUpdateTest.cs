using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Payments.SecureTokens;

namespace Payroc.Test.Unit.MockServer;

[TestFixture]
public class PartiallyUpdateTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
    {
        const string requestJson = """
            [
              {
                "path": "path",
                "op": "remove"
              },
              {
                "path": "path",
                "op": "remove"
              },
              {
                "path": "path",
                "op": "remove"
              }
            ]
            """;

        const string mockResponse = """
            {
              "secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
              "processingTerminalId": "1234001",
              "mitAgreement": "unscheduled",
              "customer": {
                "firstName": "Sarah",
                "lastName": "Hopper",
                "dateOfBirth": "1990-07-15",
                "referenceNumber": "Customer-12",
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
              "source": {
                "cardholderName": "Sarah Hazel Hopper",
                "cardNumber": "4539858876047062",
                "expiryDate": "1225",
                "cardType": "cardType",
                "currency": "AED",
                "debit": true,
                "surcharging": {
                  "allowed": true,
                  "amount": 87,
                  "percentage": 3,
                  "disclosure": "A 3% surcharge is applied to cover processing fees."
                },
                "type": "card"
              },
              "token": "296753123456",
              "status": "notValidated",
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
                    .WithPath(
                        "/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
                    )
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Payments.SecureTokens.PartiallyUpdateAsync(
            new PartiallyUpdateSecureTokensRequest
            {
                ProcessingTerminalId = "1234001",
                SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<SecureToken>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_2()
    {
        const string requestJson = """
            [
              {
                "path": "path",
                "op": "remove"
              },
              {
                "path": "path",
                "op": "remove"
              },
              {
                "path": "path",
                "op": "remove"
              },
              {
                "from": "from",
                "path": "path",
                "op": "move"
              },
              {
                "from": "from",
                "path": "path",
                "op": "copy"
              },
              {
                "path": "path",
                "op": "remove"
              }
            ]
            """;

        const string mockResponse = """
            {
              "secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
              "processingTerminalId": "1234001",
              "mitAgreement": "unscheduled",
              "customer": {
                "firstName": "Sarah",
                "lastName": "Hopper",
                "dateOfBirth": "1990-07-15",
                "referenceNumber": "Customer-12",
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
              "source": {
                "cardholderName": "Sarah Hazel Hopper",
                "cardNumber": "4539858876047062",
                "expiryDate": "1225",
                "cardType": "cardType",
                "currency": "AED",
                "debit": true,
                "surcharging": {
                  "allowed": true,
                  "amount": 87,
                  "percentage": 3,
                  "disclosure": "A 3% surcharge is applied to cover processing fees."
                },
                "type": "card"
              },
              "token": "296753123456",
              "status": "notValidated",
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
                    .WithPath(
                        "/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
                    )
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Payments.SecureTokens.PartiallyUpdateAsync(
            new PartiallyUpdateSecureTokensRequest
            {
                ProcessingTerminalId = "1234001",
                SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(
                        new PatchDocument.Move(new PatchMove { From = "from", Path = "path" })
                    ),
                    new PatchDocument(
                        new PatchDocument.Copy(new PatchCopy { From = "from", Path = "path" })
                    ),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<SecureToken>(mockResponse)).UsingDefaults()
        );
    }
}

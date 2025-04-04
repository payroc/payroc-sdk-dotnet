using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Payments.SecureTokens;

namespace Payroc.Test.Unit.MockServer;

[TestFixture]
public class AccountUpdateTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "token": "abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890",
              "type": "singleUseToken"
            }
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
                        "/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa/update-account"
                    )
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

        var response = await Client.Payments.SecureTokens.AccountUpdateAsync(
            new AccountUpdateSecureTokensRequest
            {
                SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
                ProcessingTerminalId = "1234001",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new AccountUpdate(
                    new AccountUpdate.SingleUseToken(
                        new SingleUseTokenAccountUpdate
                        {
                            Token =
                                "abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890",
                        }
                    )
                ),
            },
            RequestOptions
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<SecureToken>(mockResponse)).UsingPropertiesComparer()
        );
    }
}

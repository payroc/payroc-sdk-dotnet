using NUnit.Framework;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;
using Payroc.Tokenization.SecureTokens;

namespace Payroc.Test.Unit.MockServer.Tokenization.SecureTokens;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
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
                    "type": "email",
                    "value": "jane.doe@example.com"
                  }
                ],
                "notificationLanguage": "en"
              },
              "source": {
                "type": "card",
                "cardholderName": "Sarah Hazel Hopper",
                "cardNumber": "4539858876047062",
                "expiryDate": "1230",
                "cardType": "cardType",
                "currency": "AED",
                "debit": true,
                "surcharging": {
                  "allowed": true,
                  "amount": 87,
                  "percentage": 3,
                  "disclosure": "A 3% surcharge is applied to cover processing fees."
                }
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
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Tokenization.SecureTokens.RetrieveAsync(
            new RetrieveSecureTokensRequest
            {
                ProcessingTerminalId = "1234001",
                SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}

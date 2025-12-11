using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.PaymentFeatures.Bank;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.PaymentFeatures.Bank;

[TestFixture]
public class VerifyTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            {
              "processingTerminalId": "1234001",
              "bankAccount": {
                "nameOnAccount": "Sarah Hazel Hopper",
                "accountNumber": "1234567890",
                "transitNumber": "76543",
                "institutionNumber": "543",
                "type": "pad"
              }
            }
            """;

        const string mockResponse = """
            {
              "processingTerminalId": "1234001",
              "verified": true
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/bank-accounts/verify")
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

        var response = await Client.PaymentFeatures.Bank.VerifyAsync(
            new BankAccountVerificationRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                ProcessingTerminalId = "1234001",
                BankAccount = new BankAccountVerificationRequestBankAccount(
                    new BankAccountVerificationRequestBankAccount.Pad(
                        new PadPayload
                        {
                            NameOnAccount = "Sarah Hazel Hopper",
                            AccountNumber = "1234567890",
                            TransitNumber = "76543",
                            InstitutionNumber = "543",
                        }
                    )
                ),
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<BankAccountVerificationResult>(mockResponse))
                .UsingDefaults()
        );
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_2()
    {
        const string requestJson = """
            {
              "processingTerminalId": "1234001",
              "bankAccount": {
                "nameOnAccount": "Shara Hazel Hopper",
                "accountNumber": "1234567890",
                "routingNumber": "123456789",
                "type": "ach"
              }
            }
            """;

        const string mockResponse = """
            {
              "processingTerminalId": "1234001",
              "verified": true
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/bank-accounts/verify")
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

        var response = await Client.PaymentFeatures.Bank.VerifyAsync(
            new BankAccountVerificationRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                ProcessingTerminalId = "1234001",
                BankAccount = new BankAccountVerificationRequestBankAccount(
                    new Payroc.PaymentFeatures.Bank.BankAccountVerificationRequestBankAccount.Ach(
                        new AchPayload
                        {
                            NameOnAccount = "Shara Hazel Hopper",
                            AccountNumber = "1234567890",
                            RoutingNumber = "123456789",
                        }
                    )
                ),
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<BankAccountVerificationResult>(mockResponse))
                .UsingDefaults()
        );
    }
}

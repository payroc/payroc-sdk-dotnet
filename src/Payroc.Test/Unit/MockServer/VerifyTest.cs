using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Payments.BankAccounts;

namespace Payroc.Test.Unit.MockServer;

[TestFixture]
public class VerifyTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
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

        var response = await Client.Payments.BankAccounts.VerifyAsync(
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

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_2()
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

        var response = await Client.Payments.BankAccounts.VerifyAsync(
            new BankAccountVerificationRequest
            {
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                ProcessingTerminalId = "1234001",
                BankAccount = new BankAccountVerificationRequestBankAccount(
                    new BankAccountVerificationRequestBankAccount.Ach(
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

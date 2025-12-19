using NUnit.Framework;
using Payroc.CardPayments.Payments;
using Payroc.Boarding.Owners;

namespace Payroc.Test.Unit;

/// <summary>
/// This test ensures that README code examples compile correctly.
/// It prevents drift between documentation and actual SDK structure.
/// </summary>
[TestFixture]
public class ReadmeCompilationTest
{
    [Test]
    public void ReadmeExample_ClientInstantiation_Compiles()
    {
        // This verifies the basic client instantiation example from README
        var apiKey = "test-api-key";
        var client = new PayrocClient(apiKey);
        
        Assert.That(client, Is.Not.Null);
        Assert.That(client.CardPayments, Is.Not.Null);
        Assert.That(client.CardPayments.Payments, Is.Not.Null);
    }

    [Test]
    public void ReadmeExample_CustomEnvironment_Compiles()
    {
        // This verifies the custom environment example from README
        var apiKey = "test-api-key";
        var mockEnvironment = new PayrocEnvironment
        {
            Api = "http://localhost:3000",
            Identity = "http://localhost:3001"
        };

        var client = new PayrocClient(
            apiKey,
            new ClientOptions
            {
                Environment = mockEnvironment
            }
        );
        
        Assert.That(client, Is.Not.Null);
    }

    [Test]
    public void ReadmeExample_PaymentRequestStructure_Compiles()
    {
        // This verifies the payment creation example structure from README
        var paymentRequest = new PaymentRequest
        {
            IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
            Channel = PaymentRequestChannel.Web,
            ProcessingTerminalId = "1234001",
            Operator = "Postman",
            Order = new PaymentOrderRequest
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
                                        SerialNumber = "PAX123456789",
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
        };
        
        Assert.That(paymentRequest, Is.Not.Null);
        Assert.That(paymentRequest.IdempotencyKey, Is.EqualTo("8e03978e-40d5-43e8-bc93-6894a57f9324"));
    }

    [Test]
    public void ReadmeExample_ExceptionHandling_Compiles()
    {
        // This verifies the exception handling example from README
        var apiKey = "test-api-key";
        var client = new PayrocClient(apiKey);
        
        // Verify exception types are accessible
        Assert.That(typeof(PayrocApiException), Is.Not.Null);
        Assert.That(typeof(BadRequestError), Is.Not.Null);
        Assert.That(typeof(ConflictError), Is.Not.Null);
        Assert.That(typeof(ContentTooLargeError), Is.Not.Null);
        Assert.That(typeof(ForbiddenError), Is.Not.Null);
        Assert.That(typeof(InternalServerError), Is.Not.Null);
        Assert.That(typeof(NotAcceptableError), Is.Not.Null);
        Assert.That(typeof(NotFoundError), Is.Not.Null);
        Assert.That(typeof(UnauthorizedError), Is.Not.Null);
        Assert.That(typeof(UnsupportedMediaTypeError), Is.Not.Null);
    }

    [Test]
    public void ReadmeExample_ListPaymentsRequest_Compiles()
    {
        // This verifies the pagination and request parameter examples from README
        var listRequest1 = new ListPaymentsRequest
        {
            ProcessingTerminalId = "1234001",
            DateFrom = new DateTime(2024, 07, 01, 15, 30, 00, 000)
        };
        
        var listRequest2 = new ListPaymentsRequest
        {
            ProcessingTerminalId = "1234001",
            DateTo = new DateTime(2024, 07, 03, 15, 30, 00, 000)
        };
        
        var listRequest3 = new ListPaymentsRequest
        {
            ProcessingTerminalId = "1234001",
            After = "8516"
        };
        
        var listRequest4 = new ListPaymentsRequest
        {
            ProcessingTerminalId = "1234001",
            Before = "2571"
        };
        
        Assert.That(listRequest1, Is.Not.Null);
        Assert.That(listRequest2, Is.Not.Null);
        Assert.That(listRequest3, Is.Not.Null);
        Assert.That(listRequest4, Is.Not.Null);
    }

    [Test]
    public void ReadmeExample_PolymorphicTypes_Compiles()
    {
        // This verifies the polymorphic types example from README
        var a = new ContactMethod.Email(new() { Value = "jane.doe@example.com" });
        var b = new ContactMethod.Fax(new() { Value = "2025550110" });
        
        // Convert to ContactMethod instances
        ContactMethod contactMethodA = a;
        ContactMethod contactMethodB = b;
        
        Assert.That(contactMethodA.IsEmail, Is.True);
        Assert.That(contactMethodB.IsFax, Is.True);
    }

    [Test]
    public void ReadmeExample_PolymorphicHandling_Compiles()
    {
        // This verifies the polymorphic handling example from README
        ContactMethod contactMethod = new ContactMethod.Email(new() { Value = "test@example.com" });
        
        // Verify IsEmail property exists
        var isEmail = contactMethod.IsEmail;
        Assert.That(isEmail, Is.True);
        
        // Verify AsEmail method exists
        var email = contactMethod.AsEmail();
        Assert.That(email.Value, Is.EqualTo("test@example.com"));
    }

    [Test]
    public void ReadmeExample_RetrieveOwnersRequest_Compiles()
    {
        // This verifies the Boarding.Owners example from README
        var retrieveRequest = new RetrieveOwnersRequest { OwnerId = 4564 };
        
        Assert.That(retrieveRequest, Is.Not.Null);
        Assert.That(retrieveRequest.OwnerId, Is.EqualTo(4564));
    }

    [Test]
    public void ReadmeExample_TimeoutConfiguration_Compiles()
    {
        // This verifies the timeout configuration examples from README
        var apiKey = "test-api-key";
        
        // Client level timeout
        var client = new PayrocClient(
            apiKey,
            new ClientOptions
            {
                Timeout = TimeSpan.FromSeconds(10)
            }
        );
        
        // Request level timeout options
        var requestOptions = new RequestOptions
        {
            Timeout = TimeSpan.FromSeconds(3)
        };
        
        Assert.That(client, Is.Not.Null);
        Assert.That(requestOptions, Is.Not.Null);
    }

    [Test]
    public void ReadmeExample_TelemetryConfiguration_Compiles()
    {
        // This verifies the telemetry configuration example from README
        var apiKey = "test-api-key";
        var client = new PayrocClient(
            apiKey,
            new ClientOptions
            {
                Telemetry = false
            }
        );
        
        Assert.That(client, Is.Not.Null);
    }

    [Test]
    public void ReadmeExample_AddressStructure_Compiles()
    {
        // This verifies the Address structure example from README
        Address address = new()
        {
            Address1 = "1 Example Ave.",
            City = "Chicago",
            State = "Illinois",
            Country = "US",
            PostalCode = "60056"
        };
        
        Assert.That(address, Is.Not.Null);
    }

    [Test]
    public void ReadmeExample_ClientNamespaceAccess_Compiles()
    {
        // This verifies the key namespace paths mentioned in README
        var apiKey = "test-api-key";
        var client = new PayrocClient(apiKey);
        
        // Verify CardPayments.Payments path exists (the main example in README)
        Assert.That(client.CardPayments, Is.Not.Null);
        Assert.That(client.CardPayments.Payments, Is.Not.Null);
        
        // Verify Boarding.Owners path exists (mentioned in polymorphic example)
        Assert.That(client.Boarding, Is.Not.Null);
        Assert.That(client.Boarding.Owners, Is.Not.Null);
    }
}

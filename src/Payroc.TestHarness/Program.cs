using Payroc;
using Payroc.TestHarness.Debugging;
using Payroc.TestHarness.Factory;

Console.WriteLine("Starting Payroc SDK test harness...");

var apiKey = Environment.GetEnvironmentVariable("PAYROC_API_KEY")
    ?? throw new Exception("Payroc API Key not found");

var client = new PayrocClient(
    apiKey,
    new ClientOptions
    {
        Environment = PayrocEnvironment.Test
    }
);

// Debug Json
//JsonTester.TestJson<PricingIntent50>(DebugPayload.DataUnderTest);

// Pricing intent
//var createPricingIntentRequest = PricingIntentFactory.Create();
//var pricingIntent = await client.Boarding.PricingIntents.CreateAsync(createPricingIntentRequest);

// Merchant account
//var pricingIntentId = 1602;
//var createMerchantAccountRequest = MerchantAccountFactory.Create(pricingIntentId);
//var merchantPlatform = await client.Boarding.MerchantPlatforms.CreateAsync(createMerchantAccountRequest);

// Payment
var processingTerminalId = "1234001";
var paymentRequest = PaymentRequestFactory.Create(processingTerminalId);
var payment = await client.Payments.CreateAsync(paymentRequest);

Console.WriteLine("Testing complete...");
Console.ReadLine();
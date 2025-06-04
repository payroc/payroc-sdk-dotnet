using Payroc;
using Payroc.Payments;
using Payroc.TestHarness.Debugging;
using Payroc.TestHarness.Factory;

Console.WriteLine("Starting Payroc SDK test harness...");

var apiKey = Environment.GetEnvironmentVariable("PAYROC_API_KEY")
    ?? throw new Exception("Payroc API Key not found");

var client = new PayrocClient(
    apiKey,
    new ClientOptions
    {
        Environment = PayrocEnvironment.Uat
    }
);

var processingTerminalId = "5984001";

// Debug Json
// JsonTester.TestJson<PricingIntent50>(DebugPayload.DataUnderTest);

// Pricing intent
// var createPricingIntentRequest = PricingIntentFactory.Create();
// var pricingIntent = await client.Boarding.PricingIntents.CreateAsync(createPricingIntentRequest);

// Merchant account
// var pricingIntentId = 1602;
// var createMerchantAccountRequest = MerchantAccountFactory.Create(pricingIntentId);
// var merchantPlatform = await client.Boarding.MerchantPlatforms.CreateAsync(createMerchantAccountRequest);

// Funding Recipient
// "Entity has been rejected due to failing KYC checks"
// var fundingRecipientRequest = CreateFundingRecipientFactory.Create();
// var fundingRecipient = await client.Funding.FundingRecipients.CreateAsync(fundingRecipientRequest);

// Payment
// var paymentRequest = PaymentRequestFactory.Create(processingTerminalId);
// var payment = await client.Payments.CreateAsync(paymentRequest);
var paymentId = "GFL9F9AXXZ";

// Payment Capture
var paymentCaptureRequest = PaymentCaptureFactory.Create(processingTerminalId, paymentId);
var paymentCapture = await client.Payments.CaptureAsync(paymentCaptureRequest);

// Payment Reversal
var reversalRequest = PaymentReversalFactory.Create(processingTerminalId, paymentId);
var reversal = await client.Payments.ReverseAsync(reversalRequest);

Console.WriteLine("Testing complete...");
Console.ReadLine();

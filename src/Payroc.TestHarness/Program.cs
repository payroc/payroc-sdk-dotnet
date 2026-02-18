#pragma warning disable CS0219 // Variable is assigned but its value is never used
using Payroc;
using Payroc.TestHarness.Factory;
using Payroc.Tokenization.SecureTokens;

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
//var createPricingIntentRequest = PricingIntentFactory.Create();
//var pricingIntent = await client.Boarding.PricingIntents.CreateAsync(createPricingIntentRequest);

// Merchant account
var pricingIntentId = "1602";
                              //var createMerchantAccountRequest = MerchantAccountFactory.Create(pricingIntentId);
                              //var merchantPlatform = await client.Boarding.MerchantPlatforms.CreateAsync(createMerchantAccountRequest);

// Funding Recipient
// "Entity has been rejected due to failing KYC checks"
//var fundingRecipientRequest = CreateFundingRecipientFactory.Create();
//var fundingRecipient = await client.Funding.FundingRecipients.CreateAsync(fundingRecipientRequest);

// Payment
//var paymentRequest = PaymentRequestFactory.Create(processingTerminalId);
//var payment = await client.Payments.CreateAsync(paymentRequest);
var paymentId = "GCUFXZ8TS4"; // "C1RTVWFWPB"; // "GFL9F9AXXZ" ;

// Retrieve Payment(s)
//var retrievedPayment = await client.Payments.RetrieveAsync(new() { PaymentId = paymentId });
//var payments = await client.Payments.ListAsync(new() { Limit = 5 });

// Payment Capture
//var paymentCaptureRequest = PaymentCaptureFactory.Create(processingTerminalId, paymentId);
//var paymentCapture = await client.Payments.CaptureAsync(paymentCaptureRequest);

// Payment Reversal
//var reversalRequest = PaymentReversalFactory.Create(processingTerminalId, paymentId);
//var reversal = await client.Payments.ReverseAsync(reversalRequest);

// Card Verification
//var cardVerificationRequest = CardVerificationRequestFactory.Create(processingTerminalId);
//var cardVerification = await client.Payments.Cards.VerifyAsync(cardVerificationRequest);

var secureTokenRequest = TokenizationRequestFactory.Create(processingTerminalId);
var secureToken = await client.Tokenization.SecureTokens.CreateAsync(secureTokenRequest);

var retrievedToken = await client.Tokenization.SecureTokens.RetrieveAsync(new RetrieveSecureTokensRequest() { SecureTokenId = secureToken.SecureTokenId, ProcessingTerminalId = processingTerminalId });
var accountType = retrievedToken?.Source?.AsAch()?.AccountType;
var tokens = await client.Tokenization.SecureTokens.ListAsync(new() { ProcessingTerminalId = processingTerminalId, Limit = 5 });

Console.WriteLine("Testing complete...");
Console.ReadLine();

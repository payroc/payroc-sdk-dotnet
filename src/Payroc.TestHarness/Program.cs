using Payroc;
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

var createMerchantAccountRequest = MerchantAccountFactory.Create();


Console.WriteLine("Creating Merchant Account...");
var merchantPlatform = await client.Boarding.MerchantPlatforms.CreateAsync(createMerchantAccountRequest);

Console.WriteLine("Created Merchant Account...");
Console.ReadLine();
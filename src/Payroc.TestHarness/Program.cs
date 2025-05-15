using Payroc;

Console.WriteLine("Starting Payroc SDK test harness...");

var apiKey = Environment.GetEnvironmentVariable("PAYROC_API_KEY");

if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("Please set the PAYROC_API_KEY environment variable.");
    return;
}

var client = new PayrocClient(
    apiKey,
    new ClientOptions
    {
        Environment = PayrocEnvironment.UAT
    }
);

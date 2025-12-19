namespace Payroc.TestFunctional.HelperMethods.Boarding;

public static class TaxCodeGenerator
{
    public static string Generate()
    {
        var prefix = Random.Shared.Next(10, 100); 
        var suffix = Random.Shared.Next(1000000, 10000000);
        var timestamp = DateTimeOffset.UtcNow.Ticks % 1000000;
        return $"{prefix}-{suffix}-{timestamp}";
    }
}

namespace Payroc.TestHarness.Factory;

public class AddressFactory
{
    public static Address CreateAddress()
        => new()
        {
            Address1 = "1234 Example St",
            City = "Chicago",
            State = "IL",
            PostalCode = "60056",
            Country = "US"            
        };

    // TODO: Re-instate once Boarding and Funding are available in SDK again
    /*
    public static LegalAddress CreateLegalAddress()
        => new()
        {
            Address1 = "1 Example Ave",
            City = "Chicago",
            State = "Illinois",
            Country = "US",
            PostalCode = "60477",
            Type = "legalAddress"
        };
    */
}

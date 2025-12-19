namespace Payroc.TestFunctional.Factories.Funding.RequestBodies;

public class FundingAccountFactory
{
    public static FundingAccount Create() => new()
    {
        Type = FundingAccountType.Checking,
        Use = FundingAccountUse.Credit,
        NameOnAccount = "John Doe",
        PaymentMethods =
        [
            new(new PaymentMethodsItem.Ach(new()
                { Value = new() { RoutingNumber = "063100277", AccountNumber = "321831591" } }))
        ]
    };
}
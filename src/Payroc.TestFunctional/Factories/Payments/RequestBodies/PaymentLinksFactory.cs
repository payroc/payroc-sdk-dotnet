using Payroc.PaymentLinks;

namespace Payroc.TestFunctional.Factories.Payments;

public static class PaymentLinksFactory
{
    public static CreatePaymentLinksRequestBody Create()
    {
        return new CreatePaymentLinksRequestBody.MultiUse(
            new ()
            {
                MerchantReference = Guid.NewGuid().ToString(),
                Order = new ()
                {
                    Description = "Pie It Forward charitable trust donation",
                    Charge = new MultiUsePaymentLinkOrderCharge.Prompt(new ()
                    {
                        Currency = Currency.Usd
                    })
                },
                AuthType = MultiUsePaymentLinkAuthType.Sale,
                PaymentMethods = new List<MultiUsePaymentLinkPaymentMethodsItem>
                {
                    MultiUsePaymentLinkPaymentMethodsItem.Card
                }
            }
        );
    }
}
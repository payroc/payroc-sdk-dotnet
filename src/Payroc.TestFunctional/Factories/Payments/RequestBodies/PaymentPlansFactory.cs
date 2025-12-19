namespace Payroc.TestFunctional.Factories.Payments.PaymentPlans.RequestBodies;

public static class PaymentPlansFactory
{
    public static PaymentPlan Create()
    {
        return new PaymentPlan
        {
            Name = "Test Payment Plan Body",
            PaymentPlanId = Guid.NewGuid().ToString(),
            Currency = Currency.Usd,
            Type = PaymentPlanBaseType.Manual,
            Frequency = PaymentPlanBaseFrequency.Fortnightly,
            OnUpdate = PaymentPlanBaseOnUpdate.Continue,
            OnDelete = PaymentPlanBaseOnDelete.Complete
        };
    }
}
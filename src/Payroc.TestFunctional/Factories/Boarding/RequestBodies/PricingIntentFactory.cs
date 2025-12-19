namespace Payroc.TestFunctional.Factories.Boarding.RequestBodies;

public static class PricingIntentFactory
{
    public static PricingIntent50 Create()
    {
        return new PricingIntent50
        {
            Version = PricingAgreementUs50Version.Five0,
            Key = Guid.NewGuid().ToString(),
            Country = PricingAgreementUs50Country.Us,
            Base = new()
            {
                AddressVerification = 5,
                AnnualFee = new() { Amount = 100 },
                RegulatoryAssistanceProgram = 15,
                MerchantAdvantage = 10,
                Maintenance = 1995,
                Minimum = 100,
                Batch = 5,

                // Required in practice...
                PciNonCompliance = 3995, // 3995 - 5995
                EarlyTermination = 19500, // 19500 - 59500
                VoiceAuthorization = 45, // 5
                Chargeback = 500, // 500 - 5000
                Retrieval = 500 // 500 - 5000
            },
            Processor = new()
            {
                Card = new(new PricingAgreementUs50ProcessorCard.Tiered4(new()
                {
                    Fees = new()
                    {
                        MastercardVisaDiscover = new()
                        {
                            QualifiedRate = new() { Volume = 4, Transaction = 40 },
                            MidQualRate = new() { Volume = 4, Transaction = 40 },
                            NonQualRate = new() { Volume = 4, Transaction = 40 },
                            PremiumRate = new() { Volume = 4, Transaction = 40 }
                        },
                        Amex = new(new Tiered4FeesAmex.OptBlue(new()
                        {
                            QualifiedRate = new() { Volume = 4, Transaction = 40 },
                            MidQualRate = new() { Volume = 4, Transaction = 40 },
                            NonQualRate = new() { Volume = 4, Transaction = 40 }
                        })),
                        PinDebit = new()
                        {
                            AdditionalDiscount = .4,
                            Transaction = 50,
                            MonthlyAccess = 5
                        },
                        ElectronicBenefitsTransfer = new()
                        {
                            Transaction = 70
                        },
                        SpecialityCards = new()
                        {
                            Transaction = 5
                        }
                    }
                })),
                Ach = new()
                {
                    Fees = new()
                    {
                        Transaction = 100,
                        Batch = 6000,
                        Returns = 400,
                        UnauthorizedReturn = 1999,
                        Statement = 800,
                        MonthlyMinimum = 1000,
                        AccountVerification = 2000,
                        DiscountRateUnder10000 = 5,
                        DiscountRateAbove10000 = 1.25
                    }
                }
            }
        };
    }
}

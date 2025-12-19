namespace Payroc.TestFunctional.Factories.Funding.RequestBodies;

public class FundingInstructionsFactory
{
    public static Instruction Create(string merchantId, int? fundingAccountId ) => new()
    {
        Merchants = new []{
            new InstructionMerchantsItem
            {
                MerchantId = merchantId,
                Recipients = new []{
                    new InstructionMerchantsItemRecipientsItem
                    {
                        PaymentMethod = InstructionMerchantsItemRecipientsItemPaymentMethod.Ach,
                        FundingAccountId = fundingAccountId ?? throw new Exception("Funding Account ID is null"),
                        Amount = new InstructionMerchantsItemRecipientsItemAmount
                        {
                            Currency =  InstructionMerchantsItemRecipientsItemAmountCurrency.Usd,
                            Value = 1000
                        }
                    }
                }
            }
        }
    };
    public static Instruction Update(string merchantId, int? fundingAccountId) => new()
    {
        Merchants = new []{
            new InstructionMerchantsItem
            {
                MerchantId = merchantId,
                Recipients = new []{
                    new InstructionMerchantsItemRecipientsItem
                    {
                        PaymentMethod = InstructionMerchantsItemRecipientsItemPaymentMethod.Ach,
                        FundingAccountId = fundingAccountId ?? throw new Exception("Funding Account ID is null"),
                        Amount = new InstructionMerchantsItemRecipientsItemAmount
                        {
                            Currency =  InstructionMerchantsItemRecipientsItemAmountCurrency.Usd,
                            Value = 2000
                        }
                    }
                }
            }
        }
    };
}
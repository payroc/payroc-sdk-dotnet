namespace Payroc.TestHarness.Factory;

public class CreateProcessingAccountFactory
{
    public static CreateProcessingAccount Create(int pricingIntentId = 1602)
        => new()
        {
            Owners =
            [
                new()
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    DateOfBirth = "1964-03-22",
                    Address = AddressFactory.CreateAddress(),
                    Identifiers =
                    [
                        new()
                        {
                            Type = "nationalId",
                            Value = "987-65-4320"
                        }
                    ],
                    Relationship = new()
                    {
                        IsControlProng = true,
                        EquityPercentage = 35.4f,
                        Title = "Owner",
                        IsAuthorizedSignatory = false
                    },
                    ContactMethods =
                    [
                        new ContactMethod.Email(new() { Value = "jane.doe@example.com" })
                    ],
                }
            ],
            ContactMethods =
            [
                new ContactMethod.Email(new() { Value = "jane.doe@example.com" })
            ],
            Website = "https://example.com",
            DoingBusinessAs = "Pizza Doe",
            BusinessType = CreateProcessingAccountBusinessType.Restaurant,
            CategoryCode = 5999,
            MerchandiseOrServiceSold = "Pizza",
            BusinessStartDate = "2020-01-01",
            Timezone = CreateProcessingAccountTimezone.AmericaChicago,
            Address = AddressFactory.CreateAddress(),
            Processing = new()
            {
                TransactionAmounts = new() { Average = 5000, Highest = 10000 },
                MonthlyAmounts = new() { Average = 50000, Highest = 100000 },
                VolumeBreakdown = new() { CardPresentKeyed = 47, CardPresentSwiped = 30, MailOrTelephone = 3, Ecommerce = 20 },
                IsSeasonal = false,
                CardAcceptance = new()
                {
                    DebitOnly = false,
                    CardsAccepted =
                    [
                        ProcessingCardAcceptanceCardsAcceptedItem.Visa,
                        ProcessingCardAcceptanceCardsAcceptedItem.Mastercard,
                        ProcessingCardAcceptanceCardsAcceptedItem.Discover,
                        ProcessingCardAcceptanceCardsAcceptedItem.AmexOptBlue
                    ],
                    SpecialityCards = new()
                    {
                        AmericanExpressDirect = new() { Enabled = false },
                        ElectronicBenefitsTransfer = new() { Enabled = true, FnsNumber = "1231234" },
                        Other = new()
                        {
                            WexMerchantNumber = "1234",
                            VoyagerMerchantId = "1234",
                            FleetMerchantId = "1234"
                        }
                    },

                },
                Ach = new()
                {
                    EstimatedMonthlyTransactions = 100,
                    Refunds = new() { WrittenRefundPolicy = true },
                    Limits = new()
                    {
                        SingleTransaction = 50000,
                        DailyDeposit = 1000000,
                        MonthlyDeposit = 30000000
                    },
                    PreviouslyTerminatedForAch = true
                }
            },
            Funding = new CreateFunding()
            {
                FundingAccounts =
                [
                    new FundingAccount
                    {
                        Type  = FundingAccountType.Checking,
                        Use  = FundingAccountUse.Credit,
                        NameOnAccount  = "Jane Doe",
                        PaymentMethods  =
                        [
                            new (new PaymentMethodsItem.Ach(new(){ Value = new(){ RoutingNumber="063100277", AccountNumber="321831591" } })){ }
                        ]
                    },
                    new FundingAccount
                    {
                        Type  = FundingAccountType.GeneralLedger,
                        Use  = FundingAccountUse.CreditAndDebit,
                        NameOnAccount  = "John Doe",
                        PaymentMethods  =
                        [
                            new (new PaymentMethodsItem.Ach(new(){ Value = new(){ RoutingNumber="063100277", AccountNumber="321831591" } })){ }
                        ]
                    }
                ],
                Status = CommonFundingStatus.Disabled,
                FundingSchedule = CommonFundingFundingSchedule.Nextday,
                AcceleratedFundingFee = 1999,
                DailyDiscount = true,
            },
            Pricing = new(new Pricing.Intent(new() { PricingIntentId = pricingIntentId })) { },//1602//6123//3164
            Signature = new() { Type = CreateProcessingAccountSignature.RequestedViaEmail },
            Contacts =
            [
                new()
                {
                    Type = ContactType.Manager,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Identifiers =
                    [
                        new()
                        {
                            Type = "nationalId",
                            Value = "987-65-4320"
                        }
                    ],
                    ContactMethods =
                    [
                        new ContactMethod.Email(new() { Value = "jane.doe@example.com" })
                    ]
                }
            ]
        };
}

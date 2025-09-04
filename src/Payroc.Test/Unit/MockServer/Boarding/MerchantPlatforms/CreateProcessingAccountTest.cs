using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Boarding.MerchantPlatforms;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.MerchantPlatforms;

[TestFixture]
public class CreateProcessingAccountTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "doingBusinessAs": "Pizza Doe",
              "owners": [
                {
                  "firstName": "Jane",
                  "middleName": "Helen",
                  "lastName": "Doe",
                  "dateOfBirth": "1964-03-22",
                  "address": {
                    "address1": "1 Example Ave.",
                    "address2": "Example Address Line 2",
                    "address3": "Example Address Line 3",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "identifiers": [
                    {
                      "type": "nationalId",
                      "value": "000-00-4320"
                    }
                  ],
                  "contactMethods": [
                    {
                      "value": "jane.doe@example.com",
                      "type": "email"
                    }
                  ],
                  "relationship": {
                    "equityPercentage": 51.5,
                    "title": "CFO",
                    "isControlProng": true,
                    "isAuthorizedSignatory": false
                  }
                }
              ],
              "website": "www.example.com",
              "businessType": "restaurant",
              "categoryCode": 5999,
              "merchandiseOrServiceSold": "Pizza",
              "businessStartDate": "2020-01-01",
              "timezone": "America/Chicago",
              "address": {
                "address1": "1 Example Ave.",
                "address2": "Example Address Line 2",
                "address3": "Example Address Line 3",
                "city": "Chicago",
                "state": "Illinois",
                "country": "US",
                "postalCode": "60056"
              },
              "contactMethods": [
                {
                  "value": "jane.doe@example.com",
                  "type": "email"
                }
              ],
              "processing": {
                "transactionAmounts": {
                  "average": 5000,
                  "highest": 10000
                },
                "monthlyAmounts": {
                  "average": 50000,
                  "highest": 100000
                },
                "volumeBreakdown": {
                  "cardPresentKeyed": 47,
                  "cardPresentSwiped": 30,
                  "mailOrTelephone": 3,
                  "ecommerce": 20
                },
                "isSeasonal": true,
                "monthsOfOperation": [
                  "jan",
                  "feb"
                ],
                "ach": {
                  "naics": "5812",
                  "previouslyTerminatedForAch": false,
                  "refunds": {
                    "writtenRefundPolicy": true,
                    "refundPolicyUrl": "www.example.com/refund-poilcy-url"
                  },
                  "estimatedMonthlyTransactions": 3000,
                  "limits": {
                    "singleTransaction": 10000,
                    "dailyDeposit": 200000,
                    "monthlyDeposit": 6000000
                  },
                  "transactionTypes": [
                    "prearrangedPayment",
                    "other"
                  ],
                  "transactionTypesOther": "anotherTransactionType"
                },
                "cardAcceptance": {
                  "debitOnly": false,
                  "hsaFsa": false,
                  "cardsAccepted": [
                    "visa",
                    "mastercard"
                  ],
                  "specialityCards": {
                    "americanExpressDirect": {
                      "enabled": true,
                      "merchantNumber": "abc1234567"
                    },
                    "electronicBenefitsTransfer": {
                      "enabled": true,
                      "fnsNumber": "6789012"
                    },
                    "other": {
                      "wexMerchantNumber": "abc1234567",
                      "voyagerMerchantId": "abc1234567",
                      "fleetMerchantId": "abc1234567"
                    }
                  }
                }
              },
              "funding": {
                "fundingSchedule": "nextday",
                "acceleratedFundingFee": 1999,
                "dailyDiscount": false,
                "fundingAccounts": [
                  {
                    "type": "checking",
                    "use": "creditAndDebit",
                    "nameOnAccount": "Jane Doe",
                    "paymentMethods": [
                      {
                        "type": "ach"
                      }
                    ],
                    "metadata": {
                      "yourCustomField": "abc123"
                    }
                  }
                ]
              },
              "pricing": {
                "pricingIntentId": "6123",
                "type": "intent"
              },
              "signature": {
                "type": "requestedViaDirectLink"
              },
              "contacts": [
                {
                  "type": "manager",
                  "firstName": "Jane",
                  "middleName": "Helen",
                  "lastName": "Doe",
                  "identifiers": [
                    {
                      "type": "nationalId",
                      "value": "000-00-4320"
                    }
                  ],
                  "contactMethods": [
                    {
                      "value": "jane.doe@example.com",
                      "type": "email"
                    }
                  ]
                }
              ],
              "metadata": {
                "customerId": "2345"
              }
            }
            """;

        const string mockResponse = """
            {
              "processingAccountId": "38765",
              "createdDate": "2024-07-02T12:00:00.000Z",
              "lastModifiedDate": "2024-07-02T12:00:00.000Z",
              "status": "entered",
              "doingBusinessAs": "Pizza Doe",
              "owners": [
                {
                  "ownerId": 4564,
                  "firstName": "Jane",
                  "lastName": "Doe",
                  "link": {
                    "rel": "owner",
                    "href": "https://api.payroc.com/v1/owners/4564",
                    "method": "get"
                  }
                }
              ],
              "website": "www.example.com",
              "businessType": "restaurant",
              "categoryCode": 5999,
              "merchandiseOrServiceSold": "Pizza",
              "businessStartDate": "2020-01-01",
              "timezone": "America/Chicago",
              "address": {
                "address1": "1 Example Ave.",
                "address2": "Example Address Line 2",
                "address3": "Example Address Line 3",
                "city": "Chicago",
                "state": "Illinois",
                "country": "US",
                "postalCode": "60056"
              },
              "contactMethods": [
                {
                  "value": "jane.doe@example.com",
                  "type": "email"
                }
              ],
              "processing": {
                "merchantId": "4525644354",
                "transactionAmounts": {
                  "average": 5000,
                  "highest": 10000
                },
                "monthlyAmounts": {
                  "average": 50000,
                  "highest": 100000
                },
                "volumeBreakdown": {
                  "cardPresentKeyed": 47,
                  "cardPresentSwiped": 30,
                  "mailOrTelephone": 3,
                  "ecommerce": 20
                },
                "isSeasonal": true,
                "monthsOfOperation": [
                  "jan",
                  "feb"
                ],
                "ach": {
                  "naics": "5812",
                  "previouslyTerminatedForAch": false,
                  "refunds": {
                    "writtenRefundPolicy": true,
                    "refundPolicyUrl": "www.example.com/refund-poilcy-url"
                  },
                  "estimatedMonthlyTransactions": 3000,
                  "limits": {
                    "singleTransaction": 10000,
                    "dailyDeposit": 200000,
                    "monthlyDeposit": 6000000
                  },
                  "transactionTypes": [
                    "prearrangedPayment",
                    "other"
                  ],
                  "transactionTypesOther": "anotherTransactionType"
                },
                "cardAcceptance": {
                  "debitOnly": false,
                  "hsaFsa": false,
                  "cardsAccepted": [
                    "visa",
                    "mastercard"
                  ],
                  "specialityCards": {
                    "americanExpressDirect": {
                      "enabled": true,
                      "merchantNumber": "abc1234567"
                    },
                    "electronicBenefitsTransfer": {
                      "enabled": true,
                      "fnsNumber": "6789012"
                    },
                    "other": {
                      "wexMerchantNumber": "abc1234567",
                      "voyagerMerchantId": "abc1234567",
                      "fleetMerchantId": "abc1234567"
                    }
                  }
                }
              },
              "funding": {
                "status": "enabled",
                "fundingSchedule": "nextday",
                "acceleratedFundingFee": 1999,
                "dailyDiscount": false,
                "fundingAccounts": [
                  {
                    "fundingAccountId": 123,
                    "status": "pending",
                    "link": {
                      "rel": "fundingAccount",
                      "method": "get",
                      "href": "https://api.payroc.com/v1/funding-accounts/123"
                    }
                  }
                ]
              },
              "pricing": {
                "link": {
                  "rel": "pricing",
                  "href": "https://api.payroc.com/v1/processing-accounts/12345/pricing",
                  "method": "get"
                }
              },
              "contacts": [
                {
                  "contactId": 1543,
                  "firstName": "Jane",
                  "lastName": "Doe",
                  "link": {
                    "rel": "contact",
                    "href": "https://api.payroc.com/v1/contacts/1543",
                    "method": "get"
                  }
                }
              ],
              "signature": {
                "link": {
                  "rel": "previous",
                  "method": "get",
                  "href": "<uri>"
                },
                "type": "requestedViaDirectLink"
              },
              "metadata": {
                "customerId": "2345"
              },
              "links": [
                {
                  "rel": "previous",
                  "method": "get",
                  "href": "<uri>"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/merchant-platforms/12345/processing-accounts")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.MerchantPlatforms.CreateProcessingAccountAsync(
            new CreateProcessingAccountMerchantPlatformsRequest
            {
                MerchantPlatformId = "12345",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new CreateProcessingAccount
                {
                    DoingBusinessAs = "Pizza Doe",
                    Owners = new List<Owner>()
                    {
                        new Owner
                        {
                            FirstName = "Jane",
                            MiddleName = "Helen",
                            LastName = "Doe",
                            DateOfBirth = new DateOnly(1964, 3, 22),
                            Address = new Address
                            {
                                Address1 = "1 Example Ave.",
                                Address2 = "Example Address Line 2",
                                Address3 = "Example Address Line 3",
                                City = "Chicago",
                                State = "Illinois",
                                Country = "US",
                                PostalCode = "60056",
                            },
                            Identifiers = new List<Identifier>()
                            {
                                new Identifier { Type = "nationalId", Value = "000-00-4320" },
                            },
                            ContactMethods = new List<ContactMethod>()
                            {
                                new ContactMethod(
                                    new ContactMethod.Email(
                                        new ContactMethodEmail { Value = "jane.doe@example.com" }
                                    )
                                ),
                            },
                            Relationship = new OwnerRelationship
                            {
                                EquityPercentage = 51.5f,
                                Title = "CFO",
                                IsControlProng = true,
                                IsAuthorizedSignatory = false,
                            },
                        },
                    },
                    Website = "www.example.com",
                    BusinessType = CreateProcessingAccountBusinessType.Restaurant,
                    CategoryCode = 5999,
                    MerchandiseOrServiceSold = "Pizza",
                    BusinessStartDate = new DateOnly(2020, 1, 1),
                    Timezone = Timezone.AmericaChicago,
                    Address = new Address
                    {
                        Address1 = "1 Example Ave.",
                        Address2 = "Example Address Line 2",
                        Address3 = "Example Address Line 3",
                        City = "Chicago",
                        State = "Illinois",
                        Country = "US",
                        PostalCode = "60056",
                    },
                    ContactMethods = new List<ContactMethod>()
                    {
                        new ContactMethod(
                            new ContactMethod.Email(
                                new ContactMethodEmail { Value = "jane.doe@example.com" }
                            )
                        ),
                    },
                    Processing = new Processing
                    {
                        TransactionAmounts = new ProcessingTransactionAmounts
                        {
                            Average = 5000,
                            Highest = 10000,
                        },
                        MonthlyAmounts = new ProcessingMonthlyAmounts
                        {
                            Average = 50000,
                            Highest = 100000,
                        },
                        VolumeBreakdown = new ProcessingVolumeBreakdown
                        {
                            CardPresentKeyed = 47,
                            CardPresentSwiped = 30,
                            MailOrTelephone = 3,
                            Ecommerce = 20,
                        },
                        IsSeasonal = true,
                        MonthsOfOperation = new List<ProcessingMonthsOfOperationItem>()
                        {
                            ProcessingMonthsOfOperationItem.Jan,
                            ProcessingMonthsOfOperationItem.Feb,
                        },
                        Ach = new ProcessingAch
                        {
                            Naics = "5812",
                            PreviouslyTerminatedForAch = false,
                            Refunds = new ProcessingAchRefunds
                            {
                                WrittenRefundPolicy = true,
                                RefundPolicyUrl = "www.example.com/refund-poilcy-url",
                            },
                            EstimatedMonthlyTransactions = 3000,
                            Limits = new ProcessingAchLimits
                            {
                                SingleTransaction = 10000,
                                DailyDeposit = 200000,
                                MonthlyDeposit = 6000000,
                            },
                            TransactionTypes = new List<ProcessingAchTransactionTypesItem>()
                            {
                                ProcessingAchTransactionTypesItem.PrearrangedPayment,
                                ProcessingAchTransactionTypesItem.Other,
                            },
                            TransactionTypesOther = "anotherTransactionType",
                        },
                        CardAcceptance = new ProcessingCardAcceptance
                        {
                            DebitOnly = false,
                            HsaFsa = false,
                            CardsAccepted = new List<ProcessingCardAcceptanceCardsAcceptedItem>()
                            {
                                ProcessingCardAcceptanceCardsAcceptedItem.Visa,
                                ProcessingCardAcceptanceCardsAcceptedItem.Mastercard,
                            },
                            SpecialityCards = new ProcessingCardAcceptanceSpecialityCards
                            {
                                AmericanExpressDirect =
                                    new ProcessingCardAcceptanceSpecialityCardsAmericanExpressDirect
                                    {
                                        Enabled = true,
                                        MerchantNumber = "abc1234567",
                                    },
                                ElectronicBenefitsTransfer =
                                    new ProcessingCardAcceptanceSpecialityCardsElectronicBenefitsTransfer
                                    {
                                        Enabled = true,
                                        FnsNumber = "6789012",
                                    },
                                Other = new ProcessingCardAcceptanceSpecialityCardsOther
                                {
                                    WexMerchantNumber = "abc1234567",
                                    VoyagerMerchantId = "abc1234567",
                                    FleetMerchantId = "abc1234567",
                                },
                            },
                        },
                    },
                    Funding = new CreateFunding
                    {
                        FundingSchedule = CommonFundingFundingSchedule.Nextday,
                        AcceleratedFundingFee = 1999,
                        DailyDiscount = false,
                        FundingAccounts = new List<FundingAccount>()
                        {
                            new FundingAccount
                            {
                                Type = FundingAccountType.Checking,
                                Use = FundingAccountUse.CreditAndDebit,
                                NameOnAccount = "Jane Doe",
                                PaymentMethods = new List<PaymentMethodsItem>()
                                {
                                    new PaymentMethodsItem(
                                        new PaymentMethodsItem.Ach(new PaymentMethodAch())
                                    ),
                                },
                                Metadata = new Dictionary<string, string>()
                                {
                                    { "yourCustomField", "abc123" },
                                },
                            },
                        },
                    },
                    Pricing = new Pricing(
                        new Pricing.Intent(new PricingTemplate { PricingIntentId = "6123" })
                    ),
                    Signature = new Signature(
                        new Signature.RequestedViaDirectLink(new SignatureByDirectLink())
                    ),
                    Contacts = new List<Contact>()
                    {
                        new Contact
                        {
                            Type = ContactType.Manager,
                            FirstName = "Jane",
                            MiddleName = "Helen",
                            LastName = "Doe",
                            Identifiers = new List<Identifier>()
                            {
                                new Identifier { Type = "nationalId", Value = "000-00-4320" },
                            },
                            ContactMethods = new List<ContactMethod>()
                            {
                                new ContactMethod(
                                    new ContactMethod.Email(
                                        new ContactMethodEmail { Value = "jane.doe@example.com" }
                                    )
                                ),
                            },
                        },
                    },
                    Metadata = new Dictionary<string, string>() { { "customerId", "2345" } },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<ProcessingAccount>(mockResponse)).UsingDefaults()
        );
    }
}

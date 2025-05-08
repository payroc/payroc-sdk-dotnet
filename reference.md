# Reference

## Payments

<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">ListAsync</a>(ListPaymentsRequest { ... }) -> PayrocPager<Payment></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Return a list of payments.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.ListAsync(
    new ListPaymentsRequest
    {
        ProcessingTerminalId = "1234001",
        OrderId = "OrderRef6543",
        Operator = "Jane",
        CardholderName = "Sarah%20Hazel%20Hopper",
        First6 = "453985",
        Last4 = "7062",
        DateFrom = new DateTime(2024, 07, 01, 15, 30, 00, 000),
        DateTo = new DateTime(2024, 07, 03, 15, 30, 00, 000),
        SettlementDate = "2024-07-02",
        Before = "2571",
        After = "8516",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListPaymentsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">CreateAsync</a>(PaymentRequest { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Run a sale or pre-authorization. You can also:

- Save the customer's payment details.
- Set up recurring billing.
- Process the transaction offline.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.CreateAsync(
    new PaymentRequest
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Channel = PaymentRequestChannel.Web,
        ProcessingTerminalId = "1234001",
        Operator = "Postman",
        Order = new PaymentOrder
        {
            OrderId = "OrderRef6543",
            Description = "Large Pepperoni Pizza",
            Amount = 4999,
            Currency = Currency.Usd,
        },
        Customer = new Customer
        {
            FirstName = "Sarah",
            LastName = "Hopper",
            BillingAddress = new Address
            {
                Address1 = "1 Example Ave.",
                Address2 = "Example Address Line 2",
                Address3 = "Example Address Line 3",
                City = "Chicago",
                State = "Illinois",
                Country = "US",
                PostalCode = "60056",
            },
            ShippingAddress = new Shipping
            {
                RecipientName = "Sarah Hopper",
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
            },
        },
        PaymentMethod = new PaymentRequestPaymentMethod(
            new PaymentRequestPaymentMethod.Card(
                new CardPayload
                {
                    CardDetails = new CardPayloadCardDetails(
                        new CardPayloadCardDetails.Raw(
                            new RawCardDetails
                            {
                                Device = new Device
                                {
                                    Model = DeviceModel.BbposChp,
                                    SerialNumber = "PAX123456789",
                                },
                                RawData =
                                    "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                            }
                        )
                    ),
                }
            )
        ),
        CustomFields = new List<CustomField>()
        {
            new CustomField { Name = "yourCustomField", Value = "abc123" },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PaymentRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">GetAsync</a>(GetPaymentsRequest { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve an existing payment.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.GetAsync(new GetPaymentsRequest { PaymentId = "M2MJOG6O2Y" });
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetPaymentsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">AdjustAsync</a>(PaymentAdjustment { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Adjust a transaction.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.AdjustAsync(
    new PaymentAdjustment
    {
        PaymentId = "M2MJOG6O2Y",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Adjustments = new List<PaymentAdjustmentAdjustmentsItem>()
        {
            new PaymentAdjustmentAdjustmentsItem(
                new PaymentAdjustmentAdjustmentsItem.Customer(new CustomerAdjustment())
            ),
            new PaymentAdjustmentAdjustmentsItem(
                new PaymentAdjustmentAdjustmentsItem.Order(new OrderAdjustment { Amount = 1000000 })
            ),
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PaymentAdjustment`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">CaptureAsync</a>(PaymentCapture { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Capture an existing payment.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.CaptureAsync(
    new PaymentCapture
    {
        PaymentId = "M2MJOG6O2Y",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PaymentCapture`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">ReverseAsync</a>(PaymentReversal { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Reverse a payment.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.ReverseAsync(
    new PaymentReversal
    {
        PaymentId = "M2MJOG6O2Y",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Amount = 4999,
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PaymentReversal`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">RefundAsync</a>(ReferencedRefund { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Refund a payment.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.RefundAsync(
    new ReferencedRefund
    {
        PaymentId = "M2MJOG6O2Y",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Amount = 4999,
        Description = "Refund for order OrderRef6543",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ReferencedRefund`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Auth

<details><summary><code>client.Auth.<a href="/src/Payroc/Auth/AuthClient.cs">GetTokenAsync</a>(GetTokenAuthRequest { ... }) -> GetTokenResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Obtain an OAuth2 access token using client credentials

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Auth.GetTokenAsync(
    new GetTokenAuthRequest { ClientId = "client_id", ClientSecret = "client_secret" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetTokenAuthRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Boarding Owners

<details><summary><code>client.Boarding.Owners.<a href="/src/Payroc/Boarding/Owners/OwnersClient.cs">GetAsync</a>(GetOwnersRequest { ... }) -> Owner</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a specific owner.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.Owners.GetAsync(new GetOwnersRequest { OwnerId = 1 });
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetOwnersRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.Owners.<a href="/src/Payroc/Boarding/Owners/OwnersClient.cs">UpdateAsync</a>(UpdateOwnersRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Update a specific owner.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.Owners.UpdateAsync(
    new UpdateOwnersRequest
    {
        OwnerId = 1,
        Body = new Owner
        {
            FirstName = "Jane",
            MiddleName = "Helen",
            LastName = "Doe",
            DateOfBirth = "1964-03-22",
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
                EquityPercentage = 48.5f,
                Title = "CFO",
                IsControlProng = true,
                IsAuthorizedSignatory = false,
            },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpdateOwnersRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.Owners.<a href="/src/Payroc/Boarding/Owners/OwnersClient.cs">DeleteAsync</a>(DeleteOwnersRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Delete a owner.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.Owners.DeleteAsync(new DeleteOwnersRequest { OwnerId = 1 });
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteOwnersRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Boarding PricingIntents

<details><summary><code>client.Boarding.PricingIntents.<a href="/src/Payroc/Boarding/PricingIntents/PricingIntentsClient.cs">ListAsync</a>(ListPricingIntentsRequest { ... }) -> PayrocPager<PricingIntent50></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a list of pricing intents.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.PricingIntents.ListAsync(
    new ListPricingIntentsRequest { Before = "2571", After = "8516" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListPricingIntentsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.PricingIntents.<a href="/src/Payroc/Boarding/PricingIntents/PricingIntentsClient.cs">CreateAsync</a>(CreatePricingIntentsRequest { ... }) -> PricingIntent50</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Create a pricing intent.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.PricingIntents.CreateAsync(
    new CreatePricingIntentsRequest
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new PricingIntent50
        {
            Country = "US",
            Base = new BaseUs
            {
                AddressVerification = 5,
                AnnualFee = new BaseUsAnnualFee
                {
                    BillInMonth = BaseUsAnnualFeeBillInMonth.June,
                    Amount = 9900,
                },
                RegulatoryAssistanceProgram = 15,
                PciNonCompliance = 4995,
                MerchantAdvantage = 10,
                PlatinumSecurity = new BaseUsPlatinumSecurity(
                    new BaseUsPlatinumSecurity.Monthly(new BaseUsMonthly())
                ),
                Maintenance = 500,
                Minimum = 100,
                VoiceAuthorization = 95,
                Chargeback = 2500,
                Retrieval = 1500,
                Batch = 1500,
                EarlyTermination = 57500,
            },
            Processor = new PricingAgreementUs50Processor
            {
                Card = new PricingAgreementUs50ProcessorCard(
                    new PricingAgreementUs50ProcessorCard.InterchangePlus(
                        new InterchangePlus
                        {
                            Fees = new InterchangePlusFees
                            {
                                MastercardVisaDiscover = new ProcessorFee(),
                            },
                        }
                    )
                ),
            },
            Services = new List<ServiceUs50>()
            {
                new ServiceUs50(
                    new ServiceUs50.HardwareAdvantagePlan(
                        new HardwareAdvantagePlan { Enabled = true }
                    )
                ),
            },
            Key = "Your-Unique-Identifier",
            Metadata = new Dictionary<string, string>() { { "yourCustomField", "abc123" } },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreatePricingIntentsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.PricingIntents.<a href="/src/Payroc/Boarding/PricingIntents/PricingIntentsClient.cs">GetAsync</a>(GetPricingIntentsRequest { ... }) -> PricingIntent50</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a specific pricing intent.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.PricingIntents.GetAsync(
    new GetPricingIntentsRequest { PricingIntentId = "5" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetPricingIntentsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.PricingIntents.<a href="/src/Payroc/Boarding/PricingIntents/PricingIntentsClient.cs">UpdateAsync</a>(UpdatePricingIntentsRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Update a pricing intent.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.PricingIntents.UpdateAsync(
    new UpdatePricingIntentsRequest
    {
        PricingIntentId = "5",
        Body = new PricingIntent50
        {
            Country = "US",
            Base = new BaseUs
            {
                AddressVerification = 5,
                AnnualFee = new BaseUsAnnualFee
                {
                    BillInMonth = BaseUsAnnualFeeBillInMonth.June,
                    Amount = 9900,
                },
                RegulatoryAssistanceProgram = 15,
                PciNonCompliance = 4995,
                MerchantAdvantage = 10,
                PlatinumSecurity = new BaseUsPlatinumSecurity(
                    new BaseUsPlatinumSecurity.Monthly(new BaseUsMonthly())
                ),
                Maintenance = 500,
                Minimum = 100,
                VoiceAuthorization = 95,
                Chargeback = 2500,
                Retrieval = 1500,
                Batch = 1500,
                EarlyTermination = 57500,
            },
            Processor = new PricingAgreementUs50Processor
            {
                Card = new PricingAgreementUs50ProcessorCard(
                    new PricingAgreementUs50ProcessorCard.InterchangePlus(
                        new InterchangePlus
                        {
                            Fees = new InterchangePlusFees
                            {
                                MastercardVisaDiscover = new ProcessorFee(),
                            },
                        }
                    )
                ),
                Ach = new Ach
                {
                    Fees = new AchFees
                    {
                        Transaction = 50,
                        Batch = 5,
                        Returns = 400,
                        UnauthorizedReturn = 1999,
                        Statement = 800,
                        MonthlyMinimum = 20000,
                        AccountVerification = 10,
                        DiscountRateUnder10000 = 5.25,
                        DiscountRateAbove10000 = 10,
                    },
                },
            },
            Gateway = new GatewayUs50
            {
                Fees = new GatewayUs50Fees
                {
                    Monthly = 2000,
                    Setup = 5000,
                    PerTransaction = 2000,
                    PerDeviceMonthly = 10,
                },
            },
            Services = new List<ServiceUs50>()
            {
                new ServiceUs50(
                    new ServiceUs50.HardwareAdvantagePlan(
                        new HardwareAdvantagePlan { Enabled = true }
                    )
                ),
            },
            Key = "Your-Unique-Identifier",
            Metadata = new Dictionary<string, string>() { { "yourCustomField", "abc123" } },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpdatePricingIntentsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.PricingIntents.<a href="/src/Payroc/Boarding/PricingIntents/PricingIntentsClient.cs">DeleteAsync</a>(DeletePricingIntentsRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Delete a pricing intent.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.PricingIntents.DeleteAsync(
    new DeletePricingIntentsRequest { PricingIntentId = "5" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeletePricingIntentsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.PricingIntents.<a href="/src/Payroc/Boarding/PricingIntents/PricingIntentsClient.cs">PatchAsync</a>(PatchPricingIntentsRequest { ... }) -> PricingIntent50</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Partially update an existing pricing intent.

Structure your request to follow the RFC 6902 standard.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.PricingIntents.PatchAsync(
    new PatchPricingIntentsRequest
    {
        PricingIntentId = "5",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new List<PatchDocument>()
        {
            new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
            new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
            new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PatchPricingIntentsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Boarding MerchantPlatforms

<details><summary><code>client.Boarding.MerchantPlatforms.<a href="/src/Payroc/Boarding/MerchantPlatforms/MerchantPlatformsClient.cs">ListAsync</a>(ListMerchantPlatformsRequest { ... }) -> PayrocPager<MerchantPlatform></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve a [paginated](/api/pagination) list of the merchant platforms that are linked to the ISV's account.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.MerchantPlatforms.ListAsync(
    new ListMerchantPlatformsRequest { Before = "2571", After = "8516" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListMerchantPlatformsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.MerchantPlatforms.<a href="/src/Payroc/Boarding/MerchantPlatforms/MerchantPlatformsClient.cs">CreateAsync</a>(CreateMerchantAccount { ... }) -> MerchantPlatform</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create the entity that represents a business, including its legal information and all its processing accounts.

> **Note**: To add a processing account to an existing merchant platform, go to [Create a processing account](#createProcessingAccount).

The response contains some fields that we require for other methods:

- **merchantPlatformId** - Unique identifier that we assign to the merchant platform. Use the merchantPlatformId to retrieve and update information about the merchant platform.
- **processingAccountId**- Unique identifier that we assign to each processing account. Use the processingAccountId to retrieve and update information about the processing account.  
 <br/>
For more information about how to create a merchant platform, go to [Create a merchant platform.](/guides/integrate/boarding/merchant-platform)
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.MerchantPlatforms.CreateAsync(
    new CreateMerchantAccount
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Business = new Business
        {
            Name = "Example Corp",
            TaxId = "12-3456789",
            OrganizationType = BusinessOrganizationType.PrivateCorporation,
            CountryOfOperation = "US",
            Addresses = new List<LegalAddress>()
            {
                new LegalAddress
                {
                    Address1 = "1 Example Ave.",
                    Address2 = "Example Address Line 2",
                    Address3 = "Example Address Line 3",
                    City = "Chicago",
                    State = "Illinois",
                    Country = "US",
                    PostalCode = "60056",
                    Type = "legalAddress",
                },
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
        ProcessingAccounts = new List<CreateProcessingAccount>()
        {
            new CreateProcessingAccount
            {
                DoingBusinessAs = "Pizza Doe",
                Owners = new List<Owner>()
                {
                    new Owner
                    {
                        FirstName = "Jane",
                        MiddleName = "Helen",
                        LastName = "Doe",
                        DateOfBirth = "1964-03-22",
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
                            EquityPercentage = 48.5f,
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
                BusinessStartDate = "2020-01-01",
                Timezone = CreateProcessingAccountTimezone.AmericaChicago,
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
                    new Pricing.Intent(new PricingTemplate { PricingIntentId = 6123 })
                ),
                Signature = CreateProcessingAccountSignature.RequestedViaDirectLink,
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
        },
        Metadata = new Dictionary<string, string>() { { "customerId", "2345" } },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateMerchantAccount`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.MerchantPlatforms.<a href="/src/Payroc/Boarding/MerchantPlatforms/MerchantPlatformsClient.cs">GetAsync</a>(GetMerchantPlatformsRequest { ... }) -> MerchantPlatform</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a merchant platform, including its legal information and processing accounts.

Include the merchantPlatformId that we sent you when you created the merchant platform.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.MerchantPlatforms.GetAsync(
    new GetMerchantPlatformsRequest { MerchantPlatformId = "12345" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetMerchantPlatformsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.MerchantPlatforms.<a href="/src/Payroc/Boarding/MerchantPlatforms/MerchantPlatformsClient.cs">ListProcessingAccountsAsync</a>(ListBoardingMerchantPlatformProcessingAccountsRequest { ... }) -> PayrocPager<ProcessingAccount></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve a paginated list of processing accounts associated with a merchant platform.

When you created the merchant platform, we sent you its merchantPlatformId in the response. Send this merchantPlatformId as a path parameter in your endpoint.

> **Note**: By default, we return only open processing accounts. To include closed processing accounts, send a value of `true` for the includeClosed query parameter.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.MerchantPlatforms.ListProcessingAccountsAsync(
    new ListBoardingMerchantPlatformProcessingAccountsRequest
    {
        MerchantPlatformId = "12345",
        Before = "2571",
        After = "8516",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListBoardingMerchantPlatformProcessingAccountsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.MerchantPlatforms.<a href="/src/Payroc/Boarding/MerchantPlatforms/MerchantPlatformsClient.cs">CreateProcessingAccountAsync</a>(CreateProcessingAccountMerchantPlatformsRequest { ... }) -> ProcessingAccount</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create a processing account and add it to a merchant platform.  
 > **Note**: You can create and add a processing account only to an existing merchant platform. If you have not already created a merchant platform, go to [Create a merchant platform.](#createMerchant)

In the response we return a processingAccountId for the processing account, which you need for the following methods.

- [Retrieve processing account](#getProcessingAcccounts)
- [List processing account's funding accounts](#listProcessingAccountsFundingAccounts)
- [List contacts](#listProcessingAccountContacts)
- [Get a processing account pricing agreement](#retrieveProcessingAccountPricing)
- [List owners](#listMerchantOwners)
- [Create reminder for processing account](#createReminder)
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.MerchantPlatforms.CreateProcessingAccountAsync(
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
                    DateOfBirth = "1964-03-22",
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
            BusinessStartDate = "2020-01-01",
            Timezone = CreateProcessingAccountTimezone.AmericaChicago,
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
                MonthlyAmounts = new ProcessingMonthlyAmounts { Average = 50000, Highest = 100000 },
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
                new Pricing.Intent(new PricingTemplate { PricingIntentId = 6123 })
            ),
            Signature = CreateProcessingAccountSignature.RequestedViaDirectLink,
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
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateProcessingAccountMerchantPlatformsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Boarding ProcessingAccounts

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">GetAsync</a>(GetProcessingAccountsRequest { ... }) -> ProcessingAccount</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a specific processing account.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.ProcessingAccounts.GetAsync(
    new GetProcessingAccountsRequest { ProcessingAccountId = "38765" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetProcessingAccountsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">ListFundingAccountsAsync</a>(ListProcessingAccountFundingAccountsRequest { ... }) -> IEnumerable<FundingAccount></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a list of funding accounts associated with a processing account.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.ProcessingAccounts.ListFundingAccountsAsync(
    new ListProcessingAccountFundingAccountsRequest { ProcessingAccountId = "38765" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListProcessingAccountFundingAccountsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">ContactsAsync</a>(ContactsProcessingAccountsRequest { ... }) -> PaginatedContacts</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a list of contacts associated with a processing account.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.ProcessingAccounts.ContactsAsync(
    new ContactsProcessingAccountsRequest
    {
        ProcessingAccountId = "38765",
        Before = "2571",
        After = "8516",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ContactsProcessingAccountsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">PricingAsync</a>(PricingProcessingAccountsRequest { ... }) -> PricingProcessingAccountsResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a pricing agreement for a processing account.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.ProcessingAccounts.PricingAsync(
    new PricingProcessingAccountsRequest { ProcessingAccountId = "38765" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PricingProcessingAccountsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">ListOwnersAsync</a>(ListProcessingAccountOwnersRequest { ... }) -> PayrocPager<Owner></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve owners associated with a processing account.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.ProcessingAccounts.ListOwnersAsync(
    new ListProcessingAccountOwnersRequest
    {
        ProcessingAccountId = "38765",
        Before = "2571",
        After = "8516",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListProcessingAccountOwnersRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">CreateReminderAsync</a>(CreateReminderProcessingAccountsRequest { ... }) -> CreateReminderProcessingAccountsResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

When you create a processing account, we send a copy of the pricing agreement to the merchant to sign. You can choose to send them a copy of the pricing agreement by email, or you can generate a link to the pricing agreement.<br/>  
If you requested the merchant's signature by email and they don't respond, use our Reminders endpoint to create a reminder and to send another email.<br/>  
**Note:** You can use the Reminders endpoint only if you request the merchant's signature by email. If you generate a link to the pricing agreement, you can't use the Reminders endpoint.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.ProcessingAccounts.CreateReminderAsync(
    new CreateReminderProcessingAccountsRequest
    {
        ProcessingAccountId = "38765",
        Body = new CreateReminderProcessingAccountsRequestBody(
            new CreateReminderProcessingAccountsRequestBody.PricingAgreement(
                new PricingAgreementReminder()
            )
        ),
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateReminderProcessingAccountsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Boarding Contacts

<details><summary><code>client.Boarding.Contacts.<a href="/src/Payroc/Boarding/Contacts/ContactsClient.cs">GetAsync</a>(GetContactsRequest { ... }) -> Contact</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a specific contact.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.Contacts.GetAsync(new GetContactsRequest { ContactId = 1 });
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetContactsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.Contacts.<a href="/src/Payroc/Boarding/Contacts/ContactsClient.cs">UpdateAsync</a>(UpdateContactsRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Update a specific contact.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.Contacts.UpdateAsync(
    new UpdateContactsRequest
    {
        ContactId = 1,
        Body = new Contact
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
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpdateContactsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Boarding.Contacts.<a href="/src/Payroc/Boarding/Contacts/ContactsClient.cs">DeleteAsync</a>(DeleteContactsRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Delete a contact.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Boarding.Contacts.DeleteAsync(new DeleteContactsRequest { ContactId = 1 });
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteContactsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Funding FundingRecipients

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">ListAsync</a>(ListFundingRecipientsRequest { ... }) -> PayrocPager<FundingRecipient></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a list of all funding recipients associated with the ISV.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingRecipients.ListAsync(
    new ListFundingRecipientsRequest { Before = "2571", After = "8516" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListFundingRecipientsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">CreateAsync</a>(CreateFundingRecipient { ... }) -> FundingRecipient</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Create a funding recipient.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingRecipients.CreateAsync(
    new CreateFundingRecipient
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        RecipientType = CreateFundingRecipientRecipientType.PrivateCorporation,
        TaxId = "12-3456789",
        Address = new Address
        {
            Address1 = "1 Example Ave.",
            City = "Chicago",
            State = "Illinois",
            Country = "US",
            PostalCode = "60056",
        },
        ContactMethods = new List<ContactMethod>()
        {
            new ContactMethod(
                new ContactMethod.Email(new ContactMethodEmail { Value = "jane.doe@example.com" })
            ),
        },
        Owners = new List<Owner>()
        {
            new Owner
            {
                FirstName = "Jane",
                LastName = "Doe",
                DateOfBirth = "1964-03-22",
                Address = new Address
                {
                    Address1 = "1 Example Ave.",
                    City = "Chicago",
                    State = "Illinois",
                    Country = "US",
                    PostalCode = "60056",
                },
                Identifiers = new List<Identifier>()
                {
                    new Identifier { Type = "nationalId", Value = "xxxxx4320" },
                },
                ContactMethods = new List<ContactMethod>()
                {
                    new ContactMethod(
                        new ContactMethod.Email(
                            new ContactMethodEmail { Value = "jane.doe@example.com" }
                        )
                    ),
                },
                Relationship = new OwnerRelationship { IsControlProng = true },
            },
        },
        FundingAccounts = new List<FundingAccount>()
        {
            new FundingAccount
            {
                Type = FundingAccountType.Checking,
                Use = FundingAccountUse.Credit,
                NameOnAccount = "Jane Doe",
                PaymentMethods = new List<PaymentMethodsItem>()
                {
                    new PaymentMethodsItem(new PaymentMethodsItem.Ach(new PaymentMethodAch())),
                },
            },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateFundingRecipient`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">GetAsync</a>(GetFundingRecipientsRequest { ... }) -> FundingRecipient</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a specific funding recipient.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingRecipients.GetAsync(
    new GetFundingRecipientsRequest { RecipientId = 1 }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetFundingRecipientsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">UpdateAsync</a>(UpdateFundingRecipientsRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Update a funding recipient. If you make significant changes, we may need to approve the funding recipient again.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingRecipients.UpdateAsync(
    new UpdateFundingRecipientsRequest
    {
        RecipientId = 1,
        Body = new FundingRecipient
        {
            RecipientType = FundingRecipientRecipientType.PrivateCorporation,
            TaxId = "12-3456789",
            DoingBuinessAs = "doingBuinessAs",
            Address = new Address
            {
                Address1 = "1 Example Ave.",
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
            Owners = new List<FundingRecipientOwnersItem>() { new FundingRecipientOwnersItem() },
            FundingAccounts = new List<FundingRecipientFundingAccountsItem>()
            {
                new FundingRecipientFundingAccountsItem
                {
                    FundingAccountId = 123,
                    Status = FundingRecipientFundingAccountsItemStatus.Approved,
                    Link = new FundingRecipientFundingAccountsItemLink
                    {
                        Rel = "fundingAccount",
                        Href = "https://api.payroc.com/v1/funding-accounts/123",
                        Method = "get",
                    },
                },
                new FundingRecipientFundingAccountsItem
                {
                    FundingAccountId = 124,
                    Status = FundingRecipientFundingAccountsItemStatus.Rejected,
                    Link = new FundingRecipientFundingAccountsItemLink
                    {
                        Rel = "fundingAccount",
                        Href = "https://api.payroc.com/v1/funding-accounts/124",
                        Method = "get",
                    },
                },
            },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpdateFundingRecipientsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">DeleteAsync</a>(DeleteFundingRecipientsRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Delete a funding recipient. This includes funding accounts and owners linked to the funding recipient.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingRecipients.DeleteAsync(
    new DeleteFundingRecipientsRequest { RecipientId = 1 }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteFundingRecipientsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">ListAccountsAsync</a>(ListFundingRecipientFundingAccountsRequest { ... }) -> IEnumerable<FundingAccount></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve all funding accounts associated with the funding recipient.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingRecipients.ListAccountsAsync(
    new ListFundingRecipientFundingAccountsRequest { RecipientId = 1 }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListFundingRecipientFundingAccountsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">CreateAccountAsync</a>(CreateAccountFundingRecipientsRequest { ... }) -> FundingAccount</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Create a new funding account, and add it to the funding recipient.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingRecipients.CreateAccountAsync(
    new CreateAccountFundingRecipientsRequest
    {
        RecipientId = 1,
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new FundingAccount
        {
            Type = FundingAccountType.Checking,
            Use = FundingAccountUse.Credit,
            NameOnAccount = "Jane Doe",
            PaymentMethods = new List<PaymentMethodsItem>()
            {
                new PaymentMethodsItem(new PaymentMethodsItem.Ach(new PaymentMethodAch())),
            },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateAccountFundingRecipientsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">ListOwnersAsync</a>(ListFundingRecipientOwnersRequest { ... }) -> IEnumerable<Owner></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve all owners associated with the funding recipient.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingRecipients.ListOwnersAsync(
    new ListFundingRecipientOwnersRequest { RecipientId = 1 }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListFundingRecipientOwnersRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">CreateOwnerAsync</a>(CreateOwnerFundingRecipientsRequest { ... }) -> Owner</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Create a new owner, and add it to the funding recipient.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingRecipients.CreateOwnerAsync(
    new CreateOwnerFundingRecipientsRequest
    {
        RecipientId = 1,
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new Owner
        {
            FirstName = "Jane",
            LastName = "Doe",
            DateOfBirth = "1964-03-22",
            Address = new Address
            {
                Address1 = "1 Example Ave.",
                City = "Chicago",
                State = "Illinois",
                Country = "US",
                PostalCode = "60056",
            },
            Identifiers = new List<Identifier>()
            {
                new Identifier { Type = "nationalId", Value = "xxxxx4320" },
            },
            ContactMethods = new List<ContactMethod>()
            {
                new ContactMethod(
                    new ContactMethod.Email(
                        new ContactMethodEmail { Value = "jane.doe@example.com" }
                    )
                ),
            },
            Relationship = new OwnerRelationship { IsControlProng = true },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateOwnerFundingRecipientsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Funding FundingAccounts

<details><summary><code>client.Funding.FundingAccounts.<a href="/src/Payroc/Funding/FundingAccounts/FundingAccountsClient.cs">ListAsync</a>(ListFundingAccountsRequest { ... }) -> PayrocPager<FundingAccount></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a list of all funding accounts associated with the ISV.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingAccounts.ListAsync(
    new ListFundingAccountsRequest { Before = "2571", After = "8516" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListFundingAccountsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingAccounts.<a href="/src/Payroc/Funding/FundingAccounts/FundingAccountsClient.cs">GetAsync</a>(GetFundingAccountsRequest { ... }) -> FundingAccount</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a specific funding account.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingAccounts.GetAsync(
    new GetFundingAccountsRequest { FundingAccountId = 1 }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetFundingAccountsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingAccounts.<a href="/src/Payroc/Funding/FundingAccounts/FundingAccountsClient.cs">UpdateAsync</a>(UpdateFundingAccountsRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Update a funding account.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingAccounts.UpdateAsync(
    new UpdateFundingAccountsRequest
    {
        FundingAccountId = 1,
        Body = new FundingAccount
        {
            Type = FundingAccountType.Checking,
            Use = FundingAccountUse.Credit,
            NameOnAccount = "Jane Doe",
            PaymentMethods = new List<PaymentMethodsItem>()
            {
                new PaymentMethodsItem(new PaymentMethodsItem.Ach(new PaymentMethodAch())),
            },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpdateFundingAccountsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingAccounts.<a href="/src/Payroc/Funding/FundingAccounts/FundingAccountsClient.cs">DeleteAsync</a>(DeleteFundingAccountsRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Delete a funding account.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingAccounts.DeleteAsync(
    new DeleteFundingAccountsRequest { FundingAccountId = 1 }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteFundingAccountsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Funding FundingInstructions

<details><summary><code>client.Funding.FundingInstructions.<a href="/src/Payroc/Funding/FundingInstructions/FundingInstructionsClient.cs">ListAsync</a>(ListFundingInstructionsRequest { ... }) -> PayrocPager<ListFundingInstructionsResponseDataItem></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a list of funding instructions for a specific date range.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingInstructions.ListAsync(
    new ListFundingInstructionsRequest
    {
        Before = "2571",
        After = "8516",
        DateFrom = "2024-07-01",
        DateTo = "2024-07-03",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListFundingInstructionsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingInstructions.<a href="/src/Payroc/Funding/FundingInstructions/FundingInstructionsClient.cs">CreateAsync</a>(CreateFundingInstructionsRequest { ... }) -> Instruction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Create funding instructions to tell us how to divide funds between your funding recipients.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingInstructions.CreateAsync(
    new CreateFundingInstructionsRequest
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new Instruction(),
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateFundingInstructionsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingInstructions.<a href="/src/Payroc/Funding/FundingInstructions/FundingInstructionsClient.cs">GetAsync</a>(GetFundingInstructionsRequest { ... }) -> Instruction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a specific funding instruction.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingInstructions.GetAsync(
    new GetFundingInstructionsRequest { InstructionId = 1 }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetFundingInstructionsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingInstructions.<a href="/src/Payroc/Funding/FundingInstructions/FundingInstructionsClient.cs">UpdateAsync</a>(UpdateFundingInstructionsRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Update an existing funding instruction.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingInstructions.UpdateAsync(
    new UpdateFundingInstructionsRequest { InstructionId = 1, Body = new Instruction() }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpdateFundingInstructionsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingInstructions.<a href="/src/Payroc/Funding/FundingInstructions/FundingInstructionsClient.cs">DeleteAsync</a>(DeleteFundingInstructionsRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Delete an existing funding instruction.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingInstructions.DeleteAsync(
    new DeleteFundingInstructionsRequest { InstructionId = 1 }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteFundingInstructionsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Funding FundingActivity

<details><summary><code>client.Funding.FundingActivity.<a href="/src/Payroc/Funding/FundingActivity/FundingActivityClient.cs">GetBalanceAsync</a>(GetBalanceFundingActivityRequest { ... }) -> GetBalanceFundingActivityResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve the balance of funds that are available for each merchant.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingActivity.GetBalanceAsync(
    new GetBalanceFundingActivityRequest
    {
        Before = "2571",
        After = "8516",
        MerchantId = "4525644354",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetBalanceFundingActivityRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingActivity.<a href="/src/Payroc/Funding/FundingActivity/FundingActivityClient.cs">GetAsync</a>(GetFundingActivityRequest { ... }) -> GetFundingActivityResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve funding activity for a specific date range.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Funding.FundingActivity.GetAsync(
    new GetFundingActivityRequest
    {
        Before = "2571",
        After = "8516",
        DateFrom = "2024-07-02",
        DateTo = "2024-07-03",
        MerchantId = "4525644354",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetFundingActivityRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Payments PaymentPlans

<details><summary><code>client.Payments.PaymentPlans.<a href="/src/Payroc/Payments/PaymentPlans/PaymentPlansClient.cs">ListAsync</a>(ListPaymentPlansRequest { ... }) -> PayrocPager<PaymentPlan></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a list of payment plans.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.PaymentPlans.ListAsync(
    new ListPaymentPlansRequest
    {
        ProcessingTerminalId = "1234001",
        Before = "2571",
        After = "8516",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListPaymentPlansRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.PaymentPlans.<a href="/src/Payroc/Payments/PaymentPlans/PaymentPlansClient.cs">CreateAsync</a>(CreatePaymentPlansRequest { ... }) -> PaymentPlan</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Create a new payment plan.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.PaymentPlans.CreateAsync(
    new CreatePaymentPlansRequest
    {
        ProcessingTerminalId = "1234001",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new PaymentPlan
        {
            PaymentPlanId = "PlanRef8765",
            Name = "Premium Club",
            Description = "Monthly Premium Club subscription",
            Currency = Currency.Usd,
            SetupOrder = new PaymentPlanSetupOrder
            {
                Amount = 4999,
                Description = "Initial setup fee for Premium Club subscription",
                Breakdown = new PaymentPlanOrderBreakdown
                {
                    Subtotal = 4347,
                    Taxes = new List<Tax>()
                    {
                        new Tax { Name = "Sales Tax", Rate = 5 },
                    },
                },
            },
            RecurringOrder = new PaymentPlanRecurringOrder
            {
                Amount = 4999,
                Description = "Monthly Premium Club subscription",
                Breakdown = new PaymentPlanOrderBreakdown
                {
                    Subtotal = 4347,
                    Taxes = new List<Tax>()
                    {
                        new Tax { Name = "Sales Tax", Rate = 5 },
                    },
                },
            },
            Length = 12,
            Type = PaymentPlanType.Automatic,
            Frequency = PaymentPlanFrequency.Monthly,
            OnUpdate = PaymentPlanOnUpdate.Continue,
            OnDelete = PaymentPlanOnDelete.Complete,
            CustomFieldNames = new List<string>() { "yourCustomField" },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreatePaymentPlansRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.PaymentPlans.<a href="/src/Payroc/Payments/PaymentPlans/PaymentPlansClient.cs">GetAsync</a>(GetPaymentPlansRequest { ... }) -> PaymentPlan</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a specific payment plan.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.PaymentPlans.GetAsync(
    new GetPaymentPlansRequest { ProcessingTerminalId = "1234001", PaymentPlanId = "PlanRef8765" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetPaymentPlansRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.PaymentPlans.<a href="/src/Payroc/Payments/PaymentPlans/PaymentPlansClient.cs">DeleteAsync</a>(DeletePaymentPlansRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Delete an existing payment plan.  
**Note:** After you delete a payment plan, you can't reuse the paymentPlanId.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.PaymentPlans.DeleteAsync(
    new DeletePaymentPlansRequest
    {
        ProcessingTerminalId = "1234001",
        PaymentPlanId = "PlanRef8765",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeletePaymentPlansRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.PaymentPlans.<a href="/src/Payroc/Payments/PaymentPlans/PaymentPlansClient.cs">UpdateAsync</a>(UpdatePaymentPlansRequest { ... }) -> PaymentPlan</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Make changes to an existing payment plan.

Structure your request to follow the RFC 6902 standard.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.PaymentPlans.UpdateAsync(
    new UpdatePaymentPlansRequest
    {
        ProcessingTerminalId = "1234001",
        PaymentPlanId = "PlanRef8765",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new List<PatchDocument>()
        {
            new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
            new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
            new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpdatePaymentPlansRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Payments Subscriptions

<details><summary><code>client.Payments.Subscriptions.<a href="/src/Payroc/Payments/Subscriptions/SubscriptionsClient.cs">ListAsync</a>(ListSubscriptionsRequest { ... }) -> PayrocPager<Subscription></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

List subscriptions linked to a terminal.  
To filter your results, use the query parameters.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.Subscriptions.ListAsync(
    new ListSubscriptionsRequest
    {
        ProcessingTerminalId = "1234001",
        CustomerName = "Sarah%20Hazel%20Hopper",
        Last4 = "7062",
        PaymentPlan = "Premium%20Club",
        EndDate = "2025-07-01T00:00:00Z",
        NextDueDate = "2024-08-01T00:00:00Z",
        Before = "2571",
        After = "8516",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListSubscriptionsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.Subscriptions.<a href="/src/Payroc/Payments/Subscriptions/SubscriptionsClient.cs">CreateAsync</a>(SubscriptionRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Create a new subscription.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.Subscriptions.CreateAsync(
    new SubscriptionRequest
    {
        ProcessingTerminalId = "1234001",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        SubscriptionId = "SubRef7654",
        PaymentPlanId = "PlanRef8765",
        PaymentMethod = new SubscriptionRequestPaymentMethod(
            new SubscriptionRequestPaymentMethod.SecureToken(
                new SecureTokenPayload { Token = "1234567890123456789" }
            )
        ),
        Name = "Premium Club",
        Description = "Premium Club subscription",
        SetupOrder = new SubscriptionPaymentOrder
        {
            OrderId = "OrderRef6543",
            Amount = 4999,
            Description = "Initial setup fee for Premium Club subscription",
            Breakdown = new SubscriptionOrderBreakdown
            {
                Subtotal = 4347,
                Taxes = new List<Tax>()
                {
                    new Tax { Name = "Sales Tax", Rate = 5 },
                },
            },
        },
        RecurringOrder = new SubscriptionRecurringOrder
        {
            Amount = 4999,
            Description = "Monthly Premium Club subscription",
            Breakdown = new SubscriptionOrderBreakdown
            {
                Subtotal = 4347,
                Taxes = new List<Tax>()
                {
                    new Tax { Name = "Sales Tax", Rate = 5 },
                },
            },
        },
        StartDate = "2024-07-02",
        EndDate = "2025-07-01",
        Length = 12,
        PauseCollectionFor = 0,
        CustomFields = new List<CustomField>()
        {
            new CustomField { Name = "yourCustomField", Value = "abc123" },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `SubscriptionRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.Subscriptions.<a href="/src/Payroc/Payments/Subscriptions/SubscriptionsClient.cs">GetAsync</a>(GetSubscriptionsRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a specific subscription.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.Subscriptions.GetAsync(
    new GetSubscriptionsRequest { ProcessingTerminalId = "1234001", SubscriptionId = "SubRef7654" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetSubscriptionsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.Subscriptions.<a href="/src/Payroc/Payments/Subscriptions/SubscriptionsClient.cs">UpdateAsync</a>(UpdateSubscriptionsRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Make changes to a subscription.

Structure your request to follow the RFC 6902 standard.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.Subscriptions.UpdateAsync(
    new UpdateSubscriptionsRequest
    {
        ProcessingTerminalId = "1234001",
        SubscriptionId = "SubRef7654",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new List<PatchDocument>()
        {
            new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
            new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
            new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpdateSubscriptionsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.Subscriptions.<a href="/src/Payroc/Payments/Subscriptions/SubscriptionsClient.cs">DeactivateAsync</a>(DeactivateSubscriptionsRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Deactivate a subscription.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.Subscriptions.DeactivateAsync(
    new DeactivateSubscriptionsRequest
    {
        ProcessingTerminalId = "1234001",
        SubscriptionId = "SubRef7654",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeactivateSubscriptionsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.Subscriptions.<a href="/src/Payroc/Payments/Subscriptions/SubscriptionsClient.cs">ReactivateAsync</a>(ReactivateSubscriptionsRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Re-activate an existing subscription.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.Subscriptions.ReactivateAsync(
    new ReactivateSubscriptionsRequest
    {
        ProcessingTerminalId = "1234001",
        SubscriptionId = "SubRef7654",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ReactivateSubscriptionsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.Subscriptions.<a href="/src/Payroc/Payments/Subscriptions/SubscriptionsClient.cs">PayAsync</a>(SubscriptionPaymentRequest { ... }) -> SubscriptionPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Process payment for a manual subscription.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.Subscriptions.PayAsync(
    new SubscriptionPaymentRequest
    {
        ProcessingTerminalId = "1234001",
        SubscriptionId = "SubRef7654",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Operator = "Jane",
        Order = new SubscriptionPaymentOrder
        {
            OrderId = "OrderRef6543",
            Amount = 4999,
            Description = "Monthly Premium Club subscription",
            Breakdown = new SubscriptionOrderBreakdown
            {
                Subtotal = 4999,
                Taxes = new List<Tax>()
                {
                    new Tax { Name = "Sales Tax", Rate = 5 },
                },
            },
        },
        CustomFields = new List<CustomField>()
        {
            new CustomField { Name = "yourCustomField", Value = "abc123" },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `SubscriptionPaymentRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Payments SecureTokens

<details><summary><code>client.Payments.SecureTokens.<a href="/src/Payroc/Payments/SecureTokens/SecureTokensClient.cs">ListAsync</a>(ListSecureTokensRequest { ... }) -> PayrocPager<SecureToken></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Return a list of secure tokens that are currently saved on the terminal.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.SecureTokens.ListAsync(
    new ListSecureTokensRequest
    {
        ProcessingTerminalId = "1234001",
        SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        CustomerName = "Sarah%20Hazel%20Hopper",
        Phone = "2025550165",
        Email = "sarah.hopper@example.com",
        Token = "296753123456",
        First6 = "453985",
        Last4 = "7062",
        Before = "2571",
        After = "8516",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListSecureTokensRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.SecureTokens.<a href="/src/Payroc/Payments/SecureTokens/SecureTokensClient.cs">CreateAsync</a>(TokenizationRequest { ... }) -> SecureToken</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Save the customer's payment details to use in future transactions.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.SecureTokens.CreateAsync(
    new TokenizationRequest
    {
        ProcessingTerminalId = "1234001",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Operator = "Jane",
        MitAgreement = TokenizationRequestMitAgreement.Unscheduled,
        Customer = new Customer
        {
            FirstName = "Sarah",
            LastName = "Hopper",
            DateOfBirth = "1990-07-15",
            ReferenceNumber = "Customer-12",
            BillingAddress = new Address
            {
                Address1 = "1 Example Ave.",
                Address2 = "Example Address Line 2",
                Address3 = "Example Address Line 3",
                City = "Chicago",
                State = "Illinois",
                Country = "US",
                PostalCode = "60056",
            },
            ShippingAddress = new Shipping
            {
                RecipientName = "Sarah Hopper",
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
            },
            ContactMethods = new List<ContactMethod>()
            {
                new ContactMethod(
                    new ContactMethod.Email(
                        new ContactMethodEmail { Value = "jane.doe@example.com" }
                    )
                ),
            },
            NotificationLanguage = CustomerNotificationLanguage.En,
        },
        IpAddress = new IpAddress { Type = IpAddressType.Ipv4, Value = "104.18.24.203" },
        Source = new TokenizationRequestSource(
            new TokenizationRequestSource.Card(
                new CardPayload
                {
                    CardDetails = new CardPayloadCardDetails(
                        new CardPayloadCardDetails.Raw(
                            new RawCardDetails
                            {
                                Device = new Device
                                {
                                    Model = DeviceModel.BbposChp,
                                    SerialNumber = "PAX123456789",
                                },
                                RawData =
                                    "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                            }
                        )
                    ),
                }
            )
        ),
        CustomFields = new List<CustomField>()
        {
            new CustomField { Name = "yourCustomField", Value = "abc123" },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `TokenizationRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.SecureTokens.<a href="/src/Payroc/Payments/SecureTokens/SecureTokensClient.cs">GetAsync</a>(GetSecureTokensRequest { ... }) -> SecureToken</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Return a secure token and its related payment details.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.SecureTokens.GetAsync(
    new GetSecureTokensRequest
    {
        ProcessingTerminalId = "1234001",
        SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetSecureTokensRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.SecureTokens.<a href="/src/Payroc/Payments/SecureTokens/SecureTokensClient.cs">DeleteAsync</a>(DeleteSecureTokensRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Delete a secure token and its represented payment details.  
**Note**: If you delete a token, you can't reuse its identifier.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.SecureTokens.DeleteAsync(
    new DeleteSecureTokensRequest
    {
        ProcessingTerminalId = "1234001",
        SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteSecureTokensRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.SecureTokens.<a href="/src/Payroc/Payments/SecureTokens/SecureTokensClient.cs">UpdateAsync</a>(UpdateSecureTokensRequest { ... }) -> SecureToken</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Update the customer's payment details that are represented by the secure token.

Structure your request to follow the RFC 6902 standard.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.SecureTokens.UpdateAsync(
    new UpdateSecureTokensRequest
    {
        ProcessingTerminalId = "1234001",
        SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new List<PatchDocument>()
        {
            new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
            new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
            new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpdateSecureTokensRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.SecureTokens.<a href="/src/Payroc/Payments/SecureTokens/SecureTokensClient.cs">AccountUpdateAsync</a>(AccountUpdateSecureTokensRequest { ... }) -> SecureToken</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

If you have a single-use token, use this method to update payment details that are represented by a secure token.

If you don’t have a single-use token, and you want to update payment details represented by a secure token, go to
[updateSecureToken](https://docs.payroc.com/api/resources#updateSecureToken).

**Note**: For more information about tokenization, go to [tokenization](https://docs.payroc.com/knowledge/basic-concepts/tokenization).

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.SecureTokens.AccountUpdateAsync(
    new AccountUpdateSecureTokensRequest
    {
        SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        ProcessingTerminalId = "1234001",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new AccountUpdate(
            new AccountUpdate.SingleUseToken(
                new SingleUseTokenAccountUpdate
                {
                    Token =
                        "abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890",
                }
            )
        ),
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `AccountUpdateSecureTokensRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Payments SingleUseTokens

<details><summary><code>client.Payments.SingleUseTokens.<a href="/src/Payroc/Payments/SingleUseTokens/SingleUseTokensClient.cs">CreateAsync</a>(SingleUseTokenRequest { ... }) -> SingleUseToken</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Create a single-use token. The token expires after 30 minutes.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.SingleUseTokens.CreateAsync(
    new SingleUseTokenRequest
    {
        ProcessingTerminalId = "1234001",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Channel = SingleUseTokenRequestChannel.Web,
        Operator = "Jane",
        Source = new SingleUseTokenRequestSource(
            new SingleUseTokenRequestSource.Card(
                new CardPayload
                {
                    CardDetails = new CardPayloadCardDetails(
                        new CardPayloadCardDetails.Raw(
                            new RawCardDetails
                            {
                                Device = new Device
                                {
                                    Model = DeviceModel.BbposChp,
                                    SerialNumber = "PAX123456789",
                                },
                                RawData =
                                    "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                            }
                        )
                    ),
                }
            )
        ),
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `SingleUseTokenRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Payments HostedFields

<details><summary><code>client.Payments.HostedFields.<a href="/src/Payroc/Payments/HostedFields/HostedFieldsClient.cs">CreateAsync</a>(HostedFieldsCreateSessionRequest { ... }) -> HostedFieldsCreateSessionResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create a session token that you use to authenticate a Hosted Fields session. When you create an instance of Hosted Fields on a webpage, include the session token in the config object.

For more information about how to set up Hosted Fields, see [Set up Hosted Fields](https://docs.payroc.com/guides/integrate/hosted-fields/set-up-hosted-fields).

**Note:** You need to generate a new session token each time you load Hosted Fields on a webpage.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.HostedFields.CreateAsync(
    new HostedFieldsCreateSessionRequest
    {
        ProcessingTerminalId = "1234001",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        LibVersion = "1.1.0.123456",
        Scenario = HostedFieldsCreateSessionRequestScenario.Payment,
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `HostedFieldsCreateSessionRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Payments Refunds

<details><summary><code>client.Payments.Refunds.<a href="/src/Payroc/Payments/Refunds/RefundsClient.cs">ListAsync</a>(ListRefundsRequest { ... }) -> PayrocPager<Refund></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Return a list of refunds.  
To filter your results, use query parameters.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.Refunds.ListAsync(
    new ListRefundsRequest
    {
        ProcessingTerminalId = "1234001",
        OrderId = "OrderRef6543",
        Operator = "Jane",
        CardholderName = "Sarah%20Hazel%20Hopper",
        First6 = "453985",
        Last4 = "7062",
        DateFrom = new DateTime(2024, 07, 01, 15, 30, 00, 000),
        DateTo = new DateTime(2024, 07, 03, 15, 30, 00, 000),
        SettlementDate = "2024-07-02",
        Before = "2571",
        After = "8516",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListRefundsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.Refunds.<a href="/src/Payroc/Payments/Refunds/RefundsClient.cs">CreateAsync</a>(UnreferencedRefund { ... }) -> Refund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Create an unreferenced refund.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.Refunds.CreateAsync(
    new UnreferencedRefund
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Channel = UnreferencedRefundChannel.Pos,
        ProcessingTerminalId = "1234001",
        Order = new RefundOrder
        {
            OrderId = "OrderRef6543",
            Description = "Refund for order OrderRef6543",
            Amount = 4999,
            Currency = Currency.Usd,
        },
        RefundMethod = new UnreferencedRefundRefundMethod(
            new UnreferencedRefundRefundMethod.Card(
                new CardPayload
                {
                    CardDetails = new CardPayloadCardDetails(
                        new CardPayloadCardDetails.Raw(
                            new RawCardDetails
                            {
                                Device = new Device
                                {
                                    Model = DeviceModel.BbposChp,
                                    SerialNumber = "PAX123456789",
                                },
                                RawData =
                                    "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                            }
                        )
                    ),
                }
            )
        ),
        CustomFields = new List<CustomField>()
        {
            new CustomField { Name = "yourCustomField", Value = "abc123" },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UnreferencedRefund`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.Refunds.<a href="/src/Payroc/Payments/Refunds/RefundsClient.cs">GetAsync</a>(GetRefundsRequest { ... }) -> Refund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a specific refund.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.Refunds.GetAsync(new GetRefundsRequest { RefundId = "CD3HN88U9F" });
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetRefundsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.Refunds.<a href="/src/Payroc/Payments/Refunds/RefundsClient.cs">AdjustAsync</a>(RefundAdjustment { ... }) -> Refund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Adjust an existing refund.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.Refunds.AdjustAsync(
    new RefundAdjustment
    {
        RefundId = "CD3HN88U9F",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Operator = "Jane",
        Adjustments = new List<RefundAdjustmentAdjustmentsItem>()
        {
            new RefundAdjustmentAdjustmentsItem(
                new RefundAdjustmentAdjustmentsItem.Customer(new CustomerAdjustment())
            ),
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `RefundAdjustment`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.Refunds.<a href="/src/Payroc/Payments/Refunds/RefundsClient.cs">ReverseAsync</a>(ReverseRefundsRequest { ... }) -> Refund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Void an existing refund.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.Refunds.ReverseAsync(
    new ReverseRefundsRequest
    {
        RefundId = "CD3HN88U9F",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ReverseRefundsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Payments Cards

<details><summary><code>client.Payments.Cards.<a href="/src/Payroc/Payments/Cards/CardsClient.cs">VerifyAsync</a>(CardVerificationRequest { ... }) -> CardVerificationResult</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Verify that a card is valid. For banks that do not support verification, we charge a micro deposit that we void immediately.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.Cards.VerifyAsync(
    new CardVerificationRequest
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        ProcessingTerminalId = "1234001",
        Operator = "Jane",
        Card = new CardVerificationRequestCard(
            new CardVerificationRequestCard.Card(
                new CardPayload
                {
                    CardDetails = new CardPayloadCardDetails(
                        new CardPayloadCardDetails.Raw(
                            new RawCardDetails
                            {
                                Device = new Device
                                {
                                    Model = DeviceModel.BbposChp,
                                    SerialNumber = "PAX123456789",
                                },
                                RawData =
                                    "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                            }
                        )
                    ),
                }
            )
        ),
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CardVerificationRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.Cards.<a href="/src/Payroc/Payments/Cards/CardsClient.cs">BalanceAsync</a>(BalanceInquiry { ... }) -> Balance</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Request the balance of an Electronic Benefit Transfer (EBT) card.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.Cards.BalanceAsync(
    new BalanceInquiry
    {
        ProcessingTerminalId = "1234001",
        Operator = "Jane",
        Currency = Currency.Usd,
        Card = new BalanceInquiryCard(
            new BalanceInquiryCard.Card(
                new CardPayload
                {
                    CardDetails = new CardPayloadCardDetails(
                        new CardPayloadCardDetails.Raw(
                            new RawCardDetails
                            {
                                Device = new Device
                                {
                                    Model = DeviceModel.BbposChp,
                                    SerialNumber = "PAX123456789",
                                },
                                RawData =
                                    "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                            }
                        )
                    ),
                }
            )
        ),
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `BalanceInquiry`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.Cards.<a href="/src/Payroc/Payments/Cards/CardsClient.cs">BinLookupAsync</a>(BinLookup { ... }) -> CardInfo</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Perform a BIN (Bank Identification Number) lookup to retrieve information about a card.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.Cards.BinLookupAsync(
    new BinLookup
    {
        ProcessingTerminalId = "1234001",
        Card = new BinLookupCard(
            new BinLookupCard.Card(
                new CardPayload
                {
                    CardDetails = new CardPayloadCardDetails(
                        new CardPayloadCardDetails.Raw(
                            new RawCardDetails
                            {
                                Device = new Device
                                {
                                    Model = DeviceModel.BbposChp,
                                    SerialNumber = "PAX123456789",
                                },
                                RawData =
                                    "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                            }
                        )
                    ),
                }
            )
        ),
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `BinLookup`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Payments CurrencyConversion

<details><summary><code>client.Payments.CurrencyConversion.<a href="/src/Payroc/Payments/CurrencyConversion/CurrencyConversionClient.cs">GetFxRatesAsync</a>(FxRateInquiry { ... }) -> FxRate</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Check if a customer’s card is eligible for Dynamic Currency Conversion (DCC).
If the card is eligible for DCC, offer currency conversion to the customer during a transaction.  
**Note:** We offer this through the DCC service, which gives customers a choice to pay in the local currency or their own currency.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.CurrencyConversion.GetFxRatesAsync(
    new FxRateInquiry
    {
        Channel = FxRateInquiryChannel.Web,
        ProcessingTerminalId = "1234001",
        Operator = "Jane",
        BaseAmount = 10000,
        BaseCurrency = Currency.Usd,
        PaymentMethod = new FxRateInquiryPaymentMethod(
            new FxRateInquiryPaymentMethod.Card(
                new CardPayload
                {
                    CardDetails = new CardPayloadCardDetails(
                        new CardPayloadCardDetails.Raw(
                            new RawCardDetails
                            {
                                Device = new Device
                                {
                                    Model = DeviceModel.BbposChp,
                                    SerialNumber = "PAX123456789",
                                },
                                RawData =
                                    "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                            }
                        )
                    ),
                }
            )
        ),
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `FxRateInquiry`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Payments BankTransferPayments

<details><summary><code>client.Payments.BankTransferPayments.<a href="/src/Payroc/Payments/BankTransferPayments/BankTransferPaymentsClient.cs">ListAsync</a>(ListBankTransferPaymentsRequest { ... }) -> PayrocPager<BankTransferPayment></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a list of payments.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.BankTransferPayments.ListAsync(
    new ListBankTransferPaymentsRequest
    {
        ProcessingTerminalId = "1234001",
        OrderId = "OrderRef6543",
        NameOnAccount = "Sarah%20Hazel%20Hopper",
        Last4 = "7890",
        DateFrom = new DateTime(2024, 07, 01, 00, 00, 00, 000),
        DateTo = new DateTime(2024, 07, 31, 23, 59, 59, 000),
        SettlementDate = "2024-07-15",
        Before = "2571",
        After = "8516",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListBankTransferPaymentsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferPayments.<a href="/src/Payroc/Payments/BankTransferPayments/BankTransferPaymentsClient.cs">CreateAsync</a>(BankTransferPaymentRequest { ... }) -> BankTransferPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Run a sale with a customer's bank account details.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.BankTransferPayments.CreateAsync(
    new BankTransferPaymentRequest
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        ProcessingTerminalId = "1234001",
        Order = new BankTransferPaymentOrder
        {
            OrderId = "OrderRef6543",
            Description = "Large Pepperoni Pizza",
            Amount = 4999,
            Currency = Currency.Usd,
            Breakdown = new BankTransferBreakdown
            {
                Subtotal = 4347,
                Tip = new Tip { Type = TipType.Percentage, Percentage = 10 },
                Taxes = new List<Tax>()
                {
                    new Tax { Name = "Sales Tax", Rate = 5 },
                },
            },
        },
        Customer = new BankTransferCustomer
        {
            NotificationLanguage = BankTransferCustomerNotificationLanguage.En,
            ContactMethods = new List<ContactMethod>()
            {
                new ContactMethod(
                    new ContactMethod.Email(
                        new ContactMethodEmail { Value = "jane.doe@example.com" }
                    )
                ),
            },
        },
        CredentialOnFile = new CredentialOnFile { Tokenize = true },
        PaymentMethod = new BankTransferPaymentRequestPaymentMethod(
            new BankTransferPaymentRequestPaymentMethod.Ach(
                new AchPayload
                {
                    NameOnAccount = "Shara Hazel Hopper",
                    AccountNumber = "1234567890",
                    RoutingNumber = "123456789",
                }
            )
        ),
        CustomFields = new List<CustomField>()
        {
            new CustomField { Name = "yourCustomField", Value = "abc123" },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `BankTransferPaymentRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferPayments.<a href="/src/Payroc/Payments/BankTransferPayments/BankTransferPaymentsClient.cs">GetAsync</a>(GetBankTransferPaymentsRequest { ... }) -> BankTransferPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a specific payment.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.BankTransferPayments.GetAsync(
    new GetBankTransferPaymentsRequest { PaymentId = "M2MJOG6O2Y" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetBankTransferPaymentsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferPayments.<a href="/src/Payroc/Payments/BankTransferPayments/BankTransferPaymentsClient.cs">ReverseAsync</a>(ReverseBankTransferPaymentsRequest { ... }) -> BankTransferPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Cancel a payment in an open batch.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.BankTransferPayments.ReverseAsync(
    new ReverseBankTransferPaymentsRequest
    {
        PaymentId = "M2MJOG6O2Y",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ReverseBankTransferPaymentsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferPayments.<a href="/src/Payroc/Payments/BankTransferPayments/BankTransferPaymentsClient.cs">RefundAsync</a>(BankTransferReferencedRefund { ... }) -> BankTransferPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Refund a payment.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.BankTransferPayments.RefundAsync(
    new BankTransferReferencedRefund
    {
        PaymentId = "M2MJOG6O2Y",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Amount = 4999,
        Description = "amount to refund",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `BankTransferReferencedRefund`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferPayments.<a href="/src/Payroc/Payments/BankTransferPayments/BankTransferPaymentsClient.cs">RepresentAsync</a>(Representment { ... }) -> BankTransferPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Re-present a customer's bank account details if the first payment was declined.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.BankTransferPayments.RepresentAsync(
    new Representment
    {
        PaymentId = "M2MJOG6O2Y",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        PaymentMethod = new RepresentmentPaymentMethod(
            new RepresentmentPaymentMethod.Ach(
                new AchPayload
                {
                    NameOnAccount = "Shara Hazel Hopper",
                    AccountNumber = "1234567890",
                    RoutingNumber = "123456789",
                }
            )
        ),
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `Representment`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Payments BankTransferRefunds

<details><summary><code>client.Payments.BankTransferRefunds.<a href="/src/Payroc/Payments/BankTransferRefunds/BankTransferRefundsClient.cs">ListAsync</a>(ListBankTransferRefundsRequest { ... }) -> PayrocPager<BankTransferRefund></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Return a list of refund transactions.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.BankTransferRefunds.ListAsync(
    new ListBankTransferRefundsRequest
    {
        ProcessingTerminalId = "1234001",
        OrderId = "OrderRef6543",
        NameOnAccount = "Sarah%20Hazel%20Hopper",
        Last4 = "7062",
        DateFrom = new DateTime(2024, 07, 01, 00, 00, 00, 000),
        DateTo = new DateTime(2024, 07, 31, 23, 59, 59, 000),
        SettlementDate = "2024-07-15",
        Before = "2571",
        After = "8516",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListBankTransferRefundsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferRefunds.<a href="/src/Payroc/Payments/BankTransferRefunds/BankTransferRefundsClient.cs">CreateAsync</a>(BankTransferUnreferencedRefund { ... }) -> BankTransferRefund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Send a refund to a customer's bank account. The refund transaction is not linked to the previous transaction.  
\*Note\*\*: This function is available to only certain merchant accounts.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.BankTransferRefunds.CreateAsync(
    new BankTransferUnreferencedRefund
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        ProcessingTerminalId = "1234001",
        Order = new BankTransferRefundOrder
        {
            OrderId = "OrderRef6543",
            Description = "Refund for order OrderRef6543",
            Amount = 4999,
            Currency = Currency.Usd,
        },
        RefundMethod = new BankTransferUnreferencedRefundRefundMethod(
            new BankTransferUnreferencedRefundRefundMethod.Ach(
                new AchPayload
                {
                    NameOnAccount = "Shara Hazel Hopper",
                    AccountNumber = "1234567890",
                    RoutingNumber = "123456789",
                }
            )
        ),
        CustomFields = new List<CustomField>()
        {
            new CustomField { Name = "yourCustomField", Value = "abc123" },
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `BankTransferUnreferencedRefund`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferRefunds.<a href="/src/Payroc/Payments/BankTransferRefunds/BankTransferRefundsClient.cs">GetAsync</a>(GetBankTransferRefundsRequest { ... }) -> BankTransferRefund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Return a specific refund transaction.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.BankTransferRefunds.GetAsync(
    new GetBankTransferRefundsRequest { RefundId = "CD3HN88U9F" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetBankTransferRefundsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferRefunds.<a href="/src/Payroc/Payments/BankTransferRefunds/BankTransferRefundsClient.cs">ReverseAsync</a>(ReverseBankTransferRefundsRequest { ... }) -> BankTransferRefund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Void a refund transaction.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.BankTransferRefunds.ReverseAsync(
    new ReverseBankTransferRefundsRequest
    {
        RefundId = "CD3HN88U9F",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ReverseBankTransferRefundsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Payments BankAccounts

<details><summary><code>client.Payments.BankAccounts.<a href="/src/Payroc/Payments/BankAccounts/BankAccountsClient.cs">VerifyAsync</a>(BankAccountVerificationRequest { ... }) -> BankAccountVerificationResult</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Verify the customer's bank account details.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Payments.BankAccounts.VerifyAsync(
    new BankAccountVerificationRequest
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        ProcessingTerminalId = "1234001",
        BankAccount = new BankAccountVerificationRequestBankAccount(
            new BankAccountVerificationRequestBankAccount.Pad(
                new PadPayload
                {
                    NameOnAccount = "Sarah Hazel Hopper",
                    AccountNumber = "1234567890",
                    TransitNumber = "76543",
                    InstitutionNumber = "543",
                }
            )
        ),
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `BankAccountVerificationRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## PayrocCloud PaymentInstructions

<details><summary><code>client.PayrocCloud.PaymentInstructions.<a href="/src/Payroc/PayrocCloud/PaymentInstructions/PaymentInstructionsClient.cs">SendAsync</a>(PaymentInstructionRequest { ... }) -> PaymentInstruction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Submit an instruction request to initiate a sale on a payment device.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.PayrocCloud.PaymentInstructions.SendAsync(
    new PaymentInstructionRequest
    {
        SerialNumber = "1850010868",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Operator = "Jane",
        ProcessingTerminalId = "1234001",
        Order = new PaymentInstructionOrder
        {
            OrderId = "OrderRef6543",
            Amount = 4999,
            Currency = Currency.Usd,
        },
        CustomizationOptions = new CustomizationOptions
        {
            EntryMethod = CustomizationOptionsEntryMethod.DeviceRead,
        },
        AutoCapture = true,
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PaymentInstructionRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.PayrocCloud.PaymentInstructions.<a href="/src/Payroc/PayrocCloud/PaymentInstructions/PaymentInstructionsClient.cs">GetAsync</a>(GetPaymentInstructionsRequest { ... }) -> PaymentInstruction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve the current status of a specific payment instruction.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.PayrocCloud.PaymentInstructions.GetAsync(
    new GetPaymentInstructionsRequest { PaymentInstructionId = "e743a9165d134678a9100ebba3b29597" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetPaymentInstructionsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## PayrocCloud RefundInstructions

<details><summary><code>client.PayrocCloud.RefundInstructions.<a href="/src/Payroc/PayrocCloud/RefundInstructions/RefundInstructionsClient.cs">SendAsync</a>(RefundInstructionRequest { ... }) -> RefundInstruction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Submit an instruction request to initiate a refund on a payment device.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.PayrocCloud.RefundInstructions.SendAsync(
    new RefundInstructionRequest
    {
        SerialNumber = "1850010868",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Operator = "Jane",
        ProcessingTerminalId = "1234001",
        Order = new RefundInstructionOrder
        {
            OrderId = "OrderRef6543",
            Description = "Refund for order OrderRef6543",
            Amount = 4999,
            Currency = Currency.Usd,
        },
        CustomizationOptions = new CustomizationOptions
        {
            EntryMethod = CustomizationOptionsEntryMethod.ManualEntry,
        },
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `RefundInstructionRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.PayrocCloud.RefundInstructions.<a href="/src/Payroc/PayrocCloud/RefundInstructions/RefundInstructionsClient.cs">GetAsync</a>(GetRefundInstructionsRequest { ... }) -> RefundInstruction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve the current status of a specific refund instruction.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.PayrocCloud.RefundInstructions.GetAsync(
    new GetRefundInstructionsRequest { RefundInstructionId = "a37439165d134678a9100ebba3b29597" }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetRefundInstructionsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

## Reporting Settlement

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">ListBatchesAsync</a>(ListReportingSettlementBatchesRequest { ... }) -> PayrocPager<Batch></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve batch data for a specific date.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Reporting.Settlement.ListBatchesAsync(
    new ListReportingSettlementBatchesRequest
    {
        Before = "2571",
        After = "8516",
        Date = "2027-07-02",
        MerchantId = "4525644354",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListReportingSettlementBatchesRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">GetBatchAsync</a>(GetBatchSettlementRequest { ... }) -> Batch</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a specific batch.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Reporting.Settlement.GetBatchAsync(new GetBatchSettlementRequest { BatchId = 1 });
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetBatchSettlementRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">ListTransactionsAsync</a>(ListReportingSettlementTransactionsRequest { ... }) -> PayrocPager<Transaction></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a list of transactions.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Reporting.Settlement.ListTransactionsAsync(
    new ListReportingSettlementTransactionsRequest
    {
        Before = "2571",
        After = "8516",
        Date = "2024-07-01",
        BatchId = 1,
        MerchantId = "4525644354",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListReportingSettlementTransactionsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">GetTransactionAsync</a>(GetTransactionSettlementRequest { ... }) -> Transaction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a specific transaction.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Reporting.Settlement.GetTransactionAsync(
    new GetTransactionSettlementRequest { TransactionId = 1 }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetTransactionSettlementRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">ListAuthorizationsAsync</a>(ListReportingSettlementAuthorizationsRequest { ... }) -> PayrocPager<Authorization></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a list of authorizations.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Reporting.Settlement.ListAuthorizationsAsync(
    new ListReportingSettlementAuthorizationsRequest
    {
        Before = "2571",
        After = "8516",
        Date = "2024-07-01",
        BatchId = 1,
        MerchantId = "4525644354",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListReportingSettlementAuthorizationsRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">GetAuthorizationAsync</a>(GetAuthorizationSettlementRequest { ... }) -> Authorization</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a specific authorization.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Reporting.Settlement.GetAuthorizationAsync(
    new GetAuthorizationSettlementRequest { AuthorizationId = 1 }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetAuthorizationSettlementRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">ListDisputesAsync</a>(ListReportingSettlementDisputesRequest { ... }) -> PayrocPager<Dispute></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a list of disputes.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Reporting.Settlement.ListDisputesAsync(
    new ListReportingSettlementDisputesRequest
    {
        Before = "2571",
        After = "8516",
        Date = "2024-07-02",
        MerchantId = "4525644354",
    }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListReportingSettlementDisputesRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">GetDisputesStatusesAsync</a>(GetDisputesStatusesSettlementRequest { ... }) -> IEnumerable<DisputeStatus></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve the status history for a specific dispute.

</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Reporting.Settlement.GetDisputesStatusesAsync(
    new GetDisputesStatusesSettlementRequest { DisputeId = 1 }
);
```

</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetDisputesStatusesSettlementRequest`

</dd>
</dl>
</dd>
</dl>

</dd>
</dl>
</details>

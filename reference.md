# Reference
## Payment links
<details><summary><code>client.PaymentLinks.<a href="/src/Payroc/PaymentLinks/PaymentLinksClient.cs">ListAsync</a>(ListPaymentLinksRequest { ... }) -> PayrocPager<PaymentLinkPaginatedListDataItem></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payment links linked to a processing terminal.  

**Note:** If you want to view the details of a specific payment link and you have its paymentLinkId, use our [Retrieve Payment Link](https://docs.payroc.com/api/schema/payments/payment-links/retrieve) method.  

Use query parameters to filter the list of results that we return, for example, to search for only active links or multi-use links.  

Our gateway returns the following information about each payment link in the list:  
- **type** - Indicates whether the link can be used only once or if it can be used multiple times.  
- **authType** - Indicates whether the transaction is a sale or a pre-authorization.  
- **paymentMethods** - Indicates the payment method that the merchant accepts.  
- **charge** - Indicates whether the merchant or the customer enters the amount for the transaction.  
- **status** - Indicates if the payment link is active.  

For each payment link, we also return a paymentLinkId, which you can use for follow-on actions. 
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
await client.PaymentLinks.ListAsync(
    new ListPaymentLinksRequest
    {
        ProcessingTerminalId = "1234001",
        MerchantReference = "LinkRef6543",
        LinkType = ListPaymentLinksRequestLinkType.MultiUse,
        ChargeType = ListPaymentLinksRequestChargeType.Preset,
        Status = ListPaymentLinksRequestStatus.Active,
        RecipientName = "Sarah Hazel Hopper",
        RecipientEmail = "sarah.hopper@example.com",
        CreatedOn = new DateOnly(2024, 7, 2),
        ExpiresOn = new DateOnly(2024, 8, 2),
        Before = "2571",
        After = "8516",
        Limit = 1,
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

**request:** `ListPaymentLinksRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.PaymentLinks.<a href="/src/Payroc/PaymentLinks/PaymentLinksClient.cs">CreateAsync</a>(CreatePaymentLinksRequest { ... }) -> CreatePaymentLinksResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create a payment link that a customer can use to make a payment for goods or services.  

The request includes the following settings:
- **type** - Indicates whether the link can be used only once or if it can be used multiple times.
- **authType** - Indicates whether the transaction is a sale or a pre-authorization.
- **paymentMethod** - Indicates the payment methods that the merchant accepts.
- **charge** - Indicates whether the merchant or the customer enters the amount for the transaction.  

If your request is successful, our gateway returns a paymentLinkId, which you can use to perform follow-on actions.  

**Note:** To share the payment link with a customer, use our [Share Payment Link](https://docs.payroc.com/api/schema/payments/payment-links/sharing-events/share) method.
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
await client.PaymentLinks.CreateAsync(
    new CreatePaymentLinksRequest
    {
        ProcessingTerminalId = "1234001",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new CreatePaymentLinksRequestBody(
            new CreatePaymentLinksRequestBody.MultiUse(
                new MultiUsePaymentLink
                {
                    MerchantReference = "LinkRef6543",
                    Order = new MultiUsePaymentLinkOrder
                    {
                        Charge = new MultiUsePaymentLinkOrderCharge(
                            new MultiUsePaymentLinkOrderCharge.Prompt(
                                new PromptPaymentLinkCharge { Currency = Currency.Aed }
                            )
                        ),
                    },
                    AuthType = MultiUsePaymentLinkAuthType.Sale,
                    PaymentMethods = new List<MultiUsePaymentLinkPaymentMethodsItem>()
                    {
                        MultiUsePaymentLinkPaymentMethodsItem.Card,
                    },
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

**request:** `CreatePaymentLinksRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.PaymentLinks.<a href="/src/Payroc/PaymentLinks/PaymentLinksClient.cs">RetrieveAsync</a>(RetrievePaymentLinksRequest { ... }) -> RetrievePaymentLinksResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a payment link.  

To retrieve a payment link, you need its paymentLinkId. Our gateway returned the paymentLinkId in the response of the [Create Payment Link](https://docs.payroc.com/api/schema/payments/payment-links/create) method.  

**Note:** If you don't have the paymentLinkId, use our [List Payment Links](https://docs.payroc.com/api/schema/payments/payment-links/list) method to search for the payment link.  

Our gateway returns the following information about the payment link:  
- **type** - Indicates whether the link can be used only once or if it can be used multiple times.  
- **authType** - Indicates whether the transaction is a sale or a pre-authorization.  
- **paymentMethods** - Indicates the payment method that the merchant accepts.  
- **charge** - Indicates whether the merchant or the customer enters the amount for the transaction.
- **status** - Indicates if the payment link is active.  
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
await client.PaymentLinks.RetrieveAsync(
    new RetrievePaymentLinksRequest { PaymentLinkId = "JZURRJBUPS" }
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

**request:** `RetrievePaymentLinksRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.PaymentLinks.<a href="/src/Payroc/PaymentLinks/PaymentLinksClient.cs">PartiallyUpdateAsync</a>(PartiallyUpdatePaymentLinksRequest { ... }) -> PartiallyUpdatePaymentLinksResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to partially update a payment link. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.

To update a payment link, you need its paymentLinkId, which we sent you in the response of the [Create Payment Link](https://docs.payroc.com/api/schema/payments/payment-links/create) method.  

**Note:** If you don't have the paymentLinkId, use our [List Payment Links](https://docs.payroc.com/api/schema/payments/payment-links/list) method to search for the payment link.  

You can update the following properties of a multi-use link:
- **expiresOn parameter** - Expiration date of the link.
- **customLabels object** - Label for the payment button.
- **credentialOnFile object** - Settings for saving the customer's payment details.

You can update the following properties of a single-use link:
- **expiresOn parameter** - Expiration date of the link.
- **authType parameter** - Transaction type of the payment link.
- **amount parameter** - Total amount of the transaction.
- **currency parameter** - Currency of the transaction.
- **description parameter** - Brief description of the transaction.
- **customLabels object** - Label for the payment button.
- **credentialOnFile object** - Settings for saving the customer's payment details.

**Note:** When a merchant updates a single-use link, we update the payment URL and HTML code in the assets object. The customer can't use the original link to make a payment.
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
await client.PaymentLinks.PartiallyUpdateAsync(
    new PartiallyUpdatePaymentLinksRequest
    {
        PaymentLinkId = "JZURRJBUPS",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new List<PatchDocument>()
        {
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

**request:** `PartiallyUpdatePaymentLinksRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.PaymentLinks.<a href="/src/Payroc/PaymentLinks/PaymentLinksClient.cs">DeactivateAsync</a>(DeactivatePaymentLinksRequest { ... }) -> DeactivatePaymentLinksResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to deactivate a payment link.  

To deactivate a payment link, you need its paymentLinkId. Our gateway returned the paymentLinkId in the response of the [Create Payment Link](https://docs.payroc.com/api/schema/payments/payment-links/create) method.  

**Note:** If you don't have the paymentLinkId, use our [List Payment Links](https://docs.payroc.com/api/schema/payments/payment-links/list) method to search for the payment link.  

If your request is successful, our gateway deactivates the payment link. The customer can't use the link to make a payment, and you can't reactivate the payment link.    
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
await client.PaymentLinks.DeactivateAsync(
    new DeactivatePaymentLinksRequest { PaymentLinkId = "JZURRJBUPS" }
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

**request:** `DeactivatePaymentLinksRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Hosted Fields
<details><summary><code>client.HostedFields.<a href="/src/Payroc/HostedFields/HostedFieldsClient.cs">CreateAsync</a>(HostedFieldsCreateSessionRequest { ... }) -> HostedFieldsCreateSessionResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create a Hosted Fields session token. You need to generate a new session token each time you load Hosted Fields on a webpage.  

In your request, you need to indicate whether the merchant is using Hosted Fields to run a sale, save payment details, or update saved payment details.  

In the response, our gateway returns the session token and the time that it expires. You need the session token when you configure the JavaScript for Hosted Fields.  

For more information about adding Hosted Fields to a webpage, go to [Hosted Fields](https://docs.payroc.com/guides/integrate/hosted-fields). 
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
await client.HostedFields.CreateAsync(
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

## ApplePaySessions
<details><summary><code>client.ApplePaySessions.<a href="/src/Payroc/ApplePaySessions/ApplePaySessionsClient.cs">CreateAsync</a>(ApplePaySessions { ... }) -> ApplePayResponseSession</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to start an Apple Pay session for your merchant.  

In the response, we return the startSessionObject that you send to Apple when you retrieve the cardholder's encrypted payment details.  

**Note:** For more information about how to integrate with Apple Pay, go to [Apple Pay](https://docs.payroc.com/guides/integrate/apple-pay).
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
await client.ApplePaySessions.CreateAsync(
    new ApplePaySessions
    {
        ProcessingTerminalId = "1234001",
        AppleDomainId = "DUHDZJHGYY",
        AppleValidationUrl = "https://apple-pay-gateway.apple.com/paymentservices/startSession",
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

**request:** `ApplePaySessions` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Auth
<details><summary><code>client.Auth.<a href="/src/Payroc/Auth/AuthClient.cs">RetrieveTokenAsync</a>(RetrieveTokenAuthRequest { ... }) -> GetTokenResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Obtain an access token using client credentials
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
await client.Auth.RetrieveTokenAsync(
    new RetrieveTokenAuthRequest
    {
        ApiKey = "x-api-key",
        ClientId = "client_id",
        ClientSecret = "client_secret",
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

**request:** `RetrieveTokenAuthRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## BankTransferPayments Payments
<details><summary><code>client.BankTransferPayments.Payments.<a href="/src/Payroc/BankTransferPayments/Payments/PaymentsClient.cs">ListAsync</a>(ListPaymentsRequest { ... }) -> PayrocPager<BankTransferPayment></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payments.  

**Note:** If you want to view the details of a specific payment and you have its paymentId, use our [Retrieve Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/retrieve) method.  

Use query parameters to filter the list of results that we return, for example, to search for payments for a customer, a date range, or a settlement state.  

Our gateway returns the following information about each payment in the list:  

- Order details, including the transaction amount and when it was processed.  
- Bank account details, including the customer’s name and account number.  
- Customer's details, including the customer’s phone number.  
- Transaction details, including any refunds or re-presentments.  

For each transaction, we also return the paymentId and an optional secureTokenId, which you can use to perform follow-on actions.  
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
await client.BankTransferPayments.Payments.ListAsync(
    new Payroc.BankTransferPayments.Payments.ListPaymentsRequest
    {
        ProcessingTerminalId = "1234001",
        OrderId = "OrderRef6543",
        NameOnAccount = "Sarah%20Hazel%20Hopper",
        Last4 = "7890",
        DateFrom = new DateTime(2024, 07, 01, 00, 00, 00, 000),
        DateTo = new DateTime(2024, 07, 31, 23, 59, 59, 000),
        SettlementState = Payroc
            .BankTransferPayments
            .Payments
            .ListPaymentsRequestSettlementState
            .Settled,
        SettlementDate = new DateOnly(2024, 7, 15),
        PaymentLinkId = "JZURRJBUPS",
        Before = "2571",
        After = "8516",
        Limit = 1,
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

<details><summary><code>client.BankTransferPayments.Payments.<a href="/src/Payroc/BankTransferPayments/Payments/PaymentsClient.cs">CreateAsync</a>(BankTransferPaymentRequest { ... }) -> BankTransferPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to run a sale with a customer's bank account details.  

In the response, our gateway returns information about the bank transfer payment and a paymentId, which you need for the following methods:  
-	[Retrieve payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/retrieve) - View the details of the bank transfer payment.
-	[Reverse payment](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/reverse-payment) - Cancel the bank transfer payment if it's an open batch.
-	[Refund payment](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/create-referenced-refund) - Run a referenced refund to return funds to the customer's bank account.

**Payment methods**  

Our gateway accepts the following payment methods:  
-	Automated clearing house (ACH) details
-	Pre-authorized debit (PAD) details  

You can also use [secure tokens](https://docs.payroc.com/api/schema/payments/secure-tokens/overview) and [single-use tokens](https://docs.payroc.com/api/schema/tokenization/single-use-tokens/create) that you created from ACH details or PAD details. 
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
await client.BankTransferPayments.Payments.CreateAsync(
    new BankTransferPaymentRequest
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        ProcessingTerminalId = "1234001",
        Order = new BankTransferPaymentRequestOrder
        {
            OrderId = "OrderRef6543",
            Description = "Large Pepperoni Pizza",
            Amount = 4999,
            Currency = Currency.Usd,
            Breakdown = new BankTransferRequestBreakdown
            {
                Subtotal = 4347,
                Tip = new Tip { Type = TipType.Percentage, Percentage = 10 },
                Taxes = new List<TaxRate>()
                {
                    new TaxRate
                    {
                        Type = TaxRateType.Rate,
                        Rate = 5,
                        Name = "Sales Tax",
                    },
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
        CredentialOnFile = new SchemasCredentialOnFile { Tokenize = true },
        PaymentMethod = new BankTransferPaymentRequestPaymentMethod(
            new Payroc.BankTransferPayments.Payments.BankTransferPaymentRequestPaymentMethod.Ach(
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

<details><summary><code>client.BankTransferPayments.Payments.<a href="/src/Payroc/BankTransferPayments/Payments/PaymentsClient.cs">RetrieveAsync</a>(RetrievePaymentsRequest { ... }) -> BankTransferPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a bank transfer payment.  

To retrieve a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/create) method.  

Note: If you don’t have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/list) method to search for the payment.  

Our gateway returns the following information about the payment:  

-	Order details, including the transaction amount and when it was processed.  
-	Bank account details, including the customer’s name and account number.  
-	Customer’s details, including the customer’s phone number.  
-	Transaction details, including any refunds or re-presentments.  

If the merchant saved the customer’s bank account details, our gateway returns a secureTokenID, which you can use to perform follow-on actions.
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
await client.BankTransferPayments.Payments.RetrieveAsync(
    new Payroc.BankTransferPayments.Payments.RetrievePaymentsRequest { PaymentId = "M2MJOG6O2Y" }
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

**request:** `RetrievePaymentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.BankTransferPayments.Payments.<a href="/src/Payroc/BankTransferPayments/Payments/PaymentsClient.cs">RepresentAsync</a>(Representment { ... }) -> BankTransferPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to re-present an ACH payment.  

To re-present a payment, you need the paymentId of the return. To get the paymentId of the return, complete the following steps:  

1.	Use our [Retrieve Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/retrieve) method  to view the details of the original payment.  
2.	From the [returns object](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/retrieve#response.body.returns) in the response, get the paymentId of the return.  

Our gateway uses the bank account details from the original payment. If you want to update the customer's bank account details, send the new bank account details in the request.  

If your request is successful, our gateway re-presents the payment.
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
await client.BankTransferPayments.Payments.RepresentAsync(
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

## BankTransferPayments Refunds
<details><summary><code>client.BankTransferPayments.Refunds.<a href="/src/Payroc/BankTransferPayments/Refunds/RefundsClient.cs">ReversePaymentAsync</a>(ReversePaymentRefundsRequest { ... }) -> BankTransferPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to cancel a bank transfer payment in an open batch. This is also known as voiding a payment.  

To cancel a bank transfer payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/create) method.  

**Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/list) method to search for the bank transfer payment.  

If your request is successful, our gateway removes the bank transfer payment from the merchant’s open batch and no funds are taken from the customer's bank account.  
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
await client.BankTransferPayments.Refunds.ReversePaymentAsync(
    new ReversePaymentRefundsRequest
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

**request:** `ReversePaymentRefundsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.BankTransferPayments.Refunds.<a href="/src/Payroc/BankTransferPayments/Refunds/RefundsClient.cs">RefundAsync</a>(BankTransferReferencedRefund { ... }) -> BankTransferPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to refund a bank transfer payment that is in a closed batch.  

To refund a bank transfer payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/create) method.  

**Note:** If you don’t have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/list) method to search for the bank transfer payment.  

If your refund is successful, our gateway returns the payment amount to the customer's account.  

**Things to consider**  
- If the merchant refunds a bank transfer payment that is in an open batch, our gateway reverses the bank transfer payment.  
- Some merchants can run unreferenced refunds, which means that they don’t need a paymentId to return an amount to a customer. For more information about how to run an unreferenced refund, go to [Create Refund](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/create-unreferenced-refund).  
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
await client.BankTransferPayments.Refunds.RefundAsync(
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

<details><summary><code>client.BankTransferPayments.Refunds.<a href="/src/Payroc/BankTransferPayments/Refunds/RefundsClient.cs">ListAsync</a>(ListRefundsRequest { ... }) -> PayrocPager<BankTransferRefund></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of bank transfer refunds.  

**Note:** If you want to view the details of a specific refund and you have its refundId, use our [Retrieve Refund](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/retrieve) method.  

Use query parameters to filter the list of results that we return, for example, to search for refunds for a customer, an orderId, or a date range.  

Our gateway returns the following information about each refund in the list:  

-	Order details, including the refund amount and when it was processed.  
-	Bank account details, including the customer’s name and account number.  

For referenced refunds, our gateway also returns details about the payment that the refund is linked to.
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
await client.BankTransferPayments.Refunds.ListAsync(
    new Payroc.BankTransferPayments.Refunds.ListRefundsRequest
    {
        ProcessingTerminalId = "1234001",
        OrderId = "OrderRef6543",
        NameOnAccount = "Sarah%20Hazel%20Hopper",
        Last4 = "7062",
        DateFrom = new DateTime(2024, 07, 01, 00, 00, 00, 000),
        DateTo = new DateTime(2024, 07, 31, 23, 59, 59, 000),
        SettlementState = Payroc
            .BankTransferPayments
            .Refunds
            .ListRefundsRequestSettlementState
            .Settled,
        SettlementDate = new DateOnly(2024, 7, 15),
        Before = "2571",
        After = "8516",
        Limit = 1,
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

<details><summary><code>client.BankTransferPayments.Refunds.<a href="/src/Payroc/BankTransferPayments/Refunds/RefundsClient.cs">CreateAsync</a>(BankTransferUnreferencedRefund { ... }) -> BankTransferRefund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create an unreferenced refund. An unreferenced refund is a refund that isn’t linked to a bank transfer payment.  

**Note:** If you have the paymentId of the payment you want to refund, use our [Refund Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/create-referenced-refund) method. If you use our Refund Payment method, our gateway sends the refund amount to the customer’s original payment method and links the refund to the payment.  

In the request, you must provide the customer’s payment method and information about the order including the refund amount.  

In the response, our gateway returns information about the refund and a refundId, which you need for the following methods:  

-	[Retrieve refund](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/retrieve) – View the details of the refund.  
-	[Reverse refund](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/reverse-refund) – Cancel the refund if it’s in an open batch.  
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
await client.BankTransferPayments.Refunds.CreateAsync(
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
        RefundMethod = new BankTransferUnreferencedRefundRefundMethod(
            new Payroc.BankTransferPayments.Refunds.BankTransferUnreferencedRefundRefundMethod.Ach(
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

<details><summary><code>client.BankTransferPayments.Refunds.<a href="/src/Payroc/BankTransferPayments/Refunds/RefundsClient.cs">RetrieveAsync</a>(RetrieveRefundsRequest { ... }) -> BankTransferRefund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a refund.  

To retrieve a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/create-referenced-refund) method or the [Create Refund](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/create-unreferenced-refund) method.  

**Note:** If you don’t have the refundId, use our [List Refunds](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/list) method to search for the refund.  

Our gateway returns the following information about the refund:  

- Order details, including the refund amount and when it was processed.  
- Bank account details, including the customer’s name and account number.  

If the refund is a referenced refund, our gateway also returns details about the payment that the refund is linked to.
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
await client.BankTransferPayments.Refunds.RetrieveAsync(
    new Payroc.BankTransferPayments.Refunds.RetrieveRefundsRequest { RefundId = "CD3HN88U9F" }
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

**request:** `RetrieveRefundsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.BankTransferPayments.Refunds.<a href="/src/Payroc/BankTransferPayments/Refunds/RefundsClient.cs">ReverseRefundAsync</a>(ReverseRefundRefundsRequest { ... }) -> BankTransferRefund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to cancel a bank transfer refund in an open batch.  

To cancel a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/create-referenced-refund) or [Create Refund](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/create-unreferenced-refund) method.  

**Note:** If you don’t have the refundId, use our [List Refunds](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/list) method to search for the refund.  

If your request is successful, the gateway removes the refund from the merchant’s open batch, and no funds are returned to the cardholder’s account.  
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
await client.BankTransferPayments.Refunds.ReverseRefundAsync(
    new Payroc.BankTransferPayments.Refunds.ReverseRefundRefundsRequest
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

**request:** `ReverseRefundRefundsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Boarding Owners
<details><summary><code>client.Boarding.Owners.<a href="/src/Payroc/Boarding/Owners/OwnersClient.cs">RetrieveAsync</a>(RetrieveOwnersRequest { ... }) -> Owner</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve details about an owner of a processing account or an owner associated with a funding recipient.  

To retrieve an owner, you need their ownerId. Our gateway returned the ownerId in the response of the [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method or the [Create Funding Recipient Owner](https://docs.payroc.com/api/schema/funding/funding-recipients/create-owner) method.  

**Note:** If you don't have the ownerId, use the [Retrieve Processing Account](https://docs.payroc.com/api/schema/boarding/processing-accounts/retrieve) method if you are searching for a processing account owner, or use the [List Funding Recipient Owners](https://docs.payroc.com/api/schema/funding/funding-recipients/list-owners) method if you are searching for a funding recipient owner.  

Our gateway returns the following information about an owner:  
- Name, date of birth, and address.  
- Contact details, including their email address.  
- Relationship to the business, including whether they are a control prong or authorized signatory, and their equity stake in the business. 
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
await client.Boarding.Owners.RetrieveAsync(new RetrieveOwnersRequest { OwnerId = 1 });
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

**request:** `RetrieveOwnersRequest` 
    
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

> **Important:** You can't update the details of an owner of a processing account.  

Use this method to update the details of an owner associated with a funding recipient.  

To update an owner, you need their ownerId. Our gateway returned the ownerId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method and the [Create Funding Recipient Owner](https://docs.payroc.com/api/schema/funding/funding-recipients/create-owner) method.   

**Note:** If you don't have the ownerId, use the [List Funding Recipient Owners](https://docs.payroc.com/api/schema/funding/funding-recipients/list-owners) method, the [Retrieve Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/retrieve) method, or the [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient owner.  

You can update the following details about an owner:  

- Personal details, including their name, date of birth, and address.  
- Identification details, including their identification type and number.  
- Contact details, including their email address.  
- Relationship to the business, including whether they are a control prong.  
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
                new Identifier { Type = IdentifierType.NationalId, Value = "000-00-4320" },
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

> **Important:** You can't delete an owner of a processing account. 

Use this method to delete an owner associated with a funding recipient. You can delete an owner only if the funding recipient has more than one owner.  

To delete an owner, you need their ownerId. Our gateway returned the ownerId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method and the [Create Funding Recipient Owner](https://docs.payroc.com/api/schema/funding/funding-recipients/create-owner) method.  

**Note:** If you don't have the ownerId, use the [List Funding Recipient Owners](https://docs.payroc.com/api/schema/funding/funding-recipients/list-owners) method, the [Retrieve Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/retrieve) method, or the [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient owner.  
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

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of pricing intents associated with the ISV.  

**Note:** If you want to view the details of a specific pricing intent and you have its pricingIntentId, use our [Retrieve Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/retrieve) method.  

Our gateway returns the following information about each pricing intent in the list:  

- Information about the fees, including the base fees, gateway fees, and processor fees.  
- Status of the pricing intent, including whether we approved the pricing intent.  

For each pricing intent, we also return its pricingIntentId which you can use to perform follow-on actions.  
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
    new ListPricingIntentsRequest
    {
        Before = "2571",
        After = "8516",
        Limit = 1,
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

Use this method to create a pricing intent that you can assign to a processing account.  

In the request, you must provide the following:
-	Processing fees, including the pricing program and the fee to process each transaction.
-	Gateway fees, including the fee for each transaction handled by our gateway.
-	Base fees, including maintenance and PCI fees.

In the response, our gateway returns information about the pricing intent and the pricingIntentId, which you need for the following methods:
-	[Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) - Assign the pricing intent to a processing account, when you create the merchant platform and its processing accounts.
-	[Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) - Assign the pricing intent to a processing account.
-	[Retrieve Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/retrieve) - Retrieve information about a pricing intent.
-	[Update Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/update) - Update the details of a pricing intent. 
-	[Delete Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/delete) - Delete a pricing intent.
-	[Partially Update Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/partially-update) - Partially update the details of a pricing intent.
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
            Country = PricingAgreementUs50Country.Us,
            Version = PricingAgreementUs50Version.Five0,
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

<details><summary><code>client.Boarding.PricingIntents.<a href="/src/Payroc/Boarding/PricingIntents/PricingIntentsClient.cs">RetrieveAsync</a>(RetrievePricingIntentsRequest { ... }) -> PricingIntent50</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a pricing intent.  

To retrieve a pricing intent, you need its pricingIntentId. Our gateway returned the pricingIntentId in the response of the [Create Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/create) method.  

**Note:** If you don't have the pricingIntentId, use our [List Pricing Intents](https://docs.payroc.com/api/schema/boarding/pricing-intents/list) method to search for the pricing intent.  

Our gateway returns the following information about the pricing intent:  

- Information about the fees, including the base fees, gateway fees, and processor fees.  
- Status of the pricing intent, including whether we approved the pricing intent.  
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
await client.Boarding.PricingIntents.RetrieveAsync(
    new RetrievePricingIntentsRequest { PricingIntentId = "5" }
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

**request:** `RetrievePricingIntentsRequest` 
    
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

Use this method to update the details of a pricing intent. If you update a pricing intent, it won't affect merchant that you've previously onboarded.  

To update a pricing intent, you need its pricingIntentId. Our gateway returned the pricingIntentId in the response of the [Create Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/create) method.  

**Note:** If you don't have the pricingIntentId, use our [List Pricing Intents](https://docs.payroc.com/api/schema/boarding/pricing-intents/list) method to search for the pricing intent.  

You can update the following details about a pricing intent:  

- Fees, including the base fees, processor fees, and gateway fees.  
- Custom name for the pricing intent.  
- Additional services that merchants can sign up for.  
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
            Country = PricingAgreementUs50Country.Us,
            Version = PricingAgreementUs50Version.Five0,
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

Use this method to delete a pricing intent.  

> **Important:** When you delete a pricing intent, you can't recover it. You also won't be able to assign the pricing intent to a merchant's boarding application.  

To delete a pricing intent, you need its pricingIntentId. Our gateway returned the pricingIntentId in the response of the [Create Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/create) method.  

**Note:** If you don't have the pricingIntentId, use our [List Pricing Intents](https://docs.payroc.com/api/schema/boarding/pricing-intents/list) method to search for the pricing intent.
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

<details><summary><code>client.Boarding.PricingIntents.<a href="/src/Payroc/Boarding/PricingIntents/PricingIntentsClient.cs">PartiallyUpdateAsync</a>(PartiallyUpdatePricingIntentsRequest { ... }) -> PricingIntent50</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to partially update a pricing intent. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.  

If you update a pricing intent, it won't affect merchants you've previously onboarded.  

To update a pricing intent, you need its pricingIntentId. Our gateway returned the pricingIntentId in the response of the [Create Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/create) method.  

**Note:** If you don't have the pricingIntentId, use our [List Pricing Intents](https://docs.payroc.com/api/schema/boarding/pricing-intents/list) method to search for the pricing intent.  

You can update the following details about a pricing intent:  

- Fees, including the base fees, processor fees, and gateway fees.  
- Custom name for the pricing intent.  
- Additional services that merchants can sign up for.  
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
await client.Boarding.PricingIntents.PartiallyUpdateAsync(
    new PartiallyUpdatePricingIntentsRequest
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

**request:** `PartiallyUpdatePricingIntentsRequest` 
    
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

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of merchant platforms that are linked to your ISV account.  

**Note**: If you want to view the details of a specific merchant platform and you have its merchantPlatformId, use our [Retrieve Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/retrieve) method.  

Our gateway returns the following information about each merchant platform in the list:  
- Legal information, including its legal name and address.  
- Contact information, including the email address for the business.  
- Processing  account information, including the processingAccountId and status of each processing account that's linked to the merchant platform.  

For each merchant platform, we also return its merchantPlatformId and its linked processingAccountIds, which you can use to perform follow-on actions.  
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
    new ListMerchantPlatformsRequest
    {
        Before = "2571",
        After = "8516",
        Limit = 1,
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

Use this method to board a merchant with Payroc.  

**Note**: This method is part of our Boarding solution. To help you understand how this method works with other Boarding methods, go to [Board a Merchant](https://docs.payroc.com/guides/integrate/boarding).  

In the request, include the following information:  
- Legal information, including its legal name and address.  
- Contact information, including the email address for the business.  
- Processing account information, including the pricing model, owners, and contacts for the processing account.  

When you send a successful request, we review the merchant's information. After we complete our review and approve the merchant, we assign:  
- **merchantPlatformId** - Unique identifier for the merchant platform.  
- **processingAccountId** - Unique identifier for each processing account linked to the merchant platform.  

You need to keep these to perform follow-on actions, for example, you need the processingAccountId to [order terminals](https://docs.payroc.com/api/schema/boarding/processing-accounts/create-terminal-order) for the processing account.
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
            CountryOfOperation = BusinessCountryOfOperation.Us,
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
                    Type = AddressTypeType.LegalAddress,
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
                            new Identifier
                            {
                                Type = IdentifierType.NationalId,
                                Value = "000-00-4320",
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
                            new Identifier
                            {
                                Type = IdentifierType.NationalId,
                                Value = "000-00-4320",
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

<details><summary><code>client.Boarding.MerchantPlatforms.<a href="/src/Payroc/Boarding/MerchantPlatforms/MerchantPlatformsClient.cs">RetrieveAsync</a>(RetrieveMerchantPlatformsRequest { ... }) -> MerchantPlatform</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a merchant platform.  

To retrieve a merchant platform, you need its merchantPlatformId. Our gateway returned the merchantPlatformId in the response of the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method.  

**Note:** If you don't have the merchantPlatformId, use our [List Merchant Platforms](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list) method to search for the merchant platform.

Our gateway returns the following information about the merchant platform:
-	Legal information, including its legal name and address.
-	Contact information, including the email address for the business. 
-	Processing account information, including the processingAccountId and status of each processing account that's linked to the merchant platform.  
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
await client.Boarding.MerchantPlatforms.RetrieveAsync(
    new RetrieveMerchantPlatformsRequest { MerchantPlatformId = "12345" }
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

**request:** `RetrieveMerchantPlatformsRequest` 
    
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

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of processing accounts linked to a merchant platform.  

**Note**: If you want to view the details of a specific processing account and you have its processingAccountId, use our [Retrieve Processing Account](https://docs.payroc.com/api/schema/boarding/processing-accounts/retrieve) method.  

Use the query parameters to filter the list of results that we return, for example, to search for only closed processing accounts.  

To list the processing accounts for a merchant platform, you need its merchantPlatformId. If you don't have the merchantPlatformId, use our [List Merchant Platforms](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list) method to search for the merchant platform.

Our gateway returns the following information about eahc processing account in the list:  
- Business details, including its status, time zone, and address.  
- Owners' details, including their contact details.  
- Funding, pricing, and processing information, including its pricing model and funding accounts.  

For each processing account, we also return its processingAccountId, which you can use to perform follow-on actions.  
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
        Limit = 1,
        IncludeClosed = true,
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

Use this method to add an additional processing account to a merchant platform.  

To add a processing account to a merchant platform, you need the merchantPlatformId. Our gateway returned the merchantPlatformId in the response of the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method.  

**Note**: If you don't have the merchantPlatformId, use our [List Merchant Platforms](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list) method to search for the merchant platform.  

In the request, include the following information:  
- Business details, including its business type, time zone, and address.  
- Owners' details, including their contact details.  
- Funding, pricing, and processing information, including its pricing model and funding accounts.  

When you send a successful request, we review the information about the processing account. After we complete our review and approve the processing account, we assign a processingAccountId, which you need to perform follow-on actions.  

**Note**: You can subscribe to our processingAccount.status.changed event to get notifications when we update the status of a processing account. For more information about how to subscribe to events, go to [Events List](https://docs.payroc.com/knowledge/events/events-list).
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
                        new Identifier { Type = IdentifierType.NationalId, Value = "000-00-4320" },
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
                        new Identifier { Type = IdentifierType.NationalId, Value = "000-00-4320" },
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
<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">RetrieveAsync</a>(RetrieveProcessingAccountsRequest { ... }) -> ProcessingAccount</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a specific processing account.  

To retrieve a processing account, you need its processingAccountId. Our gateway returned the processingAccountId in the response of the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method or the [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method.  

**Note:** If you don't have the processingAccountId, use our [List Merchant Platform's Processing Accounts](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list-processing-accounts) method to search for the processing account.  

Our gateway returns the following information about the processing account:  

-	Business information, including the Merchant Category Code (MCC), status of the processing account, and address of the business.  
-	Processing information, including the merchant’s refund policies and card types that the merchant accepts.  
-	Funding information, including funding schedules, funding fees, and details for the merchant’s funding accounts.  
-	Pricing information, including [HATEOAS](https://docs.payroc.com/knowledge/basic-concepts/hypermedia-as-the-engine-of-application-state-hateoas) links to retrieve the pricing program for the processing account.  
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
await client.Boarding.ProcessingAccounts.RetrieveAsync(
    new RetrieveProcessingAccountsRequest { ProcessingAccountId = "38765" }
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

**request:** `RetrieveProcessingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">ListProcessingAccountFundingAccountsAsync</a>(ListProcessingAccountFundingAccountsRequest { ... }) -> IEnumerable<FundingAccount></code></summary>
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
await client.Boarding.ProcessingAccounts.ListProcessingAccountFundingAccountsAsync(
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

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">ListContactsAsync</a>(ListContactsProcessingAccountsRequest { ... }) -> PaginatedContacts</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a list of contacts for a processing account.    

**Note:** If you want to view the details of a specific contact and you have their contactId, use our [Retrieve Contact](https://docs.payroc.com/api/schema/boarding/contacts/retrieve) method.  

To list contacts for a processing account, you need the processingAccountId. Our gateway returned the processingAccountId in the response of the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method or the [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method.  

Our gateway returns the following information about each contact:  

- Name and contact method, including their phone number or mobile number.  
- Role within the business, for example, if they are a manager.  

For each contact, we also return a contactId, which you can use to perform follow-on actions.  
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
await client.Boarding.ProcessingAccounts.ListContactsAsync(
    new ListContactsProcessingAccountsRequest
    {
        ProcessingAccountId = "38765",
        Before = "2571",
        After = "8516",
        Limit = 1,
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

**request:** `ListContactsProcessingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">GetProcessingAccountPricingAgreementAsync</a>(GetProcessingAccountPricingAgreementProcessingAccountsRequest { ... }) -> OneOf<PricingAgreementUs40, PricingAgreementUs50></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve the pricing agreement that we apply to a processing account.  

To retrieve the pricing agreement of a processing account, you need the processingAccountId. Our gateway returned the processingAccountId in the response to the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method and [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method.  

**Note:** If you don't have the processingAccountId, use our [List Merchant Platform’s Processing Accounts](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list-processing-accounts) method to search for the processing account.  

Our gateway returns the following information about the pricing agreement that we apply to the processing account:  

- Base fees, including the annual fee and the fees for each chargeback and retrieval.  
- Processor fees, including the fees that we apply for processing card and ACH payments.  
- Gateway fees, including the setup fee and the fees for each transaction.  
- Service fees, including the fee that we apply if the merchant has signed up to a Hardware Advantage Plan.  
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
await client.Boarding.ProcessingAccounts.GetProcessingAccountPricingAgreementAsync(
    new GetProcessingAccountPricingAgreementProcessingAccountsRequest
    {
        ProcessingAccountId = "38765",
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

**request:** `GetProcessingAccountPricingAgreementProcessingAccountsRequest` 
    
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

Use this method to return a list of owners of a processing account.  

**Note:** If you want to view the details of a specific owner and you have the ownerId, go to our [Retrieve Owner](https://docs.payroc.com/api/schema/boarding/owners/retrieve) method.  

To list the owners of a processing account, you need its processingAccountId. If you don't have the processingAccountId, use our [List Merchant Platform's Processing Accounts](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list-processing-accounts) method to search for the processing account.  

Our gateway returns the following information about each owner in the list: 

- Name, date of birth, and address.  
- Contact details, including their email address.  
- Relationship to the business, including whether they are a control prong or authorized signatory, and their equity stake in the business.  
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
        Limit = 1,
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

Use this method to prompt a merchant to sign their pricing agreement.  

You can create a reminder only if you requested the merchant’s signature by email when you created the processing account for the merchant.  

To create a reminder, you need the processingAccountId. Our gateway returned the processingAccountId in the response of the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method or [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method. 

**Note:** If you don’t know the processingAccountId, use our [List Merchant Platform’s Processing Accounts](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list-processing-accounts) method to search for the processing account.  

When you send a successful request, we send an email to the merchant that prompts them to sign their pricing agreement.  
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
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
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

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">ListTerminalOrdersAsync</a>(ListTerminalOrdersProcessingAccountsRequest { ... }) -> IEnumerable<TerminalOrder></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of terminal orders associated with a processing account.  

**Note:** If you want to view the details of a specific terminal order and you have its terminalOrderId, use our [Retrieve Terminal Order](https://docs.payroc.com/api/schema/boarding/terminal-orders/retrieve) method.  

Use the query parameters to filter the list of results that we return, for example, to search for terminal orders by their status.  

To list the terminal orders for a processing account, you need its processingAccountId. If you don't have the processingAccountId, use our [List Merchant Platforms](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list) method to search for a merchant platform and its processing accounts.

Our gateway returns the following information for each terminal order in the list:  

- Status of the order  
- Items in the order  
- Training provider  
- Shipping information  

For each terminal order, we also return its terminalOrderId, which you can use to perform follow-on actions.   
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
await client.Boarding.ProcessingAccounts.ListTerminalOrdersAsync(
    new ListTerminalOrdersProcessingAccountsRequest
    {
        ProcessingAccountId = "38765",
        Status = ListTerminalOrdersProcessingAccountsRequestStatus.Open,
        FromDateTime = new DateTime(2024, 09, 08, 12, 00, 00, 000),
        ToDateTime = new DateTime(2024, 12, 08, 11, 00, 00, 000),
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

**request:** `ListTerminalOrdersProcessingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">CreateTerminalOrderAsync</a>(CreateTerminalOrder { ... }) -> TerminalOrder</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to order and configure terminals for a processing account.  

**Note**: You need the ID of the processing account before you can create an order. If you don't know the processingAccountId, go to the [Retrieve a Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/retrieve) method.  

In the request, specify the gateway settings, device settings, and application settings for the terminal.  

In the response, our gateway returns information about the terminal order including its status and terminalOrderId that you can use to [retrieve the terminal order](https://docs.payroc.com/api/schema/boarding/terminal-orders/retrieve).  

**Note**: You can subscribe to the terminalOrder.status.changed event to get notifications when we update the status of a terminal order. For more information about how to subscribe to events, go to [Events Subscriptions](https://docs.payroc.com/guides/integrate/event-subscriptions).  
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
await client.Boarding.ProcessingAccounts.CreateTerminalOrderAsync(
    new CreateTerminalOrder
    {
        ProcessingAccountId = "38765",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        TrainingProvider = TrainingProvider.Payroc,
        Shipping = new CreateTerminalOrderShipping
        {
            Preferences = new CreateTerminalOrderShippingPreferences
            {
                Method = CreateTerminalOrderShippingPreferencesMethod.NextDay,
                SaturdayDelivery = true,
            },
            Address = new CreateTerminalOrderShippingAddress
            {
                RecipientName = "Recipient Name",
                BusinessName = "Company Ltd",
                AddressLine1 = "1 Example Ave.",
                AddressLine2 = "Example Address Line 2",
                City = "Chicago",
                State = "Illinois",
                PostalCode = "60056",
                Email = "example@mail.com",
                Phone = "2025550164",
            },
        },
        OrderItems = new List<OrderItem>()
        {
            new OrderItem
            {
                Type = OrderItemType.Solution,
                SolutionTemplateId = "Roc Services_DX8000",
                SolutionQuantity = 1f,
                DeviceCondition = OrderItemDeviceCondition.New,
                SolutionSetup = new OrderItemSolutionSetup
                {
                    Timezone = SchemasTimezone.AmericaChicago,
                    IndustryTemplateId = "Retail",
                    GatewaySettings = new OrderItemSolutionSetupGatewaySettings
                    {
                        MerchantPortfolioId = "Company Ltd",
                        MerchantTemplateId = "Company Ltd Merchant Template",
                        UserTemplateId = "Company Ltd User Template",
                        TerminalTemplateId = "Company Ltd Terminal Template",
                    },
                    ApplicationSettings = new OrderItemSolutionSetupApplicationSettings
                    {
                        ClerkPrompt = false,
                        Security = new OrderItemSolutionSetupApplicationSettingsSecurity
                        {
                            RefundPassword = true,
                            KeyedSalePassword = false,
                            ReversalPassword = true,
                        },
                    },
                    DeviceSettings = new OrderItemSolutionSetupDeviceSettings
                    {
                        NumberOfMobileUsers = 2f,
                        CommunicationType =
                            OrderItemSolutionSetupDeviceSettingsCommunicationType.Wifi,
                    },
                    BatchClosure = new OrderItemSolutionSetupBatchClosure(
                        new OrderItemSolutionSetupBatchClosure.Automatic(new AutomaticBatchClose())
                    ),
                    ReceiptNotifications = new OrderItemSolutionSetupReceiptNotifications
                    {
                        EmailReceipt = true,
                        SmsReceipt = false,
                    },
                    Taxes = new List<OrderItemSolutionSetupTaxesItem>()
                    {
                        new OrderItemSolutionSetupTaxesItem
                        {
                            TaxRate = 6f,
                            TaxLabel = "Sales Tax",
                        },
                    },
                    Tips = new OrderItemSolutionSetupTips { Enabled = false },
                    Tokenization = true,
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

**request:** `CreateTerminalOrder` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">ListProcessingTerminalsAsync</a>(ListProcessingTerminalsProcessingAccountsRequest { ... }) -> PayrocPager<ProcessingTerminal></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of processing terminals associated with a processing account.  

**Note:** If you want to view the details of a specific processing terminal and you have its processingTerminalId, use our [Retrieve Processing Terminal](https://docs.payroc.com/api/schema/boarding/processing-terminals/retrieve) method.  

To list the terminals for a processing account, you need its processingAccountId. If you don't have the processingAccountId, use our [List Merchant Platforms](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list) method to search for a merchant platform and its processing accounts.   

Our gateway returns the following information for each processing terminal in the list:  

- Status indicating whether the terminal is active or inactive.  
- Configuration settings, including gateway settings and application settings.  
- Features, receipt settings, and security settings.  
- Devices that use the processing terminal's configuration.  

For each processing terminal, we also return its processingTerminalId, which you can use to perform follow-on actions.  
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
await client.Boarding.ProcessingAccounts.ListProcessingTerminalsAsync(
    new ListProcessingTerminalsProcessingAccountsRequest
    {
        ProcessingAccountId = "38765",
        Before = "2571",
        After = "8516",
        Limit = 1,
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

**request:** `ListProcessingTerminalsProcessingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Boarding ProcessingTerminals
<details><summary><code>client.Boarding.ProcessingTerminals.<a href="/src/Payroc/Boarding/ProcessingTerminals/ProcessingTerminalsClient.cs">RetrieveAsync</a>(RetrieveProcessingTerminalsRequest { ... }) -> ProcessingTerminal</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

**Important:** You can retrieve a processing terminal only if the terminal order was created using the Payroc API.

Use this method to retrieve information about a processing terminal.  

To retrieve a processing terminal, you need its processingTerminalId. Our gateway returned the processingTerminalId in the response of the [Create Terminal Order](https://docs.payroc.com/api/schema/boarding/processing-accounts/create-terminal-order) method.  

**Note:** If you don't have the processingTerminalId, use our [Retrieve Terminal Order](https://docs.payroc.com/api/schema/boarding/terminal-orders/retrieve) method or our [List Processing Terminals](https://docs.payroc.com/api/schema/boarding/processing-accounts/list-processing-terminals) method to search for the processing terminal.  

Our gateway returns the following information about the processing terminal:  

- Status indicating whether the terminal is active or inactive.  
- Configuration settings, including gateway settings and application settings.  
- Features, receipt settings, and security settings.  
- Devices that use the processing terminal's configuration.  
  
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
await client.Boarding.ProcessingTerminals.RetrieveAsync(
    new RetrieveProcessingTerminalsRequest { ProcessingTerminalId = "1234001" }
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

**request:** `RetrieveProcessingTerminalsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingTerminals.<a href="/src/Payroc/Boarding/ProcessingTerminals/ProcessingTerminalsClient.cs">RetrieveHostConfigurationAsync</a>(RetrieveHostConfigurationProcessingTerminalsRequest { ... }) -> HostConfiguration</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve the host processor configuration of a processing terminal. Integrate with this method only if you use your own gateway and want to validate the processor configuration.  

Our gateway returns the configuration settings for the merchant and the payment terminal.
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
await client.Boarding.ProcessingTerminals.RetrieveHostConfigurationAsync(
    new RetrieveHostConfigurationProcessingTerminalsRequest { ProcessingTerminalId = "1234001" }
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

**request:** `RetrieveHostConfigurationProcessingTerminalsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Boarding Contacts
<details><summary><code>client.Boarding.Contacts.<a href="/src/Payroc/Boarding/Contacts/ContactsClient.cs">RetrieveAsync</a>(RetrieveContactsRequest { ... }) -> Contact</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve details about a contact.  

To retrieve a contact, you need its contactId. Our gateway returned the contactId in the [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method.  

**Note:** If you don't have the contactId, use the [List Contacts](https://docs.payroc.com/api/schema/boarding/processing-accounts/list-contacts) method to search for the contact.  

Our gateway returns the following information about a contact:  

-	Name and contact method, including their phone number or mobile number.  
-	Role within the business, for example, if they are a manager. 
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
await client.Boarding.Contacts.RetrieveAsync(new RetrieveContactsRequest { ContactId = 1 });
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

**request:** `RetrieveContactsRequest` 
    
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

Use this method to update a contact of a processing account.  

To update a contact, you need its contactId. Our gateway returned the contactId in the response of the [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method.  

**Note:** If you don't have the contactId, use our [List Contacts](https://docs.payroc.com/api/schema/boarding/processing-accounts/list-contacts) method to search for the contact.  

You can update the following details about a contact:  

-	First name and last name.  
-	Contact details, including their phone number or mobile number.  
-	Identification details, including their identification type and number.  
-	Role within the business, for example, if they are a manager.  
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
                new Identifier { Type = IdentifierType.NationalId, Value = "000-00-4320" },
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

Use this method to delete a contact associated with a processing account.  

To delete a contact, you need their contactId. Our gateway returned the contactId in the response of the [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method.  

**Note:** If you don’t have the contactId, use our [Retrieve Processing Account](https://docs.payroc.com/api/schema/boarding/processing-accounts/retrieve) method or our [List Contacts](https://docs.payroc.com/api/schema/boarding/processing-accounts/list-contacts) method to search for the contact.  
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

## Boarding TerminalOrders
<details><summary><code>client.Boarding.TerminalOrders.<a href="/src/Payroc/Boarding/TerminalOrders/TerminalOrdersClient.cs">RetrieveAsync</a>(RetrieveTerminalOrdersRequest { ... }) -> TerminalOrder</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a terminal order.  

To retrieve a terminal order, you need it's terminalOrderId. Our gateway returned the terminalOrderId in the response of the [Create Terminal Order](https://docs.payroc.com/api/schema/boarding/processing-accounts/create-terminal-order) method.  

**Note**: If you don't have the terminalOrderId, use our [List Terminal Orders](https://docs.payroc.com/api/schema/boarding/processing-accounts/list-terminal-orders) method to search for the terminal order.  

Our gateway returns the following information about the terminal order:  
- Status of the order  
- Items in the order  
- Training provider  
- Shipping information  

**Note**: You can subscribe to our terminalOrder.status.changed event to get notifications when we update the status of a terminal order. For more information about how to subscribe to events, go to [Events Subscriptions](https://docs.payroc.com/guides/integrate/event-subscriptions).  
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
await client.Boarding.TerminalOrders.RetrieveAsync(
    new RetrieveTerminalOrdersRequest { TerminalOrderId = "12345" }
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

**request:** `RetrieveTerminalOrdersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## CardPayments Payments
<details><summary><code>client.CardPayments.Payments.<a href="/src/Payroc/CardPayments/Payments/PaymentsClient.cs">ListAsync</a>(ListPaymentsRequest { ... }) -> PayrocPager<RetrievedPayment></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payments. 

**Note:** If you want to view the details of a specific payment and you have its paymentId, use our [Retrieve Payment](https://docs.payroc.com/api/schema/card-payments/payments/retrieve) method.  

Use query parameters to filter the list of results that we return, for example, to search for payments for a customer, a tip mode, or a date range.  

Our gateway returns the following information about each payment in the list:  

- Order details, including the transaction amount and when it was processed.  
- Payment card details, including the masked card number, expiry date, and payment method. 
- Cardholder details, including their contact information and shipping address. 
- Payment details, including the payment type, status, and response. 
 
For each transaction, we also return the paymentId and an optional secureTokenId, which you can use to perform follow-on actions. 
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
await client.CardPayments.Payments.ListAsync(
    new Payroc.CardPayments.Payments.ListPaymentsRequest
    {
        ProcessingTerminalId = "1234001",
        OrderId = "OrderRef6543",
        Operator = "Jane",
        CardholderName = "Sarah%20Hazel%20Hopper",
        First6 = "453985",
        Last4 = "7062",
        Tender = ListPaymentsRequestTender.Ebt,
        DateFrom = new DateTime(2024, 07, 01, 15, 30, 00, 000),
        DateTo = new DateTime(2024, 07, 03, 15, 30, 00, 000),
        SettlementState = Payroc.CardPayments.Payments.ListPaymentsRequestSettlementState.Settled,
        SettlementDate = new DateOnly(2024, 7, 2),
        PaymentLinkId = "JZURRJBUPS",
        Before = "2571",
        After = "8516",
        Limit = 1,
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

<details><summary><code>client.CardPayments.Payments.<a href="/src/Payroc/CardPayments/Payments/PaymentsClient.cs">CreateAsync</a>(PaymentRequest { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to run a sale or a pre-authorization with a customer's payment card. 

In the response, our gateway returns information about the card payment and a paymentId, which you need for the following methods:

-	[Retrieve payment](https://docs.payroc.com/api/schema/card-payments/payments/retrieve) - View the details of the card payment.
-	[Adjust payment](https://docs.payroc.com/api/schema/card-payments/payments/adjust) - Update the details of the card payment.
-	[Capture payment](https://docs.payroc.com/api/schema/card-payments/payments/capture)  - Capture the pre-authorization.
-	[Reverse payment](https://docs.payroc.com/api/schema/card-payments/refunds/reverse)  - Cancel the card payment if it's in an open batch.
-	[Refund payment](https://docs.payroc.com/api/schema/card-payments/refunds/create-referenced-refund)  - Run a referenced refund to return funds to the payment card.

**Payment methods** 

- **Cards** - Credit, debit, and EBT
- **Digital wallets** - [Apple Pay®](https://docs.payroc.com/guides/integrate/apple-pay) and [Google Pay®](https://docs.payroc.com/guides/integrate/google-pay) 
- **Tokens** - Secure tokens and single-use tokens

**Features** 

Our Create Payment method also supports the following features: 

- [Repeat payments](https://docs.payroc.com/guides/integrate/repeat-payments/use-your-own-software) - Run multiple payments as part of a payment schedule that you manage with your own software. 
- **Offline sales** - Run a sale or a pre-authorization if the terminal loses its connection to our gateway. 
- [Tokenization](https://docs.payroc.com/guides/integrate/save-payment-details) - Save card details to use in future transactions. 
- [3-D Secure](https://docs.payroc.com/guides/integrate/3-d-secure) - Verify the identity of the cardholder. 
- [Custom fields](https://docs.payroc.com/guides/integrate/add-custom-fields) - Add your own data to a payment. 
- **Tips** - Add tips to the card payment.  
- **Taxes** - Add local taxes to the card payment. 
- **Surcharging** - Add a surcharge to the card payment. 
- **Dual pricing** - Offer different prices based on payment method, for example, if you use our RewardPay Choice pricing program. 
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
await client.CardPayments.Payments.CreateAsync(
    new PaymentRequest
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Channel = PaymentRequestChannel.Web,
        ProcessingTerminalId = "1234001",
        Operator = "Jane",
        Order = new PaymentOrderRequest
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
            new Payroc.CardPayments.Payments.PaymentRequestPaymentMethod.Card(
                new CardPayload
                {
                    CardDetails = new CardPayloadCardDetails(
                        new CardPayloadCardDetails.Raw(
                            new RawCardDetails
                            {
                                Device = new Device
                                {
                                    Model = DeviceModel.BbposChp,
                                    SerialNumber = "1850010868",
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

<details><summary><code>client.CardPayments.Payments.<a href="/src/Payroc/CardPayments/Payments/PaymentsClient.cs">RetrieveAsync</a>(RetrievePaymentsRequest { ... }) -> RetrievedPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a card payment.  

To retrieve a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/card-payments/payments/create) method.  

**Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/card-payments/payments/list) method to search for the payment.  

Our gateway returns the following information about the payment:  

- Order details, including the transaction amount and when it was processed.  
- Payment card details, including the masked card number, expiry date, and payment method.  
- Cardholder details, including their contact information and shipping address.  
- Payment details, including the payment type, status, and response.  

If the merchant saved the customer's card details, our gateway returns a secureTokenID, which you can use to perform follow-on actions.  
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
await client.CardPayments.Payments.RetrieveAsync(
    new Payroc.CardPayments.Payments.RetrievePaymentsRequest { PaymentId = "M2MJOG6O2Y" }
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

**request:** `RetrievePaymentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.CardPayments.Payments.<a href="/src/Payroc/CardPayments/Payments/PaymentsClient.cs">AdjustAsync</a>(PaymentAdjustment { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to adjust a payment in an open batch. 

To adjust a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/card-payments/payments/create) method.

**Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/card-payments/payments/list) method to search for the payment. 

You can adjust the following details of the payment:
- Sale amount and tip amount
- Payment status
- Cardholder shipping address and contact information
- Cardholder signature data

Our gateway returns information about the adjusted payment, including information about the payment card and the cardholder.
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
await client.CardPayments.Payments.AdjustAsync(
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
                new Payroc.CardPayments.Payments.PaymentAdjustmentAdjustmentsItem.Order(
                    new OrderAdjustment { Amount = 4999 }
                )
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

<details><summary><code>client.CardPayments.Payments.<a href="/src/Payroc/CardPayments/Payments/PaymentsClient.cs">CaptureAsync</a>(PaymentCapture { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to capture a pre-authorization. 

To capture a pre-authorization, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/card-payments/payments/create) method.

**Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/card-payments/payments/list) method to search for the payment.

Depending on the amount you want to capture, complete the following:
-	**Capture the full amount of the pre-authorization** - Don't send a value for the amount parameter in your request.
-	**Capture less than the amount of the pre-authorization** - Send a value for the amount parameter in your request. 
-	**Capture more than the amount of the pre-authorization** - Adjust the pre-authorization before you capture it. For more information about adjusting a pre-authorization, go to [Adjust Payment](https://docs.payroc.com/api/schema/card-payments/payments/adjust).

If your request is successful, our gateway takes the amount from the payment card. 

**Note:** For more information about pre-authorizations and captures, go to [Run a pre-authorization](https://docs.payroc.com/guides/integrate/run-a-pre-authorization).
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
await client.CardPayments.Payments.CaptureAsync(
    new PaymentCapture
    {
        PaymentId = "M2MJOG6O2Y",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        ProcessingTerminalId = "1234001",
        Operator = "Jane",
        Amount = 4999,
        Breakdown = new ItemizedBreakdownRequest
        {
            Subtotal = 4999,
            DutyAmount = 499,
            FreightAmount = 500,
            Items = new List<LineItemRequest>()
            {
                new LineItemRequest { UnitPrice = 4000, Quantity = 1 },
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

**request:** `PaymentCapture` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## CardPayments Refunds
<details><summary><code>client.CardPayments.Refunds.<a href="/src/Payroc/CardPayments/Refunds/RefundsClient.cs">ReverseAsync</a>(PaymentReversal { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to cancel or to partially cancel a payment in an open batch. This is also known as voiding a payment.  

To cancel a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/card-payments/payments/create) method.  

**Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/card-payments/payments/list) method to search for the payment.  

If your request is successful, our gateway removes the payment from the merchant's open batch and no funds are taken from the cardholder's account. 
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
await client.CardPayments.Refunds.ReverseAsync(
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

<details><summary><code>client.CardPayments.Refunds.<a href="/src/Payroc/CardPayments/Refunds/RefundsClient.cs">CreateReferencedRefundAsync</a>(ReferencedRefund { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to refund a payment that is in a closed batch.  

To refund a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/card-payments/payments/create) method.  

**Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/card-payments/payments/list) method to search for the payment.  

If your refund is successful, our gateway returns the payment amount to the cardholder's account.  

**Things to consider**  

- If the merchant refunds a payment that is in an open batch, our gateway reverses the payment.
- Some merchants can run unreferenced refunds, which means that they don't need a paymentId to return an amount to a customer. For more information about how to run an unreferenced refund, go to [Create Refund](https://docs.payroc.com/api/schema/card-payments/refunds/create-unreferenced-refund).
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
await client.CardPayments.Refunds.CreateReferencedRefundAsync(
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

<details><summary><code>client.CardPayments.Refunds.<a href="/src/Payroc/CardPayments/Refunds/RefundsClient.cs">ListAsync</a>(ListRefundsRequest { ... }) -> PayrocPager<RetrievedRefund></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of refunds.  

**Note:** If you want to view the details of a specific refund and you have its refundId, use our [Retrieve Refund](https://docs.payroc.com/api/schema/card-payments/refunds/retrieve) method.  

Use query parameters to filter the list of results that we return, for example, to search for refunds for a customer, a tender type, or a date range.
Our gateway returns the following information about each refund in the list:  
- Order details, including the refund amount and when we processed the refund.
- Payment card details, including the masked card number, expiry date, and payment method.
- Cardholder details, including their contact information and shipping address.  

For referenced refunds, our gateway also returns details about the payment that the refund is linked to.
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
await client.CardPayments.Refunds.ListAsync(
    new Payroc.CardPayments.Refunds.ListRefundsRequest
    {
        ProcessingTerminalId = "1234001",
        OrderId = "OrderRef6543",
        Operator = "Jane",
        CardholderName = "Sarah%20Hazel%20Hopper",
        First6 = "453985",
        Last4 = "7062",
        Tender = ListRefundsRequestTender.Ebt,
        DateFrom = new DateTime(2024, 07, 01, 15, 30, 00, 000),
        DateTo = new DateTime(2024, 07, 03, 15, 30, 00, 000),
        SettlementState = Payroc.CardPayments.Refunds.ListRefundsRequestSettlementState.Settled,
        SettlementDate = new DateOnly(2024, 7, 2),
        Before = "2571",
        After = "8516",
        Limit = 1,
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

<details><summary><code>client.CardPayments.Refunds.<a href="/src/Payroc/CardPayments/Refunds/RefundsClient.cs">CreateUnreferencedRefundAsync</a>(UnreferencedRefund { ... }) -> RetrievedRefund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create an unreferenced refund. An unreferenced refund is a refund that isn't linked to a payment.  

**Note:** If you have the paymentId of the payment you want to refund, use our [Refund Payment](https://docs.payroc.com/api/schema/card-payments/refunds/create-referenced-refund) method. If you use our Refund Payment method, our gateway sends the refund amount to the customer's original payment method and links the refund to the payment.  

In the request, you must provide the customer's payment details and the refund amount.  

In the response, our gateway returns information about the refund and a refundId, which you need for the following methods:  

- [Retrieve refund](https://docs.payroc.com/api/schema/card-payments/refunds/retrieve) - View the details of the refund.  
- [Adjust refund](https://docs.payroc.com/api/schema/card-payments/refunds/adjust) - Update the details of the refund.  
- [Reverse refund](https://docs.payroc.com/api/schema/card-payments/refunds/reverse-refund) - Cancel the refund if it's in an open batch.  
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
await client.CardPayments.Refunds.CreateUnreferencedRefundAsync(
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
            new Payroc.CardPayments.Refunds.UnreferencedRefundRefundMethod.Card(
                new CardPayload
                {
                    CardDetails = new CardPayloadCardDetails(
                        new CardPayloadCardDetails.Raw(
                            new RawCardDetails
                            {
                                Device = new Device
                                {
                                    Model = DeviceModel.BbposChp,
                                    SerialNumber = "1850010868",
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

<details><summary><code>client.CardPayments.Refunds.<a href="/src/Payroc/CardPayments/Refunds/RefundsClient.cs">RetrieveAsync</a>(RetrieveRefundsRequest { ... }) -> RetrievedRefund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a refund.  

To retrieve a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](https://docs.payroc.com/api/schema/card-payments/refunds/create-referenced-refund) method or the [Create Refund](https://docs.payroc.com/api/schema/card-payments/refunds/create-unreferenced-refund) method.  

**Note:** If you don't have the refundId, use our [List Refunds](https://docs.payroc.com/api/schema/card-payments/refunds/list) method to search for the refund.  

Our gateway returns the following information about the refund:  
- Order details, including the refund amount and when we processed the refund.
- Payment card details, including the masked card number, expiry date, and payment method.
- Cardholder details, including their contact information and shipping address.  

If the refund is a referenced refund, our gateway also returns details about the payment that the refund is linked to.
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
await client.CardPayments.Refunds.RetrieveAsync(
    new Payroc.CardPayments.Refunds.RetrieveRefundsRequest { RefundId = "CD3HN88U9F" }
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

**request:** `RetrieveRefundsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.CardPayments.Refunds.<a href="/src/Payroc/CardPayments/Refunds/RefundsClient.cs">AdjustAsync</a>(RefundAdjustment { ... }) -> RetrievedRefund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to adjust a refund in an open batch.  

To adjust a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](https://docs.payroc.com/api/schema/card-payments/refunds/create-referenced-refund) method or the [Create Refund](https://docs.payroc.com/api/schema/card-payments/refunds/create-unreferenced-refund) method.  

**Note:** If you don’t have the refundId, use our [List Refunds](https://docs.payroc.com/api/schema/card-payments/refunds/list) method to search for the refund.  

You can adjust the following details of the refund:
- Customer details, including shipping address and contact information.
- Status of the refund.  

Our gateway returns information about the adjusted refund, including:
- Order details, including the refund amount and when we processed the refund.
- Payment card details, including the masked card number, expiry date, and payment method.
- Cardholder details, including their contact information and shipping address.  

If the refund is a referenced refund, our gateway also returns details about the payment that the refund is linked to.
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
await client.CardPayments.Refunds.AdjustAsync(
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

<details><summary><code>client.CardPayments.Refunds.<a href="/src/Payroc/CardPayments/Refunds/RefundsClient.cs">ReverseRefundAsync</a>(ReverseRefundRefundsRequest { ... }) -> RetrievedRefund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to cancel a refund in an open batch.  

To cancel a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](https://docs.payroc.com/api/schema/card-payments/refunds/create-referenced-refund) or [Create Refund](https://docs.payroc.com/api/schema/card-payments/refunds/create-unreferenced-refund) method.  

**Note:** If you don’t have the refundId, use our [List Refunds](https://docs.payroc.com/api/schema/card-payments/refunds/list) method to search for the refund.  

If your request is successful, the gateway removes the refund from the merchant’s open batch and no funds are returned to the cardholder’s account.
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
await client.CardPayments.Refunds.ReverseRefundAsync(
    new Payroc.CardPayments.Refunds.ReverseRefundRefundsRequest
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

**request:** `ReverseRefundRefundsRequest` 
    
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

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of funding recipients linked to your account.  

Note: If you want to view the details of a specific funding recipient and you have its recipientId, use our [Retrieve Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/retrieve) method.  

Our gateway returns the following information about each funding recipient in the list:  
- Tax ID and Doing Business As (DBA) name.  
- Address and contact details.  
- Funding accounts linked to the funding recipient.  

For each funding recipient, we also return the recipientId, which you can use to perform follow-on actions.
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
    new ListFundingRecipientsRequest
    {
        Before = "2571",
        After = "8516",
        Limit = 1,
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

Use this method to create a funding recipient. 

A funding recipient is a business or organization that can receive funds but can't run transactions, for example, a charity.  

In the request, include the following information:  
-	Legal information, including its tax ID, Doing Business As (DBA) name, and address.  
-	Contact information, including the email address.  
-	Owners' details, including their contact details. 
-	Funding account details.  

Our gateway returns the recipientId of the funding recipient, which you can use to run follow-on actions. 
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
        TaxId = "123456789",
        DoingBusinessAs = "doingBusinessAs",
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
                DateOfBirth = new DateOnly(1964, 3, 22),
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
                    new Identifier { Type = IdentifierType.NationalId, Value = "xxxxx4320" },
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

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">RetrieveAsync</a>(RetrieveFundingRecipientsRequest { ... }) -> FundingRecipient</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a funding recipient.  

To retrieve a funding recipient, you need its recipientId. Our gateway returned the recipientId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method.  

**Note:** If you don't have the recipientId, use our [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient.  

Our gateway returns the following information about the funding recipient:  

- Tax ID and Doing Business As (DBA) name.  
- Address and contact details.  
- Funding accounts linked to the funding recipient.  
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
await client.Funding.FundingRecipients.RetrieveAsync(
    new RetrieveFundingRecipientsRequest { RecipientId = 1 }
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

**request:** `RetrieveFundingRecipientsRequest` 
    
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

Use this method to update the details of a funding recipient. If a request contains significant changes, we might need to re-approve the funding recipient.  

To update a funding recipient, you need it's recipientId. Our gateway returned the recipientId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method.  

**Note**: If you don't have the recipientId, use our [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient.  

You can update the following details of a funding recipient:  
- Doing Business As (DBA) name  
- Tax ID and charity ID  
- Address and contact methods  
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
            TaxId = "123456789",
            DoingBusinessAs = "doingBusinessAs",
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

Use this method to delete a funding recipient, including its funding accounts and owners.  

To delete a funding recipient, you need its recipientId. Our gateway returned the recipientId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method.   

**Note**: If you don't have the recipientId, use our [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient.  
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

Use  this method to return a list of funding accounts associated with a funding recipient.  

**Note:** If you want to view the details of a specific funding account and you have its fundingAccountId, use our [Retrieve Funding Account](https://docs.payroc.com/api/schema/funding/funding-accounts/retrieve) method.  

To retrieve the funding accounts associated with a funding recipient, you need the recipientId. If you don't have the recipientId, use our [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient.  
        
Our gateway returns the following information about each funding account:  
-	Name of the account holder.  
-	ACH details for the account.  
-	Status of the account.  

Our gateway also returns the fundingAccountId, which you can use to run follow-on actions.
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

Use this method to create a funding account and add it to a funding recipient.  

To add a funding account to a funding recipient, you need the recipientId. Our gateway returned the recipientId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method.  

**Note:** If you don't have the recipientId, use our [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient.  

In the request, include the following information:  
-	Account type, for example, if the account is a checking or savings account.  
-	Account holder's name.  
-	ACH information, including the routing number and account number of the account.  

Our gateway returns the fundingAccountId, which you can use to run follow-on actions.
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

Use this method to return a list of owners of a funding recipient.  

**Note:** If you want to view the details of a specific owner and you have their ownerId, use our [Retrieve Owner](https://docs.payroc.com/api/schema/boarding/owners/retrieve) method.  

To list the owners of a funding recipient, you need its recipientId. Our gateway returned the recipientId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method. If you don't have the recipientId, use our [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient.  

Our gateway returns the following information about each owner in the list:  
-	Name, date of birth, and address.  
-	Contact details, including their email address.  
-	Relationship to the funding recipient.  

Our gateway also returns the ownerId, which you can use to perform follow-on actions.  
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

Use this method to add an additional owner to a funding recipient.  

To add an owner to a funding recipient, you need the recipientId. Our gateway returned the recipientId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method.  

**Note:** If you don't have the recipientId, use our [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient.  

In the request, include the following information about the owner:  

- Name, date of birth, and address.  
- Contact details, including their email address.  
- Relationship to the funding recipient, including whether they are a control prong.  

In the response, our gateway returns the ownerId, which you can use to run follow-on actions.  
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
            DateOfBirth = new DateOnly(1964, 3, 22),
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
                new Identifier { Type = IdentifierType.NationalId, Value = "xxxxx4320" },
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

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of funding accounts associated with your account.  

**Note:** If you want to view the details of a specific funding account and you have its fundingAccountId, use our [Retrieve Funding Account](https://docs.payroc.com/api/schema/funding/funding-accounts/retrieve) method.  

Our gateway returns the following information about each funding account in the list:  
- Name of the account holder and ACH details for the account.  
- Status of the account.  
- Whether we send funds to the account, withdraw funds from the account, or both.  

For each funding account, we also return the fundingAccountId, which you can use to perform follow-on actions.  
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
    new ListFundingAccountsRequest
    {
        Before = "2571",
        After = "8516",
        Limit = 1,
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

**request:** `ListFundingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingAccounts.<a href="/src/Payroc/Funding/FundingAccounts/FundingAccountsClient.cs">RetrieveAsync</a>(RetrieveFundingAccountsRequest { ... }) -> FundingAccount</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a funding account.  

To retrieve a funding account, you need its fundingAccountId. Our gateway returned the fundingAccountId when you created the funding account.  

**Note:** If you don't have the fundingAccountId, use our [List Funding Accounts](https://docs.payroc.com/api/schema/funding/funding-accounts/list) method to search for the account.  

Our gateway returns the following information about the funding account:  
- Name of the account holder and ACH details for the account.  
- Status of the account.  
- Whether we send funds to the account, withdraw funds from the account, or both.  
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
await client.Funding.FundingAccounts.RetrieveAsync(
    new RetrieveFundingAccountsRequest { FundingAccountId = 1 }
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

**request:** `RetrieveFundingAccountsRequest` 
    
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

> **Important:** You can't update the details of a funding account that is associated with a processing account.  

Use this method to update the details of a funding account that is associated with a funding recipient.  

To update a funding account, you need its fundingAccountId. Our gateway returned the fundingAccountId when you created the funding account.  

**Note:** If you don’t have the fundingAccountId, use our [List Funding Accounts](https://docs.payroc.com/api/schema/funding/funding-accounts/list) method to search for the funding account.  

You can update the following details about the funding account: 
-	Account type. 
-	Account holder's name. 
-	ACH information for the account. 
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

> **Important:** You can't delete a funding account that is associated with a processing account.  

Use this method to delete a funding account that is associated with a funding recipient.  

To delete a funding account, you need its fundingAccountId. Our gateway returned the fundingAccountId when you created the funding account.  

**Note:** If you don't have the fundingAccountId, use our [List Funding Accounts](https://docs.payroc.com/api/schema/funding/funding-accounts/list) method to search for the funding account.  
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

> Important: You can return a list of funding instructions from only the previous two years. If you want to view a funding instruction from more than two years ago and you have its instructionId, use our [Retrieve Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/retrieve) method.  

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of funding instructions within a specific date range.  

**Note:** If you want to view the details of a specific funding instruction and you have its instructionId, use our [Retrieve Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/retrieve) method.  

Our gateway returns the following information for each instruction in the list:  
-	Status of the funding instruction.  
-	Funding information, including which merchant's funding balance we distribute and the funding account that we send the balance to.  

For each funding instruction, we also return the instructionId, which you can use to perform follow-on actions. 
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
        Limit = 1,
        DateFrom = new DateOnly(2024, 7, 1),
        DateTo = new DateOnly(2024, 7, 3),
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

Use this method to create a funding instruction that tells us how to distribute the funds from your merchants' transactions.  

**Note:** Before you create a funding instruction, you can use our [List Funding Balances](https://docs.payroc.com/api/schema/funding/funding-activity/retrieve-balance) method to view the amount of available funds that a merchant has.  

In your request, include an array of merchantInstruction objects. Each merchantInstruction object contains the following:  
-	Merchant ID (MID) of the merchant whose funding balance you want to distribute.  
-	Funding account that you want to send funds to.  
-	Amount that you want to send to the funding account.  

Our gateway returns the instructionId, which you can use to run follow-on actions.
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

<details><summary><code>client.Funding.FundingInstructions.<a href="/src/Payroc/Funding/FundingInstructions/FundingInstructionsClient.cs">RetrieveAsync</a>(RetrieveFundingInstructionsRequest { ... }) -> Instruction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a funding instruction.  

To retrieve a funding instruction, you need its instructionId. Our gateway returned the instructionId in the response of the [Create Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/create) method. 

**Note:** If you don't have the instructionId, use our [List Funding Instructions](https://docs.payroc.com/api/schema/funding/funding-instructions/list) method to search for the funding instruction.  

Our gateway returns the following information about the funding instruction:  
-	Status of the funding instruction.  
-	Funding information, including which merchant's funding balance we distribute and the funding account that we send the balance to.  
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
await client.Funding.FundingInstructions.RetrieveAsync(
    new RetrieveFundingInstructionsRequest { InstructionId = 1 }
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

**request:** `RetrieveFundingInstructionsRequest` 
    
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

> **Important:** You can update a funding instruction only if its status is `accepted`. To view the status of a funding instruction, use our [Retrieve Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/retrieve) method. 

Use this method to update the details of a funding instruction.  

To update a funding instruction, you need its instructionId. Our gateway returned the instructionId in the response of the [Create Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/create) method.  

**Note:** If you don't have the fundingInstructionId, use our [List Funding Instructions](https://docs.payroc.com/api/schema/funding/funding-instructions/list) method to search for the funding instruction.  

You can modify the following information for the funding instruction:  
-	Merchant ID (MID) of the merchant whose funding balance you want to distribute.  
-	Funding account that you want to send funds to.  
-	Amount that you want to send to the funding account.  
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

> **Important:** You can delete a funding instruction only if its status is `accepted`. To view the status of a funding instruction, use our [Retrieve Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/retrieve) method.  

Use this method to delete a funding instruction.  

To delete a funding instruction, you need its instructionId. Our gateway returned the instructionId in the response of the [Create Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/create) method.  

**Note:** If you don't have the instructionId, use our [List Funding Instructions](https://docs.payroc.com/api/schema/funding/funding-instructions/list) method to search for the funding instruction.
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
<details><summary><code>client.Funding.FundingActivity.<a href="/src/Payroc/Funding/FundingActivity/FundingActivityClient.cs">RetrieveBalanceAsync</a>(RetrieveBalanceFundingActivityRequest { ... }) -> RetrieveBalanceFundingActivityResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of funding balances available for each merchant linked to your account.  

Use query parameters to filter the list of results we return, for example, to search for the funding balance for a specific merchant.  

Our gateway returns the following information about each merchant in the list:  
- Total funds for the merchant.  
- Available funds that you can use for funding instructions.  
- Pending funds that we have not yet sent to funding accounts.  
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
await client.Funding.FundingActivity.RetrieveBalanceAsync(
    new RetrieveBalanceFundingActivityRequest
    {
        Before = "2571",
        After = "8516",
        Limit = 1,
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

**request:** `RetrieveBalanceFundingActivityRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingActivity.<a href="/src/Payroc/Funding/FundingActivity/FundingActivityClient.cs">ListAsync</a>(ListFundingActivityRequest { ... }) -> PayrocPager<ActivityRecord></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of activity associated with your merchants' funding balances within a specific date range.  

Use query parameters to filter the list of results we return, for example, to view the activity for a specific merchant's funding balance.  

Our gateway returns the following information about each activity in the list:
- Name of the merchant who owns the funding balance.
-	Amount of funds added or removed from the funding balance.
-	Funding account that received funds from the funding balance.
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
await client.Funding.FundingActivity.ListAsync(
    new ListFundingActivityRequest
    {
        Before = "2571",
        After = "8516",
        Limit = 1,
        DateFrom = new DateOnly(2024, 7, 2),
        DateTo = new DateOnly(2024, 7, 3),
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

**request:** `ListFundingActivityRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Notifications EventSubscriptions
<details><summary><code>client.Notifications.EventSubscriptions.<a href="/src/Payroc/Notifications/EventSubscriptions/EventSubscriptionsClient.cs">ListAsync</a>(ListEventSubscriptionsRequest { ... }) -> PayrocPager<EventSubscription></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of event subscriptions that are linked to your ISV account.  

**Note:** If you want to view the details of a specific event subscription and you have its id, use our [Retrieve Event Subscription](https://docs.payroc.com/api/schema/notifications/event-subscriptions/retrieve) method.  

Use query parameters to filter the list of results that we return, for example, to search for subscriptions with a specific status or an event type.  

Our gateway returns the following information about each subscription in the list:  
- Event types that you have subscribed to.  
- Whether you have enabled notifications for the subscription.  
- How we contact you when an event occurs, including the endpoint that send notifications to.  
- If there are any issues when we try to send you a notification, for example, if we can't contact your endpoint.  

For each event subscription, we also return its id, which you can use to perform follow-on actions.  
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
await client.Notifications.EventSubscriptions.ListAsync(
    new ListEventSubscriptionsRequest
    {
        Status = ListEventSubscriptionsRequestStatus.Registered,
        Event = "processingAccount.status.changed",
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

**request:** `ListEventSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Notifications.EventSubscriptions.<a href="/src/Payroc/Notifications/EventSubscriptions/EventSubscriptionsClient.cs">CreateAsync</a>(CreateEventSubscriptionsRequest { ... }) -> EventSubscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create an event subscription that we use to notify you when an event occurs, for example, when we change the status of a processing account.  

In the request, include the events that you want to subscribe to and the public endpoint that we send event notifications to. For a complete list of events that you can subscribe to, go to [Events List](https://docs.payroc.com/knowledge/events/events-list).  

In the response, our gateway returns the id of the event subscription, which you can use to perform follow-on actions.  
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
await client.Notifications.EventSubscriptions.CreateAsync(
    new CreateEventSubscriptionsRequest
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new EventSubscription
        {
            Enabled = true,
            EventTypes = new List<string>() { "processingAccount.status.changed" },
            Notifications = new List<Notification>()
            {
                new Notification(
                    new Notification.Webhook(
                        new Webhook
                        {
                            Uri = "https://my-server/notification/endpoint",
                            Secret = "aBcD1234eFgH5678iJkL9012mNoP3456",
                            SupportEmailAddress = "supportEmailAddress",
                        }
                    )
                ),
            },
            Metadata = new Dictionary<string, object?>() { { "yourCustomField", "abc123" } },
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

**request:** `CreateEventSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Notifications.EventSubscriptions.<a href="/src/Payroc/Notifications/EventSubscriptions/EventSubscriptionsClient.cs">RetrieveAsync</a>(RetrieveEventSubscriptionsRequest { ... }) -> EventSubscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve the details of an event subscription.  

In your request, include the subscriptionId that we sent to you when we created the event subscription.  
  
**Note:** If you don't know the subscriptionId of the event subscription, go to [List event subscriptions](#listEventSubscriptions).
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
await client.Notifications.EventSubscriptions.RetrieveAsync(
    new RetrieveEventSubscriptionsRequest { SubscriptionId = 1 }
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

**request:** `RetrieveEventSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Notifications.EventSubscriptions.<a href="/src/Payroc/Notifications/EventSubscriptions/EventSubscriptionsClient.cs">UpdateAsync</a>(UpdateEventSubscriptionsRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to update the details of an event subscription.  

To update an event subscription, you need its subscriptionId. Our gateway returned the subscriptionId in the response of the [Create Event Subscription](https://docs.payroc.com/api/schema/notifications/event-subscriptions/create) method.  

**Note:** If you don’t have the subscriptionId, use our [List Event Subscriptions](https://docs.payroc.com/api/schema/notifications/event-subscriptions/list) method to search for the event subscription.  

You can update the following details about an event subscription:  

- Status of the event subscription.  
- Events that you have subscribed to. For a list of events that you can subscribe to, go to [Events list](https://docs.payroc.com/knowledge/events/events-list).  
- Information about how we contact you when an event occurs.  
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
await client.Notifications.EventSubscriptions.UpdateAsync(
    new UpdateEventSubscriptionsRequest
    {
        SubscriptionId = 1,
        Body = new EventSubscription
        {
            Enabled = true,
            EventTypes = new List<string>() { "processingAccount.status.changed" },
            Notifications = new List<Notification>()
            {
                new Notification(
                    new Notification.Webhook(
                        new Webhook
                        {
                            Uri = "https://my-server/notification/endpoint",
                            Secret = "aBcD1234eFgH5678iJkL9012mNoP3456",
                            SupportEmailAddress = "supportEmailAddress",
                        }
                    )
                ),
            },
            Metadata = new Dictionary<string, object?>() { { "yourCustomField", "abc123" } },
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

**request:** `UpdateEventSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Notifications.EventSubscriptions.<a href="/src/Payroc/Notifications/EventSubscriptions/EventSubscriptionsClient.cs">DeleteAsync</a>(DeleteEventSubscriptionsRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to delete an event subscription.  

> **Important:** After you delete an event subscription, you can’t recover it. You won't receive event notifications from the event subscription.  

To delete an event subscription, you need its subscriptionId. Our gateway returned the subscriptionId in the response of the [Create Event Subscription](https://docs.payroc.com/api/schema/notifications/event-subscriptions/create) method.  

If you want to stop receiving event notifications but don't want to delete the event subscription, use our [Update Event Subscription](https://docs.payroc.com/api/schema/notifications/event-subscriptions/update) method to deactivate it.  
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
await client.Notifications.EventSubscriptions.DeleteAsync(
    new DeleteEventSubscriptionsRequest { SubscriptionId = 1 }
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

**request:** `DeleteEventSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Notifications.EventSubscriptions.<a href="/src/Payroc/Notifications/EventSubscriptions/EventSubscriptionsClient.cs">PartiallyUpdateAsync</a>(PartiallyUpdateEventSubscriptionsRequest { ... }) -> EventSubscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to partially update an event subscription. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.  

To update an event subscription, you need its subscriptionId. Our gateway returned the subscriptionId in the id field in the response of the [Create Event Subscription](https://docs.payroc.com/api/schema/notifications/event-subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List Event Subscriptions](https://docs.payroc.com/api/schema/notifications/event-subscriptions/list) method to search for the subscription.  

You can update the following properties of an event subscription:  
- **eventTypes** - Subscribe to new events or remove events that you are subscribed to.  
- **notifications** - Information about your endpoint and who we email if we can't contact your endpoint.  
- **enabled** - Turn on or turn off notifications for the subscription.  
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
await client.Notifications.EventSubscriptions.PartiallyUpdateAsync(
    new PartiallyUpdateEventSubscriptionsRequest
    {
        SubscriptionId = 1,
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new List<PatchDocument>()
        {
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

**request:** `PartiallyUpdateEventSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## PaymentFeatures Cards
<details><summary><code>client.PaymentFeatures.Cards.<a href="/src/Payroc/PaymentFeatures/Cards/CardsClient.cs">VerifyCardAsync</a>(CardVerificationRequest { ... }) -> CardVerificationResult</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to verify a customer’s card details.  

In the request, send the customer’s card details.  

In the response, our gateway indicates if the card details are valid and if you should use them in follow-on actions.  
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
await client.PaymentFeatures.Cards.VerifyCardAsync(
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
                                    SerialNumber = "1850010868",
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

<details><summary><code>client.PaymentFeatures.Cards.<a href="/src/Payroc/PaymentFeatures/Cards/CardsClient.cs">ViewEbtBalanceAsync</a>(BalanceInquiry { ... }) -> Balance</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to view the balance of an Electronic Benefit Transfer (EBT) card.  

If the request is successful, our gateway returns the current balance of an EBT card. 
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
await client.PaymentFeatures.Cards.ViewEbtBalanceAsync(
    new BalanceInquiry
    {
        ProcessingTerminalId = "1234001",
        Operator = "Jane",
        Currency = Currency.Usd,
        Card = new BalanceInquiryCard(
            new Payroc.PaymentFeatures.Cards.BalanceInquiryCard.Card(
                new CardPayload
                {
                    CardDetails = new CardPayloadCardDetails(
                        new CardPayloadCardDetails.Raw(
                            new RawCardDetails
                            {
                                Device = new Device
                                {
                                    Model = DeviceModel.BbposChp,
                                    SerialNumber = "1850010868",
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

<details><summary><code>client.PaymentFeatures.Cards.<a href="/src/Payroc/PaymentFeatures/Cards/CardsClient.cs">LookupBinAsync</a>(BinLookup { ... }) -> CardInfo</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a debit card, a credit card, or an EBT card. If you apply surcharges to transactions, you can also check if the card supports surcharging.  

In the response, our gateway returns the following information about the card:  

- **Card details** - Information about the card, for example, the issuing bank and the masked card number.  

- **Surcharging information** - If you apply a surcharge to transactions, our gateway checks that the card supports surcharging and returns information about the surcharge. For more information about surcharging, go to [Credit card surcharging](https://docs.payroc.com/knowledge/card-payments/credit-card-surcharging). 
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
await client.PaymentFeatures.Cards.LookupBinAsync(
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
                                    SerialNumber = "1850010868",
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

<details><summary><code>client.PaymentFeatures.Cards.<a href="/src/Payroc/PaymentFeatures/Cards/CardsClient.cs">RetrieveFxRatesAsync</a>(FxRateInquiry { ... }) -> FxRate</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

> **Important:** There are restrictions on which merchants can use this method. For more information, go to [Dynamic Currency Conversion](https://docs.payroc.com/knowledge/card-payments/dynamic-currency-conversion).  

Use this method to check if a card is eligible for Dynamic Currency Conversion (DCC) and to retrieve the conversion rate for a transaction amount. DCC provides a customer with the option to use their card's currency instead of the merchant's currency, for example, in Ireland, an American customer can pay in US dollars instead of Euros.  

The request includes the following:  

- **Payment method** - Card information, a secure token, or digital wallet.  
- **Transaction information** - Amount and currency of the transaction in the merchant's currency.  

If the card is eligible for DCC, our gateway returns the transaction amount in the card's currency and a dccOffer object that contains information about the conversion rate. The dccOffer object contains the following fields that you need when you [run a sale](https://docs.payroc.com/api/schema/card-payments/payments/create) or [unreferenced refund](https://docs.payroc.com/api/schema/card-payments/refunds/create-unreferenced-refund) with DCC:  
- fxAmount  
- fxCurrency  
- fxRate  
- markup  
- accepted  
- offerReference  
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
await client.PaymentFeatures.Cards.RetrieveFxRatesAsync(
    new FxRateInquiry
    {
        Channel = FxRateInquiryChannel.Web,
        ProcessingTerminalId = "1234001",
        Operator = "Jane",
        BaseAmount = 10000,
        BaseCurrency = Currency.Usd,
        PaymentMethod = new FxRateInquiryPaymentMethod(
            new Payroc.PaymentFeatures.Cards.FxRateInquiryPaymentMethod.Card(
                new CardPayload
                {
                    CardDetails = new CardPayloadCardDetails(
                        new CardPayloadCardDetails.Raw(
                            new RawCardDetails
                            {
                                Device = new Device
                                {
                                    Model = DeviceModel.BbposChp,
                                    SerialNumber = "1850010868",
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

## PaymentFeatures Bank
<details><summary><code>client.PaymentFeatures.Bank.<a href="/src/Payroc/PaymentFeatures/Bank/BankClient.cs">VerifyAsync</a>(BankAccountVerificationRequest { ... }) -> BankAccountVerificationResult</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to verify a customer's bank account details.  

In the request, send the customer's bank account details. Our gateway can verify the following types of bank details:  
- Automated Clearing House (ACH) details  
- Pre-Authorized Debit (PAD) details  

In the response, our gateway indicates if the account details are valid and if you should use them in follow-on actions.  
  
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
await client.PaymentFeatures.Bank.VerifyAsync(
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

## PaymentLinks SharingEvents
<details><summary><code>client.PaymentLinks.SharingEvents.<a href="/src/Payroc/PaymentLinks/SharingEvents/SharingEventsClient.cs">ListAsync</a>(ListSharingEventsRequest { ... }) -> PayrocPager<PaymentLinkEmailShareEvent></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of sharing events for a payment link. A sharing event occurs when a merchant shares a payment link with a customer.  

To list the sharing events for a payment link, you need its paymentLinkId. Our gateway returned the paymentLinkId in the response of the [Create Payment Link](https://docs.payroc.com/api/schema/payments/payment-links/create) method.  

**Note:** If you don't have the paymentLinkId, use our [List Payment Links](https://docs.payroc.com/api/schema/payments/payment-links/list) method to search for the payment link.  

Use query parameters to filter the list of results that we return, for example, to search for links sent to a specific customer.  

Our gateway returns the following information for each sharing event in the list:  
- Customer that the merchant sent the link to.  
- Date that the merchant sent the link.  
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
await client.PaymentLinks.SharingEvents.ListAsync(
    new ListSharingEventsRequest
    {
        PaymentLinkId = "JZURRJBUPS",
        RecipientName = "Sarah Hazel Hopper",
        RecipientEmail = "sarah.hopper@example.com",
        Before = "2571",
        After = "8516",
        Limit = 1,
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

**request:** `ListSharingEventsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.PaymentLinks.SharingEvents.<a href="/src/Payroc/PaymentLinks/SharingEvents/SharingEventsClient.cs">ShareAsync</a>(ShareSharingEventsRequest { ... }) -> PaymentLinkEmailShareEvent</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to email a payment link to a customer.  

To email a payment link, you need its paymentLinkId. Our gateway returned the paymentLinkId in the response of the [Create Payment Link](https://docs.payroc.com/api/schema/payments/payment-links/create) method.  

**Note:** If you don't have the paymentLinkId, use our [List Payment Links](https://docs.payroc.com/api/schema/payments/payment-links/list) method to search for the payment link.  

In the request, you must provide the recipient's name and email address.  

In the response, our gateway returns a sharingEventId, which you can use to [List Payment Link Sharing Events](https://docs.payroc.com/api/schema/payments/payment-links/sharing-events/list).  
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
await client.PaymentLinks.SharingEvents.ShareAsync(
    new ShareSharingEventsRequest
    {
        PaymentLinkId = "JZURRJBUPS",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new PaymentLinkEmailShareEvent
        {
            SharingMethod = PaymentLinkEmailShareEventSharingMethod.Email,
            MerchantCopy = true,
            Message =
                "Dear Sarah,\n\nYour insurance is expiring this month.\nPlease, pay the renewal fee by the end of the month to renew it.\n",
            Recipients = new List<PaymentLinkEmailRecipient>()
            {
                new PaymentLinkEmailRecipient
                {
                    Name = "Sarah Hazel Hopper",
                    Email = "sarah.hopper@example.com",
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

**request:** `ShareSharingEventsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## PayrocCloud PaymentInstructions
<details><summary><code>client.PayrocCloud.PaymentInstructions.<a href="/src/Payroc/PayrocCloud/PaymentInstructions/PaymentInstructionsClient.cs">SubmitAsync</a>(PaymentInstructionRequest { ... }) -> PaymentInstruction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to submit an instruction request to initiate a sale on a payment device.  

In the request, include the order amount and currency.  

When you send a successful request, our gateway returns information about the payment instruction and a paymentInstructionId, which you need for the following methods:
- [Retrieve payment instruction](https://docs.payroc.com/api/schema/payroc-cloud/payment-instructions/retrieve) - View the details of the payment instruction.
- [Cancel payment instruction](https://docs.payroc.com/api/schema/payroc-cloud/payment-instructions/delete) - Cancel the payment instruction.
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
await client.PayrocCloud.PaymentInstructions.SubmitAsync(
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

<details><summary><code>client.PayrocCloud.PaymentInstructions.<a href="/src/Payroc/PayrocCloud/PaymentInstructions/PaymentInstructionsClient.cs">RetrieveAsync</a>(RetrievePaymentInstructionsRequest { ... }) -> PaymentInstruction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a payment instruction.  

To retrieve a payment instruction, you need its paymentInstructionId. Our gateway returned the paymentInstructionId in the response of the [Submit Payment Instruction](https://docs.payroc.com/api/schema/payroc-cloud/payment-instructions/submit) method.  

Our gateway returns the status of the payment instruction. If the payment device completed the payment instruction, the response also includes a link to the payment.
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
await client.PayrocCloud.PaymentInstructions.RetrieveAsync(
    new RetrievePaymentInstructionsRequest
    {
        PaymentInstructionId = "e743a9165d134678a9100ebba3b29597",
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

**request:** `RetrievePaymentInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.PayrocCloud.PaymentInstructions.<a href="/src/Payroc/PayrocCloud/PaymentInstructions/PaymentInstructionsClient.cs">DeleteAsync</a>(DeletePaymentInstructionsRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to cancel a payment instruction.  

You can cancel a payment instruction only if its status is `inProgress`. To retrieve the status of a payment instruction, use our [Retrieve Payment Instruction](https://docs.payroc.com/api/schema/payroc-cloud/payment-instructions/retrieve) method.  

To cancel a payment instruction, you need its paymentInstructionId. Our gateway returned the paymentInstructionId in the response of the [Submit Payment Instruction](https://docs.payroc.com/api/schema/payroc-cloud/payment-instructions/submit) method.
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
await client.PayrocCloud.PaymentInstructions.DeleteAsync(
    new DeletePaymentInstructionsRequest
    {
        PaymentInstructionId = "e743a9165d134678a9100ebba3b29597",
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

**request:** `DeletePaymentInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## PayrocCloud RefundInstructions
<details><summary><code>client.PayrocCloud.RefundInstructions.<a href="/src/Payroc/PayrocCloud/RefundInstructions/RefundInstructionsClient.cs">SubmitAsync</a>(RefundInstructionRequest { ... }) -> RefundInstruction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to submit an instruction request to initiate a refund on a payment device.  

In the request, include the refund amount and currency.  

If the request is successful, our gateway returns information about the refund instruction and a refundInstructionId, which you need for the following methods:
- [Retrieve refund instruction](https://docs.payroc.com/api/schema/payroc-cloud/refund-instructions/retrieve) - View the details of the refund instruction.
- [Cancel refund instruction](https://docs.payroc.com/api/schema/payroc-cloud/refund-instructions/delete) - Cancel the refund instruction.
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
await client.PayrocCloud.RefundInstructions.SubmitAsync(
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

<details><summary><code>client.PayrocCloud.RefundInstructions.<a href="/src/Payroc/PayrocCloud/RefundInstructions/RefundInstructionsClient.cs">RetrieveAsync</a>(RetrieveRefundInstructionsRequest { ... }) -> RefundInstruction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a refund instruction.  

To retrieve a refund instruction, you need its refundInstructionId. Our gateway returned the refundInstructionId in the response of the [Submit Refund Instruction](https://docs.payroc.com/api/schema/payroc-cloud/refund-instructions/submit) method.  

Our gateway returns the status of the refund instruction. If the payment device completed the refund instruction, the response also includes a link to the refund.
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
await client.PayrocCloud.RefundInstructions.RetrieveAsync(
    new RetrieveRefundInstructionsRequest
    {
        RefundInstructionId = "a37439165d134678a9100ebba3b29597",
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

**request:** `RetrieveRefundInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.PayrocCloud.RefundInstructions.<a href="/src/Payroc/PayrocCloud/RefundInstructions/RefundInstructionsClient.cs">DeleteAsync</a>(DeleteRefundInstructionsRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to cancel a refund instruction.  

You can cancel a refund instruction only if its status is `inProgress`. To retrieve the status of a refund instruction, use our [Retrieve Refund Instruction](https://docs.payroc.com/api/schema/payroc-cloud/refund-instructions/retrieve) method.  

To cancel a refund instruction, you need its refundInstructionId. Our gateway returned the refundInstructionId in the response of the [Submit Refund Instruction](https://docs.payroc.com/api/schema/payroc-cloud/refund-instructions/submit) method. 
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
await client.PayrocCloud.RefundInstructions.DeleteAsync(
    new DeleteRefundInstructionsRequest { RefundInstructionId = "a37439165d134678a9100ebba3b29597" }
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

**request:** `DeleteRefundInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## PayrocCloud SignatureInstructions
<details><summary><code>client.PayrocCloud.SignatureInstructions.<a href="/src/Payroc/PayrocCloud/SignatureInstructions/SignatureInstructionsClient.cs">SubmitAsync</a>(SignatureInstructionRequest { ... }) -> SignatureInstruction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to submit an instruction to capture a customer's signature on a payment device.  

Our gateway returns information about the signature instruction and a signatureInstructionId, which you need for the following methods:
- [Retrieve signature instruction](https://docs.payroc.com/api/schema/payroc-cloud/signature-instructions/retrieve) - View the details of the signature instruction.
- [Cancel signature instruction](https://docs.payroc.com/api/schema/payroc-cloud/signature-instructions/delete) - Cancel the signature instruction.
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
await client.PayrocCloud.SignatureInstructions.SubmitAsync(
    new SignatureInstructionRequest
    {
        SerialNumber = "1850010868",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        ProcessingTerminalId = "1234001",
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

**request:** `SignatureInstructionRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.PayrocCloud.SignatureInstructions.<a href="/src/Payroc/PayrocCloud/SignatureInstructions/SignatureInstructionsClient.cs">RetrieveAsync</a>(RetrieveSignatureInstructionsRequest { ... }) -> SignatureInstruction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a signature instruction.  

To retrieve a signature instruction, you need its signatureInstructionId. Our gateway returned the signatureInstructionId in the response of the [Submit Signature Instruction](https://docs.payroc.com/api/schema/payroc-cloud/signature-instructions/submit) method.  

Our gateway returns the status of the instruction. If the payment device completed the instruction, the response also includes a link to retrieve the signature.
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
await client.PayrocCloud.SignatureInstructions.RetrieveAsync(
    new RetrieveSignatureInstructionsRequest
    {
        SignatureInstructionId = "a37439165d134678a9100ebba3b29597",
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

**request:** `RetrieveSignatureInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.PayrocCloud.SignatureInstructions.<a href="/src/Payroc/PayrocCloud/SignatureInstructions/SignatureInstructionsClient.cs">DeleteAsync</a>(DeleteSignatureInstructionsRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to cancel a signature instruction.  

To cancel a signature instruction, you need its signatureInstructionId. Our gateway returned the signatureInstructionId in the response of the [Submit signature instruction](https://docs.payroc.com/api/schema/payroc-cloud/signature-instructions/submit) method.
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
await client.PayrocCloud.SignatureInstructions.DeleteAsync(
    new DeleteSignatureInstructionsRequest
    {
        SignatureInstructionId = "a37439165d134678a9100ebba3b29597",
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

**request:** `DeleteSignatureInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## PayrocCloud Signatures
<details><summary><code>client.PayrocCloud.Signatures.<a href="/src/Payroc/PayrocCloud/Signatures/SignaturesClient.cs">RetrieveAsync</a>(RetrieveSignaturesRequest { ... }) -> RetrieveSignaturesResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve a signature that a payment device captured using Payroc Cloud. 

Our gateway returns the following information about the signature:
- Image of the signature
- Format of the image
- Date that the device captured the image
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
await client.PayrocCloud.Signatures.RetrieveAsync(
    new RetrieveSignaturesRequest { SignatureId = "JDN4ILZB0T" }
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

**request:** `RetrieveSignaturesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## RepeatPayments PaymentPlans
<details><summary><code>client.RepeatPayments.PaymentPlans.<a href="/src/Payroc/RepeatPayments/PaymentPlans/PaymentPlansClient.cs">ListAsync</a>(ListPaymentPlansRequest { ... }) -> PayrocPager<PaymentPlan></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payment plans for a processing terminal.  

**Note:** If you want to view the details of a specific payment plan and you have its paymentPlanId, use our [Retrieve Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/retrieve) method.  

Our gateway returns the following information about each payment plan in the list:  

  -	Name, length, and currency of the plan  
  -	How often our gateway collects each payment  
  -	How much our gateway collects for each payment  
  -	What happens if the merchant updates or deletes the plan  

For each payment plan, we return the paymentPlanId, which you can use to perform follow-on actions.
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
await client.RepeatPayments.PaymentPlans.ListAsync(
    new ListPaymentPlansRequest
    {
        ProcessingTerminalId = "1234001",
        Before = "2571",
        After = "8516",
        Limit = 1,
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

<details><summary><code>client.RepeatPayments.PaymentPlans.<a href="/src/Payroc/RepeatPayments/PaymentPlans/PaymentPlansClient.cs">CreateAsync</a>(CreatePaymentPlansRequest { ... }) -> PaymentPlan</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create a payment schedule that you can assign customers to.  

**Note:** This method is part of our Repeat Payments feature. To help you understand how this method works with our Subscriptions endpoints, go to [Repeat Payments](https://docs.payroc.com/guides/integrate/repeat-payments).  

When you create a payment plan you need to provide a unique paymentPlanId that you use to run follow-on actions:  

-	[Retrieve Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/retrieve)  - View the details of the payment plan.  
-	[Update Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/partially-update)  - Update the details of the payment plan.  
-	[Delete Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/delete)  - Delete the payment plan.  
-	[Create Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create)  - Subscribe a customer to the payment plan.  

The request includes the following settings:  

-	**type** - Indicates if our gateway or the merchant collects payments. If the merchant manually collects payments, integrate with the [Pay Manual Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/pay) method.  
-	**recurringOrder** - Amount of each payment if the gateway automatically collect payments.  
-	**setupOrder** - Setup fee that our gateway immediately collects from the customer's payment method.  
-	**onUpdate and onDelete** - Indicates what happens to associated subscriptions if the merchant updates or deletes the payment plan.  
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
await client.RepeatPayments.PaymentPlans.CreateAsync(
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
            Length = 12,
            Type = PaymentPlanBaseType.Automatic,
            Frequency = PaymentPlanBaseFrequency.Monthly,
            OnUpdate = PaymentPlanBaseOnUpdate.Continue,
            OnDelete = PaymentPlanBaseOnDelete.Complete,
            CustomFieldNames = new List<string>() { "yourCustomField" },
            SetupOrder = new PaymentPlanSetupOrder
            {
                Amount = 4999,
                Description = "Initial setup fee for Premium Club subscription",
                Breakdown = new PaymentPlanOrderBreakdown
                {
                    Subtotal = 4347,
                    Taxes = new List<RetrievedTax>()
                    {
                        new RetrievedTax { Name = "Sales Tax", Rate = 5 },
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
                    Taxes = new List<RetrievedTax>()
                    {
                        new RetrievedTax { Name = "Sales Tax", Rate = 5 },
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

**request:** `CreatePaymentPlansRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.RepeatPayments.PaymentPlans.<a href="/src/Payroc/RepeatPayments/PaymentPlans/PaymentPlansClient.cs">RetrieveAsync</a>(RetrievePaymentPlansRequest { ... }) -> PaymentPlan</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a payment plan.  

To retrieve a payment plan, you need its paymentPlanId. Our gateway returned the paymentPlanId in the response of the [Create Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/create) method.  

**Note:** If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/list) method to search for the payment plan.  

Our gateway returns the following information about the payment plan:  

  -	Name, length, and currency of the plan  
  -	How often our gateway collects each payment  
  -	How much our gateway collects for each payment  
  -	What happens if the merchant updates or deletes the plan  
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
await client.RepeatPayments.PaymentPlans.RetrieveAsync(
    new RetrievePaymentPlansRequest
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

**request:** `RetrievePaymentPlansRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.RepeatPayments.PaymentPlans.<a href="/src/Payroc/RepeatPayments/PaymentPlans/PaymentPlansClient.cs">DeleteAsync</a>(DeletePaymentPlansRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to delete a payment plan.  

> **Important:** When you delete a payment plan, you can’t recover it. You also won’t be able to add subscriptions to the payment plan.  

To delete a payment plan, you need its paymentPlanId, which you sent in the request of the [Create Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/create) method.  

**Note:** If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/list) method to search for the payment plan.  

The value you sent for the onDelete parameter when you created the payment plan indicates what happens to associated subscriptions when you delete the plan:  

  -	`complete` - Our gateway stops taking payments for the subscriptions associated with the payment plan.  
  -	`continue` - Our gateway continues to take payments for the subscriptions associated with the payment plan. To stop a subscription for a cancelled payment plan, go to the [Deactivate Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/deactivate) method.  
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
await client.RepeatPayments.PaymentPlans.DeleteAsync(
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

<details><summary><code>client.RepeatPayments.PaymentPlans.<a href="/src/Payroc/RepeatPayments/PaymentPlans/PaymentPlansClient.cs">PartiallyUpdateAsync</a>(PartiallyUpdatePaymentPlansRequest { ... }) -> PaymentPlan</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to partially update a payment plan. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.  

To update a payment plan, you need its paymentPlanId, which you sent in the request of the [Create Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/create) method.  

**Note:** If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/list) method to search for the payment plan.  

You can update all of the properties of the payment plan except for the paymentPlanId.  

The value you sent for the onUpdate parameter when you created the payment plan indicates what happens to the associated subscriptions when you update the plan:  
- `update` - Our gateway updates the subscriptions associated with the payment plan.
- `continue` - Our  gateway doesn't update the subscriptions associated with the payment plan.
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
await client.RepeatPayments.PaymentPlans.PartiallyUpdateAsync(
    new PartiallyUpdatePaymentPlansRequest
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

**request:** `PartiallyUpdatePaymentPlansRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## RepeatPayments Subscriptions
<details><summary><code>client.RepeatPayments.Subscriptions.<a href="/src/Payroc/RepeatPayments/Subscriptions/SubscriptionsClient.cs">ListAsync</a>(ListSubscriptionsRequest { ... }) -> PayrocPager<Subscription></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of subscriptions.  

Note: If you want to view the details of a specific subscription and you have its subscriptionId, use our [Retrieve subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/retrieve) method.  

Use query parameters to filter the list of results that we return, for example, to search for subscriptions for a customer, a payment plan, or frequency.  

Our gateway returns information about the following for each subscription in the list:  

-	Payment plan the subscription is linked to.  
-	Secure token that represents cardholder’s payment details.  
-	Current state of the subscription, including its status, next due date, and invoices.  
-	Fees for setup and the cost of the recurring order.  
-	Subscription length, end date, and frequency.  

For each subscription, we also return the subscriptionId, the paymentPlanId, and the secureTokenId, which you can use to perform follow-actions. 
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
await client.RepeatPayments.Subscriptions.ListAsync(
    new ListSubscriptionsRequest
    {
        ProcessingTerminalId = "1234001",
        CustomerName = "Sarah%20Hazel%20Hopper",
        Last4 = "7062",
        PaymentPlan = "Premium%20Club",
        Frequency = ListSubscriptionsRequestFrequency.Weekly,
        Status = ListSubscriptionsRequestStatus.Active,
        EndDate = new DateOnly(2025, 7, 1),
        NextDueDate = new DateOnly(2024, 8, 1),
        Before = "2571",
        After = "8516",
        Limit = 1,
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

<details><summary><code>client.RepeatPayments.Subscriptions.<a href="/src/Payroc/RepeatPayments/Subscriptions/SubscriptionsClient.cs">CreateAsync</a>(SubscriptionRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to assign a customer to a payment plan.  

**Note:** This method is part of our Repeat Payments feature. To help you understand how this method works with our Payment plans endpoints, go to [Repeat Payments](https://docs.payroc.com/guides/integrate/repeat-payments).  

When you create a subscription you need to provide a unique subscriptionId that you use to run follow-on actions:  

- [Retrieve Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/retrieve) - View the details of the subscription.
- [Update Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/partially-update) - Update the details of the subscription.
- [Deactivate Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/deactivate) - Stop taking payments for the subscription.
- [Re-activate Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/reactivate) - Start taking payments again for the subscription.
- [Pay Manual Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/pay) - Manually collect a payment for the subscription.

The request includes the following settings:
- **paymentPlanId** - Unique identifier of the payment plan that the merchant wants to use. If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/list) method to search for the payment plan.
- **paymentMethod** - Object that contains information about the secure token, which represents the customer's card details or bank account details.
- **startDate** - Date that you want to start to take payments.

You can also update the settings that the subscription inherited from the payment plan, for example, you can change the amount for each payment. If you change the settings for the subscription, it doesn't change the settings in the payment plan that it's linked to. 
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
await client.RepeatPayments.Subscriptions.CreateAsync(
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
        SetupOrder = new SubscriptionPaymentOrderRequest
        {
            OrderId = "OrderRef6543",
            Amount = 4999,
            Description = "Initial setup fee for Premium Club subscription",
        },
        RecurringOrder = new SubscriptionRecurringOrderRequest
        {
            Amount = 4999,
            Description = "Monthly Premium Club subscription",
            Breakdown = new SubscriptionOrderBreakdownRequest
            {
                Subtotal = 4347,
                Taxes = new List<TaxRate>()
                {
                    new TaxRate
                    {
                        Type = TaxRateType.Rate,
                        Rate = 5,
                        Name = "Sales Tax",
                    },
                },
            },
        },
        StartDate = new DateOnly(2024, 7, 2),
        EndDate = new DateOnly(2025, 7, 1),
        Length = 12,
        PauseCollectionFor = 0,
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

<details><summary><code>client.RepeatPayments.Subscriptions.<a href="/src/Payroc/RepeatPayments/Subscriptions/SubscriptionsClient.cs">RetrieveAsync</a>(RetrieveSubscriptionsRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a subscription.  

To retrieve a subscription, you need its subscriptionId. You sent the subscriptionId in the request of the [Create subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List subscriptions](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/list) method to search for the subscription.  

Our gateway returns information about the following for the subscription:  

-	Payment plan the subscription is linked to.  
-	Secure token that represents cardholder’s payment details.  
-	Current state of the subscription, including its status, next due date, and invoices.  
-	Fees for setup and the cost of the recurring order.  
-	Subscription length, end date, and frequency.  

We also return the paymentPlanId and the secureTokenId, which you can use to perform follow-on actions.  
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
await client.RepeatPayments.Subscriptions.RetrieveAsync(
    new RetrieveSubscriptionsRequest
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

**request:** `RetrieveSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.RepeatPayments.Subscriptions.<a href="/src/Payroc/RepeatPayments/Subscriptions/SubscriptionsClient.cs">PartiallyUpdateAsync</a>(PartiallyUpdateSubscriptionsRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to partially update a subscription. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.  

To update a subscription, you need its subscriptionId, which you sent in the request of the [Create subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List subscriptions](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/list) method to search for the payment.  

You can update all of the properties of the subscription except for the following:  

**Can't delete**  
- recurringOrder
- description
- name

**Can't perform any PATCH operation**  
- currentState
- type
- frequency
- paymentPlan
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
await client.RepeatPayments.Subscriptions.PartiallyUpdateAsync(
    new PartiallyUpdateSubscriptionsRequest
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

**request:** `PartiallyUpdateSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.RepeatPayments.Subscriptions.<a href="/src/Payroc/RepeatPayments/Subscriptions/SubscriptionsClient.cs">DeactivateAsync</a>(DeactivateSubscriptionsRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to deactivate a subscription.  

To deactivate a subscription, you need its subscriptionId, which you sent in the request of the [Create Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List Subscriptions](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/list) method to search for the subscription.  

If your request is successful, our gateway stops taking payments from the customer.  

To reactivate the subscription, use our [Reactivate Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/reactivate) method.
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
await client.RepeatPayments.Subscriptions.DeactivateAsync(
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

<details><summary><code>client.RepeatPayments.Subscriptions.<a href="/src/Payroc/RepeatPayments/Subscriptions/SubscriptionsClient.cs">ReactivateAsync</a>(ReactivateSubscriptionsRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to reactivate a subscription.  

To reactivate a subscription, you need its subscriptionId, which you sent in the request of the [Create Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List Subscriptions](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/list) method to search for the subscription.  

If your request is successful, our gateway restarts taking payments from the customer.  

To deactivate the subscription, use our [Deactivate Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/deactivate) method.
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
await client.RepeatPayments.Subscriptions.ReactivateAsync(
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

<details><summary><code>client.RepeatPayments.Subscriptions.<a href="/src/Payroc/RepeatPayments/Subscriptions/SubscriptionsClient.cs">PayAsync</a>(SubscriptionPaymentRequest { ... }) -> SubscriptionPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to manually collect a payment linked to a subscription. You can manually collect a payment only if the merchant chose not to let our gateway automatically collect each payment.  

To manually collect a payment, you need the subscriptionId of the subscription that's linked to the payment. You sent the subscriptionId in the request of the [Create Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List Subscriptions](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/list) method to search for the subscription.  

The request includes an order object that contains information about the amount that you want to collect.  

In the response, our gateway returns information about the payment and a paymentId. You can use the paymentId in follow-on actions with the [Payments](https://docs.payroc.com/api/schema/payments) endpoints or [Bank Transfer Payments](https://docs.payroc.com/api/schema/payments/bank-transfer-payments) endpoints.  
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
await client.RepeatPayments.Subscriptions.PayAsync(
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

## Reporting Settlement
<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">ListBatchesAsync</a>(ListReportingSettlementBatchesRequest { ... }) -> PayrocPager<Batch></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of batches that your merchants submitted to the processor on a specific date.  

**Note:** If you want to view the details of a specific batch and you have its batchId, use our [Retrieve Batch](https://docs.payroc.com/api/schema/reporting/settlement/retrieve-batch) method.  

Use query parameters to filter the list of results that we return, for example, to search for batches that were submitted by a specific merchant.  

> **Important:** You must provide a value for the date query parameter.  

Our gateway returns the following information about each batch in the list:  
-	Transaction information, including the number of transactions and total value of sales.  
-	Merchant information, including the merchant ID (MID) and the processing account that the batch is associated with.  
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
        Limit = 1,
        Date = new DateOnly(2027, 7, 2),
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

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">RetrieveBatchAsync</a>(RetrieveBatchSettlementRequest { ... }) -> Batch</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a batch.  

**Note:** To retrieve a batch, you need its batchId. If you don't have the batchId, use our [List Batches](https://docs.payroc.com/api/schema/reporting/settlement/list-batches) method to search for the batch.  

Our gateway returns the following information about the batch:  

-	Transaction information, including the number of transactions and total value of sales.  
-	Merchant information, including the merchant ID (MID) and the processing account that the batch is associated with.  
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
await client.Reporting.Settlement.RetrieveBatchAsync(
    new RetrieveBatchSettlementRequest { BatchId = 1 }
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

**request:** `RetrieveBatchSettlementRequest` 
    
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

Use this method to return a paginated list of your merchants’ transactions.  

**Note:** If you want to view the details of a specific transaction and you have its transactionId, use our [Retrieve Transaction](https://docs.payroc.com/api/schema/reporting/settlement/retrieve-transaction) method.  

Use query parameters to filter the list of results that we return, for example, to search for transactions for a specific merchant.  

> **Important:** You must provide a value for either the date query parameter or the batchId query parameter.  

Our gateway returns the following information about each transaction in the list:  

-	Merchant and processing account that ran the transaction.  
-	Transaction type, date, amount, and the payment method that the customer used.  
-	Batch that contains the transaction, and authorization details for the transaction.  
-	Processor that settled the transaction and the ACH deposit containing the transaction.  
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
        Limit = 1,
        Date = new DateOnly(2024, 7, 2),
        BatchId = 1,
        MerchantId = "4525644354",
        TransactionType = ListTransactionsSettlementRequestTransactionType.Capture,
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

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">RetrieveTransactionAsync</a>(RetrieveTransactionSettlementRequest { ... }) -> Transaction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a transaction.  

**Note:** To retrieve a transaction, you need its transactionId. If you don't have the transactionId, use our [List Transactions](https://docs.payroc.com/api/schema/reporting/settlement/list-transactions) method to search for the transaction.  

Our gateway returns the following information about the transaction:  

-	Merchant and processing account that ran the transaction.  
-	Transaction type, date, amount, and the payment method that the customer used.  
-	Batch that contains the transaction, and authorization details for the transaction.   
-	Processor that settled the transaction and the ACH deposit containing the transaction.  
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
await client.Reporting.Settlement.RetrieveTransactionAsync(
    new RetrieveTransactionSettlementRequest { TransactionId = 1 }
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

**request:** `RetrieveTransactionSettlementRequest` 
    
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

Use this method to retrieve a [paginated](https://docs.payroc.com/api/pagination) list of authorizations.  

Use query parameters to filter the list of results that we return, for example, to search for authorizations linked to a specific merchant.  

> **Important:** You must provide a value for either the date query parameter or the batchId query parameter.  

Our gateway returns the following information about each authorization in the list:
- Authorization response from the issuing bank.
- Amount that the issuing bank authorized.
- Merchant that ran the authorization.
- Details about the customer's card, the transaction, and the batch.
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
        Limit = 1,
        Date = new DateOnly(2024, 7, 2),
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

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">RetrieveAuthorizationAsync</a>(RetrieveAuthorizationSettlementRequest { ... }) -> Authorization</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about an authorization.  

**Note:** To retrieve an authorization, you need its authorizationId. If you don't have the authorizationId, use our [List Authorizations](https://docs.payroc.com/api/schema/reporting/settlement/list-authorizations) method to search for the authorization.  

Our gateway returns the following information about the authorization:
- Authorization response from the issuing bank.
- Amount that the issuing bank authorized.
- Merchant that ran the authorization.
- Details about the customer's card, the transaction, and the batch.
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
await client.Reporting.Settlement.RetrieveAuthorizationAsync(
    new RetrieveAuthorizationSettlementRequest { AuthorizationId = 1 }
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

**request:** `RetrieveAuthorizationSettlementRequest` 
    
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

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of disputes.  

Use query parameters to filter the list of results that we return, for example, to search for disputes linked to a specific merchant.  

> **Important:** You must provide a value for the date query parameter.  

Our gateway returns the following information about each dispute in the list:  
- Its status, type, and description.  
- Transaction that the dispute is linked to, including the transaction date, merchant who ran the transaction, and the payment method that the cardholder used.  
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
        Limit = 1,
        Date = new DateOnly(2024, 7, 2),
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

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">ListDisputesStatusesAsync</a>(ListDisputesStatusesSettlementRequest { ... }) -> IEnumerable<DisputeStatus></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return the status history of a dispute.  

To view the status history of a dispute, you need its disputeId. If you don't have the disputeId, use our [List Disputes](https://docs.payroc.com/api/schema/reporting/settlement/list-disputes) method to search for the dispute. 

Our gateway returns a list that contains each status change, the date it was changed, and its updated status.  
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
await client.Reporting.Settlement.ListDisputesStatusesAsync(
    new ListDisputesStatusesSettlementRequest { DisputeId = 1 }
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

**request:** `ListDisputesStatusesSettlementRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">ListAchDepositsAsync</a>(ListReportingSettlementAchDepositsRequest { ... }) -> PayrocPager<AchDeposit></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of ACH deposits that we paid to your merchants.  

**Note:** If you want to view the details of a specific ACH deposit and you have its achDepositId, use our [Retrieve ACH Deposit](https://docs.payroc.com/api/schema/reporting/settlement/retrieve-ach-deposit) method.  

Use query parameters to filter the list of results that we return, for example, to search for ACH deposits that we paid to a specific merchant.  

> **Important:** You must provide a value for the date query parameter.  

Our gateway returns the following information about each ACH deposit in the list: 
- Merchant that we sent the ACH deposit to.
- Total amount that we paid the merchant.
- Breakdown of sales, returns, and fees.
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
await client.Reporting.Settlement.ListAchDepositsAsync(
    new ListReportingSettlementAchDepositsRequest
    {
        Before = "2571",
        After = "8516",
        Limit = 1,
        Date = new DateOnly(2024, 7, 2),
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

**request:** `ListReportingSettlementAchDepositsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">RetrieveAchDepositAsync</a>(RetrieveAchDepositSettlementRequest { ... }) -> AchDeposit</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about an ACH deposit that we paid to a merchant.  

**Note:** To retrieve an ACH deposit, you need its achDepositId. If you don't have the achDepositId, use our [List ACH Deposits](https://docs.payroc.com/api/schema/reporting/settlement/list-ach-deposits) method to search for the ACH deposit.  

Our gateway returns the following information about the ACH deposit:  

- Merchant that we sent the ACH deposit to.  
- Total amount that we paid the merchant.  
- Breakdown of sales, returns, and fees.  
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
await client.Reporting.Settlement.RetrieveAchDepositAsync(
    new RetrieveAchDepositSettlementRequest { AchDepositId = 1 }
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

**request:** `RetrieveAchDepositSettlementRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">ListAchDepositFeesAsync</a>(ListReportingSettlementAchDepositFeesRequest { ... }) -> PayrocPager<AchDepositFee></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a list of ACH deposit fees.

> **Important:** You must provide a value for either the 'date' query parameter or the 'achDepositId' query parameter.
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
await client.Reporting.Settlement.ListAchDepositFeesAsync(
    new ListReportingSettlementAchDepositFeesRequest
    {
        Before = "2571",
        After = "8516",
        Limit = 1,
        Date = new DateOnly(2024, 7, 2),
        AchDepositId = 1,
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

**request:** `ListReportingSettlementAchDepositFeesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Tokenization SecureTokens
<details><summary><code>client.Tokenization.SecureTokens.<a href="/src/Payroc/Tokenization/SecureTokens/SecureTokensClient.cs">ListAsync</a>(ListSecureTokensRequest { ... }) -> PayrocPager<SecureTokenWithAccountType></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of secure tokens.  

**Note:** If you want to view the details of a specific secure token and you have its secureTokenId, use our [Retrieve Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/retrieve) method.  

Use query parameters to filter the list of results that we return, for example, to search for secure tokens by customer or by the first four digits of a card number.  

Our gateway returns information about the following for each secure token in the list:  

  -	Payment details that the secure token represents.  
  -	Customer details, including shipping and billing addresses.  
  -	Secure token that you can use to carry out transactions.  

  For each secure token, we also return the secureTokenId, which you can use to perform follow-on actions.
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
await client.Tokenization.SecureTokens.ListAsync(
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
        Limit = 1,
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

<details><summary><code>client.Tokenization.SecureTokens.<a href="/src/Payroc/Tokenization/SecureTokens/SecureTokensClient.cs">CreateAsync</a>(TokenizationRequest { ... }) -> SecureToken</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create a secure token that represents a customer's payment details.  

When you create a secure token, you need to generate and provide a secureTokenId that you use to run follow-on actions:  
- [Retrieve Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/retrieve) – View the details of the secure token.  
- [Delete Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/delete) – Delete the secure token.  
- [Update Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/partially-update) – Update the details of the secure token.  
- [Update Account Details](https://docs.payroc.com/api/schema/tokenization/secure-tokens/update-account) – Update the secure token with the details from a single-use token.  

**Note:** If you don't generate a secureTokenId to identify the token, our gateway generates a unique identifier and returns it in the response.  

If the request is successful, our gateway returns a token that the merchant can use in transactions instead of the customer's sensitive payment details, for example, when they [run a sale](https://docs.payroc.com/api/schema/card-payments/payments/create).
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
await client.Tokenization.SecureTokens.CreateAsync(
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
            DateOfBirth = new DateOnly(1990, 7, 15),
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
            new Payroc.Tokenization.SecureTokens.TokenizationRequestSource.Card(
                new CardPayload
                {
                    CardDetails = new CardPayloadCardDetails(
                        new CardPayloadCardDetails.Raw(
                            new RawCardDetails
                            {
                                Device = new Device
                                {
                                    Model = DeviceModel.BbposChp,
                                    SerialNumber = "1850010868",
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

<details><summary><code>client.Tokenization.SecureTokens.<a href="/src/Payroc/Tokenization/SecureTokens/SecureTokensClient.cs">RetrieveAsync</a>(RetrieveSecureTokensRequest { ... }) -> SecureTokenWithAccountType</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a secure token.  

To retrieve a secure token, you need its secureTokenID, which you sent in the request of the [Create Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/create) method.  

**Note:** If you don't have the secureTokenId, use our [List Secure Tokens](https://docs.payroc.com/api/schema/tokenization/secure-tokens/list) method to search for the secure token.  

Our gateway returns the following information about the secure token:  

  -	Payment details that the secure token represents.  
  -	Customer details, including shipping and billing addresses.  
  -	Secure token that you can use to carry out transactions.  
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
await client.Tokenization.SecureTokens.RetrieveAsync(
    new RetrieveSecureTokensRequest
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

**request:** `RetrieveSecureTokensRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Tokenization.SecureTokens.<a href="/src/Payroc/Tokenization/SecureTokens/SecureTokensClient.cs">DeleteAsync</a>(DeleteSecureTokensRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to delete a secure token and its related payment details from our vault.  

To delete a secure token, you need its secureTokenId, which you sent in the request of the [Create Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/create) method.  

**Note:** If you don’t have the secureTokenId, use our [List Secure Tokens](https://docs.payroc.com/api/schema/tokenization/secure-tokens/list) method to search for the secure token.  

When you delete a secure token, you can’t recover it, and you can’t reuse its identifier for a new token.  
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
await client.Tokenization.SecureTokens.DeleteAsync(
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

<details><summary><code>client.Tokenization.SecureTokens.<a href="/src/Payroc/Tokenization/SecureTokens/SecureTokensClient.cs">PartiallyUpdateAsync</a>(PartiallyUpdateSecureTokensRequest { ... }) -> SecureToken</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to partially update a secure token. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.  

To update a secure token, you need its secureTokenId, which you sent in the request of the [Create Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/create) method.  

**Note:** If you don't have the secureTokenId, use our [List Secure Tokens](https://docs.payroc.com/api/schema/tokenization/secure-tokens/list) method to search  for the payment.  

You can update all of the properties of the secure token, except the following:  
- processingTerminalId  
- type  
- token  
- status  
- source/Card  
  - type  
  - cardNumber  
  - cardType  
  - currency  
  - debit  
  - surcharging  
- source/ACH account  
  - accountNumber  
  - routingNumber  
- source/PAD account  
  - type  
  - accountNumber  
  - transitNumber  
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
await client.Tokenization.SecureTokens.PartiallyUpdateAsync(
    new PartiallyUpdateSecureTokensRequest
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

**request:** `PartiallyUpdateSecureTokensRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Tokenization.SecureTokens.<a href="/src/Payroc/Tokenization/SecureTokens/SecureTokensClient.cs">UpdateAccountAsync</a>(UpdateAccountSecureTokensRequest { ... }) -> SecureToken</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to update a secure token if you have a single-use token from Hosted Fields.  

**Note:** If you don't have a single-use token, you can update saved payment details with our [Update Secure Token](https://docs.payroc.com/api/resources#updateSecureToken) method. For more information about our two options to update a secure token, go to [Update saved payment details](https://docs.payroc.com/guides/integrate/update-saved-payment-details).  
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
await client.Tokenization.SecureTokens.UpdateAccountAsync(
    new UpdateAccountSecureTokensRequest
    {
        ProcessingTerminalId = "1234001",
        SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
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

**request:** `UpdateAccountSecureTokensRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Tokenization SingleUseTokens
<details><summary><code>client.Tokenization.SingleUseTokens.<a href="/src/Payroc/Tokenization/SingleUseTokens/SingleUseTokensClient.cs">CreateAsync</a>(SingleUseTokenRequest { ... }) -> SingleUseToken</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create a single-use token that represents a customer’s payment details.  

A single-use token expires after 30 minutes and merchants can use them only once.  

**Note:** To create a reusable permanent token, go to [Create Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/create).  

In the request, send the customer’s payment details. If the request is successful, our gateway returns a token that you can use in a follow-on action, for example, [run a sale](https://docs.payroc.com/api/schema/card-payments/payments/create).
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
await client.Tokenization.SingleUseTokens.CreateAsync(
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
                                    SerialNumber = "1850010868",
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

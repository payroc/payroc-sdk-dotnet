# Reference
## Payments
<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">ListAsync</a>(Payments.ListPaymentsRequest { ... }) -> Core.PayrocPager<Payment></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](/api/pagination) list of payments. 

**Note:** If you want to view a specific payment and you have its paymentId, use our [Retrieve Payment](/api/schema/payments/get) method.  

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
        SettlementDate = new DateOnly(2024, 7, 2),
        PaymentLinkId = "JZURRJBUPS",
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

**request:** `Payments.ListPaymentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">CreateAsync</a>(Payments.PaymentRequest { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to run a sale or a pre-authorization with a customer's payment card. 

In the response, our gateway returns information about the card payment and a paymentId, which you need for the following methods:

-	[Retrieve payment](/api/schema/payments/get) - View the details of the card payment.
-	[Adjust payment](/api/schema/payments/adjust) - Update the details of the card payment.
-	[Capture payment](/api/schema/payments/capture)  - Capture the pre-authorization.
-	[Reverse payment](/api/schema/payments/reverse)  - Cancel the card payment if it's in an open batch.
-	[Refund payment](/api/schema/payments/refund)  - Run a referenced refund to return funds to the payment card.

**Payment methods** 

- **Cards** - Credit, debit, and EBT
- **Digital wallets** - [Apple Pay®](/guides/integrate/apple-pay) and [Google Pay®](/guides/integrate/google-pay) 
- **Tokens** - Secure tokens and single-use tokens

**Features** 

Our Create Payment method also supports the following features: 

- [Repeat payments](/guides/integrate/repeat-payments/use-your-own-software) - Run multiple payments as part of a payment schedule that you manage with your own software. 
- **Offline sales** - Run a sale or a pre-authorization if the terminal loses its connection to our gateway. 
- [Tokenization](h/guides/integrate/save-payment-details) - Save card details to use in future transactions. 
- [3-D Secure](/guides/integrate/3-d-secure) - Verify the identity of the cardholder. 
- [Custom fields](/guides/integrate/add-custom-fields) - Add your own data to a payment. 
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
await client.Payments.CreateAsync(
    new PaymentRequest
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Channel = PaymentRequestChannel.Web,
        ProcessingTerminalId = "1234001",
        Operator = "Jane",
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

**request:** `Payments.PaymentRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">RetrieveAsync</a>(Payments.RetrievePaymentsRequest { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a card payment.  

To retrieve a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](/api/schema/payments/create) method.  

**Note:** If you don't have the paymentId, use our [List Payments](/api/schema/payments/list) method to search for the payment.  

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
await client.Payments.RetrieveAsync(new RetrievePaymentsRequest { PaymentId = "M2MJOG6O2Y" });
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

**request:** `Payments.RetrievePaymentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">AdjustAsync</a>(Payments.PaymentAdjustment { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to adjust a payment in an open batch. 

To adjust a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](/api/schema/payments/create) method.

**Note:** If you don't have the paymentId, use our [List Payments](/api/schema/payments/list) method to search for the payment. 

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

**request:** `Payments.PaymentAdjustment` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">CaptureAsync</a>(Payments.PaymentCapture { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to capture a pre-authorization. 

To capture a pre-authorization, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](/api/schema/payments/create) method.

**Note:** If you don't have the paymentId, use our [List Payments](/api/schema/payments/list) method to search for the payment.

Depending on the amount you want to capture, complete the following:
-	**Capture the full amount of the pre-authorization** - Don't send a value for the amount parameter in your request.
-	**Capture less than the amount of the pre-authorization** - Send a value for the amount parameter in your request. 
-	**Capture more than the amount of the pre-authorization** - Adjust the pre-authorization before you capture it. For more information about adjusting a pre-authorization, go to [Adjust Payment](/api/schema/payments/adjust).

If your request is successful, our gateway takes the amount from the payment card. 

**Note:** For more information about pre-authorizations and captures, go to [Run a pre-authorization](/guides/integrate/run-a-pre-authorization).
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
        ProcessingTerminalId = "1234001",
        Operator = "Jane",
        Amount = 4999,
        Breakdown = new ItemizedBreakdown
        {
            Subtotal = 4999,
            DutyAmount = 499,
            FreightAmount = 500,
            Items = new List<LineItem>()
            {
                new LineItem { UnitPrice = 4000, Quantity = 1 },
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

**request:** `Payments.PaymentCapture` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">ReverseAsync</a>(Payments.PaymentReversal { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to cancel or to partially cancel a payment in an open batch. This is also known as voiding a payment.  

To cancel a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](/api/schema/payments/create) method.  

**Note:** If you don't have the paymentId, use our [List Payments](/api/schema/payments/list) method to search for the payment.  

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

**request:** `Payments.PaymentReversal` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">RefundAsync</a>(Payments.ReferencedRefund { ... }) -> Payment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to refund a payment that is in a closed batch.  

To refund a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](/api/schema/payments/create) method.  

**Note:** If you don't have the paymentId, use our [List Payments](/api/schema/payments/list) method to search for the payment.  

If your refund is successful, our gateway returns the payment amount to the cardholder's account.  

**Things to consider**  

- If the merchant refunds a payment that is in an open batch, our gateway reverses the payment.
- Some merchants can run unreferenced refunds, which means that they don't need a paymentId to return an amount to a customer. For more information about how to run an unreferenced refund, go to [Create Refund](/api/schema/payments/refunds/create).
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

**request:** `Payments.ReferencedRefund` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Event Subscriptions
<details><summary><code>client.EventSubscriptions.<a href="/src/Payroc/EventSubscriptions/EventSubscriptionsClient.cs">ListEventSubscriptionsAsync</a>(EventSubscriptions.ListEventSubscriptionsRequest { ... }) -> PaginatedEventSubscriptions</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve a [paginated](https://docs.payroc.com/api/pagination) list of event subscriptions that are linked to the ISV's account. 
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
await client.EventSubscriptions.ListEventSubscriptionsAsync(
    new ListEventSubscriptionsRequest { Event = "processingAccount.status.changed" }
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

**request:** `EventSubscriptions.ListEventSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EventSubscriptions.<a href="/src/Payroc/EventSubscriptions/EventSubscriptionsClient.cs">CreateEventSubscriptionAsync</a>(EventSubscriptions.CreateEventSubscriptionRequest { ... }) -> EventSubscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to subscribe to events that you want us to notify you about, for example, when we change the status of a processing account. You can subscribe to more than one event in the request. For a complete list of events you can subscribe to, go to [Events](https://docs.payroc.com/knowledge/basic-concepts/events).  
<br/>
In the response we return the ID of the event subscription, which you need for the following methods:  
- [Update event subscription](#updateEventSubscription)  
- [Partially update an event subscription](#patchEventSubscription)  
- [Retrieve an event subscription](#getEventSubscription)  
- [List event subscriptions](#listEventSubscriptions)  
- [Delete event subscription](#deleteEventSubscription)  
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
await client.EventSubscriptions.CreateEventSubscriptionAsync(
    new CreateEventSubscriptionRequest
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
            Metadata = new Dictionary<string, object>() { { "yourCustomField", "abc123" } },
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

**request:** `EventSubscriptions.CreateEventSubscriptionRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EventSubscriptions.<a href="/src/Payroc/EventSubscriptions/EventSubscriptionsClient.cs">GetEventSubscriptionAsync</a>(EventSubscriptions.GetEventSubscriptionRequest { ... }) -> EventSubscription</code></summary>
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
await client.EventSubscriptions.GetEventSubscriptionAsync(
    new GetEventSubscriptionRequest { SubscriptionId = 1 }
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

**request:** `EventSubscriptions.GetEventSubscriptionRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EventSubscriptions.<a href="/src/Payroc/EventSubscriptions/EventSubscriptionsClient.cs">UpdateEventSubscriptionAsync</a>(EventSubscriptions.UpdateEventSubscriptionRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to update an event subscription.  
You can make the following updates to the event subscription:  
  - **Event types** - Add or remove events that you have subscribed to.  
  - **Notification methods** - Update information about your endpoint and who we email if we can't contact your endpoint.  
    <br/>  
Include the subscriptionId that we sent you when you created the event subscription. We returned this in the id field.  
  
**Note:** If you don't know the subscriptionId, go to [List event subscriptions](#listEventSubscriptions)
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
await client.EventSubscriptions.UpdateEventSubscriptionAsync(
    new UpdateEventSubscriptionRequest
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
            Metadata = new Dictionary<string, object>() { { "yourCustomField", "abc123" } },
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

**request:** `EventSubscriptions.UpdateEventSubscriptionRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EventSubscriptions.<a href="/src/Payroc/EventSubscriptions/EventSubscriptionsClient.cs">DeleteEventSubscriptionAsync</a>(EventSubscriptions.DeleteEventSubscriptionRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to delete an event subscription.
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
await client.EventSubscriptions.DeleteEventSubscriptionAsync(
    new DeleteEventSubscriptionRequest { SubscriptionId = 1 }
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

**request:** `EventSubscriptions.DeleteEventSubscriptionRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EventSubscriptions.<a href="/src/Payroc/EventSubscriptions/EventSubscriptionsClient.cs">PatchEventSubscriptionAsync</a>(EventSubscriptions.PatchEventSubscriptionRequest { ... }) -> EventSubscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to partially update an event subscription. You can make the following updates to the event subscription:  
  - Add or remove events that you have subscribed to.  
  - Update information about your endpoint and who we email if we can't contact your endpoint.  
  - Turn on or turn off notifications for the event.  
    <br/>  
Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902/) standard.  
  
**Note:** You need the subscriptionId that we sent you when you created the event subscription. If you don't know the subscriptionId, go to [List event subscriptions](#listEventSubscriptions).
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
await client.EventSubscriptions.PatchEventSubscriptionAsync(
    new PatchEventSubscriptionRequest
    {
        SubscriptionId = 1,
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

**request:** `EventSubscriptions.PatchEventSubscriptionRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Auth
<details><summary><code>client.Auth.<a href="/src/Payroc/Auth/AuthClient.cs">RetrieveTokenAsync</a>(Auth.RetrieveTokenAuthRequest { ... }) -> Auth.GetTokenResponse</code></summary>
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

**request:** `Auth.RetrieveTokenAuthRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Boarding Owners
<details><summary><code>client.Boarding.Owners.<a href="/src/Payroc/Boarding/Owners/OwnersClient.cs">RetrieveAsync</a>(Boarding.Owners.RetrieveOwnersRequest { ... }) -> Owner</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

If you have the ownerId, you can use this method to retrieve the details of the owner. The response includes the owner's contact details and information about the owner's relationship to the business.  

If you don't have the ownerId, you can use the following methods to retrieve the owner's information:
  - **Owners of processing accounts** - Use the HATEOAS links that we send you when you [Retrieve a processing account](/api/schema/boarding/processing-accounts/get) or [List merchant platform's processing accounts](/api/schema/boarding/merchant-platforms/list-processing-accounts).
  - **Owners of funding recipients** - Use the [List funding recipient owners](/api/schema/funding/funding-recipients/list-owners) method. 
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

**request:** `Boarding.Owners.RetrieveOwnersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.Owners.<a href="/src/Payroc/Boarding/Owners/OwnersClient.cs">UpdateAsync</a>(Boarding.Owners.UpdateOwnersRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

> **Important:** You cannot update the owner of a processing account. 

Use this method to update the details of a funding recipient owner such as their contact details and their relationship to the business.  

Include the ownerId in the path parameter of your request. If you don’t know the ownerId, you can [Retrieve the funding recipient](/api/schema/funding/funding-recipients/get), [List funding recipients](/api/schema/funding/funding-recipients/list), or [List funding recipient owners](/api/schema/funding/funding-recipients/list-owners) to locate the ownerId. 
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

**request:** `Boarding.Owners.UpdateOwnersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.Owners.<a href="/src/Payroc/Boarding/Owners/OwnersClient.cs">DeleteAsync</a>(Boarding.Owners.DeleteOwnersRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

> **Important:** You cannot delete the owner of a processing account. 

Use this method to delete an owner from a funding recipient. You can delete an owner only if the funding recipient has more than one owner.  

Include the ownerId in the path parameter of your request. If you don’t know the ownerId, you can [Retrieve the funding recipient](/api/schema/funding/funding-recipients/get), [List funding recipients](/api/schema/funding/funding-recipients/list), or [List funding recipient owners](/api/schema/funding/funding-recipients/list-owners) to locate the ownerId.
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

**request:** `Boarding.Owners.DeleteOwnersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Boarding PricingIntents
<details><summary><code>client.Boarding.PricingIntents.<a href="/src/Payroc/Boarding/PricingIntents/PricingIntentsClient.cs">ListAsync</a>(Boarding.PricingIntents.ListPricingIntentsRequest { ... }) -> Core.PayrocPager<PricingIntent50></code></summary>
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

**request:** `Boarding.PricingIntents.ListPricingIntentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.PricingIntents.<a href="/src/Payroc/Boarding/PricingIntents/PricingIntentsClient.cs">CreateAsync</a>(Boarding.PricingIntents.CreatePricingIntentsRequest { ... }) -> PricingIntent50</code></summary>
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
            Version = "5.0",
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

**request:** `Boarding.PricingIntents.CreatePricingIntentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.PricingIntents.<a href="/src/Payroc/Boarding/PricingIntents/PricingIntentsClient.cs">RetrieveAsync</a>(Boarding.PricingIntents.RetrievePricingIntentsRequest { ... }) -> PricingIntent50</code></summary>
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

**request:** `Boarding.PricingIntents.RetrievePricingIntentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.PricingIntents.<a href="/src/Payroc/Boarding/PricingIntents/PricingIntentsClient.cs">UpdateAsync</a>(Boarding.PricingIntents.UpdatePricingIntentsRequest { ... })</code></summary>
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
            Version = "5.0",
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

**request:** `Boarding.PricingIntents.UpdatePricingIntentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.PricingIntents.<a href="/src/Payroc/Boarding/PricingIntents/PricingIntentsClient.cs">DeleteAsync</a>(Boarding.PricingIntents.DeletePricingIntentsRequest { ... })</code></summary>
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

**request:** `Boarding.PricingIntents.DeletePricingIntentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.PricingIntents.<a href="/src/Payroc/Boarding/PricingIntents/PricingIntentsClient.cs">PatchAsync</a>(Boarding.PricingIntents.PatchPricingIntentsRequest { ... }) -> PricingIntent50</code></summary>
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

**request:** `Boarding.PricingIntents.PatchPricingIntentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Boarding MerchantPlatforms
<details><summary><code>client.Boarding.MerchantPlatforms.<a href="/src/Payroc/Boarding/MerchantPlatforms/MerchantPlatformsClient.cs">ListAsync</a>(Boarding.MerchantPlatforms.ListMerchantPlatformsRequest { ... }) -> Core.PayrocPager<MerchantPlatform></code></summary>
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

**request:** `Boarding.MerchantPlatforms.ListMerchantPlatformsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.MerchantPlatforms.<a href="/src/Payroc/Boarding/MerchantPlatforms/MerchantPlatformsClient.cs">CreateAsync</a>(Boarding.MerchantPlatforms.CreateMerchantAccount { ... }) -> MerchantPlatform</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create the entity that represents a business, including its legal information and all its processing accounts.  

> **Note:** To add a processing account to an existing merchant platform, go to [Create a processing account](/api/schema/boarding/merchant-platforms/create-processing-account).  
  
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
                    new Pricing.Intent(new PricingTemplate { PricingIntentId = 6123 })
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

**request:** `Boarding.MerchantPlatforms.CreateMerchantAccount` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.MerchantPlatforms.<a href="/src/Payroc/Boarding/MerchantPlatforms/MerchantPlatformsClient.cs">RetrieveAsync</a>(Boarding.MerchantPlatforms.RetrieveMerchantPlatformsRequest { ... }) -> MerchantPlatform</code></summary>
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

**request:** `Boarding.MerchantPlatforms.RetrieveMerchantPlatformsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.MerchantPlatforms.<a href="/src/Payroc/Boarding/MerchantPlatforms/MerchantPlatformsClient.cs">ListProcessingAccountsAsync</a>(Boarding.MerchantPlatforms.ListBoardingMerchantPlatformProcessingAccountsRequest { ... }) -> Core.PayrocPager<ProcessingAccount></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve a paginated list of processing accounts associated with a merchant platform.  
  
When you created the merchant platform, we sent you its merchantPlatformId in the response. Send this merchantPlatformId as a path parameter in your endpoint.  
  
> **Note:** By default, we return only open processing accounts. To include closed processing accounts, send a value of `true` for the includeClosed query parameter.  
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

**request:** `Boarding.MerchantPlatforms.ListBoardingMerchantPlatformProcessingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.MerchantPlatforms.<a href="/src/Payroc/Boarding/MerchantPlatforms/MerchantPlatformsClient.cs">CreateProcessingAccountAsync</a>(Boarding.MerchantPlatforms.CreateProcessingAccountMerchantPlatformsRequest { ... }) -> ProcessingAccount</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create a processing account and add it to a merchant platform.  
    > **Note:** You can create and add a processing account only to an existing merchant platform. If you have not already created a merchant platform, go to [Create a merchant platform.](/api/schema/boarding/merchant-platforms/create)  
  
In the response we return a processingAccountId for the processing account, which you need for the following methods.  
- [Retrieve processing account](/api/schema/boarding/processing-accounts/get)  
- [List processing account's funding accounts](/api/schema/boarding/processing-accounts/list-funding-accounts)  
- [List contacts](/api/schema/boarding/processing-accounts/contacts)  
- [Get a processing account pricing agreement](/api/schema/boarding/processing-accounts/pricing)  
- [List owners](/api/schema/boarding/processing-accounts/list-owners)  
- [Create reminder for processing account](/api/schema/boarding/processing-accounts/create-reminder)  
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

**request:** `Boarding.MerchantPlatforms.CreateProcessingAccountMerchantPlatformsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Boarding ProcessingAccounts
<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">RetrieveAsync</a>(Boarding.ProcessingAccounts.RetrieveProcessingAccountsRequest { ... }) -> ProcessingAccount</code></summary>
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

**request:** `Boarding.ProcessingAccounts.RetrieveProcessingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">ListProcessingAccountFundingAccountsAsync</a>(Boarding.ProcessingAccounts.ListProcessingAccountFundingAccountsRequest { ... }) -> IEnumerable<FundingAccount></code></summary>
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

**request:** `Boarding.ProcessingAccounts.ListProcessingAccountFundingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">ListContactsAsync</a>(Boarding.ProcessingAccounts.ListContactsProcessingAccountsRequest { ... }) -> PaginatedContacts</code></summary>
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
await client.Boarding.ProcessingAccounts.ListContactsAsync(
    new ListContactsProcessingAccountsRequest
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

**request:** `Boarding.ProcessingAccounts.ListContactsProcessingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">GetProcessingAccountPricingAgreementAsync</a>(Boarding.ProcessingAccounts.GetProcessingAccountPricingAgreementProcessingAccountsRequest { ... }) -> OneOf<PricingAgreementUs40, PricingAgreementUs50></code></summary>
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

**request:** `Boarding.ProcessingAccounts.GetProcessingAccountPricingAgreementProcessingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">ListOwnersAsync</a>(Boarding.ProcessingAccounts.ListProcessingAccountOwnersRequest { ... }) -> Core.PayrocPager<Owner></code></summary>
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

**request:** `Boarding.ProcessingAccounts.ListProcessingAccountOwnersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">CreateReminderAsync</a>(Boarding.ProcessingAccounts.CreateReminderProcessingAccountsRequest { ... }) -> Boarding.ProcessingAccounts.CreateReminderProcessingAccountsResponse</code></summary>
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

**request:** `Boarding.ProcessingAccounts.CreateReminderProcessingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">ListTerminalOrdersAsync</a>(Boarding.ProcessingAccounts.ListTerminalOrdersProcessingAccountsRequest { ... }) -> IEnumerable<TerminalOrder></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve a list of terminal orders associated with a processing account.  
Send the processingAccountId in the path parameter of your request.
> **Note**: If you don't know the processingAccountId, go to [List merchant platform's processing accounts](#listMerchantLocations).
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

**request:** `Boarding.ProcessingAccounts.ListTerminalOrdersProcessingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">CreateTerminalOrderAsync</a>(Boarding.ProcessingAccounts.CreateTerminalOrder { ... }) -> TerminalOrder</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to order and configure terminals for a processing account. When you create an order, you specify the gateway settings, device settings, and application settings for the terminals. 
**Note**: You need the ID of the merchant's processing account before you can create an order. If you don't know the processingAccountId, go to [Retrieve a Merchant Platform.](#getMerchantAcccounts)
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
                Type = "solution",
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

**request:** `Boarding.ProcessingAccounts.CreateTerminalOrder` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingAccounts.<a href="/src/Payroc/Boarding/ProcessingAccounts/ProcessingAccountsClient.cs">ListProcessingTerminalsAsync</a>(Boarding.ProcessingAccounts.ListProcessingTerminalsProcessingAccountsRequest { ... }) -> Core.PayrocPager<ProcessingTerminal></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve a [paginated](/api/pagination) list of processing terminals associated with a processing account.  
Send the processingAccountId in the path parameter of your request.  
> **Note**: If you don't know the processingAccountId, go to [List merchant platform's processing accounts](#listMerchantLocations).
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

**request:** `Boarding.ProcessingAccounts.ListProcessingTerminalsProcessingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Boarding ProcessingTerminals
<details><summary><code>client.Boarding.ProcessingTerminals.<a href="/src/Payroc/Boarding/ProcessingTerminals/ProcessingTerminalsClient.cs">RetrieveAsync</a>(Boarding.ProcessingTerminals.RetrieveProcessingTerminalsRequest { ... }) -> ProcessingTerminal</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve the configuration for a processing terminal. The configuration includes the terminal's settings and details about how our gateway should handle transactions.
To get a processingTerminalId, go to [Retrieve Terminal Order](#getTerminalOrder) or [List Processing Terminals](#listProcessingAccountsProcessingTerminals).
> **Note**: If you want to retrieve the processor configuration for the processing terminal, go to [Retrieve Host Configuration](#getProcessingTerminalHostConfiguration). 
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

**request:** `Boarding.ProcessingTerminals.RetrieveProcessingTerminalsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.ProcessingTerminals.<a href="/src/Payroc/Boarding/ProcessingTerminals/ProcessingTerminalsClient.cs">RetrieveHostConfigurationAsync</a>(Boarding.ProcessingTerminals.RetrieveHostConfigurationProcessingTerminalsRequest { ... }) -> HostConfiguration</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve the host configuration of a processing terminal. You need this information to connect a third-party gateway to a merchant account.
> **Note**: If you need information about a merchant or a list of processing accounts associated with a merchant's account, go to [Retrieve Merchant Platform](#getMerchantAcccounts).
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

**request:** `Boarding.ProcessingTerminals.RetrieveHostConfigurationProcessingTerminalsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Boarding Contacts
<details><summary><code>client.Boarding.Contacts.<a href="/src/Payroc/Boarding/Contacts/ContactsClient.cs">RetrieveAsync</a>(Boarding.Contacts.RetrieveContactsRequest { ... }) -> Contact</code></summary>
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

**request:** `Boarding.Contacts.RetrieveContactsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.Contacts.<a href="/src/Payroc/Boarding/Contacts/ContactsClient.cs">UpdateAsync</a>(Boarding.Contacts.UpdateContactsRequest { ... })</code></summary>
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

**request:** `Boarding.Contacts.UpdateContactsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Boarding.Contacts.<a href="/src/Payroc/Boarding/Contacts/ContactsClient.cs">DeleteAsync</a>(Boarding.Contacts.DeleteContactsRequest { ... })</code></summary>
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

**request:** `Boarding.Contacts.DeleteContactsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Boarding TerminalOrders
<details><summary><code>client.Boarding.TerminalOrders.<a href="/src/Payroc/Boarding/TerminalOrders/TerminalOrdersClient.cs">RetrieveAsync</a>(Boarding.TerminalOrders.RetrieveTerminalOrdersRequest { ... }) -> TerminalOrder</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a terminal order, including the status of the order and the settings for the items in the order.
Include the terminalOrderId that we sent you when you created the terminal order.
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

**request:** `Boarding.TerminalOrders.RetrieveTerminalOrdersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Funding FundingRecipients
<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">ListAsync</a>(Funding.FundingRecipients.ListFundingRecipientsRequest { ... }) -> Core.PayrocPager<FundingRecipient></code></summary>
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

**request:** `Funding.FundingRecipients.ListFundingRecipientsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">CreateAsync</a>(Funding.FundingRecipients.CreateFundingRecipient { ... }) -> FundingRecipient</code></summary>
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

**request:** `Funding.FundingRecipients.CreateFundingRecipient` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">RetrieveAsync</a>(Funding.FundingRecipients.RetrieveFundingRecipientsRequest { ... }) -> FundingRecipient</code></summary>
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

**request:** `Funding.FundingRecipients.RetrieveFundingRecipientsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">UpdateAsync</a>(Funding.FundingRecipients.UpdateFundingRecipientsRequest { ... })</code></summary>
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

**request:** `Funding.FundingRecipients.UpdateFundingRecipientsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">DeleteAsync</a>(Funding.FundingRecipients.DeleteFundingRecipientsRequest { ... })</code></summary>
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

**request:** `Funding.FundingRecipients.DeleteFundingRecipientsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">ListAccountsAsync</a>(Funding.FundingRecipients.ListFundingRecipientFundingAccountsRequest { ... }) -> IEnumerable<FundingAccount></code></summary>
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

**request:** `Funding.FundingRecipients.ListFundingRecipientFundingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">CreateAccountAsync</a>(Funding.FundingRecipients.CreateAccountFundingRecipientsRequest { ... }) -> FundingAccount</code></summary>
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

**request:** `Funding.FundingRecipients.CreateAccountFundingRecipientsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">ListOwnersAsync</a>(Funding.FundingRecipients.ListFundingRecipientOwnersRequest { ... }) -> IEnumerable<Owner></code></summary>
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

**request:** `Funding.FundingRecipients.ListFundingRecipientOwnersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingRecipients.<a href="/src/Payroc/Funding/FundingRecipients/FundingRecipientsClient.cs">CreateOwnerAsync</a>(Funding.FundingRecipients.CreateOwnerFundingRecipientsRequest { ... }) -> Owner</code></summary>
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

**request:** `Funding.FundingRecipients.CreateOwnerFundingRecipientsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Funding FundingAccounts
<details><summary><code>client.Funding.FundingAccounts.<a href="/src/Payroc/Funding/FundingAccounts/FundingAccountsClient.cs">ListAsync</a>(Funding.FundingAccounts.ListFundingAccountsRequest { ... }) -> Core.PayrocPager<FundingAccount></code></summary>
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

**request:** `Funding.FundingAccounts.ListFundingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingAccounts.<a href="/src/Payroc/Funding/FundingAccounts/FundingAccountsClient.cs">RetrieveAsync</a>(Funding.FundingAccounts.RetrieveFundingAccountsRequest { ... }) -> FundingAccount</code></summary>
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

**request:** `Funding.FundingAccounts.RetrieveFundingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingAccounts.<a href="/src/Payroc/Funding/FundingAccounts/FundingAccountsClient.cs">UpdateAsync</a>(Funding.FundingAccounts.UpdateFundingAccountsRequest { ... })</code></summary>
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

**request:** `Funding.FundingAccounts.UpdateFundingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingAccounts.<a href="/src/Payroc/Funding/FundingAccounts/FundingAccountsClient.cs">DeleteAsync</a>(Funding.FundingAccounts.DeleteFundingAccountsRequest { ... })</code></summary>
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

**request:** `Funding.FundingAccounts.DeleteFundingAccountsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Funding FundingInstructions
<details><summary><code>client.Funding.FundingInstructions.<a href="/src/Payroc/Funding/FundingInstructions/FundingInstructionsClient.cs">ListAsync</a>(Funding.FundingInstructions.ListFundingInstructionsRequest { ... }) -> Core.PayrocPager<Funding.FundingInstructions.ListFundingInstructionsResponseDataItem></code></summary>
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

**request:** `Funding.FundingInstructions.ListFundingInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingInstructions.<a href="/src/Payroc/Funding/FundingInstructions/FundingInstructionsClient.cs">CreateAsync</a>(Funding.FundingInstructions.CreateFundingInstructionsRequest { ... }) -> Instruction</code></summary>
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

**request:** `Funding.FundingInstructions.CreateFundingInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingInstructions.<a href="/src/Payroc/Funding/FundingInstructions/FundingInstructionsClient.cs">RetrieveAsync</a>(Funding.FundingInstructions.RetrieveFundingInstructionsRequest { ... }) -> Instruction</code></summary>
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

**request:** `Funding.FundingInstructions.RetrieveFundingInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingInstructions.<a href="/src/Payroc/Funding/FundingInstructions/FundingInstructionsClient.cs">UpdateAsync</a>(Funding.FundingInstructions.UpdateFundingInstructionsRequest { ... })</code></summary>
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

**request:** `Funding.FundingInstructions.UpdateFundingInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingInstructions.<a href="/src/Payroc/Funding/FundingInstructions/FundingInstructionsClient.cs">DeleteAsync</a>(Funding.FundingInstructions.DeleteFundingInstructionsRequest { ... })</code></summary>
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

**request:** `Funding.FundingInstructions.DeleteFundingInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Funding FundingActivity
<details><summary><code>client.Funding.FundingActivity.<a href="/src/Payroc/Funding/FundingActivity/FundingActivityClient.cs">RetrieveBalanceAsync</a>(Funding.FundingActivity.RetrieveBalanceFundingActivityRequest { ... }) -> Funding.FundingActivity.RetrieveBalanceFundingActivityResponse</code></summary>
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
await client.Funding.FundingActivity.RetrieveBalanceAsync(
    new RetrieveBalanceFundingActivityRequest
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

**request:** `Funding.FundingActivity.RetrieveBalanceFundingActivityRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Funding.FundingActivity.<a href="/src/Payroc/Funding/FundingActivity/FundingActivityClient.cs">RetrieveAsync</a>(Funding.FundingActivity.RetrieveFundingActivityRequest { ... }) -> Funding.FundingActivity.RetrieveFundingActivityResponse</code></summary>
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
await client.Funding.FundingActivity.RetrieveAsync(
    new RetrieveFundingActivityRequest
    {
        Before = "2571",
        After = "8516",
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

**request:** `Funding.FundingActivity.RetrieveFundingActivityRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Payments PaymentLinks
<details><summary><code>client.Payments.PaymentLinks.<a href="/src/Payroc/Payments/PaymentLinks/PaymentLinksClient.cs">ListAsync</a>(Payments.PaymentLinks.ListPaymentLinksRequest { ... }) -> Core.PayrocPager<PaymentLinkPaginatedListDataItem></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve a [paginated](https://docs.payroc.com/api/pagination) list of payment links for a processing terminal.
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
await client.Payments.PaymentLinks.ListAsync(
    new ListPaymentLinksRequest
    {
        ProcessingTerminalId = "1234001",
        MerchantReference = "LinkRef6543",
        RecipientName = "Sarah Hazel Hopper",
        RecipientEmail = "sarah.hopper@example.com",
        CreatedOn = new DateOnly(2024, 7, 2),
        ExpiresOn = new DateOnly(2024, 8, 2),
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

**request:** `Payments.PaymentLinks.ListPaymentLinksRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.PaymentLinks.<a href="/src/Payroc/Payments/PaymentLinks/PaymentLinksClient.cs">CreateAsync</a>(Payments.PaymentLinks.CreatePaymentLinksRequest { ... }) -> Payments.PaymentLinks.CreatePaymentLinksResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create a payment link that a customer can use to make a payment for goods or services. The request contains the following settings for the payment link:
- **type** - Indicates whether the link can be used only once or if it can be used multiple times.
- **authType** - Indicates whether the transaction is a sale or a pre-authorization.
- **paymentMethod** - Indicates the payment methods that the merchant accepts.
- **charge** - Indicates whether the merchant or the customer enters the amount for the transaction.
The response contains a paymentLinkId that you can use to [share the payment link](#sharePaymentLink) or to [retrieve information about the link](#retrievePaymentLink).
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
await client.Payments.PaymentLinks.CreateAsync(
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

**request:** `Payments.PaymentLinks.CreatePaymentLinksRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.PaymentLinks.<a href="/src/Payroc/Payments/PaymentLinks/PaymentLinksClient.cs">RetrieveAsync</a>(Payments.PaymentLinks.RetrievePaymentLinksRequest { ... }) -> Payments.PaymentLinks.RetrievePaymentLinksResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a payment link.
You need the paymentLinkId that we sent to you when you created the payment link.
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
await client.Payments.PaymentLinks.RetrieveAsync(
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

**request:** `Payments.PaymentLinks.RetrievePaymentLinksRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.PaymentLinks.<a href="/src/Payroc/Payments/PaymentLinks/PaymentLinksClient.cs">UpdateAsync</a>(Payments.PaymentLinks.UpdatePaymentLinksRequest { ... }) -> Payments.PaymentLinks.UpdatePaymentLinksResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to partially update a payment link. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.

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
await client.Payments.PaymentLinks.UpdateAsync(
    new UpdatePaymentLinksRequest
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

**request:** `Payments.PaymentLinks.UpdatePaymentLinksRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.PaymentLinks.<a href="/src/Payroc/Payments/PaymentLinks/PaymentLinksClient.cs">DeactivateAsync</a>(Payments.PaymentLinks.DeactivatePaymentLinksRequest { ... }) -> Payments.PaymentLinks.DeactivatePaymentLinksResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to deactivate a payment link. If the merchant deactivates a payment link, they can't reactivate it. To take payment, the merchant must create a new payment link.  

**Note:** After the merchant deactivates a payment link, a customer can't use the link to make a payment. 
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
await client.Payments.PaymentLinks.DeactivateAsync(
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

**request:** `Payments.PaymentLinks.DeactivatePaymentLinksRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Payments PaymentPlans
<details><summary><code>client.Payments.PaymentPlans.<a href="/src/Payroc/Payments/PaymentPlans/PaymentPlansClient.cs">ListAsync</a>(Payments.PaymentPlans.ListPaymentPlansRequest { ... }) -> Core.PayrocPager<PaymentPlan></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve a [paginated](/api/pagination) list of payment plans for a processing terminal.  

**Note:** If you want to view a specific payment plan and you have its paymentPlanId, use our [Retrieve Payment Plan](/api/schema/payments/payment-plans/get) method.  

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

**request:** `Payments.PaymentPlans.ListPaymentPlansRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.PaymentPlans.<a href="/src/Payroc/Payments/PaymentPlans/PaymentPlansClient.cs">CreateAsync</a>(Payments.PaymentPlans.CreatePaymentPlansRequest { ... }) -> PaymentPlan</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create a payment schedule that you can assign customers to.  

**Note:** This method is part of our Repeat Payments feature. To help you understand how this method works with our Subscriptions endpoints, go to [Repeat Payments](/guides/integrate/repeat-payments).  

When you create a payment plan you need to provide a unique paymentPlanId that you use to run follow-on actions:  

-	[Retrieve Payment Plan](/api/schema/payments/payment-plans/get)  - View the details of the payment plan.  
-	[Update Payment Plan](/api/schema/payments/payment-plans/update)  - Update the details of the payment plan.  
-	[Delete Payment Plan](/api/schema/payments/payment-plans/delete)  - Delete the payment plan.  
-	[Create Subscription](/api/schema/payments/subscriptions/create)  - Subscribe a customer to the payment plan.  

The request includes the following settings:  

-	**type** - Indicates if our gateway or the merchant collects payments. If the merchant manually collects payments, integrate with the [Pay Manual Subscription](/api/schema/payments/subscriptions/pay) method.  
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

**request:** `Payments.PaymentPlans.CreatePaymentPlansRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.PaymentPlans.<a href="/src/Payroc/Payments/PaymentPlans/PaymentPlansClient.cs">RetrieveAsync</a>(Payments.PaymentPlans.RetrievePaymentPlansRequest { ... }) -> PaymentPlan</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a payment plan.  

To retrieve a payment plan, you need its paymentPlanId. Our gateway returned the paymentPlanId in the response of the [Create Payment Plan](/api/schema/payments/payment-plans/create) method.  

**Note:** If you don't have the paymentPlanId, use our [List Payment Plans](/api/schema/payments/payment-plans/list) method to search for the payment plan.  

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
await client.Payments.PaymentPlans.RetrieveAsync(
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

**request:** `Payments.PaymentPlans.RetrievePaymentPlansRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.PaymentPlans.<a href="/src/Payroc/Payments/PaymentPlans/PaymentPlansClient.cs">DeleteAsync</a>(Payments.PaymentPlans.DeletePaymentPlansRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to delete a payment plan.  

> **Important:** When you delete a payment plan, you can’t recover it. You also won’t be able to add subscriptions to the payment plan.  

To delete a payment plan, you need its paymentPlanId, which you sent in the request of the [Create Payment Plan](/api/schema/payments/payment-plans/create) method.  

**Note:** If you don't have the paymentPlanId, use our [List Payment Plans](/api/schema/payments/payment-plans/list) method to search for the payment plan.  

The value you sent for the onDelete parameter when you created the payment plan indicates what happens to associated subscriptions when you delete the plan:  

  -	`complete` - Our gateway stops taking payments for the subscriptions associated with the payment plan.  
  -	`continue` - Our gateway continues to take payments for the subscriptions associated with the payment plan. To stop a subscription for a cancelled payment plan, go to the [Deactivate Subscription](/api/schema/payments/subscriptions/deactivate) method.  
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

**request:** `Payments.PaymentPlans.DeletePaymentPlansRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.PaymentPlans.<a href="/src/Payroc/Payments/PaymentPlans/PaymentPlansClient.cs">UpdateAsync</a>(Payments.PaymentPlans.UpdatePaymentPlansRequest { ... }) -> PaymentPlan</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to partially update a payment plan. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.  

To update a payment plan, you need its paymentPlanId, which you sent in the request of the [Create Payment Plan](/api/schema/payments/payment-plans/create) method.  

**Note:** If you don't have the paymentPlanId, use our [List Payment Plans](/api/schema/payments/payment-plans/list) method to search for the payment plan.  

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

**request:** `Payments.PaymentPlans.UpdatePaymentPlansRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Payments Subscriptions
<details><summary><code>client.Payments.Subscriptions.<a href="/src/Payroc/Payments/Subscriptions/SubscriptionsClient.cs">ListAsync</a>(Payments.Subscriptions.ListSubscriptionsRequest { ... }) -> Core.PayrocPager<Subscription></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](/api/pagination) list of subscriptions.  

Note: If you want to view a specific subscription and you have its subscriptionId, use our [Retrieve subscription](/api/schema/payments/subscriptions/get) method.  

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
await client.Payments.Subscriptions.ListAsync(
    new ListSubscriptionsRequest
    {
        ProcessingTerminalId = "1234001",
        CustomerName = "Sarah%20Hazel%20Hopper",
        Last4 = "7062",
        PaymentPlan = "Premium%20Club",
        EndDate = new DateOnly(2025, 7, 1),
        NextDueDate = new DateOnly(2024, 8, 1),
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

**request:** `Payments.Subscriptions.ListSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.Subscriptions.<a href="/src/Payroc/Payments/Subscriptions/SubscriptionsClient.cs">CreateAsync</a>(Payments.Subscriptions.SubscriptionRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to assign a customer to a payment plan.  

**Note:** This method is part of our Repeat Payments feature. To help you understand how this method works with our Payment plans endpoints, go to [Repeat Payments](/guides/integrate/repeat-payments).  

When you create a subscription you need to provide a unique subscriptionId that you use to run follow-on actions:  

- [Retrieve Subscription](/api/schema/payments/subscriptions/get) - View the details of the subscription.
- [Update Subscription](/api/schema/payments/subscriptions/patch) - Update the details of the subscription.
- [Deactivate Subscription](/api/schema/payments/subscriptions/deactivate) - Stop taking payments for the subscription.
- [Re-activate Subscription](/api/schema/payments/subscriptions/reactivate) - Start taking payments again for the subscription.
- [Pay Manual Subscription](/api/schema/payments/subscriptions/pay) - Manually collect a payment for the subscription.

The request includes the following settings:
- **paymentPlanId** - Unique identifier of the payment plan that the merchant wants to use.
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
        StartDate = new DateOnly(2024, 7, 2),
        EndDate = new DateOnly(2025, 7, 1),
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

**request:** `Payments.Subscriptions.SubscriptionRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.Subscriptions.<a href="/src/Payroc/Payments/Subscriptions/SubscriptionsClient.cs">RetrieveAsync</a>(Payments.Subscriptions.RetrieveSubscriptionsRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a subscription.  

To retrieve a subscription, you need its subscriptionId. You sent the subscriptionId in the request of the [Create subscription](/api/schema/payments/subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List subscriptions](/api/schema/payments/subscriptions/list) method to search for the subscription.  

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
await client.Payments.Subscriptions.RetrieveAsync(
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

**request:** `Payments.Subscriptions.RetrieveSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.Subscriptions.<a href="/src/Payroc/Payments/Subscriptions/SubscriptionsClient.cs">UpdateAsync</a>(Payments.Subscriptions.UpdateSubscriptionsRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to partially update a subscription. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.  

To update a subscription, you need its subscriptionId, which you sent in the request of the [Create subscription](/api/schema/payments/subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List subscriptions](/api/schema/payments/subscriptions/list) method to search for the payment.  

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

**request:** `Payments.Subscriptions.UpdateSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.Subscriptions.<a href="/src/Payroc/Payments/Subscriptions/SubscriptionsClient.cs">DeactivateAsync</a>(Payments.Subscriptions.DeactivateSubscriptionsRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to deactivate a subscription.  

To deactivate a subscription, you need its subscriptionId, which you sent in the request of the [Create Subscription](/api/schema/payments/subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List Subscriptions](/api/schema/payments/subscriptions/list) method to search for the subscription.  

If your request is successful, our gateway stops taking payments from the customer.  

To reactivate the subscription, use our [Reactivate Subscription](/api/schema/payments/subscriptions/reactivate) method.
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

**request:** `Payments.Subscriptions.DeactivateSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.Subscriptions.<a href="/src/Payroc/Payments/Subscriptions/SubscriptionsClient.cs">ReactivateAsync</a>(Payments.Subscriptions.ReactivateSubscriptionsRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to reactivate a subscription.  

To reactivate a subscription, you need its subscriptionId, which you sent in the request of the [Create Subscription](/api/schema/payments/subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List Subscriptions](/api/schema/payments/subscriptions/list) method to search for the subscription.  

If your request is successful, our gateway restarts taking payments from the customer.  

To deactivate the subscription, use our [Deactivate Subscription](/api/schema/payments/subscriptions/deactivate) method.
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

**request:** `Payments.Subscriptions.ReactivateSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.Subscriptions.<a href="/src/Payroc/Payments/Subscriptions/SubscriptionsClient.cs">PayAsync</a>(Payments.Subscriptions.SubscriptionPaymentRequest { ... }) -> SubscriptionPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to manually collect a payment linked to a subscription. You can manually collect a payment only if the merchant chose not to let our gateway automatically collect each payment.  

To manually collect a payment, you need the subscriptionId of the subscription that's linked to the payment. You sent the subscriptionId in the request of the [Create Subscription](/api/schema/payments/subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List Subscriptions](/api/schema/payments/subscriptions/list) method to search for the subscription.  

The request includes an order object that contains information about the amount that you want to collect.  

In the response, our gateway returns information about the payment and a paymentId. You can use the paymentId in follow-on actions with the [Payments](/api/schema/payments) endpoints or [Bank Transfer Payments](/api/schema/payments/bank-transfer-payments) endpoints.  
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

**request:** `Payments.Subscriptions.SubscriptionPaymentRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Payments SecureTokens
<details><summary><code>client.Payments.SecureTokens.<a href="/src/Payroc/Payments/SecureTokens/SecureTokensClient.cs">ListAsync</a>(Payments.SecureTokens.ListSecureTokensRequest { ... }) -> Core.PayrocPager<SecureToken></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](/api/pagination) list of secure tokens.  

**Note:** If you want to view a specific seure token and you have its secureTokenId, use our [Retrieve Secure Token](/api/schema/payments/secure-tokens/get) method.  

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

**request:** `Payments.SecureTokens.ListSecureTokensRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.SecureTokens.<a href="/src/Payroc/Payments/SecureTokens/SecureTokensClient.cs">CreateAsync</a>(Payments.SecureTokens.TokenizationRequest { ... }) -> SecureToken</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create a secure token that represents a customer's payment details.  

When you create a secure token, you need to generate and provide a secureTokenId that you use to run follow-on actions:  
- [Retrieve Secure Token](/api/schema/payments/secure-tokens/get) – View the details of the secure token.  
- [Delete Secure Token](/api/schema/payments/secure-tokens/delete) – Delete the secure token.  
- [Update Secure Token](/api/schema/payments/secure-tokens/update) – Update the details of the secure token.  
- [Update Account Details](/api/schema/payments/secure-tokens/account-update) – Update the secure token with the details from a single-use token.  

**Note:** If you don't generate a secureTokenId to identify the token, our gateway generates a unique identifier and returns it in the response.  

If the request is successful, our gateway returns a token that the merchant can use in transactions instead of the customer's sensitive payment details, for example, when they [run a sale](/api/schema/payments/create).
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

**request:** `Payments.SecureTokens.TokenizationRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.SecureTokens.<a href="/src/Payroc/Payments/SecureTokens/SecureTokensClient.cs">RetrieveAsync</a>(Payments.SecureTokens.RetrieveSecureTokensRequest { ... }) -> SecureToken</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a secure token.  

To retrieve a secure token, you need its secureTokenID, which you sent in the request of the [Create Secure Token](/api/schema/payments/secure-tokens/create) method.  

**Note:** If you don't have the secureTokenId, use our [List Secure Tokens](/api/schema/payments/secure-tokens/list) method to search for the secure token.  

Our gateway returns information about the following for each secure token in the list:  

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
await client.Payments.SecureTokens.RetrieveAsync(
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

**request:** `Payments.SecureTokens.RetrieveSecureTokensRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.SecureTokens.<a href="/src/Payroc/Payments/SecureTokens/SecureTokensClient.cs">DeleteAsync</a>(Payments.SecureTokens.DeleteSecureTokensRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to delete a secure token and its related payment details from our vault.  

To delete a secure token, you need its secureTokenId, which you sent in the request of the [Create Secure Token](/api/schema/payments/secure-tokens/create) method.  

**Note:** If you don’t have the secureTokenId, use our [List Secure Tokens](/api/schema/payments/secure-tokens/list) method to search for the secure token.  

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

**request:** `Payments.SecureTokens.DeleteSecureTokensRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.SecureTokens.<a href="/src/Payroc/Payments/SecureTokens/SecureTokensClient.cs">UpdateAsync</a>(Payments.SecureTokens.UpdateSecureTokensRequest { ... }) -> SecureToken</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to partially update a secure token. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.  

To update a secure token, you need its secureTokenId, which you sent in the request of the [Create Secure Token](https://docs.payroc.com/api/schema/payments/secure-tokens/create) method.  

**Note:** If you don't have the secureTokenId, use our [List Secure Tokens](https://docs.payroc.com/api/schema/payments/secure-tokens/list) method to search  for the payment.  

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

**request:** `Payments.SecureTokens.UpdateSecureTokensRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.SecureTokens.<a href="/src/Payroc/Payments/SecureTokens/SecureTokensClient.cs">UpdateAccountAsync</a>(Payments.SecureTokens.UpdateAccountSecureTokensRequest { ... }) -> SecureToken</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to update a secure token if you have a single-use token from Hosted Fields.  

**Note:** If you don't have a single-use token, you can update saved payment details with our [Update Secure Token](/api/resources#updateSecureToken) method. For more information about our two options to update a secure token, go to [Update saved payment details](/guides/integrate/update-saved-payment-details).  
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
await client.Payments.SecureTokens.UpdateAccountAsync(
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

**request:** `Payments.SecureTokens.UpdateAccountSecureTokensRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Payments SingleUseTokens
<details><summary><code>client.Payments.SingleUseTokens.<a href="/src/Payroc/Payments/SingleUseTokens/SingleUseTokensClient.cs">CreateAsync</a>(Payments.SingleUseTokens.SingleUseTokenRequest { ... }) -> SingleUseToken</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create a single-use token that represents a customer’s payment details.  

A single-use token expires after 30 minutes and merchants can use them only once.  

**Note:** To create a reusable permanent token, go to [Create Secure Token](https://docs.payroc.com/api/schema/payments/secure-tokens/create).  

In the request, send the customer’s payment details. If the request is successful, our gateway returns a token that you can use in a follow-on action, for example, [run a sale](https://docs.payroc.com/api/schema/payments/create).
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

**request:** `Payments.SingleUseTokens.SingleUseTokenRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Payments HostedFields
<details><summary><code>client.Payments.HostedFields.<a href="/src/Payroc/Payments/HostedFields/HostedFieldsClient.cs">CreateAsync</a>(Payments.HostedFields.HostedFieldsCreateSessionRequest { ... }) -> HostedFieldsCreateSessionResponse</code></summary>
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

**request:** `Payments.HostedFields.HostedFieldsCreateSessionRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Payments ApplePaySessions
<details><summary><code>client.Payments.ApplePaySessions.<a href="/src/Payroc/Payments/ApplePaySessions/ApplePaySessionsClient.cs">CreateAsync</a>(Payments.ApplePaySessions.ApplePaySessions { ... }) -> ApplePayResponseSession</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to start an Apple Pay session for your merchant. In the response, we return the startSessionObject that you send to Apple when you retrieve the cardholder's encrypted payment details.
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
await client.Payments.ApplePaySessions.CreateAsync(
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

**request:** `Payments.ApplePaySessions.ApplePaySessions` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Payments Refunds
<details><summary><code>client.Payments.Refunds.<a href="/src/Payroc/Payments/Refunds/RefundsClient.cs">ListAsync</a>(Payments.Refunds.ListRefundsRequest { ... }) -> Core.PayrocPager<Refund></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](/api/pagination) list of refunds.  

**Note:** If you want to view a specific refund and you have its refundId, use our [Retrieve Refund](/api/schema/payments/refunds/get) method.  

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
        SettlementDate = new DateOnly(2024, 7, 2),
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

**request:** `Payments.Refunds.ListRefundsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.Refunds.<a href="/src/Payroc/Payments/Refunds/RefundsClient.cs">CreateAsync</a>(Payments.Refunds.UnreferencedRefund { ... }) -> Refund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create an unreferenced refund. An unreferenced refund is a refund that isn't linked to a payment.  

**Note:** If you have the paymentId of the payment you want to refund, use our [Refund Payment](https://docs.payroc.com/api/schema/payments/refund) method. If you use our Refund Payment method, our gateway sends the refund amount to the customer's original payment method and links the refund to the payment.  

In the request, you must provide the customer's payment details and the refund amount.  

In the response, our gateway returns information about the refund and a refundId, which you need for the following methods:  

- [Retrieve refund](/api/schema/payments/refunds/get) - View the details of the refund.  
- [Adjust refund](/api/schema/payments/refunds/adjust) - Update the details of the refund.  
- [Reverse refund](/api/schema/payments/refunds/reverse) - Cancel the refund if it's in an open batch.  
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

**request:** `Payments.Refunds.UnreferencedRefund` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.Refunds.<a href="/src/Payroc/Payments/Refunds/RefundsClient.cs">RetrieveAsync</a>(Payments.Refunds.RetrieveRefundsRequest { ... }) -> Refund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a refund.  

To retrieve a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](/api/schema/payments/refund) method or the [Create Refund](/api/schema/payments/refunds/create) method.  

**Note:** If you don't have the refundId, use our [List Refunds](/api/schema/payments/refunds/list) method to search for the refund.  

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
await client.Payments.Refunds.RetrieveAsync(new RetrieveRefundsRequest { RefundId = "CD3HN88U9F" });
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

**request:** `Payments.Refunds.RetrieveRefundsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.Refunds.<a href="/src/Payroc/Payments/Refunds/RefundsClient.cs">AdjustAsync</a>(Payments.Refunds.RefundAdjustment { ... }) -> Refund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to adjust a refund in an open batch.  

To adjust a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](/api/schema/payments/refund) method or the [Create Refund](/api/schema/payments/refunds/create) method.  

**Note:** If you don’t have the refundId, use our [List Refunds](/api/schema/payments/refunds/list) method to search for the refund.  

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

**request:** `Payments.Refunds.RefundAdjustment` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.Refunds.<a href="/src/Payroc/Payments/Refunds/RefundsClient.cs">ReverseAsync</a>(Payments.Refunds.ReverseRefundsRequest { ... }) -> Refund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to cancel a refund in an open batch.  

To cancel a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](/api/schema/payments/refund) or [Create Refund](/api/schema/payments/refunds/create) method.  

**Note:** If you don’t have the refundId, use our [List Refunds](/api/schema/payments/refunds/list) method to search for the refund.  

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

**request:** `Payments.Refunds.ReverseRefundsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Payments Cards
<details><summary><code>client.Payments.Cards.<a href="/src/Payroc/Payments/Cards/CardsClient.cs">VerifyAsync</a>(Payments.Cards.CardVerificationRequest { ... }) -> CardVerificationResult</code></summary>
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

**request:** `Payments.Cards.CardVerificationRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.Cards.<a href="/src/Payroc/Payments/Cards/CardsClient.cs">ViewBalanceAsync</a>(Payments.Cards.BalanceInquiry { ... }) -> Balance</code></summary>
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
await client.Payments.Cards.ViewBalanceAsync(
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

**request:** `Payments.Cards.BalanceInquiry` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.Cards.<a href="/src/Payroc/Payments/Cards/CardsClient.cs">LookupBinAsync</a>(Payments.Cards.BinLookup { ... }) -> CardInfo</code></summary>
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

- **Surcharging information** - If you apply a surcharge to transactions, our gateway checks that the card supports surcharging and returns information about the surcharge. For more information about surcharging, go to [Credit card surcharging](/knowledge/card-payments/credit-card-surcharging). 
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
await client.Payments.Cards.LookupBinAsync(
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

**request:** `Payments.Cards.BinLookup` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Payments CurrencyConversion
<details><summary><code>client.Payments.CurrencyConversion.<a href="/src/Payroc/Payments/CurrencyConversion/CurrencyConversionClient.cs">RetrieveFxRatesAsync</a>(Payments.CurrencyConversion.FxRateInquiry { ... }) -> FxRate</code></summary>
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
await client.Payments.CurrencyConversion.RetrieveFxRatesAsync(
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

**request:** `Payments.CurrencyConversion.FxRateInquiry` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Payments BankTransferPayments
<details><summary><code>client.Payments.BankTransferPayments.<a href="/src/Payroc/Payments/BankTransferPayments/BankTransferPaymentsClient.cs">ListAsync</a>(Payments.BankTransferPayments.ListBankTransferPaymentsRequest { ... }) -> Core.PayrocPager<BankTransferPayment></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](/api/pagination) list of payments.  

**Note:** If you want to view a specific payment and you have its paymentId, use our [Retrieve Payment](/api/schema/payments/bank-transfer-payments/get) method.  

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
await client.Payments.BankTransferPayments.ListAsync(
    new ListBankTransferPaymentsRequest
    {
        ProcessingTerminalId = "1234001",
        OrderId = "OrderRef6543",
        NameOnAccount = "Sarah%20Hazel%20Hopper",
        Last4 = "7890",
        DateFrom = new DateTime(2024, 07, 01, 00, 00, 00, 000),
        DateTo = new DateTime(2024, 07, 31, 23, 59, 59, 000),
        SettlementDate = new DateOnly(2024, 7, 15),
        PaymentLinkId = "JZURRJBUPS",
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

**request:** `Payments.BankTransferPayments.ListBankTransferPaymentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferPayments.<a href="/src/Payroc/Payments/BankTransferPayments/BankTransferPaymentsClient.cs">CreateAsync</a>(Payments.BankTransferPayments.BankTransferPaymentRequest { ... }) -> BankTransferPayment</code></summary>
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
        CredentialOnFile = new SchemasCredentialOnFile { Tokenize = true },
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

**request:** `Payments.BankTransferPayments.BankTransferPaymentRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferPayments.<a href="/src/Payroc/Payments/BankTransferPayments/BankTransferPaymentsClient.cs">RetrieveAsync</a>(Payments.BankTransferPayments.RetrieveBankTransferPaymentsRequest { ... }) -> BankTransferPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a bank transfer payment.  

To retrieve a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](/api/schema/payments/bank-transfer-payments/create) method.  

Note: If you don’t have the paymentId, use our [List Payments](/api/schema/payments/bank-transfer-payments/list) method to search for the payment.  

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
await client.Payments.BankTransferPayments.RetrieveAsync(
    new RetrieveBankTransferPaymentsRequest { PaymentId = "M2MJOG6O2Y" }
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

**request:** `Payments.BankTransferPayments.RetrieveBankTransferPaymentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferPayments.<a href="/src/Payroc/Payments/BankTransferPayments/BankTransferPaymentsClient.cs">ReverseAsync</a>(Payments.BankTransferPayments.ReverseBankTransferPaymentsRequest { ... }) -> BankTransferPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to cancel a bank transfer payment in an open batch. This is also known as voiding a payment.  

To cancel a bank transfer payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](/api/schema/payments/bank-transfer-payments/create) method.  

**Note:** If you don't have the paymentId, use our [List Payments](/api/schema/payments/bank-transfer-payments/list) method to search for the bank transfer payment.  

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

**request:** `Payments.BankTransferPayments.ReverseBankTransferPaymentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferPayments.<a href="/src/Payroc/Payments/BankTransferPayments/BankTransferPaymentsClient.cs">RefundAsync</a>(Payments.BankTransferPayments.BankTransferReferencedRefund { ... }) -> BankTransferPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to refund a bank transfer payment that is in a closed batch.  

To refund a bank transfer payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](/api/schema/payments/bank-transfer-payments/create) method.  

**Note:** If you don’t have the paymentId, use our [List Payments](/api/schema/payments/bank-transfer-payments/list) method to search for the bank transfer payment.  

If your refund is successful, our gateway returns the payment amount to the customer's account.  

**Things to consider**  
- If the merchant refunds a bank transfer payment that is in an open batch, our gateway reverses the bank transfer payment.  
- Some merchants can run unreferenced refunds, which means that they don’t need a paymentId to return an amount to a customer. For more information about how to run an unreferenced refund, go to [Create Refund](/api/schema/payments/bank-transfer-refunds/create).  
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

**request:** `Payments.BankTransferPayments.BankTransferReferencedRefund` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferPayments.<a href="/src/Payroc/Payments/BankTransferPayments/BankTransferPaymentsClient.cs">RepresentAsync</a>(Payments.BankTransferPayments.Representment { ... }) -> BankTransferPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to re-present an ACH payment.  

To re-present a payment, you need the paymentId of the return. To get the paymentId of the return, complete the following steps:  

1.	Use our [Retrieve Payment](/api/schema/payments/bank-transfer-payments/get) method  to view the details of the original payment.  
2.	From the [returns object](/api/schema/payments/bank-transfer-payments/get#response.body.returns) in the response, get the paymentId of the return.  

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

**request:** `Payments.BankTransferPayments.Representment` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Payments BankTransferRefunds
<details><summary><code>client.Payments.BankTransferRefunds.<a href="/src/Payroc/Payments/BankTransferRefunds/BankTransferRefundsClient.cs">ListAsync</a>(Payments.BankTransferRefunds.ListBankTransferRefundsRequest { ... }) -> Core.PayrocPager<BankTransferRefund></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](/api/pagination) list of bank transfer refunds.  

**Note:** If you want to view a specific refund and you have its refundId, use our Retrieve Refund method.  

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
await client.Payments.BankTransferRefunds.ListAsync(
    new ListBankTransferRefundsRequest
    {
        ProcessingTerminalId = "1234001",
        OrderId = "OrderRef6543",
        NameOnAccount = "Sarah%20Hazel%20Hopper",
        Last4 = "7062",
        DateFrom = new DateTime(2024, 07, 01, 00, 00, 00, 000),
        DateTo = new DateTime(2024, 07, 31, 23, 59, 59, 000),
        SettlementDate = new DateOnly(2024, 7, 15),
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

**request:** `Payments.BankTransferRefunds.ListBankTransferRefundsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferRefunds.<a href="/src/Payroc/Payments/BankTransferRefunds/BankTransferRefundsClient.cs">CreateAsync</a>(Payments.BankTransferRefunds.BankTransferUnreferencedRefund { ... }) -> BankTransferRefund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to create an unreferenced refund. An unreferenced refund is a refund that isn’t linked to a bank transfer payment.  

**Note:** If you have the paymentId of the payment you want to refund, use our [Refund Payment](/api/schema/payments/bank-transfer-payments/refund) method. If you use our Refund Payment method, our gateway sends the refund amount to the customer’s original payment method and links the refund to the payment.  

In the request, you must provide the customer’s payment method and information about the order including the refund amount.  

In the response, our gateway returns information about the refund and a refundId, which you need for the following methods:  

-	[Retrieve refund](/api/schema/payments/bank-transfer-refunds/get) – View the details of the refund.  
-	[Reverse refund](/api/schema/payments/bank-transfer-refunds/reverse) – Cancel the refund if it’s in an open batch.  
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

**request:** `Payments.BankTransferRefunds.BankTransferUnreferencedRefund` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferRefunds.<a href="/src/Payroc/Payments/BankTransferRefunds/BankTransferRefundsClient.cs">RetrieveAsync</a>(Payments.BankTransferRefunds.RetrieveBankTransferRefundsRequest { ... }) -> BankTransferRefund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a refund.  

To retrieve a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](/api/schema/payments/bank-transfer-payments/refund) method or the [Create Refund](/api/schema/payments/bank-transfer-refunds/create) method.  

**Note:** If you don’t have the refundId, use our [List Refunds](/api/schema/payments/bank-transfer-refunds/list) method to search for the refund.  

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
await client.Payments.BankTransferRefunds.RetrieveAsync(
    new RetrieveBankTransferRefundsRequest { RefundId = "CD3HN88U9F" }
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

**request:** `Payments.BankTransferRefunds.RetrieveBankTransferRefundsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.BankTransferRefunds.<a href="/src/Payroc/Payments/BankTransferRefunds/BankTransferRefundsClient.cs">ReverseAsync</a>(Payments.BankTransferRefunds.ReverseBankTransferRefundsRequest { ... }) -> BankTransferRefund</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to cancel a bank transfer refund in an open batch.  

To cancel a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](/api/schema/payments/bank-transfer-payments/refund) or [Create Refund](/api/schema/payments/bank-transfer-refunds/create) method.  

**Note:** If you don’t have the refundId, use our [List Refunds](/api/schema/payments/bank-transfer-refunds/list) method to search for the refund.  

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

**request:** `Payments.BankTransferRefunds.ReverseBankTransferRefundsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Payments BankAccounts
<details><summary><code>client.Payments.BankAccounts.<a href="/src/Payroc/Payments/BankAccounts/BankAccountsClient.cs">VerifyAsync</a>(Payments.BankAccounts.BankAccountVerificationRequest { ... }) -> BankAccountVerificationResult</code></summary>
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

**request:** `Payments.BankAccounts.BankAccountVerificationRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Payments PaymentLinks SharingEvents
<details><summary><code>client.Payments.PaymentLinks.SharingEvents.<a href="/src/Payroc/Payments/PaymentLinks/SharingEvents/SharingEventsClient.cs">ListAsync</a>(Payments.PaymentLinks.SharingEvents.ListSharingEventsRequest { ... }) -> Core.PayrocPager<PaymentLinkEmailShareEvent></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve a [paginated](https://docs.payroc.com/api/pagination) list of sharing events for a payment link. A sharing event occurs when a merchant shares a payment link with a customer.
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
await client.Payments.PaymentLinks.SharingEvents.ListAsync(
    new ListSharingEventsRequest
    {
        PaymentLinkId = "JZURRJBUPS",
        RecipientName = "Sarah Hazel Hopper",
        RecipientEmail = "sarah.hopper@example.com",
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

**request:** `Payments.PaymentLinks.SharingEvents.ListSharingEventsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Payments.PaymentLinks.SharingEvents.<a href="/src/Payroc/Payments/PaymentLinks/SharingEvents/SharingEventsClient.cs">ShareAsync</a>(Payments.PaymentLinks.SharingEvents.ShareSharingEventsRequest { ... }) -> PaymentLinkEmailShareEvent</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to email a payment link that the merchant has already created.
**Note:** To create a payment link, go to [Create payment link](#createPaymentLink).
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
await client.Payments.PaymentLinks.SharingEvents.ShareAsync(
    new ShareSharingEventsRequest
    {
        PaymentLinkId = "JZURRJBUPS",
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Body = new PaymentLinkEmailShareEvent
        {
            SharingMethod = "email",
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

**request:** `Payments.PaymentLinks.SharingEvents.ShareSharingEventsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## PayrocCloud PaymentInstructions
<details><summary><code>client.PayrocCloud.PaymentInstructions.<a href="/src/Payroc/PayrocCloud/PaymentInstructions/PaymentInstructionsClient.cs">SendAsync</a>(PayrocCloud.PaymentInstructions.PaymentInstructionRequest { ... }) -> PaymentInstruction</code></summary>
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

**request:** `PayrocCloud.PaymentInstructions.PaymentInstructionRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.PayrocCloud.PaymentInstructions.<a href="/src/Payroc/PayrocCloud/PaymentInstructions/PaymentInstructionsClient.cs">RetrieveAsync</a>(PayrocCloud.PaymentInstructions.RetrievePaymentInstructionsRequest { ... }) -> PaymentInstruction</code></summary>
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

**request:** `PayrocCloud.PaymentInstructions.RetrievePaymentInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## PayrocCloud RefundInstructions
<details><summary><code>client.PayrocCloud.RefundInstructions.<a href="/src/Payroc/PayrocCloud/RefundInstructions/RefundInstructionsClient.cs">SendAsync</a>(PayrocCloud.RefundInstructions.RefundInstructionRequest { ... }) -> RefundInstruction</code></summary>
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

**request:** `PayrocCloud.RefundInstructions.RefundInstructionRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.PayrocCloud.RefundInstructions.<a href="/src/Payroc/PayrocCloud/RefundInstructions/RefundInstructionsClient.cs">RetrieveAsync</a>(PayrocCloud.RefundInstructions.RetrieveRefundInstructionsRequest { ... }) -> RefundInstruction</code></summary>
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

**request:** `PayrocCloud.RefundInstructions.RetrieveRefundInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Reporting Settlement
<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">ListBatchesAsync</a>(Reporting.Settlement.ListReportingSettlementBatchesRequest { ... }) -> Core.PayrocPager<Batch></code></summary>
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

**request:** `Reporting.Settlement.ListReportingSettlementBatchesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">RetrieveBatchAsync</a>(Reporting.Settlement.RetrieveBatchSettlementRequest { ... }) -> Batch</code></summary>
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

**request:** `Reporting.Settlement.RetrieveBatchSettlementRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">ListTransactionsAsync</a>(Reporting.Settlement.ListReportingSettlementTransactionsRequest { ... }) -> Core.PayrocPager<Transaction></code></summary>
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
        Date = new DateOnly(2024, 7, 1),
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

**request:** `Reporting.Settlement.ListReportingSettlementTransactionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">RetrieveTransactionAsync</a>(Reporting.Settlement.RetrieveTransactionSettlementRequest { ... }) -> Transaction</code></summary>
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

**request:** `Reporting.Settlement.RetrieveTransactionSettlementRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">ListAuthorizationsAsync</a>(Reporting.Settlement.ListReportingSettlementAuthorizationsRequest { ... }) -> Core.PayrocPager<Authorization></code></summary>
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
        Date = new DateOnly(2024, 7, 1),
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

**request:** `Reporting.Settlement.ListReportingSettlementAuthorizationsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">RetrieveAuthorizationAsync</a>(Reporting.Settlement.RetrieveAuthorizationSettlementRequest { ... }) -> Authorization</code></summary>
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

**request:** `Reporting.Settlement.RetrieveAuthorizationSettlementRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">ListDisputesAsync</a>(Reporting.Settlement.ListReportingSettlementDisputesRequest { ... }) -> Core.PayrocPager<Dispute></code></summary>
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

**request:** `Reporting.Settlement.ListReportingSettlementDisputesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">RetrieveDisputesStatusesAsync</a>(Reporting.Settlement.RetrieveDisputesStatusesSettlementRequest { ... }) -> IEnumerable<DisputeStatus></code></summary>
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
await client.Reporting.Settlement.RetrieveDisputesStatusesAsync(
    new RetrieveDisputesStatusesSettlementRequest { DisputeId = 1 }
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

**request:** `Reporting.Settlement.RetrieveDisputesStatusesSettlementRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">ListAchDepositsAsync</a>(Reporting.Settlement.ListReportingSettlementAchDepositsRequest { ... }) -> Core.PayrocPager<AchDeposit></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a list of ACH deposits.
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

**request:** `Reporting.Settlement.ListReportingSettlementAchDepositsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">RetrieveAchDepositAsync</a>(Reporting.Settlement.RetrieveAchDepositSettlementRequest { ... }) -> AchDeposit</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a specific ACH deposit.
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
    new RetrieveAchDepositSettlementRequest { AchDepositId = 99 }
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

**request:** `Reporting.Settlement.RetrieveAchDepositSettlementRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">ListAchDepositFeesAsync</a>(Reporting.Settlement.ListReportingSettlementAchDepositFeesRequest { ... }) -> Core.PayrocPager<AchDepositFee></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve a list of ACH deposit fees.
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
        Date = new DateOnly(2024, 7, 2),
        AchDepositId = 99,
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

**request:** `Reporting.Settlement.ListReportingSettlementAchDepositFeesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

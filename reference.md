# Reference
## Payments
<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">ListAsync</a>(Payments.ListPaymentsRequest { ... }) -> Core.PayrocPager<RetrievedPayment></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payments. 

**Note:** If you want to view the details of a specific payment and you have its paymentId, use our [Retrieve Payment](https://docs.payroc.com/api/schema/payments/retrieve) method.  

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

-	[Retrieve payment](https://docs.payroc.com/api/schema/payments/retrieve) - View the details of the card payment.
-	[Adjust payment](https://docs.payroc.com/api/schema/payments/adjust) - Update the details of the card payment.
-	[Capture payment](https://docs.payroc.com/api/schema/payments/capture)  - Capture the pre-authorization.
-	[Reverse payment](https://docs.payroc.com/api/schema/payments/reverse)  - Cancel the card payment if it's in an open batch.
-	[Refund payment](https://docs.payroc.com/api/schema/payments/refund)  - Run a referenced refund to return funds to the payment card.

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

<details><summary><code>client.Payments.<a href="/src/Payroc/Payments/PaymentsClient.cs">RetrieveAsync</a>(Payments.RetrievePaymentsRequest { ... }) -> RetrievedPayment</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a card payment.  

To retrieve a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/create) method.  

**Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/list) method to search for the payment.  

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

To adjust a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/create) method.

**Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/list) method to search for the payment. 

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
                new PaymentAdjustmentAdjustmentsItem.Order(new OrderAdjustment { Amount = 4999 })
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

To capture a pre-authorization, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/create) method.

**Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/list) method to search for the payment.

Depending on the amount you want to capture, complete the following:
-	**Capture the full amount of the pre-authorization** - Don't send a value for the amount parameter in your request.
-	**Capture less than the amount of the pre-authorization** - Send a value for the amount parameter in your request. 
-	**Capture more than the amount of the pre-authorization** - Adjust the pre-authorization before you capture it. For more information about adjusting a pre-authorization, go to [Adjust Payment](https://docs.payroc.com/api/schema/payments/adjust).

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

To cancel a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/create) method.  

**Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/list) method to search for the payment.  

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

To refund a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/create) method.  

**Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/list) method to search for the payment.  

If your refund is successful, our gateway returns the payment amount to the cardholder's account.  

**Things to consider**  

- If the merchant refunds a payment that is in an open batch, our gateway reverses the payment.
- Some merchants can run unreferenced refunds, which means that they don't need a paymentId to return an amount to a customer. For more information about how to run an unreferenced refund, go to [Create Refund](https://docs.payroc.com/api/schema/payments/refunds/create).
</dd>
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

## Notifications EventSubscriptions
<details><summary><code>client.Notifications.EventSubscriptions.<a href="/src/Payroc/Notifications/EventSubscriptions/EventSubscriptionsClient.cs">ListAsync</a>(Notifications.EventSubscriptions.ListEventSubscriptionsRequest { ... }) -> Core.PayrocPager<EventSubscription></code></summary>
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

**request:** `Notifications.EventSubscriptions.ListEventSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Notifications.EventSubscriptions.<a href="/src/Payroc/Notifications/EventSubscriptions/EventSubscriptionsClient.cs">CreateAsync</a>(Notifications.EventSubscriptions.CreateEventSubscriptionsRequest { ... }) -> EventSubscription</code></summary>
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

**request:** `Notifications.EventSubscriptions.CreateEventSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Notifications.EventSubscriptions.<a href="/src/Payroc/Notifications/EventSubscriptions/EventSubscriptionsClient.cs">RetrieveAsync</a>(Notifications.EventSubscriptions.RetrieveEventSubscriptionsRequest { ... }) -> EventSubscription</code></summary>
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

**request:** `Notifications.EventSubscriptions.RetrieveEventSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Notifications.EventSubscriptions.<a href="/src/Payroc/Notifications/EventSubscriptions/EventSubscriptionsClient.cs">UpdateAsync</a>(Notifications.EventSubscriptions.UpdateEventSubscriptionsRequest { ... })</code></summary>
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

**request:** `Notifications.EventSubscriptions.UpdateEventSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Notifications.EventSubscriptions.<a href="/src/Payroc/Notifications/EventSubscriptions/EventSubscriptionsClient.cs">DeleteAsync</a>(Notifications.EventSubscriptions.DeleteEventSubscriptionsRequest { ... })</code></summary>
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

**request:** `Notifications.EventSubscriptions.DeleteEventSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Notifications.EventSubscriptions.<a href="/src/Payroc/Notifications/EventSubscriptions/EventSubscriptionsClient.cs">PartiallyUpdateAsync</a>(Notifications.EventSubscriptions.PartiallyUpdateEventSubscriptionsRequest { ... }) -> EventSubscription</code></summary>
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

**request:** `Notifications.EventSubscriptions.PartiallyUpdateEventSubscriptionsRequest` 
    
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

<details><summary><code>client.Payments.PaymentLinks.<a href="/src/Payroc/Payments/PaymentLinks/PaymentLinksClient.cs">PartiallyUpdateAsync</a>(Payments.PaymentLinks.PartiallyUpdatePaymentLinksRequest { ... }) -> Payments.PaymentLinks.PartiallyUpdatePaymentLinksResponse</code></summary>
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
await client.Payments.PaymentLinks.PartiallyUpdateAsync(
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

**request:** `Payments.PaymentLinks.PartiallyUpdatePaymentLinksRequest` 
    
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

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payment plans for a processing terminal.  

**Note:** If you want to view the details of a specific payment plan and you have its paymentPlanId, use our [Retrieve Payment Plan](https://docs.payroc.com/api/schema/payments/payment-plans/retrieve) method.  

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

**Note:** This method is part of our Repeat Payments feature. To help you understand how this method works with our Subscriptions endpoints, go to [Repeat Payments](https://docs.payroc.com/guides/integrate/repeat-payments).  

When you create a payment plan you need to provide a unique paymentPlanId that you use to run follow-on actions:  

-	[Retrieve Payment Plan](https://docs.payroc.com/api/schema/payments/payment-plans/retrieve)  - View the details of the payment plan.  
-	[Update Payment Plan](https://docs.payroc.com/api/schema/payments/payment-plans/partially-update)  - Update the details of the payment plan.  
-	[Delete Payment Plan](https://docs.payroc.com/api/schema/payments/payment-plans/delete)  - Delete the payment plan.  
-	[Create Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/create)  - Subscribe a customer to the payment plan.  

The request includes the following settings:  

-	**type** - Indicates if our gateway or the merchant collects payments. If the merchant manually collects payments, integrate with the [Pay Manual Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/pay) method.  
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

To retrieve a payment plan, you need its paymentPlanId. Our gateway returned the paymentPlanId in the response of the [Create Payment Plan](https://docs.payroc.com/api/schema/payments/payment-plans/create) method.  

**Note:** If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/payments/payment-plans/list) method to search for the payment plan.  

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

To delete a payment plan, you need its paymentPlanId, which you sent in the request of the [Create Payment Plan](https://docs.payroc.com/api/schema/payments/payment-plans/create) method.  

**Note:** If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/payments/payment-plans/list) method to search for the payment plan.  

The value you sent for the onDelete parameter when you created the payment plan indicates what happens to associated subscriptions when you delete the plan:  

  -	`complete` - Our gateway stops taking payments for the subscriptions associated with the payment plan.  
  -	`continue` - Our gateway continues to take payments for the subscriptions associated with the payment plan. To stop a subscription for a cancelled payment plan, go to the [Deactivate Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/deactivate) method.  
</dd>
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

<details><summary><code>client.Payments.PaymentPlans.<a href="/src/Payroc/Payments/PaymentPlans/PaymentPlansClient.cs">PartiallyUpdateAsync</a>(Payments.PaymentPlans.PartiallyUpdatePaymentPlansRequest { ... }) -> PaymentPlan</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to partially update a payment plan. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.  

To update a payment plan, you need its paymentPlanId, which you sent in the request of the [Create Payment Plan](https://docs.payroc.com/api/schema/payments/payment-plans/create) method.  

**Note:** If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/payments/payment-plans/list) method to search for the payment plan.  

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
await client.Payments.PaymentPlans.PartiallyUpdateAsync(
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

**request:** `Payments.PaymentPlans.PartiallyUpdatePaymentPlansRequest` 
    
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

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of subscriptions.  

Note: If you want to view the details of a specific subscription and you have its subscriptionId, use our [Retrieve subscription](https://docs.payroc.com/api/schema/payments/subscriptions/retrieve) method.  

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

**Note:** This method is part of our Repeat Payments feature. To help you understand how this method works with our Payment plans endpoints, go to [Repeat Payments](https://docs.payroc.com/guides/integrate/repeat-payments).  

When you create a subscription you need to provide a unique subscriptionId that you use to run follow-on actions:  

- [Retrieve Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/retrieve) - View the details of the subscription.
- [Update Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/partially-update) - Update the details of the subscription.
- [Deactivate Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/deactivate) - Stop taking payments for the subscription.
- [Re-activate Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/reactivate) - Start taking payments again for the subscription.
- [Pay Manual Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/pay) - Manually collect a payment for the subscription.

The request includes the following settings:
- **paymentPlanId** - Unique identifier of the payment plan that the merchant wants to use. If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/payments/payment-plans/list) method to search for the payment plan.
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

To retrieve a subscription, you need its subscriptionId. You sent the subscriptionId in the request of the [Create subscription](https://docs.payroc.com/api/schema/payments/subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List subscriptions](https://docs.payroc.com/api/schema/payments/subscriptions/list) method to search for the subscription.  

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

<details><summary><code>client.Payments.Subscriptions.<a href="/src/Payroc/Payments/Subscriptions/SubscriptionsClient.cs">PartiallyUpdateAsync</a>(Payments.Subscriptions.PartiallyUpdateSubscriptionsRequest { ... }) -> Subscription</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to partially update a subscription. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.  

To update a subscription, you need its subscriptionId, which you sent in the request of the [Create subscription](https://docs.payroc.com/api/schema/payments/subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List subscriptions](https://docs.payroc.com/api/schema/payments/subscriptions/list) method to search for the payment.  

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
await client.Payments.Subscriptions.PartiallyUpdateAsync(
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

**request:** `Payments.Subscriptions.PartiallyUpdateSubscriptionsRequest` 
    
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

To deactivate a subscription, you need its subscriptionId, which you sent in the request of the [Create Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List Subscriptions](https://docs.payroc.com/api/schema/payments/subscriptions/list) method to search for the subscription.  

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

To reactivate a subscription, you need its subscriptionId, which you sent in the request of the [Create Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List Subscriptions](https://docs.payroc.com/api/schema/payments/subscriptions/list) method to search for the subscription.  

If your request is successful, our gateway restarts taking payments from the customer.  

To deactivate the subscription, use our [Deactivate Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/deactivate) method.
</dd>
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

To manually collect a payment, you need the subscriptionId of the subscription that's linked to the payment. You sent the subscriptionId in the request of the [Create Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/create) method.  

**Note:** If you don't have the subscriptionId, use our [List Subscriptions](https://docs.payroc.com/api/schema/payments/subscriptions/list) method to search for the subscription.  

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
<details><summary><code>client.Payments.SecureTokens.<a href="/src/Payroc/Payments/SecureTokens/SecureTokensClient.cs">ListAsync</a>(Payments.SecureTokens.ListSecureTokensRequest { ... }) -> Core.PayrocPager<SecureTokenWithAccountType></code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of secure tokens.  

**Note:** If you want to view the details of a specific secure token and you have its secureTokenId, use our [Retrieve Secure Token](https://docs.payroc.com/api/schema/payments/secure-tokens/retrieve) method.  

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
- [Retrieve Secure Token](https://docs.payroc.com/api/schema/payments/secure-tokens/retrieve) – View the details of the secure token.  
- [Delete Secure Token](https://docs.payroc.com/api/schema/payments/secure-tokens/delete) – Delete the secure token.  
- [Update Secure Token](https://docs.payroc.com/api/schema/payments/secure-tokens/partially-update) – Update the details of the secure token.  
- [Update Account Details](https://docs.payroc.com/api/schema/payments/secure-tokens/update-account) – Update the secure token with the details from a single-use token.  

**Note:** If you don't generate a secureTokenId to identify the token, our gateway generates a unique identifier and returns it in the response.  

If the request is successful, our gateway returns a token that the merchant can use in transactions instead of the customer's sensitive payment details, for example, when they [run a sale](https://docs.payroc.com/api/schema/payments/create).
</dd>
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

<details><summary><code>client.Payments.SecureTokens.<a href="/src/Payroc/Payments/SecureTokens/SecureTokensClient.cs">RetrieveAsync</a>(Payments.SecureTokens.RetrieveSecureTokensRequest { ... }) -> SecureTokenWithAccountType</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Use this method to retrieve information about a secure token.  

To retrieve a secure token, you need its secureTokenID, which you sent in the request of the [Create Secure Token](https://docs.payroc.com/api/schema/payments/secure-tokens/create) method.  

**Note:** If you don't have the secureTokenId, use our [List Secure Tokens](https://docs.payroc.com/api/schema/payments/secure-tokens/list) method to search for the secure token.  

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

To delete a secure token, you need its secureTokenId, which you sent in the request of the [Create Secure Token](https://docs.payroc.com/api/schema/payments/secure-tokens/create) method.  

**Note:** If you don’t have the secureTokenId, use our [List Secure Tokens](https://docs.payroc.com/api/schema/payments/secure-tokens/list) method to search for the secure token.  

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

<details><summary><code>client.Payments.SecureTokens.<a href="/src/Payroc/Payments/SecureTokens/SecureTokensClient.cs">PartiallyUpdateAsync</a>(Payments.SecureTokens.PartiallyUpdateSecureTokensRequest { ... }) -> SecureToken</code></summary>
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
await client.Payments.SecureTokens.PartiallyUpdateAsync(
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

**request:** `Payments.SecureTokens.PartiallyUpdateSecureTokensRequest` 
    
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

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of refunds.  

**Note:** If you want to view the details of a specific refund and you have its refundId, use our [Retrieve Refund](https://docs.payroc.com/api/schema/payments/refunds/retrieve) method.  

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

- [Retrieve refund](https://docs.payroc.com/api/schema/payments/refunds/retrieve) - View the details of the refund.  
- [Adjust refund](https://docs.payroc.com/api/schema/payments/refunds/adjust) - Update the details of the refund.  
- [Reverse refund](https://docs.payroc.com/api/schema/payments/refunds/reverse) - Cancel the refund if it's in an open batch.  
</dd>
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

To retrieve a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](https://docs.payroc.com/api/schema/payments/refund) method or the [Create Refund](https://docs.payroc.com/api/schema/payments/refunds/create) method.  

**Note:** If you don't have the refundId, use our [List Refunds](https://docs.payroc.com/api/schema/payments/refunds/list) method to search for the refund.  

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

To adjust a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](https://docs.payroc.com/api/schema/payments/refund) method or the [Create Refund](https://docs.payroc.com/api/schema/payments/refunds/create) method.  

**Note:** If you don’t have the refundId, use our [List Refunds](https://docs.payroc.com/api/schema/payments/refunds/list) method to search for the refund.  

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

To cancel a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](https://docs.payroc.com/api/schema/payments/refund) or [Create Refund](https://docs.payroc.com/api/schema/payments/refunds/create) method.  

**Note:** If you don’t have the refundId, use our [List Refunds](https://docs.payroc.com/api/schema/payments/refunds/list) method to search for the refund.  

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

> **Important:** There are restrictions on which merchants can use this method. For more information, go to [Dynamic Currency Conversion](https://docs.payroc.com/knowledge/card-payments/dynamic-currency-conversion).  

Use this method to check if a card is eligible for Dynamic Currency Conversion (DCC) and to retrieve the conversion rate for a transaction amount. DCC provides a customer with the option to use their card's currency instead of the merchant's currency, for example, in Ireland, an American customer can pay in US dollars instead of Euros.  

The request includes the following:  

- **Payment method** - Card information, a secure token, or digital wallet.  
- **Transaction information** - Amount and currency of the transaction in the merchant's currency.  

If the card is eligible for DCC, our gateway returns the transaction amount in the card's currency and a dccOffer object that contains information about the conversion rate. The dccOffer object contains the following fields that you need when you [run a sale](https://docs.payroc.com/api/schema/payments/create) or [unreferenced refund](https://docs.payroc.com/api/schema/payments/refunds/create) with DCC:  
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

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payments.  

**Note:** If you want to view the details of a specific payment and you have its paymentId, use our [Retrieve Payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/retrieve) method.  

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

Use this method to run a sale with a customer's bank account details.  

In the response, our gateway returns information about the bank transfer payment and a paymentId, which you need for the following methods:  
-	[Retrieve payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/retrieve) - View the details of the bank transfer payment.
-	[Reverse payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/reverse) - Cancel the bank transfer payment if it's an open batch.
-	[Refund payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/refund) - Run a referenced refund to return funds to the customer's bank account.

**Payment methods**  

Our gateway accepts the following payment methods:  
-	Automated clearing house (ACH) details
-	Pre-authorized debit (PAD) details  

You can also use [secure tokens](https://docs.payroc.com/api/schema/payments/secure-tokens/overview) and [single-use tokens](https://docs.payroc.com/api/schema/payments/single-use-tokens/create) that you created from ACH details or PAD details. 
</dd>
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

To retrieve a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/create) method.  

Note: If you don’t have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/list) method to search for the payment.  

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

To cancel a bank transfer payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/create) method.  

**Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/list) method to search for the bank transfer payment.  

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

To refund a bank transfer payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/create) method.  

**Note:** If you don’t have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/list) method to search for the bank transfer payment.  

If your refund is successful, our gateway returns the payment amount to the customer's account.  

**Things to consider**  
- If the merchant refunds a bank transfer payment that is in an open batch, our gateway reverses the bank transfer payment.  
- Some merchants can run unreferenced refunds, which means that they don’t need a paymentId to return an amount to a customer. For more information about how to run an unreferenced refund, go to [Create Refund](https://docs.payroc.com/api/schema/payments/bank-transfer-refunds/create).  
</dd>
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

1.	Use our [Retrieve Payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/retrieve) method  to view the details of the original payment.  
2.	From the [returns object](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/retrieve#response.body.returns) in the response, get the paymentId of the return.  

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

Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of bank transfer refunds.  

**Note:** If you want to view the details of a specific refund and you have its refundId, use our [Retrieve Refund](https://docs.payroc.com/api/schema/payments/bank-transfer-refunds/retrieve) method.  

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

**Note:** If you have the paymentId of the payment you want to refund, use our [Refund Payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/refund) method. If you use our Refund Payment method, our gateway sends the refund amount to the customer’s original payment method and links the refund to the payment.  

In the request, you must provide the customer’s payment method and information about the order including the refund amount.  

In the response, our gateway returns information about the refund and a refundId, which you need for the following methods:  

-	[Retrieve refund](https://docs.payroc.com/api/schema/payments/bank-transfer-refunds/retrieve) – View the details of the refund.  
-	[Reverse refund](https://docs.payroc.com/api/schema/payments/bank-transfer-refunds/reverse) – Cancel the refund if it’s in an open batch.  
</dd>
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

To retrieve a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/refund) method or the [Create Refund](https://docs.payroc.com/api/schema/payments/bank-transfer-refunds/create) method.  

**Note:** If you don’t have the refundId, use our [List Refunds](https://docs.payroc.com/api/schema/payments/bank-transfer-refunds/list) method to search for the refund.  

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

To cancel a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/refund) or [Create Refund](https://docs.payroc.com/api/schema/payments/bank-transfer-refunds/create) method.  

**Note:** If you don’t have the refundId, use our [List Refunds](https://docs.payroc.com/api/schema/payments/bank-transfer-refunds/list) method to search for the refund.  

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
<details><summary><code>client.PayrocCloud.PaymentInstructions.<a href="/src/Payroc/PayrocCloud/PaymentInstructions/PaymentInstructionsClient.cs">SubmitAsync</a>(PayrocCloud.PaymentInstructions.PaymentInstructionRequest { ... }) -> PaymentInstruction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Submit an instruction request to initiate a sale on a payment device.  

In the response, our gateway returns information about the payment instruction and a paymentInstructionId, which you need for the following methods:
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

**request:** `PayrocCloud.PaymentInstructions.RetrievePaymentInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.PayrocCloud.PaymentInstructions.<a href="/src/Payroc/PayrocCloud/PaymentInstructions/PaymentInstructionsClient.cs">DeleteAsync</a>(PayrocCloud.PaymentInstructions.DeletePaymentInstructionsRequest { ... })</code></summary>
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

**request:** `PayrocCloud.PaymentInstructions.DeletePaymentInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## PayrocCloud RefundInstructions
<details><summary><code>client.PayrocCloud.RefundInstructions.<a href="/src/Payroc/PayrocCloud/RefundInstructions/RefundInstructionsClient.cs">SubmitAsync</a>(PayrocCloud.RefundInstructions.RefundInstructionRequest { ... }) -> RefundInstruction</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Submit an instruction request to initiate a refund on a payment device.  

In the response, our gateway returns information about the refund instruction and a refundInstructionId, which you need for the following methods:
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

**request:** `PayrocCloud.RefundInstructions.RetrieveRefundInstructionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.PayrocCloud.RefundInstructions.<a href="/src/Payroc/PayrocCloud/RefundInstructions/RefundInstructionsClient.cs">DeleteAsync</a>(PayrocCloud.RefundInstructions.DeleteRefundInstructionsRequest { ... })</code></summary>
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

**request:** `PayrocCloud.RefundInstructions.DeleteRefundInstructionsRequest` 
    
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

<details><summary><code>client.Reporting.Settlement.<a href="/src/Payroc/Reporting/Settlement/SettlementClient.cs">ListDisputesStatusesAsync</a>(Reporting.Settlement.ListDisputesStatusesSettlementRequest { ... }) -> IEnumerable<DisputeStatus></code></summary>
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

**request:** `Reporting.Settlement.ListDisputesStatusesSettlementRequest` 
    
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

**request:** `Reporting.Settlement.ListReportingSettlementAchDepositFeesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

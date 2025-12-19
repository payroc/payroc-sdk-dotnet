// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.CardPayments.Payments;

[JsonConverter(typeof(PaymentAdjustmentAdjustmentsItem.JsonConverter))]
[Serializable]
public record PaymentAdjustmentAdjustmentsItem
{
    internal PaymentAdjustmentAdjustmentsItem(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of PaymentAdjustmentAdjustmentsItem with <see cref="PaymentAdjustmentAdjustmentsItem.Order"/>.
    /// </summary>
    public PaymentAdjustmentAdjustmentsItem(PaymentAdjustmentAdjustmentsItem.Order value)
    {
        Type = "order";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PaymentAdjustmentAdjustmentsItem with <see cref="PaymentAdjustmentAdjustmentsItem.Status"/>.
    /// </summary>
    public PaymentAdjustmentAdjustmentsItem(PaymentAdjustmentAdjustmentsItem.Status value)
    {
        Type = "status";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PaymentAdjustmentAdjustmentsItem with <see cref="PaymentAdjustmentAdjustmentsItem.Customer"/>.
    /// </summary>
    public PaymentAdjustmentAdjustmentsItem(PaymentAdjustmentAdjustmentsItem.Customer value)
    {
        Type = "customer";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PaymentAdjustmentAdjustmentsItem with <see cref="PaymentAdjustmentAdjustmentsItem.Signature"/>.
    /// </summary>
    public PaymentAdjustmentAdjustmentsItem(PaymentAdjustmentAdjustmentsItem.Signature value)
    {
        Type = "signature";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="Type"/> is "order"
    /// </summary>
    public bool IsOrder => Type == "order";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "status"
    /// </summary>
    public bool IsStatus => Type == "status";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "customer"
    /// </summary>
    public bool IsCustomer => Type == "customer";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "signature"
    /// </summary>
    public bool IsSignature => Type == "signature";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.OrderAdjustment"/> if <see cref="Type"/> is 'order', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'order'.</exception>
    public Payroc.OrderAdjustment AsOrder() =>
        IsOrder
            ? (Payroc.OrderAdjustment)Value!
            : throw new System.Exception("PaymentAdjustmentAdjustmentsItem.Type is not 'order'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.StatusAdjustment"/> if <see cref="Type"/> is 'status', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'status'.</exception>
    public Payroc.StatusAdjustment AsStatus() =>
        IsStatus
            ? (Payroc.StatusAdjustment)Value!
            : throw new System.Exception("PaymentAdjustmentAdjustmentsItem.Type is not 'status'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.CustomerAdjustment"/> if <see cref="Type"/> is 'customer', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'customer'.</exception>
    public Payroc.CustomerAdjustment AsCustomer() =>
        IsCustomer
            ? (Payroc.CustomerAdjustment)Value!
            : throw new System.Exception("PaymentAdjustmentAdjustmentsItem.Type is not 'customer'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SignatureAdjustment"/> if <see cref="Type"/> is 'signature', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'signature'.</exception>
    public Payroc.SignatureAdjustment AsSignature() =>
        IsSignature
            ? (Payroc.SignatureAdjustment)Value!
            : throw new System.Exception(
                "PaymentAdjustmentAdjustmentsItem.Type is not 'signature'"
            );

    public T Match<T>(
        Func<Payroc.OrderAdjustment, T> onOrder,
        Func<Payroc.StatusAdjustment, T> onStatus,
        Func<Payroc.CustomerAdjustment, T> onCustomer,
        Func<Payroc.SignatureAdjustment, T> onSignature,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "order" => onOrder(AsOrder()),
            "status" => onStatus(AsStatus()),
            "customer" => onCustomer(AsCustomer()),
            "signature" => onSignature(AsSignature()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.OrderAdjustment> onOrder,
        Action<Payroc.StatusAdjustment> onStatus,
        Action<Payroc.CustomerAdjustment> onCustomer,
        Action<Payroc.SignatureAdjustment> onSignature,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "order":
                onOrder(AsOrder());
                break;
            case "status":
                onStatus(AsStatus());
                break;
            case "customer":
                onCustomer(AsCustomer());
                break;
            case "signature":
                onSignature(AsSignature());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.OrderAdjustment"/> and returns true if successful.
    /// </summary>
    public bool TryAsOrder(out Payroc.OrderAdjustment? value)
    {
        if (Type == "order")
        {
            value = (Payroc.OrderAdjustment)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.StatusAdjustment"/> and returns true if successful.
    /// </summary>
    public bool TryAsStatus(out Payroc.StatusAdjustment? value)
    {
        if (Type == "status")
        {
            value = (Payroc.StatusAdjustment)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.CustomerAdjustment"/> and returns true if successful.
    /// </summary>
    public bool TryAsCustomer(out Payroc.CustomerAdjustment? value)
    {
        if (Type == "customer")
        {
            value = (Payroc.CustomerAdjustment)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.SignatureAdjustment"/> and returns true if successful.
    /// </summary>
    public bool TryAsSignature(out Payroc.SignatureAdjustment? value)
    {
        if (Type == "signature")
        {
            value = (Payroc.SignatureAdjustment)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator PaymentAdjustmentAdjustmentsItem(
        PaymentAdjustmentAdjustmentsItem.Order value
    ) => new(value);

    public static implicit operator PaymentAdjustmentAdjustmentsItem(
        PaymentAdjustmentAdjustmentsItem.Status value
    ) => new(value);

    public static implicit operator PaymentAdjustmentAdjustmentsItem(
        PaymentAdjustmentAdjustmentsItem.Customer value
    ) => new(value);

    public static implicit operator PaymentAdjustmentAdjustmentsItem(
        PaymentAdjustmentAdjustmentsItem.Signature value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<PaymentAdjustmentAdjustmentsItem>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(PaymentAdjustmentAdjustmentsItem).IsAssignableFrom(typeToConvert);

        public override PaymentAdjustmentAdjustmentsItem Read(
            ref Utf8JsonReader reader,
            System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("type", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'type'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'type' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'type' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'type' is null");

            var value = discriminator switch
            {
                "order" => json.Deserialize<Payroc.OrderAdjustment?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.OrderAdjustment"),
                "status" => json.Deserialize<Payroc.StatusAdjustment?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.StatusAdjustment"),
                "customer" => json.Deserialize<Payroc.CustomerAdjustment?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.CustomerAdjustment"),
                "signature" => json.Deserialize<Payroc.SignatureAdjustment?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.SignatureAdjustment"),
                _ => json.Deserialize<object?>(options),
            };
            return new PaymentAdjustmentAdjustmentsItem(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PaymentAdjustmentAdjustmentsItem value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "order" => JsonSerializer.SerializeToNode(value.Value, options),
                    "status" => JsonSerializer.SerializeToNode(value.Value, options),
                    "customer" => JsonSerializer.SerializeToNode(value.Value, options),
                    "signature" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for order
    /// </summary>
    [Serializable]
    public struct Order
    {
        public Order(Payroc.OrderAdjustment value)
        {
            Value = value;
        }

        internal Payroc.OrderAdjustment Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator PaymentAdjustmentAdjustmentsItem.Order(
            Payroc.OrderAdjustment value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for status
    /// </summary>
    [Serializable]
    public struct Status
    {
        public Status(Payroc.StatusAdjustment value)
        {
            Value = value;
        }

        internal Payroc.StatusAdjustment Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator PaymentAdjustmentAdjustmentsItem.Status(
            Payroc.StatusAdjustment value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for customer
    /// </summary>
    [Serializable]
    public struct Customer
    {
        public Customer(Payroc.CustomerAdjustment value)
        {
            Value = value;
        }

        internal Payroc.CustomerAdjustment Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator PaymentAdjustmentAdjustmentsItem.Customer(
            Payroc.CustomerAdjustment value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for signature
    /// </summary>
    [Serializable]
    public struct Signature
    {
        public Signature(Payroc.SignatureAdjustment value)
        {
            Value = value;
        }

        internal Payroc.SignatureAdjustment Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator PaymentAdjustmentAdjustmentsItem.Signature(
            Payroc.SignatureAdjustment value
        ) => new(value);
    }
}

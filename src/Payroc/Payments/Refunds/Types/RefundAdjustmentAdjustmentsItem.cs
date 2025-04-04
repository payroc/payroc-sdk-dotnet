// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.Refunds;

[JsonConverter(typeof(RefundAdjustmentAdjustmentsItem.JsonConverter))]
public record RefundAdjustmentAdjustmentsItem
{
    internal RefundAdjustmentAdjustmentsItem(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of RefundAdjustmentAdjustmentsItem with <see cref="RefundAdjustmentAdjustmentsItem.Status"/>.
    /// </summary>
    public RefundAdjustmentAdjustmentsItem(RefundAdjustmentAdjustmentsItem.Status value)
    {
        Type = "status";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of RefundAdjustmentAdjustmentsItem with <see cref="RefundAdjustmentAdjustmentsItem.Customer"/>.
    /// </summary>
    public RefundAdjustmentAdjustmentsItem(RefundAdjustmentAdjustmentsItem.Customer value)
    {
        Type = "customer";
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
    /// Returns true if <see cref="Type"/> is "status"
    /// </summary>
    public bool IsStatus => Type == "status";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "customer"
    /// </summary>
    public bool IsCustomer => Type == "customer";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.StatusAdjustment"/> if <see cref="Type"/> is 'status', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'status'.</exception>
    public Payroc.StatusAdjustment AsStatus() =>
        IsStatus
            ? (Payroc.StatusAdjustment)Value!
            : throw new Exception("RefundAdjustmentAdjustmentsItem.Type is not 'status'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.CustomerAdjustment"/> if <see cref="Type"/> is 'customer', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'customer'.</exception>
    public Payroc.CustomerAdjustment AsCustomer() =>
        IsCustomer
            ? (Payroc.CustomerAdjustment)Value!
            : throw new Exception("RefundAdjustmentAdjustmentsItem.Type is not 'customer'");

    public T Match<T>(
        Func<Payroc.StatusAdjustment, T> onStatus,
        Func<Payroc.CustomerAdjustment, T> onCustomer,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "status" => onStatus(AsStatus()),
            "customer" => onCustomer(AsCustomer()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.StatusAdjustment> onStatus,
        Action<Payroc.CustomerAdjustment> onCustomer,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "status":
                onStatus(AsStatus());
                break;
            case "customer":
                onCustomer(AsCustomer());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
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

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator RefundAdjustmentAdjustmentsItem(
        RefundAdjustmentAdjustmentsItem.Status value
    ) => new(value);

    public static implicit operator RefundAdjustmentAdjustmentsItem(
        RefundAdjustmentAdjustmentsItem.Customer value
    ) => new(value);

    internal sealed class JsonConverter : JsonConverter<RefundAdjustmentAdjustmentsItem>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(RefundAdjustmentAdjustmentsItem).IsAssignableFrom(typeToConvert);

        public override RefundAdjustmentAdjustmentsItem Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
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
                "status" => json.Deserialize<Payroc.StatusAdjustment>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.StatusAdjustment"),
                "customer" => json.Deserialize<Payroc.CustomerAdjustment>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.CustomerAdjustment"),
                _ => json.Deserialize<object?>(options),
            };
            return new RefundAdjustmentAdjustmentsItem(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RefundAdjustmentAdjustmentsItem value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "status" => JsonSerializer.SerializeToNode(value.Value, options),
                    "customer" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for status
    /// </summary>
    public struct Status
    {
        public Status(Payroc.StatusAdjustment value)
        {
            Value = value;
        }

        internal Payroc.StatusAdjustment Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Status(Payroc.StatusAdjustment value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for customer
    /// </summary>
    public struct Customer
    {
        public Customer(Payroc.CustomerAdjustment value)
        {
            Value = value;
        }

        internal Payroc.CustomerAdjustment Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Customer(Payroc.CustomerAdjustment value) => new(value);
    }
}

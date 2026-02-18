// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Polymorphic object that contains tax details.
///
/// The value of the type parameter determines which variant you should use:
/// -	`amount` - Tax is a fixed amount.
/// -	`rate` - Tax is a percentage.
/// </summary>
[JsonConverter(typeof(Tax.JsonConverter))]
[Serializable]
public record Tax
{
    internal Tax(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of Tax with <see cref="Tax.Amount"/>.
    /// </summary>
    public Tax(Tax.Amount value)
    {
        Type = "amount";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of Tax with <see cref="Tax.Rate"/>.
    /// </summary>
    public Tax(Tax.Rate value)
    {
        Type = "rate";
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
    /// Returns true if <see cref="Type"/> is "amount"
    /// </summary>
    public bool IsAmount => Type == "amount";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "rate"
    /// </summary>
    public bool IsRate => Type == "rate";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.TaxAmount"/> if <see cref="Type"/> is 'amount', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'amount'.</exception>
    public Payroc.TaxAmount AsAmount() =>
        IsAmount
            ? (Payroc.TaxAmount)Value!
            : throw new System.Exception("Tax.Type is not 'amount'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.TaxRate"/> if <see cref="Type"/> is 'rate', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'rate'.</exception>
    public Payroc.TaxRate AsRate() =>
        IsRate ? (Payroc.TaxRate)Value! : throw new System.Exception("Tax.Type is not 'rate'");

    public T Match<T>(
        Func<Payroc.TaxAmount, T> onAmount,
        Func<Payroc.TaxRate, T> onRate,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "amount" => onAmount(AsAmount()),
            "rate" => onRate(AsRate()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.TaxAmount> onAmount,
        Action<Payroc.TaxRate> onRate,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "amount":
                onAmount(AsAmount());
                break;
            case "rate":
                onRate(AsRate());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.TaxAmount"/> and returns true if successful.
    /// </summary>
    public bool TryAsAmount(out Payroc.TaxAmount? value)
    {
        if (Type == "amount")
        {
            value = (Payroc.TaxAmount)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.TaxRate"/> and returns true if successful.
    /// </summary>
    public bool TryAsRate(out Payroc.TaxRate? value)
    {
        if (Type == "rate")
        {
            value = (Payroc.TaxRate)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator Tax(Tax.Amount value) => new(value);

    public static implicit operator Tax(Tax.Rate value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<Tax>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(Tax).IsAssignableFrom(typeToConvert);

        public override Tax Read(
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
                "amount" => json.Deserialize<Payroc.TaxAmount?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.TaxAmount"),
                "rate" => json.Deserialize<Payroc.TaxRate?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.TaxRate"),
                _ => json.Deserialize<object?>(options),
            };
            return new Tax(discriminator, value);
        }

        public override void Write(Utf8JsonWriter writer, Tax value, JsonSerializerOptions options)
        {
            JsonNode json =
                value.Type switch
                {
                    "amount" => JsonSerializer.SerializeToNode(value.Value, options),
                    "rate" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for amount
    /// </summary>
    [Serializable]
    public struct Amount
    {
        public Amount(Payroc.TaxAmount value)
        {
            Value = value;
        }

        internal Payroc.TaxAmount Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator Tax.Amount(Payroc.TaxAmount value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for rate
    /// </summary>
    [Serializable]
    public struct Rate
    {
        public Rate(Payroc.TaxRate value)
        {
            Value = value;
        }

        internal Payroc.TaxRate Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator Tax.Rate(Payroc.TaxRate value) => new(value);
    }
}

// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains pricing information.
/// </summary>
[JsonConverter(typeof(Pricing.JsonConverter))]
public record Pricing
{
    internal Pricing(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of Pricing with <see cref="Pricing.Intent"/>.
    /// </summary>
    public Pricing(Pricing.Intent value)
    {
        Type = "intent";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of Pricing with <see cref="Pricing.Agreement"/>.
    /// </summary>
    public Pricing(Pricing.Agreement value)
    {
        Type = "agreement";
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
    /// Returns true if <see cref="Type"/> is "intent"
    /// </summary>
    public bool IsIntent => Type == "intent";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "agreement"
    /// </summary>
    public bool IsAgreement => Type == "agreement";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PricingTemplate"/> if <see cref="Type"/> is 'intent', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'intent'.</exception>
    public Payroc.PricingTemplate AsIntent() =>
        IsIntent
            ? (Payroc.PricingTemplate)Value!
            : throw new Exception("Pricing.Type is not 'intent'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PricingAgreement"/> if <see cref="Type"/> is 'agreement', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'agreement'.</exception>
    public Payroc.PricingAgreement AsAgreement() =>
        IsAgreement
            ? (Payroc.PricingAgreement)Value!
            : throw new Exception("Pricing.Type is not 'agreement'");

    public T Match<T>(
        Func<Payroc.PricingTemplate, T> onIntent,
        Func<Payroc.PricingAgreement, T> onAgreement,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "intent" => onIntent(AsIntent()),
            "agreement" => onAgreement(AsAgreement()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.PricingTemplate> onIntent,
        Action<Payroc.PricingAgreement> onAgreement,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "intent":
                onIntent(AsIntent());
                break;
            case "agreement":
                onAgreement(AsAgreement());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PricingTemplate"/> and returns true if successful.
    /// </summary>
    public bool TryAsIntent(out Payroc.PricingTemplate? value)
    {
        if (Type == "intent")
        {
            value = (Payroc.PricingTemplate)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PricingAgreement"/> and returns true if successful.
    /// </summary>
    public bool TryAsAgreement(out Payroc.PricingAgreement? value)
    {
        if (Type == "agreement")
        {
            value = (Payroc.PricingAgreement)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator Pricing(Pricing.Intent value) => new(value);

    public static implicit operator Pricing(Pricing.Agreement value) => new(value);

    internal sealed class JsonConverter : JsonConverter<Pricing>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(Pricing).IsAssignableFrom(typeToConvert);

        public override Pricing Read(
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
                "intent" => json.Deserialize<Payroc.PricingTemplate>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.PricingTemplate"),
                "agreement" => json.Deserialize<Payroc.PricingAgreement>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.PricingAgreement"),
                _ => json.Deserialize<object?>(options),
            };
            return new Pricing(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Pricing value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "intent" => JsonSerializer.SerializeToNode(value.Value, options),
                    "agreement" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for intent
    /// </summary>
    public struct Intent
    {
        public Intent(Payroc.PricingTemplate value)
        {
            Value = value;
        }

        internal Payroc.PricingTemplate Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Intent(Payroc.PricingTemplate value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for agreement
    /// </summary>
    public struct Agreement
    {
        public Agreement(Payroc.PricingAgreement value)
        {
            Value = value;
        }

        internal Payroc.PricingAgreement Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Agreement(Payroc.PricingAgreement value) => new(value);
    }
}

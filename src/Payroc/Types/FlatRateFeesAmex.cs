// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the fees for American Express transactions.
/// </summary>
[JsonConverter(typeof(FlatRateFeesAmex.JsonConverter))]
[Serializable]
public record FlatRateFeesAmex
{
    internal FlatRateFeesAmex(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of FlatRateFeesAmex with <see cref="FlatRateFeesAmex.Direct"/>.
    /// </summary>
    public FlatRateFeesAmex(FlatRateFeesAmex.Direct value)
    {
        Type = "direct";
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
    /// Returns true if <see cref="Type"/> is "direct"
    /// </summary>
    public bool IsDirect => Type == "direct";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.FlatRateAmexDirect"/> if <see cref="Type"/> is 'direct', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'direct'.</exception>
    public Payroc.FlatRateAmexDirect AsDirect() =>
        IsDirect
            ? (Payroc.FlatRateAmexDirect)Value!
            : throw new Exception("FlatRateFeesAmex.Type is not 'direct'");

    public T Match<T>(
        Func<Payroc.FlatRateAmexDirect, T> onDirect,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "direct" => onDirect(AsDirect()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.FlatRateAmexDirect> onDirect,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "direct":
                onDirect(AsDirect());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.FlatRateAmexDirect"/> and returns true if successful.
    /// </summary>
    public bool TryAsDirect(out Payroc.FlatRateAmexDirect? value)
    {
        if (Type == "direct")
        {
            value = (Payroc.FlatRateAmexDirect)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator FlatRateFeesAmex(FlatRateFeesAmex.Direct value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<FlatRateFeesAmex>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(FlatRateFeesAmex).IsAssignableFrom(typeToConvert);

        public override FlatRateFeesAmex Read(
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
                "direct" => json.Deserialize<Payroc.FlatRateAmexDirect>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.FlatRateAmexDirect"),
                _ => json.Deserialize<object?>(options),
            };
            return new FlatRateFeesAmex(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            FlatRateFeesAmex value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "direct" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for direct
    /// </summary>
    [Serializable]
    public struct Direct
    {
        public Direct(Payroc.FlatRateAmexDirect value)
        {
            Value = value;
        }

        internal Payroc.FlatRateAmexDirect Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Direct(Payroc.FlatRateAmexDirect value) => new(value);
    }
}

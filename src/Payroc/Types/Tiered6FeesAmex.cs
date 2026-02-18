// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Polymorphic object that contains fees for American Express transactions.
///
/// The value of the type field determines which variant you should use:
/// -	`optBlue` - Amex OptBlue pricing program.
/// -	`direct` - Amex Direct pricing program.
/// </summary>
[JsonConverter(typeof(Tiered6FeesAmex.JsonConverter))]
[Serializable]
public record Tiered6FeesAmex
{
    internal Tiered6FeesAmex(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of Tiered6FeesAmex with <see cref="Tiered6FeesAmex.OptBlue"/>.
    /// </summary>
    public Tiered6FeesAmex(Tiered6FeesAmex.OptBlue value)
    {
        Type = "optBlue";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of Tiered6FeesAmex with <see cref="Tiered6FeesAmex.Direct"/>.
    /// </summary>
    public Tiered6FeesAmex(Tiered6FeesAmex.Direct value)
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
    /// Returns true if <see cref="Type"/> is "optBlue"
    /// </summary>
    public bool IsOptBlue => Type == "optBlue";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "direct"
    /// </summary>
    public bool IsDirect => Type == "direct";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.Tiered6AmexOptBlue"/> if <see cref="Type"/> is 'optBlue', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'optBlue'.</exception>
    public Payroc.Tiered6AmexOptBlue AsOptBlue() =>
        IsOptBlue
            ? (Payroc.Tiered6AmexOptBlue)Value!
            : throw new System.Exception("Tiered6FeesAmex.Type is not 'optBlue'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.Tiered6AmexDirect"/> if <see cref="Type"/> is 'direct', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'direct'.</exception>
    public Payroc.Tiered6AmexDirect AsDirect() =>
        IsDirect
            ? (Payroc.Tiered6AmexDirect)Value!
            : throw new System.Exception("Tiered6FeesAmex.Type is not 'direct'");

    public T Match<T>(
        Func<Payroc.Tiered6AmexOptBlue, T> onOptBlue,
        Func<Payroc.Tiered6AmexDirect, T> onDirect,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "optBlue" => onOptBlue(AsOptBlue()),
            "direct" => onDirect(AsDirect()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.Tiered6AmexOptBlue> onOptBlue,
        Action<Payroc.Tiered6AmexDirect> onDirect,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "optBlue":
                onOptBlue(AsOptBlue());
                break;
            case "direct":
                onDirect(AsDirect());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.Tiered6AmexOptBlue"/> and returns true if successful.
    /// </summary>
    public bool TryAsOptBlue(out Payroc.Tiered6AmexOptBlue? value)
    {
        if (Type == "optBlue")
        {
            value = (Payroc.Tiered6AmexOptBlue)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.Tiered6AmexDirect"/> and returns true if successful.
    /// </summary>
    public bool TryAsDirect(out Payroc.Tiered6AmexDirect? value)
    {
        if (Type == "direct")
        {
            value = (Payroc.Tiered6AmexDirect)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator Tiered6FeesAmex(Tiered6FeesAmex.OptBlue value) => new(value);

    public static implicit operator Tiered6FeesAmex(Tiered6FeesAmex.Direct value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<Tiered6FeesAmex>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(Tiered6FeesAmex).IsAssignableFrom(typeToConvert);

        public override Tiered6FeesAmex Read(
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
                "optBlue" => json.Deserialize<Payroc.Tiered6AmexOptBlue?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.Tiered6AmexOptBlue"),
                "direct" => json.Deserialize<Payroc.Tiered6AmexDirect?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.Tiered6AmexDirect"),
                _ => json.Deserialize<object?>(options),
            };
            return new Tiered6FeesAmex(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Tiered6FeesAmex value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "optBlue" => JsonSerializer.SerializeToNode(value.Value, options),
                    "direct" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for optBlue
    /// </summary>
    [Serializable]
    public struct OptBlue
    {
        public OptBlue(Payroc.Tiered6AmexOptBlue value)
        {
            Value = value;
        }

        internal Payroc.Tiered6AmexOptBlue Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator Tiered6FeesAmex.OptBlue(Payroc.Tiered6AmexOptBlue value) =>
            new(value);
    }

    /// <summary>
    /// Discriminated union type for direct
    /// </summary>
    [Serializable]
    public struct Direct
    {
        public Direct(Payroc.Tiered6AmexDirect value)
        {
            Value = value;
        }

        internal Payroc.Tiered6AmexDirect Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator Tiered6FeesAmex.Direct(Payroc.Tiered6AmexDirect value) =>
            new(value);
    }
}

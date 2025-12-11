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
[JsonConverter(typeof(Tiered3FeesAmex.JsonConverter))]
[Serializable]
public record Tiered3FeesAmex
{
    internal Tiered3FeesAmex(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of Tiered3FeesAmex with <see cref="Tiered3FeesAmex.OptBlue"/>.
    /// </summary>
    public Tiered3FeesAmex(Tiered3FeesAmex.OptBlue value)
    {
        Type = "optBlue";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of Tiered3FeesAmex with <see cref="Tiered3FeesAmex.Direct"/>.
    /// </summary>
    public Tiered3FeesAmex(Tiered3FeesAmex.Direct value)
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
    /// Returns the value as a <see cref="Payroc.Tiered3AmexOptBlue"/> if <see cref="Type"/> is 'optBlue', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'optBlue'.</exception>
    public Payroc.Tiered3AmexOptBlue AsOptBlue() =>
        IsOptBlue
            ? (Payroc.Tiered3AmexOptBlue)Value!
            : throw new System.Exception("Tiered3FeesAmex.Type is not 'optBlue'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.Tiered3AmexDirect"/> if <see cref="Type"/> is 'direct', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'direct'.</exception>
    public Payroc.Tiered3AmexDirect AsDirect() =>
        IsDirect
            ? (Payroc.Tiered3AmexDirect)Value!
            : throw new System.Exception("Tiered3FeesAmex.Type is not 'direct'");

    public T Match<T>(
        Func<Payroc.Tiered3AmexOptBlue, T> onOptBlue,
        Func<Payroc.Tiered3AmexDirect, T> onDirect,
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
        Action<Payroc.Tiered3AmexOptBlue> onOptBlue,
        Action<Payroc.Tiered3AmexDirect> onDirect,
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
    /// Attempts to cast the value to a <see cref="Payroc.Tiered3AmexOptBlue"/> and returns true if successful.
    /// </summary>
    public bool TryAsOptBlue(out Payroc.Tiered3AmexOptBlue? value)
    {
        if (Type == "optBlue")
        {
            value = (Payroc.Tiered3AmexOptBlue)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.Tiered3AmexDirect"/> and returns true if successful.
    /// </summary>
    public bool TryAsDirect(out Payroc.Tiered3AmexDirect? value)
    {
        if (Type == "direct")
        {
            value = (Payroc.Tiered3AmexDirect)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator Tiered3FeesAmex(Tiered3FeesAmex.OptBlue value) => new(value);

    public static implicit operator Tiered3FeesAmex(Tiered3FeesAmex.Direct value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<Tiered3FeesAmex>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(Tiered3FeesAmex).IsAssignableFrom(typeToConvert);

        public override Tiered3FeesAmex Read(
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
                "optBlue" => json.Deserialize<Payroc.Tiered3AmexOptBlue?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.Tiered3AmexOptBlue"),
                "direct" => json.Deserialize<Payroc.Tiered3AmexDirect?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.Tiered3AmexDirect"),
                _ => json.Deserialize<object?>(options),
            };
            return new Tiered3FeesAmex(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Tiered3FeesAmex value,
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
        public OptBlue(Payroc.Tiered3AmexOptBlue value)
        {
            Value = value;
        }

        internal Payroc.Tiered3AmexOptBlue Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator Tiered3FeesAmex.OptBlue(Payroc.Tiered3AmexOptBlue value) =>
            new(value);
    }

    /// <summary>
    /// Discriminated union type for direct
    /// </summary>
    [Serializable]
    public struct Direct
    {
        public Direct(Payroc.Tiered3AmexDirect value)
        {
            Value = value;
        }

        internal Payroc.Tiered3AmexDirect Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator Tiered3FeesAmex.Direct(Payroc.Tiered3AmexDirect value) =>
            new(value);
    }
}

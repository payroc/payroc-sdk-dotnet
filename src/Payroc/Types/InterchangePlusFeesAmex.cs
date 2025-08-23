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
[JsonConverter(typeof(InterchangePlusFeesAmex.JsonConverter))]
[Serializable]
public record InterchangePlusFeesAmex
{
    internal InterchangePlusFeesAmex(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of InterchangePlusFeesAmex with <see cref="InterchangePlusFeesAmex.OptBlue"/>.
    /// </summary>
    public InterchangePlusFeesAmex(InterchangePlusFeesAmex.OptBlue value)
    {
        Type = "optBlue";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of InterchangePlusFeesAmex with <see cref="InterchangePlusFeesAmex.Direct"/>.
    /// </summary>
    public InterchangePlusFeesAmex(InterchangePlusFeesAmex.Direct value)
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
    /// Returns the value as a <see cref="Payroc.InterchangePlusAmexOptBlue"/> if <see cref="Type"/> is 'optBlue', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'optBlue'.</exception>
    public Payroc.InterchangePlusAmexOptBlue AsOptBlue() =>
        IsOptBlue
            ? (Payroc.InterchangePlusAmexOptBlue)Value!
            : throw new Exception("InterchangePlusFeesAmex.Type is not 'optBlue'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.InterchangePlusAmexDirect"/> if <see cref="Type"/> is 'direct', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'direct'.</exception>
    public Payroc.InterchangePlusAmexDirect AsDirect() =>
        IsDirect
            ? (Payroc.InterchangePlusAmexDirect)Value!
            : throw new Exception("InterchangePlusFeesAmex.Type is not 'direct'");

    public T Match<T>(
        Func<Payroc.InterchangePlusAmexOptBlue, T> onOptBlue,
        Func<Payroc.InterchangePlusAmexDirect, T> onDirect,
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
        Action<Payroc.InterchangePlusAmexOptBlue> onOptBlue,
        Action<Payroc.InterchangePlusAmexDirect> onDirect,
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
    /// Attempts to cast the value to a <see cref="Payroc.InterchangePlusAmexOptBlue"/> and returns true if successful.
    /// </summary>
    public bool TryAsOptBlue(out Payroc.InterchangePlusAmexOptBlue? value)
    {
        if (Type == "optBlue")
        {
            value = (Payroc.InterchangePlusAmexOptBlue)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.InterchangePlusAmexDirect"/> and returns true if successful.
    /// </summary>
    public bool TryAsDirect(out Payroc.InterchangePlusAmexDirect? value)
    {
        if (Type == "direct")
        {
            value = (Payroc.InterchangePlusAmexDirect)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator InterchangePlusFeesAmex(
        InterchangePlusFeesAmex.OptBlue value
    ) => new(value);

    public static implicit operator InterchangePlusFeesAmex(InterchangePlusFeesAmex.Direct value) =>
        new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<InterchangePlusFeesAmex>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(InterchangePlusFeesAmex).IsAssignableFrom(typeToConvert);

        public override InterchangePlusFeesAmex Read(
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
                "optBlue" => json.Deserialize<Payroc.InterchangePlusAmexOptBlue>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.InterchangePlusAmexOptBlue"
                    ),
                "direct" => json.Deserialize<Payroc.InterchangePlusAmexDirect>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.InterchangePlusAmexDirect"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new InterchangePlusFeesAmex(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            InterchangePlusFeesAmex value,
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
        public OptBlue(Payroc.InterchangePlusAmexOptBlue value)
        {
            Value = value;
        }

        internal Payroc.InterchangePlusAmexOptBlue Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator OptBlue(Payroc.InterchangePlusAmexOptBlue value) =>
            new(value);
    }

    /// <summary>
    /// Discriminated union type for direct
    /// </summary>
    [Serializable]
    public struct Direct
    {
        public Direct(Payroc.InterchangePlusAmexDirect value)
        {
            Value = value;
        }

        internal Payroc.InterchangePlusAmexDirect Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Direct(Payroc.InterchangePlusAmexDirect value) =>
            new(value);
    }
}

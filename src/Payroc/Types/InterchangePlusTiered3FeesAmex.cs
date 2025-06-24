// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(InterchangePlusTiered3FeesAmex.JsonConverter))]
[Serializable]
public record InterchangePlusTiered3FeesAmex
{
    internal InterchangePlusTiered3FeesAmex(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of InterchangePlusTiered3FeesAmex with <see cref="InterchangePlusTiered3FeesAmex.OptBlue"/>.
    /// </summary>
    public InterchangePlusTiered3FeesAmex(InterchangePlusTiered3FeesAmex.OptBlue value)
    {
        Type = "optBlue";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of InterchangePlusTiered3FeesAmex with <see cref="InterchangePlusTiered3FeesAmex.Direct"/>.
    /// </summary>
    public InterchangePlusTiered3FeesAmex(InterchangePlusTiered3FeesAmex.Direct value)
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
    /// Returns the value as a <see cref="Payroc.InterchangePlusTiered3AmexOptBlue"/> if <see cref="Type"/> is 'optBlue', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'optBlue'.</exception>
    public Payroc.InterchangePlusTiered3AmexOptBlue AsOptBlue() =>
        IsOptBlue
            ? (Payroc.InterchangePlusTiered3AmexOptBlue)Value!
            : throw new Exception("InterchangePlusTiered3FeesAmex.Type is not 'optBlue'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.InterchangePlusTiered3AmexDirect"/> if <see cref="Type"/> is 'direct', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'direct'.</exception>
    public Payroc.InterchangePlusTiered3AmexDirect AsDirect() =>
        IsDirect
            ? (Payroc.InterchangePlusTiered3AmexDirect)Value!
            : throw new Exception("InterchangePlusTiered3FeesAmex.Type is not 'direct'");

    public T Match<T>(
        Func<Payroc.InterchangePlusTiered3AmexOptBlue, T> onOptBlue,
        Func<Payroc.InterchangePlusTiered3AmexDirect, T> onDirect,
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
        Action<Payroc.InterchangePlusTiered3AmexOptBlue> onOptBlue,
        Action<Payroc.InterchangePlusTiered3AmexDirect> onDirect,
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
    /// Attempts to cast the value to a <see cref="Payroc.InterchangePlusTiered3AmexOptBlue"/> and returns true if successful.
    /// </summary>
    public bool TryAsOptBlue(out Payroc.InterchangePlusTiered3AmexOptBlue? value)
    {
        if (Type == "optBlue")
        {
            value = (Payroc.InterchangePlusTiered3AmexOptBlue)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.InterchangePlusTiered3AmexDirect"/> and returns true if successful.
    /// </summary>
    public bool TryAsDirect(out Payroc.InterchangePlusTiered3AmexDirect? value)
    {
        if (Type == "direct")
        {
            value = (Payroc.InterchangePlusTiered3AmexDirect)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator InterchangePlusTiered3FeesAmex(
        InterchangePlusTiered3FeesAmex.OptBlue value
    ) => new(value);

    public static implicit operator InterchangePlusTiered3FeesAmex(
        InterchangePlusTiered3FeesAmex.Direct value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<InterchangePlusTiered3FeesAmex>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(InterchangePlusTiered3FeesAmex).IsAssignableFrom(typeToConvert);

        public override InterchangePlusTiered3FeesAmex Read(
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
                "optBlue" => json.Deserialize<Payroc.InterchangePlusTiered3AmexOptBlue>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.InterchangePlusTiered3AmexOptBlue"
                    ),
                "direct" => json.Deserialize<Payroc.InterchangePlusTiered3AmexDirect>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.InterchangePlusTiered3AmexDirect"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new InterchangePlusTiered3FeesAmex(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            InterchangePlusTiered3FeesAmex value,
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
        public OptBlue(Payroc.InterchangePlusTiered3AmexOptBlue value)
        {
            Value = value;
        }

        internal Payroc.InterchangePlusTiered3AmexOptBlue Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator OptBlue(Payroc.InterchangePlusTiered3AmexOptBlue value) =>
            new(value);
    }

    /// <summary>
    /// Discriminated union type for direct
    /// </summary>
    [Serializable]
    public struct Direct
    {
        public Direct(Payroc.InterchangePlusTiered3AmexDirect value)
        {
            Value = value;
        }

        internal Payroc.InterchangePlusTiered3AmexDirect Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Direct(Payroc.InterchangePlusTiered3AmexDirect value) =>
            new(value);
    }
}

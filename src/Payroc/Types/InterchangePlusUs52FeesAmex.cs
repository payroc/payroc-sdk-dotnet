// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using global::System.Text.Json;
using global::System.Text.Json.Nodes;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the fees for American Express transactions.
/// </summary>
[JsonConverter(typeof(InterchangePlusUs52FeesAmex.JsonConverter))]
[Serializable]
public record InterchangePlusUs52FeesAmex
{
    internal InterchangePlusUs52FeesAmex(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of InterchangePlusUs52FeesAmex with <see cref="InterchangePlusUs52FeesAmex.OptBlue"/>.
    /// </summary>
    public InterchangePlusUs52FeesAmex(InterchangePlusUs52FeesAmex.OptBlue value)
    {
        Type = "optBlue";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of InterchangePlusUs52FeesAmex with <see cref="InterchangePlusUs52FeesAmex.Direct"/>.
    /// </summary>
    public InterchangePlusUs52FeesAmex(InterchangePlusUs52FeesAmex.Direct value)
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
            : throw new global::System.Exception(
                "InterchangePlusUs52FeesAmex.Type is not 'optBlue'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.InterchangePlusAmexDirect"/> if <see cref="Type"/> is 'direct', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'direct'.</exception>
    public Payroc.InterchangePlusAmexDirect AsDirect() =>
        IsDirect
            ? (Payroc.InterchangePlusAmexDirect)Value!
            : throw new global::System.Exception(
                "InterchangePlusUs52FeesAmex.Type is not 'direct'"
            );

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

    public static implicit operator InterchangePlusUs52FeesAmex(
        InterchangePlusUs52FeesAmex.OptBlue value
    ) => new(value);

    public static implicit operator InterchangePlusUs52FeesAmex(
        InterchangePlusUs52FeesAmex.Direct value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<InterchangePlusUs52FeesAmex>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(InterchangePlusUs52FeesAmex).IsAssignableFrom(typeToConvert);

        public override InterchangePlusUs52FeesAmex Read(
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

            // Strip the discriminant property to prevent it from leaking into AdditionalProperties
            var jsonObject = System.Text.Json.Nodes.JsonObject.Create(json);
            jsonObject?.Remove("type");
            var jsonWithoutDiscriminator =
                jsonObject != null ? JsonSerializer.SerializeToElement(jsonObject, options) : json;

            var value = discriminator switch
            {
                "optBlue" =>
                    jsonWithoutDiscriminator.Deserialize<Payroc.InterchangePlusAmexOptBlue?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Payroc.InterchangePlusAmexOptBlue"
                        ),
                "direct" => jsonWithoutDiscriminator.Deserialize<Payroc.InterchangePlusAmexDirect?>(
                    options
                )
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.InterchangePlusAmexDirect"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new InterchangePlusUs52FeesAmex(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            InterchangePlusUs52FeesAmex value,
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

        public override InterchangePlusUs52FeesAmex ReadAsPropertyName(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new JsonException("The JSON property name could not be read as a string.");
            return new InterchangePlusUs52FeesAmex(stringValue, stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            InterchangePlusUs52FeesAmex value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Type);
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

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator InterchangePlusUs52FeesAmex.OptBlue(
            Payroc.InterchangePlusAmexOptBlue value
        ) => new(value);
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

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator InterchangePlusUs52FeesAmex.Direct(
            Payroc.InterchangePlusAmexDirect value
        ) => new(value);
    }
}

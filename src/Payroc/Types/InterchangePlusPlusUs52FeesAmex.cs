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
[JsonConverter(typeof(InterchangePlusPlusUs52FeesAmex.JsonConverter))]
[Serializable]
public record InterchangePlusPlusUs52FeesAmex
{
    internal InterchangePlusPlusUs52FeesAmex(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of InterchangePlusPlusUs52FeesAmex with <see cref="InterchangePlusPlusUs52FeesAmex.OptBlue"/>.
    /// </summary>
    public InterchangePlusPlusUs52FeesAmex(InterchangePlusPlusUs52FeesAmex.OptBlue value)
    {
        Type = "optBlue";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of InterchangePlusPlusUs52FeesAmex with <see cref="InterchangePlusPlusUs52FeesAmex.Direct"/>.
    /// </summary>
    public InterchangePlusPlusUs52FeesAmex(InterchangePlusPlusUs52FeesAmex.Direct value)
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
    /// Returns the value as a <see cref="Payroc.InterchangePlusPlusAmexOptBlue"/> if <see cref="Type"/> is 'optBlue', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'optBlue'.</exception>
    public Payroc.InterchangePlusPlusAmexOptBlue AsOptBlue() =>
        IsOptBlue
            ? (Payroc.InterchangePlusPlusAmexOptBlue)Value!
            : throw new System.Exception("InterchangePlusPlusUs52FeesAmex.Type is not 'optBlue'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.InterchangePlusPlusAmexDirect"/> if <see cref="Type"/> is 'direct', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'direct'.</exception>
    public Payroc.InterchangePlusPlusAmexDirect AsDirect() =>
        IsDirect
            ? (Payroc.InterchangePlusPlusAmexDirect)Value!
            : throw new System.Exception("InterchangePlusPlusUs52FeesAmex.Type is not 'direct'");

    public T Match<T>(
        Func<Payroc.InterchangePlusPlusAmexOptBlue, T> onOptBlue,
        Func<Payroc.InterchangePlusPlusAmexDirect, T> onDirect,
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
        Action<Payroc.InterchangePlusPlusAmexOptBlue> onOptBlue,
        Action<Payroc.InterchangePlusPlusAmexDirect> onDirect,
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
    /// Attempts to cast the value to a <see cref="Payroc.InterchangePlusPlusAmexOptBlue"/> and returns true if successful.
    /// </summary>
    public bool TryAsOptBlue(out Payroc.InterchangePlusPlusAmexOptBlue? value)
    {
        if (Type == "optBlue")
        {
            value = (Payroc.InterchangePlusPlusAmexOptBlue)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.InterchangePlusPlusAmexDirect"/> and returns true if successful.
    /// </summary>
    public bool TryAsDirect(out Payroc.InterchangePlusPlusAmexDirect? value)
    {
        if (Type == "direct")
        {
            value = (Payroc.InterchangePlusPlusAmexDirect)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator InterchangePlusPlusUs52FeesAmex(
        InterchangePlusPlusUs52FeesAmex.OptBlue value
    ) => new(value);

    public static implicit operator InterchangePlusPlusUs52FeesAmex(
        InterchangePlusPlusUs52FeesAmex.Direct value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<InterchangePlusPlusUs52FeesAmex>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(InterchangePlusPlusUs52FeesAmex).IsAssignableFrom(typeToConvert);

        public override InterchangePlusPlusUs52FeesAmex Read(
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

            // Strip the discriminant property to prevent it from leaking into AdditionalProperties
            var jsonObject = System.Text.Json.Nodes.JsonObject.Create(json);
            jsonObject?.Remove("type");
            var jsonWithoutDiscriminator =
                jsonObject != null ? JsonSerializer.SerializeToElement(jsonObject, options) : json;

            var value = discriminator switch
            {
                "optBlue" =>
                    jsonWithoutDiscriminator.Deserialize<Payroc.InterchangePlusPlusAmexOptBlue?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Payroc.InterchangePlusPlusAmexOptBlue"
                        ),
                "direct" =>
                    jsonWithoutDiscriminator.Deserialize<Payroc.InterchangePlusPlusAmexDirect?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Payroc.InterchangePlusPlusAmexDirect"
                        ),
                _ => json.Deserialize<object?>(options),
            };
            return new InterchangePlusPlusUs52FeesAmex(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            InterchangePlusPlusUs52FeesAmex value,
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
        public OptBlue(Payroc.InterchangePlusPlusAmexOptBlue value)
        {
            Value = value;
        }

        internal Payroc.InterchangePlusPlusAmexOptBlue Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator InterchangePlusPlusUs52FeesAmex.OptBlue(
            Payroc.InterchangePlusPlusAmexOptBlue value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for direct
    /// </summary>
    [Serializable]
    public struct Direct
    {
        public Direct(Payroc.InterchangePlusPlusAmexDirect value)
        {
            Value = value;
        }

        internal Payroc.InterchangePlusPlusAmexDirect Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator InterchangePlusPlusUs52FeesAmex.Direct(
            Payroc.InterchangePlusPlusAmexDirect value
        ) => new(value);
    }
}

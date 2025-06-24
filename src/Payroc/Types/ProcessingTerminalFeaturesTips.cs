// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(ProcessingTerminalFeaturesTips.JsonConverter))]
[Serializable]
public record ProcessingTerminalFeaturesTips
{
    internal ProcessingTerminalFeaturesTips(string type, object? value)
    {
        Enabled = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of ProcessingTerminalFeaturesTips with <see cref="ProcessingTerminalFeaturesTips.True"/>.
    /// </summary>
    public ProcessingTerminalFeaturesTips(ProcessingTerminalFeaturesTips.True value)
    {
        Enabled = "true";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of ProcessingTerminalFeaturesTips with <see cref="ProcessingTerminalFeaturesTips.False"/>.
    /// </summary>
    public ProcessingTerminalFeaturesTips(ProcessingTerminalFeaturesTips.False value)
    {
        Enabled = "false";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("enabled")]
    public string Enabled { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="Enabled"/> is "true"
    /// </summary>
    public bool IsTrue => Enabled == "true";

    /// <summary>
    /// Returns true if <see cref="Enabled"/> is "false"
    /// </summary>
    public bool IsFalse => Enabled == "false";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.TipProcessingEnabled"/> if <see cref="Enabled"/> is 'true', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Enabled"/> is not 'true'.</exception>
    public Payroc.TipProcessingEnabled AsTrue() =>
        IsTrue
            ? (Payroc.TipProcessingEnabled)Value!
            : throw new Exception("ProcessingTerminalFeaturesTips.Enabled is not 'true'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.TipProcessingDisabled"/> if <see cref="Enabled"/> is 'false', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Enabled"/> is not 'false'.</exception>
    public Payroc.TipProcessingDisabled AsFalse() =>
        IsFalse
            ? (Payroc.TipProcessingDisabled)Value!
            : throw new Exception("ProcessingTerminalFeaturesTips.Enabled is not 'false'");

    public T Match<T>(
        Func<Payroc.TipProcessingEnabled, T> onTrue,
        Func<Payroc.TipProcessingDisabled, T> onFalse,
        Func<string, object?, T> onUnknown_
    )
    {
        return Enabled switch
        {
            "true" => onTrue(AsTrue()),
            "false" => onFalse(AsFalse()),
            _ => onUnknown_(Enabled, Value),
        };
    }

    public void Visit(
        Action<Payroc.TipProcessingEnabled> onTrue,
        Action<Payroc.TipProcessingDisabled> onFalse,
        Action<string, object?> onUnknown_
    )
    {
        switch (Enabled)
        {
            case "true":
                onTrue(AsTrue());
                break;
            case "false":
                onFalse(AsFalse());
                break;
            default:
                onUnknown_(Enabled, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.TipProcessingEnabled"/> and returns true if successful.
    /// </summary>
    public bool TryAsTrue(out Payroc.TipProcessingEnabled? value)
    {
        if (Enabled == "true")
        {
            value = (Payroc.TipProcessingEnabled)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.TipProcessingDisabled"/> and returns true if successful.
    /// </summary>
    public bool TryAsFalse(out Payroc.TipProcessingDisabled? value)
    {
        if (Enabled == "false")
        {
            value = (Payroc.TipProcessingDisabled)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator ProcessingTerminalFeaturesTips(
        ProcessingTerminalFeaturesTips.True value
    ) => new(value);

    public static implicit operator ProcessingTerminalFeaturesTips(
        ProcessingTerminalFeaturesTips.False value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<ProcessingTerminalFeaturesTips>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(ProcessingTerminalFeaturesTips).IsAssignableFrom(typeToConvert);

        public override ProcessingTerminalFeaturesTips Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("enabled", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'enabled'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'enabled' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'enabled' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'enabled' is null");

            var value = discriminator switch
            {
                "true" => json.Deserialize<Payroc.TipProcessingEnabled>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.TipProcessingEnabled"),
                "false" => json.Deserialize<Payroc.TipProcessingDisabled>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.TipProcessingDisabled"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new ProcessingTerminalFeaturesTips(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ProcessingTerminalFeaturesTips value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Enabled switch
                {
                    "true" => JsonSerializer.SerializeToNode(value.Value, options),
                    "false" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["enabled"] = value.Enabled;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for true
    /// </summary>
    [Serializable]
    public struct True
    {
        public True(Payroc.TipProcessingEnabled value)
        {
            Value = value;
        }

        internal Payroc.TipProcessingEnabled Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator True(Payroc.TipProcessingEnabled value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for false
    /// </summary>
    [Serializable]
    public struct False
    {
        public False(Payroc.TipProcessingDisabled value)
        {
            Value = value;
        }

        internal Payroc.TipProcessingDisabled Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator False(Payroc.TipProcessingDisabled value) => new(value);
    }
}

// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about when and how the terminal closes the batch.
/// </summary>
[JsonConverter(typeof(ProcessingTerminalBatchClosure.JsonConverter))]
public record ProcessingTerminalBatchClosure
{
    internal ProcessingTerminalBatchClosure(string type, object? value)
    {
        BatchCloseType = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of ProcessingTerminalBatchClosure with <see cref="ProcessingTerminalBatchClosure.Automatic"/>.
    /// </summary>
    public ProcessingTerminalBatchClosure(ProcessingTerminalBatchClosure.Automatic value)
    {
        BatchCloseType = "automatic";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of ProcessingTerminalBatchClosure with <see cref="ProcessingTerminalBatchClosure.Manual"/>.
    /// </summary>
    public ProcessingTerminalBatchClosure(ProcessingTerminalBatchClosure.Manual value)
    {
        BatchCloseType = "manual";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("batchCloseType")]
    public string BatchCloseType { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="BatchCloseType"/> is "automatic"
    /// </summary>
    public bool IsAutomatic => BatchCloseType == "automatic";

    /// <summary>
    /// Returns true if <see cref="BatchCloseType"/> is "manual"
    /// </summary>
    public bool IsManual => BatchCloseType == "manual";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SchemasAutomaticBatchClose"/> if <see cref="BatchCloseType"/> is 'automatic', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="BatchCloseType"/> is not 'automatic'.</exception>
    public Payroc.SchemasAutomaticBatchClose AsAutomatic() =>
        IsAutomatic
            ? (Payroc.SchemasAutomaticBatchClose)Value!
            : throw new Exception(
                "ProcessingTerminalBatchClosure.BatchCloseType is not 'automatic'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SchemasManualBatchClose"/> if <see cref="BatchCloseType"/> is 'manual', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="BatchCloseType"/> is not 'manual'.</exception>
    public Payroc.SchemasManualBatchClose AsManual() =>
        IsManual
            ? (Payroc.SchemasManualBatchClose)Value!
            : throw new Exception("ProcessingTerminalBatchClosure.BatchCloseType is not 'manual'");

    public T Match<T>(
        Func<Payroc.SchemasAutomaticBatchClose, T> onAutomatic,
        Func<Payroc.SchemasManualBatchClose, T> onManual,
        Func<string, object?, T> onUnknown_
    )
    {
        return BatchCloseType switch
        {
            "automatic" => onAutomatic(AsAutomatic()),
            "manual" => onManual(AsManual()),
            _ => onUnknown_(BatchCloseType, Value),
        };
    }

    public void Visit(
        Action<Payroc.SchemasAutomaticBatchClose> onAutomatic,
        Action<Payroc.SchemasManualBatchClose> onManual,
        Action<string, object?> onUnknown_
    )
    {
        switch (BatchCloseType)
        {
            case "automatic":
                onAutomatic(AsAutomatic());
                break;
            case "manual":
                onManual(AsManual());
                break;
            default:
                onUnknown_(BatchCloseType, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.SchemasAutomaticBatchClose"/> and returns true if successful.
    /// </summary>
    public bool TryAsAutomatic(out Payroc.SchemasAutomaticBatchClose? value)
    {
        if (BatchCloseType == "automatic")
        {
            value = (Payroc.SchemasAutomaticBatchClose)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.SchemasManualBatchClose"/> and returns true if successful.
    /// </summary>
    public bool TryAsManual(out Payroc.SchemasManualBatchClose? value)
    {
        if (BatchCloseType == "manual")
        {
            value = (Payroc.SchemasManualBatchClose)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator ProcessingTerminalBatchClosure(
        ProcessingTerminalBatchClosure.Automatic value
    ) => new(value);

    public static implicit operator ProcessingTerminalBatchClosure(
        ProcessingTerminalBatchClosure.Manual value
    ) => new(value);

    internal sealed class JsonConverter : JsonConverter<ProcessingTerminalBatchClosure>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(ProcessingTerminalBatchClosure).IsAssignableFrom(typeToConvert);

        public override ProcessingTerminalBatchClosure Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("batchCloseType", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'batchCloseType'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'batchCloseType' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'batchCloseType' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'batchCloseType' is null");

            var value = discriminator switch
            {
                "automatic" => json.Deserialize<Payroc.SchemasAutomaticBatchClose>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.SchemasAutomaticBatchClose"
                    ),
                "manual" => json.Deserialize<Payroc.SchemasManualBatchClose>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.SchemasManualBatchClose"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new ProcessingTerminalBatchClosure(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ProcessingTerminalBatchClosure value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.BatchCloseType switch
                {
                    "automatic" => JsonSerializer.SerializeToNode(value.Value, options),
                    "manual" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["batchCloseType"] = value.BatchCloseType;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for automatic
    /// </summary>
    public struct Automatic
    {
        public Automatic(Payroc.SchemasAutomaticBatchClose value)
        {
            Value = value;
        }

        internal Payroc.SchemasAutomaticBatchClose Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Automatic(Payroc.SchemasAutomaticBatchClose value) =>
            new(value);
    }

    /// <summary>
    /// Discriminated union type for manual
    /// </summary>
    public struct Manual
    {
        public Manual(Payroc.SchemasManualBatchClose value)
        {
            Value = value;
        }

        internal Payroc.SchemasManualBatchClose Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Manual(Payroc.SchemasManualBatchClose value) => new(value);
    }
}

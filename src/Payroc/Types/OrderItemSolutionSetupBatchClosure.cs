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
[JsonConverter(typeof(OrderItemSolutionSetupBatchClosure.JsonConverter))]
[Serializable]
public record OrderItemSolutionSetupBatchClosure
{
    internal OrderItemSolutionSetupBatchClosure(string type, object? value)
    {
        BatchCloseType = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of OrderItemSolutionSetupBatchClosure with <see cref="OrderItemSolutionSetupBatchClosure.Automatic"/>.
    /// </summary>
    public OrderItemSolutionSetupBatchClosure(OrderItemSolutionSetupBatchClosure.Automatic value)
    {
        BatchCloseType = "automatic";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of OrderItemSolutionSetupBatchClosure with <see cref="OrderItemSolutionSetupBatchClosure.Manual"/>.
    /// </summary>
    public OrderItemSolutionSetupBatchClosure(OrderItemSolutionSetupBatchClosure.Manual value)
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
    /// Returns the value as a <see cref="Payroc.AutomaticBatchClose"/> if <see cref="BatchCloseType"/> is 'automatic', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="BatchCloseType"/> is not 'automatic'.</exception>
    public Payroc.AutomaticBatchClose AsAutomatic() =>
        IsAutomatic
            ? (Payroc.AutomaticBatchClose)Value!
            : throw new Exception(
                "OrderItemSolutionSetupBatchClosure.BatchCloseType is not 'automatic'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.ManualBatchClose"/> if <see cref="BatchCloseType"/> is 'manual', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="BatchCloseType"/> is not 'manual'.</exception>
    public Payroc.ManualBatchClose AsManual() =>
        IsManual
            ? (Payroc.ManualBatchClose)Value!
            : throw new Exception(
                "OrderItemSolutionSetupBatchClosure.BatchCloseType is not 'manual'"
            );

    public T Match<T>(
        Func<Payroc.AutomaticBatchClose, T> onAutomatic,
        Func<Payroc.ManualBatchClose, T> onManual,
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
        Action<Payroc.AutomaticBatchClose> onAutomatic,
        Action<Payroc.ManualBatchClose> onManual,
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
    /// Attempts to cast the value to a <see cref="Payroc.AutomaticBatchClose"/> and returns true if successful.
    /// </summary>
    public bool TryAsAutomatic(out Payroc.AutomaticBatchClose? value)
    {
        if (BatchCloseType == "automatic")
        {
            value = (Payroc.AutomaticBatchClose)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.ManualBatchClose"/> and returns true if successful.
    /// </summary>
    public bool TryAsManual(out Payroc.ManualBatchClose? value)
    {
        if (BatchCloseType == "manual")
        {
            value = (Payroc.ManualBatchClose)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator OrderItemSolutionSetupBatchClosure(
        OrderItemSolutionSetupBatchClosure.Automatic value
    ) => new(value);

    public static implicit operator OrderItemSolutionSetupBatchClosure(
        OrderItemSolutionSetupBatchClosure.Manual value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<OrderItemSolutionSetupBatchClosure>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(OrderItemSolutionSetupBatchClosure).IsAssignableFrom(typeToConvert);

        public override OrderItemSolutionSetupBatchClosure Read(
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
                "automatic" => json.Deserialize<Payroc.AutomaticBatchClose>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.AutomaticBatchClose"),
                "manual" => json.Deserialize<Payroc.ManualBatchClose>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.ManualBatchClose"),
                _ => json.Deserialize<object?>(options),
            };
            return new OrderItemSolutionSetupBatchClosure(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            OrderItemSolutionSetupBatchClosure value,
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
    [Serializable]
    public struct Automatic
    {
        public Automatic(Payroc.AutomaticBatchClose value)
        {
            Value = value;
        }

        internal Payroc.AutomaticBatchClose Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Automatic(Payroc.AutomaticBatchClose value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for manual
    /// </summary>
    [Serializable]
    public struct Manual
    {
        public Manual(Payroc.ManualBatchClose value)
        {
            Value = value;
        }

        internal Payroc.ManualBatchClose Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Manual(Payroc.ManualBatchClose value) => new(value);
    }
}

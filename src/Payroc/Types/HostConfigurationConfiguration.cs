// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(HostConfigurationConfiguration.JsonConverter))]
public record HostConfigurationConfiguration
{
    internal HostConfigurationConfiguration(string type, object? value)
    {
        Processor = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of HostConfigurationConfiguration with <see cref="HostConfigurationConfiguration.Processor"/>.
    /// </summary>
    public HostConfigurationConfiguration(HostConfigurationConfiguration.ProcessorType value)
    {
        Processor = "processor";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("processor")]
    public string Processor { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="Processor"/> is "processor"
    /// </summary>
    public bool IsProcessor => Processor == "processor";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.Tsys"/> if <see cref="Processor"/> is 'processor', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Processor"/> is not 'processor'.</exception>
    public Payroc.Tsys AsProcessor() =>
        IsProcessor
            ? (Payroc.Tsys)Value!
            : throw new Exception("HostConfigurationConfiguration.Processor is not 'processor'");

    public T Match<T>(Func<Payroc.Tsys, T> onProcessor, Func<string, object?, T> onUnknown_)
    {
        return Processor switch
        {
            "processor" => onProcessor(AsProcessor()),
            _ => onUnknown_(Processor, Value),
        };
    }

    public void Visit(Action<Payroc.Tsys> onProcessor, Action<string, object?> onUnknown_)
    {
        switch (Processor)
        {
            case "processor":
                onProcessor(AsProcessor());
                break;
            default:
                onUnknown_(Processor, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.Tsys"/> and returns true if successful.
    /// </summary>
    public bool TryAsProcessor(out Payroc.Tsys? value)
    {
        if (Processor == "processor")
        {
            value = (Payroc.Tsys)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator HostConfigurationConfiguration(
        HostConfigurationConfiguration.ProcessorType value
    ) => new(value);

    internal sealed class JsonConverter : JsonConverter<HostConfigurationConfiguration>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(HostConfigurationConfiguration).IsAssignableFrom(typeToConvert);

        public override HostConfigurationConfiguration Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("processor", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'processor'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'processor' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'processor' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'processor' is null");

            var value = discriminator switch
            {
                "processor" => json.Deserialize<Payroc.Tsys>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.Tsys"),
                _ => json.Deserialize<object?>(options),
            };
            return new HostConfigurationConfiguration(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            HostConfigurationConfiguration value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Processor switch
                {
                    "processor" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["processor"] = value.Processor;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for processor
    /// </summary>
    public struct ProcessorType
    {
        public ProcessorType(Payroc.Tsys value)
        {
            Value = value;
        }

        internal Payroc.Tsys Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator ProcessorType(Payroc.Tsys value) => new(value);
    }
}

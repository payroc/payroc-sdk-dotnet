// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the Hardware Advantage Plan.
/// </summary>
[JsonConverter(typeof(ServiceUs50.JsonConverter))]
[Serializable]
public record ServiceUs50
{
    internal ServiceUs50(string type, object? value)
    {
        Name = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of ServiceUs50 with <see cref="ServiceUs50.HardwareAdvantagePlan"/>.
    /// </summary>
    public ServiceUs50(ServiceUs50.HardwareAdvantagePlan value)
    {
        Name = "hardwareAdvantagePlan";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="Name"/> is "hardwareAdvantagePlan"
    /// </summary>
    public bool IsHardwareAdvantagePlan => Name == "hardwareAdvantagePlan";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.HardwareAdvantagePlan"/> if <see cref="Name"/> is 'hardwareAdvantagePlan', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Name"/> is not 'hardwareAdvantagePlan'.</exception>
    public Payroc.HardwareAdvantagePlan AsHardwareAdvantagePlan() =>
        IsHardwareAdvantagePlan
            ? (Payroc.HardwareAdvantagePlan)Value!
            : throw new Exception("ServiceUs50.Name is not 'hardwareAdvantagePlan'");

    public T Match<T>(
        Func<Payroc.HardwareAdvantagePlan, T> onHardwareAdvantagePlan,
        Func<string, object?, T> onUnknown_
    )
    {
        return Name switch
        {
            "hardwareAdvantagePlan" => onHardwareAdvantagePlan(AsHardwareAdvantagePlan()),
            _ => onUnknown_(Name, Value),
        };
    }

    public void Visit(
        Action<Payroc.HardwareAdvantagePlan> onHardwareAdvantagePlan,
        Action<string, object?> onUnknown_
    )
    {
        switch (Name)
        {
            case "hardwareAdvantagePlan":
                onHardwareAdvantagePlan(AsHardwareAdvantagePlan());
                break;
            default:
                onUnknown_(Name, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.HardwareAdvantagePlan"/> and returns true if successful.
    /// </summary>
    public bool TryAsHardwareAdvantagePlan(out Payroc.HardwareAdvantagePlan? value)
    {
        if (Name == "hardwareAdvantagePlan")
        {
            value = (Payroc.HardwareAdvantagePlan)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator ServiceUs50(ServiceUs50.HardwareAdvantagePlan value) =>
        new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<ServiceUs50>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(ServiceUs50).IsAssignableFrom(typeToConvert);

        public override ServiceUs50 Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("name", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'name'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'name' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'name' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'name' is null");

            var value = discriminator switch
            {
                "hardwareAdvantagePlan" => json.Deserialize<Payroc.HardwareAdvantagePlan>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.HardwareAdvantagePlan"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new ServiceUs50(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ServiceUs50 value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Name switch
                {
                    "hardwareAdvantagePlan" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["name"] = value.Name;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for hardwareAdvantagePlan
    /// </summary>
    [Serializable]
    public struct HardwareAdvantagePlan
    {
        public HardwareAdvantagePlan(Payroc.HardwareAdvantagePlan value)
        {
            Value = value;
        }

        internal Payroc.HardwareAdvantagePlan Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator HardwareAdvantagePlan(Payroc.HardwareAdvantagePlan value) =>
            new(value);
    }
}

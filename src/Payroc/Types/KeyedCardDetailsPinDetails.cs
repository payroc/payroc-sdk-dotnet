// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(KeyedCardDetailsPinDetails.JsonConverter))]
public record KeyedCardDetailsPinDetails
{
    internal KeyedCardDetailsPinDetails(string type, object? value)
    {
        DataFormat = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of KeyedCardDetailsPinDetails with <see cref="KeyedCardDetailsPinDetails.Dukpt"/>.
    /// </summary>
    public KeyedCardDetailsPinDetails(KeyedCardDetailsPinDetails.Dukpt value)
    {
        DataFormat = "dukpt";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("dataFormat")]
    public string DataFormat { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="DataFormat"/> is "dukpt"
    /// </summary>
    public bool IsDukpt => DataFormat == "dukpt";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.DukptPinDetails"/> if <see cref="DataFormat"/> is 'dukpt', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="DataFormat"/> is not 'dukpt'.</exception>
    public Payroc.DukptPinDetails AsDukpt() =>
        IsDukpt
            ? (Payroc.DukptPinDetails)Value!
            : throw new Exception("KeyedCardDetailsPinDetails.DataFormat is not 'dukpt'");

    public T Match<T>(Func<Payroc.DukptPinDetails, T> onDukpt, Func<string, object?, T> onUnknown_)
    {
        return DataFormat switch
        {
            "dukpt" => onDukpt(AsDukpt()),
            _ => onUnknown_(DataFormat, Value),
        };
    }

    public void Visit(Action<Payroc.DukptPinDetails> onDukpt, Action<string, object?> onUnknown_)
    {
        switch (DataFormat)
        {
            case "dukpt":
                onDukpt(AsDukpt());
                break;
            default:
                onUnknown_(DataFormat, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.DukptPinDetails"/> and returns true if successful.
    /// </summary>
    public bool TryAsDukpt(out Payroc.DukptPinDetails? value)
    {
        if (DataFormat == "dukpt")
        {
            value = (Payroc.DukptPinDetails)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator KeyedCardDetailsPinDetails(
        KeyedCardDetailsPinDetails.Dukpt value
    ) => new(value);

    internal sealed class JsonConverter : JsonConverter<KeyedCardDetailsPinDetails>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(KeyedCardDetailsPinDetails).IsAssignableFrom(typeToConvert);

        public override KeyedCardDetailsPinDetails Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("dataFormat", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'dataFormat'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'dataFormat' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'dataFormat' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'dataFormat' is null");

            var value = discriminator switch
            {
                "dukpt" => json.Deserialize<Payroc.DukptPinDetails>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.DukptPinDetails"),
                _ => json.Deserialize<object?>(options),
            };
            return new KeyedCardDetailsPinDetails(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            KeyedCardDetailsPinDetails value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.DataFormat switch
                {
                    "dukpt" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["dataFormat"] = value.DataFormat;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for dukpt
    /// </summary>
    public struct Dukpt
    {
        public Dukpt(Payroc.DukptPinDetails value)
        {
            Value = value;
        }

        internal Payroc.DukptPinDetails Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Dukpt(Payroc.DukptPinDetails value) => new(value);
    }
}

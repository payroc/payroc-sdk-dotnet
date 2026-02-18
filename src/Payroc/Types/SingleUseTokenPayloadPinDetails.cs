// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Polymorphic object that contains information about a customer's PIN.
///
/// The value of the dataFormat parameter determines which variant you should use:
/// - `dukpt` - PIN information is encrypted.
/// - `raw` - PIN information is unencrypted.
/// </summary>
[JsonConverter(typeof(SingleUseTokenPayloadPinDetails.JsonConverter))]
[Serializable]
public record SingleUseTokenPayloadPinDetails
{
    internal SingleUseTokenPayloadPinDetails(string type, object? value)
    {
        DataFormat = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of SingleUseTokenPayloadPinDetails with <see cref="SingleUseTokenPayloadPinDetails.Dukpt"/>.
    /// </summary>
    public SingleUseTokenPayloadPinDetails(SingleUseTokenPayloadPinDetails.Dukpt value)
    {
        DataFormat = "dukpt";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of SingleUseTokenPayloadPinDetails with <see cref="SingleUseTokenPayloadPinDetails.Raw"/>.
    /// </summary>
    public SingleUseTokenPayloadPinDetails(SingleUseTokenPayloadPinDetails.Raw value)
    {
        DataFormat = "raw";
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
    /// Returns true if <see cref="DataFormat"/> is "raw"
    /// </summary>
    public bool IsRaw => DataFormat == "raw";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.DukptPinDetails"/> if <see cref="DataFormat"/> is 'dukpt', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="DataFormat"/> is not 'dukpt'.</exception>
    public Payroc.DukptPinDetails AsDukpt() =>
        IsDukpt
            ? (Payroc.DukptPinDetails)Value!
            : throw new System.Exception(
                "SingleUseTokenPayloadPinDetails.DataFormat is not 'dukpt'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.RawPinDetails"/> if <see cref="DataFormat"/> is 'raw', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="DataFormat"/> is not 'raw'.</exception>
    public Payroc.RawPinDetails AsRaw() =>
        IsRaw
            ? (Payroc.RawPinDetails)Value!
            : throw new System.Exception("SingleUseTokenPayloadPinDetails.DataFormat is not 'raw'");

    public T Match<T>(
        Func<Payroc.DukptPinDetails, T> onDukpt,
        Func<Payroc.RawPinDetails, T> onRaw,
        Func<string, object?, T> onUnknown_
    )
    {
        return DataFormat switch
        {
            "dukpt" => onDukpt(AsDukpt()),
            "raw" => onRaw(AsRaw()),
            _ => onUnknown_(DataFormat, Value),
        };
    }

    public void Visit(
        Action<Payroc.DukptPinDetails> onDukpt,
        Action<Payroc.RawPinDetails> onRaw,
        Action<string, object?> onUnknown_
    )
    {
        switch (DataFormat)
        {
            case "dukpt":
                onDukpt(AsDukpt());
                break;
            case "raw":
                onRaw(AsRaw());
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

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.RawPinDetails"/> and returns true if successful.
    /// </summary>
    public bool TryAsRaw(out Payroc.RawPinDetails? value)
    {
        if (DataFormat == "raw")
        {
            value = (Payroc.RawPinDetails)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator SingleUseTokenPayloadPinDetails(
        SingleUseTokenPayloadPinDetails.Dukpt value
    ) => new(value);

    public static implicit operator SingleUseTokenPayloadPinDetails(
        SingleUseTokenPayloadPinDetails.Raw value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<SingleUseTokenPayloadPinDetails>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(SingleUseTokenPayloadPinDetails).IsAssignableFrom(typeToConvert);

        public override SingleUseTokenPayloadPinDetails Read(
            ref Utf8JsonReader reader,
            System.Type typeToConvert,
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
                "dukpt" => json.Deserialize<Payroc.DukptPinDetails?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.DukptPinDetails"),
                "raw" => json.Deserialize<Payroc.RawPinDetails?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.RawPinDetails"),
                _ => json.Deserialize<object?>(options),
            };
            return new SingleUseTokenPayloadPinDetails(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SingleUseTokenPayloadPinDetails value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.DataFormat switch
                {
                    "dukpt" => JsonSerializer.SerializeToNode(value.Value, options),
                    "raw" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["dataFormat"] = value.DataFormat;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for dukpt
    /// </summary>
    [Serializable]
    public struct Dukpt
    {
        public Dukpt(Payroc.DukptPinDetails value)
        {
            Value = value;
        }

        internal Payroc.DukptPinDetails Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator SingleUseTokenPayloadPinDetails.Dukpt(
            Payroc.DukptPinDetails value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for raw
    /// </summary>
    [Serializable]
    public struct Raw
    {
        public Raw(Payroc.RawPinDetails value)
        {
            Value = value;
        }

        internal Payroc.RawPinDetails Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator SingleUseTokenPayloadPinDetails.Raw(
            Payroc.RawPinDetails value
        ) => new(value);
    }
}

// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Polymorphic object that contains payment card details that the merchant manually entered into the device.
///
/// The value of the dataFormat parameter determines which variant you should use:
/// -	`fullyEncrypted` - Some payment card details are encrypted.
/// -	`partiallyEncrypted` - Payment card details are in plain text.
/// -	`plainText` - All payment card details are encrypted.
/// </summary>
[JsonConverter(typeof(KeyedCardDetailsKeyedData.JsonConverter))]
[Serializable]
public record KeyedCardDetailsKeyedData
{
    internal KeyedCardDetailsKeyedData(string type, object? value)
    {
        DataFormat = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of KeyedCardDetailsKeyedData with <see cref="KeyedCardDetailsKeyedData.FullyEncrypted"/>.
    /// </summary>
    public KeyedCardDetailsKeyedData(KeyedCardDetailsKeyedData.FullyEncrypted value)
    {
        DataFormat = "fullyEncrypted";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of KeyedCardDetailsKeyedData with <see cref="KeyedCardDetailsKeyedData.PartiallyEncrypted"/>.
    /// </summary>
    public KeyedCardDetailsKeyedData(KeyedCardDetailsKeyedData.PartiallyEncrypted value)
    {
        DataFormat = "partiallyEncrypted";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of KeyedCardDetailsKeyedData with <see cref="KeyedCardDetailsKeyedData.PlainText"/>.
    /// </summary>
    public KeyedCardDetailsKeyedData(KeyedCardDetailsKeyedData.PlainText value)
    {
        DataFormat = "plainText";
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
    /// Returns true if <see cref="DataFormat"/> is "fullyEncrypted"
    /// </summary>
    public bool IsFullyEncrypted => DataFormat == "fullyEncrypted";

    /// <summary>
    /// Returns true if <see cref="DataFormat"/> is "partiallyEncrypted"
    /// </summary>
    public bool IsPartiallyEncrypted => DataFormat == "partiallyEncrypted";

    /// <summary>
    /// Returns true if <see cref="DataFormat"/> is "plainText"
    /// </summary>
    public bool IsPlainText => DataFormat == "plainText";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.FullyEncryptedKeyedDataFormat"/> if <see cref="DataFormat"/> is 'fullyEncrypted', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="DataFormat"/> is not 'fullyEncrypted'.</exception>
    public Payroc.FullyEncryptedKeyedDataFormat AsFullyEncrypted() =>
        IsFullyEncrypted
            ? (Payroc.FullyEncryptedKeyedDataFormat)Value!
            : throw new System.Exception(
                "KeyedCardDetailsKeyedData.DataFormat is not 'fullyEncrypted'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PartiallyEncryptedKeyedDataFormat"/> if <see cref="DataFormat"/> is 'partiallyEncrypted', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="DataFormat"/> is not 'partiallyEncrypted'.</exception>
    public Payroc.PartiallyEncryptedKeyedDataFormat AsPartiallyEncrypted() =>
        IsPartiallyEncrypted
            ? (Payroc.PartiallyEncryptedKeyedDataFormat)Value!
            : throw new System.Exception(
                "KeyedCardDetailsKeyedData.DataFormat is not 'partiallyEncrypted'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PlainTextKeyedDataFormat"/> if <see cref="DataFormat"/> is 'plainText', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="DataFormat"/> is not 'plainText'.</exception>
    public Payroc.PlainTextKeyedDataFormat AsPlainText() =>
        IsPlainText
            ? (Payroc.PlainTextKeyedDataFormat)Value!
            : throw new System.Exception("KeyedCardDetailsKeyedData.DataFormat is not 'plainText'");

    public T Match<T>(
        Func<Payroc.FullyEncryptedKeyedDataFormat, T> onFullyEncrypted,
        Func<Payroc.PartiallyEncryptedKeyedDataFormat, T> onPartiallyEncrypted,
        Func<Payroc.PlainTextKeyedDataFormat, T> onPlainText,
        Func<string, object?, T> onUnknown_
    )
    {
        return DataFormat switch
        {
            "fullyEncrypted" => onFullyEncrypted(AsFullyEncrypted()),
            "partiallyEncrypted" => onPartiallyEncrypted(AsPartiallyEncrypted()),
            "plainText" => onPlainText(AsPlainText()),
            _ => onUnknown_(DataFormat, Value),
        };
    }

    public void Visit(
        Action<Payroc.FullyEncryptedKeyedDataFormat> onFullyEncrypted,
        Action<Payroc.PartiallyEncryptedKeyedDataFormat> onPartiallyEncrypted,
        Action<Payroc.PlainTextKeyedDataFormat> onPlainText,
        Action<string, object?> onUnknown_
    )
    {
        switch (DataFormat)
        {
            case "fullyEncrypted":
                onFullyEncrypted(AsFullyEncrypted());
                break;
            case "partiallyEncrypted":
                onPartiallyEncrypted(AsPartiallyEncrypted());
                break;
            case "plainText":
                onPlainText(AsPlainText());
                break;
            default:
                onUnknown_(DataFormat, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.FullyEncryptedKeyedDataFormat"/> and returns true if successful.
    /// </summary>
    public bool TryAsFullyEncrypted(out Payroc.FullyEncryptedKeyedDataFormat? value)
    {
        if (DataFormat == "fullyEncrypted")
        {
            value = (Payroc.FullyEncryptedKeyedDataFormat)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PartiallyEncryptedKeyedDataFormat"/> and returns true if successful.
    /// </summary>
    public bool TryAsPartiallyEncrypted(out Payroc.PartiallyEncryptedKeyedDataFormat? value)
    {
        if (DataFormat == "partiallyEncrypted")
        {
            value = (Payroc.PartiallyEncryptedKeyedDataFormat)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PlainTextKeyedDataFormat"/> and returns true if successful.
    /// </summary>
    public bool TryAsPlainText(out Payroc.PlainTextKeyedDataFormat? value)
    {
        if (DataFormat == "plainText")
        {
            value = (Payroc.PlainTextKeyedDataFormat)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator KeyedCardDetailsKeyedData(
        KeyedCardDetailsKeyedData.FullyEncrypted value
    ) => new(value);

    public static implicit operator KeyedCardDetailsKeyedData(
        KeyedCardDetailsKeyedData.PartiallyEncrypted value
    ) => new(value);

    public static implicit operator KeyedCardDetailsKeyedData(
        KeyedCardDetailsKeyedData.PlainText value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<KeyedCardDetailsKeyedData>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(KeyedCardDetailsKeyedData).IsAssignableFrom(typeToConvert);

        public override KeyedCardDetailsKeyedData Read(
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
                "fullyEncrypted" => json.Deserialize<Payroc.FullyEncryptedKeyedDataFormat?>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.FullyEncryptedKeyedDataFormat"
                    ),
                "partiallyEncrypted" => json.Deserialize<Payroc.PartiallyEncryptedKeyedDataFormat?>(
                    options
                )
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.PartiallyEncryptedKeyedDataFormat"
                    ),
                "plainText" => json.Deserialize<Payroc.PlainTextKeyedDataFormat?>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.PlainTextKeyedDataFormat"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new KeyedCardDetailsKeyedData(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            KeyedCardDetailsKeyedData value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.DataFormat switch
                {
                    "fullyEncrypted" => JsonSerializer.SerializeToNode(value.Value, options),
                    "partiallyEncrypted" => JsonSerializer.SerializeToNode(value.Value, options),
                    "plainText" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["dataFormat"] = value.DataFormat;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for fullyEncrypted
    /// </summary>
    [Serializable]
    public struct FullyEncrypted
    {
        public FullyEncrypted(Payroc.FullyEncryptedKeyedDataFormat value)
        {
            Value = value;
        }

        internal Payroc.FullyEncryptedKeyedDataFormat Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator KeyedCardDetailsKeyedData.FullyEncrypted(
            Payroc.FullyEncryptedKeyedDataFormat value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for partiallyEncrypted
    /// </summary>
    [Serializable]
    public struct PartiallyEncrypted
    {
        public PartiallyEncrypted(Payroc.PartiallyEncryptedKeyedDataFormat value)
        {
            Value = value;
        }

        internal Payroc.PartiallyEncryptedKeyedDataFormat Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator KeyedCardDetailsKeyedData.PartiallyEncrypted(
            Payroc.PartiallyEncryptedKeyedDataFormat value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for plainText
    /// </summary>
    [Serializable]
    public struct PlainText
    {
        public PlainText(Payroc.PlainTextKeyedDataFormat value)
        {
            Value = value;
        }

        internal Payroc.PlainTextKeyedDataFormat Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator KeyedCardDetailsKeyedData.PlainText(
            Payroc.PlainTextKeyedDataFormat value
        ) => new(value);
    }
}

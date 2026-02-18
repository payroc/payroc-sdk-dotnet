// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Polymorphic object that contains payment card details that a device captured from the magnetic strip.
///
/// The value of the dataFormat parameter determines which variant you should use:
/// -	`encrypted` - Payment card details are encrypted.
/// -	`plainText` - Payment card details are in plain text.
/// </summary>
[JsonConverter(typeof(SwipedCardDetailsSwipedData.JsonConverter))]
[Serializable]
public record SwipedCardDetailsSwipedData
{
    internal SwipedCardDetailsSwipedData(string type, object? value)
    {
        DataFormat = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of SwipedCardDetailsSwipedData with <see cref="SwipedCardDetailsSwipedData.Encrypted"/>.
    /// </summary>
    public SwipedCardDetailsSwipedData(SwipedCardDetailsSwipedData.Encrypted value)
    {
        DataFormat = "encrypted";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of SwipedCardDetailsSwipedData with <see cref="SwipedCardDetailsSwipedData.PlainText"/>.
    /// </summary>
    public SwipedCardDetailsSwipedData(SwipedCardDetailsSwipedData.PlainText value)
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
    /// Returns true if <see cref="DataFormat"/> is "encrypted"
    /// </summary>
    public bool IsEncrypted => DataFormat == "encrypted";

    /// <summary>
    /// Returns true if <see cref="DataFormat"/> is "plainText"
    /// </summary>
    public bool IsPlainText => DataFormat == "plainText";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.EncryptedSwipedDataFormat"/> if <see cref="DataFormat"/> is 'encrypted', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="DataFormat"/> is not 'encrypted'.</exception>
    public Payroc.EncryptedSwipedDataFormat AsEncrypted() =>
        IsEncrypted
            ? (Payroc.EncryptedSwipedDataFormat)Value!
            : throw new System.Exception(
                "SwipedCardDetailsSwipedData.DataFormat is not 'encrypted'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PlainTextSwipedDataFormat"/> if <see cref="DataFormat"/> is 'plainText', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="DataFormat"/> is not 'plainText'.</exception>
    public Payroc.PlainTextSwipedDataFormat AsPlainText() =>
        IsPlainText
            ? (Payroc.PlainTextSwipedDataFormat)Value!
            : throw new System.Exception(
                "SwipedCardDetailsSwipedData.DataFormat is not 'plainText'"
            );

    public T Match<T>(
        Func<Payroc.EncryptedSwipedDataFormat, T> onEncrypted,
        Func<Payroc.PlainTextSwipedDataFormat, T> onPlainText,
        Func<string, object?, T> onUnknown_
    )
    {
        return DataFormat switch
        {
            "encrypted" => onEncrypted(AsEncrypted()),
            "plainText" => onPlainText(AsPlainText()),
            _ => onUnknown_(DataFormat, Value),
        };
    }

    public void Visit(
        Action<Payroc.EncryptedSwipedDataFormat> onEncrypted,
        Action<Payroc.PlainTextSwipedDataFormat> onPlainText,
        Action<string, object?> onUnknown_
    )
    {
        switch (DataFormat)
        {
            case "encrypted":
                onEncrypted(AsEncrypted());
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
    /// Attempts to cast the value to a <see cref="Payroc.EncryptedSwipedDataFormat"/> and returns true if successful.
    /// </summary>
    public bool TryAsEncrypted(out Payroc.EncryptedSwipedDataFormat? value)
    {
        if (DataFormat == "encrypted")
        {
            value = (Payroc.EncryptedSwipedDataFormat)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PlainTextSwipedDataFormat"/> and returns true if successful.
    /// </summary>
    public bool TryAsPlainText(out Payroc.PlainTextSwipedDataFormat? value)
    {
        if (DataFormat == "plainText")
        {
            value = (Payroc.PlainTextSwipedDataFormat)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator SwipedCardDetailsSwipedData(
        SwipedCardDetailsSwipedData.Encrypted value
    ) => new(value);

    public static implicit operator SwipedCardDetailsSwipedData(
        SwipedCardDetailsSwipedData.PlainText value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<SwipedCardDetailsSwipedData>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(SwipedCardDetailsSwipedData).IsAssignableFrom(typeToConvert);

        public override SwipedCardDetailsSwipedData Read(
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
                "encrypted" => json.Deserialize<Payroc.EncryptedSwipedDataFormat?>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.EncryptedSwipedDataFormat"
                    ),
                "plainText" => json.Deserialize<Payroc.PlainTextSwipedDataFormat?>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.PlainTextSwipedDataFormat"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new SwipedCardDetailsSwipedData(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SwipedCardDetailsSwipedData value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.DataFormat switch
                {
                    "encrypted" => JsonSerializer.SerializeToNode(value.Value, options),
                    "plainText" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["dataFormat"] = value.DataFormat;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for encrypted
    /// </summary>
    [Serializable]
    public struct Encrypted
    {
        public Encrypted(Payroc.EncryptedSwipedDataFormat value)
        {
            Value = value;
        }

        internal Payroc.EncryptedSwipedDataFormat Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator SwipedCardDetailsSwipedData.Encrypted(
            Payroc.EncryptedSwipedDataFormat value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for plainText
    /// </summary>
    [Serializable]
    public struct PlainText
    {
        public PlainText(Payroc.PlainTextSwipedDataFormat value)
        {
            Value = value;
        }

        internal Payroc.PlainTextSwipedDataFormat Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator SwipedCardDetailsSwipedData.PlainText(
            Payroc.PlainTextSwipedDataFormat value
        ) => new(value);
    }
}

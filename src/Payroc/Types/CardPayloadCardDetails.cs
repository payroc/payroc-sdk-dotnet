// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(CardPayloadCardDetails.JsonConverter))]
[Serializable]
public record CardPayloadCardDetails
{
    internal CardPayloadCardDetails(string type, object? value)
    {
        EntryMethod = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of CardPayloadCardDetails with <see cref="CardPayloadCardDetails.Raw"/>.
    /// </summary>
    public CardPayloadCardDetails(CardPayloadCardDetails.Raw value)
    {
        EntryMethod = "raw";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of CardPayloadCardDetails with <see cref="CardPayloadCardDetails.Icc"/>.
    /// </summary>
    public CardPayloadCardDetails(CardPayloadCardDetails.Icc value)
    {
        EntryMethod = "icc";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of CardPayloadCardDetails with <see cref="CardPayloadCardDetails.Keyed"/>.
    /// </summary>
    public CardPayloadCardDetails(CardPayloadCardDetails.Keyed value)
    {
        EntryMethod = "keyed";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of CardPayloadCardDetails with <see cref="CardPayloadCardDetails.Swiped"/>.
    /// </summary>
    public CardPayloadCardDetails(CardPayloadCardDetails.Swiped value)
    {
        EntryMethod = "swiped";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("entryMethod")]
    public string EntryMethod { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="EntryMethod"/> is "raw"
    /// </summary>
    public bool IsRaw => EntryMethod == "raw";

    /// <summary>
    /// Returns true if <see cref="EntryMethod"/> is "icc"
    /// </summary>
    public bool IsIcc => EntryMethod == "icc";

    /// <summary>
    /// Returns true if <see cref="EntryMethod"/> is "keyed"
    /// </summary>
    public bool IsKeyed => EntryMethod == "keyed";

    /// <summary>
    /// Returns true if <see cref="EntryMethod"/> is "swiped"
    /// </summary>
    public bool IsSwiped => EntryMethod == "swiped";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.RawCardDetails"/> if <see cref="EntryMethod"/> is 'raw', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="EntryMethod"/> is not 'raw'.</exception>
    public Payroc.RawCardDetails AsRaw() =>
        IsRaw
            ? (Payroc.RawCardDetails)Value!
            : throw new Exception("CardPayloadCardDetails.EntryMethod is not 'raw'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.IccCardDetails"/> if <see cref="EntryMethod"/> is 'icc', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="EntryMethod"/> is not 'icc'.</exception>
    public Payroc.IccCardDetails AsIcc() =>
        IsIcc
            ? (Payroc.IccCardDetails)Value!
            : throw new Exception("CardPayloadCardDetails.EntryMethod is not 'icc'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.KeyedCardDetails"/> if <see cref="EntryMethod"/> is 'keyed', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="EntryMethod"/> is not 'keyed'.</exception>
    public Payroc.KeyedCardDetails AsKeyed() =>
        IsKeyed
            ? (Payroc.KeyedCardDetails)Value!
            : throw new Exception("CardPayloadCardDetails.EntryMethod is not 'keyed'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SwipedCardDetails"/> if <see cref="EntryMethod"/> is 'swiped', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="EntryMethod"/> is not 'swiped'.</exception>
    public Payroc.SwipedCardDetails AsSwiped() =>
        IsSwiped
            ? (Payroc.SwipedCardDetails)Value!
            : throw new Exception("CardPayloadCardDetails.EntryMethod is not 'swiped'");

    public T Match<T>(
        Func<Payroc.RawCardDetails, T> onRaw,
        Func<Payroc.IccCardDetails, T> onIcc,
        Func<Payroc.KeyedCardDetails, T> onKeyed,
        Func<Payroc.SwipedCardDetails, T> onSwiped,
        Func<string, object?, T> onUnknown_
    )
    {
        return EntryMethod switch
        {
            "raw" => onRaw(AsRaw()),
            "icc" => onIcc(AsIcc()),
            "keyed" => onKeyed(AsKeyed()),
            "swiped" => onSwiped(AsSwiped()),
            _ => onUnknown_(EntryMethod, Value),
        };
    }

    public void Visit(
        Action<Payroc.RawCardDetails> onRaw,
        Action<Payroc.IccCardDetails> onIcc,
        Action<Payroc.KeyedCardDetails> onKeyed,
        Action<Payroc.SwipedCardDetails> onSwiped,
        Action<string, object?> onUnknown_
    )
    {
        switch (EntryMethod)
        {
            case "raw":
                onRaw(AsRaw());
                break;
            case "icc":
                onIcc(AsIcc());
                break;
            case "keyed":
                onKeyed(AsKeyed());
                break;
            case "swiped":
                onSwiped(AsSwiped());
                break;
            default:
                onUnknown_(EntryMethod, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.RawCardDetails"/> and returns true if successful.
    /// </summary>
    public bool TryAsRaw(out Payroc.RawCardDetails? value)
    {
        if (EntryMethod == "raw")
        {
            value = (Payroc.RawCardDetails)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.IccCardDetails"/> and returns true if successful.
    /// </summary>
    public bool TryAsIcc(out Payroc.IccCardDetails? value)
    {
        if (EntryMethod == "icc")
        {
            value = (Payroc.IccCardDetails)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.KeyedCardDetails"/> and returns true if successful.
    /// </summary>
    public bool TryAsKeyed(out Payroc.KeyedCardDetails? value)
    {
        if (EntryMethod == "keyed")
        {
            value = (Payroc.KeyedCardDetails)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.SwipedCardDetails"/> and returns true if successful.
    /// </summary>
    public bool TryAsSwiped(out Payroc.SwipedCardDetails? value)
    {
        if (EntryMethod == "swiped")
        {
            value = (Payroc.SwipedCardDetails)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator CardPayloadCardDetails(CardPayloadCardDetails.Raw value) =>
        new(value);

    public static implicit operator CardPayloadCardDetails(CardPayloadCardDetails.Icc value) =>
        new(value);

    public static implicit operator CardPayloadCardDetails(CardPayloadCardDetails.Keyed value) =>
        new(value);

    public static implicit operator CardPayloadCardDetails(CardPayloadCardDetails.Swiped value) =>
        new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<CardPayloadCardDetails>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(CardPayloadCardDetails).IsAssignableFrom(typeToConvert);

        public override CardPayloadCardDetails Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("entryMethod", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'entryMethod'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'entryMethod' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'entryMethod' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'entryMethod' is null");

            var value = discriminator switch
            {
                "raw" => json.Deserialize<Payroc.RawCardDetails>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.RawCardDetails"),
                "icc" => json.Deserialize<Payroc.IccCardDetails>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.IccCardDetails"),
                "keyed" => json.Deserialize<Payroc.KeyedCardDetails>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.KeyedCardDetails"),
                "swiped" => json.Deserialize<Payroc.SwipedCardDetails>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.SwipedCardDetails"),
                _ => json.Deserialize<object?>(options),
            };
            return new CardPayloadCardDetails(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CardPayloadCardDetails value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.EntryMethod switch
                {
                    "raw" => JsonSerializer.SerializeToNode(value.Value, options),
                    "icc" => JsonSerializer.SerializeToNode(value.Value, options),
                    "keyed" => JsonSerializer.SerializeToNode(value.Value, options),
                    "swiped" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["entryMethod"] = value.EntryMethod;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for raw
    /// </summary>
    [Serializable]
    public struct Raw
    {
        public Raw(Payroc.RawCardDetails value)
        {
            Value = value;
        }

        internal Payroc.RawCardDetails Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Raw(Payroc.RawCardDetails value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for icc
    /// </summary>
    [Serializable]
    public struct Icc
    {
        public Icc(Payroc.IccCardDetails value)
        {
            Value = value;
        }

        internal Payroc.IccCardDetails Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Icc(Payroc.IccCardDetails value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for keyed
    /// </summary>
    [Serializable]
    public struct Keyed
    {
        public Keyed(Payroc.KeyedCardDetails value)
        {
            Value = value;
        }

        internal Payroc.KeyedCardDetails Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Keyed(Payroc.KeyedCardDetails value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for swiped
    /// </summary>
    [Serializable]
    public struct Swiped
    {
        public Swiped(Payroc.SwipedCardDetails value)
        {
            Value = value;
        }

        internal Payroc.SwipedCardDetails Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Swiped(Payroc.SwipedCardDetails value) => new(value);
    }
}

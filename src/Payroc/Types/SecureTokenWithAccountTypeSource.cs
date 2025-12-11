// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the payment method that we tokenized.
/// </summary>
[JsonConverter(typeof(SecureTokenWithAccountTypeSource.JsonConverter))]
[Serializable]
public record SecureTokenWithAccountTypeSource
{
    internal SecureTokenWithAccountTypeSource(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of SecureTokenWithAccountTypeSource with <see cref="SecureTokenWithAccountTypeSource.Ach"/>.
    /// </summary>
    public SecureTokenWithAccountTypeSource(SecureTokenWithAccountTypeSource.Ach value)
    {
        Type = "ach";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of SecureTokenWithAccountTypeSource with <see cref="SecureTokenWithAccountTypeSource.Pad"/>.
    /// </summary>
    public SecureTokenWithAccountTypeSource(SecureTokenWithAccountTypeSource.Pad value)
    {
        Type = "pad";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of SecureTokenWithAccountTypeSource with <see cref="SecureTokenWithAccountTypeSource.Card"/>.
    /// </summary>
    public SecureTokenWithAccountTypeSource(SecureTokenWithAccountTypeSource.Card value)
    {
        Type = "card";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="Type"/> is "ach"
    /// </summary>
    public bool IsAch => Type == "ach";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "pad"
    /// </summary>
    public bool IsPad => Type == "pad";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "card"
    /// </summary>
    public bool IsCard => Type == "card";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.AchSourceWithAccountType"/> if <see cref="Type"/> is 'ach', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'ach'.</exception>
    public Payroc.AchSourceWithAccountType AsAch() =>
        IsAch
            ? (Payroc.AchSourceWithAccountType)Value!
            : throw new System.Exception("SecureTokenWithAccountTypeSource.Type is not 'ach'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PadSourceWithAccountType"/> if <see cref="Type"/> is 'pad', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'pad'.</exception>
    public Payroc.PadSourceWithAccountType AsPad() =>
        IsPad
            ? (Payroc.PadSourceWithAccountType)Value!
            : throw new System.Exception("SecureTokenWithAccountTypeSource.Type is not 'pad'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.CardSource"/> if <see cref="Type"/> is 'card', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'card'.</exception>
    public Payroc.CardSource AsCard() =>
        IsCard
            ? (Payroc.CardSource)Value!
            : throw new System.Exception("SecureTokenWithAccountTypeSource.Type is not 'card'");

    public T Match<T>(
        Func<Payroc.AchSourceWithAccountType, T> onAch,
        Func<Payroc.PadSourceWithAccountType, T> onPad,
        Func<Payroc.CardSource, T> onCard,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "ach" => onAch(AsAch()),
            "pad" => onPad(AsPad()),
            "card" => onCard(AsCard()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.AchSourceWithAccountType> onAch,
        Action<Payroc.PadSourceWithAccountType> onPad,
        Action<Payroc.CardSource> onCard,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "ach":
                onAch(AsAch());
                break;
            case "pad":
                onPad(AsPad());
                break;
            case "card":
                onCard(AsCard());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.AchSourceWithAccountType"/> and returns true if successful.
    /// </summary>
    public bool TryAsAch(out Payroc.AchSourceWithAccountType? value)
    {
        if (Type == "ach")
        {
            value = (Payroc.AchSourceWithAccountType)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PadSourceWithAccountType"/> and returns true if successful.
    /// </summary>
    public bool TryAsPad(out Payroc.PadSourceWithAccountType? value)
    {
        if (Type == "pad")
        {
            value = (Payroc.PadSourceWithAccountType)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.CardSource"/> and returns true if successful.
    /// </summary>
    public bool TryAsCard(out Payroc.CardSource? value)
    {
        if (Type == "card")
        {
            value = (Payroc.CardSource)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator SecureTokenWithAccountTypeSource(
        SecureTokenWithAccountTypeSource.Ach value
    ) => new(value);

    public static implicit operator SecureTokenWithAccountTypeSource(
        SecureTokenWithAccountTypeSource.Pad value
    ) => new(value);

    public static implicit operator SecureTokenWithAccountTypeSource(
        SecureTokenWithAccountTypeSource.Card value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<SecureTokenWithAccountTypeSource>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(SecureTokenWithAccountTypeSource).IsAssignableFrom(typeToConvert);

        public override SecureTokenWithAccountTypeSource Read(
            ref Utf8JsonReader reader,
            System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("type", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'type'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'type' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'type' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'type' is null");

            var value = discriminator switch
            {
                "ach" => json.Deserialize<Payroc.AchSourceWithAccountType?>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.AchSourceWithAccountType"
                    ),
                "pad" => json.Deserialize<Payroc.PadSourceWithAccountType?>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.PadSourceWithAccountType"
                    ),
                "card" => json.Deserialize<Payroc.CardSource?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.CardSource"),
                _ => json.Deserialize<object?>(options),
            };
            return new SecureTokenWithAccountTypeSource(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SecureTokenWithAccountTypeSource value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "ach" => JsonSerializer.SerializeToNode(value.Value, options),
                    "pad" => JsonSerializer.SerializeToNode(value.Value, options),
                    "card" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for ach
    /// </summary>
    [Serializable]
    public struct Ach
    {
        public Ach(Payroc.AchSourceWithAccountType value)
        {
            Value = value;
        }

        internal Payroc.AchSourceWithAccountType Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator SecureTokenWithAccountTypeSource.Ach(
            Payroc.AchSourceWithAccountType value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for pad
    /// </summary>
    [Serializable]
    public struct Pad
    {
        public Pad(Payroc.PadSourceWithAccountType value)
        {
            Value = value;
        }

        internal Payroc.PadSourceWithAccountType Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator SecureTokenWithAccountTypeSource.Pad(
            Payroc.PadSourceWithAccountType value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for card
    /// </summary>
    [Serializable]
    public struct Card
    {
        public Card(Payroc.CardSource value)
        {
            Value = value;
        }

        internal Payroc.CardSource Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator SecureTokenWithAccountTypeSource.Card(
            Payroc.CardSource value
        ) => new(value);
    }
}

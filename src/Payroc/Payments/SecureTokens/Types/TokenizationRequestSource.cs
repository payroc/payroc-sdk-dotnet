// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.SecureTokens;

/// <summary>
/// Object that contains information about the payment method to tokenize.
/// </summary>
[JsonConverter(typeof(TokenizationRequestSource.JsonConverter))]
[Serializable]
public record TokenizationRequestSource
{
    internal TokenizationRequestSource(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of TokenizationRequestSource with <see cref="TokenizationRequestSource.Ach"/>.
    /// </summary>
    public TokenizationRequestSource(TokenizationRequestSource.Ach value)
    {
        Type = "ach";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of TokenizationRequestSource with <see cref="TokenizationRequestSource.Pad"/>.
    /// </summary>
    public TokenizationRequestSource(TokenizationRequestSource.Pad value)
    {
        Type = "pad";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of TokenizationRequestSource with <see cref="TokenizationRequestSource.Card"/>.
    /// </summary>
    public TokenizationRequestSource(TokenizationRequestSource.Card value)
    {
        Type = "card";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of TokenizationRequestSource with <see cref="TokenizationRequestSource.SingleUseToken"/>.
    /// </summary>
    public TokenizationRequestSource(TokenizationRequestSource.SingleUseToken value)
    {
        Type = "singleUseToken";
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
    /// Returns true if <see cref="Type"/> is "singleUseToken"
    /// </summary>
    public bool IsSingleUseToken => Type == "singleUseToken";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.AchPayload"/> if <see cref="Type"/> is 'ach', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'ach'.</exception>
    public Payroc.AchPayload AsAch() =>
        IsAch
            ? (Payroc.AchPayload)Value!
            : throw new Exception("TokenizationRequestSource.Type is not 'ach'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PadPayload"/> if <see cref="Type"/> is 'pad', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'pad'.</exception>
    public Payroc.PadPayload AsPad() =>
        IsPad
            ? (Payroc.PadPayload)Value!
            : throw new Exception("TokenizationRequestSource.Type is not 'pad'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.CardPayload"/> if <see cref="Type"/> is 'card', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'card'.</exception>
    public Payroc.CardPayload AsCard() =>
        IsCard
            ? (Payroc.CardPayload)Value!
            : throw new Exception("TokenizationRequestSource.Type is not 'card'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SingleUseTokenPayload"/> if <see cref="Type"/> is 'singleUseToken', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'singleUseToken'.</exception>
    public Payroc.SingleUseTokenPayload AsSingleUseToken() =>
        IsSingleUseToken
            ? (Payroc.SingleUseTokenPayload)Value!
            : throw new Exception("TokenizationRequestSource.Type is not 'singleUseToken'");

    public T Match<T>(
        Func<Payroc.AchPayload, T> onAch,
        Func<Payroc.PadPayload, T> onPad,
        Func<Payroc.CardPayload, T> onCard,
        Func<Payroc.SingleUseTokenPayload, T> onSingleUseToken,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "ach" => onAch(AsAch()),
            "pad" => onPad(AsPad()),
            "card" => onCard(AsCard()),
            "singleUseToken" => onSingleUseToken(AsSingleUseToken()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.AchPayload> onAch,
        Action<Payroc.PadPayload> onPad,
        Action<Payroc.CardPayload> onCard,
        Action<Payroc.SingleUseTokenPayload> onSingleUseToken,
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
            case "singleUseToken":
                onSingleUseToken(AsSingleUseToken());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.AchPayload"/> and returns true if successful.
    /// </summary>
    public bool TryAsAch(out Payroc.AchPayload? value)
    {
        if (Type == "ach")
        {
            value = (Payroc.AchPayload)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PadPayload"/> and returns true if successful.
    /// </summary>
    public bool TryAsPad(out Payroc.PadPayload? value)
    {
        if (Type == "pad")
        {
            value = (Payroc.PadPayload)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.CardPayload"/> and returns true if successful.
    /// </summary>
    public bool TryAsCard(out Payroc.CardPayload? value)
    {
        if (Type == "card")
        {
            value = (Payroc.CardPayload)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.SingleUseTokenPayload"/> and returns true if successful.
    /// </summary>
    public bool TryAsSingleUseToken(out Payroc.SingleUseTokenPayload? value)
    {
        if (Type == "singleUseToken")
        {
            value = (Payroc.SingleUseTokenPayload)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator TokenizationRequestSource(
        TokenizationRequestSource.Ach value
    ) => new(value);

    public static implicit operator TokenizationRequestSource(
        TokenizationRequestSource.Pad value
    ) => new(value);

    public static implicit operator TokenizationRequestSource(
        TokenizationRequestSource.Card value
    ) => new(value);

    public static implicit operator TokenizationRequestSource(
        TokenizationRequestSource.SingleUseToken value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<TokenizationRequestSource>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(TokenizationRequestSource).IsAssignableFrom(typeToConvert);

        public override TokenizationRequestSource Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
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
                "ach" => json.Deserialize<Payroc.AchPayload>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.AchPayload"),
                "pad" => json.Deserialize<Payroc.PadPayload>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.PadPayload"),
                "card" => json.Deserialize<Payroc.CardPayload>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.CardPayload"),
                "singleUseToken" => json.Deserialize<Payroc.SingleUseTokenPayload>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.SingleUseTokenPayload"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new TokenizationRequestSource(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TokenizationRequestSource value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "ach" => JsonSerializer.SerializeToNode(value.Value, options),
                    "pad" => JsonSerializer.SerializeToNode(value.Value, options),
                    "card" => JsonSerializer.SerializeToNode(value.Value, options),
                    "singleUseToken" => JsonSerializer.SerializeToNode(value.Value, options),
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
        public Ach(Payroc.AchPayload value)
        {
            Value = value;
        }

        internal Payroc.AchPayload Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Ach(Payroc.AchPayload value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for pad
    /// </summary>
    [Serializable]
    public struct Pad
    {
        public Pad(Payroc.PadPayload value)
        {
            Value = value;
        }

        internal Payroc.PadPayload Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Pad(Payroc.PadPayload value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for card
    /// </summary>
    [Serializable]
    public struct Card
    {
        public Card(Payroc.CardPayload value)
        {
            Value = value;
        }

        internal Payroc.CardPayload Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Card(Payroc.CardPayload value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for singleUseToken
    /// </summary>
    [Serializable]
    public struct SingleUseToken
    {
        public SingleUseToken(Payroc.SingleUseTokenPayload value)
        {
            Value = value;
        }

        internal Payroc.SingleUseTokenPayload Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator SingleUseToken(Payroc.SingleUseTokenPayload value) =>
            new(value);
    }
}

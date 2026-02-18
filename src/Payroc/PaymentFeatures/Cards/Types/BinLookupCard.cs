// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.PaymentFeatures.Cards;

/// <summary>
/// Polymorphic object that contains payment details.
///
/// The value of the type parameter determines which variant you should use:
/// -	`card` - Payment card details
/// -	`cardBin` - Bank identification number (BIN) of the payment card
/// -	`secureToken` - Secure token details
/// -	`digitalWallet` - Digital wallet details
/// </summary>
[JsonConverter(typeof(BinLookupCard.JsonConverter))]
[Serializable]
public record BinLookupCard
{
    internal BinLookupCard(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of BinLookupCard with <see cref="BinLookupCard.Card"/>.
    /// </summary>
    public BinLookupCard(BinLookupCard.Card value)
    {
        Type = "card";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of BinLookupCard with <see cref="BinLookupCard.CardBin"/>.
    /// </summary>
    public BinLookupCard(BinLookupCard.CardBin value)
    {
        Type = "cardBin";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of BinLookupCard with <see cref="BinLookupCard.SecureToken"/>.
    /// </summary>
    public BinLookupCard(BinLookupCard.SecureToken value)
    {
        Type = "secureToken";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of BinLookupCard with <see cref="BinLookupCard.DigitalWallet"/>.
    /// </summary>
    public BinLookupCard(BinLookupCard.DigitalWallet value)
    {
        Type = "digitalWallet";
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
    /// Returns true if <see cref="Type"/> is "card"
    /// </summary>
    public bool IsCard => Type == "card";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "cardBin"
    /// </summary>
    public bool IsCardBin => Type == "cardBin";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "secureToken"
    /// </summary>
    public bool IsSecureToken => Type == "secureToken";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "digitalWallet"
    /// </summary>
    public bool IsDigitalWallet => Type == "digitalWallet";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.CardPayload"/> if <see cref="Type"/> is 'card', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'card'.</exception>
    public Payroc.CardPayload AsCard() =>
        IsCard
            ? (Payroc.CardPayload)Value!
            : throw new System.Exception("BinLookupCard.Type is not 'card'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.CardBinPayload"/> if <see cref="Type"/> is 'cardBin', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'cardBin'.</exception>
    public Payroc.CardBinPayload AsCardBin() =>
        IsCardBin
            ? (Payroc.CardBinPayload)Value!
            : throw new System.Exception("BinLookupCard.Type is not 'cardBin'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SecureTokenPayload"/> if <see cref="Type"/> is 'secureToken', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'secureToken'.</exception>
    public Payroc.SecureTokenPayload AsSecureToken() =>
        IsSecureToken
            ? (Payroc.SecureTokenPayload)Value!
            : throw new System.Exception("BinLookupCard.Type is not 'secureToken'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.DigitalWalletPayload"/> if <see cref="Type"/> is 'digitalWallet', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'digitalWallet'.</exception>
    public Payroc.DigitalWalletPayload AsDigitalWallet() =>
        IsDigitalWallet
            ? (Payroc.DigitalWalletPayload)Value!
            : throw new System.Exception("BinLookupCard.Type is not 'digitalWallet'");

    public T Match<T>(
        Func<Payroc.CardPayload, T> onCard,
        Func<Payroc.CardBinPayload, T> onCardBin,
        Func<Payroc.SecureTokenPayload, T> onSecureToken,
        Func<Payroc.DigitalWalletPayload, T> onDigitalWallet,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "card" => onCard(AsCard()),
            "cardBin" => onCardBin(AsCardBin()),
            "secureToken" => onSecureToken(AsSecureToken()),
            "digitalWallet" => onDigitalWallet(AsDigitalWallet()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.CardPayload> onCard,
        Action<Payroc.CardBinPayload> onCardBin,
        Action<Payroc.SecureTokenPayload> onSecureToken,
        Action<Payroc.DigitalWalletPayload> onDigitalWallet,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "card":
                onCard(AsCard());
                break;
            case "cardBin":
                onCardBin(AsCardBin());
                break;
            case "secureToken":
                onSecureToken(AsSecureToken());
                break;
            case "digitalWallet":
                onDigitalWallet(AsDigitalWallet());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
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
    /// Attempts to cast the value to a <see cref="Payroc.CardBinPayload"/> and returns true if successful.
    /// </summary>
    public bool TryAsCardBin(out Payroc.CardBinPayload? value)
    {
        if (Type == "cardBin")
        {
            value = (Payroc.CardBinPayload)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.SecureTokenPayload"/> and returns true if successful.
    /// </summary>
    public bool TryAsSecureToken(out Payroc.SecureTokenPayload? value)
    {
        if (Type == "secureToken")
        {
            value = (Payroc.SecureTokenPayload)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.DigitalWalletPayload"/> and returns true if successful.
    /// </summary>
    public bool TryAsDigitalWallet(out Payroc.DigitalWalletPayload? value)
    {
        if (Type == "digitalWallet")
        {
            value = (Payroc.DigitalWalletPayload)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator BinLookupCard(BinLookupCard.Card value) => new(value);

    public static implicit operator BinLookupCard(BinLookupCard.CardBin value) => new(value);

    public static implicit operator BinLookupCard(BinLookupCard.SecureToken value) => new(value);

    public static implicit operator BinLookupCard(BinLookupCard.DigitalWallet value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<BinLookupCard>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(BinLookupCard).IsAssignableFrom(typeToConvert);

        public override BinLookupCard Read(
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
                "card" => json.Deserialize<Payroc.CardPayload?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.CardPayload"),
                "cardBin" => json.Deserialize<Payroc.CardBinPayload?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.CardBinPayload"),
                "secureToken" => json.Deserialize<Payroc.SecureTokenPayload?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.SecureTokenPayload"),
                "digitalWallet" => json.Deserialize<Payroc.DigitalWalletPayload?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.DigitalWalletPayload"),
                _ => json.Deserialize<object?>(options),
            };
            return new BinLookupCard(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BinLookupCard value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "card" => JsonSerializer.SerializeToNode(value.Value, options),
                    "cardBin" => JsonSerializer.SerializeToNode(value.Value, options),
                    "secureToken" => JsonSerializer.SerializeToNode(value.Value, options),
                    "digitalWallet" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
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

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator BinLookupCard.Card(Payroc.CardPayload value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for cardBin
    /// </summary>
    [Serializable]
    public struct CardBin
    {
        public CardBin(Payroc.CardBinPayload value)
        {
            Value = value;
        }

        internal Payroc.CardBinPayload Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator BinLookupCard.CardBin(Payroc.CardBinPayload value) =>
            new(value);
    }

    /// <summary>
    /// Discriminated union type for secureToken
    /// </summary>
    [Serializable]
    public struct SecureToken
    {
        public SecureToken(Payroc.SecureTokenPayload value)
        {
            Value = value;
        }

        internal Payroc.SecureTokenPayload Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator BinLookupCard.SecureToken(
            Payroc.SecureTokenPayload value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for digitalWallet
    /// </summary>
    [Serializable]
    public struct DigitalWallet
    {
        public DigitalWallet(Payroc.DigitalWalletPayload value)
        {
            Value = value;
        }

        internal Payroc.DigitalWalletPayload Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator BinLookupCard.DigitalWallet(
            Payroc.DigitalWalletPayload value
        ) => new(value);
    }
}

// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.CurrencyConversion;

/// <summary>
/// Object that contains information about the customer's payment details.
/// </summary>
[JsonConverter(typeof(FxRateInquiryPaymentMethod.JsonConverter))]
[Serializable]
public record FxRateInquiryPaymentMethod
{
    internal FxRateInquiryPaymentMethod(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of FxRateInquiryPaymentMethod with <see cref="FxRateInquiryPaymentMethod.Card"/>.
    /// </summary>
    public FxRateInquiryPaymentMethod(FxRateInquiryPaymentMethod.Card value)
    {
        Type = "card";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of FxRateInquiryPaymentMethod with <see cref="FxRateInquiryPaymentMethod.SecureToken"/>.
    /// </summary>
    public FxRateInquiryPaymentMethod(FxRateInquiryPaymentMethod.SecureToken value)
    {
        Type = "secureToken";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of FxRateInquiryPaymentMethod with <see cref="FxRateInquiryPaymentMethod.DigitalWallet"/>.
    /// </summary>
    public FxRateInquiryPaymentMethod(FxRateInquiryPaymentMethod.DigitalWallet value)
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
            : throw new Exception("FxRateInquiryPaymentMethod.Type is not 'card'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SecureTokenPayload"/> if <see cref="Type"/> is 'secureToken', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'secureToken'.</exception>
    public Payroc.SecureTokenPayload AsSecureToken() =>
        IsSecureToken
            ? (Payroc.SecureTokenPayload)Value!
            : throw new Exception("FxRateInquiryPaymentMethod.Type is not 'secureToken'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.DigitalWalletPayload"/> if <see cref="Type"/> is 'digitalWallet', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'digitalWallet'.</exception>
    public Payroc.DigitalWalletPayload AsDigitalWallet() =>
        IsDigitalWallet
            ? (Payroc.DigitalWalletPayload)Value!
            : throw new Exception("FxRateInquiryPaymentMethod.Type is not 'digitalWallet'");

    public T Match<T>(
        Func<Payroc.CardPayload, T> onCard,
        Func<Payroc.SecureTokenPayload, T> onSecureToken,
        Func<Payroc.DigitalWalletPayload, T> onDigitalWallet,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "card" => onCard(AsCard()),
            "secureToken" => onSecureToken(AsSecureToken()),
            "digitalWallet" => onDigitalWallet(AsDigitalWallet()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.CardPayload> onCard,
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

    public static implicit operator FxRateInquiryPaymentMethod(
        FxRateInquiryPaymentMethod.Card value
    ) => new(value);

    public static implicit operator FxRateInquiryPaymentMethod(
        FxRateInquiryPaymentMethod.SecureToken value
    ) => new(value);

    public static implicit operator FxRateInquiryPaymentMethod(
        FxRateInquiryPaymentMethod.DigitalWallet value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<FxRateInquiryPaymentMethod>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(FxRateInquiryPaymentMethod).IsAssignableFrom(typeToConvert);

        public override FxRateInquiryPaymentMethod Read(
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
                "card" => json.Deserialize<Payroc.CardPayload>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.CardPayload"),
                "secureToken" => json.Deserialize<Payroc.SecureTokenPayload>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.SecureTokenPayload"),
                "digitalWallet" => json.Deserialize<Payroc.DigitalWalletPayload>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.DigitalWalletPayload"),
                _ => json.Deserialize<object?>(options),
            };
            return new FxRateInquiryPaymentMethod(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            FxRateInquiryPaymentMethod value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "card" => JsonSerializer.SerializeToNode(value.Value, options),
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

        public override string ToString() => Value.ToString();

        public static implicit operator Card(Payroc.CardPayload value) => new(value);
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

        public override string ToString() => Value.ToString();

        public static implicit operator SecureToken(Payroc.SecureTokenPayload value) => new(value);
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

        public override string ToString() => Value.ToString();

        public static implicit operator DigitalWallet(Payroc.DigitalWalletPayload value) =>
            new(value);
    }
}

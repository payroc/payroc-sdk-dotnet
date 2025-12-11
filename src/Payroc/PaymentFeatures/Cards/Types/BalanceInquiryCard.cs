// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.PaymentFeatures.Cards;

/// <summary>
/// Object that contains information about the card.
/// </summary>
[JsonConverter(typeof(BalanceInquiryCard.JsonConverter))]
[Serializable]
public record BalanceInquiryCard
{
    internal BalanceInquiryCard(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of BalanceInquiryCard with <see cref="BalanceInquiryCard.Card"/>.
    /// </summary>
    public BalanceInquiryCard(BalanceInquiryCard.Card value)
    {
        Type = "card";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of BalanceInquiryCard with <see cref="BalanceInquiryCard.SingleUseToken"/>.
    /// </summary>
    public BalanceInquiryCard(BalanceInquiryCard.SingleUseToken value)
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
    /// Returns true if <see cref="Type"/> is "card"
    /// </summary>
    public bool IsCard => Type == "card";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "singleUseToken"
    /// </summary>
    public bool IsSingleUseToken => Type == "singleUseToken";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.CardPayload"/> if <see cref="Type"/> is 'card', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'card'.</exception>
    public Payroc.CardPayload AsCard() =>
        IsCard
            ? (Payroc.CardPayload)Value!
            : throw new System.Exception("BalanceInquiryCard.Type is not 'card'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SingleUseTokenPayload"/> if <see cref="Type"/> is 'singleUseToken', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'singleUseToken'.</exception>
    public Payroc.SingleUseTokenPayload AsSingleUseToken() =>
        IsSingleUseToken
            ? (Payroc.SingleUseTokenPayload)Value!
            : throw new System.Exception("BalanceInquiryCard.Type is not 'singleUseToken'");

    public T Match<T>(
        Func<Payroc.CardPayload, T> onCard,
        Func<Payroc.SingleUseTokenPayload, T> onSingleUseToken,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "card" => onCard(AsCard()),
            "singleUseToken" => onSingleUseToken(AsSingleUseToken()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.CardPayload> onCard,
        Action<Payroc.SingleUseTokenPayload> onSingleUseToken,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
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

    public static implicit operator BalanceInquiryCard(BalanceInquiryCard.Card value) => new(value);

    public static implicit operator BalanceInquiryCard(BalanceInquiryCard.SingleUseToken value) =>
        new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<BalanceInquiryCard>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(BalanceInquiryCard).IsAssignableFrom(typeToConvert);

        public override BalanceInquiryCard Read(
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
                "singleUseToken" => json.Deserialize<Payroc.SingleUseTokenPayload?>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.SingleUseTokenPayload"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new BalanceInquiryCard(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BalanceInquiryCard value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "card" => JsonSerializer.SerializeToNode(value.Value, options),
                    "singleUseToken" => JsonSerializer.SerializeToNode(value.Value, options),
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

        public static implicit operator BalanceInquiryCard.Card(Payroc.CardPayload value) =>
            new(value);
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

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator BalanceInquiryCard.SingleUseToken(
            Payroc.SingleUseTokenPayload value
        ) => new(value);
    }
}

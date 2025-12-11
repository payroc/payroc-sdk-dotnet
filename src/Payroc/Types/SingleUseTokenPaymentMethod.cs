// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the customer's payment details.
/// </summary>
[JsonConverter(typeof(SingleUseTokenPaymentMethod.JsonConverter))]
[Serializable]
public record SingleUseTokenPaymentMethod
{
    internal SingleUseTokenPaymentMethod(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of SingleUseTokenPaymentMethod with <see cref="SingleUseTokenPaymentMethod.Card"/>.
    /// </summary>
    public SingleUseTokenPaymentMethod(SingleUseTokenPaymentMethod.Card value)
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
    /// Returns true if <see cref="Type"/> is "card"
    /// </summary>
    public bool IsCard => Type == "card";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.CardPayload"/> if <see cref="Type"/> is 'card', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'card'.</exception>
    public Payroc.CardPayload AsCard() =>
        IsCard
            ? (Payroc.CardPayload)Value!
            : throw new System.Exception("SingleUseTokenPaymentMethod.Type is not 'card'");

    public T Match<T>(Func<Payroc.CardPayload, T> onCard, Func<string, object?, T> onUnknown_)
    {
        return Type switch
        {
            "card" => onCard(AsCard()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(Action<Payroc.CardPayload> onCard, Action<string, object?> onUnknown_)
    {
        switch (Type)
        {
            case "card":
                onCard(AsCard());
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

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator SingleUseTokenPaymentMethod(
        SingleUseTokenPaymentMethod.Card value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<SingleUseTokenPaymentMethod>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(SingleUseTokenPaymentMethod).IsAssignableFrom(typeToConvert);

        public override SingleUseTokenPaymentMethod Read(
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
                _ => json.Deserialize<object?>(options),
            };
            return new SingleUseTokenPaymentMethod(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SingleUseTokenPaymentMethod value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "card" => JsonSerializer.SerializeToNode(value.Value, options),
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

        public static implicit operator SingleUseTokenPaymentMethod.Card(
            Payroc.CardPayload value
        ) => new(value);
    }
}

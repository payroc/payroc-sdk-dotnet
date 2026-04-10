// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using global::System.Text.Json;
using global::System.Text.Json.Nodes;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Polymorphic object that contains payment card details.
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
            : throw new global::System.Exception("SingleUseTokenPaymentMethod.Type is not 'card'");

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
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(SingleUseTokenPaymentMethod).IsAssignableFrom(typeToConvert);

        public override SingleUseTokenPaymentMethod Read(
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

            // Strip the discriminant property to prevent it from leaking into AdditionalProperties
            var jsonObject = System.Text.Json.Nodes.JsonObject.Create(json);
            jsonObject?.Remove("type");
            var jsonWithoutDiscriminator =
                jsonObject != null ? JsonSerializer.SerializeToElement(jsonObject, options) : json;

            var value = discriminator switch
            {
                "card" => jsonWithoutDiscriminator.Deserialize<Payroc.CardPayload?>(options)
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

        public override SingleUseTokenPaymentMethod ReadAsPropertyName(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new JsonException("The JSON property name could not be read as a string.");
            return new SingleUseTokenPaymentMethod(stringValue, stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            SingleUseTokenPaymentMethod value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Type);
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

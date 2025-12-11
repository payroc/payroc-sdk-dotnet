// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.RepeatPayments.Subscriptions;

/// <summary>
/// Object that contains information about the customer's payment details.
/// </summary>
[JsonConverter(typeof(SubscriptionRequestPaymentMethod.JsonConverter))]
[Serializable]
public record SubscriptionRequestPaymentMethod
{
    internal SubscriptionRequestPaymentMethod(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of SubscriptionRequestPaymentMethod with <see cref="SubscriptionRequestPaymentMethod.SecureToken"/>.
    /// </summary>
    public SubscriptionRequestPaymentMethod(SubscriptionRequestPaymentMethod.SecureToken value)
    {
        Type = "secureToken";
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
    /// Returns true if <see cref="Type"/> is "secureToken"
    /// </summary>
    public bool IsSecureToken => Type == "secureToken";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SecureTokenPayload"/> if <see cref="Type"/> is 'secureToken', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'secureToken'.</exception>
    public Payroc.SecureTokenPayload AsSecureToken() =>
        IsSecureToken
            ? (Payroc.SecureTokenPayload)Value!
            : throw new System.Exception(
                "SubscriptionRequestPaymentMethod.Type is not 'secureToken'"
            );

    public T Match<T>(
        Func<Payroc.SecureTokenPayload, T> onSecureToken,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "secureToken" => onSecureToken(AsSecureToken()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.SecureTokenPayload> onSecureToken,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "secureToken":
                onSecureToken(AsSecureToken());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
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

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator SubscriptionRequestPaymentMethod(
        SubscriptionRequestPaymentMethod.SecureToken value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<SubscriptionRequestPaymentMethod>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(SubscriptionRequestPaymentMethod).IsAssignableFrom(typeToConvert);

        public override SubscriptionRequestPaymentMethod Read(
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
                "secureToken" => json.Deserialize<Payroc.SecureTokenPayload?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.SecureTokenPayload"),
                _ => json.Deserialize<object?>(options),
            };
            return new SubscriptionRequestPaymentMethod(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SubscriptionRequestPaymentMethod value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "secureToken" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
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

        public static implicit operator SubscriptionRequestPaymentMethod.SecureToken(
            Payroc.SecureTokenPayload value
        ) => new(value);
    }
}

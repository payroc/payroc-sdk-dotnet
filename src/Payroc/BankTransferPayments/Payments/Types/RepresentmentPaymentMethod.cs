// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.BankTransferPayments.Payments;

/// <summary>
/// Object that contains information about the customer's payment details.
/// </summary>
[JsonConverter(typeof(RepresentmentPaymentMethod.JsonConverter))]
[Serializable]
public record RepresentmentPaymentMethod
{
    internal RepresentmentPaymentMethod(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of RepresentmentPaymentMethod with <see cref="RepresentmentPaymentMethod.Ach"/>.
    /// </summary>
    public RepresentmentPaymentMethod(RepresentmentPaymentMethod.Ach value)
    {
        Type = "ach";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of RepresentmentPaymentMethod with <see cref="RepresentmentPaymentMethod.SecureToken"/>.
    /// </summary>
    public RepresentmentPaymentMethod(RepresentmentPaymentMethod.SecureToken value)
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
    /// Returns true if <see cref="Type"/> is "ach"
    /// </summary>
    public bool IsAch => Type == "ach";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "secureToken"
    /// </summary>
    public bool IsSecureToken => Type == "secureToken";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.AchPayload"/> if <see cref="Type"/> is 'ach', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'ach'.</exception>
    public Payroc.AchPayload AsAch() =>
        IsAch
            ? (Payroc.AchPayload)Value!
            : throw new System.Exception("RepresentmentPaymentMethod.Type is not 'ach'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SecureTokenPayload"/> if <see cref="Type"/> is 'secureToken', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'secureToken'.</exception>
    public Payroc.SecureTokenPayload AsSecureToken() =>
        IsSecureToken
            ? (Payroc.SecureTokenPayload)Value!
            : throw new System.Exception("RepresentmentPaymentMethod.Type is not 'secureToken'");

    public T Match<T>(
        Func<Payroc.AchPayload, T> onAch,
        Func<Payroc.SecureTokenPayload, T> onSecureToken,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "ach" => onAch(AsAch()),
            "secureToken" => onSecureToken(AsSecureToken()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.AchPayload> onAch,
        Action<Payroc.SecureTokenPayload> onSecureToken,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "ach":
                onAch(AsAch());
                break;
            case "secureToken":
                onSecureToken(AsSecureToken());
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

    public static implicit operator RepresentmentPaymentMethod(
        RepresentmentPaymentMethod.Ach value
    ) => new(value);

    public static implicit operator RepresentmentPaymentMethod(
        RepresentmentPaymentMethod.SecureToken value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<RepresentmentPaymentMethod>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(RepresentmentPaymentMethod).IsAssignableFrom(typeToConvert);

        public override RepresentmentPaymentMethod Read(
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
                "ach" => json.Deserialize<Payroc.AchPayload?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.AchPayload"),
                "secureToken" => json.Deserialize<Payroc.SecureTokenPayload?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.SecureTokenPayload"),
                _ => json.Deserialize<object?>(options),
            };
            return new RepresentmentPaymentMethod(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RepresentmentPaymentMethod value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "ach" => JsonSerializer.SerializeToNode(value.Value, options),
                    "secureToken" => JsonSerializer.SerializeToNode(value.Value, options),
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

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator RepresentmentPaymentMethod.Ach(Payroc.AchPayload value) =>
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

        public static implicit operator RepresentmentPaymentMethod.SecureToken(
            Payroc.SecureTokenPayload value
        ) => new(value);
    }
}

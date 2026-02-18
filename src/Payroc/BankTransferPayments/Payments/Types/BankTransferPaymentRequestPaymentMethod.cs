// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.BankTransferPayments.Payments;

/// <summary>
/// Polymorphic object that contains payment detail information.
///
/// The value of the type parameter determines which variant you should use:
/// -	`ach` - Automated Clearing House (ACH) details
/// -	`pad` - Pre-authorized debit (PAD) details
/// -	`secureToken` - Secure token details
/// -	`singleUseToken` - Single-use token details
/// </summary>
[JsonConverter(typeof(BankTransferPaymentRequestPaymentMethod.JsonConverter))]
[Serializable]
public record BankTransferPaymentRequestPaymentMethod
{
    internal BankTransferPaymentRequestPaymentMethod(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of BankTransferPaymentRequestPaymentMethod with <see cref="BankTransferPaymentRequestPaymentMethod.Ach"/>.
    /// </summary>
    public BankTransferPaymentRequestPaymentMethod(
        BankTransferPaymentRequestPaymentMethod.Ach value
    )
    {
        Type = "ach";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of BankTransferPaymentRequestPaymentMethod with <see cref="BankTransferPaymentRequestPaymentMethod.Pad"/>.
    /// </summary>
    public BankTransferPaymentRequestPaymentMethod(
        BankTransferPaymentRequestPaymentMethod.Pad value
    )
    {
        Type = "pad";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of BankTransferPaymentRequestPaymentMethod with <see cref="BankTransferPaymentRequestPaymentMethod.SecureToken"/>.
    /// </summary>
    public BankTransferPaymentRequestPaymentMethod(
        BankTransferPaymentRequestPaymentMethod.SecureToken value
    )
    {
        Type = "secureToken";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of BankTransferPaymentRequestPaymentMethod with <see cref="BankTransferPaymentRequestPaymentMethod.SingleUseToken"/>.
    /// </summary>
    public BankTransferPaymentRequestPaymentMethod(
        BankTransferPaymentRequestPaymentMethod.SingleUseToken value
    )
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
    /// Returns true if <see cref="Type"/> is "secureToken"
    /// </summary>
    public bool IsSecureToken => Type == "secureToken";

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
            : throw new System.Exception(
                "BankTransferPaymentRequestPaymentMethod.Type is not 'ach'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PadPayload"/> if <see cref="Type"/> is 'pad', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'pad'.</exception>
    public Payroc.PadPayload AsPad() =>
        IsPad
            ? (Payroc.PadPayload)Value!
            : throw new System.Exception(
                "BankTransferPaymentRequestPaymentMethod.Type is not 'pad'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SecureTokenPayload"/> if <see cref="Type"/> is 'secureToken', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'secureToken'.</exception>
    public Payroc.SecureTokenPayload AsSecureToken() =>
        IsSecureToken
            ? (Payroc.SecureTokenPayload)Value!
            : throw new System.Exception(
                "BankTransferPaymentRequestPaymentMethod.Type is not 'secureToken'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SingleUseTokenPayload"/> if <see cref="Type"/> is 'singleUseToken', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'singleUseToken'.</exception>
    public Payroc.SingleUseTokenPayload AsSingleUseToken() =>
        IsSingleUseToken
            ? (Payroc.SingleUseTokenPayload)Value!
            : throw new System.Exception(
                "BankTransferPaymentRequestPaymentMethod.Type is not 'singleUseToken'"
            );

    public T Match<T>(
        Func<Payroc.AchPayload, T> onAch,
        Func<Payroc.PadPayload, T> onPad,
        Func<Payroc.SecureTokenPayload, T> onSecureToken,
        Func<Payroc.SingleUseTokenPayload, T> onSingleUseToken,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "ach" => onAch(AsAch()),
            "pad" => onPad(AsPad()),
            "secureToken" => onSecureToken(AsSecureToken()),
            "singleUseToken" => onSingleUseToken(AsSingleUseToken()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.AchPayload> onAch,
        Action<Payroc.PadPayload> onPad,
        Action<Payroc.SecureTokenPayload> onSecureToken,
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
            case "secureToken":
                onSecureToken(AsSecureToken());
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

    public static implicit operator BankTransferPaymentRequestPaymentMethod(
        BankTransferPaymentRequestPaymentMethod.Ach value
    ) => new(value);

    public static implicit operator BankTransferPaymentRequestPaymentMethod(
        BankTransferPaymentRequestPaymentMethod.Pad value
    ) => new(value);

    public static implicit operator BankTransferPaymentRequestPaymentMethod(
        BankTransferPaymentRequestPaymentMethod.SecureToken value
    ) => new(value);

    public static implicit operator BankTransferPaymentRequestPaymentMethod(
        BankTransferPaymentRequestPaymentMethod.SingleUseToken value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<BankTransferPaymentRequestPaymentMethod>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(BankTransferPaymentRequestPaymentMethod).IsAssignableFrom(typeToConvert);

        public override BankTransferPaymentRequestPaymentMethod Read(
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
                "pad" => json.Deserialize<Payroc.PadPayload?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.PadPayload"),
                "secureToken" => json.Deserialize<Payroc.SecureTokenPayload?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.SecureTokenPayload"),
                "singleUseToken" => json.Deserialize<Payroc.SingleUseTokenPayload?>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.SingleUseTokenPayload"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new BankTransferPaymentRequestPaymentMethod(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BankTransferPaymentRequestPaymentMethod value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "ach" => JsonSerializer.SerializeToNode(value.Value, options),
                    "pad" => JsonSerializer.SerializeToNode(value.Value, options),
                    "secureToken" => JsonSerializer.SerializeToNode(value.Value, options),
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

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator BankTransferPaymentRequestPaymentMethod.Ach(
            Payroc.AchPayload value
        ) => new(value);
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

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator BankTransferPaymentRequestPaymentMethod.Pad(
            Payroc.PadPayload value
        ) => new(value);
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

        public static implicit operator BankTransferPaymentRequestPaymentMethod.SecureToken(
            Payroc.SecureTokenPayload value
        ) => new(value);
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

        public static implicit operator BankTransferPaymentRequestPaymentMethod.SingleUseToken(
            Payroc.SingleUseTokenPayload value
        ) => new(value);
    }
}

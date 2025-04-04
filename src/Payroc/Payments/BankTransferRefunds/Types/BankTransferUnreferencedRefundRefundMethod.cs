// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.BankTransferRefunds;

/// <summary>
/// Object that contains information about how the merchant refunds the customer.
/// </summary>
[JsonConverter(typeof(BankTransferUnreferencedRefundRefundMethod.JsonConverter))]
public record BankTransferUnreferencedRefundRefundMethod
{
    internal BankTransferUnreferencedRefundRefundMethod(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of BankTransferUnreferencedRefundRefundMethod with <see cref="BankTransferUnreferencedRefundRefundMethod.Ach"/>.
    /// </summary>
    public BankTransferUnreferencedRefundRefundMethod(
        BankTransferUnreferencedRefundRefundMethod.Ach value
    )
    {
        Type = "ach";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of BankTransferUnreferencedRefundRefundMethod with <see cref="BankTransferUnreferencedRefundRefundMethod.Pad"/>.
    /// </summary>
    public BankTransferUnreferencedRefundRefundMethod(
        BankTransferUnreferencedRefundRefundMethod.Pad value
    )
    {
        Type = "pad";
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
    /// Returns the value as a <see cref="Payroc.AchPayload"/> if <see cref="Type"/> is 'ach', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'ach'.</exception>
    public Payroc.AchPayload AsAch() =>
        IsAch
            ? (Payroc.AchPayload)Value!
            : throw new Exception("BankTransferUnreferencedRefundRefundMethod.Type is not 'ach'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SecureTokenPayload"/> if <see cref="Type"/> is 'pad', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'pad'.</exception>
    public Payroc.SecureTokenPayload AsPad() =>
        IsPad
            ? (Payroc.SecureTokenPayload)Value!
            : throw new Exception("BankTransferUnreferencedRefundRefundMethod.Type is not 'pad'");

    public T Match<T>(
        Func<Payroc.AchPayload, T> onAch,
        Func<Payroc.SecureTokenPayload, T> onPad,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "ach" => onAch(AsAch()),
            "pad" => onPad(AsPad()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.AchPayload> onAch,
        Action<Payroc.SecureTokenPayload> onPad,
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
    public bool TryAsPad(out Payroc.SecureTokenPayload? value)
    {
        if (Type == "pad")
        {
            value = (Payroc.SecureTokenPayload)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator BankTransferUnreferencedRefundRefundMethod(
        BankTransferUnreferencedRefundRefundMethod.Ach value
    ) => new(value);

    public static implicit operator BankTransferUnreferencedRefundRefundMethod(
        BankTransferUnreferencedRefundRefundMethod.Pad value
    ) => new(value);

    internal sealed class JsonConverter : JsonConverter<BankTransferUnreferencedRefundRefundMethod>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(BankTransferUnreferencedRefundRefundMethod).IsAssignableFrom(typeToConvert);

        public override BankTransferUnreferencedRefundRefundMethod Read(
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
                "pad" => json.Deserialize<Payroc.SecureTokenPayload>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.SecureTokenPayload"),
                _ => json.Deserialize<object?>(options),
            };
            return new BankTransferUnreferencedRefundRefundMethod(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BankTransferUnreferencedRefundRefundMethod value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "ach" => JsonSerializer.SerializeToNode(value.Value, options),
                    "pad" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for ach
    /// </summary>
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
    public struct Pad
    {
        public Pad(Payroc.SecureTokenPayload value)
        {
            Value = value;
        }

        internal Payroc.SecureTokenPayload Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Pad(Payroc.SecureTokenPayload value) => new(value);
    }
}

// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.PaymentFeatures.Bank;

/// <summary>
/// Object that contains information about the bank account.
/// </summary>
[JsonConverter(typeof(BankAccountVerificationRequestBankAccount.JsonConverter))]
[Serializable]
public record BankAccountVerificationRequestBankAccount
{
    internal BankAccountVerificationRequestBankAccount(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of BankAccountVerificationRequestBankAccount with <see cref="BankAccountVerificationRequestBankAccount.Ach"/>.
    /// </summary>
    public BankAccountVerificationRequestBankAccount(
        BankAccountVerificationRequestBankAccount.Ach value
    )
    {
        Type = "ach";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of BankAccountVerificationRequestBankAccount with <see cref="BankAccountVerificationRequestBankAccount.Pad"/>.
    /// </summary>
    public BankAccountVerificationRequestBankAccount(
        BankAccountVerificationRequestBankAccount.Pad value
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
            : throw new System.Exception(
                "BankAccountVerificationRequestBankAccount.Type is not 'ach'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PadPayload"/> if <see cref="Type"/> is 'pad', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'pad'.</exception>
    public Payroc.PadPayload AsPad() =>
        IsPad
            ? (Payroc.PadPayload)Value!
            : throw new System.Exception(
                "BankAccountVerificationRequestBankAccount.Type is not 'pad'"
            );

    public T Match<T>(
        Func<Payroc.AchPayload, T> onAch,
        Func<Payroc.PadPayload, T> onPad,
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
        Action<Payroc.PadPayload> onPad,
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

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator BankAccountVerificationRequestBankAccount(
        BankAccountVerificationRequestBankAccount.Ach value
    ) => new(value);

    public static implicit operator BankAccountVerificationRequestBankAccount(
        BankAccountVerificationRequestBankAccount.Pad value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<BankAccountVerificationRequestBankAccount>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(BankAccountVerificationRequestBankAccount).IsAssignableFrom(typeToConvert);

        public override BankAccountVerificationRequestBankAccount Read(
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
                _ => json.Deserialize<object?>(options),
            };
            return new BankAccountVerificationRequestBankAccount(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BankAccountVerificationRequestBankAccount value,
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
    [Serializable]
    public struct Ach
    {
        public Ach(Payroc.AchPayload value)
        {
            Value = value;
        }

        internal Payroc.AchPayload Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator BankAccountVerificationRequestBankAccount.Ach(
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

        public static implicit operator BankAccountVerificationRequestBankAccount.Pad(
            Payroc.PadPayload value
        ) => new(value);
    }
}

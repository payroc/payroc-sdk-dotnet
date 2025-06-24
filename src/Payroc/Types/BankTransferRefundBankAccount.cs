// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the bank account.
/// </summary>
[JsonConverter(typeof(BankTransferRefundBankAccount.JsonConverter))]
[Serializable]
public record BankTransferRefundBankAccount
{
    internal BankTransferRefundBankAccount(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of BankTransferRefundBankAccount with <see cref="BankTransferRefundBankAccount.Ach"/>.
    /// </summary>
    public BankTransferRefundBankAccount(BankTransferRefundBankAccount.Ach value)
    {
        Type = "ach";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of BankTransferRefundBankAccount with <see cref="BankTransferRefundBankAccount.Pad"/>.
    /// </summary>
    public BankTransferRefundBankAccount(BankTransferRefundBankAccount.Pad value)
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
    /// Returns the value as a <see cref="Payroc.AchBankAccount"/> if <see cref="Type"/> is 'ach', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'ach'.</exception>
    public Payroc.AchBankAccount AsAch() =>
        IsAch
            ? (Payroc.AchBankAccount)Value!
            : throw new Exception("BankTransferRefundBankAccount.Type is not 'ach'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PadBankAccount"/> if <see cref="Type"/> is 'pad', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'pad'.</exception>
    public Payroc.PadBankAccount AsPad() =>
        IsPad
            ? (Payroc.PadBankAccount)Value!
            : throw new Exception("BankTransferRefundBankAccount.Type is not 'pad'");

    public T Match<T>(
        Func<Payroc.AchBankAccount, T> onAch,
        Func<Payroc.PadBankAccount, T> onPad,
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
        Action<Payroc.AchBankAccount> onAch,
        Action<Payroc.PadBankAccount> onPad,
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
    /// Attempts to cast the value to a <see cref="Payroc.AchBankAccount"/> and returns true if successful.
    /// </summary>
    public bool TryAsAch(out Payroc.AchBankAccount? value)
    {
        if (Type == "ach")
        {
            value = (Payroc.AchBankAccount)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PadBankAccount"/> and returns true if successful.
    /// </summary>
    public bool TryAsPad(out Payroc.PadBankAccount? value)
    {
        if (Type == "pad")
        {
            value = (Payroc.PadBankAccount)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator BankTransferRefundBankAccount(
        BankTransferRefundBankAccount.Ach value
    ) => new(value);

    public static implicit operator BankTransferRefundBankAccount(
        BankTransferRefundBankAccount.Pad value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<BankTransferRefundBankAccount>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(BankTransferRefundBankAccount).IsAssignableFrom(typeToConvert);

        public override BankTransferRefundBankAccount Read(
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
                "ach" => json.Deserialize<Payroc.AchBankAccount>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.AchBankAccount"),
                "pad" => json.Deserialize<Payroc.PadBankAccount>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.PadBankAccount"),
                _ => json.Deserialize<object?>(options),
            };
            return new BankTransferRefundBankAccount(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BankTransferRefundBankAccount value,
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
        public Ach(Payroc.AchBankAccount value)
        {
            Value = value;
        }

        internal Payroc.AchBankAccount Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Ach(Payroc.AchBankAccount value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for pad
    /// </summary>
    [Serializable]
    public struct Pad
    {
        public Pad(Payroc.PadBankAccount value)
        {
            Value = value;
        }

        internal Payroc.PadBankAccount Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Pad(Payroc.PadBankAccount value) => new(value);
    }
}

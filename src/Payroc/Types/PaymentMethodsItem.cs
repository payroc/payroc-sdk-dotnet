// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(PaymentMethodsItem.JsonConverter))]
[Serializable]
public record PaymentMethodsItem
{
    internal PaymentMethodsItem(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of PaymentMethodsItem with <see cref="PaymentMethodsItem.Ach"/>.
    /// </summary>
    public PaymentMethodsItem(PaymentMethodsItem.Ach value)
    {
        Type = "ach";
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
    /// Returns the value as a <see cref="Payroc.PaymentMethodAch"/> if <see cref="Type"/> is 'ach', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'ach'.</exception>
    public Payroc.PaymentMethodAch AsAch() =>
        IsAch
            ? (Payroc.PaymentMethodAch)Value!
            : throw new System.Exception("PaymentMethodsItem.Type is not 'ach'");

    public T Match<T>(Func<Payroc.PaymentMethodAch, T> onAch, Func<string, object?, T> onUnknown_)
    {
        return Type switch
        {
            "ach" => onAch(AsAch()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(Action<Payroc.PaymentMethodAch> onAch, Action<string, object?> onUnknown_)
    {
        switch (Type)
        {
            case "ach":
                onAch(AsAch());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PaymentMethodAch"/> and returns true if successful.
    /// </summary>
    public bool TryAsAch(out Payroc.PaymentMethodAch? value)
    {
        if (Type == "ach")
        {
            value = (Payroc.PaymentMethodAch)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator PaymentMethodsItem(PaymentMethodsItem.Ach value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<PaymentMethodsItem>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(PaymentMethodsItem).IsAssignableFrom(typeToConvert);

        public override PaymentMethodsItem Read(
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
                "ach" => json.Deserialize<Payroc.PaymentMethodAch?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.PaymentMethodAch"),
                _ => json.Deserialize<object?>(options),
            };
            return new PaymentMethodsItem(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PaymentMethodsItem value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "ach" => JsonSerializer.SerializeToNode(value.Value, options),
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
        public Ach(Payroc.PaymentMethodAch value)
        {
            Value = value;
        }

        internal Payroc.PaymentMethodAch Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator PaymentMethodsItem.Ach(Payroc.PaymentMethodAch value) =>
            new(value);
    }
}

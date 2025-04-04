// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.BankTransferPayments;

/// <summary>
/// Object that contains information about the customer's payment details.
/// </summary>
[JsonConverter(typeof(RepresentmentPaymentMethod.JsonConverter))]
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
    /// Returns the value as a <see cref="Payroc.AchPayload"/> if <see cref="Type"/> is 'ach', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'ach'.</exception>
    public Payroc.AchPayload AsAch() =>
        IsAch
            ? (Payroc.AchPayload)Value!
            : throw new Exception("RepresentmentPaymentMethod.Type is not 'ach'");

    public T Match<T>(Func<Payroc.AchPayload, T> onAch, Func<string, object?, T> onUnknown_)
    {
        return Type switch
        {
            "ach" => onAch(AsAch()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(Action<Payroc.AchPayload> onAch, Action<string, object?> onUnknown_)
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

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator RepresentmentPaymentMethod(
        RepresentmentPaymentMethod.Ach value
    ) => new(value);

    internal sealed class JsonConverter : JsonConverter<RepresentmentPaymentMethod>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(RepresentmentPaymentMethod).IsAssignableFrom(typeToConvert);

        public override RepresentmentPaymentMethod Read(
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
}

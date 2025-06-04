// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.PaymentLinks;

[JsonConverter(typeof(GetPaymentLinksResponse.JsonConverter))]
public record GetPaymentLinksResponse
{
    internal GetPaymentLinksResponse(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of GetPaymentLinksResponse with <see cref="GetPaymentLinksResponse.MultiUse"/>.
    /// </summary>
    public GetPaymentLinksResponse(GetPaymentLinksResponse.MultiUse value)
    {
        Type = "multiUse";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of GetPaymentLinksResponse with <see cref="GetPaymentLinksResponse.SingleUse"/>.
    /// </summary>
    public GetPaymentLinksResponse(GetPaymentLinksResponse.SingleUse value)
    {
        Type = "singleUse";
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
    /// Returns true if <see cref="Type"/> is "multiUse"
    /// </summary>
    public bool IsMultiUse => Type == "multiUse";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "singleUse"
    /// </summary>
    public bool IsSingleUse => Type == "singleUse";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.MultiUsePaymentLink"/> if <see cref="Type"/> is 'multiUse', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'multiUse'.</exception>
    public Payroc.MultiUsePaymentLink AsMultiUse() =>
        IsMultiUse
            ? (Payroc.MultiUsePaymentLink)Value!
            : throw new Exception("GetPaymentLinksResponse.Type is not 'multiUse'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SingleUsePaymentLink"/> if <see cref="Type"/> is 'singleUse', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'singleUse'.</exception>
    public Payroc.SingleUsePaymentLink AsSingleUse() =>
        IsSingleUse
            ? (Payroc.SingleUsePaymentLink)Value!
            : throw new Exception("GetPaymentLinksResponse.Type is not 'singleUse'");

    public T Match<T>(
        Func<Payroc.MultiUsePaymentLink, T> onMultiUse,
        Func<Payroc.SingleUsePaymentLink, T> onSingleUse,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "multiUse" => onMultiUse(AsMultiUse()),
            "singleUse" => onSingleUse(AsSingleUse()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.MultiUsePaymentLink> onMultiUse,
        Action<Payroc.SingleUsePaymentLink> onSingleUse,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "multiUse":
                onMultiUse(AsMultiUse());
                break;
            case "singleUse":
                onSingleUse(AsSingleUse());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.MultiUsePaymentLink"/> and returns true if successful.
    /// </summary>
    public bool TryAsMultiUse(out Payroc.MultiUsePaymentLink? value)
    {
        if (Type == "multiUse")
        {
            value = (Payroc.MultiUsePaymentLink)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.SingleUsePaymentLink"/> and returns true if successful.
    /// </summary>
    public bool TryAsSingleUse(out Payroc.SingleUsePaymentLink? value)
    {
        if (Type == "singleUse")
        {
            value = (Payroc.SingleUsePaymentLink)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator GetPaymentLinksResponse(
        GetPaymentLinksResponse.MultiUse value
    ) => new(value);

    public static implicit operator GetPaymentLinksResponse(
        GetPaymentLinksResponse.SingleUse value
    ) => new(value);

    internal sealed class JsonConverter : JsonConverter<GetPaymentLinksResponse>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(GetPaymentLinksResponse).IsAssignableFrom(typeToConvert);

        public override GetPaymentLinksResponse Read(
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
                "multiUse" => json.Deserialize<Payroc.MultiUsePaymentLink>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.MultiUsePaymentLink"),
                "singleUse" => json.Deserialize<Payroc.SingleUsePaymentLink>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.SingleUsePaymentLink"),
                _ => json.Deserialize<object?>(options),
            };
            return new GetPaymentLinksResponse(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            GetPaymentLinksResponse value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "multiUse" => JsonSerializer.SerializeToNode(value.Value, options),
                    "singleUse" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for multiUse
    /// </summary>
    public struct MultiUse
    {
        public MultiUse(Payroc.MultiUsePaymentLink value)
        {
            Value = value;
        }

        internal Payroc.MultiUsePaymentLink Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator MultiUse(Payroc.MultiUsePaymentLink value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for singleUse
    /// </summary>
    public struct SingleUse
    {
        public SingleUse(Payroc.SingleUsePaymentLink value)
        {
            Value = value;
        }

        internal Payroc.SingleUsePaymentLink Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator SingleUse(Payroc.SingleUsePaymentLink value) => new(value);
    }
}

// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Tokenization.SecureTokens;

/// <summary>
/// Polymorphic object that contains authentication information from 3-D Secure.
///
/// The value of the type parameter determines which variant you should use:
/// -	`gatewayThreeDSecure` - Use our gateway to run a 3-D Secure check.
/// -	`thirdPartyThreeDSecure` - Use a third party to run a 3-D Secure check.
/// </summary>
[JsonConverter(typeof(TokenizationRequestThreeDSecure.JsonConverter))]
[Serializable]
public record TokenizationRequestThreeDSecure
{
    internal TokenizationRequestThreeDSecure(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of TokenizationRequestThreeDSecure with <see cref="TokenizationRequestThreeDSecure.GatewayThreeDSecure"/>.
    /// </summary>
    public TokenizationRequestThreeDSecure(
        TokenizationRequestThreeDSecure.GatewayThreeDSecure value
    )
    {
        Type = "gatewayThreeDSecure";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of TokenizationRequestThreeDSecure with <see cref="TokenizationRequestThreeDSecure.ThirdPartyThreeDSecure"/>.
    /// </summary>
    public TokenizationRequestThreeDSecure(
        TokenizationRequestThreeDSecure.ThirdPartyThreeDSecure value
    )
    {
        Type = "thirdPartyThreeDSecure";
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
    /// Returns true if <see cref="Type"/> is "gatewayThreeDSecure"
    /// </summary>
    public bool IsGatewayThreeDSecure => Type == "gatewayThreeDSecure";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "thirdPartyThreeDSecure"
    /// </summary>
    public bool IsThirdPartyThreeDSecure => Type == "thirdPartyThreeDSecure";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.GatewayThreeDSecure"/> if <see cref="Type"/> is 'gatewayThreeDSecure', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'gatewayThreeDSecure'.</exception>
    public Payroc.GatewayThreeDSecure AsGatewayThreeDSecure() =>
        IsGatewayThreeDSecure
            ? (Payroc.GatewayThreeDSecure)Value!
            : throw new System.Exception(
                "TokenizationRequestThreeDSecure.Type is not 'gatewayThreeDSecure'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.ThirdPartyThreeDSecure"/> if <see cref="Type"/> is 'thirdPartyThreeDSecure', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'thirdPartyThreeDSecure'.</exception>
    public Payroc.ThirdPartyThreeDSecure AsThirdPartyThreeDSecure() =>
        IsThirdPartyThreeDSecure
            ? (Payroc.ThirdPartyThreeDSecure)Value!
            : throw new System.Exception(
                "TokenizationRequestThreeDSecure.Type is not 'thirdPartyThreeDSecure'"
            );

    public T Match<T>(
        Func<Payroc.GatewayThreeDSecure, T> onGatewayThreeDSecure,
        Func<Payroc.ThirdPartyThreeDSecure, T> onThirdPartyThreeDSecure,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "gatewayThreeDSecure" => onGatewayThreeDSecure(AsGatewayThreeDSecure()),
            "thirdPartyThreeDSecure" => onThirdPartyThreeDSecure(AsThirdPartyThreeDSecure()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.GatewayThreeDSecure> onGatewayThreeDSecure,
        Action<Payroc.ThirdPartyThreeDSecure> onThirdPartyThreeDSecure,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "gatewayThreeDSecure":
                onGatewayThreeDSecure(AsGatewayThreeDSecure());
                break;
            case "thirdPartyThreeDSecure":
                onThirdPartyThreeDSecure(AsThirdPartyThreeDSecure());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.GatewayThreeDSecure"/> and returns true if successful.
    /// </summary>
    public bool TryAsGatewayThreeDSecure(out Payroc.GatewayThreeDSecure? value)
    {
        if (Type == "gatewayThreeDSecure")
        {
            value = (Payroc.GatewayThreeDSecure)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.ThirdPartyThreeDSecure"/> and returns true if successful.
    /// </summary>
    public bool TryAsThirdPartyThreeDSecure(out Payroc.ThirdPartyThreeDSecure? value)
    {
        if (Type == "thirdPartyThreeDSecure")
        {
            value = (Payroc.ThirdPartyThreeDSecure)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator TokenizationRequestThreeDSecure(
        TokenizationRequestThreeDSecure.GatewayThreeDSecure value
    ) => new(value);

    public static implicit operator TokenizationRequestThreeDSecure(
        TokenizationRequestThreeDSecure.ThirdPartyThreeDSecure value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<TokenizationRequestThreeDSecure>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(TokenizationRequestThreeDSecure).IsAssignableFrom(typeToConvert);

        public override TokenizationRequestThreeDSecure Read(
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
                "gatewayThreeDSecure" => json.Deserialize<Payroc.GatewayThreeDSecure?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.GatewayThreeDSecure"),
                "thirdPartyThreeDSecure" => json.Deserialize<Payroc.ThirdPartyThreeDSecure?>(
                    options
                ) ?? throw new JsonException("Failed to deserialize Payroc.ThirdPartyThreeDSecure"),
                _ => json.Deserialize<object?>(options),
            };
            return new TokenizationRequestThreeDSecure(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TokenizationRequestThreeDSecure value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "gatewayThreeDSecure" => JsonSerializer.SerializeToNode(value.Value, options),
                    "thirdPartyThreeDSecure" => JsonSerializer.SerializeToNode(
                        value.Value,
                        options
                    ),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for gatewayThreeDSecure
    /// </summary>
    [Serializable]
    public struct GatewayThreeDSecure
    {
        public GatewayThreeDSecure(Payroc.GatewayThreeDSecure value)
        {
            Value = value;
        }

        internal Payroc.GatewayThreeDSecure Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator TokenizationRequestThreeDSecure.GatewayThreeDSecure(
            Payroc.GatewayThreeDSecure value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for thirdPartyThreeDSecure
    /// </summary>
    [Serializable]
    public struct ThirdPartyThreeDSecure
    {
        public ThirdPartyThreeDSecure(Payroc.ThirdPartyThreeDSecure value)
        {
            Value = value;
        }

        internal Payroc.ThirdPartyThreeDSecure Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator TokenizationRequestThreeDSecure.ThirdPartyThreeDSecure(
            Payroc.ThirdPartyThreeDSecure value
        ) => new(value);
    }
}

// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.CardPayments.Payments;

/// <summary>
/// Polymorphic object that contains authentication information from 3-D Secure.
///
/// The value of the serviceProvider parameter determines which variant you should use:
/// -	`gateway` - Use our gateway to run a 3-D Secure check.
/// -	`thirdParty` - Use a third party to run a 3-D Secure check.
/// </summary>
[JsonConverter(typeof(PaymentRequestThreeDSecure.JsonConverter))]
[Serializable]
public record PaymentRequestThreeDSecure
{
    internal PaymentRequestThreeDSecure(string type, object? value)
    {
        ServiceProvider = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of PaymentRequestThreeDSecure with <see cref="PaymentRequestThreeDSecure.Gateway"/>.
    /// </summary>
    public PaymentRequestThreeDSecure(PaymentRequestThreeDSecure.Gateway value)
    {
        ServiceProvider = "gateway";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PaymentRequestThreeDSecure with <see cref="PaymentRequestThreeDSecure.ThirdParty"/>.
    /// </summary>
    public PaymentRequestThreeDSecure(PaymentRequestThreeDSecure.ThirdParty value)
    {
        ServiceProvider = "thirdParty";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("serviceProvider")]
    public string ServiceProvider { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="ServiceProvider"/> is "gateway"
    /// </summary>
    public bool IsGateway => ServiceProvider == "gateway";

    /// <summary>
    /// Returns true if <see cref="ServiceProvider"/> is "thirdParty"
    /// </summary>
    public bool IsThirdParty => ServiceProvider == "thirdParty";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.GatewayThreeDSecure"/> if <see cref="ServiceProvider"/> is 'gateway', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="ServiceProvider"/> is not 'gateway'.</exception>
    public Payroc.GatewayThreeDSecure AsGateway() =>
        IsGateway
            ? (Payroc.GatewayThreeDSecure)Value!
            : throw new System.Exception(
                "PaymentRequestThreeDSecure.ServiceProvider is not 'gateway'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.ThirdPartyThreeDSecure"/> if <see cref="ServiceProvider"/> is 'thirdParty', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="ServiceProvider"/> is not 'thirdParty'.</exception>
    public Payroc.ThirdPartyThreeDSecure AsThirdParty() =>
        IsThirdParty
            ? (Payroc.ThirdPartyThreeDSecure)Value!
            : throw new System.Exception(
                "PaymentRequestThreeDSecure.ServiceProvider is not 'thirdParty'"
            );

    public T Match<T>(
        Func<Payroc.GatewayThreeDSecure, T> onGateway,
        Func<Payroc.ThirdPartyThreeDSecure, T> onThirdParty,
        Func<string, object?, T> onUnknown_
    )
    {
        return ServiceProvider switch
        {
            "gateway" => onGateway(AsGateway()),
            "thirdParty" => onThirdParty(AsThirdParty()),
            _ => onUnknown_(ServiceProvider, Value),
        };
    }

    public void Visit(
        Action<Payroc.GatewayThreeDSecure> onGateway,
        Action<Payroc.ThirdPartyThreeDSecure> onThirdParty,
        Action<string, object?> onUnknown_
    )
    {
        switch (ServiceProvider)
        {
            case "gateway":
                onGateway(AsGateway());
                break;
            case "thirdParty":
                onThirdParty(AsThirdParty());
                break;
            default:
                onUnknown_(ServiceProvider, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.GatewayThreeDSecure"/> and returns true if successful.
    /// </summary>
    public bool TryAsGateway(out Payroc.GatewayThreeDSecure? value)
    {
        if (ServiceProvider == "gateway")
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
    public bool TryAsThirdParty(out Payroc.ThirdPartyThreeDSecure? value)
    {
        if (ServiceProvider == "thirdParty")
        {
            value = (Payroc.ThirdPartyThreeDSecure)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator PaymentRequestThreeDSecure(
        PaymentRequestThreeDSecure.Gateway value
    ) => new(value);

    public static implicit operator PaymentRequestThreeDSecure(
        PaymentRequestThreeDSecure.ThirdParty value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<PaymentRequestThreeDSecure>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(PaymentRequestThreeDSecure).IsAssignableFrom(typeToConvert);

        public override PaymentRequestThreeDSecure Read(
            ref Utf8JsonReader reader,
            System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("serviceProvider", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'serviceProvider'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'serviceProvider' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'serviceProvider' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'serviceProvider' is null");

            var value = discriminator switch
            {
                "gateway" => json.Deserialize<Payroc.GatewayThreeDSecure?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.GatewayThreeDSecure"),
                "thirdParty" => json.Deserialize<Payroc.ThirdPartyThreeDSecure?>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.ThirdPartyThreeDSecure"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new PaymentRequestThreeDSecure(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PaymentRequestThreeDSecure value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.ServiceProvider switch
                {
                    "gateway" => JsonSerializer.SerializeToNode(value.Value, options),
                    "thirdParty" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["serviceProvider"] = value.ServiceProvider;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for gateway
    /// </summary>
    [Serializable]
    public struct Gateway
    {
        public Gateway(Payroc.GatewayThreeDSecure value)
        {
            Value = value;
        }

        internal Payroc.GatewayThreeDSecure Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator PaymentRequestThreeDSecure.Gateway(
            Payroc.GatewayThreeDSecure value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for thirdParty
    /// </summary>
    [Serializable]
    public struct ThirdParty
    {
        public ThirdParty(Payroc.ThirdPartyThreeDSecure value)
        {
            Value = value;
        }

        internal Payroc.ThirdPartyThreeDSecure Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator PaymentRequestThreeDSecure.ThirdParty(
            Payroc.ThirdPartyThreeDSecure value
        ) => new(value);
    }
}

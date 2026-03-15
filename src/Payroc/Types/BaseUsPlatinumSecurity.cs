// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Polymorphic object that contains billing details for Platinum Security.
///
/// The value of the billingFrequency field determines which variant you should use:
/// -	`monthly` - We collect the fee for Platinum Security each month.
/// -	`annual` - We collect the fee for Platinum Security each year.
/// </summary>
[JsonConverter(typeof(BaseUsPlatinumSecurity.JsonConverter))]
[Serializable]
public record BaseUsPlatinumSecurity
{
    internal BaseUsPlatinumSecurity(string type, object? value)
    {
        BillingFrequency = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of BaseUsPlatinumSecurity with <see cref="BaseUsPlatinumSecurity.Monthly"/>.
    /// </summary>
    public BaseUsPlatinumSecurity(BaseUsPlatinumSecurity.Monthly value)
    {
        BillingFrequency = "monthly";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of BaseUsPlatinumSecurity with <see cref="BaseUsPlatinumSecurity.Annual"/>.
    /// </summary>
    public BaseUsPlatinumSecurity(BaseUsPlatinumSecurity.Annual value)
    {
        BillingFrequency = "annual";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("billingFrequency")]
    public string BillingFrequency { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="BillingFrequency"/> is "monthly"
    /// </summary>
    public bool IsMonthly => BillingFrequency == "monthly";

    /// <summary>
    /// Returns true if <see cref="BillingFrequency"/> is "annual"
    /// </summary>
    public bool IsAnnual => BillingFrequency == "annual";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PlatinumSecurityMonthly"/> if <see cref="BillingFrequency"/> is 'monthly', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="BillingFrequency"/> is not 'monthly'.</exception>
    public Payroc.PlatinumSecurityMonthly AsMonthly() =>
        IsMonthly
            ? (Payroc.PlatinumSecurityMonthly)Value!
            : throw new System.Exception(
                "BaseUsPlatinumSecurity.BillingFrequency is not 'monthly'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PlatinumSecurityAnnual"/> if <see cref="BillingFrequency"/> is 'annual', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="BillingFrequency"/> is not 'annual'.</exception>
    public Payroc.PlatinumSecurityAnnual AsAnnual() =>
        IsAnnual
            ? (Payroc.PlatinumSecurityAnnual)Value!
            : throw new System.Exception("BaseUsPlatinumSecurity.BillingFrequency is not 'annual'");

    public T Match<T>(
        Func<Payroc.PlatinumSecurityMonthly, T> onMonthly,
        Func<Payroc.PlatinumSecurityAnnual, T> onAnnual,
        Func<string, object?, T> onUnknown_
    )
    {
        return BillingFrequency switch
        {
            "monthly" => onMonthly(AsMonthly()),
            "annual" => onAnnual(AsAnnual()),
            _ => onUnknown_(BillingFrequency, Value),
        };
    }

    public void Visit(
        Action<Payroc.PlatinumSecurityMonthly> onMonthly,
        Action<Payroc.PlatinumSecurityAnnual> onAnnual,
        Action<string, object?> onUnknown_
    )
    {
        switch (BillingFrequency)
        {
            case "monthly":
                onMonthly(AsMonthly());
                break;
            case "annual":
                onAnnual(AsAnnual());
                break;
            default:
                onUnknown_(BillingFrequency, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PlatinumSecurityMonthly"/> and returns true if successful.
    /// </summary>
    public bool TryAsMonthly(out Payroc.PlatinumSecurityMonthly? value)
    {
        if (BillingFrequency == "monthly")
        {
            value = (Payroc.PlatinumSecurityMonthly)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PlatinumSecurityAnnual"/> and returns true if successful.
    /// </summary>
    public bool TryAsAnnual(out Payroc.PlatinumSecurityAnnual? value)
    {
        if (BillingFrequency == "annual")
        {
            value = (Payroc.PlatinumSecurityAnnual)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator BaseUsPlatinumSecurity(BaseUsPlatinumSecurity.Monthly value) =>
        new(value);

    public static implicit operator BaseUsPlatinumSecurity(BaseUsPlatinumSecurity.Annual value) =>
        new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<BaseUsPlatinumSecurity>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(BaseUsPlatinumSecurity).IsAssignableFrom(typeToConvert);

        public override BaseUsPlatinumSecurity Read(
            ref Utf8JsonReader reader,
            System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("billingFrequency", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'billingFrequency'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'billingFrequency' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'billingFrequency' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'billingFrequency' is null");

            // Strip the discriminant property to prevent it from leaking into AdditionalProperties
            var jsonObject = System.Text.Json.Nodes.JsonObject.Create(json);
            jsonObject?.Remove("billingFrequency");
            var jsonWithoutDiscriminator =
                jsonObject != null ? JsonSerializer.SerializeToElement(jsonObject, options) : json;

            var value = discriminator switch
            {
                "monthly" => jsonWithoutDiscriminator.Deserialize<Payroc.PlatinumSecurityMonthly?>(
                    options
                )
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.PlatinumSecurityMonthly"
                    ),
                "annual" => jsonWithoutDiscriminator.Deserialize<Payroc.PlatinumSecurityAnnual?>(
                    options
                ) ?? throw new JsonException("Failed to deserialize Payroc.PlatinumSecurityAnnual"),
                _ => json.Deserialize<object?>(options),
            };
            return new BaseUsPlatinumSecurity(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BaseUsPlatinumSecurity value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.BillingFrequency switch
                {
                    "monthly" => JsonSerializer.SerializeToNode(value.Value, options),
                    "annual" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["billingFrequency"] = value.BillingFrequency;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for monthly
    /// </summary>
    [Serializable]
    public struct Monthly
    {
        public Monthly(Payroc.PlatinumSecurityMonthly value)
        {
            Value = value;
        }

        internal Payroc.PlatinumSecurityMonthly Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator BaseUsPlatinumSecurity.Monthly(
            Payroc.PlatinumSecurityMonthly value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for annual
    /// </summary>
    [Serializable]
    public struct Annual
    {
        public Annual(Payroc.PlatinumSecurityAnnual value)
        {
            Value = value;
        }

        internal Payroc.PlatinumSecurityAnnual Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator BaseUsPlatinumSecurity.Annual(
            Payroc.PlatinumSecurityAnnual value
        ) => new(value);
    }
}

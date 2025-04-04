// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the Platinum Security fee.
/// </summary>
[JsonConverter(typeof(BaseUsPlatinumSecurity.JsonConverter))]
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
    /// Returns the value as a <see cref="Payroc.BaseUsMonthly"/> if <see cref="BillingFrequency"/> is 'monthly', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="BillingFrequency"/> is not 'monthly'.</exception>
    public Payroc.BaseUsMonthly AsMonthly() =>
        IsMonthly
            ? (Payroc.BaseUsMonthly)Value!
            : throw new Exception("BaseUsPlatinumSecurity.BillingFrequency is not 'monthly'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.BaseUsAnnual"/> if <see cref="BillingFrequency"/> is 'annual', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="BillingFrequency"/> is not 'annual'.</exception>
    public Payroc.BaseUsAnnual AsAnnual() =>
        IsAnnual
            ? (Payroc.BaseUsAnnual)Value!
            : throw new Exception("BaseUsPlatinumSecurity.BillingFrequency is not 'annual'");

    public T Match<T>(
        Func<Payroc.BaseUsMonthly, T> onMonthly,
        Func<Payroc.BaseUsAnnual, T> onAnnual,
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
        Action<Payroc.BaseUsMonthly> onMonthly,
        Action<Payroc.BaseUsAnnual> onAnnual,
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
    /// Attempts to cast the value to a <see cref="Payroc.BaseUsMonthly"/> and returns true if successful.
    /// </summary>
    public bool TryAsMonthly(out Payroc.BaseUsMonthly? value)
    {
        if (BillingFrequency == "monthly")
        {
            value = (Payroc.BaseUsMonthly)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.BaseUsAnnual"/> and returns true if successful.
    /// </summary>
    public bool TryAsAnnual(out Payroc.BaseUsAnnual? value)
    {
        if (BillingFrequency == "annual")
        {
            value = (Payroc.BaseUsAnnual)Value!;
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

    internal sealed class JsonConverter : JsonConverter<BaseUsPlatinumSecurity>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(BaseUsPlatinumSecurity).IsAssignableFrom(typeToConvert);

        public override BaseUsPlatinumSecurity Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
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

            var value = discriminator switch
            {
                "monthly" => json.Deserialize<Payroc.BaseUsMonthly>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.BaseUsMonthly"),
                "annual" => json.Deserialize<Payroc.BaseUsAnnual>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.BaseUsAnnual"),
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
    public struct Monthly
    {
        public Monthly(Payroc.BaseUsMonthly value)
        {
            Value = value;
        }

        internal Payroc.BaseUsMonthly Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Monthly(Payroc.BaseUsMonthly value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for annual
    /// </summary>
    public struct Annual
    {
        public Annual(Payroc.BaseUsAnnual value)
        {
            Value = value;
        }

        internal Payroc.BaseUsAnnual Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Annual(Payroc.BaseUsAnnual value) => new(value);
    }
}

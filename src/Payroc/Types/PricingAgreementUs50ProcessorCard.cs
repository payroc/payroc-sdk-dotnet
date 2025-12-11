// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about card fees.
/// </summary>
[JsonConverter(typeof(PricingAgreementUs50ProcessorCard.JsonConverter))]
[Serializable]
public record PricingAgreementUs50ProcessorCard
{
    internal PricingAgreementUs50ProcessorCard(string type, object? value)
    {
        PlanType = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of PricingAgreementUs50ProcessorCard with <see cref="PricingAgreementUs50ProcessorCard.InterchangePlus"/>.
    /// </summary>
    public PricingAgreementUs50ProcessorCard(
        PricingAgreementUs50ProcessorCard.InterchangePlus value
    )
    {
        PlanType = "interchangePlus";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PricingAgreementUs50ProcessorCard with <see cref="PricingAgreementUs50ProcessorCard.InterchangePlusPlus"/>.
    /// </summary>
    public PricingAgreementUs50ProcessorCard(
        PricingAgreementUs50ProcessorCard.InterchangePlusPlus value
    )
    {
        PlanType = "interchangePlusPlus";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PricingAgreementUs50ProcessorCard with <see cref="PricingAgreementUs50ProcessorCard.Tiered3"/>.
    /// </summary>
    public PricingAgreementUs50ProcessorCard(PricingAgreementUs50ProcessorCard.Tiered3 value)
    {
        PlanType = "tiered3";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PricingAgreementUs50ProcessorCard with <see cref="PricingAgreementUs50ProcessorCard.Tiered4"/>.
    /// </summary>
    public PricingAgreementUs50ProcessorCard(PricingAgreementUs50ProcessorCard.Tiered4 value)
    {
        PlanType = "tiered4";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PricingAgreementUs50ProcessorCard with <see cref="PricingAgreementUs50ProcessorCard.Tiered6"/>.
    /// </summary>
    public PricingAgreementUs50ProcessorCard(PricingAgreementUs50ProcessorCard.Tiered6 value)
    {
        PlanType = "tiered6";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PricingAgreementUs50ProcessorCard with <see cref="PricingAgreementUs50ProcessorCard.FlatRate"/>.
    /// </summary>
    public PricingAgreementUs50ProcessorCard(PricingAgreementUs50ProcessorCard.FlatRate value)
    {
        PlanType = "flatRate";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PricingAgreementUs50ProcessorCard with <see cref="PricingAgreementUs50ProcessorCard.ConsumerChoice"/>.
    /// </summary>
    public PricingAgreementUs50ProcessorCard(PricingAgreementUs50ProcessorCard.ConsumerChoice value)
    {
        PlanType = "consumerChoice";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PricingAgreementUs50ProcessorCard with <see cref="PricingAgreementUs50ProcessorCard.RewardPayChoice"/>.
    /// </summary>
    public PricingAgreementUs50ProcessorCard(
        PricingAgreementUs50ProcessorCard.RewardPayChoice value
    )
    {
        PlanType = "rewardPayChoice";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("planType")]
    public string PlanType { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="PlanType"/> is "interchangePlus"
    /// </summary>
    public bool IsInterchangePlus => PlanType == "interchangePlus";

    /// <summary>
    /// Returns true if <see cref="PlanType"/> is "interchangePlusPlus"
    /// </summary>
    public bool IsInterchangePlusPlus => PlanType == "interchangePlusPlus";

    /// <summary>
    /// Returns true if <see cref="PlanType"/> is "tiered3"
    /// </summary>
    public bool IsTiered3 => PlanType == "tiered3";

    /// <summary>
    /// Returns true if <see cref="PlanType"/> is "tiered4"
    /// </summary>
    public bool IsTiered4 => PlanType == "tiered4";

    /// <summary>
    /// Returns true if <see cref="PlanType"/> is "tiered6"
    /// </summary>
    public bool IsTiered6 => PlanType == "tiered6";

    /// <summary>
    /// Returns true if <see cref="PlanType"/> is "flatRate"
    /// </summary>
    public bool IsFlatRate => PlanType == "flatRate";

    /// <summary>
    /// Returns true if <see cref="PlanType"/> is "consumerChoice"
    /// </summary>
    public bool IsConsumerChoice => PlanType == "consumerChoice";

    /// <summary>
    /// Returns true if <see cref="PlanType"/> is "rewardPayChoice"
    /// </summary>
    public bool IsRewardPayChoice => PlanType == "rewardPayChoice";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.InterchangePlus"/> if <see cref="PlanType"/> is 'interchangePlus', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="PlanType"/> is not 'interchangePlus'.</exception>
    public Payroc.InterchangePlus AsInterchangePlus() =>
        IsInterchangePlus
            ? (Payroc.InterchangePlus)Value!
            : throw new System.Exception(
                "PricingAgreementUs50ProcessorCard.PlanType is not 'interchangePlus'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.InterchangePlusPlus"/> if <see cref="PlanType"/> is 'interchangePlusPlus', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="PlanType"/> is not 'interchangePlusPlus'.</exception>
    public Payroc.InterchangePlusPlus AsInterchangePlusPlus() =>
        IsInterchangePlusPlus
            ? (Payroc.InterchangePlusPlus)Value!
            : throw new System.Exception(
                "PricingAgreementUs50ProcessorCard.PlanType is not 'interchangePlusPlus'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.Tiered3"/> if <see cref="PlanType"/> is 'tiered3', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="PlanType"/> is not 'tiered3'.</exception>
    public Payroc.Tiered3 AsTiered3() =>
        IsTiered3
            ? (Payroc.Tiered3)Value!
            : throw new System.Exception(
                "PricingAgreementUs50ProcessorCard.PlanType is not 'tiered3'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.Tiered4"/> if <see cref="PlanType"/> is 'tiered4', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="PlanType"/> is not 'tiered4'.</exception>
    public Payroc.Tiered4 AsTiered4() =>
        IsTiered4
            ? (Payroc.Tiered4)Value!
            : throw new System.Exception(
                "PricingAgreementUs50ProcessorCard.PlanType is not 'tiered4'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.Tiered6"/> if <see cref="PlanType"/> is 'tiered6', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="PlanType"/> is not 'tiered6'.</exception>
    public Payroc.Tiered6 AsTiered6() =>
        IsTiered6
            ? (Payroc.Tiered6)Value!
            : throw new System.Exception(
                "PricingAgreementUs50ProcessorCard.PlanType is not 'tiered6'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.FlatRate"/> if <see cref="PlanType"/> is 'flatRate', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="PlanType"/> is not 'flatRate'.</exception>
    public Payroc.FlatRate AsFlatRate() =>
        IsFlatRate
            ? (Payroc.FlatRate)Value!
            : throw new System.Exception(
                "PricingAgreementUs50ProcessorCard.PlanType is not 'flatRate'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.ConsumerChoice"/> if <see cref="PlanType"/> is 'consumerChoice', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="PlanType"/> is not 'consumerChoice'.</exception>
    public Payroc.ConsumerChoice AsConsumerChoice() =>
        IsConsumerChoice
            ? (Payroc.ConsumerChoice)Value!
            : throw new System.Exception(
                "PricingAgreementUs50ProcessorCard.PlanType is not 'consumerChoice'"
            );

    /// <summary>
    /// Returns the value as a <see cref="Payroc.RewardPayChoice"/> if <see cref="PlanType"/> is 'rewardPayChoice', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="PlanType"/> is not 'rewardPayChoice'.</exception>
    public Payroc.RewardPayChoice AsRewardPayChoice() =>
        IsRewardPayChoice
            ? (Payroc.RewardPayChoice)Value!
            : throw new System.Exception(
                "PricingAgreementUs50ProcessorCard.PlanType is not 'rewardPayChoice'"
            );

    public T Match<T>(
        Func<Payroc.InterchangePlus, T> onInterchangePlus,
        Func<Payroc.InterchangePlusPlus, T> onInterchangePlusPlus,
        Func<Payroc.Tiered3, T> onTiered3,
        Func<Payroc.Tiered4, T> onTiered4,
        Func<Payroc.Tiered6, T> onTiered6,
        Func<Payroc.FlatRate, T> onFlatRate,
        Func<Payroc.ConsumerChoice, T> onConsumerChoice,
        Func<Payroc.RewardPayChoice, T> onRewardPayChoice,
        Func<string, object?, T> onUnknown_
    )
    {
        return PlanType switch
        {
            "interchangePlus" => onInterchangePlus(AsInterchangePlus()),
            "interchangePlusPlus" => onInterchangePlusPlus(AsInterchangePlusPlus()),
            "tiered3" => onTiered3(AsTiered3()),
            "tiered4" => onTiered4(AsTiered4()),
            "tiered6" => onTiered6(AsTiered6()),
            "flatRate" => onFlatRate(AsFlatRate()),
            "consumerChoice" => onConsumerChoice(AsConsumerChoice()),
            "rewardPayChoice" => onRewardPayChoice(AsRewardPayChoice()),
            _ => onUnknown_(PlanType, Value),
        };
    }

    public void Visit(
        Action<Payroc.InterchangePlus> onInterchangePlus,
        Action<Payroc.InterchangePlusPlus> onInterchangePlusPlus,
        Action<Payroc.Tiered3> onTiered3,
        Action<Payroc.Tiered4> onTiered4,
        Action<Payroc.Tiered6> onTiered6,
        Action<Payroc.FlatRate> onFlatRate,
        Action<Payroc.ConsumerChoice> onConsumerChoice,
        Action<Payroc.RewardPayChoice> onRewardPayChoice,
        Action<string, object?> onUnknown_
    )
    {
        switch (PlanType)
        {
            case "interchangePlus":
                onInterchangePlus(AsInterchangePlus());
                break;
            case "interchangePlusPlus":
                onInterchangePlusPlus(AsInterchangePlusPlus());
                break;
            case "tiered3":
                onTiered3(AsTiered3());
                break;
            case "tiered4":
                onTiered4(AsTiered4());
                break;
            case "tiered6":
                onTiered6(AsTiered6());
                break;
            case "flatRate":
                onFlatRate(AsFlatRate());
                break;
            case "consumerChoice":
                onConsumerChoice(AsConsumerChoice());
                break;
            case "rewardPayChoice":
                onRewardPayChoice(AsRewardPayChoice());
                break;
            default:
                onUnknown_(PlanType, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.InterchangePlus"/> and returns true if successful.
    /// </summary>
    public bool TryAsInterchangePlus(out Payroc.InterchangePlus? value)
    {
        if (PlanType == "interchangePlus")
        {
            value = (Payroc.InterchangePlus)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.InterchangePlusPlus"/> and returns true if successful.
    /// </summary>
    public bool TryAsInterchangePlusPlus(out Payroc.InterchangePlusPlus? value)
    {
        if (PlanType == "interchangePlusPlus")
        {
            value = (Payroc.InterchangePlusPlus)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.Tiered3"/> and returns true if successful.
    /// </summary>
    public bool TryAsTiered3(out Payroc.Tiered3? value)
    {
        if (PlanType == "tiered3")
        {
            value = (Payroc.Tiered3)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.Tiered4"/> and returns true if successful.
    /// </summary>
    public bool TryAsTiered4(out Payroc.Tiered4? value)
    {
        if (PlanType == "tiered4")
        {
            value = (Payroc.Tiered4)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.Tiered6"/> and returns true if successful.
    /// </summary>
    public bool TryAsTiered6(out Payroc.Tiered6? value)
    {
        if (PlanType == "tiered6")
        {
            value = (Payroc.Tiered6)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.FlatRate"/> and returns true if successful.
    /// </summary>
    public bool TryAsFlatRate(out Payroc.FlatRate? value)
    {
        if (PlanType == "flatRate")
        {
            value = (Payroc.FlatRate)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.ConsumerChoice"/> and returns true if successful.
    /// </summary>
    public bool TryAsConsumerChoice(out Payroc.ConsumerChoice? value)
    {
        if (PlanType == "consumerChoice")
        {
            value = (Payroc.ConsumerChoice)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.RewardPayChoice"/> and returns true if successful.
    /// </summary>
    public bool TryAsRewardPayChoice(out Payroc.RewardPayChoice? value)
    {
        if (PlanType == "rewardPayChoice")
        {
            value = (Payroc.RewardPayChoice)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator PricingAgreementUs50ProcessorCard(
        PricingAgreementUs50ProcessorCard.InterchangePlus value
    ) => new(value);

    public static implicit operator PricingAgreementUs50ProcessorCard(
        PricingAgreementUs50ProcessorCard.InterchangePlusPlus value
    ) => new(value);

    public static implicit operator PricingAgreementUs50ProcessorCard(
        PricingAgreementUs50ProcessorCard.Tiered3 value
    ) => new(value);

    public static implicit operator PricingAgreementUs50ProcessorCard(
        PricingAgreementUs50ProcessorCard.Tiered4 value
    ) => new(value);

    public static implicit operator PricingAgreementUs50ProcessorCard(
        PricingAgreementUs50ProcessorCard.Tiered6 value
    ) => new(value);

    public static implicit operator PricingAgreementUs50ProcessorCard(
        PricingAgreementUs50ProcessorCard.FlatRate value
    ) => new(value);

    public static implicit operator PricingAgreementUs50ProcessorCard(
        PricingAgreementUs50ProcessorCard.ConsumerChoice value
    ) => new(value);

    public static implicit operator PricingAgreementUs50ProcessorCard(
        PricingAgreementUs50ProcessorCard.RewardPayChoice value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<PricingAgreementUs50ProcessorCard>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(PricingAgreementUs50ProcessorCard).IsAssignableFrom(typeToConvert);

        public override PricingAgreementUs50ProcessorCard Read(
            ref Utf8JsonReader reader,
            System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("planType", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'planType'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'planType' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'planType' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'planType' is null");

            var value = discriminator switch
            {
                "interchangePlus" => json.Deserialize<Payroc.InterchangePlus?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.InterchangePlus"),
                "interchangePlusPlus" => json.Deserialize<Payroc.InterchangePlusPlus?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.InterchangePlusPlus"),
                "tiered3" => json.Deserialize<Payroc.Tiered3?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.Tiered3"),
                "tiered4" => json.Deserialize<Payroc.Tiered4?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.Tiered4"),
                "tiered6" => json.Deserialize<Payroc.Tiered6?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.Tiered6"),
                "flatRate" => json.Deserialize<Payroc.FlatRate?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.FlatRate"),
                "consumerChoice" => json.Deserialize<Payroc.ConsumerChoice?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.ConsumerChoice"),
                "rewardPayChoice" => json.Deserialize<Payroc.RewardPayChoice?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.RewardPayChoice"),
                _ => json.Deserialize<object?>(options),
            };
            return new PricingAgreementUs50ProcessorCard(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PricingAgreementUs50ProcessorCard value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.PlanType switch
                {
                    "interchangePlus" => JsonSerializer.SerializeToNode(value.Value, options),
                    "interchangePlusPlus" => JsonSerializer.SerializeToNode(value.Value, options),
                    "tiered3" => JsonSerializer.SerializeToNode(value.Value, options),
                    "tiered4" => JsonSerializer.SerializeToNode(value.Value, options),
                    "tiered6" => JsonSerializer.SerializeToNode(value.Value, options),
                    "flatRate" => JsonSerializer.SerializeToNode(value.Value, options),
                    "consumerChoice" => JsonSerializer.SerializeToNode(value.Value, options),
                    "rewardPayChoice" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["planType"] = value.PlanType;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for interchangePlus
    /// </summary>
    [Serializable]
    public struct InterchangePlus
    {
        public InterchangePlus(Payroc.InterchangePlus value)
        {
            Value = value;
        }

        internal Payroc.InterchangePlus Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator PricingAgreementUs50ProcessorCard.InterchangePlus(
            Payroc.InterchangePlus value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for interchangePlusPlus
    /// </summary>
    [Serializable]
    public struct InterchangePlusPlus
    {
        public InterchangePlusPlus(Payroc.InterchangePlusPlus value)
        {
            Value = value;
        }

        internal Payroc.InterchangePlusPlus Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator PricingAgreementUs50ProcessorCard.InterchangePlusPlus(
            Payroc.InterchangePlusPlus value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for tiered3
    /// </summary>
    [Serializable]
    public struct Tiered3
    {
        public Tiered3(Payroc.Tiered3 value)
        {
            Value = value;
        }

        internal Payroc.Tiered3 Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator PricingAgreementUs50ProcessorCard.Tiered3(
            Payroc.Tiered3 value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for tiered4
    /// </summary>
    [Serializable]
    public struct Tiered4
    {
        public Tiered4(Payroc.Tiered4 value)
        {
            Value = value;
        }

        internal Payroc.Tiered4 Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator PricingAgreementUs50ProcessorCard.Tiered4(
            Payroc.Tiered4 value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for tiered6
    /// </summary>
    [Serializable]
    public struct Tiered6
    {
        public Tiered6(Payroc.Tiered6 value)
        {
            Value = value;
        }

        internal Payroc.Tiered6 Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator PricingAgreementUs50ProcessorCard.Tiered6(
            Payroc.Tiered6 value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for flatRate
    /// </summary>
    [Serializable]
    public struct FlatRate
    {
        public FlatRate(Payroc.FlatRate value)
        {
            Value = value;
        }

        internal Payroc.FlatRate Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator PricingAgreementUs50ProcessorCard.FlatRate(
            Payroc.FlatRate value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for consumerChoice
    /// </summary>
    [Serializable]
    public struct ConsumerChoice
    {
        public ConsumerChoice(Payroc.ConsumerChoice value)
        {
            Value = value;
        }

        internal Payroc.ConsumerChoice Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator PricingAgreementUs50ProcessorCard.ConsumerChoice(
            Payroc.ConsumerChoice value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for rewardPayChoice
    /// </summary>
    [Serializable]
    public struct RewardPayChoice
    {
        public RewardPayChoice(Payroc.RewardPayChoice value)
        {
            Value = value;
        }

        internal Payroc.RewardPayChoice Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator PricingAgreementUs50ProcessorCard.RewardPayChoice(
            Payroc.RewardPayChoice value
        ) => new(value);
    }
}

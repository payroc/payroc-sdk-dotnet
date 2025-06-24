// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

[JsonConverter(typeof(CreateReminderProcessingAccountsResponse.JsonConverter))]
[Serializable]
public record CreateReminderProcessingAccountsResponse
{
    internal CreateReminderProcessingAccountsResponse(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of CreateReminderProcessingAccountsResponse with <see cref="CreateReminderProcessingAccountsResponse.PricingAgreement"/>.
    /// </summary>
    public CreateReminderProcessingAccountsResponse(
        CreateReminderProcessingAccountsResponse.PricingAgreement value
    )
    {
        Type = "pricingAgreement";
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
    /// Returns true if <see cref="Type"/> is "pricingAgreement"
    /// </summary>
    public bool IsPricingAgreement => Type == "pricingAgreement";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PricingAgreementReminder"/> if <see cref="Type"/> is 'pricingAgreement', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'pricingAgreement'.</exception>
    public Payroc.PricingAgreementReminder AsPricingAgreement() =>
        IsPricingAgreement
            ? (Payroc.PricingAgreementReminder)Value!
            : throw new Exception(
                "CreateReminderProcessingAccountsResponse.Type is not 'pricingAgreement'"
            );

    public T Match<T>(
        Func<Payroc.PricingAgreementReminder, T> onPricingAgreement,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "pricingAgreement" => onPricingAgreement(AsPricingAgreement()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.PricingAgreementReminder> onPricingAgreement,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "pricingAgreement":
                onPricingAgreement(AsPricingAgreement());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PricingAgreementReminder"/> and returns true if successful.
    /// </summary>
    public bool TryAsPricingAgreement(out Payroc.PricingAgreementReminder? value)
    {
        if (Type == "pricingAgreement")
        {
            value = (Payroc.PricingAgreementReminder)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator CreateReminderProcessingAccountsResponse(
        CreateReminderProcessingAccountsResponse.PricingAgreement value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<CreateReminderProcessingAccountsResponse>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(CreateReminderProcessingAccountsResponse).IsAssignableFrom(typeToConvert);

        public override CreateReminderProcessingAccountsResponse Read(
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
                "pricingAgreement" => json.Deserialize<Payroc.PricingAgreementReminder>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.PricingAgreementReminder"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new CreateReminderProcessingAccountsResponse(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreateReminderProcessingAccountsResponse value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "pricingAgreement" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for pricingAgreement
    /// </summary>
    [Serializable]
    public struct PricingAgreement
    {
        public PricingAgreement(Payroc.PricingAgreementReminder value)
        {
            Value = value;
        }

        internal Payroc.PricingAgreementReminder Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator PricingAgreement(Payroc.PricingAgreementReminder value) =>
            new(value);
    }
}

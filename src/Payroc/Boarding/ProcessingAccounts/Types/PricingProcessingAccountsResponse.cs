// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

[JsonConverter(typeof(PricingProcessingAccountsResponse.JsonConverter))]
[Serializable]
public record PricingProcessingAccountsResponse
{
    internal PricingProcessingAccountsResponse(string type, object? value)
    {
        Version = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of PricingProcessingAccountsResponse with <see cref="PricingProcessingAccountsResponse._40"/>.
    /// </summary>
    public PricingProcessingAccountsResponse(PricingProcessingAccountsResponse._40 value)
    {
        Version = "4.0";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PricingProcessingAccountsResponse with <see cref="PricingProcessingAccountsResponse._50"/>.
    /// </summary>
    public PricingProcessingAccountsResponse(PricingProcessingAccountsResponse._50 value)
    {
        Version = "5.0";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="Version"/> is "4.0"
    /// </summary>
    public bool Is40 => Version == "4.0";

    /// <summary>
    /// Returns true if <see cref="Version"/> is "5.0"
    /// </summary>
    public bool Is50 => Version == "5.0";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PricingAgreementUs40"/> if <see cref="Version"/> is '4.0', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Version"/> is not '4.0'.</exception>
    public Payroc.PricingAgreementUs40 As40() =>
        Is40
            ? (Payroc.PricingAgreementUs40)Value!
            : throw new Exception("PricingProcessingAccountsResponse.Version is not '4.0'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PricingAgreementUs50"/> if <see cref="Version"/> is '5.0', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Version"/> is not '5.0'.</exception>
    public Payroc.PricingAgreementUs50 As50() =>
        Is50
            ? (Payroc.PricingAgreementUs50)Value!
            : throw new Exception("PricingProcessingAccountsResponse.Version is not '5.0'");

    public T Match<T>(
        Func<Payroc.PricingAgreementUs40, T> on40,
        Func<Payroc.PricingAgreementUs50, T> on50,
        Func<string, object?, T> onUnknown_
    )
    {
        return Version switch
        {
            "4.0" => on40(As40()),
            "5.0" => on50(As50()),
            _ => onUnknown_(Version, Value),
        };
    }

    public void Visit(
        Action<Payroc.PricingAgreementUs40> on40,
        Action<Payroc.PricingAgreementUs50> on50,
        Action<string, object?> onUnknown_
    )
    {
        switch (Version)
        {
            case "4.0":
                on40(As40());
                break;
            case "5.0":
                on50(As50());
                break;
            default:
                onUnknown_(Version, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PricingAgreementUs40"/> and returns true if successful.
    /// </summary>
    public bool TryAs40(out Payroc.PricingAgreementUs40? value)
    {
        if (Version == "4.0")
        {
            value = (Payroc.PricingAgreementUs40)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PricingAgreementUs50"/> and returns true if successful.
    /// </summary>
    public bool TryAs50(out Payroc.PricingAgreementUs50? value)
    {
        if (Version == "5.0")
        {
            value = (Payroc.PricingAgreementUs50)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator PricingProcessingAccountsResponse(
        PricingProcessingAccountsResponse._40 value
    ) => new(value);

    public static implicit operator PricingProcessingAccountsResponse(
        PricingProcessingAccountsResponse._50 value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<PricingProcessingAccountsResponse>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(PricingProcessingAccountsResponse).IsAssignableFrom(typeToConvert);

        public override PricingProcessingAccountsResponse Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("version", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'version'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'version' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'version' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'version' is null");

            var value = discriminator switch
            {
                "4.0" => json.Deserialize<Payroc.PricingAgreementUs40>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.PricingAgreementUs40"),
                "5.0" => json.Deserialize<Payroc.PricingAgreementUs50>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.PricingAgreementUs50"),
                _ => json.Deserialize<object?>(options),
            };
            return new PricingProcessingAccountsResponse(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PricingProcessingAccountsResponse value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Version switch
                {
                    "4.0" => JsonSerializer.SerializeToNode(value.Value, options),
                    "5.0" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["version"] = value.Version;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for 4.0
    /// </summary>
    [Serializable]
    public struct _40
    {
        public _40(Payroc.PricingAgreementUs40 value)
        {
            Value = value;
        }

        internal Payroc.PricingAgreementUs40 Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator _40(Payroc.PricingAgreementUs40 value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for 5.0
    /// </summary>
    [Serializable]
    public struct _50
    {
        public _50(Payroc.PricingAgreementUs50 value)
        {
            Value = value;
        }

        internal Payroc.PricingAgreementUs50 Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator _50(Payroc.PricingAgreementUs50 value) => new(value);
    }
}

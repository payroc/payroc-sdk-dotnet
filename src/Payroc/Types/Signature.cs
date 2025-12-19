// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that includes information about how we captured the owner's signature.
/// </summary>
[JsonConverter(typeof(Signature.JsonConverter))]
[Serializable]
public record Signature
{
    internal Signature(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of Signature with <see cref="Signature.RequestedViaDirectLink"/>.
    /// </summary>
    public Signature(Signature.RequestedViaDirectLink value)
    {
        Type = "requestedViaDirectLink";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of Signature with <see cref="Signature.RequestedViaEmail"/>.
    /// </summary>
    public Signature(Signature.RequestedViaEmail value)
    {
        Type = "requestedViaEmail";
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
    /// Returns true if <see cref="Type"/> is "requestedViaDirectLink"
    /// </summary>
    public bool IsRequestedViaDirectLink => Type == "requestedViaDirectLink";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "requestedViaEmail"
    /// </summary>
    public bool IsRequestedViaEmail => Type == "requestedViaEmail";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SignatureByDirectLink"/> if <see cref="Type"/> is 'requestedViaDirectLink', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'requestedViaDirectLink'.</exception>
    public Payroc.SignatureByDirectLink AsRequestedViaDirectLink() =>
        IsRequestedViaDirectLink
            ? (Payroc.SignatureByDirectLink)Value!
            : throw new System.Exception("Signature.Type is not 'requestedViaDirectLink'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SignatureByEmail"/> if <see cref="Type"/> is 'requestedViaEmail', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'requestedViaEmail'.</exception>
    public Payroc.SignatureByEmail AsRequestedViaEmail() =>
        IsRequestedViaEmail
            ? (Payroc.SignatureByEmail)Value!
            : throw new System.Exception("Signature.Type is not 'requestedViaEmail'");

    public T Match<T>(
        Func<Payroc.SignatureByDirectLink, T> onRequestedViaDirectLink,
        Func<Payroc.SignatureByEmail, T> onRequestedViaEmail,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "requestedViaDirectLink" => onRequestedViaDirectLink(AsRequestedViaDirectLink()),
            "requestedViaEmail" => onRequestedViaEmail(AsRequestedViaEmail()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.SignatureByDirectLink> onRequestedViaDirectLink,
        Action<Payroc.SignatureByEmail> onRequestedViaEmail,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "requestedViaDirectLink":
                onRequestedViaDirectLink(AsRequestedViaDirectLink());
                break;
            case "requestedViaEmail":
                onRequestedViaEmail(AsRequestedViaEmail());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.SignatureByDirectLink"/> and returns true if successful.
    /// </summary>
    public bool TryAsRequestedViaDirectLink(out Payroc.SignatureByDirectLink? value)
    {
        if (Type == "requestedViaDirectLink")
        {
            value = (Payroc.SignatureByDirectLink)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.SignatureByEmail"/> and returns true if successful.
    /// </summary>
    public bool TryAsRequestedViaEmail(out Payroc.SignatureByEmail? value)
    {
        if (Type == "requestedViaEmail")
        {
            value = (Payroc.SignatureByEmail)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator Signature(Signature.RequestedViaDirectLink value) => new(value);

    public static implicit operator Signature(Signature.RequestedViaEmail value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<Signature>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(Signature).IsAssignableFrom(typeToConvert);

        public override Signature Read(
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
                "requestedViaDirectLink" => json.Deserialize<Payroc.SignatureByDirectLink?>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.SignatureByDirectLink"
                    ),
                "requestedViaEmail" => json.Deserialize<Payroc.SignatureByEmail?>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.SignatureByEmail"),
                _ => json.Deserialize<object?>(options),
            };
            return new Signature(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Signature value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "requestedViaDirectLink" => JsonSerializer.SerializeToNode(
                        value.Value,
                        options
                    ),
                    "requestedViaEmail" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for requestedViaDirectLink
    /// </summary>
    [Serializable]
    public struct RequestedViaDirectLink
    {
        public RequestedViaDirectLink(Payroc.SignatureByDirectLink value)
        {
            Value = value;
        }

        internal Payroc.SignatureByDirectLink Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator Signature.RequestedViaDirectLink(
            Payroc.SignatureByDirectLink value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for requestedViaEmail
    /// </summary>
    [Serializable]
    public struct RequestedViaEmail
    {
        public RequestedViaEmail(Payroc.SignatureByEmail value)
        {
            Value = value;
        }

        internal Payroc.SignatureByEmail Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator Signature.RequestedViaEmail(
            Payroc.SignatureByEmail value
        ) => new(value);
    }
}

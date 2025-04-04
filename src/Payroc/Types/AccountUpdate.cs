// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the token.
/// </summary>
[JsonConverter(typeof(AccountUpdate.JsonConverter))]
public record AccountUpdate
{
    internal AccountUpdate(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of AccountUpdate with <see cref="AccountUpdate.SingleUseToken"/>.
    /// </summary>
    public AccountUpdate(AccountUpdate.SingleUseToken value)
    {
        Type = "singleUseToken";
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
    /// Returns true if <see cref="Type"/> is "singleUseToken"
    /// </summary>
    public bool IsSingleUseToken => Type == "singleUseToken";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.SingleUseTokenAccountUpdate"/> if <see cref="Type"/> is 'singleUseToken', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'singleUseToken'.</exception>
    public Payroc.SingleUseTokenAccountUpdate AsSingleUseToken() =>
        IsSingleUseToken
            ? (Payroc.SingleUseTokenAccountUpdate)Value!
            : throw new Exception("AccountUpdate.Type is not 'singleUseToken'");

    public T Match<T>(
        Func<Payroc.SingleUseTokenAccountUpdate, T> onSingleUseToken,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "singleUseToken" => onSingleUseToken(AsSingleUseToken()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.SingleUseTokenAccountUpdate> onSingleUseToken,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "singleUseToken":
                onSingleUseToken(AsSingleUseToken());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.SingleUseTokenAccountUpdate"/> and returns true if successful.
    /// </summary>
    public bool TryAsSingleUseToken(out Payroc.SingleUseTokenAccountUpdate? value)
    {
        if (Type == "singleUseToken")
        {
            value = (Payroc.SingleUseTokenAccountUpdate)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator AccountUpdate(AccountUpdate.SingleUseToken value) => new(value);

    internal sealed class JsonConverter : JsonConverter<AccountUpdate>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(AccountUpdate).IsAssignableFrom(typeToConvert);

        public override AccountUpdate Read(
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
                "singleUseToken" => json.Deserialize<Payroc.SingleUseTokenAccountUpdate>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.SingleUseTokenAccountUpdate"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new AccountUpdate(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            AccountUpdate value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "singleUseToken" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for singleUseToken
    /// </summary>
    public struct SingleUseToken
    {
        public SingleUseToken(Payroc.SingleUseTokenAccountUpdate value)
        {
            Value = value;
        }

        internal Payroc.SingleUseTokenAccountUpdate Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator SingleUseToken(Payroc.SingleUseTokenAccountUpdate value) =>
            new(value);
    }
}

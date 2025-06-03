// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(Notification.JsonConverter))]
public record Notification
{
    internal Notification(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of Notification with <see cref="Notification.Webhook"/>.
    /// </summary>
    public Notification(Notification.Webhook value)
    {
        Type = "webhook";
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
    /// Returns true if <see cref="Type"/> is "webhook"
    /// </summary>
    public bool IsWebhook => Type == "webhook";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.Webhook"/> if <see cref="Type"/> is 'webhook', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'webhook'.</exception>
    public Payroc.Webhook AsWebhook() =>
        IsWebhook
            ? (Payroc.Webhook)Value!
            : throw new Exception("Notification.Type is not 'webhook'");

    public T Match<T>(Func<Payroc.Webhook, T> onWebhook, Func<string, object?, T> onUnknown_)
    {
        return Type switch
        {
            "webhook" => onWebhook(AsWebhook()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(Action<Payroc.Webhook> onWebhook, Action<string, object?> onUnknown_)
    {
        switch (Type)
        {
            case "webhook":
                onWebhook(AsWebhook());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.Webhook"/> and returns true if successful.
    /// </summary>
    public bool TryAsWebhook(out Payroc.Webhook? value)
    {
        if (Type == "webhook")
        {
            value = (Payroc.Webhook)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator Notification(Notification.Webhook value) => new(value);

    internal sealed class JsonConverter : JsonConverter<Notification>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(Notification).IsAssignableFrom(typeToConvert);

        public override Notification Read(
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
                "webhook" => json.Deserialize<Payroc.Webhook>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.Webhook"),
                _ => json.Deserialize<object?>(options),
            };
            return new Notification(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Notification value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "webhook" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for webhook
    /// </summary>
    public struct Webhook
    {
        public Webhook(Payroc.Webhook value)
        {
            Value = value;
        }

        internal Payroc.Webhook Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Webhook(Payroc.Webhook value) => new(value);
    }
}

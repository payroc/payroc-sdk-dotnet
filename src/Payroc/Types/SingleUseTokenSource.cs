// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the payment method that we tokenized.
/// </summary>
[JsonConverter(typeof(SingleUseTokenSource.JsonConverter))]
public record SingleUseTokenSource
{
    internal SingleUseTokenSource(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of SingleUseTokenSource with <see cref="SingleUseTokenSource.Ach"/>.
    /// </summary>
    public SingleUseTokenSource(SingleUseTokenSource.Ach value)
    {
        Type = "ach";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of SingleUseTokenSource with <see cref="SingleUseTokenSource.Pad"/>.
    /// </summary>
    public SingleUseTokenSource(SingleUseTokenSource.Pad value)
    {
        Type = "pad";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of SingleUseTokenSource with <see cref="SingleUseTokenSource.Card"/>.
    /// </summary>
    public SingleUseTokenSource(SingleUseTokenSource.Card value)
    {
        Type = "card";
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
    /// Returns true if <see cref="Type"/> is "ach"
    /// </summary>
    public bool IsAch => Type == "ach";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "pad"
    /// </summary>
    public bool IsPad => Type == "pad";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "card"
    /// </summary>
    public bool IsCard => Type == "card";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.AchSource"/> if <see cref="Type"/> is 'ach', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'ach'.</exception>
    public Payroc.AchSource AsAch() =>
        IsAch
            ? (Payroc.AchSource)Value!
            : throw new Exception("SingleUseTokenSource.Type is not 'ach'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PadSource"/> if <see cref="Type"/> is 'pad', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'pad'.</exception>
    public Payroc.PadSource AsPad() =>
        IsPad
            ? (Payroc.PadSource)Value!
            : throw new Exception("SingleUseTokenSource.Type is not 'pad'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.CardSource"/> if <see cref="Type"/> is 'card', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'card'.</exception>
    public Payroc.CardSource AsCard() =>
        IsCard
            ? (Payroc.CardSource)Value!
            : throw new Exception("SingleUseTokenSource.Type is not 'card'");

    public T Match<T>(
        Func<Payroc.AchSource, T> onAch,
        Func<Payroc.PadSource, T> onPad,
        Func<Payroc.CardSource, T> onCard,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "ach" => onAch(AsAch()),
            "pad" => onPad(AsPad()),
            "card" => onCard(AsCard()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.AchSource> onAch,
        Action<Payroc.PadSource> onPad,
        Action<Payroc.CardSource> onCard,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "ach":
                onAch(AsAch());
                break;
            case "pad":
                onPad(AsPad());
                break;
            case "card":
                onCard(AsCard());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.AchSource"/> and returns true if successful.
    /// </summary>
    public bool TryAsAch(out Payroc.AchSource? value)
    {
        if (Type == "ach")
        {
            value = (Payroc.AchSource)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PadSource"/> and returns true if successful.
    /// </summary>
    public bool TryAsPad(out Payroc.PadSource? value)
    {
        if (Type == "pad")
        {
            value = (Payroc.PadSource)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.CardSource"/> and returns true if successful.
    /// </summary>
    public bool TryAsCard(out Payroc.CardSource? value)
    {
        if (Type == "card")
        {
            value = (Payroc.CardSource)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator SingleUseTokenSource(SingleUseTokenSource.Ach value) =>
        new(value);

    public static implicit operator SingleUseTokenSource(SingleUseTokenSource.Pad value) =>
        new(value);

    public static implicit operator SingleUseTokenSource(SingleUseTokenSource.Card value) =>
        new(value);

    internal sealed class JsonConverter : JsonConverter<SingleUseTokenSource>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(SingleUseTokenSource).IsAssignableFrom(typeToConvert);

        public override SingleUseTokenSource Read(
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
                "ach" => json.Deserialize<Payroc.AchSource>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.AchSource"),
                "pad" => json.Deserialize<Payroc.PadSource>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.PadSource"),
                "card" => json.Deserialize<Payroc.CardSource>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.CardSource"),
                _ => json.Deserialize<object?>(options),
            };
            return new SingleUseTokenSource(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SingleUseTokenSource value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "ach" => JsonSerializer.SerializeToNode(value.Value, options),
                    "pad" => JsonSerializer.SerializeToNode(value.Value, options),
                    "card" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for ach
    /// </summary>
    public struct Ach
    {
        public Ach(Payroc.AchSource value)
        {
            Value = value;
        }

        internal Payroc.AchSource Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Ach(Payroc.AchSource value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for pad
    /// </summary>
    public struct Pad
    {
        public Pad(Payroc.PadSource value)
        {
            Value = value;
        }

        internal Payroc.PadSource Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Pad(Payroc.PadSource value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for card
    /// </summary>
    public struct Card
    {
        public Card(Payroc.CardSource value)
        {
            Value = value;
        }

        internal Payroc.CardSource Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Card(Payroc.CardSource value) => new(value);
    }
}

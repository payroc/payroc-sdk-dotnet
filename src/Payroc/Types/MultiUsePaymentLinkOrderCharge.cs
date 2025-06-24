// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(MultiUsePaymentLinkOrderCharge.JsonConverter))]
[Serializable]
public record MultiUsePaymentLinkOrderCharge
{
    internal MultiUsePaymentLinkOrderCharge(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of MultiUsePaymentLinkOrderCharge with <see cref="MultiUsePaymentLinkOrderCharge.Prompt"/>.
    /// </summary>
    public MultiUsePaymentLinkOrderCharge(MultiUsePaymentLinkOrderCharge.Prompt value)
    {
        Type = "prompt";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of MultiUsePaymentLinkOrderCharge with <see cref="MultiUsePaymentLinkOrderCharge.Preset"/>.
    /// </summary>
    public MultiUsePaymentLinkOrderCharge(MultiUsePaymentLinkOrderCharge.Preset value)
    {
        Type = "preset";
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
    /// Returns true if <see cref="Type"/> is "prompt"
    /// </summary>
    public bool IsPrompt => Type == "prompt";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "preset"
    /// </summary>
    public bool IsPreset => Type == "preset";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PromptPaymentLinkCharge"/> if <see cref="Type"/> is 'prompt', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'prompt'.</exception>
    public Payroc.PromptPaymentLinkCharge AsPrompt() =>
        IsPrompt
            ? (Payroc.PromptPaymentLinkCharge)Value!
            : throw new Exception("MultiUsePaymentLinkOrderCharge.Type is not 'prompt'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PresetPaymentLinkCharge"/> if <see cref="Type"/> is 'preset', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'preset'.</exception>
    public Payroc.PresetPaymentLinkCharge AsPreset() =>
        IsPreset
            ? (Payroc.PresetPaymentLinkCharge)Value!
            : throw new Exception("MultiUsePaymentLinkOrderCharge.Type is not 'preset'");

    public T Match<T>(
        Func<Payroc.PromptPaymentLinkCharge, T> onPrompt,
        Func<Payroc.PresetPaymentLinkCharge, T> onPreset,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "prompt" => onPrompt(AsPrompt()),
            "preset" => onPreset(AsPreset()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.PromptPaymentLinkCharge> onPrompt,
        Action<Payroc.PresetPaymentLinkCharge> onPreset,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "prompt":
                onPrompt(AsPrompt());
                break;
            case "preset":
                onPreset(AsPreset());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PromptPaymentLinkCharge"/> and returns true if successful.
    /// </summary>
    public bool TryAsPrompt(out Payroc.PromptPaymentLinkCharge? value)
    {
        if (Type == "prompt")
        {
            value = (Payroc.PromptPaymentLinkCharge)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PresetPaymentLinkCharge"/> and returns true if successful.
    /// </summary>
    public bool TryAsPreset(out Payroc.PresetPaymentLinkCharge? value)
    {
        if (Type == "preset")
        {
            value = (Payroc.PresetPaymentLinkCharge)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator MultiUsePaymentLinkOrderCharge(
        MultiUsePaymentLinkOrderCharge.Prompt value
    ) => new(value);

    public static implicit operator MultiUsePaymentLinkOrderCharge(
        MultiUsePaymentLinkOrderCharge.Preset value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<MultiUsePaymentLinkOrderCharge>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(MultiUsePaymentLinkOrderCharge).IsAssignableFrom(typeToConvert);

        public override MultiUsePaymentLinkOrderCharge Read(
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
                "prompt" => json.Deserialize<Payroc.PromptPaymentLinkCharge>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.PromptPaymentLinkCharge"
                    ),
                "preset" => json.Deserialize<Payroc.PresetPaymentLinkCharge>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Payroc.PresetPaymentLinkCharge"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new MultiUsePaymentLinkOrderCharge(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            MultiUsePaymentLinkOrderCharge value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "prompt" => JsonSerializer.SerializeToNode(value.Value, options),
                    "preset" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for prompt
    /// </summary>
    [Serializable]
    public struct Prompt
    {
        public Prompt(Payroc.PromptPaymentLinkCharge value)
        {
            Value = value;
        }

        internal Payroc.PromptPaymentLinkCharge Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Prompt(Payroc.PromptPaymentLinkCharge value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for preset
    /// </summary>
    [Serializable]
    public struct Preset
    {
        public Preset(Payroc.PresetPaymentLinkCharge value)
        {
            Value = value;
        }

        internal Payroc.PresetPaymentLinkCharge Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Preset(Payroc.PresetPaymentLinkCharge value) => new(value);
    }
}

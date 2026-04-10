using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(SupportedOperationsItem.SupportedOperationsItemSerializer))]
[Serializable]
public readonly record struct SupportedOperationsItem : IStringEnum
{
    public static readonly SupportedOperationsItem Capture = new(Values.Capture);

    public static readonly SupportedOperationsItem Refund = new(Values.Refund);

    public static readonly SupportedOperationsItem FullyReverse = new(Values.FullyReverse);

    public static readonly SupportedOperationsItem PartiallyReverse = new(Values.PartiallyReverse);

    public static readonly SupportedOperationsItem IncrementAuthorization = new(
        Values.IncrementAuthorization
    );

    public static readonly SupportedOperationsItem AdjustTip = new(Values.AdjustTip);

    public static readonly SupportedOperationsItem AddSignature = new(Values.AddSignature);

    public static readonly SupportedOperationsItem SetAsReady = new(Values.SetAsReady);

    public static readonly SupportedOperationsItem SetAsPending = new(Values.SetAsPending);

    public SupportedOperationsItem(string value)
    {
        Value = value;
    }

    /// <summary>
    /// The string value of the enum.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Create a string enum with the given value.
    /// </summary>
    public static SupportedOperationsItem FromCustom(string value)
    {
        return new SupportedOperationsItem(value);
    }

    public bool Equals(string? other)
    {
        return Value.Equals(other);
    }

    /// <summary>
    /// Returns the string value of the enum.
    /// </summary>
    public override string ToString()
    {
        return Value;
    }

    public static bool operator ==(SupportedOperationsItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SupportedOperationsItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SupportedOperationsItem value) => value.Value;

    public static explicit operator SupportedOperationsItem(string value) => new(value);

    internal class SupportedOperationsItemSerializer : JsonConverter<SupportedOperationsItem>
    {
        public override SupportedOperationsItem Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new SupportedOperationsItem(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SupportedOperationsItem value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override SupportedOperationsItem ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new SupportedOperationsItem(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            SupportedOperationsItem value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Capture = "capture";

        public const string Refund = "refund";

        public const string FullyReverse = "fullyReverse";

        public const string PartiallyReverse = "partiallyReverse";

        public const string IncrementAuthorization = "incrementAuthorization";

        public const string AdjustTip = "adjustTip";

        public const string AddSignature = "addSignature";

        public const string SetAsReady = "setAsReady";

        public const string SetAsPending = "setAsPending";
    }
}

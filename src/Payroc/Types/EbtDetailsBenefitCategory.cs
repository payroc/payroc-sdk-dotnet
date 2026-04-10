using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(EbtDetailsBenefitCategory.EbtDetailsBenefitCategorySerializer))]
[Serializable]
public readonly record struct EbtDetailsBenefitCategory : IStringEnum
{
    public static readonly EbtDetailsBenefitCategory Cash = new(Values.Cash);

    public static readonly EbtDetailsBenefitCategory FoodStamp = new(Values.FoodStamp);

    public EbtDetailsBenefitCategory(string value)
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
    public static EbtDetailsBenefitCategory FromCustom(string value)
    {
        return new EbtDetailsBenefitCategory(value);
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

    public static bool operator ==(EbtDetailsBenefitCategory value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(EbtDetailsBenefitCategory value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(EbtDetailsBenefitCategory value) => value.Value;

    public static explicit operator EbtDetailsBenefitCategory(string value) => new(value);

    internal class EbtDetailsBenefitCategorySerializer : JsonConverter<EbtDetailsBenefitCategory>
    {
        public override EbtDetailsBenefitCategory Read(
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
            return new EbtDetailsBenefitCategory(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            EbtDetailsBenefitCategory value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override EbtDetailsBenefitCategory ReadAsPropertyName(
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
            return new EbtDetailsBenefitCategory(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            EbtDetailsBenefitCategory value,
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
        public const string Cash = "cash";

        public const string FoodStamp = "foodStamp";
    }
}

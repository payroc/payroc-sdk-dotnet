using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.HostedFields;

[JsonConverter(typeof(StringEnumSerializer<HostedFieldsCreateSessionRequestScenario>))]
[Serializable]
public readonly record struct HostedFieldsCreateSessionRequestScenario : IStringEnum
{
    public static readonly HostedFieldsCreateSessionRequestScenario Payment = new(Values.Payment);

    public static readonly HostedFieldsCreateSessionRequestScenario Tokenization = new(
        Values.Tokenization
    );

    public HostedFieldsCreateSessionRequestScenario(string value)
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
    public static HostedFieldsCreateSessionRequestScenario FromCustom(string value)
    {
        return new HostedFieldsCreateSessionRequestScenario(value);
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

    public static bool operator ==(
        HostedFieldsCreateSessionRequestScenario value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        HostedFieldsCreateSessionRequestScenario value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(HostedFieldsCreateSessionRequestScenario value) =>
        value.Value;

    public static explicit operator HostedFieldsCreateSessionRequestScenario(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Payment = "payment";

        public const string Tokenization = "tokenization";
    }
}

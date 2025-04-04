using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<ThirdPartyThreeDSecureEci>))]
public readonly record struct ThirdPartyThreeDSecureEci : IStringEnum
{
    public static readonly ThirdPartyThreeDSecureEci FullyAuthenticated = Custom(
        Values.FullyAuthenticated
    );

    public static readonly ThirdPartyThreeDSecureEci AttemptedAuthentication = Custom(
        Values.AttemptedAuthentication
    );

    public ThirdPartyThreeDSecureEci(string value)
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
    public static ThirdPartyThreeDSecureEci Custom(string value)
    {
        return new ThirdPartyThreeDSecureEci(value);
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

    public static bool operator ==(ThirdPartyThreeDSecureEci value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ThirdPartyThreeDSecureEci value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string FullyAuthenticated = "fullyAuthenticated";

        public const string AttemptedAuthentication = "attemptedAuthentication";
    }
}

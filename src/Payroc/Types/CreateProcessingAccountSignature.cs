using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CreateProcessingAccountSignature>))]
public readonly record struct CreateProcessingAccountSignature : IStringEnum
{
    public static readonly CreateProcessingAccountSignature RequestedViaEmail = new(
        Values.RequestedViaEmail
    );

    public static readonly CreateProcessingAccountSignature RequestedViaDirectLink = new(
        Values.RequestedViaDirectLink
    );

    public CreateProcessingAccountSignature(string value)
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
    public static CreateProcessingAccountSignature FromCustom(string value)
    {
        return new CreateProcessingAccountSignature(value);
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

    public static bool operator ==(CreateProcessingAccountSignature value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreateProcessingAccountSignature value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreateProcessingAccountSignature value) => value.Value;

    public static explicit operator CreateProcessingAccountSignature(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string RequestedViaEmail = "requestedViaEmail";

        public const string RequestedViaDirectLink = "requestedViaDirectLink";
    }
}

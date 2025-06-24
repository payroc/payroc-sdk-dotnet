using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<OfflineProcessingOperation>))]
[Serializable]
public readonly record struct OfflineProcessingOperation : IStringEnum
{
    public static readonly OfflineProcessingOperation OfflineDecline = new(Values.OfflineDecline);

    public static readonly OfflineProcessingOperation OfflineApproval = new(Values.OfflineApproval);

    public static readonly OfflineProcessingOperation DeferredAuthorization = new(
        Values.DeferredAuthorization
    );

    public OfflineProcessingOperation(string value)
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
    public static OfflineProcessingOperation FromCustom(string value)
    {
        return new OfflineProcessingOperation(value);
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

    public static bool operator ==(OfflineProcessingOperation value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(OfflineProcessingOperation value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(OfflineProcessingOperation value) => value.Value;

    public static explicit operator OfflineProcessingOperation(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string OfflineDecline = "offlineDecline";

        public const string OfflineApproval = "offlineApproval";

        public const string DeferredAuthorization = "deferredAuthorization";
    }
}

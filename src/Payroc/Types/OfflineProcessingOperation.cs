using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<OfflineProcessingOperation>))]
public readonly record struct OfflineProcessingOperation : IStringEnum
{
    public static readonly OfflineProcessingOperation OfflineDecline = Custom(
        Values.OfflineDecline
    );

    public static readonly OfflineProcessingOperation OfflineApproval = Custom(
        Values.OfflineApproval
    );

    public static readonly OfflineProcessingOperation DeferredAuthorization = Custom(
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
    public static OfflineProcessingOperation Custom(string value)
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

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string OfflineDecline = "offlineDecline";

        public const string OfflineApproval = "offlineApproval";

        public const string DeferredAuthorization = "deferredAuthorization";
    }
}

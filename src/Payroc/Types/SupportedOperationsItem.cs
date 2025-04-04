using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SupportedOperationsItem>))]
public readonly record struct SupportedOperationsItem : IStringEnum
{
    public static readonly SupportedOperationsItem Capture = Custom(Values.Capture);

    public static readonly SupportedOperationsItem Refund = Custom(Values.Refund);

    public static readonly SupportedOperationsItem FullyReverse = Custom(Values.FullyReverse);

    public static readonly SupportedOperationsItem PartiallyReverse = Custom(
        Values.PartiallyReverse
    );

    public static readonly SupportedOperationsItem IncrementAuthorization = Custom(
        Values.IncrementAuthorization
    );

    public static readonly SupportedOperationsItem AdjustTip = Custom(Values.AdjustTip);

    public static readonly SupportedOperationsItem AddSignature = Custom(Values.AddSignature);

    public static readonly SupportedOperationsItem SetAsReady = Custom(Values.SetAsReady);

    public static readonly SupportedOperationsItem SetAsPending = Custom(Values.SetAsPending);

    public static readonly SupportedOperationsItem SetAsDeclined = Custom(Values.SetAsDeclined);

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
    public static SupportedOperationsItem Custom(string value)
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

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
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

        public const string SetAsDeclined = "setAsDeclined";
    }
}

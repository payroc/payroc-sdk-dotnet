using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<DisputeStatusStatus>))]
public readonly record struct DisputeStatusStatus : IStringEnum
{
    public static readonly DisputeStatusStatus PrearbitrationInProcess = Custom(
        Values.PrearbitrationInProcess
    );

    public static readonly DisputeStatusStatus PrearbitrationAccepted = Custom(
        Values.PrearbitrationAccepted
    );

    public static readonly DisputeStatusStatus PrearbitrationDeclined = Custom(
        Values.PrearbitrationDeclined
    );

    public static readonly DisputeStatusStatus ArbitrationFiledWithCardBand = Custom(
        Values.ArbitrationFiledWithCardBand
    );

    public static readonly DisputeStatusStatus ArbitrationFundsToBeReturned = Custom(
        Values.ArbitrationFundsToBeReturned
    );

    public static readonly DisputeStatusStatus ArbitrationLost = Custom(Values.ArbitrationLost);

    public static readonly DisputeStatusStatus ArbitrationSettledPartialAmount = Custom(
        Values.ArbitrationSettledPartialAmount
    );

    public static readonly DisputeStatusStatus PrecomplianceInProcess = Custom(
        Values.PrecomplianceInProcess
    );

    public static readonly DisputeStatusStatus PrecomplianceAccepted = Custom(
        Values.PrecomplianceAccepted
    );

    public static readonly DisputeStatusStatus PrecomplianceDeclined = Custom(
        Values.PrecomplianceDeclined
    );

    public static readonly DisputeStatusStatus ComplianceFiledWithCardBand = Custom(
        Values.ComplianceFiledWithCardBand
    );

    public static readonly DisputeStatusStatus ComplianceLost = Custom(Values.ComplianceLost);

    public static readonly DisputeStatusStatus ComplianceSettledPartialAmount = Custom(
        Values.ComplianceSettledPartialAmount
    );

    public static readonly DisputeStatusStatus Invalid = Custom(Values.Invalid);

    public static readonly DisputeStatusStatus IssuerReversal = Custom(Values.IssuerReversal);

    public static readonly DisputeStatusStatus New = Custom(Values.New);

    public static readonly DisputeStatusStatus Rejected = Custom(Values.Rejected);

    public static readonly DisputeStatusStatus RepresentmentInProgress = Custom(
        Values.RepresentmentInProgress
    );

    public static readonly DisputeStatusStatus RepresentmentFailed = Custom(
        Values.RepresentmentFailed
    );

    public static readonly DisputeStatusStatus RepresentmentPaid = Custom(Values.RepresentmentPaid);

    public static readonly DisputeStatusStatus RepresentmentReceived = Custom(
        Values.RepresentmentReceived
    );

    public static readonly DisputeStatusStatus Stand = Custom(Values.Stand);

    public DisputeStatusStatus(string value)
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
    public static DisputeStatusStatus Custom(string value)
    {
        return new DisputeStatusStatus(value);
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

    public static bool operator ==(DisputeStatusStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(DisputeStatusStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string PrearbitrationInProcess = "prearbitrationInProcess";

        public const string PrearbitrationAccepted = "prearbitrationAccepted";

        public const string PrearbitrationDeclined = "prearbitrationDeclined";

        public const string ArbitrationFiledWithCardBand = "arbitrationFiledWithCardBand";

        public const string ArbitrationFundsToBeReturned = "arbitrationFundsToBeReturned";

        public const string ArbitrationLost = "arbitrationLost";

        public const string ArbitrationSettledPartialAmount = "arbitrationSettledPartialAmount";

        public const string PrecomplianceInProcess = "precomplianceInProcess";

        public const string PrecomplianceAccepted = "precomplianceAccepted";

        public const string PrecomplianceDeclined = "precomplianceDeclined";

        public const string ComplianceFiledWithCardBand = "complianceFiledWithCardBand";

        public const string ComplianceLost = "complianceLost";

        public const string ComplianceSettledPartialAmount = "complianceSettledPartialAmount";

        public const string Invalid = "invalid";

        public const string IssuerReversal = "issuerReversal";

        public const string New = "new";

        public const string Rejected = "rejected";

        public const string RepresentmentInProgress = "representmentInProgress";

        public const string RepresentmentFailed = "representmentFailed";

        public const string RepresentmentPaid = "representmentPaid";

        public const string RepresentmentReceived = "representmentReceived";

        public const string Stand = "stand";
    }
}

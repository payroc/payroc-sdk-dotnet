using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<DisputeStatusStatus>))]
public readonly record struct DisputeStatusStatus : IStringEnum
{
    public static readonly DisputeStatusStatus PrearbitrationInProcess = new(
        Values.PrearbitrationInProcess
    );

    public static readonly DisputeStatusStatus PrearbitrationAccepted = new(
        Values.PrearbitrationAccepted
    );

    public static readonly DisputeStatusStatus PrearbitrationDeclined = new(
        Values.PrearbitrationDeclined
    );

    public static readonly DisputeStatusStatus ArbitrationFiledWithCardBand = new(
        Values.ArbitrationFiledWithCardBand
    );

    public static readonly DisputeStatusStatus ArbitrationFundsToBeReturned = new(
        Values.ArbitrationFundsToBeReturned
    );

    public static readonly DisputeStatusStatus ArbitrationLost = new(Values.ArbitrationLost);

    public static readonly DisputeStatusStatus ArbitrationSettledPartialAmount = new(
        Values.ArbitrationSettledPartialAmount
    );

    public static readonly DisputeStatusStatus PrecomplianceInProcess = new(
        Values.PrecomplianceInProcess
    );

    public static readonly DisputeStatusStatus PrecomplianceAccepted = new(
        Values.PrecomplianceAccepted
    );

    public static readonly DisputeStatusStatus PrecomplianceDeclined = new(
        Values.PrecomplianceDeclined
    );

    public static readonly DisputeStatusStatus ComplianceFiledWithCardBand = new(
        Values.ComplianceFiledWithCardBand
    );

    public static readonly DisputeStatusStatus ComplianceLost = new(Values.ComplianceLost);

    public static readonly DisputeStatusStatus ComplianceSettledPartialAmount = new(
        Values.ComplianceSettledPartialAmount
    );

    public static readonly DisputeStatusStatus Invalid = new(Values.Invalid);

    public static readonly DisputeStatusStatus IssuerReversal = new(Values.IssuerReversal);

    public static readonly DisputeStatusStatus New = new(Values.New);

    public static readonly DisputeStatusStatus Rejected = new(Values.Rejected);

    public static readonly DisputeStatusStatus RepresentmentInProgress = new(
        Values.RepresentmentInProgress
    );

    public static readonly DisputeStatusStatus RepresentmentFailed = new(
        Values.RepresentmentFailed
    );

    public static readonly DisputeStatusStatus RepresentmentPaid = new(Values.RepresentmentPaid);

    public static readonly DisputeStatusStatus RepresentmentReceived = new(
        Values.RepresentmentReceived
    );

    public static readonly DisputeStatusStatus Stand = new(Values.Stand);

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
    public static DisputeStatusStatus FromCustom(string value)
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

    public static explicit operator string(DisputeStatusStatus value) => value.Value;

    public static explicit operator DisputeStatusStatus(string value) => new(value);

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

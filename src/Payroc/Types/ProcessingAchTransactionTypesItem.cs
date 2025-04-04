using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<ProcessingAchTransactionTypesItem>))]
public readonly record struct ProcessingAchTransactionTypesItem : IStringEnum
{
    public static readonly ProcessingAchTransactionTypesItem PrearrangedPayment = Custom(
        Values.PrearrangedPayment
    );

    public static readonly ProcessingAchTransactionTypesItem CorpCashDisbursement = Custom(
        Values.CorpCashDisbursement
    );

    public static readonly ProcessingAchTransactionTypesItem TelephoneInitiatedPayment = Custom(
        Values.TelephoneInitiatedPayment
    );

    public static readonly ProcessingAchTransactionTypesItem WebInitiatedPayment = Custom(
        Values.WebInitiatedPayment
    );

    public static readonly ProcessingAchTransactionTypesItem Other = Custom(Values.Other);

    public ProcessingAchTransactionTypesItem(string value)
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
    public static ProcessingAchTransactionTypesItem Custom(string value)
    {
        return new ProcessingAchTransactionTypesItem(value);
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

    public static bool operator ==(ProcessingAchTransactionTypesItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ProcessingAchTransactionTypesItem value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string PrearrangedPayment = "prearrangedPayment";

        public const string CorpCashDisbursement = "corpCashDisbursement";

        public const string TelephoneInitiatedPayment = "telephoneInitiatedPayment";

        public const string WebInitiatedPayment = "webInitiatedPayment";

        public const string Other = "other";
    }
}

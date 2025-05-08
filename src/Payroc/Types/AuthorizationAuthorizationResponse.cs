using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<AuthorizationAuthorizationResponse>))]
public readonly record struct AuthorizationAuthorizationResponse : IStringEnum
{
    public static readonly AuthorizationAuthorizationResponse ActivityCountLimitExceeded = new(
        Values.ActivityCountLimitExceeded
    );

    public static readonly AuthorizationAuthorizationResponse AlreadyReversed = new(
        Values.AlreadyReversed
    );

    public static readonly AuthorizationAuthorizationResponse Approved = new(Values.Approved);

    public static readonly AuthorizationAuthorizationResponse ApproveVip = new(Values.ApproveVip);

    public static readonly AuthorizationAuthorizationResponse ApproveWithId = new(
        Values.ApproveWithId
    );

    public static readonly AuthorizationAuthorizationResponse CannotVerifyPin = new(
        Values.CannotVerifyPin
    );

    public static readonly AuthorizationAuthorizationResponse CardAuthenticationFailed = new(
        Values.CardAuthenticationFailed
    );

    public static readonly AuthorizationAuthorizationResponse CardTypeVerificationError = new(
        Values.CardTypeVerificationError
    );

    public static readonly AuthorizationAuthorizationResponse CashRequestExceedsIssuerLimit = new(
        Values.CashRequestExceedsIssuerLimit
    );

    public static readonly AuthorizationAuthorizationResponse CashServiceNotAvailable = new(
        Values.CashServiceNotAvailable
    );

    public static readonly AuthorizationAuthorizationResponse CidVerificationError = new(
        Values.CidVerificationError
    );

    public static readonly AuthorizationAuthorizationResponse ContactCardIssuer = new(
        Values.ContactCardIssuer
    );

    public static readonly AuthorizationAuthorizationResponse CryptographicFailure = new(
        Values.CryptographicFailure
    );

    public static readonly AuthorizationAuthorizationResponse DailyThresholdExceeded = new(
        Values.DailyThresholdExceeded
    );

    public static readonly AuthorizationAuthorizationResponse DeclineCvv2Failure = new(
        Values.DeclineCvv2Failure
    );

    public static readonly AuthorizationAuthorizationResponse Deny = new(Values.Deny);

    public static readonly AuthorizationAuthorizationResponse DenyAccountCanceled = new(
        Values.DenyAccountCanceled
    );

    public static readonly AuthorizationAuthorizationResponse DenyClosedMerchant = new(
        Values.DenyClosedMerchant
    );

    public static readonly AuthorizationAuthorizationResponse DenyNewCardIssued = new(
        Values.DenyNewCardIssued
    );

    public static readonly AuthorizationAuthorizationResponse DenyPickUpCard = new(
        Values.DenyPickUpCard
    );

    public static readonly AuthorizationAuthorizationResponse DestinationCannotBeFoundForRouting =
        new(Values.DestinationCannotBeFoundForRouting);

    public static readonly AuthorizationAuthorizationResponse DoNotHonor = new(Values.DoNotHonor);

    public static readonly AuthorizationAuthorizationResponse DuplicateTransmissionDetected = new(
        Values.DuplicateTransmissionDetected
    );

    public static readonly AuthorizationAuthorizationResponse Error = new(Values.Error);

    public static readonly AuthorizationAuthorizationResponse ExceedsWithdrawalAmountLimit = new(
        Values.ExceedsWithdrawalAmountLimit
    );

    public static readonly AuthorizationAuthorizationResponse ExpiredCard = new(Values.ExpiredCard);

    public static readonly AuthorizationAuthorizationResponse FileTemporarilyUnavailable = new(
        Values.FileTemporarilyUnavailable
    );

    public static readonly AuthorizationAuthorizationResponse ForceStip = new(Values.ForceStip);

    public static readonly AuthorizationAuthorizationResponse FormatError = new(Values.FormatError);

    public static readonly AuthorizationAuthorizationResponse ForwardToIssuer = new(
        Values.ForwardToIssuer
    );

    public static readonly AuthorizationAuthorizationResponse FunctionNotSupported = new(
        Values.FunctionNotSupported
    );

    public static readonly AuthorizationAuthorizationResponse HonorWithId = new(Values.HonorWithId);

    public static readonly AuthorizationAuthorizationResponse IncorrectCvv = new(
        Values.IncorrectCvv
    );

    public static readonly AuthorizationAuthorizationResponse IncorrectPin = new(
        Values.IncorrectPin
    );

    public static readonly AuthorizationAuthorizationResponse IneligibleForResubmission = new(
        Values.IneligibleForResubmission
    );

    public static readonly AuthorizationAuthorizationResponse InsufficientFunds = new(
        Values.InsufficientFunds
    );

    public static readonly AuthorizationAuthorizationResponse InvalidAccount = new(
        Values.InvalidAccount
    );

    public static readonly AuthorizationAuthorizationResponse InvalidAccountNumber = new(
        Values.InvalidAccountNumber
    );

    public static readonly AuthorizationAuthorizationResponse InvalidAmount = new(
        Values.InvalidAmount
    );

    public static readonly AuthorizationAuthorizationResponse InvalidAuthorizationLifeCycle = new(
        Values.InvalidAuthorizationLifeCycle
    );

    public static readonly AuthorizationAuthorizationResponse InvalidBillerInformation = new(
        Values.InvalidBillerInformation
    );

    public static readonly AuthorizationAuthorizationResponse InvalidCardSecurityCode = new(
        Values.InvalidCardSecurityCode
    );

    public static readonly AuthorizationAuthorizationResponse InvalidCurrencyCode = new(
        Values.InvalidCurrencyCode
    );

    public static readonly AuthorizationAuthorizationResponse InvalidMerchant = new(
        Values.InvalidMerchant
    );

    public static readonly AuthorizationAuthorizationResponse InvalidResponse = new(
        Values.InvalidResponse
    );

    public static readonly AuthorizationAuthorizationResponse InvalidTransaction = new(
        Values.InvalidTransaction
    );

    public static readonly AuthorizationAuthorizationResponse IssuerNotAvailable = new(
        Values.IssuerNotAvailable
    );

    public static readonly AuthorizationAuthorizationResponse IssuerTimeout = new(
        Values.IssuerTimeout
    );

    public static readonly AuthorizationAuthorizationResponse IssuerUnavailable = new(
        Values.IssuerUnavailable
    );

    public static readonly AuthorizationAuthorizationResponse NoActionTaken = new(
        Values.NoActionTaken
    );

    public static readonly AuthorizationAuthorizationResponse NoCardRecord = new(
        Values.NoCardRecord
    );

    public static readonly AuthorizationAuthorizationResponse NoCheckingAccount = new(
        Values.NoCheckingAccount
    );

    public static readonly AuthorizationAuthorizationResponse NoCreditAccount = new(
        Values.NoCreditAccount
    );

    public static readonly AuthorizationAuthorizationResponse NoFinancialImpact = new(
        Values.NoFinancialImpact
    );

    public static readonly AuthorizationAuthorizationResponse NoReasonToDecline = new(
        Values.NoReasonToDecline
    );

    public static readonly AuthorizationAuthorizationResponse NoSavingsAccount = new(
        Values.NoSavingsAccount
    );

    public static readonly AuthorizationAuthorizationResponse NoSuchIssuer = new(
        Values.NoSuchIssuer
    );

    public static readonly AuthorizationAuthorizationResponse PartialApproval = new(
        Values.PartialApproval
    );

    public static readonly AuthorizationAuthorizationResponse PartialAuthorization = new(
        Values.PartialAuthorization
    );

    public static readonly AuthorizationAuthorizationResponse PickUpCard = new(Values.PickUpCard);

    public static readonly AuthorizationAuthorizationResponse PickUpCardSpecialCondition = new(
        Values.PickUpCardSpecialCondition
    );

    public static readonly AuthorizationAuthorizationResponse PinChangeRequestDeclined = new(
        Values.PinChangeRequestDeclined
    );

    public static readonly AuthorizationAuthorizationResponse PinCryptographicErrorFound = new(
        Values.PinCryptographicErrorFound
    );

    public static readonly AuthorizationAuthorizationResponse PinEntryTriesExceeded = new(
        Values.PinEntryTriesExceeded
    );

    public static readonly AuthorizationAuthorizationResponse PinNotChanged = new(
        Values.PinNotChanged
    );

    public static readonly AuthorizationAuthorizationResponse PleaseCallIssuer = new(
        Values.PleaseCallIssuer
    );

    public static readonly AuthorizationAuthorizationResponse ReenterTransaction = new(
        Values.ReenterTransaction
    );

    public static readonly AuthorizationAuthorizationResponse ReferToCardIssuer = new(
        Values.ReferToCardIssuer
    );

    public static readonly AuthorizationAuthorizationResponse ReferToCardIssuerSpecialCondition =
        new(Values.ReferToCardIssuerSpecialCondition);

    public static readonly AuthorizationAuthorizationResponse RestrictedCard = new(
        Values.RestrictedCard
    );

    public static readonly AuthorizationAuthorizationResponse Reversal = new(Values.Reversal);

    public static readonly AuthorizationAuthorizationResponse ReversalDataInconsistent = new(
        Values.ReversalDataInconsistent
    );

    public static readonly AuthorizationAuthorizationResponse RevokeAllAuthorizationsOrder = new(
        Values.RevokeAllAuthorizationsOrder
    );

    public static readonly AuthorizationAuthorizationResponse ScheduledTransactionstoppedByCardholder =
        new(Values.ScheduledTransactionstoppedByCardholder);

    public static readonly AuthorizationAuthorizationResponse SecurityViolation = new(
        Values.SecurityViolation
    );

    public static readonly AuthorizationAuthorizationResponse Successful = new(Values.Successful);

    public static readonly AuthorizationAuthorizationResponse SurchargeAmountNotPermitted = new(
        Values.SurchargeAmountNotPermitted
    );

    public static readonly AuthorizationAuthorizationResponse SuspectFraud = new(
        Values.SuspectFraud
    );

    public static readonly AuthorizationAuthorizationResponse SystemMalfunction = new(
        Values.SystemMalfunction
    );

    public static readonly AuthorizationAuthorizationResponse TransactionAmountExceedsApprovalAmount =
        new(Values.TransactionAmountExceedsApprovalAmount);

    public static readonly AuthorizationAuthorizationResponse TransactionCannotBeCompleted = new(
        Values.TransactionCannotBeCompleted
    );

    public static readonly AuthorizationAuthorizationResponse TransactionNotAllowedAtMerchant = new(
        Values.TransactionNotAllowedAtMerchant
    );

    public static readonly AuthorizationAuthorizationResponse TransactionNotAllowedAtTerminal = new(
        Values.TransactionNotAllowedAtTerminal
    );

    public static readonly AuthorizationAuthorizationResponse TransactionNotPermitted = new(
        Values.TransactionNotPermitted
    );

    public static readonly AuthorizationAuthorizationResponse TransactionNotPermittedToCardholder =
        new(Values.TransactionNotPermittedToCardholder);

    public static readonly AuthorizationAuthorizationResponse UnableToGoOnline = new(
        Values.UnableToGoOnline
    );

    public static readonly AuthorizationAuthorizationResponse UnableToLocateRecordInFile = new(
        Values.UnableToLocateRecordInFile
    );

    public static readonly AuthorizationAuthorizationResponse UnableToVerifyPin = new(
        Values.UnableToVerifyPin
    );

    public static readonly AuthorizationAuthorizationResponse UnacceptablePin = new(
        Values.UnacceptablePin
    );

    public static readonly AuthorizationAuthorizationResponse Unknown = new(Values.Unknown);

    public static readonly AuthorizationAuthorizationResponse UnsafePin = new(Values.UnsafePin);

    public AuthorizationAuthorizationResponse(string value)
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
    public static AuthorizationAuthorizationResponse FromCustom(string value)
    {
        return new AuthorizationAuthorizationResponse(value);
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

    public static bool operator ==(AuthorizationAuthorizationResponse value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AuthorizationAuthorizationResponse value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(AuthorizationAuthorizationResponse value) => value.Value;

    public static explicit operator AuthorizationAuthorizationResponse(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string ActivityCountLimitExceeded = "activityCountLimitExceeded";

        public const string AlreadyReversed = "alreadyReversed";

        public const string Approved = "approved";

        public const string ApproveVip = "approveVip";

        public const string ApproveWithId = "approveWithId";

        public const string CannotVerifyPin = "cannotVerifyPin";

        public const string CardAuthenticationFailed = "cardAuthenticationFailed";

        public const string CardTypeVerificationError = "cardTypeVerificationError";

        public const string CashRequestExceedsIssuerLimit = "cashRequestExceedsIssuerLimit";

        public const string CashServiceNotAvailable = "cashServiceNotAvailable";

        public const string CidVerificationError = "cidVerificationError";

        public const string ContactCardIssuer = "contactCardIssuer";

        public const string CryptographicFailure = "cryptographicFailure";

        public const string DailyThresholdExceeded = "dailyThresholdExceeded";

        public const string DeclineCvv2Failure = "declineCvv2Failure";

        public const string Deny = "deny";

        public const string DenyAccountCanceled = "denyAccountCanceled";

        public const string DenyClosedMerchant = "denyClosedMerchant";

        public const string DenyNewCardIssued = "denyNewCardIssued";

        public const string DenyPickUpCard = "denyPickUpCard";

        public const string DestinationCannotBeFoundForRouting =
            "destinationCannotBeFoundForRouting";

        public const string DoNotHonor = "doNotHonor";

        public const string DuplicateTransmissionDetected = "duplicateTransmissionDetected";

        public const string Error = "error";

        public const string ExceedsWithdrawalAmountLimit = "exceedsWithdrawalAmountLimit";

        public const string ExpiredCard = "expiredCard";

        public const string FileTemporarilyUnavailable = "fileTemporarilyUnavailable";

        public const string ForceStip = "forceStip";

        public const string FormatError = "formatError";

        public const string ForwardToIssuer = "forwardToIssuer";

        public const string FunctionNotSupported = "functionNotSupported";

        public const string HonorWithId = "honorWithId";

        public const string IncorrectCvv = "incorrectCvv";

        public const string IncorrectPin = "incorrectPin";

        public const string IneligibleForResubmission = "ineligibleForResubmission";

        public const string InsufficientFunds = "insufficientFunds";

        public const string InvalidAccount = "invalidAccount";

        public const string InvalidAccountNumber = "invalidAccountNumber";

        public const string InvalidAmount = "invalidAmount";

        public const string InvalidAuthorizationLifeCycle = "invalidAuthorizationLifeCycle";

        public const string InvalidBillerInformation = "invalidBillerInformation";

        public const string InvalidCardSecurityCode = "invalidCardSecurityCode";

        public const string InvalidCurrencyCode = "invalidCurrencyCode";

        public const string InvalidMerchant = "invalidMerchant";

        public const string InvalidResponse = "invalidResponse";

        public const string InvalidTransaction = "invalidTransaction";

        public const string IssuerNotAvailable = "issuerNotAvailable";

        public const string IssuerTimeout = "issuerTimeout";

        public const string IssuerUnavailable = "issuerUnavailable";

        public const string NoActionTaken = "noActionTaken";

        public const string NoCardRecord = "noCardRecord";

        public const string NoCheckingAccount = "noCheckingAccount";

        public const string NoCreditAccount = "noCreditAccount";

        public const string NoFinancialImpact = "noFinancialImpact";

        public const string NoReasonToDecline = "noReasonToDecline";

        public const string NoSavingsAccount = "noSavingsAccount";

        public const string NoSuchIssuer = "noSuchIssuer";

        public const string PartialApproval = "partialApproval";

        public const string PartialAuthorization = "partialAuthorization";

        public const string PickUpCard = "pickUpCard";

        public const string PickUpCardSpecialCondition = "pickUpCardSpecialCondition";

        public const string PinChangeRequestDeclined = "pinChangeRequestDeclined";

        public const string PinCryptographicErrorFound = "pinCryptographicErrorFound";

        public const string PinEntryTriesExceeded = "pinEntryTriesExceeded";

        public const string PinNotChanged = "pinNotChanged";

        public const string PleaseCallIssuer = "pleaseCallIssuer";

        public const string ReenterTransaction = "reenterTransaction";

        public const string ReferToCardIssuer = "referToCardIssuer";

        public const string ReferToCardIssuerSpecialCondition = "referToCardIssuerSpecialCondition";

        public const string RestrictedCard = "restrictedCard";

        public const string Reversal = "reversal";

        public const string ReversalDataInconsistent = "reversalDataInconsistent";

        public const string RevokeAllAuthorizationsOrder = "revokeAllAuthorizationsOrder";

        public const string ScheduledTransactionstoppedByCardholder =
            "scheduledTransactionstoppedByCardholder";

        public const string SecurityViolation = "securityViolation";

        public const string Successful = "successful";

        public const string SurchargeAmountNotPermitted = "surchargeAmountNotPermitted";

        public const string SuspectFraud = "suspectFraud";

        public const string SystemMalfunction = "systemMalfunction";

        public const string TransactionAmountExceedsApprovalAmount =
            "transactionAmountExceedsApprovalAmount";

        public const string TransactionCannotBeCompleted = "transactionCannotBeCompleted";

        public const string TransactionNotAllowedAtMerchant = "transactionNotAllowedAtMerchant";

        public const string TransactionNotAllowedAtTerminal = "transactionNotAllowedAtTerminal";

        public const string TransactionNotPermitted = "transactionNotPermitted";

        public const string TransactionNotPermittedToCardholder =
            "transactionNotPermittedToCardholder";

        public const string UnableToGoOnline = "unableToGoOnline";

        public const string UnableToLocateRecordInFile = "unableToLocateRecordInFile";

        public const string UnableToVerifyPin = "unableToVerifyPin";

        public const string UnacceptablePin = "unacceptablePin";

        public const string Unknown = "unknown";

        public const string UnsafePin = "unsafePin";
    }
}

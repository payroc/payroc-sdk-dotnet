using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<AuthorizationAuthorizationResponse>))]
public readonly record struct AuthorizationAuthorizationResponse : IStringEnum
{
    public static readonly AuthorizationAuthorizationResponse ActivityCountLimitExceeded = Custom(
        Values.ActivityCountLimitExceeded
    );

    public static readonly AuthorizationAuthorizationResponse AlreadyReversed = Custom(
        Values.AlreadyReversed
    );

    public static readonly AuthorizationAuthorizationResponse Approved = Custom(Values.Approved);

    public static readonly AuthorizationAuthorizationResponse ApproveVip = Custom(
        Values.ApproveVip
    );

    public static readonly AuthorizationAuthorizationResponse ApproveWithId = Custom(
        Values.ApproveWithId
    );

    public static readonly AuthorizationAuthorizationResponse CannotVerifyPin = Custom(
        Values.CannotVerifyPin
    );

    public static readonly AuthorizationAuthorizationResponse CardAuthenticationFailed = Custom(
        Values.CardAuthenticationFailed
    );

    public static readonly AuthorizationAuthorizationResponse CardTypeVerificationError = Custom(
        Values.CardTypeVerificationError
    );

    public static readonly AuthorizationAuthorizationResponse CashRequestExceedsIssuerLimit =
        Custom(Values.CashRequestExceedsIssuerLimit);

    public static readonly AuthorizationAuthorizationResponse CashServiceNotAvailable = Custom(
        Values.CashServiceNotAvailable
    );

    public static readonly AuthorizationAuthorizationResponse CidVerificationError = Custom(
        Values.CidVerificationError
    );

    public static readonly AuthorizationAuthorizationResponse ContactCardIssuer = Custom(
        Values.ContactCardIssuer
    );

    public static readonly AuthorizationAuthorizationResponse CryptographicFailure = Custom(
        Values.CryptographicFailure
    );

    public static readonly AuthorizationAuthorizationResponse DailyThresholdExceeded = Custom(
        Values.DailyThresholdExceeded
    );

    public static readonly AuthorizationAuthorizationResponse DeclineCvv2Failure = Custom(
        Values.DeclineCvv2Failure
    );

    public static readonly AuthorizationAuthorizationResponse Deny = Custom(Values.Deny);

    public static readonly AuthorizationAuthorizationResponse DenyAccountCanceled = Custom(
        Values.DenyAccountCanceled
    );

    public static readonly AuthorizationAuthorizationResponse DenyClosedMerchant = Custom(
        Values.DenyClosedMerchant
    );

    public static readonly AuthorizationAuthorizationResponse DenyNewCardIssued = Custom(
        Values.DenyNewCardIssued
    );

    public static readonly AuthorizationAuthorizationResponse DenyPickUpCard = Custom(
        Values.DenyPickUpCard
    );

    public static readonly AuthorizationAuthorizationResponse DestinationCannotBeFoundForRouting =
        Custom(Values.DestinationCannotBeFoundForRouting);

    public static readonly AuthorizationAuthorizationResponse DoNotHonor = Custom(
        Values.DoNotHonor
    );

    public static readonly AuthorizationAuthorizationResponse DuplicateTransmissionDetected =
        Custom(Values.DuplicateTransmissionDetected);

    public static readonly AuthorizationAuthorizationResponse Error = Custom(Values.Error);

    public static readonly AuthorizationAuthorizationResponse ExceedsWithdrawalAmountLimit = Custom(
        Values.ExceedsWithdrawalAmountLimit
    );

    public static readonly AuthorizationAuthorizationResponse ExpiredCard = Custom(
        Values.ExpiredCard
    );

    public static readonly AuthorizationAuthorizationResponse FileTemporarilyUnavailable = Custom(
        Values.FileTemporarilyUnavailable
    );

    public static readonly AuthorizationAuthorizationResponse ForceStip = Custom(Values.ForceStip);

    public static readonly AuthorizationAuthorizationResponse FormatError = Custom(
        Values.FormatError
    );

    public static readonly AuthorizationAuthorizationResponse ForwardToIssuer = Custom(
        Values.ForwardToIssuer
    );

    public static readonly AuthorizationAuthorizationResponse FunctionNotSupported = Custom(
        Values.FunctionNotSupported
    );

    public static readonly AuthorizationAuthorizationResponse HonorWithId = Custom(
        Values.HonorWithId
    );

    public static readonly AuthorizationAuthorizationResponse IncorrectCvv = Custom(
        Values.IncorrectCvv
    );

    public static readonly AuthorizationAuthorizationResponse IncorrectPin = Custom(
        Values.IncorrectPin
    );

    public static readonly AuthorizationAuthorizationResponse IneligibleForResubmission = Custom(
        Values.IneligibleForResubmission
    );

    public static readonly AuthorizationAuthorizationResponse InsufficientFunds = Custom(
        Values.InsufficientFunds
    );

    public static readonly AuthorizationAuthorizationResponse InvalidAccount = Custom(
        Values.InvalidAccount
    );

    public static readonly AuthorizationAuthorizationResponse InvalidAccountNumber = Custom(
        Values.InvalidAccountNumber
    );

    public static readonly AuthorizationAuthorizationResponse InvalidAmount = Custom(
        Values.InvalidAmount
    );

    public static readonly AuthorizationAuthorizationResponse InvalidAuthorizationLifeCycle =
        Custom(Values.InvalidAuthorizationLifeCycle);

    public static readonly AuthorizationAuthorizationResponse InvalidBillerInformation = Custom(
        Values.InvalidBillerInformation
    );

    public static readonly AuthorizationAuthorizationResponse InvalidCardSecurityCode = Custom(
        Values.InvalidCardSecurityCode
    );

    public static readonly AuthorizationAuthorizationResponse InvalidCurrencyCode = Custom(
        Values.InvalidCurrencyCode
    );

    public static readonly AuthorizationAuthorizationResponse InvalidMerchant = Custom(
        Values.InvalidMerchant
    );

    public static readonly AuthorizationAuthorizationResponse InvalidResponse = Custom(
        Values.InvalidResponse
    );

    public static readonly AuthorizationAuthorizationResponse InvalidTransaction = Custom(
        Values.InvalidTransaction
    );

    public static readonly AuthorizationAuthorizationResponse IssuerNotAvailable = Custom(
        Values.IssuerNotAvailable
    );

    public static readonly AuthorizationAuthorizationResponse IssuerTimeout = Custom(
        Values.IssuerTimeout
    );

    public static readonly AuthorizationAuthorizationResponse IssuerUnavailable = Custom(
        Values.IssuerUnavailable
    );

    public static readonly AuthorizationAuthorizationResponse NoActionTaken = Custom(
        Values.NoActionTaken
    );

    public static readonly AuthorizationAuthorizationResponse NoCardRecord = Custom(
        Values.NoCardRecord
    );

    public static readonly AuthorizationAuthorizationResponse NoCheckingAccount = Custom(
        Values.NoCheckingAccount
    );

    public static readonly AuthorizationAuthorizationResponse NoCreditAccount = Custom(
        Values.NoCreditAccount
    );

    public static readonly AuthorizationAuthorizationResponse NoFinancialImpact = Custom(
        Values.NoFinancialImpact
    );

    public static readonly AuthorizationAuthorizationResponse NoReasonToDecline = Custom(
        Values.NoReasonToDecline
    );

    public static readonly AuthorizationAuthorizationResponse NoSavingsAccount = Custom(
        Values.NoSavingsAccount
    );

    public static readonly AuthorizationAuthorizationResponse NoSuchIssuer = Custom(
        Values.NoSuchIssuer
    );

    public static readonly AuthorizationAuthorizationResponse PartialApproval = Custom(
        Values.PartialApproval
    );

    public static readonly AuthorizationAuthorizationResponse PartialAuthorization = Custom(
        Values.PartialAuthorization
    );

    public static readonly AuthorizationAuthorizationResponse PickUpCard = Custom(
        Values.PickUpCard
    );

    public static readonly AuthorizationAuthorizationResponse PickUpCardSpecialCondition = Custom(
        Values.PickUpCardSpecialCondition
    );

    public static readonly AuthorizationAuthorizationResponse PinChangeRequestDeclined = Custom(
        Values.PinChangeRequestDeclined
    );

    public static readonly AuthorizationAuthorizationResponse PinCryptographicErrorFound = Custom(
        Values.PinCryptographicErrorFound
    );

    public static readonly AuthorizationAuthorizationResponse PinEntryTriesExceeded = Custom(
        Values.PinEntryTriesExceeded
    );

    public static readonly AuthorizationAuthorizationResponse PinNotChanged = Custom(
        Values.PinNotChanged
    );

    public static readonly AuthorizationAuthorizationResponse PleaseCallIssuer = Custom(
        Values.PleaseCallIssuer
    );

    public static readonly AuthorizationAuthorizationResponse ReenterTransaction = Custom(
        Values.ReenterTransaction
    );

    public static readonly AuthorizationAuthorizationResponse ReferToCardIssuer = Custom(
        Values.ReferToCardIssuer
    );

    public static readonly AuthorizationAuthorizationResponse ReferToCardIssuerSpecialCondition =
        Custom(Values.ReferToCardIssuerSpecialCondition);

    public static readonly AuthorizationAuthorizationResponse RestrictedCard = Custom(
        Values.RestrictedCard
    );

    public static readonly AuthorizationAuthorizationResponse Reversal = Custom(Values.Reversal);

    public static readonly AuthorizationAuthorizationResponse ReversalDataInconsistent = Custom(
        Values.ReversalDataInconsistent
    );

    public static readonly AuthorizationAuthorizationResponse RevokeAllAuthorizationsOrder = Custom(
        Values.RevokeAllAuthorizationsOrder
    );

    public static readonly AuthorizationAuthorizationResponse ScheduledTransactionstoppedByCardholder =
        Custom(Values.ScheduledTransactionstoppedByCardholder);

    public static readonly AuthorizationAuthorizationResponse SecurityViolation = Custom(
        Values.SecurityViolation
    );

    public static readonly AuthorizationAuthorizationResponse Successful = Custom(
        Values.Successful
    );

    public static readonly AuthorizationAuthorizationResponse SurchargeAmountNotPermitted = Custom(
        Values.SurchargeAmountNotPermitted
    );

    public static readonly AuthorizationAuthorizationResponse SuspectFraud = Custom(
        Values.SuspectFraud
    );

    public static readonly AuthorizationAuthorizationResponse SystemMalfunction = Custom(
        Values.SystemMalfunction
    );

    public static readonly AuthorizationAuthorizationResponse TransactionAmountExceedsApprovalAmount =
        Custom(Values.TransactionAmountExceedsApprovalAmount);

    public static readonly AuthorizationAuthorizationResponse TransactionCannotBeCompleted = Custom(
        Values.TransactionCannotBeCompleted
    );

    public static readonly AuthorizationAuthorizationResponse TransactionNotAllowedAtMerchant =
        Custom(Values.TransactionNotAllowedAtMerchant);

    public static readonly AuthorizationAuthorizationResponse TransactionNotAllowedAtTerminal =
        Custom(Values.TransactionNotAllowedAtTerminal);

    public static readonly AuthorizationAuthorizationResponse TransactionNotPermitted = Custom(
        Values.TransactionNotPermitted
    );

    public static readonly AuthorizationAuthorizationResponse TransactionNotPermittedToCardholder =
        Custom(Values.TransactionNotPermittedToCardholder);

    public static readonly AuthorizationAuthorizationResponse UnableToGoOnline = Custom(
        Values.UnableToGoOnline
    );

    public static readonly AuthorizationAuthorizationResponse UnableToLocateRecordInFile = Custom(
        Values.UnableToLocateRecordInFile
    );

    public static readonly AuthorizationAuthorizationResponse UnableToVerifyPin = Custom(
        Values.UnableToVerifyPin
    );

    public static readonly AuthorizationAuthorizationResponse UnacceptablePin = Custom(
        Values.UnacceptablePin
    );

    public static readonly AuthorizationAuthorizationResponse Unknown = Custom(Values.Unknown);

    public static readonly AuthorizationAuthorizationResponse UnsafePin = Custom(Values.UnsafePin);

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
    public static AuthorizationAuthorizationResponse Custom(string value)
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

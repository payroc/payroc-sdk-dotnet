using Payroc.ApplePaySessions;
using Payroc.Attachments;
using Payroc.Auth;
using Payroc.BankTransferPayments;
using Payroc.Boarding;
using Payroc.CardPayments;
using Payroc.Funding;
using Payroc.HostedFields;
using Payroc.Notifications;
using Payroc.PaymentFeatures;
using Payroc.PaymentLinks;
using Payroc.PayrocCloud;
using Payroc.RepeatPayments;
using Payroc.Reporting;
using Payroc.Tokenization;

namespace Payroc;

public partial interface IBasePayrocClient
{
    public IPaymentLinksClient PaymentLinks { get; }
    public IHostedFieldsClient HostedFields { get; }
    public IApplePaySessionsClient ApplePaySessions { get; }
    public IAttachmentsClient Attachments { get; }
    public IAuthClient Auth { get; }
    public IFundingClient Funding { get; }
    public IBankTransferPaymentsClient BankTransferPayments { get; }
    public IBoardingClient Boarding { get; }
    public ICardPaymentsClient CardPayments { get; }
    public INotificationsClient Notifications { get; }
    public IPaymentFeaturesClient PaymentFeatures { get; }
    public IPayrocCloudClient PayrocCloud { get; }
    public IRepeatPaymentsClient RepeatPayments { get; }
    public IReportingClient Reporting { get; }
    public ITokenizationClient Tokenization { get; }
}

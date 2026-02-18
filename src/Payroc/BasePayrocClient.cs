using Payroc.ApplePaySessions;
using Payroc.Attachments;
using Payroc.Auth;
using Payroc.BankTransferPayments;
using Payroc.Boarding;
using Payroc.CardPayments;
using Payroc.Core;
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

public partial class BasePayrocClient : IBasePayrocClient
{
    private readonly RawClient _client;

    public BasePayrocClient(string apiKey, ClientOptions? clientOptions = null)
    {
        try
        {
            clientOptions ??= new ClientOptions();
            clientOptions.ExceptionHandler = new ExceptionHandler(
                new PayrocExceptionInterceptor(clientOptions)
            );
            var platformHeaders = new Headers(
                new Dictionary<string, string>()
                {
                    { "X-Fern-Language", "C#" },
                    { "X-Fern-SDK-Name", "Payroc" },
                    { "X-Fern-SDK-Version", Version.Current },
                }
            );
            foreach (var header in platformHeaders)
            {
                if (!clientOptions.Headers.ContainsKey(header.Key))
                {
                    clientOptions.Headers[header.Key] = header.Value;
                }
            }
            var clientOptionsWithAuth = clientOptions.Clone();
            var inferredAuthProvider = new InferredAuthTokenProvider(
                apiKey,
                new AuthClient(new RawClient(clientOptions))
            );
            clientOptionsWithAuth.Headers["Authorization"] =
                new Func<global::System.Threading.Tasks.ValueTask<string>>(async () =>
                    (await inferredAuthProvider.GetAuthHeadersAsync().ConfigureAwait(false))
                        .First()
                        .Value
                );
            _client = new RawClient(clientOptionsWithAuth);
            PaymentLinks = new PaymentLinksClient(_client);
            HostedFields = new HostedFieldsClient(_client);
            ApplePaySessions = new ApplePaySessionsClient(_client);
            Attachments = new AttachmentsClient(_client);
            Auth = new AuthClient(_client);
            Funding = new FundingClient(_client);
            BankTransferPayments = new BankTransferPaymentsClient(_client);
            Boarding = new BoardingClient(_client);
            CardPayments = new CardPaymentsClient(_client);
            Notifications = new NotificationsClient(_client);
            PaymentFeatures = new PaymentFeaturesClient(_client);
            PayrocCloud = new PayrocCloudClient(_client);
            RepeatPayments = new RepeatPaymentsClient(_client);
            Reporting = new ReportingClient(_client);
            Tokenization = new TokenizationClient(_client);
        }
        catch (Exception ex)
        {
            var interceptor = new PayrocExceptionInterceptor(clientOptions);
            interceptor.Intercept(ex);
            throw;
        }
    }

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

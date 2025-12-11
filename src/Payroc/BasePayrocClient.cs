using Payroc.ApplePaySessions;
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

public partial class BasePayrocClient
{
    private readonly RawClient _client;

    public BasePayrocClient(
        string clientId,
        string clientSecret,
        ClientOptions? clientOptions = null
    )
    {
        try
        {
            var defaultHeaders = new Headers(
                new Dictionary<string, string>()
                {
                    { "X-Fern-Language", "C#" },
                    { "X-Fern-SDK-Name", "Payroc" },
                    { "X-Fern-SDK-Version", Version.Current },
                }
            );
            clientOptions ??= new ClientOptions();
            clientOptions.ExceptionHandler = new ExceptionHandler(new PayrocExceptionInterceptor(clientOptions));
            foreach (var header in defaultHeaders)
            {
                if (!clientOptions.Headers.ContainsKey(header.Key))
                {
                    clientOptions.Headers[header.Key] = header.Value;
                }
            }
            var tokenProvider = new OAuthTokenProvider(
                clientId,
                clientSecret,
                new AuthClient(new RawClient(clientOptions.Clone()))
            );
            clientOptions.Headers["Authorization"] = new Func<string>(() =>
                tokenProvider.GetAccessTokenAsync().Result
            );
            _client = new RawClient(clientOptions);
            PaymentLinks = new PaymentLinksClient(_client);
            HostedFields = new HostedFieldsClient(_client);
            ApplePaySessions = new ApplePaySessionsClient(_client);
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
            var interceptor = new PayrocExceptionInterceptor(clientOptions ?? new ClientOptions());
            interceptor.Intercept(ex);
            throw;
        }
    }

    public PaymentLinksClient PaymentLinks { get; }

    public HostedFieldsClient HostedFields { get; }

    public ApplePaySessionsClient ApplePaySessions { get; }

    public AuthClient Auth { get; }

    public FundingClient Funding { get; }

    public BankTransferPaymentsClient BankTransferPayments { get; }

    public BoardingClient Boarding { get; }

    public CardPaymentsClient CardPayments { get; }

    public NotificationsClient Notifications { get; }

    public PaymentFeaturesClient PaymentFeatures { get; }

    public PayrocCloudClient PayrocCloud { get; }

    public RepeatPaymentsClient RepeatPayments { get; }

    public ReportingClient Reporting { get; }

    public TokenizationClient Tokenization { get; }
}

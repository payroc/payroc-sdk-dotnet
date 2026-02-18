namespace Payroc.Core;

public class PayrocExceptionInterceptor : IExceptionInterceptor
{
    private readonly ISentryClient? _sentry;

    private static readonly string[] SensitivePatterns =
    [
        "bearer ",
        "authorization:",
        "api_key=",
        "api-key=",
        "apikey=",
        "api_key:",
        "api-key:",
        "apikey:",
        "password=",
        "password:",
        "token=",
        "token:",
    ];

    public PayrocExceptionInterceptor(ClientOptions? clientOptions)
    {
        clientOptions ??= new ClientOptions();

        if (!clientOptions.Telemetry)
        {
            return;
        }

        var options = new SentryOptions
        {
            Dsn =  "https://c3d832677ad08b915dcc3fdafc8afe26@o4505201678483456.ingest.us.sentry.io/4509367402954752",
            IsGlobalModeEnabled = false,
#if DEBUG
            Debug = true,
#endif
        };

        options.SetBeforeSend((sentryEvent, _) => ScrubSensitiveData(sentryEvent));

        _sentry = new SentryClient(options);
    }

    private static SentryEvent ScrubSensitiveData(SentryEvent @event)
    {
        if (@event.User != null)
        {
            @event.User.Email = null;
            @event.User.IpAddress = null;
        }

        if (@event.SentryExceptions != null)
        {
            foreach (var exception in @event.SentryExceptions)
            {
                if (!string.IsNullOrEmpty(exception.Value))
                {
                    exception.Value = ScrubString(exception.Value!);
                }
            }
        }

        // Note: Sentry's integrations automatically remove sensitive headers like
        // Authorization and Cookie when send-default-pii is disabled (which is the default).
        // We don't need to use reflection to modify these - Sentry handles it at the integration level.

        return @event;
    }

    private static string ScrubString(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        var result = input;

        foreach (var pattern in SensitivePatterns)
        {
            var searchStart = 0;

            while (searchStart < result.Length)
            {
                var index = result.IndexOf(pattern, searchStart, StringComparison.OrdinalIgnoreCase);
                if (index == -1)
                {
                    break;
                }

                var valueStart = index + pattern.Length;

                // Skip whitespace
                while (valueStart < result.Length && result[valueStart] == ' ')
                {
                    valueStart++;
                }

                // Find end of value
                var valueEnd = valueStart;
                while (valueEnd < result.Length)
                {
                    var ch = result[valueEnd];
                    if (ch == ' ' || ch == ',' || ch == '&' || ch == '\n' || ch == '\r' || ch == '\t')
                    {
                        break;
                    }
                    valueEnd++;
                }

                // Replace value with [REDACTED]
                result = result[..valueStart] + "[REDACTED]" + result[valueEnd..];
                searchStart = valueStart + 10; // Length of "[REDACTED]"
            }
        }

        return result;
    }

    public Exception Intercept(Exception exception)
    {
        _sentry?.CaptureException(exception);
        return exception;
    }
}


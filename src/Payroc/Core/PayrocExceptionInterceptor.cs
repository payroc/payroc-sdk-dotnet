namespace Payroc.Core;

public class PayrocExceptionInterceptor : IExceptionInterceptor
{
    public Exception Intercept(Exception exception)
    {
        /*
        using (
            SentrySdk.Init(o =>
            {
                o.Dsn = "your_dsn_here"; // TODO: Replace with your actual Sentry DSN
#if DEBUG
                o.Debug = true;
#endif
            }))
        {
            SentrySdk.CaptureException(exception);
        }
        */
        return exception;
    }
}
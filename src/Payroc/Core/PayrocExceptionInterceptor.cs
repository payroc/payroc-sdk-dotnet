namespace Payroc.Core;

public class PayrocExceptionInterceptor : IExceptionInterceptor
{
    public Exception Intercept(Exception exception)
    {
        using (
            SentrySdk.Init(o =>
            {
                o.Dsn = "https://c3d832677ad08b915dcc3fdafc8afe26@o4505201678483456.ingest.us.sentry.io/4509367402954752";
#if DEBUG
                o.Debug = true;
#endif
            }))
        {
            SentrySdk.CaptureException(exception);
        }

        return exception;
    }
}
namespace Payroc;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class NotAcceptableError(FourHundredSix body)
    : PayrocApiException("NotAcceptableError", 406, body)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new FourHundredSix Body => body;
}

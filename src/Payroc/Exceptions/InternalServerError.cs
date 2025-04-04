namespace Payroc;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class InternalServerError(FiveHundred body)
    : PayrocApiException("InternalServerError", 500, body)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new FiveHundred Body => body;
}

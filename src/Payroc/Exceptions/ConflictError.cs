namespace Payroc;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class ConflictError(FourHundredNine body) : PayrocApiException("ConflictError", 409, body)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new FourHundredNine Body => body;
}

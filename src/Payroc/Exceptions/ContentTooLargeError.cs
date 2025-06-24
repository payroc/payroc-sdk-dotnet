namespace Payroc;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
[Serializable]
public class ContentTooLargeError(FourHundredThirteen body)
    : PayrocApiException("ContentTooLargeError", 413, body)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new FourHundredThirteen Body => body;
}

namespace Payroc;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
[Serializable]
public class ForbiddenError(object body) : PayrocApiException("ForbiddenError", 403, body)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new object Body => body;
}

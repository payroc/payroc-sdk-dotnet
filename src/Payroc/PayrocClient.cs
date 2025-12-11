using Payroc.Core;

namespace Payroc;

public class PayrocClient(string? apiKey = null, ClientOptions? clientOptions = null)
    : BasePayrocClient(apiKey, string.Empty, CreateClientOptions(clientOptions))
{
    private static ClientOptions CreateClientOptions(ClientOptions? clientOptions)
    {
        clientOptions ??= new ClientOptions();
        clientOptions.ExceptionHandler = new ExceptionHandler(new PayrocExceptionInterceptor(clientOptions));
        return clientOptions;
    }
}

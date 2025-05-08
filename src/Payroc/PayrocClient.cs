using Payroc.Core;

namespace Payroc;

public class PayrocClient(string? apiKey = null, ClientOptions? clientOptions = null)
    : BasePayrocClient(apiKey, CreateClientOptions(clientOptions))
{
    private static ClientOptions CreateClientOptions(ClientOptions? clientOptions)
    {
        clientOptions ??= new ClientOptions();
        clientOptions.ExceptionHandler = new ExceptionHandler(new PayrocExceptionInterceptor());
        return clientOptions;
    }
}

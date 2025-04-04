using Payroc.Core;

namespace Payroc;

public class PayrocClient(string? token = null, ClientOptions? clientOptions = null)
    : BasePayrocClient(token, CreateClientOptions(clientOptions))
{
    private static ClientOptions CreateClientOptions(ClientOptions? clientOptions)
    {
        clientOptions ??= new ClientOptions();
        clientOptions.ExceptionHandler = new ExceptionHandler(new PayrocExceptionInterceptor());
        return clientOptions;
    }
}

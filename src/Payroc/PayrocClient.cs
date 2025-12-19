using Payroc.Core;

namespace Payroc;

public class PayrocClient(string apiKey, ClientOptions? clientOptions = null)
    : BasePayrocClient(apiKey, CreateClientOptions(clientOptions))
{
    private static ClientOptions CreateClientOptions(ClientOptions? clientOptions)
    {
        clientOptions ??= new ClientOptions();
        clientOptions.ExceptionHandler = new ExceptionHandler(new PayrocExceptionInterceptor(clientOptions));
        return clientOptions;
    }
}

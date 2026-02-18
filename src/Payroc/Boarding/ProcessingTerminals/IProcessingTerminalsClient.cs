using Payroc;

namespace Payroc.Boarding.ProcessingTerminals;

public partial interface IProcessingTerminalsClient
{
    /// <summary>
    /// **Important:** You can retrieve a processing terminal only if the terminal order was created using the Payroc API.
    ///
    /// Use this method to retrieve information about a processing terminal.
    ///
    /// To retrieve a processing terminal, you need its processingTerminalId. Our gateway returned the processingTerminalId in the response of the [Create Terminal Order](https://docs.payroc.com/api/schema/boarding/processing-accounts/create-terminal-order) method.
    ///
    /// **Note:** If you don't have the processingTerminalId, use our [Retrieve Terminal Order](https://docs.payroc.com/api/schema/boarding/terminal-orders/retrieve) method or our [List Processing Terminals](https://docs.payroc.com/api/schema/boarding/processing-accounts/list-processing-terminals) method to search for the processing terminal.
    ///
    /// Our gateway returns the following information about the processing terminal:
    ///
    /// - Status indicating whether the terminal is active or inactive.
    /// - Configuration settings, including gateway settings and application settings.
    /// - Features, receipt settings, and security settings.
    /// - Devices that use the processing terminal's configuration.
    /// </summary>
    WithRawResponseTask<ProcessingTerminal> RetrieveAsync(
        RetrieveProcessingTerminalsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve the host processor configuration of a processing terminal. Integrate with this method only if you use your own gateway and want to validate the processor configuration.
    ///
    /// Our gateway returns the configuration settings for the merchant and the payment terminal.
    /// </summary>
    WithRawResponseTask<HostConfiguration> RetrieveHostConfigurationAsync(
        RetrieveHostConfigurationProcessingTerminalsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

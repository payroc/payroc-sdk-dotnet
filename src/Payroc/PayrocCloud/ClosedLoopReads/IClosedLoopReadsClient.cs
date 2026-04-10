using Payroc;

namespace Payroc.PayrocCloud.ClosedLoopReads;

public partial interface IClosedLoopReadsClient
{
    /// <summary>
    /// Use this method to retrieve information that a payment device captured from a closed-loop card.
    ///
    /// A closed-loop card is a type of card that a customer can use only with a specific merchant. Each time a payment device captures information from a closed-loop card, we store the information as a closed-loop read.
    ///
    /// Our gateway returns the following information from a closed-loop read:
    /// -	Date that the payment device captured the information.
    /// -	Unstructured payload from the card.
    /// </summary>
    WithRawResponseTask<ClosedLoopResponse> RetrieveAsync(
        RetrieveClosedLoopReadsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

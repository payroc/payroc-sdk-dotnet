using Payroc;

namespace Payroc.PayrocCloud.Signatures;

public partial interface ISignaturesClient
{
    /// <summary>
    /// Use this method to retrieve a signature that a payment device captured using Payroc Cloud.
    ///
    /// Our gateway returns the following information about the signature:
    /// - Image of the signature
    /// - Format of the image
    /// - Date that the device captured the image
    /// </summary>
    WithRawResponseTask<RetrieveSignaturesResponse> RetrieveAsync(
        RetrieveSignaturesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

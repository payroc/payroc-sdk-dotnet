using Payroc;

namespace Payroc.Attachments;

public partial interface IAttachmentsClient
{
    /// <summary>
    /// &gt; Before you upload an attachment, make sure that you follow local privacy regulations and get the merchant's consent to process their information.
    ///
    /// **Note:** You need the ID of the processing account before you can upload an attachment. If you don't know the processingAccountId, go to the [Retrieve a Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/retrieve) method.
    ///
    /// The attachment must be an uncompressed file under 30MB in one of the following formats:
    /// - .bmp, csv, .doc, .docx, .gif, .htm, .html, .jpg, .jpeg, .msg, .pdf, .png, .ppt, .pptx, .tif, .tiff, .txt, .xls, .xlsx
    ///
    /// In the request, include the attachment that you want to upload and the following information about the attachment:
    /// - **type** - Type of attachment that you want to upload.
    /// - **description** - Short description of the attachment.
    ///
    /// In the response, our gateway returns information about the attachment including its upload status and an attachmentId that you can use to [Retrieve the details of the Attachment](https://docs.payroc.com/api/schema/attachments/get-attachment).
    /// </summary>
    WithRawResponseTask<Attachment> UploadToProcessingAccountAsync(
        UploadAttachment request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve the details of an attachment.
    ///
    /// To retrieve the details of an attachment you need its attachmentId. Our gateway returned the attachmentId in the response of the method that you used to upload the attachment.
    ///
    /// Our gateway returns information about the attachment, including its upload status and the entity that the attachment is linked to. Our gateway doesn't return the file that you uploaded.
    /// </summary>
    WithRawResponseTask<Attachment> GetAttachmentAsync(
        GetAttachmentRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}

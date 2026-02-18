using System.Text.Json;
using Payroc;
using Payroc.Core;

namespace Payroc.Attachments;

public partial class AttachmentsClient : IAttachmentsClient
{
    private RawClient _client;

    internal AttachmentsClient(RawClient client)
    {
        try
        {
            _client = client;
        }
        catch (Exception ex)
        {
            client.Options.ExceptionHandler?.CaptureException(ex);
            throw;
        }
    }

    private async Task<WithRawResponse<Attachment>> UploadToProcessingAccountAsyncCore(
        UploadAttachment request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _headers = await new Payroc.Core.HeadersBuilder.Builder()
                    .Add("Idempotency-Key", request.IdempotencyKey)
                    .Add(_client.Options.Headers)
                    .Add(_client.Options.AdditionalHeaders)
                    .Add(options?.AdditionalHeaders)
                    .BuildAsync()
                    .ConfigureAwait(false);
                var multipartFormRequest_ = new MultipartFormRequest
                {
                    BaseUrl = _client.Options.Environment.Api,
                    Method = HttpMethod.Post,
                    Path = string.Format(
                        "processing-accounts/{0}/attachments",
                        ValueConvert.ToPathParameterString(request.ProcessingAccountId)
                    ),
                    Headers = _headers,
                    Options = options,
                };
                multipartFormRequest_.AddJsonPart("attachment", request.Attachment);
                multipartFormRequest_.AddFileParameterPart("file", request.File);
                var response = await _client
                    .SendRequestAsync(multipartFormRequest_, cancellationToken)
                    .ConfigureAwait(false);
                if (response.StatusCode is >= 200 and < 400)
                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        var responseData = JsonUtils.Deserialize<Attachment>(responseBody)!;
                        return new WithRawResponse<Attachment>()
                        {
                            Data = responseData,
                            RawResponse = new RawResponse()
                            {
                                StatusCode = response.Raw.StatusCode,
                                Url =
                                    response.Raw.RequestMessage?.RequestUri
                                    ?? new Uri("about:blank"),
                                Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                            },
                        };
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocApiException(
                            "Failed to deserialize response",
                            response.StatusCode,
                            responseBody,
                            e
                        );
                    }
                }
                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        switch (response.StatusCode)
                        {
                            case 400:
                                throw new BadRequestError(
                                    JsonUtils.Deserialize<FourHundred>(responseBody)
                                );
                            case 401:
                                throw new UnauthorizedError(
                                    JsonUtils.Deserialize<FourHundredOne>(responseBody)
                                );
                            case 403:
                                throw new ForbiddenError(
                                    JsonUtils.Deserialize<object>(responseBody)
                                );
                            case 404:
                                throw new NotFoundError(
                                    JsonUtils.Deserialize<FourHundredFour>(responseBody)
                                );
                            case 406:
                                throw new NotAcceptableError(
                                    JsonUtils.Deserialize<FourHundredSix>(responseBody)
                                );
                            case 409:
                                throw new ConflictError(
                                    JsonUtils.Deserialize<FourHundredNine>(responseBody)
                                );
                            case 413:
                                throw new ContentTooLargeError(
                                    JsonUtils.Deserialize<FourHundredThirteen>(responseBody)
                                );
                            case 415:
                                throw new UnsupportedMediaTypeError(
                                    JsonUtils.Deserialize<FourHundredFifteen>(responseBody)
                                );
                            case 500:
                                throw new InternalServerError(
                                    JsonUtils.Deserialize<FiveHundred>(responseBody)
                                );
                        }
                    }
                    catch (JsonException)
                    {
                        // unable to map error response, throwing generic error
                    }
                    throw new PayrocApiException(
                        $"Error with status code {response.StatusCode}",
                        response.StatusCode,
                        responseBody
                    );
                }
            })
            .ConfigureAwait(false);
    }

    private async Task<WithRawResponse<Attachment>> GetAttachmentAsyncCore(
        GetAttachmentRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _headers = await new Payroc.Core.HeadersBuilder.Builder()
                    .Add(_client.Options.Headers)
                    .Add(_client.Options.AdditionalHeaders)
                    .Add(options?.AdditionalHeaders)
                    .BuildAsync()
                    .ConfigureAwait(false);
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Get,
                            Path = string.Format(
                                "attachments/{0}",
                                ValueConvert.ToPathParameterString(request.AttachmentId)
                            ),
                            Headers = _headers,
                            Options = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
                if (response.StatusCode is >= 200 and < 400)
                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        var responseData = JsonUtils.Deserialize<Attachment>(responseBody)!;
                        return new WithRawResponse<Attachment>()
                        {
                            Data = responseData,
                            RawResponse = new RawResponse()
                            {
                                StatusCode = response.Raw.StatusCode,
                                Url =
                                    response.Raw.RequestMessage?.RequestUri
                                    ?? new Uri("about:blank"),
                                Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                            },
                        };
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocApiException(
                            "Failed to deserialize response",
                            response.StatusCode,
                            responseBody,
                            e
                        );
                    }
                }
                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        switch (response.StatusCode)
                        {
                            case 400:
                                throw new BadRequestError(
                                    JsonUtils.Deserialize<FourHundred>(responseBody)
                                );
                            case 401:
                                throw new UnauthorizedError(
                                    JsonUtils.Deserialize<FourHundredOne>(responseBody)
                                );
                            case 403:
                                throw new ForbiddenError(
                                    JsonUtils.Deserialize<object>(responseBody)
                                );
                            case 404:
                                throw new NotFoundError(
                                    JsonUtils.Deserialize<FourHundredFour>(responseBody)
                                );
                            case 406:
                                throw new NotAcceptableError(
                                    JsonUtils.Deserialize<FourHundredSix>(responseBody)
                                );
                            case 500:
                                throw new InternalServerError(
                                    JsonUtils.Deserialize<FiveHundred>(responseBody)
                                );
                        }
                    }
                    catch (JsonException)
                    {
                        // unable to map error response, throwing generic error
                    }
                    throw new PayrocApiException(
                        $"Error with status code {response.StatusCode}",
                        response.StatusCode,
                        responseBody
                    );
                }
            })
            .ConfigureAwait(false);
    }

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
    /// <example><code>
    /// await client.Attachments.UploadToProcessingAccountAsync(
    ///     new UploadAttachment
    ///     {
    ///         ProcessingAccountId = "38765",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Attachment = new UploadToProcessingAccountAttachmentsRequestAttachment
    ///         {
    ///             Type = UploadToProcessingAccountAttachmentsRequestAttachmentType.BankingEvidence,
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<Attachment> UploadToProcessingAccountAsync(
        UploadAttachment request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<Attachment>(
            UploadToProcessingAccountAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Use this method to retrieve the details of an attachment.
    ///
    /// To retrieve the details of an attachment you need its attachmentId. Our gateway returned the attachmentId in the response of the method that you used to upload the attachment.
    ///
    /// Our gateway returns information about the attachment, including its upload status and the entity that the attachment is linked to. Our gateway doesn't return the file that you uploaded.
    /// </summary>
    /// <example><code>
    /// await client.Attachments.GetAttachmentAsync(new GetAttachmentRequest { AttachmentId = "12876" });
    /// </code></example>
    public WithRawResponseTask<Attachment> GetAttachmentAsync(
        GetAttachmentRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<Attachment>(
            GetAttachmentAsyncCore(request, options, cancellationToken)
        );
    }
}

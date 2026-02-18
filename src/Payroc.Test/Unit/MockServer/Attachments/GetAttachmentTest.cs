using NUnit.Framework;
using Payroc.Attachments;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Attachments;

[TestFixture]
public class GetAttachmentTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "attachmentId": "15387",
              "type": "personalIdentification",
              "uploadStatus": "accepted",
              "fileName": "oliviaDoePassport.pdf",
              "contentType": "application/pdf",
              "description": "Passport for Olivia Doe",
              "entity": {
                "type": "processingAccount",
                "id": "2585"
              },
              "createdDate": "2025-09-18T10:19:18.000Z",
              "lastModifiedDate": "2025-09-18T10:19:18.000Z",
              "metadata": {
                "passportId": "123456789"
              }
            }
            """;

        Server
            .Given(
                WireMock.RequestBuilders.Request.Create().WithPath("/attachments/12876").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Attachments.GetAttachmentAsync(
            new GetAttachmentRequest { AttachmentId = "12876" }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}

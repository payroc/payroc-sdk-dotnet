using NUnit.Framework;
using Payroc;
using Payroc.Boarding.Contacts;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.Contacts;

[TestFixture]
public class UpdateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public void MockServerTest()
    {
        const string requestJson = """
            {
              "type": "manager",
              "firstName": "Jane",
              "middleName": "Helen",
              "lastName": "Doe",
              "identifiers": [
                {
                  "type": "nationalId",
                  "value": "000-00-4320"
                }
              ],
              "contactMethods": [
                {
                  "type": "email",
                  "value": "jane.doe@example.com"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/contacts/1")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPut()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.Boarding.Contacts.UpdateAsync(
                new UpdateContactsRequest
                {
                    ContactId = 1,
                    Body = new Contact
                    {
                        Type = ContactType.Manager,
                        FirstName = "Jane",
                        MiddleName = "Helen",
                        LastName = "Doe",
                        Identifiers = new List<Identifier>()
                        {
                            new Identifier
                            {
                                Type = IdentifierType.NationalId,
                                Value = "000-00-4320",
                            },
                        },
                        ContactMethods = new List<ContactMethod>()
                        {
                            new ContactMethod(
                                new ContactMethod.Email(
                                    new ContactMethodEmail { Value = "jane.doe@example.com" }
                                )
                            ),
                        },
                    },
                }
            )
        );
    }
}

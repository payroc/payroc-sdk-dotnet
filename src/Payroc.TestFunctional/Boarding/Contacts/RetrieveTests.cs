using Payroc.Boarding.Contacts;

namespace Payroc.TestFunctional.Boarding.Contacts;

[TestFixture, Category("Boarding.Contacts")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var contactsRequest = new RetrieveContactsRequest
        {
            ContactId = int.Parse(BoardingTestFixture.SharedContactId)
        };
        
        var contactsResponse = await client.Boarding.Contacts.RetrieveAsync(contactsRequest);
        
        Assert.That(contactsResponse.ContactId?.ToString(), Is.EqualTo(BoardingTestFixture.SharedContactId));
        Assert.That(contactsResponse.FirstName, Is.Not.Null);
        Assert.That(contactsResponse.LastName, Is.Not.Null);
    }
}
using System.Net;
using NUnit.Framework;
using Payroc.Payments;

namespace Payroc.Test.Integration;

[TestFixture]
public class TestPayments
{
    private PayrocClient _client;

    [SetUp]
    public void Setup()
    {
        Assert.Ignore("Make sure a proxy is running, then ignore this line.");
        const string proxy = "10.59.224.225:9090";
        _client = new PayrocClient("your_token_here", 
            new ClientOptions
            {
                HttpClient = new HttpClient(new SocketsHttpHandler
                {
                    Proxy = new WebProxy(proxy)
                })
            }
        );
    }

    [Test]
    public async Task TestListPaymentsAsync()
    {
        var request = new ListPaymentsRequest
        {
            Limit = 10
        };

        var pager = await _client.Payments.ListAsync(request);
        var firstPage = await pager.GetNextPageAsync();

        Assert.That(firstPage, Is.Not.Null);
        Assert.That(firstPage.Items, Is.Not.Empty);
        Assert.That(pager.HasNextPage, Is.True);

        var secondPage = await pager.GetNextPageAsync();

        Assert.That(secondPage, Is.Not.Null);
        Assert.That(secondPage.Items, Is.Not.Empty);
    }

    [Test]
    public async Task TestPaginateBackwardsAsync()
    {
        var request = new ListPaymentsRequest
        {
            Limit = 10
        };

        var pager = await _client.Payments.ListAsync(request);
        var firstPage = await pager.GetNextPageAsync();
        var secondPage = await pager.GetNextPageAsync();

        Assert.That(secondPage, Is.Not.Null);
        Assert.That(secondPage.Items, Is.Not.Empty);
        Assert.That(pager.HasPreviousPage, Is.True);

        var previousPage = await pager.GetPreviousPageAsync();

        Assert.That(previousPage, Is.Not.Null);
        Assert.That(previousPage.Items, Is.Not.Empty);
    }
}
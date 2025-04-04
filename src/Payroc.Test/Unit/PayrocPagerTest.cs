using System.Net;
using NUnit.Framework;
using Payroc.Core;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

// ReSharper disable NullableWarningSuppressionIsUsed

namespace Payroc.Test.Unit;

[TestFixture]
public class PayrocPagerTest
{
    private static readonly HttpClient HttpClient = new();
    private WireMockServer _server;
    private string _baseUrl;

    [SetUp]
    public void Setup()
    {
        _server = WireMockServer.Start();
        _baseUrl = _server.Url ?? throw new InvalidOperationException("Server URL is not available.");
    }

    [TearDown]
    public void TearDown()
    {
        _server.Stop();
        _server.Dispose();
    }

    private static Task<HttpResponseMessage> SendRequest(HttpRequestMessage request,
        CancellationToken cancellationToken = default) => HttpClient.SendAsync(request, cancellationToken);

    [Test]
    public async Task TestInitializeAsync_SetsUpPagerCorrectly()
    {
        _server.Given(Request.Create().WithPath("/items").UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(new
                {
                    data = new[] { "item1", "item2" },
                    links = new[] { new { rel = "next", href = _baseUrl + "/items?page=2" } }
                }));

        var initialRequest = new HttpRequestMessage(HttpMethod.Get, _baseUrl + "/items");

        var pager = await PayrocPagerFactory.CreateAsync<string>(
            SendRequest,
            initialRequest
        );

        Assert.Multiple(() =>
        {
            Assert.That(pager.HasNextPage, Is.True);
            Assert.That(pager.HasPreviousPage, Is.False);
            Assert.That(pager.CurrentPage.Items, Is.EquivalentTo(new[] { "item1", "item2" }));
        });
    }

    [Test]
    public async Task TestGetNextPageAsync_ReturnsNextPage()
    {
        _server.Given(Request.Create().WithPath("/items").UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(new
                {
                    data = new[] { "item1", "item2" },
                    links = new[]
                        { new { rel = "next", href = _baseUrl + "/items?page=2" } }
                }));

        _server.Given(Request.Create().WithPath("/items").WithParam("page", "2").UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(new
                {
                    data = new[] { "item3", "item4" },
                    links = new[]
                        { new { rel = "previous", href = _baseUrl + "/items?page=1" } }
                }));

        var initialRequest = new HttpRequestMessage(HttpMethod.Get, _baseUrl + "/items");

        var pager = await PayrocPagerFactory.CreateAsync<string>(
            SendRequest,
            initialRequest
        );
        var nextPage = await pager.GetNextPageAsync();

        Assert.Multiple(() =>
        {
            Assert.That(nextPage.Items, Is.EquivalentTo(new[] { "item3", "item4" }));
            Assert.That(pager.HasNextPage, Is.False);
            Assert.That(pager.HasPreviousPage, Is.True);
        });
    }

    [Test]
    public async Task TestGetPreviousPageAsync_ReturnsPreviousPage()
    {
        _server.Given(Request.Create().WithPath("/items").UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(new
                {
                    data = new[] { "item1", "item2" },
                    links = new[]
                        { new { rel = "next", href = _baseUrl + "/items?page=2" } }
                }));

        _server.Given(Request.Create().WithPath("/items").WithParam("page", "2").UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(new
                {
                    data = new[] { "item3", "item4" },
                    links = new[]
                        { new { rel = "previous", href = _baseUrl + "/items?page=1" } }
                }));

        var initialRequest = new HttpRequestMessage(HttpMethod.Get, _baseUrl + "/items");

        var pager = await PayrocPagerFactory.CreateAsync<string>(
            SendRequest,
            initialRequest
        );
        await pager.GetNextPageAsync();
        var previousPage = await pager.GetPreviousPageAsync();

        Assert.Multiple(() =>
        {
            Assert.That(previousPage.Items, Is.EquivalentTo(new[] { "item1", "item2" }));
            Assert.That(pager.HasNextPage, Is.True);
            Assert.That(pager.HasPreviousPage, Is.False);
        });
    }

    [Test]
    public async Task TestAsyncEnumerator_WorksCorrectly()
    {
        _server.Given(Request.Create().WithPath("/items").UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(new
                {
                    data = new[] { "item1", "item2" },
                    links = new[] { new { rel = "next", href = _baseUrl + "/items?page=2" } }
                }));

        _server.Given(Request.Create().WithPath("/items").WithParam("page", "2").UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(new
                {
                    data = new[] { "item3", "item4" },
                    links = new[] { new { rel = "previous", href = _baseUrl + "/items?page=1" } }
                }));

        var initialRequest = new HttpRequestMessage(HttpMethod.Get, _baseUrl + "/items");
        var pager = await PayrocPagerFactory.CreateAsync<string>(
            SendRequest,
            initialRequest
        );

        var items = new List<string>();
        await foreach (var item in pager)
        {
            items.Add(item);
        }

        Assert.That(items, Is.EquivalentTo(new[] { "item1", "item2", "item3", "item4" }));
    }

    [Test]
    public async Task TestGetNextPagesAsync_WorksCorrectly()
    {
        _server.Given(Request.Create().WithPath("/items").UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(new
                {
                    data = new[] { "item1", "item2" },
                    links = new[] { new { rel = "next", href = _baseUrl + "/items?page=2" } }
                }));

        _server.Given(Request.Create().WithPath("/items").WithParam("page", "2").UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(new
                {
                    data = new[] { "item3", "item4" },
                    links = new[] { new { rel = "previous", href = _baseUrl + "/items?page=1" } }
                }));

        var initialRequest = new HttpRequestMessage(HttpMethod.Get, _baseUrl + "/items");
        var pager = await PayrocPagerFactory.CreateAsync<string>(
            SendRequest,
            initialRequest
        );

        var pages = new List<Page<string>>();
        await foreach (var page in pager.GetNextPagesAsync())
        {
            pages.Add(page);
        }

        Assert.That(pages, Has.Count.EqualTo(1));
        Assert.That(pages[0].Items, Is.EquivalentTo(new[] { "item3", "item4" }));
    }

    [Test]
    public async Task TestGetPreviousPagesAsync_WorksCorrectly()
    {
        _server.Given(Request.Create().WithPath("/items").UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(new
                {
                    data = new[] { "item1", "item2" },
                    links = new[] { new { rel = "next", href = _baseUrl + "/items?page=2" } }
                }));

        _server.Given(Request.Create().WithPath("/items").WithParam("page", "2").UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(new
                {
                    data = new[] { "item3", "item4" },
                    links = new[] { new { rel = "previous", href = _baseUrl + "/items?page=1" } }
                }));

        var initialRequest = new HttpRequestMessage(HttpMethod.Get, _baseUrl + "/items");
        var pager = await PayrocPagerFactory.CreateAsync<string>(
            SendRequest,
            initialRequest
        );
        await pager.GetNextPageAsync();

        var pages = new List<Page<string>>();
        await foreach (var page in pager.GetPreviousPagesAsync())
        {
            pages.Add(page);
        }

        Assert.That(pages, Has.Count.EqualTo(1));
        Assert.That(pages[0].Items, Is.EquivalentTo(new[] { "item1", "item2" }));
    }
}
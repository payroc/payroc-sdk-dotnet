using System.Net;
using NUnit.Framework;
using Payroc.Core;
using SystemTask = global::System.Threading.Tasks.Task;

namespace Payroc.Test.Core.Pagination;

[TestFixture(Category = "Pagination")]
public class PayrocPagerHeaderPropagationTest
{
    [Test]
    public async SystemTask PayrocPager_PropagatesAuthorizationHeaderToSecondPage()
    {
        var requestCount = 0;
        string? capturedAuthHeader1 = null;
        string? capturedAuthHeader2 = null;
        string? capturedAuthHeader3 = null;

        var initialRequest = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com/page1");
        initialRequest.Headers.Add("Authorization", "Bearer static-token");

        var context = new PayrocPagerContext
        {
            SendRequest = (request, cancellationToken) =>
            {
                requestCount++;
                if (requestCount == 1)
                {
                    capturedAuthHeader1 = request.Headers.Authorization?.ToString();
                }
                else if (requestCount == 2)
                {
                    capturedAuthHeader2 = request.Headers.Authorization?.ToString();
                }
                else if (requestCount == 3)
                {
                    capturedAuthHeader3 = request.Headers.Authorization?.ToString();
                }

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        requestCount switch
                        {
                            1 => """{"data": ["item1", "item2"], "links": [{"rel": "next", "href": "https://api.example.com/page2"}]}""",
                            2 => """{"data": ["item3", "item4"], "links": [{"rel": "next", "href": "https://api.example.com/page3"}]}""",
                            _ => """{"data": ["item5"], "links": []}"""
                        }
                    )
                };
                return SystemTask.FromResult(response);
            },
            InitialHttpRequest = initialRequest,
            ClientOptions = new ClientOptions
            {
                Headers = new Headers { ["Authorization"] = "Bearer static-token" }
            },
            RequestOptions = new RequestOptions()
        };

        var pager = await PayrocPagerFactory.CreateAsync<string>(context);

        Assert.That(pager.HasNextPage, Is.True);
        Assert.That(capturedAuthHeader1, Is.EqualTo("Bearer static-token"));

        await pager.GetNextPageAsync();
        Assert.That(capturedAuthHeader2, Is.EqualTo("Bearer static-token"));

        await pager.GetNextPageAsync();
        Assert.That(capturedAuthHeader3, Is.EqualTo("Bearer static-token"));
    }

    [Test]
    public async SystemTask PayrocPager_RefreshesTokenBetweenPages()
    {
        var requestCount = 0;
        var tokenVersion = 1;
        string? capturedAuthHeader1 = null;
        string? capturedAuthHeader2 = null;
        string? capturedAuthHeader3 = null;

        var initialRequest = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com/page1");
        initialRequest.Headers.Add("Authorization", $"Bearer token-v{tokenVersion}");

        var context = new PayrocPagerContext
        {
            SendRequest = (request, cancellationToken) =>
            {
                requestCount++;
                if (requestCount == 1)
                {
                    capturedAuthHeader1 = request.Headers.Authorization?.ToString();
                    tokenVersion = 2; // Update for next page
                }
                else if (requestCount == 2)
                {
                    capturedAuthHeader2 = request.Headers.Authorization?.ToString();
                }
                else if (requestCount == 3)
                {
                    capturedAuthHeader3 = request.Headers.Authorization?.ToString();
                }

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        requestCount switch
                        {
                            1 => """{"data": ["item1"], "links": [{"rel": "next", "href": "https://api.example.com/page2"}]}""",
                            2 => """{"data": ["item2"], "links": [{"rel": "next", "href": "https://api.example.com/page3"}]}""",
                            _ => """{"data": ["item3"], "links": []}"""
                        }
                    )
                };
                return SystemTask.FromResult(response);
            },
            InitialHttpRequest = initialRequest,
            ClientOptions = new ClientOptions
            {
                Headers = new Headers { ["Authorization"] = new HeaderValue((Func<string>)(() => $"Bearer token-v{tokenVersion}")) }
            },
            RequestOptions = new RequestOptions()
        };

        var pager = await PayrocPagerFactory.CreateAsync<string>(context);

        Assert.That(capturedAuthHeader1, Is.EqualTo("Bearer token-v1"));

        await pager.GetNextPageAsync();
        Assert.That(capturedAuthHeader2, Is.EqualTo("Bearer token-v2"));

        await pager.GetNextPageAsync();
        Assert.That(capturedAuthHeader3, Is.EqualTo("Bearer token-v2"));
    }

    [Test]
    public async SystemTask PayrocPager_IteratesAllPagesWithAuthentication()
    {
        var requestCount = 0;
        var allAuthHeadersPresent = true;

        var initialRequest = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com/page1");
        initialRequest.Headers.Add("Authorization", "Bearer test-token");

        var context = new PayrocPagerContext
        {
            SendRequest = (request, cancellationToken) =>
            {
                requestCount++;
                if (request.Headers.Authorization?.ToString() != "Bearer test-token")
                {
                    allAuthHeadersPresent = false;
                }

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        requestCount switch
                        {
                            1 => """{"data": ["item1", "item2"], "links": [{"rel": "next", "href": "https://api.example.com/page2"}]}""",
                            2 => """{"data": ["item3", "item4"], "links": [{"rel": "next", "href": "https://api.example.com/page3"}]}""",
                            3 => """{"data": ["item5", "item6"], "links": [{"rel": "next", "href": "https://api.example.com/page4"}]}""",
                            _ => """{"data": ["item7"], "links": []}"""
                        }
                    )
                };
                return SystemTask.FromResult(response);
            },
            InitialHttpRequest = initialRequest,
            ClientOptions = new ClientOptions
            {
                Headers = new Headers { ["Authorization"] = "Bearer test-token" }
            },
            RequestOptions = new RequestOptions()
        };

        var pager = await PayrocPagerFactory.CreateAsync<string>(context);

        var itemCount = 0;
        await foreach (var item in pager)
        {
            itemCount++;
        }

        Assert.Multiple(() =>
        {
            Assert.That(requestCount, Is.EqualTo(4));
            Assert.That(itemCount, Is.EqualTo(7));
            Assert.That(allAuthHeadersPresent, Is.True, "Authorization header should be present in all requests");
        });
    }

    [Test]
    public async SystemTask PayrocPager_PropagatesAuthorizationToPreviousPage()
    {
        var requestCount = 0;
        string? capturedAuthHeaderInitial = null;
        string? capturedAuthHeaderPrevious = null;

        var initialRequest = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com/page2");
        initialRequest.Headers.Add("Authorization", "Bearer test-token");

        var context = new PayrocPagerContext
        {
            SendRequest = (request, cancellationToken) =>
            {
                requestCount++;
                if (requestCount == 1)
                {
                    capturedAuthHeaderInitial = request.Headers.Authorization?.ToString();
                }
                else if (requestCount == 2)
                {
                    capturedAuthHeaderPrevious = request.Headers.Authorization?.ToString();
                }

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        requestCount switch
                        {
                            1 => """{"data": ["item3", "item4"], "links": [{"rel": "previous", "href": "https://api.example.com/page1"}]}""",
                            _ => """{"data": ["item1", "item2"], "links": []}"""
                        }
                    )
                };
                return SystemTask.FromResult(response);
            },
            InitialHttpRequest = initialRequest,
            ClientOptions = new ClientOptions
            {
                Headers = new Headers { ["Authorization"] = "Bearer test-token" }
            },
            RequestOptions = new RequestOptions()
        };

        var pager = await PayrocPagerFactory.CreateAsync<string>(context);

        Assert.That(pager.HasPreviousPage, Is.True);
        Assert.That(capturedAuthHeaderInitial, Is.EqualTo("Bearer test-token"));

        await pager.GetPreviousPageAsync();
        Assert.That(capturedAuthHeaderPrevious, Is.EqualTo("Bearer test-token"));
    }

    [Test]
    public async SystemTask PayrocPager_WorksWithoutAuthorizationHeader()
    {
        var requestCount = 0;

        var context = new PayrocPagerContext
        {
            SendRequest = (request, cancellationToken) =>
            {
                requestCount++;
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        requestCount switch
                        {
                            1 => """{"data": ["item1"], "links": [{"rel": "next", "href": "https://api.example.com/page2"}]}""",
                            _ => """{"data": ["item2"], "links": []}"""
                        }
                    )
                };
                return SystemTask.FromResult(response);
            },
            InitialHttpRequest = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com/page1"),
            ClientOptions = new ClientOptions
            {
                Headers = new Headers()
            },
            RequestOptions = new RequestOptions()
        };

        var pager = await PayrocPagerFactory.CreateAsync<string>(context);

        var itemCount = 0;
        await foreach (var item in pager)
        {
            itemCount++;
        }

        Assert.Multiple(() =>
        {
            Assert.That(requestCount, Is.EqualTo(2));
            Assert.That(itemCount, Is.EqualTo(2));
        });
    }

    [Test]
    public async SystemTask PayrocPager_ReplacesAuthorizationHeaderOnEachRequest()
    {
        var requestCount = 0;
        var authHeaderCallCount = 0;

        var context = new PayrocPagerContext
        {
            SendRequest = (request, cancellationToken) =>
            {
                requestCount++;
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        requestCount switch
                        {
                            1 => """{"data": ["item1"], "links": [{"rel": "next", "href": "https://api.example.com/page2"}]}""",
                            2 => """{"data": ["item2"], "links": [{"rel": "next", "href": "https://api.example.com/page3"}]}""",
                            _ => """{"data": ["item3"], "links": []}"""
                        }
                    )
                };
                return SystemTask.FromResult(response);
            },
            InitialHttpRequest = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com/page1"),
            ClientOptions = new ClientOptions
            {
                Headers = new Headers
                {
                    ["Authorization"] = new HeaderValue((Func<string>)(() =>
                    {
                        authHeaderCallCount++;
                        return $"Bearer token-call-{authHeaderCallCount}";
                    }))
                }
            },
            RequestOptions = new RequestOptions()
        };

        var pager = await PayrocPagerFactory.CreateAsync<string>(context);

        await pager.GetNextPageAsync();
        await pager.GetNextPageAsync();

        Assert.That(authHeaderCallCount, Is.EqualTo(2), "Authorization header function should be called for each subsequent page request");
    }
}

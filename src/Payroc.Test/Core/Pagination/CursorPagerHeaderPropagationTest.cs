using NUnit.Framework;
using Payroc.Core;
using SystemTask = global::System.Threading.Tasks.Task;

namespace Payroc.Test.Core.Pagination;

[TestFixture(Category = "Pagination")]
public class CursorPagerHeaderPropagationTest
{
    [Test]
    public async SystemTask CursorPager_PropagatesRequestOptionsToAllPages()
    {
        var requestCount = 0;
        var capturedOptions = new List<TestRequestOptions?>();

        var testOptions = new TestRequestOptions
        {
            AuthToken = "bearer-token-xyz",
            CorrelationId = "correlation-123"
        };

        var responses = new List<Response>
        {
            new()
            {
                Data = new Data { Items = ["item1", "item2"] },
                Cursor = new Cursor { Next = "cursor2" },
            },
            new()
            {
                Data = new Data { Items = ["item3", "item4"] },
                Cursor = new Cursor { Next = "cursor3" },
            },
            new()
            {
                Data = new Data { Items = ["item5"] },
                Cursor = new Cursor { Next = null },
            },
        }.GetEnumerator();

        Pager<object> pager = await CursorPager<
            Request,
            TestRequestOptions,
            Response,
            string,
            object
        >.CreateInstanceAsync(
            new Request { Cursor = null },
            testOptions,
            (request, options, _) =>
            {
                requestCount++;
                capturedOptions.Add(options);
                responses.MoveNext();
                return SystemTask.FromResult(responses.Current);
            },
            (request, cursor) => request.Cursor = cursor,
            response => response?.Cursor?.Next,
            response => response?.Data?.Items?.ToList()
        );

        var itemCount = 0;
        await foreach (var page in pager.AsPagesAsync())
        {
            itemCount += page.Items.Count;
        }

        Assert.Multiple(() =>
        {
            Assert.That(requestCount, Is.EqualTo(3));
            Assert.That(itemCount, Is.EqualTo(5));
            Assert.That(capturedOptions, Has.Count.EqualTo(3));
            
            foreach (var options in capturedOptions)
            {
                Assert.That(options, Is.Not.Null);
                Assert.That(options!.AuthToken, Is.EqualTo("bearer-token-xyz"));
                Assert.That(options.CorrelationId, Is.EqualTo("correlation-123"));
            }
        });
    }

    [Test]
    public async SystemTask CursorPager_SendRequestDelegateCanAccessOptionsForHeaderEvaluation()
    {
        var requestCount = 0;
        var authHeadersFromOptions = new List<string>();

        var testOptions = new TestRequestOptions
        {
            AuthToken = "dynamic-auth-token"
        };

        var responses = new List<Response>
        {
            new()
            {
                Data = new Data { Items = ["item1"] },
                Cursor = new Cursor { Next = "cursor2" },
            },
            new()
            {
                Data = new Data { Items = ["item2"] },
                Cursor = new Cursor { Next = "cursor3" },
            },
            new()
            {
                Data = new Data { Items = ["item3"] },
                Cursor = new Cursor { Next = null },
            },
        }.GetEnumerator();

        Pager<object> pager = await CursorPager<
            Request,
            TestRequestOptions,
            Response,
            string,
            object
        >.CreateInstanceAsync(
            new Request { Cursor = null },
            testOptions,
            (request, options, _) =>
            {
                requestCount++;
                if (options != null)
                {
                    authHeadersFromOptions.Add($"Bearer {options.AuthToken}");
                }
                responses.MoveNext();
                return SystemTask.FromResult(responses.Current);
            },
            (request, cursor) => request.Cursor = cursor,
            response => response?.Cursor?.Next,
            response => response?.Data?.Items?.ToList()
        );

        await foreach (var _ in pager.AsPagesAsync())
        {
        }

        Assert.Multiple(() =>
        {
            Assert.That(requestCount, Is.EqualTo(3));
            Assert.That(authHeadersFromOptions, Has.Count.EqualTo(3));
            Assert.That(authHeadersFromOptions, Has.All.EqualTo("Bearer dynamic-auth-token"));
        });
    }

    [Test]
    public async SystemTask CursorPager_WorksWithNullOptions()
    {
        var requestCount = 0;

        var responses = new List<Response>
        {
            new()
            {
                Data = new Data { Items = ["item1"] },
                Cursor = new Cursor { Next = "cursor2" },
            },
            new()
            {
                Data = new Data { Items = ["item2"] },
                Cursor = new Cursor { Next = null },
            },
        }.GetEnumerator();

        Pager<object> pager = await CursorPager<
            Request,
            TestRequestOptions?,
            Response,
            string,
            object
        >.CreateInstanceAsync(
            new Request { Cursor = null },
            null,
            (request, options, _) =>
            {
                requestCount++;
                Assert.That(options, Is.Null);
                responses.MoveNext();
                return SystemTask.FromResult(responses.Current);
            },
            (request, cursor) => request.Cursor = cursor,
            response => response?.Cursor?.Next,
            response => response?.Data?.Items?.ToList()
        );

        var itemCount = 0;
        await foreach (var page in pager.AsPagesAsync())
        {
            itemCount += page.Items.Count;
        }

        Assert.Multiple(() =>
        {
            Assert.That(requestCount, Is.EqualTo(2));
            Assert.That(itemCount, Is.EqualTo(2));
        });
    }

    [Test]
    public async SystemTask CursorPager_OptionsAvailableInGetNextPageAsync()
    {
        var capturedOptionsInPages = new List<string?>();

        var testOptions = new TestRequestOptions
        {
            CorrelationId = "correlation-xyz"
        };

        var responses = new List<Response>
        {
            new()
            {
                Data = new Data { Items = ["item1"] },
                Cursor = new Cursor { Next = "cursor2" },
            },
            new()
            {
                Data = new Data { Items = ["item2"] },
                Cursor = new Cursor { Next = "cursor3" },
            },
            new()
            {
                Data = new Data { Items = ["item3"] },
                Cursor = new Cursor { Next = null },
            },
        }.GetEnumerator();

        Pager<object> pager = await CursorPager<
            Request,
            TestRequestOptions,
            Response,
            string,
            object
        >.CreateInstanceAsync(
            new Request { Cursor = null },
            testOptions,
            (request, options, _) =>
            {
                capturedOptionsInPages.Add(options?.CorrelationId);
                responses.MoveNext();
                return SystemTask.FromResult(responses.Current);
            },
            (request, cursor) => request.Cursor = cursor,
            response => response?.Cursor?.Next,
            response => response?.Data?.Items?.ToList()
        );

        Assert.That(pager.HasNextPage, Is.True);

        await pager.GetNextPageAsync();
        await pager.GetNextPageAsync();

        Assert.Multiple(() =>
        {
            Assert.That(capturedOptionsInPages, Has.Count.EqualTo(3));
            Assert.That(capturedOptionsInPages, Has.All.EqualTo("correlation-xyz"));
        });
    }

    [Test]
    public async SystemTask CursorPager_PropagatesCustomHeadersAcrossMultiplePages()
    {
        var requestCount = 0;
        var idempotencyKeys = new List<string>();
        var apiVersions = new List<string>();

        var testOptions = new TestRequestOptions
        {
            IdempotencyKey = "idempotent-key-abc",
            ApiVersion = "v2"
        };

        var responses = new List<Response>
        {
            new()
            {
                Data = new Data { Items = ["item1", "item2", "item3"] },
                Cursor = new Cursor { Next = "cursor2" },
            },
            new()
            {
                Data = new Data { Items = ["item4", "item5"] },
                Cursor = new Cursor { Next = "cursor3" },
            },
            new()
            {
                Data = new Data { Items = ["item6"] },
                Cursor = new Cursor { Next = "cursor4" },
            },
            new()
            {
                Data = new Data { Items = [] },
                Cursor = new Cursor { Next = null },
            },
        }.GetEnumerator();

        Pager<object> pager = await CursorPager<
            Request,
            TestRequestOptions,
            Response,
            string,
            object
        >.CreateInstanceAsync(
            new Request { Cursor = null },
            testOptions,
            (request, options, _) =>
            {
                requestCount++;
                if (options != null)
                {
                    idempotencyKeys.Add(options.IdempotencyKey ?? "");
                    apiVersions.Add(options.ApiVersion ?? "");
                }
                responses.MoveNext();
                return SystemTask.FromResult(responses.Current);
            },
            (request, cursor) => request.Cursor = cursor,
            response => response?.Cursor?.Next,
            response => response?.Data?.Items?.ToList()
        );

        var itemCount = 0;
        await foreach (var page in pager.AsPagesAsync())
        {
            itemCount += page.Items.Count;
        }

        Assert.Multiple(() =>
        {
            Assert.That(requestCount, Is.EqualTo(4));
            Assert.That(itemCount, Is.EqualTo(6));
            Assert.That(idempotencyKeys, Has.Count.EqualTo(4));
            Assert.That(idempotencyKeys, Has.All.EqualTo("idempotent-key-abc"));
            Assert.That(apiVersions, Has.Count.EqualTo(4));
            Assert.That(apiVersions, Has.All.EqualTo("v2"));
        });
    }

    private class Request
    {
        public required string? Cursor { get; set; }
    }

    private class Response
    {
        public required Data Data { get; set; }
        public required Cursor Cursor { get; set; }
    }

    private class Data
    {
        public required IEnumerable<string> Items { get; set; }
    }

    private class Cursor
    {
        public required string? Next { get; set; }
    }

    private class TestRequestOptions
    {
        public string? AuthToken { get; set; }
        public string? CorrelationId { get; set; }
        public string? IdempotencyKey { get; set; }
        public string? ApiVersion { get; set; }
    }
}

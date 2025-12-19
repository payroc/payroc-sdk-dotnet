using NUnit.Framework;
using Payroc.Core;
using SystemTask = global::System.Threading.Tasks.Task;

namespace Payroc.Test.Core.Pagination;

[TestFixture(Category = "Pagination")]
public class OffsetPagerHeaderPropagationTest
{
    [Test]
    public async SystemTask OffsetPager_PropagatesRequestOptionsToAllPages()
    {
        var requestCount = 0;
        var capturedOptions = new List<TestRequestOptions?>();

        var testOptions = new TestRequestOptions
        {
            CustomHeader = "test-value",
            ApiKey = "api-key-123"
        };

        var responses = new List<Response>
        {
            new() { Data = new() { Items = ["item1", "item2"] } },
            new() { Data = new() { Items = ["item3", "item4"] } },
            new() { Data = new() { Items = ["item5"] } },
            new() { Data = new() { Items = [] } },
        }.GetEnumerator();

        Pager<object> pager = await OffsetPager<
            Request,
            TestRequestOptions,
            Response,
            int,
            object?,
            object
        >.CreateInstanceAsync(
            new Request { Pagination = new Pagination { Page = 1 } },
            testOptions,
            (request, options, _) =>
            {
                requestCount++;
                capturedOptions.Add(options);
                responses.MoveNext();
                return SystemTask.FromResult(responses.Current);
            },
            request => request?.Pagination?.Page ?? 0,
            (request, offset) =>
            {
                request.Pagination ??= new();
                request.Pagination.Page = offset;
            },
            null,
            response => response?.Data?.Items?.ToList(),
            null
        );

        var itemCount = 0;
        await foreach (var page in pager.AsPagesAsync())
        {
            itemCount += page.Items.Count;
        }

        Assert.Multiple(() =>
        {
            Assert.That(requestCount, Is.EqualTo(4));
            Assert.That(itemCount, Is.EqualTo(5));
            Assert.That(capturedOptions, Has.Count.EqualTo(4));
            
            foreach (var options in capturedOptions)
            {
                Assert.That(options, Is.Not.Null);
                Assert.That(options!.CustomHeader, Is.EqualTo("test-value"));
                Assert.That(options.ApiKey, Is.EqualTo("api-key-123"));
            }
        });
    }

    [Test]
    public async SystemTask OffsetPager_SendRequestDelegateCanAccessOptionsForHeaderEvaluation()
    {
        var requestCount = 0;
        var authHeadersFromOptions = new List<string>();

        var testOptions = new TestRequestOptions
        {
            ApiKey = "dynamic-key"
        };

        var responses = new List<Response>
        {
            new() { Data = new() { Items = ["item1"] } },
            new() { Data = new() { Items = ["item2"] } },
            new() { Data = new() { Items = [] } },
        }.GetEnumerator();

        Pager<object> pager = await OffsetPager<
            Request,
            TestRequestOptions,
            Response,
            int,
            object?,
            object
        >.CreateInstanceAsync(
            new Request { Pagination = new Pagination { Page = 1 } },
            testOptions,
            (request, options, _) =>
            {
                requestCount++;
                if (options != null)
                {
                    authHeadersFromOptions.Add($"Bearer {options.ApiKey}");
                }
                responses.MoveNext();
                return SystemTask.FromResult(responses.Current);
            },
            request => request?.Pagination?.Page ?? 0,
            (request, offset) =>
            {
                request.Pagination ??= new();
                request.Pagination.Page = offset;
            },
            null,
            response => response?.Data?.Items?.ToList(),
            null
        );

        await foreach (var _ in pager.AsPagesAsync())
        {
        }

        Assert.Multiple(() =>
        {
            Assert.That(requestCount, Is.EqualTo(3));
            Assert.That(authHeadersFromOptions, Has.Count.EqualTo(3));
            Assert.That(authHeadersFromOptions, Has.All.EqualTo("Bearer dynamic-key"));
        });
    }

    [Test]
    public async SystemTask OffsetPager_WorksWithNullOptions()
    {
        var requestCount = 0;

        var responses = new List<Response>
        {
            new() { Data = new() { Items = ["item1"] } },
            new() { Data = new() { Items = ["item2"] } },
            new() { Data = new() { Items = [] } },
        }.GetEnumerator();

        Pager<object> pager = await OffsetPager<
            Request,
            TestRequestOptions?,
            Response,
            int,
            object?,
            object
        >.CreateInstanceAsync(
            new Request { Pagination = new Pagination { Page = 1 } },
            null,
            (request, options, _) =>
            {
                requestCount++;
                Assert.That(options, Is.Null);
                responses.MoveNext();
                return SystemTask.FromResult(responses.Current);
            },
            request => request?.Pagination?.Page ?? 0,
            (request, offset) =>
            {
                request.Pagination ??= new();
                request.Pagination.Page = offset;
            },
            null,
            response => response?.Data?.Items?.ToList(),
            null
        );

        var itemCount = 0;
        await foreach (var page in pager.AsPagesAsync())
        {
            itemCount += page.Items.Count;
        }

        Assert.Multiple(() =>
        {
            Assert.That(requestCount, Is.EqualTo(3));
            Assert.That(itemCount, Is.EqualTo(2));
        });
    }

    [Test]
    public async SystemTask OffsetPager_OptionsAvailableInGetNextPageAsync()
    {
        var capturedOptionsInPages = new List<string?>();

        var testOptions = new TestRequestOptions
        {
            CustomHeader = "page-header"
        };

        var responses = new List<Response>
        {
            new() { Data = new() { Items = ["item1"] } },
            new() { Data = new() { Items = ["item2"] } },
            new() { Data = new() { Items = ["item3"] } },
            new() { Data = new() { Items = [] } },
        }.GetEnumerator();

        Pager<object> pager = await OffsetPager<
            Request,
            TestRequestOptions,
            Response,
            int,
            object?,
            object
        >.CreateInstanceAsync(
            new Request { Pagination = new Pagination { Page = 1 } },
            testOptions,
            (request, options, _) =>
            {
                capturedOptionsInPages.Add(options?.CustomHeader);
                responses.MoveNext();
                return SystemTask.FromResult(responses.Current);
            },
            request => request?.Pagination?.Page ?? 0,
            (request, offset) =>
            {
                request.Pagination ??= new();
                request.Pagination.Page = offset;
            },
            null,
            response => response?.Data?.Items?.ToList(),
            null
        );

        Assert.That(pager.HasNextPage, Is.True);

        await pager.GetNextPageAsync();
        await pager.GetNextPageAsync();
        await pager.GetNextPageAsync();

        Assert.Multiple(() =>
        {
            Assert.That(capturedOptionsInPages, Has.Count.EqualTo(4));
            Assert.That(capturedOptionsInPages, Has.All.EqualTo("page-header"));
        });
    }

    private class Request
    {
        public Pagination? Pagination { get; set; }
    }

    private class Pagination
    {
        public int Page { get; set; }
    }

    private class Response
    {
        public Data? Data { get; set; }
    }

    private class Data
    {
        public IEnumerable<string>? Items { get; set; }
    }

    private class TestRequestOptions
    {
        public string? CustomHeader { get; set; }
        public string? ApiKey { get; set; }
    }
}

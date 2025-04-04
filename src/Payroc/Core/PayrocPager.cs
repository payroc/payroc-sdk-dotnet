using System.Runtime.CompilerServices;
using global::System.Net.Http;
using global::System.Net.Http.Json;
using global::System.Text.Json;

namespace Payroc.Core;

internal static class PayrocPagerFactory
{
    public static async Task<PayrocPager<TItem>> CreateAsync<TItem>(
        Func<HttpRequestMessage, CancellationToken,
            Task<HttpResponseMessage>> sendRequest,
        HttpRequestMessage initialRequest,
        CancellationToken cancellationToken = default
    )
    {
        var response = await sendRequest(initialRequest, cancellationToken).ConfigureAwait(false);
        var (
            nextPageRequest,
            hasNextPage,
            previousPageRequest,
            hasPreviousPage,
            page
            ) = await PayrocPager<TItem>.ParseHttpCallAsync(initialRequest, response, cancellationToken)
            .ConfigureAwait(false);
        return new PayrocPager<TItem>(
            sendRequest,
            nextPageRequest,
            hasNextPage,
            previousPageRequest,
            hasPreviousPage,
            page
        );
    }
}

public class PayrocPager<TItem> : BiPager<TItem>
{
    private const string NextRel = "next";
    private const string PreviousRel = "previous";
    private HttpRequestMessage? _nextPageRequest;
    private HttpRequestMessage? _previousPageRequest;

    private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _sendRequest;

    public PayrocPager(
        Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendRequest,
        HttpRequestMessage? nextPageRequest,
        bool hasNextPage,
        HttpRequestMessage? previousPageRequest,
        bool hasPreviousPage,
        Page<TItem> page
    )
    {
        _sendRequest = sendRequest;
        _nextPageRequest = nextPageRequest;
        HasNextPage = hasNextPage;
        _previousPageRequest = previousPageRequest;
        HasPreviousPage = hasPreviousPage;
        CurrentPage = page;
    }

    public bool HasNextPage { get; private set; }
    public bool HasPreviousPage { get; private set; }
    public Page<TItem> CurrentPage { get; private set; }

    public async Task<Page<TItem>> GetNextPageAsync(CancellationToken cancellationToken = default)
    {
        if (_nextPageRequest == null)
        {
            return Page<TItem>.Empty;
        }
        return await SendRequestAndHandleResponse(_nextPageRequest, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Page<TItem>> GetPreviousPageAsync(CancellationToken cancellationToken = default)
    {
        if (_previousPageRequest == null)
        {
            return Page<TItem>.Empty;
        }
        return await SendRequestAndHandleResponse(_previousPageRequest, cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task<Page<TItem>> SendRequestAndHandleResponse(
        HttpRequestMessage request,
        CancellationToken cancellationToken = default)
    {
        var response = await _sendRequest(request, cancellationToken).ConfigureAwait(false);
        var (
            nextPageRequest,
            hasNextPage,
            previousPageRequest,
            hasPreviousPage,
            page
            ) = await ParseHttpCallAsync(request, response, cancellationToken).ConfigureAwait(false);
        _nextPageRequest = nextPageRequest;
        HasNextPage = hasNextPage;
        _previousPageRequest = previousPageRequest;
        HasPreviousPage = hasPreviousPage;
        CurrentPage = page;
        return page;
    }
    internal static async Task<(
        HttpRequestMessage? nextPageRequest,
        bool hasNextPage,
        HttpRequestMessage? previousPageRequest,
        bool hasPreviousPage,
        Page<TItem> page
        )> ParseHttpCallAsync(
        HttpRequestMessage request,
        HttpResponseMessage response,
        CancellationToken cancellationToken = default
    )
    {
        var json = await response.Content
            .ReadFromJsonAsync<JsonElement>(JsonOptions.JsonSerializerOptions, cancellationToken)
            .ConfigureAwait(false);

        var prevUri = GetLinkUri(json, PreviousRel);
        var hasPreviousPage = prevUri != null;
        var previousPageRequest = prevUri == null ? null : CloneRequestWithNewUri(request, prevUri);

        var nextUri = GetLinkUri(json, NextRel);
        var hasNextPage = nextUri != null;
        var nextPageRequest = nextUri == null ? null : CloneRequestWithNewUri(request, nextUri);

        var data = json.GetProperty("data").Deserialize<IReadOnlyList<TItem>>(JsonOptions.JsonSerializerOptions);
        var page = data == null ? Page<TItem>.Empty : new Page<TItem>(data);

        return (
            nextPageRequest,
            hasNextPage,
            previousPageRequest,
            hasPreviousPage,
            page
        );
    }

    private static Uri? GetLinkUri(JsonElement json, string rel)
    {
        var links = json.GetProperty("links")
            .EnumerateArray();
        foreach (var link in links)
        {
            if (!link.TryGetProperty("rel", out var relProperty)) continue;
            if (relProperty.GetString() != rel) continue;
            var uriString = link.GetProperty("href").GetString();
            if (uriString == null) continue;
            return new Uri(uriString);
        }

        return null;
    }

    /// <summary>
    /// Creates a clone of the request, preserving headers and content
    /// </summary>
    private static HttpRequestMessage CloneRequestWithNewUri(HttpRequestMessage request, Uri newUri)
    {
        var clonedRequest = new HttpRequestMessage(request.Method, request.RequestUri);
        clonedRequest.Version = request.Version;

        if (request.Content != null)
        {
            clonedRequest.Content = request.Content;

            foreach (var header in request.Content.Headers)
            {
                clonedRequest.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        foreach (var header in request.Headers)
        {
            clonedRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        clonedRequest.RequestUri = newUri;

        return clonedRequest;
    }

    public async IAsyncEnumerator<TItem> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        foreach (var item in CurrentPage)
        {
            yield return item;
        }
        await foreach (var page in GetNextPagesAsync(cancellationToken))
        {
            foreach (var item in page)
            {
                yield return item;
            }
        }
    }

    public async IAsyncEnumerable<Page<TItem>> GetNextPagesAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        while (HasNextPage)
        {
            yield return await GetNextPageAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public async IAsyncEnumerable<Page<TItem>> GetPreviousPagesAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        while (HasPreviousPage)
        {
            yield return await GetPreviousPageAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
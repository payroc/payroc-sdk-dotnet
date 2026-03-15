# Regression: Retry Disposed Content (ObjectDisposedException)

- **Date:** 15 March 2026
- **Fern metadata at time of fix:**
  ```json
  {
    "cliVersion": "4.29.0",
    "generatorName": "fernapi/fern-csharp-sdk",
    "generatorVersion": "2.30.0",
    "originGitCommit": "6d46b4d33f7a26c12a9950737bb7f220a8b8c59c",
    "sdkVersion": "0.0.3934"
  }
  ```

## Scenario

When an API call with a JSON (or other non-empty) request body fails with a retryable status code
(408, 429, 5xx), the SDK retries the request. With `MaxRetries >= 2`, the second retry throws
`ObjectDisposedException` because the request body content has already been disposed.

A customer reported this when attempting to board a merchant platform + processing account. Setting
`MaxRetries = 0` was the only workaround.

## Root Cause

In `RawClient.CloneRequestAsync`, the `default` branch for non-multipart content performed a
**shallow copy** — it assigned the same `StringContent` instance to the cloned request:

```csharp
default:
    clonedRequest.Content = request.Content; // shared reference
    break;
```

The retry loop in `SendWithRetriesAsync` wraps each cloned request in a `using` statement:

```csharp
using var retryRequest = await CloneRequestAsync(request).ConfigureAwait(false);
```

When the first retry's `retryRequest` is disposed at the end of the loop iteration, the shared
`StringContent` is also disposed. The next retry then attempts to send the same disposed content,
resulting in `ObjectDisposedException`.

## Fix

Commit `b389e9e` ("SDK regeneration #668") changed `CloneRequestAsync` to **deep-copy** all
non-multipart content via `CopyToAsync` into a fresh `MemoryStream` + `StreamContent`, preserving
the original content headers:

```csharp
default:
    var bodyStream = new MemoryStream();
    await request.Content.CopyToAsync(bodyStream).ConfigureAwait(false);
    bodyStream.Position = 0;
    var clonedContent = new StreamContent(bodyStream);
    foreach (var header in request.Content.Headers)
    {
        clonedContent.Headers.TryAddWithoutValidation(header.Key, header.Value);
    }
    clonedRequest.Content = clonedContent;
    break;
```

The same commit also fixed a sub-bug in the multipart branch where part headers were being copied
from the parent `MultipartContent.Headers` instead of each individual `content.Headers`.

## Test Coverage

The regression test in this folder sends a JSON POST that triggers **two consecutive 500 errors**
before succeeding on the third attempt (MaxRetries = 2). This precisely matches the customer's
scenario where the second retry was the one that threw `ObjectDisposedException`.

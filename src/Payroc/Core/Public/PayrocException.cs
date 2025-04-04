using System;

namespace Payroc;

/// <summary>
/// Base exception class for all exceptions thrown by the SDK.
/// </summary>
public class PayrocException(string message, Exception? innerException = null)
    : Exception(message, innerException);

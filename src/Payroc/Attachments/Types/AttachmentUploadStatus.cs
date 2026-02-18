using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Attachments;

[JsonConverter(typeof(StringEnumSerializer<AttachmentUploadStatus>))]
[Serializable]
public readonly record struct AttachmentUploadStatus : IStringEnum
{
    public static readonly AttachmentUploadStatus Pending = new(Values.Pending);

    public static readonly AttachmentUploadStatus Accepted = new(Values.Accepted);

    public static readonly AttachmentUploadStatus Rejected = new(Values.Rejected);

    public AttachmentUploadStatus(string value)
    {
        Value = value;
    }

    /// <summary>
    /// The string value of the enum.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Create a string enum with the given value.
    /// </summary>
    public static AttachmentUploadStatus FromCustom(string value)
    {
        return new AttachmentUploadStatus(value);
    }

    public bool Equals(string? other)
    {
        return Value.Equals(other);
    }

    /// <summary>
    /// Returns the string value of the enum.
    /// </summary>
    public override string ToString()
    {
        return Value;
    }

    public static bool operator ==(AttachmentUploadStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AttachmentUploadStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(AttachmentUploadStatus value) => value.Value;

    public static explicit operator AttachmentUploadStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Pending = "pending";

        public const string Accepted = "accepted";

        public const string Rejected = "rejected";
    }
}

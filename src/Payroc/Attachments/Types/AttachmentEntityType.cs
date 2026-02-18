using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Attachments;

[JsonConverter(typeof(StringEnumSerializer<AttachmentEntityType>))]
[Serializable]
public readonly record struct AttachmentEntityType : IStringEnum
{
    public static readonly AttachmentEntityType ProcessingAccount = new(Values.ProcessingAccount);

    public AttachmentEntityType(string value)
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
    public static AttachmentEntityType FromCustom(string value)
    {
        return new AttachmentEntityType(value);
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

    public static bool operator ==(AttachmentEntityType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AttachmentEntityType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(AttachmentEntityType value) => value.Value;

    public static explicit operator AttachmentEntityType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string ProcessingAccount = "processingAccount";
    }
}

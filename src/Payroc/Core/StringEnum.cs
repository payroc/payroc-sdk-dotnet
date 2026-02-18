using global::System.Text.Json.Serialization;

namespace Payroc.Core;

public interface IStringEnum : IEquatable<string>
{
    public string Value { get; }
}

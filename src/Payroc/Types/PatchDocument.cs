// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// A JSON Patch operation as defined by RFC 6902.
/// </summary>
[JsonConverter(typeof(PatchDocument.JsonConverter))]
public record PatchDocument
{
    internal PatchDocument(string type, object? value)
    {
        Op = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of PatchDocument with <see cref="PatchDocument.Add"/>.
    /// </summary>
    public PatchDocument(PatchDocument.Add value)
    {
        Op = "add";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PatchDocument with <see cref="PatchDocument.Remove"/>.
    /// </summary>
    public PatchDocument(PatchDocument.Remove value)
    {
        Op = "remove";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PatchDocument with <see cref="PatchDocument.Replace"/>.
    /// </summary>
    public PatchDocument(PatchDocument.Replace value)
    {
        Op = "replace";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PatchDocument with <see cref="PatchDocument.Move"/>.
    /// </summary>
    public PatchDocument(PatchDocument.Move value)
    {
        Op = "move";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PatchDocument with <see cref="PatchDocument.Copy"/>.
    /// </summary>
    public PatchDocument(PatchDocument.Copy value)
    {
        Op = "copy";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of PatchDocument with <see cref="PatchDocument.Test"/>.
    /// </summary>
    public PatchDocument(PatchDocument.Test value)
    {
        Op = "test";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("op")]
    public string Op { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="Op"/> is "add"
    /// </summary>
    public bool IsAdd => Op == "add";

    /// <summary>
    /// Returns true if <see cref="Op"/> is "remove"
    /// </summary>
    public bool IsRemove => Op == "remove";

    /// <summary>
    /// Returns true if <see cref="Op"/> is "replace"
    /// </summary>
    public bool IsReplace => Op == "replace";

    /// <summary>
    /// Returns true if <see cref="Op"/> is "move"
    /// </summary>
    public bool IsMove => Op == "move";

    /// <summary>
    /// Returns true if <see cref="Op"/> is "copy"
    /// </summary>
    public bool IsCopy => Op == "copy";

    /// <summary>
    /// Returns true if <see cref="Op"/> is "test"
    /// </summary>
    public bool IsTest => Op == "test";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PatchAdd"/> if <see cref="Op"/> is 'add', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Op"/> is not 'add'.</exception>
    public Payroc.PatchAdd AsAdd() =>
        IsAdd ? (Payroc.PatchAdd)Value! : throw new Exception("PatchDocument.Op is not 'add'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PatchRemove"/> if <see cref="Op"/> is 'remove', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Op"/> is not 'remove'.</exception>
    public Payroc.PatchRemove AsRemove() =>
        IsRemove
            ? (Payroc.PatchRemove)Value!
            : throw new Exception("PatchDocument.Op is not 'remove'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PatchReplace"/> if <see cref="Op"/> is 'replace', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Op"/> is not 'replace'.</exception>
    public Payroc.PatchReplace AsReplace() =>
        IsReplace
            ? (Payroc.PatchReplace)Value!
            : throw new Exception("PatchDocument.Op is not 'replace'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PatchMove"/> if <see cref="Op"/> is 'move', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Op"/> is not 'move'.</exception>
    public Payroc.PatchMove AsMove() =>
        IsMove ? (Payroc.PatchMove)Value! : throw new Exception("PatchDocument.Op is not 'move'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PatchCopy"/> if <see cref="Op"/> is 'copy', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Op"/> is not 'copy'.</exception>
    public Payroc.PatchCopy AsCopy() =>
        IsCopy ? (Payroc.PatchCopy)Value! : throw new Exception("PatchDocument.Op is not 'copy'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.PatchTest"/> if <see cref="Op"/> is 'test', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Op"/> is not 'test'.</exception>
    public Payroc.PatchTest AsTest() =>
        IsTest ? (Payroc.PatchTest)Value! : throw new Exception("PatchDocument.Op is not 'test'");

    public T Match<T>(
        Func<Payroc.PatchAdd, T> onAdd,
        Func<Payroc.PatchRemove, T> onRemove,
        Func<Payroc.PatchReplace, T> onReplace,
        Func<Payroc.PatchMove, T> onMove,
        Func<Payroc.PatchCopy, T> onCopy,
        Func<Payroc.PatchTest, T> onTest,
        Func<string, object?, T> onUnknown_
    )
    {
        return Op switch
        {
            "add" => onAdd(AsAdd()),
            "remove" => onRemove(AsRemove()),
            "replace" => onReplace(AsReplace()),
            "move" => onMove(AsMove()),
            "copy" => onCopy(AsCopy()),
            "test" => onTest(AsTest()),
            _ => onUnknown_(Op, Value),
        };
    }

    public void Visit(
        Action<Payroc.PatchAdd> onAdd,
        Action<Payroc.PatchRemove> onRemove,
        Action<Payroc.PatchReplace> onReplace,
        Action<Payroc.PatchMove> onMove,
        Action<Payroc.PatchCopy> onCopy,
        Action<Payroc.PatchTest> onTest,
        Action<string, object?> onUnknown_
    )
    {
        switch (Op)
        {
            case "add":
                onAdd(AsAdd());
                break;
            case "remove":
                onRemove(AsRemove());
                break;
            case "replace":
                onReplace(AsReplace());
                break;
            case "move":
                onMove(AsMove());
                break;
            case "copy":
                onCopy(AsCopy());
                break;
            case "test":
                onTest(AsTest());
                break;
            default:
                onUnknown_(Op, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PatchAdd"/> and returns true if successful.
    /// </summary>
    public bool TryAsAdd(out Payroc.PatchAdd? value)
    {
        if (Op == "add")
        {
            value = (Payroc.PatchAdd)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PatchRemove"/> and returns true if successful.
    /// </summary>
    public bool TryAsRemove(out Payroc.PatchRemove? value)
    {
        if (Op == "remove")
        {
            value = (Payroc.PatchRemove)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PatchReplace"/> and returns true if successful.
    /// </summary>
    public bool TryAsReplace(out Payroc.PatchReplace? value)
    {
        if (Op == "replace")
        {
            value = (Payroc.PatchReplace)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PatchMove"/> and returns true if successful.
    /// </summary>
    public bool TryAsMove(out Payroc.PatchMove? value)
    {
        if (Op == "move")
        {
            value = (Payroc.PatchMove)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PatchCopy"/> and returns true if successful.
    /// </summary>
    public bool TryAsCopy(out Payroc.PatchCopy? value)
    {
        if (Op == "copy")
        {
            value = (Payroc.PatchCopy)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.PatchTest"/> and returns true if successful.
    /// </summary>
    public bool TryAsTest(out Payroc.PatchTest? value)
    {
        if (Op == "test")
        {
            value = (Payroc.PatchTest)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator PatchDocument(PatchDocument.Add value) => new(value);

    public static implicit operator PatchDocument(PatchDocument.Remove value) => new(value);

    public static implicit operator PatchDocument(PatchDocument.Replace value) => new(value);

    public static implicit operator PatchDocument(PatchDocument.Move value) => new(value);

    public static implicit operator PatchDocument(PatchDocument.Copy value) => new(value);

    public static implicit operator PatchDocument(PatchDocument.Test value) => new(value);

    internal sealed class JsonConverter : JsonConverter<PatchDocument>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(PatchDocument).IsAssignableFrom(typeToConvert);

        public override PatchDocument Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("op", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'op'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'op' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'op' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'op' is null");

            var value = discriminator switch
            {
                "add" => json.Deserialize<Payroc.PatchAdd>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.PatchAdd"),
                "remove" => json.Deserialize<Payroc.PatchRemove>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.PatchRemove"),
                "replace" => json.Deserialize<Payroc.PatchReplace>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.PatchReplace"),
                "move" => json.Deserialize<Payroc.PatchMove>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.PatchMove"),
                "copy" => json.Deserialize<Payroc.PatchCopy>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.PatchCopy"),
                "test" => json.Deserialize<Payroc.PatchTest>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.PatchTest"),
                _ => json.Deserialize<object?>(options),
            };
            return new PatchDocument(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PatchDocument value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Op switch
                {
                    "add" => JsonSerializer.SerializeToNode(value.Value, options),
                    "remove" => JsonSerializer.SerializeToNode(value.Value, options),
                    "replace" => JsonSerializer.SerializeToNode(value.Value, options),
                    "move" => JsonSerializer.SerializeToNode(value.Value, options),
                    "copy" => JsonSerializer.SerializeToNode(value.Value, options),
                    "test" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["op"] = value.Op;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for add
    /// </summary>
    public struct Add
    {
        public Add(Payroc.PatchAdd value)
        {
            Value = value;
        }

        internal Payroc.PatchAdd Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Add(Payroc.PatchAdd value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for remove
    /// </summary>
    public struct Remove
    {
        public Remove(Payroc.PatchRemove value)
        {
            Value = value;
        }

        internal Payroc.PatchRemove Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Remove(Payroc.PatchRemove value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for replace
    /// </summary>
    public struct Replace
    {
        public Replace(Payroc.PatchReplace value)
        {
            Value = value;
        }

        internal Payroc.PatchReplace Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Replace(Payroc.PatchReplace value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for move
    /// </summary>
    public struct Move
    {
        public Move(Payroc.PatchMove value)
        {
            Value = value;
        }

        internal Payroc.PatchMove Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Move(Payroc.PatchMove value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for copy
    /// </summary>
    public struct Copy
    {
        public Copy(Payroc.PatchCopy value)
        {
            Value = value;
        }

        internal Payroc.PatchCopy Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Copy(Payroc.PatchCopy value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for test
    /// </summary>
    public struct Test
    {
        public Test(Payroc.PatchTest value)
        {
            Value = value;
        }

        internal Payroc.PatchTest Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Test(Payroc.PatchTest value) => new(value);
    }
}

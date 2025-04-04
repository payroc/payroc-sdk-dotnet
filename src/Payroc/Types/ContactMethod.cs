// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(ContactMethod.JsonConverter))]
public record ContactMethod
{
    internal ContactMethod(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of ContactMethod with <see cref="ContactMethod.Email"/>.
    /// </summary>
    public ContactMethod(ContactMethod.Email value)
    {
        Type = "email";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of ContactMethod with <see cref="ContactMethod.Phone"/>.
    /// </summary>
    public ContactMethod(ContactMethod.Phone value)
    {
        Type = "phone";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of ContactMethod with <see cref="ContactMethod.Mobile"/>.
    /// </summary>
    public ContactMethod(ContactMethod.Mobile value)
    {
        Type = "mobile";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of ContactMethod with <see cref="ContactMethod.Fax"/>.
    /// </summary>
    public ContactMethod(ContactMethod.Fax value)
    {
        Type = "fax";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="Type"/> is "email"
    /// </summary>
    public bool IsEmail => Type == "email";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "phone"
    /// </summary>
    public bool IsPhone => Type == "phone";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "mobile"
    /// </summary>
    public bool IsMobile => Type == "mobile";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "fax"
    /// </summary>
    public bool IsFax => Type == "fax";

    /// <summary>
    /// Returns the value as a <see cref="Payroc.ContactMethodEmail"/> if <see cref="Type"/> is 'email', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'email'.</exception>
    public Payroc.ContactMethodEmail AsEmail() =>
        IsEmail
            ? (Payroc.ContactMethodEmail)Value!
            : throw new Exception("ContactMethod.Type is not 'email'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.ContactMethodPhone"/> if <see cref="Type"/> is 'phone', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'phone'.</exception>
    public Payroc.ContactMethodPhone AsPhone() =>
        IsPhone
            ? (Payroc.ContactMethodPhone)Value!
            : throw new Exception("ContactMethod.Type is not 'phone'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.ContactMethodMobile"/> if <see cref="Type"/> is 'mobile', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'mobile'.</exception>
    public Payroc.ContactMethodMobile AsMobile() =>
        IsMobile
            ? (Payroc.ContactMethodMobile)Value!
            : throw new Exception("ContactMethod.Type is not 'mobile'");

    /// <summary>
    /// Returns the value as a <see cref="Payroc.ContactMethodFax"/> if <see cref="Type"/> is 'fax', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'fax'.</exception>
    public Payroc.ContactMethodFax AsFax() =>
        IsFax
            ? (Payroc.ContactMethodFax)Value!
            : throw new Exception("ContactMethod.Type is not 'fax'");

    public T Match<T>(
        Func<Payroc.ContactMethodEmail, T> onEmail,
        Func<Payroc.ContactMethodPhone, T> onPhone,
        Func<Payroc.ContactMethodMobile, T> onMobile,
        Func<Payroc.ContactMethodFax, T> onFax,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "email" => onEmail(AsEmail()),
            "phone" => onPhone(AsPhone()),
            "mobile" => onMobile(AsMobile()),
            "fax" => onFax(AsFax()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Payroc.ContactMethodEmail> onEmail,
        Action<Payroc.ContactMethodPhone> onPhone,
        Action<Payroc.ContactMethodMobile> onMobile,
        Action<Payroc.ContactMethodFax> onFax,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "email":
                onEmail(AsEmail());
                break;
            case "phone":
                onPhone(AsPhone());
                break;
            case "mobile":
                onMobile(AsMobile());
                break;
            case "fax":
                onFax(AsFax());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.ContactMethodEmail"/> and returns true if successful.
    /// </summary>
    public bool TryAsEmail(out Payroc.ContactMethodEmail? value)
    {
        if (Type == "email")
        {
            value = (Payroc.ContactMethodEmail)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.ContactMethodPhone"/> and returns true if successful.
    /// </summary>
    public bool TryAsPhone(out Payroc.ContactMethodPhone? value)
    {
        if (Type == "phone")
        {
            value = (Payroc.ContactMethodPhone)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.ContactMethodMobile"/> and returns true if successful.
    /// </summary>
    public bool TryAsMobile(out Payroc.ContactMethodMobile? value)
    {
        if (Type == "mobile")
        {
            value = (Payroc.ContactMethodMobile)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Payroc.ContactMethodFax"/> and returns true if successful.
    /// </summary>
    public bool TryAsFax(out Payroc.ContactMethodFax? value)
    {
        if (Type == "fax")
        {
            value = (Payroc.ContactMethodFax)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator ContactMethod(ContactMethod.Email value) => new(value);

    public static implicit operator ContactMethod(ContactMethod.Phone value) => new(value);

    public static implicit operator ContactMethod(ContactMethod.Mobile value) => new(value);

    public static implicit operator ContactMethod(ContactMethod.Fax value) => new(value);

    internal sealed class JsonConverter : JsonConverter<ContactMethod>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(ContactMethod).IsAssignableFrom(typeToConvert);

        public override ContactMethod Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("type", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'type'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'type' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'type' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'type' is null");

            var value = discriminator switch
            {
                "email" => json.Deserialize<Payroc.ContactMethodEmail>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.ContactMethodEmail"),
                "phone" => json.Deserialize<Payroc.ContactMethodPhone>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.ContactMethodPhone"),
                "mobile" => json.Deserialize<Payroc.ContactMethodMobile>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.ContactMethodMobile"),
                "fax" => json.Deserialize<Payroc.ContactMethodFax>(options)
                    ?? throw new JsonException("Failed to deserialize Payroc.ContactMethodFax"),
                _ => json.Deserialize<object?>(options),
            };
            return new ContactMethod(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ContactMethod value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "email" => JsonSerializer.SerializeToNode(value.Value, options),
                    "phone" => JsonSerializer.SerializeToNode(value.Value, options),
                    "mobile" => JsonSerializer.SerializeToNode(value.Value, options),
                    "fax" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for email
    /// </summary>
    public struct Email
    {
        public Email(Payroc.ContactMethodEmail value)
        {
            Value = value;
        }

        internal Payroc.ContactMethodEmail Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Email(Payroc.ContactMethodEmail value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for phone
    /// </summary>
    public struct Phone
    {
        public Phone(Payroc.ContactMethodPhone value)
        {
            Value = value;
        }

        internal Payroc.ContactMethodPhone Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Phone(Payroc.ContactMethodPhone value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for mobile
    /// </summary>
    public struct Mobile
    {
        public Mobile(Payroc.ContactMethodMobile value)
        {
            Value = value;
        }

        internal Payroc.ContactMethodMobile Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Mobile(Payroc.ContactMethodMobile value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for fax
    /// </summary>
    public struct Fax
    {
        public Fax(Payroc.ContactMethodFax value)
        {
            Value = value;
        }

        internal Payroc.ContactMethodFax Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Fax(Payroc.ContactMethodFax value) => new(value);
    }
}

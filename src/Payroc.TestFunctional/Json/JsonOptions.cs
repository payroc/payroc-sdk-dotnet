using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Payroc.TestFunctional.Json;

public static class JsonOptions
{
    public static readonly JsonSerializerOptions DefaultDeserialization = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static readonly JsonSerializerOptions RelaxedDeserialization = CreateRelaxedOptions();

    private static JsonSerializerOptions CreateRelaxedOptions()
    {
        var options = new JsonSerializerOptions(DefaultDeserialization)
        {
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers =
                {
                    ti =>
                    {
                        foreach (var prop in ti.Properties)
                        {
                            // If [JsonIgnore], skip "required" enforcement
                            if (prop.AttributeProvider?
                                .GetCustomAttributes(typeof(JsonIgnoreAttribute), true)
                                .Any() == true)
                            {
                                prop.IsRequired = false;
                            }
                        }
                    }
                }
            }
        };

        return options;
    }
}

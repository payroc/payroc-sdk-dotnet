using System.Text.Json;

namespace Payroc.Test.Utils;

public static class JsonDataPropertyReader
{
    public static IEnumerable<T> DeserializeDataProperty<T>(string json)
    {
        using var doc = JsonDocument.Parse(json);

        var dataElement = doc.RootElement.GetProperty("data");

        return JsonSerializer.Deserialize<IEnumerable<T>>(dataElement.GetRawText())!;
    }
}

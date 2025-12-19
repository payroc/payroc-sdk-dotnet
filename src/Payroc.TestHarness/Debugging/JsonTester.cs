namespace Payroc.TestHarness.Debugging;

public static class JsonTester
{
    public static void TestJson<T>(string json)
    {
        try
        {
            var deserializedObj = System.Text.Json.JsonSerializer.Deserialize<T>(json);
            Console.WriteLine("Json is ok");
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            Console.WriteLine("Json deserialize failed");
        }
    }
}

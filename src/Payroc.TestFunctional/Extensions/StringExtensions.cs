namespace Payroc.TestFunctional.Extensions;

public static class StringExtensions
{
    private const string TestDataFolder = "TestData";
    private const string JsonExtension = ".json";

    public static string AsTestDataFilePath(this string filename)
        => filename.AsJsonFilename().WithTestDataFolder();

    private static string AsJsonFilename(this string filename)
        => filename.Contains(JsonExtension)
            ? filename
            : filename + JsonExtension;

    private static string WithTestDataFolder(this string filepath)
        => filepath.Contains(TestDataFolder)
            ? filepath
            : Path.Combine(TestDataFolder, filepath);
}

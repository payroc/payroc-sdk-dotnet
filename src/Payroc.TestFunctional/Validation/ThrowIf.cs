namespace Payroc.TestFunctional.Validation;

public class ThrowIf
{
    public static void IsNullOrEmpty(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("File path cannot be null or empty.", nameof(value));
        }
    }

    public static void FileNotFound(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("The specified file was not found.", filePath);
        }
    }
}

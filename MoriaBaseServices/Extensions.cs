using System.Text.RegularExpressions;

namespace MoriaBaseServices;
public static class Extensions
{
    public static bool IsNumber(this string currentText, string newText)
    {
        var fullText = currentText + newText;

        if (fullText.Count(c => c == '.' || c == ',') > 1)
            return false;

        return Regex.IsMatch(fullText, @"^[0-9]*(?:[\,][0-9]*)?$");
    }

    public static string GetFolderName(this string text)
    {
        return text
            .Replace('<', '_')
            .Replace('>', '_')
            .Replace(':', '_')
            .Replace('"', '_')
            .Replace('\\', '_')
            .Replace('/', '_')
            .Replace('|', '_')
            .Replace('?', '_')
            .Replace('*', '_');
    }
}

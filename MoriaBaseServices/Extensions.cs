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
}

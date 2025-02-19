using System.Text.RegularExpressions;

namespace AdvicerApp.BL.Extensions;

public static class BadWordsFilter
{
    private static readonly string[] BadWords =
    {
        "pissoz",  "isvermek", "yalan"
    };

    private static readonly Regex BadWordsRegex = new Regex(
        string.Join("|", BadWords.Select(Regex.Escape)),
        RegexOptions.IgnoreCase | RegexOptions.Compiled
    );

    public static bool ContainsBadWords(string text)
    {
        return BadWordsRegex.IsMatch(text);
    }
}

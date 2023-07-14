using System.Text;
using System;

/// <summary>
/// Credit: https://stackoverflow.com/a/62782791/7608550
/// </summary>
public static class SafeReplace
{
    public static string ReplaceWholeWord(this string text, string word, string bywhat)
    {
        static bool IsWordChar(char c) => char.IsLetterOrDigit(c) || c == '_';
        StringBuilder sb = null;
        int p = 0, j = 0;
        while (j < text.Length && (j = text.IndexOf(word, j, StringComparison.Ordinal)) >= 0)
            if ((j == 0 || !IsWordChar(text[j - 1])) &&
                (j + word.Length == text.Length || !IsWordChar(text[j + word.Length])))
            {
                sb ??= new StringBuilder();
                sb.Append(text, p, j - p);
                sb.Append(bywhat);
                j += word.Length;
                p = j;
            }
            else j++;
        if (sb == null) return text;
        sb.Append(text, p, text.Length - p);
        return sb.ToString();
    }
}
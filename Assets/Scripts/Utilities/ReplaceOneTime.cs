public class ReplaceOneTime
{
    /// <summary>
    /// Credit: https://stackoverflow.com/a/8809392/7608550
    /// </summary>
    /// <param name="text"></param>
    /// <param name="oldChar"></param>
    /// <param name="newChar"></param>
    /// <returns></returns>
    public static string ReplaceNow(string text, string oldChar, string newChar)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        else
        {
            if (text.Contains(oldChar))
                text = text.Remove(text.IndexOf(oldChar)) + newChar + text.Substring(text.IndexOf(oldChar) + 1);

            return text;
        }
    }
}

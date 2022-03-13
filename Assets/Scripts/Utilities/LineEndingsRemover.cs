public class LineEndingsRemover
{
    /// <summary>
    /// Remove line endings
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Fix(string text)
    {
        if (string.IsNullOrEmpty(text)
            || string.IsNullOrWhiteSpace(text))
            return string.Empty;
        else
            return text.Replace("\r", string.Empty).Replace("/n", string.Empty);
    }
}

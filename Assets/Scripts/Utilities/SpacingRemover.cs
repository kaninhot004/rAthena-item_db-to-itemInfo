public class SpacingRemover
{
    public static string Remove(string text, bool isTabRemove = false)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        while (text.Contains(" "))
            text = text.Replace(" ", string.Empty);

        while (isTabRemove && text.Contains("\t"))
            text = text.Replace("\t", string.Empty);

        return text;
    }
}

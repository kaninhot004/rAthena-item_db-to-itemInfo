public class SpacingRemover
{
    public static string Remove(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        while (text.Contains(" "))
            text = text.Replace(" ", string.Empty);

        return text;
    }
}

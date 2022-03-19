public class CommentRemover
{
    /// <summary>
    /// Remove YAML comments (Also some words from rAthena)
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Fix(string text)
    {
        if (string.IsNullOrEmpty(text)
            || string.IsNullOrWhiteSpace(text))
            return string.Empty;
        else
        {
            text = text.Replace("    # !todo check english name", string.Empty);
            text = text.Replace("   # unknown view", string.Empty);

            if (text.Contains("#"))
                text = text.Substring(0, text.IndexOf("#"));

            text = text.Replace("Header:", string.Empty);
            text = text.Replace("  Type: ITEM_DB", string.Empty);

            if (text.Contains("  Version: "))
                text = string.Empty;

            return text;
        }
    }

    /// <summary>
    /// Fix comment
    /// </summary>
    /// <param name="texts"></param>
    /// <param name="index"></param>
    public static string FixCommentSeperateLine(string[] texts, int index)
    {
        var text = texts[index];

        if (text.Contains("/*")
            && !text.Contains("*/"))
        {
            int lineIncremental = 1;

            while ((index + lineIncremental) < texts.Length)
            {
                if (texts[index + lineIncremental].Contains("*/"))
                {
                    // Remove all line from 'Found end comment lines' till current line
                    while (lineIncremental > 0)
                    {
                        texts[index + lineIncremental] = string.Empty;

                        lineIncremental--;
                    }

                    break;
                }
                else
                    lineIncremental++;
            }
        }

        // This line had /* and */
        while (text.Contains("/*")
            && text.Contains("*/"))
        {
            var copier = text;

            text = copier.Substring(0, copier.IndexOf("/*")) + copier.Substring(copier.IndexOf("*/") + 2);
        }

        // Still had /* (Unexpected error)
        while (text.Contains("/*"))
        {
            var copier = text;

            text = copier.Substring(0, copier.IndexOf("/*"));
        }

        // Still had */ (Unexpected error)
        while (text.Contains("*/"))
        {
            var copier = text;

            text = copier.Substring(copier.IndexOf("*/") + 2);
        }

        // Remove \ in current line (Mostly found on autobonus)
        text = text.Replace("\\", string.Empty);

        text = Fix(text);

        text = LineEndingsRemover.Fix(text);

        return text;
    }
}

using EasyButtons;
using System.Text;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class ErrorResourceNameHelper : MonoBehaviour
{
    [Button()]
    public void ExportErrorResourceNameFixer()
    {
        List<string> errorItemIds = new List<string>();

        var path = Application.dataPath + "/Assets/error_resource_names.txt";
        var path2 = Application.dataPath + "/Assets/error_resource_name_helper.txt";

        // Is file exists?
        if (!File.Exists(path)
            || !File.Exists(path2))
            return;

        var errorResourceNameFile = File.ReadAllText(path);
        var errorResourceNameFile2 = File.ReadAllText(path2);

        var errorResourceNameLines = errorResourceNameFile.Split('\n');
        var errorResourceNameLines2 = errorResourceNameFile2.Split('\n');

        for (int i = 0; i < errorResourceNameLines.Length; i++)
        {
            var text = errorResourceNameLines[i];

            text = CommentRemover.Fix(text);

            text = LineEndingsRemover.Fix(text);

            if (string.IsNullOrEmpty(text)
                || string.IsNullOrWhiteSpace(text))
                continue;

            errorItemIds.Add(text);
        }

        Debug.Log("There are " + errorItemIds.Count + " error resource name.");

        StringBuilder builder = new StringBuilder();

        bool isApply = false;
        string itemId = string.Empty;
        int helpCount = 0;
        for (int i = 0; i < errorResourceNameLines2.Length; i++)
        {
            var text = errorResourceNameLines2[i];

            text = CommentRemover.Fix(text);

            text = LineEndingsRemover.Fix(text);
            text = SpacingRemover.Remove(text, true);

            if (string.IsNullOrEmpty(text)
                || string.IsNullOrWhiteSpace(text)
                || text.Contains("unidentifiedResourceName"))
                continue;

            if (text.Contains("]={"))
            {
                itemId = text.Replace("[", string.Empty).Replace("]={", string.Empty);
                if (errorItemIds.Contains(itemId))
                {
                    isApply = true;
                    errorItemIds.Remove(itemId);
                }
            }
            else if (isApply
                && text.Contains("identifiedResourceName"))
            {
                isApply = false;
                var resourceName = Encoding.Default.GetString(Encoding.UTF8.GetBytes(text.Replace("identifiedResourceName", string.Empty).Replace(",", string.Empty) + "\n"));
                if (!string.IsNullOrEmpty(resourceName)
                    && (!resourceName.Contains("=\"\"")))
                {
                    builder.Append(itemId + resourceName);
                    helpCount++;
                }
            }
        }

        Debug.Log("There are " + helpCount + " resource name found.");

        File.WriteAllText("error_resource_name_fixed.txt", builder.ToString(), Encoding.UTF8);

        Debug.Log("'error_resource_name_fixed.txt' has been successfully created.");
    }
}

using EasyButtons;
using System.Text;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class RandomOptionMaker : MonoBehaviour
{
    // Constants

    const int MINIMUM_VALUE = 1;
    const int MAXIMUM_VALUE = 5;

    // Local classes / structs

    // Enums

    // Variables

    // Methods

    [Button()]
    public void ExportRefineDatabase()
    {
        List<string> randomOptions = new List<string>();

        var path = Application.dataPath + "/Assets/item_randomopt_db.yml";

        // Is file exists?
        if (!File.Exists(path))
            return;

        var randomOptionFile = File.ReadAllText(path);

        var randomOptionLines = randomOptionFile.Split('\n');

        for (int i = 0; i < randomOptionLines.Length; i++)
        {
            var text = randomOptionLines[i];

            text = CommentRemover.Fix(text);

            text = LineEndingsRemover.Fix(text);

            if (string.IsNullOrEmpty(text)
                || string.IsNullOrWhiteSpace(text)
                || text.Contains("BODY_ATTR_")
                || text.Contains("WEAPON_ATTR_"))
                continue;

            if (text.Contains("    Option: "))
                randomOptions.Add(text.Replace("    Option: ", string.Empty));
        }

        Debug.Log("There are " + randomOptions.Count + " random option.");

        StringBuilder builder = new StringBuilder();

        builder.Append("Header:\n");
        builder.Append("  Type: RANDOM_OPTION_GROUP\n");
        builder.Append("  Version: 1\n");
        builder.Append("\n");
        builder.Append("Body:\n");
        builder.Append("  - Id: 1\n");
        builder.Append("    Group: Group_1\n");
        builder.Append("    Slots:\n");

        for (int i = 0; i < 5; i++)
        {
            builder.Append("      - Slot: " + (i + 1) + "\n");
            builder.Append("        Options:\n");
            for (int j = 0; j < randomOptions.Count; j++)
            {
                builder.Append("          - Option: " + randomOptions[j] + "\n");
                builder.Append("            MinValue: " + MINIMUM_VALUE + "\n");
                builder.Append("            MaxValue: " + MAXIMUM_VALUE + "\n");
            }
        }

        File.WriteAllText("item_randomopt_group.yml", builder.ToString(), Encoding.UTF8);

        Debug.Log("'item_randomopt_group.yml' has been successfully created.");
    }
}

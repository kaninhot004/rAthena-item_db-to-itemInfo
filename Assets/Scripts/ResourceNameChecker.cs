using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ResourceNameChecker : MonoBehaviour
{
    [SerializeField] Button _checkButton;

    void Start()
    {
        if (_checkButton)
            _checkButton.onClick.AddListener(Check);
    }

    public void Check()
    {
        var path = Application.dataPath + "/Assets/resourceName.txt";

        // Is file exists?
        if (!File.Exists(path))
            return;

        var resourceNamesFile = File.ReadAllText(path, Encoding.UTF8);

        var resourceNames = resourceNamesFile.Split('\n');

        StringBuilder errorId = new StringBuilder();

        for (int i = 0; i < resourceNames.Length; i++)
        {
            var text = resourceNames[i];

            if (string.IsNullOrEmpty(text)
                || string.IsNullOrWhiteSpace(text))
                continue;

            text = LineEndingsRemover.Fix(text);

            if (text[0] == '/')
                continue;

            var texts = text.Split('=');

            var id = int.Parse(texts[0]);
            var name = texts[1].Replace("\"", string.Empty);

            if (!string.IsNullOrEmpty(name) && (name != "\"\""))
            {
                var url = Application.dataPath + "/Resources/collection/" + Encoding.Default.GetString(Encoding.UTF8.GetBytes(name)) + ".bmp";
                bool isExist = File.Exists(url);

                if (!isExist)
                    errorId.AppendLine(id.ToString("f0") + "=" + Encoding.Default.GetString(Encoding.UTF8.GetBytes(name)));
            }
        }

        File.WriteAllText("sprite-error-list.txt", errorId.ToString(), Encoding.UTF8);

        Debug.Log("There are " + errorId.Length + " sprite error");
    }
}

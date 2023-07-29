using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text;

public class ItemPreview : MonoBehaviour
{
    [SerializeField] GameObject _panelObject;
    [SerializeField] Button _closeButton;

    [SerializeField] InputField _itemIdInputField;
    [SerializeField] Button _loadButton;

    [SerializeField] Text _itemNameText;
    [SerializeField] Text[] _descriptionTexts;

    void Start()
    {
        _closeButton.onClick.AddListener(OnCloseButtonTap);
        _loadButton.onClick.AddListener(OnLoadButtonTap);

        CleanUpDescription();
    }
    void CleanUpDescription()
    {
        for (int i = 0; i < _descriptionTexts.Length; i++)
        {
            _descriptionTexts[i].gameObject.SetActive(false);
            _descriptionTexts[i].text = string.Empty;
        }
    }
    void OnLoadButtonTap()
    {
        if (!string.IsNullOrEmpty(_itemIdInputField.text))
        {
            var path = "itemInfo_Debug.txt";

            // Is file exists?
            if (!File.Exists(path))
            {
                _itemNameText.text = "Error: 'itemInfo_Debug.txt' not found";

                return;
            }

            CleanUpDescription();

            bool isFound = false;

            var itemId = int.Parse(_itemIdInputField.text);
            string itemName = string.Empty;
            string itemResourceName = string.Empty;
            List<string> itemDescription = new List<string>();
            string itemSlot = string.Empty;
            string itemClassNumber = string.Empty;

            var itemInformationsFile = File.ReadAllText(path);

            var itemInformations = itemInformationsFile.Split('\n');

            bool isDescriptionLines = false;

            for (int i = 0; i < itemInformations.Length; i++)
            {
                var line = itemInformations[i];

                if (!isFound
                    && line.Contains("[" + itemId + "] = {"))
                {
                    isFound = true;

                    continue;
                }

                if (isFound
                    && line.Contains("\t\tidentifiedDisplayName = \""))
                {
                    itemName = line.Replace("\t\tidentifiedDisplayName = \"", string.Empty).Replace("\",", string.Empty);

                    continue;
                }

                if (isFound
                    && line.Contains("\t\tidentifiedResourceName = \""))
                {
                    itemResourceName = line.Replace("\t\tidentifiedResourceName = \"", string.Empty).Replace("\",", string.Empty);

                    continue;
                }

                if (isFound
                    && line.Contains("\t\tidentifiedDescriptionName = {"))
                {
                    isDescriptionLines = true;

                    continue;
                }

                if (isFound
                    && isDescriptionLines
                    && (line != "\t\t},")
                    && (line != "\t\t\t\"\"")
                    )
                {
                    var description = line.Replace("\t\t\t\"", string.Empty).Replace("\",", string.Empty).Replace("^000000", "</color>");

                    while (description.Contains("^"))
                    {
                        var color = description.IndexOf("^") + 1;
                        var fullColorText = description.Substring(color, 6);
                        description = description.Replace("^" + fullColorText, "<color=#" + fullColorText + ">");
                    }

                    if (!string.IsNullOrEmpty(description))
                        itemDescription.Add(description);
                }

                if (isFound
                    && (line == "\t\t},"))
                {
                    isDescriptionLines = false;

                    continue;
                }

                if (isFound
                    && line.Contains("\t\tslotCount = "))
                {
                    itemSlot = line.Replace("\t\tslotCount = ", string.Empty).Replace(",", string.Empty);

                    continue;
                }

                if (isFound
                    && line.Contains("\t\tClassNum = "))
                {
                    itemClassNumber = line.Replace("\t\tClassNum = ", string.Empty).Replace(",", string.Empty);

                    break;
                }
            }

            _itemNameText.text = itemName
                + ((itemSlot != "0") ? "[" + itemSlot + "]" : string.Empty)
                + ((itemClassNumber != "0") ? " | View: " + itemClassNumber : string.Empty)
                + " | <color=grey>" + itemResourceName + "</color>"
                ;

            int printCount = 0;
            int column = 0;

            for (int i = 0; i < itemDescription.Count; i++)
            {
                _descriptionTexts[column].text += itemDescription[i] + "\n";
                _descriptionTexts[column].gameObject.SetActive(true);
                printCount++;
                if (printCount >= 34)
                {
                    column++;
                    printCount = 0;
                    continue;
                }
            }
        }
    }
    void OnCloseButtonTap()
    {
        _panelObject.SetActive(false);
    }

    public void Setup()
    {
        _panelObject.SetActive(true);
    }
}

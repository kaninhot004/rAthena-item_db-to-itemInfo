using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text;
using TMPro;

public class ItemPreview : MonoBehaviour
{
    const string FONT_SIZE_KEY = "FONT_SIZE";

    [SerializeField] GameObject _panelObject;
    [SerializeField] Button _closeButton;

    [SerializeField] InputField _itemIdInputField;
    [SerializeField] InputField _fontSizeInputField;

    [SerializeField] Text _itemNameText;
    [SerializeField] TextMeshProUGUI _descriptionText;
    [SerializeField] TextMeshProUGUI[] _descriptionTexts;
    [SerializeField] RectTransform[] _descriptionBackgrounds;

    [SerializeField] Image _collectionImage;

    void Start()
    {
        _closeButton.onClick.AddListener(OnCloseButtonTap);

        _itemIdInputField.onEndEdit.AddListener((text) =>
        {
            OnLoadButtonTap(text);
        });

        _fontSizeInputField.onEndEdit.AddListener((text) =>
        {
            if (!string.IsNullOrEmpty(text))
                FontSizeUpdate(Mathf.Clamp(int.Parse(text), 11, 300), true);
        });

        if (PlayerPrefs.HasKey(FONT_SIZE_KEY))
            FontSizeUpdate(PlayerPrefs.GetInt(FONT_SIZE_KEY), false);

        CleanUpDescription();
    }
    void FontSizeUpdate(int size, bool isSetSave)
    {
        if (isSetSave)
        {
            PlayerPrefs.SetInt(FONT_SIZE_KEY, size);
            PlayerPrefs.Save();
        }

        _fontSizeInputField.text = size.ToString();

        for (int i = 0; i < _descriptionTexts.Length; i++)
        {
            _descriptionTexts[i].fontSize = size;
            _descriptionTexts[i].rectTransform.sizeDelta = new Vector2(185 + (15 * (size - 11)), 405);
            _descriptionBackgrounds[i].sizeDelta = new Vector2(185 + (15 * (size - 11)), 405);
        }
    }
    void CleanUpDescription()
    {
        _descriptionText.text = string.Empty;
    }
    void OnLoadButtonTap(string text)
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
            StringBuilder itemDescriptionOneLine = new StringBuilder();
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
                    {
                        itemDescription.Add(description);
                        itemDescriptionOneLine.AppendLine(description);
                    }
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
                //+ " | <color=grey>" + itemResourceName + "</color>"
                ;

            _descriptionText.text = itemDescriptionOneLine.ToString();

            _collectionImage.sprite = Resources.Load<Sprite>("collection/" + Encoding.Default.GetString(Encoding.UTF8.GetBytes(itemResourceName)));
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

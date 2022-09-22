using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    const string ITEM_ID_TOGGLE = "IsPrintItemId";
    const string SUB_TYPE_TOGGLE = "IsPrintSubType";

    [SerializeField] Button _openSettingButton;
    [SerializeField] GameObject _settingCanvasObject;
    [SerializeField] Button _closeSettingButton;

    [SerializeField] Toggle _printZeroValueToggle;
    [SerializeField] Toggle _onlyPrintTestItemToggle;
    [SerializeField] Toggle _onlyPrintCustomItemToggle;
    [SerializeField] Toggle _randomResourceNameToggle;
    [SerializeField] Toggle _randomResourceNameForCustomItemOnlyToggle;
    [SerializeField] Toggle _newLineForAvailableJobToggle;
    [SerializeField] Toggle _newLineForAvailableClassToggle;
    [SerializeField] Toggle _itemIdToggle;
    [SerializeField] Toggle _subTypeToggle;

    [SerializeField] Converter _converter;

    void Awake()
    {
        _openSettingButton.onClick.AddListener(OnOpenSettingButtonTap);
        _settingCanvasObject.SetActive(false);
        _closeSettingButton.onClick.AddListener(OnCloseSettingButtonTap);

        // Load Toggle Save
        _printZeroValueToggle.isOn = (PlayerPrefs.GetInt("IsZeroValuePrintAble") == 1) ? true : false;
        _onlyPrintTestItemToggle.isOn = (PlayerPrefs.GetInt("IsOnlyUseTestTextAsset") == 1) ? true : false;
        _onlyPrintCustomItemToggle.isOn = (PlayerPrefs.GetInt("IsOnlyUseCustomTextAsset") == 1) ? true : false;
        _randomResourceNameToggle.isOn = (PlayerPrefs.GetInt("IsRandomResourceName") == 1) ? true : false;
        _randomResourceNameForCustomItemOnlyToggle.isOn = (PlayerPrefs.GetInt("IsRandomResourceNameForCustomTextAssetOnly") == 1) ? true : false;
        _newLineForAvailableJobToggle.isOn = (PlayerPrefs.GetInt("IsUseNewLineInsteadOfCommaForAvailableJob") == 1) ? true : false;
        _newLineForAvailableClassToggle.isOn = (PlayerPrefs.GetInt("IsUseNewLineInsteadOfCommaForAvailableClass") == 1) ? true : false;
        _itemIdToggle.isOn = (PlayerPrefs.GetInt(ITEM_ID_TOGGLE) == 1) ? true : false;
        _subTypeToggle.isOn = (PlayerPrefs.GetInt(SUB_TYPE_TOGGLE) == 1) ? true : false;
        _converter.IsZeroValuePrintAble = _printZeroValueToggle.isOn;
        _converter.IsOnlyUseTestTextAsset = _onlyPrintTestItemToggle.isOn;
        _converter.IsOnlyUseCustomTextAsset = _onlyPrintCustomItemToggle.isOn;
        _converter.IsRandomResourceName = _randomResourceNameToggle.isOn;
        _converter.IsRandomResourceNameForCustomTextAssetOnly = _randomResourceNameForCustomItemOnlyToggle.isOn;
        _converter.IsUseNewLineInsteadOfCommaForAvailableJob = _newLineForAvailableJobToggle.isOn;
        _converter.IsUseNewLineInsteadOfCommaForAvailableClass = _newLineForAvailableClassToggle.isOn;
        _converter.IsHideItemId = _itemIdToggle.isOn;
        _converter.IsHideSubType = _subTypeToggle.isOn;

        // Add Toggle Listener
        _printZeroValueToggle.onValueChanged.AddListener(OnPrintZeroValueToggle);
        _onlyPrintTestItemToggle.onValueChanged.AddListener(OnOnlyPrintTestItemToggle);
        _onlyPrintCustomItemToggle.onValueChanged.AddListener(OnOnlyPrintCustomItemToggle);
        _randomResourceNameToggle.onValueChanged.AddListener(OnRandomResourceNameToggle);
        _randomResourceNameForCustomItemOnlyToggle.onValueChanged.AddListener(OnRandomResourceNameForCustomItemOnlyToggle);
        _newLineForAvailableJobToggle.onValueChanged.AddListener(OnNewLineForAvailableJobToggle);
        _newLineForAvailableClassToggle.onValueChanged.AddListener(OnNewLineForAvailableClassToggle);
        _itemIdToggle.onValueChanged.AddListener(OnItemIdToggle);
        _subTypeToggle.onValueChanged.AddListener(OnSubTypeToggle);
    }
    void OnSubTypeToggle(bool isOn)
    {
        PlayerPrefs.SetInt(SUB_TYPE_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsHideSubType = isOn;
    }
    void OnItemIdToggle(bool isOn)
    {
        PlayerPrefs.SetInt(ITEM_ID_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsHideItemId = isOn;
    }
    void OnNewLineForAvailableClassToggle(bool isOn)
    {
        PlayerPrefs.SetInt("IsUseNewLineInsteadOfCommaForAvailableClass", isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsUseNewLineInsteadOfCommaForAvailableClass = isOn;
    }
    void OnNewLineForAvailableJobToggle(bool isOn)
    {
        PlayerPrefs.SetInt("IsUseNewLineInsteadOfCommaForAvailableJob", isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsUseNewLineInsteadOfCommaForAvailableJob = isOn;
    }
    void OnRandomResourceNameForCustomItemOnlyToggle(bool isOn)
    {
        PlayerPrefs.SetInt("IsRandomResourceNameForCustomTextAssetOnly", isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsRandomResourceNameForCustomTextAssetOnly = isOn;
    }
    void OnRandomResourceNameToggle(bool isOn)
    {
        PlayerPrefs.SetInt("IsRandomResourceName", isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsRandomResourceName = isOn;
    }
    void OnOnlyPrintCustomItemToggle(bool isOn)
    {
        PlayerPrefs.SetInt("IsOnlyUseCustomTextAsset", isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsOnlyUseCustomTextAsset = isOn;
    }
    void OnOnlyPrintTestItemToggle(bool isOn)
    {
        PlayerPrefs.SetInt("IsOnlyUseTestTextAsset", isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsOnlyUseTestTextAsset = isOn;
    }
    void OnPrintZeroValueToggle(bool isOn)
    {
        PlayerPrefs.SetInt("IsZeroValuePrintAble", isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsZeroValuePrintAble = isOn;
    }

    void OnOpenSettingButtonTap()
    {
        _settingCanvasObject.SetActive(true);
    }
    void OnCloseSettingButtonTap()
    {
        _settingCanvasObject.SetActive(false);
    }
}

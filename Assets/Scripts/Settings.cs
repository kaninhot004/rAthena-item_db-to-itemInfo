using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Button _openSettingButton;
    [SerializeField] GameObject _settingCanvasObject;
    [SerializeField] Button _closeSettingButton;

    [SerializeField] Toggle _seperateItemBonusByNewLineToggle;

    [SerializeField] Converter _converter;

    void Awake()
    {
        _openSettingButton.onClick.AddListener(OnOpenSettingButtonTap);
        _settingCanvasObject.SetActive(false);
        _closeSettingButton.onClick.AddListener(OnCloseSettingButtonTap);
        _seperateItemBonusByNewLineToggle.onValueChanged.AddListener(OnSeperateItemBonusByNewLinePressed);
    }
    void OnSeperateItemBonusByNewLinePressed(bool isOn)
    {
        _converter.IsSeperateItemBonusByNewLineToggle = isOn;
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

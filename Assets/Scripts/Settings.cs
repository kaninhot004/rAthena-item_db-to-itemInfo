using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    const string ITEM_ID_TOGGLE = "IsPrintItemId";
    const string SUB_TYPE_TOGGLE = "IsPrintSubType";

    const string ITEM_LINK_TOGGLE = "IsUseItemLink";
    const string REFINABLE_TOGGLE = "IsSkipRefinable";
    const string GRADABLE_TOGGLE = "IsSkipGradable";
    const string SKIP_MIN_LV_TOGGLE = "IsSkipMinLv";
    const string SKIP_MAX_LV_TOGGLE = "IsSkipMaxLv";
    const string SKIP_GENDER_TOGGLE = "IsSkipGender";
    const string SKIP_CLASS_TOGGLE = "IsSkipClass";
    const string SKIP_JOB_TOGGLE = "IsSkipJob";
    const string ENCHANTMENT_DOUBLE_CLICK_TOGGLE = "IsEnchantmentDoubleClickable";
    const string FORCE_EQUIPMENT_NO_VALUE_TOGGLE = "IsEquipmentNoValue";
    const string FORCE_ITEM_NO_BONUS_NO_COMBO_TOGGLE = "IsItemNoBonusNoCombo";
    const string SKIP_NORMAL_GAME_ITEM_COMBO_TOGGLE = "IsSkipNormalGameItemCombo";
    const string REMOVE_BRACKETS_TOGGLE = "IsRemoveBrackets";
    const string REMOVE_COMBO_ITEM_REQ_TOGGLE = "IsRemoveComboItemReq";

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

    [SerializeField] Toggle _itemLinkToggle;
    [SerializeField] Toggle _refinableToggle;
    [SerializeField] Toggle _gradableToggle;
    [SerializeField] Toggle _skipMinLvToggle;
    [SerializeField] Toggle _skipMaxLvToggle;
    [SerializeField] Toggle _skipGenderToggle;
    [SerializeField] Toggle _skipClassToggle;
    [SerializeField] Toggle _skipJobToggle;
    [SerializeField] Toggle _enchantmentClickableToggle;
    [SerializeField] Toggle _forceEquipmentNoValueToggle;
    [SerializeField] Toggle _forceItemNoBonusNoComboToggle;
    [SerializeField] Toggle _skipNormalGameItemComboToggle;
    [SerializeField] Toggle _removeBracketsToggle;
    [SerializeField] Toggle _removeComboItemReqToggle;

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
        _itemLinkToggle.isOn = (PlayerPrefs.GetInt(ITEM_LINK_TOGGLE) == 1) ? true : false;
        _refinableToggle.isOn = (PlayerPrefs.GetInt(REFINABLE_TOGGLE) == 1) ? true : false;
        _gradableToggle.isOn = (PlayerPrefs.GetInt(GRADABLE_TOGGLE) == 1) ? true : false;
        _skipMinLvToggle.isOn = (PlayerPrefs.GetInt(SKIP_MIN_LV_TOGGLE) == 1) ? true : false;
        _skipMaxLvToggle.isOn = (PlayerPrefs.GetInt(SKIP_MAX_LV_TOGGLE) == 1) ? true : false;
        _skipGenderToggle.isOn = (PlayerPrefs.GetInt(SKIP_GENDER_TOGGLE) == 1) ? true : false;
        _skipClassToggle.isOn = (PlayerPrefs.GetInt(SKIP_CLASS_TOGGLE) == 1) ? true : false;
        _skipJobToggle.isOn = (PlayerPrefs.GetInt(SKIP_JOB_TOGGLE) == 1) ? true : false;
        _enchantmentClickableToggle.isOn = (PlayerPrefs.GetInt(ENCHANTMENT_DOUBLE_CLICK_TOGGLE) == 1) ? true : false;
        _forceEquipmentNoValueToggle.isOn = (PlayerPrefs.GetInt(FORCE_EQUIPMENT_NO_VALUE_TOGGLE) == 1) ? true : false;
        _forceItemNoBonusNoComboToggle.isOn = (PlayerPrefs.GetInt(FORCE_ITEM_NO_BONUS_NO_COMBO_TOGGLE) == 1) ? true : false;
        _skipNormalGameItemComboToggle.isOn = (PlayerPrefs.GetInt(SKIP_NORMAL_GAME_ITEM_COMBO_TOGGLE) == 1) ? true : false;
        _removeBracketsToggle.isOn = (PlayerPrefs.GetInt(REMOVE_BRACKETS_TOGGLE) == 1) ? true : false;
        _removeComboItemReqToggle.isOn = (PlayerPrefs.GetInt(REMOVE_COMBO_ITEM_REQ_TOGGLE) == 1) ? true : false;
        _converter.IsZeroValuePrintAble = _printZeroValueToggle.isOn;
        _converter.IsOnlyUseTestTextAsset = _onlyPrintTestItemToggle.isOn;
        _converter.IsOnlyUseCustomTextAsset = _onlyPrintCustomItemToggle.isOn;
        _converter.IsRandomResourceName = _randomResourceNameToggle.isOn;
        _converter.IsRandomResourceNameForCustomTextAssetOnly = _randomResourceNameForCustomItemOnlyToggle.isOn;
        _converter.IsUseNewLineInsteadOfCommaForAvailableJob = _newLineForAvailableJobToggle.isOn;
        _converter.IsUseNewLineInsteadOfCommaForAvailableClass = _newLineForAvailableClassToggle.isOn;
        _converter.IsHideItemId = _itemIdToggle.isOn;
        _converter.IsHideSubType = _subTypeToggle.isOn;
        _converter.IsItemLink = _itemLinkToggle.isOn;
        _converter.IsHideRefinable = _refinableToggle.isOn;
        _converter.IsHideGradable = _gradableToggle.isOn;
        _converter.IsSkipEquipMinimumLevel = _skipMinLvToggle.isOn;
        _converter.IsSkipEquipMaximumLevel = _skipMaxLvToggle.isOn;
        _converter.IsSkipItemGender = _skipGenderToggle.isOn;
        _converter.IsSkipItemClass = _skipClassToggle.isOn;
        _converter.IsSkipItemJob = _skipJobToggle.isOn;
        _converter.IsEnchantmentAbleToUse = _enchantmentClickableToggle.isOn;
        _converter.IsEquipmentNoValue = _forceEquipmentNoValueToggle.isOn;
        _converter.IsItemNoBonus = _forceItemNoBonusNoComboToggle.isOn;
        _converter.IsSkipNormalEquipEtcCombo = _skipNormalGameItemComboToggle.isOn;
        _converter.IsRemoveBrackets = _removeBracketsToggle.isOn;
        _converter.IsRemoveComboItemReq = _removeComboItemReqToggle.isOn;

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
        _itemLinkToggle.onValueChanged.AddListener(OnItemLinkToggle);
        _refinableToggle.onValueChanged.AddListener(OnRefinableToggle);
        _gradableToggle.onValueChanged.AddListener(OnGradableToggle);
        _skipMinLvToggle.onValueChanged.AddListener(OnSkipMinLvToggle);
        _skipMaxLvToggle.onValueChanged.AddListener(OnSkipMaxLvToggle);
        _skipGenderToggle.onValueChanged.AddListener(OnSkipGenderToggle);
        _skipClassToggle.onValueChanged.AddListener(OnSkipClassToggle);
        _skipJobToggle.onValueChanged.AddListener(OnSkipJobToggle);
        _enchantmentClickableToggle.onValueChanged.AddListener(OnEnchantmentClickableToggle);
        _forceEquipmentNoValueToggle.onValueChanged.AddListener(OnForceEquipmentNoValueToggle);
        _forceItemNoBonusNoComboToggle.onValueChanged.AddListener(OnForceItemNoBonusNoComboToggle);
        _skipNormalGameItemComboToggle.onValueChanged.AddListener(OnSkipNormalGameItemComboToggle);
        _removeBracketsToggle.onValueChanged.AddListener(OnRemoveBracketsToggle);
        _removeComboItemReqToggle.onValueChanged.AddListener(OnRemoveComboItemReqToggle);
    }
    void OnRemoveComboItemReqToggle(bool isOn)
    {
        PlayerPrefs.SetInt(REMOVE_COMBO_ITEM_REQ_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsRemoveComboItemReq = isOn;
    }
    void OnRemoveBracketsToggle(bool isOn)
    {
        PlayerPrefs.SetInt(REMOVE_BRACKETS_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsRemoveBrackets = isOn;
    }
    void OnSkipNormalGameItemComboToggle(bool isOn)
    {
        PlayerPrefs.SetInt(SKIP_NORMAL_GAME_ITEM_COMBO_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsSkipNormalEquipEtcCombo = isOn;
    }
    void OnForceItemNoBonusNoComboToggle(bool isOn)
    {
        PlayerPrefs.SetInt(FORCE_ITEM_NO_BONUS_NO_COMBO_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsItemNoBonus = isOn;
    }
    void OnForceEquipmentNoValueToggle(bool isOn)
    {
        PlayerPrefs.SetInt(FORCE_EQUIPMENT_NO_VALUE_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsEquipmentNoValue = isOn;
    }
    void OnEnchantmentClickableToggle(bool isOn)
    {
        PlayerPrefs.SetInt(ENCHANTMENT_DOUBLE_CLICK_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsEnchantmentAbleToUse = isOn;
    }
    void OnSkipGenderToggle(bool isOn)
    {
        PlayerPrefs.SetInt(SKIP_GENDER_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsSkipItemGender = isOn;
    }
    void OnSkipClassToggle(bool isOn)
    {
        PlayerPrefs.SetInt(SKIP_CLASS_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsSkipItemClass = isOn;
    }
    void OnSkipJobToggle(bool isOn)
    {
        PlayerPrefs.SetInt(SKIP_JOB_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsSkipItemJob = isOn;
    }
    void OnSkipMinLvToggle(bool isOn)
    {
        PlayerPrefs.SetInt(SKIP_MIN_LV_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsSkipEquipMinimumLevel = isOn;
    }
    void OnSkipMaxLvToggle(bool isOn)
    {
        PlayerPrefs.SetInt(SKIP_MAX_LV_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsSkipEquipMaximumLevel = isOn;
    }
    void OnGradableToggle(bool isOn)
    {
        PlayerPrefs.SetInt(GRADABLE_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsHideGradable = isOn;
    }
    void OnRefinableToggle(bool isOn)
    {
        PlayerPrefs.SetInt(REFINABLE_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsHideRefinable = isOn;
    }
    void OnItemLinkToggle(bool isOn)
    {
        PlayerPrefs.SetInt(ITEM_LINK_TOGGLE, isOn ? 1 : 0);
        PlayerPrefs.Save();
        _converter.IsItemLink = isOn;
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

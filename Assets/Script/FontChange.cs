using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;


public class FontChange : MonoBehaviour
{
    public TMP_Dropdown langaugeDropdown;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            LoadValue();
        }
        else
        {
            
            UserLocalization(0);
        }
        RefreshSetting();
    }
    public void UserLocalization(int index)
    {
        langaugeDropdown.value = index;
        LocalizationSettings.SelectedLocale =
            LocalizationSettings.AvailableLocales.Locales[index];
        SaveValue(index);
    }
    private void SaveValue(int index)
    {
        PlayerPrefs.SetInt("Language", index);
    }
    private void LoadValue()
    {
        langaugeDropdown.value = PlayerPrefs.GetInt("Language");
        UserLocalization(PlayerPrefs.GetInt("Language"));
    }
    public void RefreshSetting()
    {
        langaugeDropdown.RefreshShownValue();
    }
}

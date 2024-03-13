using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GraphicSetting : MonoBehaviour
{
    public Toggle screenMode;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;

    Resolution[] resolutions;
    List<Resolution> res = new();
    List<string> options = new();

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int index = 0;
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width % 16 == 0 && resolutions[i].height % 9 == 0)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                if (!options.Contains(option))
                {
                    options.Add(option);
                    res.Add(resolutions[i]);
                }
                if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = index;
                }
                index++;
            }
        }
        resolutionDropdown.AddOptions(options);

        if (PlayerPrefs.HasKey("Resolution"))
        {
            LoadValue();
            RefreshSetting();
        }
        else
        {
            resolutionDropdown.value = currentResolutionIndex;
            SetResolution(currentResolutionIndex);
            SetQuality(2);
            SetFullScreen(true);
            RefreshSetting();
        }
    }
    public void RefreshSetting()
    {
        resolutionDropdown.RefreshShownValue();
        qualityDropdown.RefreshShownValue();
    }
    // �ػ� ����
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = res[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
    }
    // �׷��� ���� low standard high �⺻ ����Ƽ ����
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }
    // ��üȭ�� ����. üũ�ڽ��� üũ�Ҷ� ����
    public void SetFullScreen(bool isFull)
    {
        Screen.fullScreen = isFull;
        PlayerPrefs.SetInt("ScreenMode", System.Convert.ToInt32(isFull));
    }
    private void LoadValue()
    {
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        qualityDropdown.value = PlayerPrefs.GetInt("Quality");
        screenMode.isOn = System.Convert.ToBoolean(PlayerPrefs.GetInt("ScreenMode"));
    }
}

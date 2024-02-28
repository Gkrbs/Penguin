using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GraphicSetting : MonoBehaviour
{
    public Toggle toggle;
    public Dropdown resolutionDropdown;

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
                index++;
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
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // 세이브 데이터 있을 때 저장된 데이터를 읽어서 세팅해야함
        SetResolution(currentResolutionIndex);

        SetFullScreen(toggle.isOn);
        // .
    }

    // 해상도 설정
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = res[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    // 그래픽 설정 low standard high 기본 유니티 제공
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    // 전체화면 설정. 체크박스를 체크할때 적용
    public void SetFullScreen(bool isFull)
    {
        Screen.fullScreen = isFull;
    }
}

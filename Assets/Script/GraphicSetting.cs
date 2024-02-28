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

        // ���̺� ������ ���� �� ����� �����͸� �о �����ؾ���
        SetResolution(currentResolutionIndex);

        SetFullScreen(toggle.isOn);
        // .
    }

    // �ػ� ����
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = res[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    // �׷��� ���� low standard high �⺻ ����Ƽ ����
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    // ��üȭ�� ����. üũ�ڽ��� üũ�Ҷ� ����
    public void SetFullScreen(bool isFull)
    {
        Screen.fullScreen = isFull;
    }
}

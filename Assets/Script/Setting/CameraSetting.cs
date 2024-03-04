using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class CameraSetting : MonoBehaviour
{
    public CinemachineFreeLook cinemachineFreeLook;

    public Slider fovSlider;
    public Slider xAxisSlider;
    public Slider yAxisSlider;

    [SerializeField] float defaultXAxis;
    [SerializeField] float defaultYAxis;

    private void Start()
    {
        defaultXAxis = cinemachineFreeLook.m_XAxis.m_MaxSpeed;
        defaultYAxis = cinemachineFreeLook.m_YAxis.m_MaxSpeed;
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadValue();
        }
        else
        {
            SetFov();
            SetXAxisSpeed();
            SetYAxisSpeed();
        }
    }
    public void SetFov()
    {
        float value = fovSlider.value;
        cinemachineFreeLook.m_Lens.FieldOfView = value;
        PlayerPrefs.SetFloat("FOV", value);

    }
    public void SetXAxisSpeed()
    {
        float value = xAxisSlider.value;
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = defaultXAxis * value;
        PlayerPrefs.SetFloat("XSpeed", value);
    }
    public void SetYAxisSpeed()
    {
        float value = yAxisSlider.value;
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = defaultYAxis * value;
        PlayerPrefs.SetFloat("YSpeed", value);
    }
    private void LoadValue()
    {
        fovSlider.value = PlayerPrefs.GetFloat("FOV");
        xAxisSlider.value = PlayerPrefs.GetFloat("XSpeed");
        yAxisSlider.value = PlayerPrefs.GetFloat("YSpeed");
    }
}

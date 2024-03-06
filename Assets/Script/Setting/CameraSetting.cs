using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Lightbug.CharacterControllerPro.Demo;

public class CameraSetting : MonoBehaviour
{
    public CinemachineFreeLook cinemachineFreeLook;
    public Camera3D came3d;
    public Slider fovSlider;
    public Slider xAxisSlider;
    public Slider yAxisSlider;

    [SerializeField] float defaultXAxis;
    [SerializeField] float defaultYAxis;

    private void Start()
    {
        defaultXAxis = came3d.yawSpeed;//cinemachineFreeLook.m_XAxis.m_MaxSpeed;
        defaultYAxis = came3d.pitchSpeed;//cinemachineFreeLook.m_YAxis.m_MaxSpeed;
        if (PlayerPrefs.HasKey("FOV"))
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
        came3d.yawSpeed = defaultXAxis * value;
        //cinemachineFreeLook.m_XAxis.m_MaxSpeed = defaultXAxis * value;
        PlayerPrefs.SetFloat("XSpeed", value);
    }
    public void SetYAxisSpeed()
    {
        float value = yAxisSlider.value;
        came3d.pitchSpeed = defaultYAxis * value;
        //cinemachineFreeLook.m_YAxis.m_MaxSpeed = defaultYAxis * value;
        PlayerPrefs.SetFloat("YSpeed", value);
    }
    private void LoadValue()
    {
        fovSlider.value = PlayerPrefs.GetFloat("FOV");
        xAxisSlider.value = PlayerPrefs.GetFloat("XSpeed");
        yAxisSlider.value = PlayerPrefs.GetFloat("YSpeed");
    }
}

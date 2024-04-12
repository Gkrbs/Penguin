using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Lightbug.CharacterControllerPro.Demo;

public class CameraSetting : MonoBehaviour
{
    public Camera3D came3d;
    public Slider xAxisSlider;
    public Slider yAxisSlider;

    [SerializeField] float defaultXAxis;
    [SerializeField] float defaultYAxis;

    private void Start()
    {
        if (PlayerPrefs.HasKey("XSpeed"))
        {
            LoadValue();
        }
        else
        {
            if (came3d != null)
            {
                SetXAxisSpeed();
                SetYAxisSpeed();
            }
        }
    }

    public void SetXAxisSpeed()
    {
        float value = xAxisSlider.value;
        if (came3d != null)
            came3d.yawSpeed = defaultXAxis * value;
        //cinemachineFreeLook.m_XAxis.m_MaxSpeed = defaultXAxis * value;
        PlayerPrefs.SetFloat("XSpeed", value);
    }
    public void SetYAxisSpeed()
    {
        float value = yAxisSlider.value;
        if (came3d != null)
            came3d.pitchSpeed = defaultYAxis * value;
        //cinemachineFreeLook.m_YAxis.m_MaxSpeed = defaultYAxis * value;
        PlayerPrefs.SetFloat("YSpeed", value);
    }
    private void LoadValue()
    {
        xAxisSlider.value = PlayerPrefs.GetFloat("XSpeed");
        yAxisSlider.value = PlayerPrefs.GetFloat("YSpeed");
    }
}

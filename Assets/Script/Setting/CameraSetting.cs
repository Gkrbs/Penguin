using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using Lightbug.CharacterControllerPro.Demo;

public class CameraSetting : MonoBehaviour
{
    public Camera3D came3d;
    public Slider xAxisSlider;
    public Slider yAxisSlider;
    
    public TMP_Text xAxisValue;
    public TMP_Text yAxisValue;

    public GrapplingObj grapplingObj;

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
        xAxisValue.text = (Mathf.Floor(value * 100f) / 100f).ToString("F2");
        if (came3d != null)
            came3d.yawSpeed = defaultXAxis * value;
        PlayerPrefs.SetFloat("XSpeed", value);
    }
    public void SetYAxisSpeed()
    {
        float value = yAxisSlider.value;
        yAxisValue.text = (Mathf.Floor(value * 100f) / 100f).ToString("F2");
        if (came3d != null)
            came3d.pitchSpeed = defaultYAxis * value;
        PlayerPrefs.SetFloat("YSpeed", value);
    }
    public void SetGrappleMode(bool isHolding)
    {
        if(grapplingObj !=null)
            grapplingObj.IS_HOLDING = isHolding;
        PlayerPrefs.SetInt("GrappleMode", System.Convert.ToInt32(isHolding));
    }
    private void LoadValue()
    {
        xAxisSlider.value = PlayerPrefs.GetFloat("XSpeed");
        yAxisSlider.value = PlayerPrefs.GetFloat("YSpeed");
        if (grapplingObj != null)
            grapplingObj.IS_HOLDING = System.Convert.ToBoolean(PlayerPrefs.GetInt("GrappleMode"));
    }
}

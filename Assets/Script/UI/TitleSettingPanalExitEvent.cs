using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSettingPanalExitEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject _setting_panel;
    public KeyCode code; 
    public void Exit()
    {
        if(_setting_panel.activeSelf)
            _setting_panel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(code))
        {
            Exit();
        }
    }
}

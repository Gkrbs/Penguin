using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSettingPanalExitEvent : MonoBehaviour
{
    public KeyCode code;
    [SerializeField]
    private GameObject _setting_panel, _level_panel;
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private AudioSource _effect_audio;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void Start()
    {
    
        SoundManager.instance?.PlayLoop(_audio,"TitleTema");
        SoundManager.instance?.PlayLoop(_effect_audio, "WindSound");
    }
    public void Exit()
    {
        if (_setting_panel.activeSelf)
            _setting_panel.SetActive(false);
        else if (_level_panel.activeSelf)
            _level_panel.SetActive(false);
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

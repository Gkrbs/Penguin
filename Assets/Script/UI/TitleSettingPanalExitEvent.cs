using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSettingPanalExitEvent : MonoBehaviour
{
    public KeyCode code;
    [SerializeField]
    private GameObject _setting_panel;
    private AudioSource _audio;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
    
        SoundManager.instance?.PlayLoop(_audio,"TitleTema");
    }
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

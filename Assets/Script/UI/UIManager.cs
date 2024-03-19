using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private bool _open_menu = false;
    private float _time_sec = 0.0f;
    [SerializeField]
    private GameObject _menu_panel;
    [SerializeField]
    private GameObject _setting_panel;
    [SerializeField]
    private GameObject _timer;
    private TMP_Text _timer_text;

    private void Start()
    {
        _time_sec = 0.0f;
        _timer_text = _timer.GetComponent<TMP_Text>();
    }

    private void VisibleMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_open_menu)
            {

                if (_setting_panel.activeSelf)
                {
                    _setting_panel.SetActive(false);

                }
                if (_menu_panel.activeSelf)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    _menu_panel.SetActive(false);
                    Time.timeScale = 1.0f;
                }
            }
            else
            {
                if (!_menu_panel.activeSelf)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    _menu_panel.SetActive(true);
                    Time.timeScale = 0.0f;
                }
            }
        }
    }
    public void SetTimeText()
    {
        int hour = (int)(_time_sec / 3600f);
        int minute = (int)(_time_sec / 60f);
        float second = _time_sec % 60f;
        string text = "Timer - " +hour.ToString("D2") + ":"+ minute.ToString("D2") + ":" + second.ToString("F2");
        _timer_text.text = text;
        _time_sec += Time.deltaTime;

    }
    // Update is called once per frame
    void Update()
    {
        VisibleMenu();
        SetTimeText();
    }
}

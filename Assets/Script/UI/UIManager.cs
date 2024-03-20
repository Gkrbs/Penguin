using Lightbug.CharacterControllerPro.Demo;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private bool _open_menu = false;
    private float _time_sec = 0.0f;
    //private const int disable_img_cod = 858585;

    [SerializeField]
    private GameObject _menu_panel;
    [SerializeField]
    private GameObject _setting_panel;
    [SerializeField]
    private GameObject _timer;
    private TMP_Text _timer_text;
    [SerializeField]
    private GameObject _jetpack_img, _create_wall_img;
    [SerializeField]
    private NormalMovement nm;
    [SerializeField]
    private KeyCode code;

    private void Start()
    {
        _time_sec = 0.0f;
        _timer_text = _timer.GetComponent<TMP_Text>();
    }

    private void VisibleMenu()
    {
        if (Input.GetKeyDown(code))
        {
            if (_open_menu)
            {

                if (_setting_panel.activeSelf)
                {
                    _menu_panel.SetActive(true);
                    return;

                }
                if (_menu_panel.activeSelf)
                {
                    DisbledMenuPopUp();
                }
            }
            else
            {
                if (!_menu_panel.activeSelf)
                {
                    _open_menu = true;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    _menu_panel.SetActive(true);
                    Time.timeScale = 0.0f;
                }
            }
        }
    }
    public void DisbledMenuPopUp()
    {
        _open_menu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _menu_panel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void SetTimeText()
    {
        int hour = (int)(_time_sec / 3600f);
        int minute = (int)(_time_sec / 60f);
        int second = (int)(_time_sec % 60f);
        string text = "";
        if(hour < 10)
            text = "Timer - " + hour.ToString("D2") + ":" + minute.ToString("D2") + ":" + second.ToString("D2");
        else
            text = "Timer - " + hour.ToString() + ":" + minute.ToString("D2") + ":" + second.ToString("D2");

        _timer_text.text = text;
        _time_sec += Time.deltaTime;

    }

    private void VisibleItemImage()
    {
        if (nm.jetpackCount > 0)
        {
            _jetpack_img.GetComponent<Image>().color = Color.white;
            _jetpack_img.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.6f);
        }
        else
        {
            _jetpack_img.GetComponent<Image>().color = Color.gray;
            _jetpack_img.transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.6f);
        }

        if (nm.wallCount > 0)
        {
            _create_wall_img.GetComponent<Image>().color = Color.white;
            _create_wall_img.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.6f);
        }
        else
        {
            _create_wall_img.GetComponent<Image>().color = Color.gray;
            _create_wall_img.transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,0.6f);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        VisibleMenu();
        SetTimeText();
        VisibleItemImage();
    }
}

using Lightbug.CharacterControllerPro.Demo;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private bool _open_menu = false;

    [SerializeField]
    private GameObject _easy_level_contol_key_text;
    [SerializeField]
    private GameObject _menu_panel;
    [SerializeField]
    private GameObject _setting_panel;
    [SerializeField]
    private GameObject _jetpack_img, _create_wall_img;
    [SerializeField]
    private NormalMovement nm;
    [SerializeField]
    private KeyCode code;

    [SerializeField]
    private Sprite _enable_jetpack_img, _disable_jetpack_img, _enable_create_wall_img, _disable_create_wall_img;
    public bool OPEN_MENU
    {
        get { return _open_menu; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = GetComponent<UIManager>();
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        if (Timer.instance != null)
        {
            Timer.instance?.ResetTimer();
            Timer.instance?.StartTimer();
        }
        _easy_level_contol_key_text.SetActive(false);

        if (GameManager.instance == null) return;
        if (GameManager.instance.SELECTED_LEVEL == GameManager.LEVELS.EASY)
            _easy_level_contol_key_text.SetActive(true);
    }

    private void VisibleMenu()
    {
        if (Input.GetKeyDown(code))
        {
            if (_open_menu)
            {

                if (_setting_panel.activeSelf)
                {
                    _setting_panel.SetActive(false);
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


    private void VisibleItemImage()
    {
        if (nm.jetpackCount > 0)
        {
            _jetpack_img.GetComponent<Image>().sprite = _enable_jetpack_img;
        }
        else
        {
            _jetpack_img.GetComponent<Image>().sprite = _disable_jetpack_img;
        }
        if (nm.wallCount > 0)
        {
            _create_wall_img.GetComponent<Image>().sprite = _enable_create_wall_img;
        }
        else
        {
            _create_wall_img.GetComponent<Image>().sprite = _disable_create_wall_img;
        }
    }
    private void SelectedItem()
    {
        if (nm.jetpackSelected)
        {
            _jetpack_img.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            _jetpack_img.transform.GetChild(1).gameObject.SetActive(false);
        }

        if (nm.wallSelected)
        {
            _create_wall_img.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            _create_wall_img.transform.GetChild(1).gameObject.SetActive(false);
        }

    }
    // Update is called once per frame
    void Update()
    {
        VisibleMenu();
        VisibleItemImage();
        SelectedItem();
    }
}

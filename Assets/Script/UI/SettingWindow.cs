using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingWindow : MonoBehaviour
{
    private bool _is_changed = false;
    [SerializeField]
    private Image _current_buttn;
    [SerializeField]
    private Sprite _btn_img, _select_btn_img;
    [SerializeField]
    private GameObject _current_tabs;
    [SerializeField]
    private KeyCode _code;
    // Start is called before the first frame update
    public void OnChangedImageEvent(Image selected_button)
    {
        if (_current_buttn == selected_button)
            return;
        _current_buttn.sprite = _btn_img;
        _current_buttn = selected_button;
        _current_buttn.sprite = _select_btn_img;
        _is_changed = true;

    }
    public void OnChangedTabEvent(GameObject selected_tab)
    {
        if (!_is_changed)
            return;
        _current_tabs.SetActive(false);
        _current_tabs = selected_tab;
        _current_tabs.SetActive(true);
        _is_changed = false;

    }
    private void Update()
    {
        if (Input.GetKeyDown(_code))
        {
            gameObject.SetActive(false);
        }
    }
}

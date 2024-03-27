using Lightbug.CharacterControllerPro.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemControl : MonoBehaviour
{
    [SerializeField]
    private float _rot_speed = 10.0f;
    [SerializeField]
    private Item item;
    [SerializeField]
    private Sprite[] _item_icons;
    [SerializeField]
    private Image _item_image;
    [SerializeField]
    private GameObject canvas;
    private Animator _ani;

    private void Awake()
    {
        _ani = GetComponentInParent<Animator>();
    }

    private void Start()
    {
        if (item == null)
            item = GetComponentInChildren<Item>();
        if (_item_image == null)
            _item_image = GetComponentInChildren<Image>();
        item.ItemActiveEvent += ItemActive;
        _item_image.sprite =_item_icons[(int)item.currentItemType];
    }
    private void ItemActive(bool value)
    {
        canvas.SetActive(value);
        if(_ani != null)
            _ani.SetBool("ACTIVE_ITEM", value);

    }
    void Update()
    {
        item.gameObject.transform.Rotate(0f, 0f, _rot_speed * Time.deltaTime);    
    }
}

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
    private Animator _parent_ani;
    private AudioSource _audio;


    private void Awake()
    {
        _parent_ani = GetComponentInParent<Animator>();
        _audio = GetComponentInParent<AudioSource>();
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
        if(_parent_ani != null)
            _parent_ani.SetBool("ACTIVE_ITEM", value);
        if (!value && SoundManager.instance != null)
        {
            SoundManager.instance.PlayOneShot(_audio,"ItemPickUp");
        }
    }
    void Update()
    {
        item.gameObject.transform.Rotate(0f, 0f, _rot_speed * Time.deltaTime);    
    }
}

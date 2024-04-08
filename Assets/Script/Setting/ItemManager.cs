using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _easy_mode_items;

    private void enable_item(GameObject[] items)
    {
        foreach (GameObject item in items)
        {
            item.SetActive(true);
        }
    }

    private void disable_item(GameObject[] items)
    {
        foreach (GameObject item in items)
        {
            item.SetActive(false);
        }
    }

    private void set_item()
    {
        if (GameManager.instance == null) return;

        switch (GameManager.instance.SELECTED_LEVEL)
        {
            case GameManager.LEVELS.EASY:
                enable_item(_easy_mode_items);
                break;
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {
        set_item();
    }
}

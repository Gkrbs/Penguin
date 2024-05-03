using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _easy_mode_items;
    public GameObject[] achieve_item;
    private void enable_item(GameObject[] items)
    {
        foreach (GameObject item in items)
        {
            item.SetActive(true);
        }
    }

    private void disable_ahieve_item(GameObject[] items)
    {
        foreach (GameObject item in items)
        {
            AchievePoint ac = item.GetComponent<AchievePoint>();
            if (ac == null) continue;
            if (SteamManager.instance.achieve.isThisAchievementUnlocked((int)ac.id))
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
    private void set_achievements()
    {
        if (GameManager.instance == null || SteamManager.instance == null) return;

        if (GameManager.instance.SELECTED_LEVEL == GameManager.LEVELS.EASY)
        {
            if (!SteamManager.instance.achieve.isThisAchievementUnlocked((int)AchievementManager.IDS.TO_ENTER_EASY_MODE))
            {
                SteamManager.instance.achieve.UnlockedAchievement((int)AchievementManager.IDS.TO_ENTER_EASY_MODE);
            }
        }
        else if (GameManager.instance.SELECTED_LEVEL == GameManager.LEVELS.NORMAL)
        {
            if (!SteamManager.instance.achieve.isThisAchievementUnlocked((int)AchievementManager.IDS.TO_ENTER_NORMAL_MODE))
            {
                SteamManager.instance.achieve.UnlockedAchievement((int)AchievementManager.IDS.TO_ENTER_NORMAL_MODE);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        set_item();
        set_achievements();
        disable_ahieve_item(achieve_item);
    }
}

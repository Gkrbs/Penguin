using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private bool _collision_player = false;

    [SerializeField]
    private string SceneName = "";

    private void set_achievements()
    {
        if (GameManager.instance == null || SteamManager.instance == null) return;

        if (GameManager.instance.SELECTED_LEVEL == GameManager.LEVELS.EASY)
        {
            if (!SteamManager.instance.achieve.isThisAchievementUnlocked((int)AchievementManager.IDS.CLEAR_EASY_MODE))
            {
                SteamManager.instance.achieve.UnlockedAchievement((int)AchievementManager.IDS.CLEAR_EASY_MODE);
            }
        }
        else if (GameManager.instance.SELECTED_LEVEL == GameManager.LEVELS.NORMAL)
        {
            if (!SteamManager.instance.achieve.isThisAchievementUnlocked((int)AchievementManager.IDS.CLEAR_NORMAL_MODE))
            {
                SteamManager.instance.achieve.UnlockedAchievement((int)AchievementManager.IDS.CLEAR_NORMAL_MODE);
            }
        }

        if (!SteamManager.instance.achieve.isThisAchievementUnlocked((int)AchievementManager.IDS.TOTAL_CLEARCOUNT))
        {
            SteamManager.instance.achieve.AchievementCount((int)AchievementManager.IDS.TOTAL_CLEARCOUNT);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            set_achievements();
            if (Timer.instance != null)
                Timer.instance?.StopTimer();
            if (GameManager.instance != null)
            {
                GameManager.instance?.SelectLevel(GameManager.LEVELS.NONE);
                GameManager.instance?.SavePosToDefault();
            }

            _collision_player = true;
        }
    }

    private void Update()
    {
        if (_collision_player)
        {
            bl_SceneLoaderManager.LoadScene(SceneName);
        }
    }
}

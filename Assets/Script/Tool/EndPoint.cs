using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private bool _collision_player = false;

    [SerializeField]
    private string SceneName = "";
    [SerializeField]
    private GameObject _fadeout_panel;

    private Animation _fadeout_ani;


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
            GameManager.instance?.SelectLevel(GameManager.LEVELS.NONE);
            GameManager.instance?.SavePosToDefault();
            Timer.instance?.StopTimer();
            if (_fadeout_panel != null)
            {
                set_achievements();
                _fadeout_panel?.SetActive(true);
                _fadeout_ani ??= _fadeout_panel.GetComponent<Animation>();
                _fadeout_ani?.Play();
            }
            _collision_player = true;
        }
    }

    private void Update()
    {
        if (_collision_player)
        {
            if (_fadeout_ani != null && !_fadeout_ani.isPlaying)
            {
                bl_SceneLoaderManager.LoadScene(SceneName);
            }
            if (_fadeout_ani == null)
            {
                bl_SceneLoaderManager.LoadScene(SceneName);
            }
        }
    }
}

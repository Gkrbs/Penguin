using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Core;

public class SaveLoad : MonoBehaviour
{
    Vector3 position;
    public CharacterActor characterActor;
    public GameObject player;
    public Animator anim;

    private void Start()
    {
        if (GameManager.instance == null) return;
        if (GameManager.instance.SELECTED_LEVEL == GameManager.LEVELS.NORMAL)
        {
            gameObject.SetActive(false);
            return;
        }
        if(PlayerPrefs.HasKey("autoSaveX"))
        {
            GameManager.instance.autoSavePosition = new Vector3(PlayerPrefs.GetFloat("autoSaveX"),
                                                        PlayerPrefs.GetFloat("autoSaveY"),
                                                        PlayerPrefs.GetFloat("autoSaveZ"));
            LoadPosition(GameManager.instance.autoSavePosition);
        }
        if (GameManager.instance.ezClear)
        {
            LoadPosition(GameManager.instance.defaultPos);
            GameManager.instance.SavePosToDefault();
            position = GameManager.instance.defaultPos;
            GameManager.instance.ezClear = false;
            PlayerPrefs.SetInt("ezClear", 0);
        }
        position = GameManager.instance.autoSavePosition;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance == null) return;
        if (characterActor.IsGrounded) AutoSave();
        else return;
        if (Input.GetKeyDown(KeyCode.T) && characterActor.IsGrounded)
        {
            anim.SetTrigger("Save");
            position = player.transform.position;
            if (!SteamManager.instance.achieve.isThisAchievementUnlocked((int)AchievementManager.IDS.TOTAL_SAVE_COUNT))
                SteamManager.instance.achieve.AchievementCount((int)AchievementManager.IDS.TOTAL_SAVE_COUNT);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadPosition(position);
            if (!SteamManager.instance.achieve.isThisAchievementUnlocked((int)AchievementManager.IDS.TOTAL_LOAD_COUNT))
                SteamManager.instance.achieve.AchievementCount((int)AchievementManager.IDS.TOTAL_LOAD_COUNT);
        }
    }
    void LoadPosition(Vector3 position)
    {
        player.gameObject.SetActive(false);
        player.transform.position = position;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.gameObject.SetActive(true);
    }
    void AutoSave()
    {
        if (characterActor.IsGrounded)
        {
            GameManager.instance.autoSavePosition = player.transform.position;
            PlayerPrefs.SetFloat("autoSaveX", player.transform.position.x);
            PlayerPrefs.SetFloat("autoSaveY", player.transform.position.y);
            PlayerPrefs.SetFloat("autoSaveZ", player.transform.position.z);
        }
        if (Timer.instance != null&& Timer.instance.DO_TIMER)
        {
            PlayerPrefs.SetFloat("ezExitTime", Timer.instance.F_PLAY_TIME);
        }
    }
}

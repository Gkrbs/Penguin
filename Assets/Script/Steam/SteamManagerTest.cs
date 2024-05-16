using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
public class SteamManagerTest : MonoBehaviour
{
    private bool _is_init = false;
    public uint appId;
    public AchievementManagerTest achieve;
    public static SteamManagerTest instance;
    public string achieve_id = "";
    public bool IS_INIT
    {
        get { return _is_init; }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            achieve = GetComponent<AchievementManagerTest>();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        try
        {

            // steam is running
            SteamClient.Init(appId, true);
            _is_init = true;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            _is_init = false;
        }
    }

    public void OnEnableAchieveButtonClicked()
    {
        achieve.UnlockedAchievement(achieve_id);
    }

    public void OnDisableAchieveButtonClicked()
    {
        achieve.ClearAchievementStatus(achieve_id);
    }

    public void OnCountUpAchieveButtonClicked()
    {
        achieve.AchievementCount(achieve_id);
    }

    private void OnApplicationQuit()
    {
        try
        {
            SteamClient.Shutdown();
        }
        catch
        {

        }
    }

}

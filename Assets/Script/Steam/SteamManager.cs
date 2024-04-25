using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
public class SteamManager : MonoBehaviour
{
    public uint appId;

    public static SteamManager instance;
    //private void OnDisable()
    //{
    //    SteamClient.Shutdown();
    //}
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        try
        {
            SteamClient.Init(appId, true);// steam is running
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    private void Update()
    {
        
        //SteamUserStats.GetAchievements(true);
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

    public bool isThisAchievementUnlocked(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);
        return ach.State;
    }

    public void UnlockedAchievement(string id)
    { 
        var ach = new Steamworks.Data.Achievement(id);
        ach.Trigger();
    }

    public void ClearAchievementStatus(string id)
    { 
        var ach = new Steamworks.Data.Achievement(id);
        ach.Clear();
    }
}

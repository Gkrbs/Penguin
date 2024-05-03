using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
public class SteamManager : MonoBehaviour
{
    private bool _is_init = false;
    public uint appId;
    public AchievementManager achieve;
    public static SteamManager instance;

    public bool IS_INIT
    {
        get { return _is_init; }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            achieve = GetComponent<AchievementManager>();
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
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {

    //    }
    //    else if (Input.GetKeyDown(KeyCode.S))
    //    { 
    //    }

    //}

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

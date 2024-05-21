using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 defaultPos = new Vector3(-7.7f, -5.56f, -4.36f);
    public Vector3 autoSavePosition;
    public bool ezClear = false;
    public enum LEVELS
    {
        NONE,
        EASY,
        NORMAL
    }

    private LEVELS _selected_level = LEVELS.NONE;
    public LEVELS SELECTED_LEVEL
    {
        get { return _selected_level; }
    }

    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        { 
            instance = GetComponent<GameManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        autoSavePosition = defaultPos;
        ezClear = System.Convert.ToBoolean(PlayerPrefs.GetInt("ezClear"));
    }

    public void SelectLevel(LEVELS level)
    {
        _selected_level = level;
    }
    public void SavePosToDefault()
    {
        autoSavePosition = defaultPos;
        ezClear = true;
        PlayerPrefs.SetInt("ezClear", 1);
        PlayerPrefs.SetFloat("autoSaveX", defaultPos.x);
        PlayerPrefs.SetFloat("autoSaveY", defaultPos.y);
        PlayerPrefs.SetFloat("autoSaveZ", defaultPos.z);
        SaveEasyModeExitTime(0.0F);
    }

    public void SaveEasyModeExitTime(float time)
    {
        PlayerPrefs.SetFloat("ezExitTime",time);
    }
    public float LoadEasyModeExitTime()
    { 
        return PlayerPrefs.GetFloat("ezExitTime");
    }
}

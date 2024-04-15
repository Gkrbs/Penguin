using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 autoSavePosition;
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
    }

    public void SelectLevel(LEVELS level)
    {
        _selected_level = level;
    }
}

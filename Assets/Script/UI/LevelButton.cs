using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private GameManager.LEVELS _selected_level = GameManager.LEVELS.NONE;
    public void OnClicked()
    {
        GameManager.instance?.SelectLevel(_selected_level);
    }
}

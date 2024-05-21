using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBtn : MonoBehaviour
{
    public void ExitTitle()
    {
        if (Timer.instance != null)
        {
            Timer.instance.StopTimer();
        }
        if (GameManager.instance != null)
        {
            if(GameManager.instance.SELECTED_LEVEL == GameManager.LEVELS.EASY)
                GameManager.instance.SaveEasyModeExitTime(Timer.instance.F_PLAY_TIME);
        }
    }
}

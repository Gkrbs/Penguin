using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    /*
    이지 입장               0
    노말 입장               1
    이지 클리어             2
    노말 클리어             3
    타임어택                4
    그래플링연습장 121점    5
    떨어진 최대 시간        6
    3스테이로
    2스테이지로
    점프 횟수
    그래플 횟수
    제트팩 사용 횟수
    발판 사용 횟수
    음성 나온 횟수
    클리어횟수
    세이브횟수
    로드횟수
     */
    public enum IDS
    {
        TO_ENTER_EASY_MODE,
        TO_ENTER_NORMAL_MODE,
        CLEAR_EASY_MODE,
        CLEAR_NORMAL_MODE,
        TIME_ATTACK,
        GRAPPLING_TRAINING_MAX_POINT,
        LONGEST_DROP_TIME,
        FALLEN_DOWN_STAGE_3,
        FALLEN_DOWN_STAGE_2,
        TOTAL_JUMP_COUNT,
        TOTAL_GRAPPLING_COUNT,
        TOTAL_JECTPACK_COUNT,
        TOTAL_CREATE_WALL_COUNT,
        TOTAL_ACTIVE_VOICE_COUNT,
        TOTAL_CLEARCOUNT,
        TOTAL_SAVE_COUNT,
        TOTAL_LOAD_COUNT

    }
    public float time_attack_limit_time = 7200f;
    public float longest_drop_time = 4f;
    public string[] arc_id;
    public bool isThisAchievementUnlocked(int id)
    {
        if (!SteamManager.instance.IS_INIT)
            return false;
        var ach = new Steamworks.Data.Achievement(arc_id[id]);
        return ach.State;
    }

    public void UnlockedAchievement(int id)
    {
        if (!SteamManager.instance.IS_INIT)
            return;
        var ach = new Steamworks.Data.Achievement(arc_id[id]);
        ach.Trigger();
    }

    public void ClearAchievementStatus(int id)
    {
        if (!SteamManager.instance.IS_INIT)
            return;
        var ach = new Steamworks.Data.Achievement(arc_id[id]);
        ach.Clear();
    }
}

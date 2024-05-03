using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    /*
    ���� ����               0
    �븻 ����               1
    ���� Ŭ����             2
    �븻 Ŭ����             3
    Ÿ�Ӿ���                4
    �׷��ø������� 121��    5
    ������ �ִ� �ð�        6
    3�����̷�
    2����������
    ���� Ƚ��
    �׷��� Ƚ��
    ��Ʈ�� ��� Ƚ��
    ���� ��� Ƚ��
    ���� ���� Ƚ��
    Ŭ����Ƚ��
    ���̺�Ƚ��
    �ε�Ƚ��
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

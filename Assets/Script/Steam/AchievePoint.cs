using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievePoint : MonoBehaviour
{
    public AchievementManager.IDS id;

    private void set_achievements()
    {
        if (GameManager.instance == null || SteamManager.instance == null) return;


        if (!SteamManager.instance.achieve.isThisAchievementUnlocked((int)id))
        {
            SteamManager.instance.achieve.UnlockedAchievement((int)id);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            set_achievements();
        }
    }
}

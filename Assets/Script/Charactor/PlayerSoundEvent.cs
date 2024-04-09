using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEvent : MonoBehaviour
{
    public AudioSource audio;
    public float start_time = 0.0f;

    public void SoundEvent(string clipName)
    {
        SoundManager.instance.PlayOneShot(audio, clipName);
    }

    public void FootStepSoundEvent()
    {
        int ran = Random.Range(1, 3);
        SoundManager.instance.PlayOneShot(audio, "FootStep_" + ran);
    }
    public void JumpSoundEvent()
    {
        SoundManager.instance.PlayOneShot(audio, "Light_Snow_footsteps_");
    }
    public void WingSoundEvent()
    {
        int ran = Random.Range(4, 6);
        SoundManager.instance.PlayOneShot(audio, "Chicken_flapping_wings_" + ran, start_time);
    }
}

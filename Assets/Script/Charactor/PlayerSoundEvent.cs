using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEvent : MonoBehaviour
{
    public AudioSource audio;

    public void SoundEvent(string clipName)
    {
        SoundManager.instance.PlayOneShot(audio, clipName);
    }

    public void FootStepSoundEvent()
    {
        int ran = Random.Range(1, 6);
        SoundManager.instance.PlayOneShot(audio, "Light_Snow_footsteps_" + ran);
    }
    public void JumpSoundEvent()
    {
        SoundManager.instance.PlayOneShot(audio, "Light_Snow_footsteps_");
    }
    public void WingSoundEvent()
    {
        int ran = Random.Range(4, 6);
        SoundManager.instance.PlayOneShot(audio, "Chicken_flapping_wings_" + ran);
    }
}

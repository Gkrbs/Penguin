using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEvent : MonoBehaviour
{
    public AudioSource audio;

    public void SoundEvent(string clipName)
    {
        Debug.Log("SoundEvent" + clipName);
        SoundSetting.instance.PlayOneShot(audio, clipName);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackGround : MonoBehaviour
{
    public AudioSource audio;
    public string audioName;
    void Start()
    {
        SoundManager.instance.PlayLoop(audio, audioName);
    }
}

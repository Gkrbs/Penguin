using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip[] clips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }
    public void PlayOneShot(AudioSource audio, string name)
    {
        AudioClip clip = null;
        foreach(AudioClip audioClip in clips)
        {
            if(audioClip.name == name)
            {
                clip = audioClip;
                break;
            }
        }
        if(clip == null)
        {
            Debug.Log("no clip name");
            return;
        }
        audio.loop = false;
        audio.PlayOneShot(clip);
    }
    public void PlayLoop(AudioSource audio, string name)
    {
        AudioClip clip = null;
        foreach (AudioClip audioClip in clips)
        {
            if (audioClip.name == name)
            {
                clip = audioClip;
                break;
            }
        }
        if (clip == null)
        {
            Debug.Log("no clip name");
            return;
        }
        audio.clip = clip;
        audio.loop = true;
        audio.Play();
    }
}

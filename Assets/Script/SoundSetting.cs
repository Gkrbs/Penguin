using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
public class SoundSetting : MonoBehaviour
{
    public static SoundSetting instance;

    public Sound[] sounds;
    public AudioMixer audioMixer;

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
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        //Play("background");
    }

    // 저장 기능 추가해야함.
    // 볼륨 조절
    public void SetMaster(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume)*20);
    }
    public void SetBackground(float volume)
    {
        audioMixer.SetFloat("Background", Mathf.Log10(volume) * 20);
    }
    public void SetEffect(float volume)
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(volume) * 20);
    }
    public void SetCharacter(float volume)
    {
        audioMixer.SetFloat("Character", Mathf.Log10(volume) * 20);
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("no sound name");
            return;
        }
        s.source.Play();
    }
    public void StopAll()
    {
        foreach(Sound s in sounds)
        {
            s.source.Stop();
        }
    }
}

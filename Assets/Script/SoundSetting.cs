using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;
public class SoundSetting : MonoBehaviour
{
    public static SoundSetting instance;

    [SerializeField] AudioSource backgroundSource;
    [SerializeField] AudioSource EffectSource;
    [SerializeField] AudioSource CharacterSource;

    public Sound[] sounds;
    public AudioMixer audioMixer;

    public Slider masterSlider;
    public Slider BackgroundSlider;
    public Slider EffectSlider;
    public Slider CharacterSlider;

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
            s.source.outputAudioMixerGroup = audioMixer.outputAudioMixerGroup;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMaster();
            SetBackground();
            SetEffect();
            SetCharacter();
        }
        //Play("background");
    }

    // 저장 기능 추가해야함.
    // 볼륨 조절
    public void SetMaster()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }
    public void SetBackground()
    {
        float volume = BackgroundSlider.value;
        audioMixer.SetFloat("Background", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BackgroundVolume", volume);
    }
    public void SetEffect()
    {
        float volume = EffectSlider.value;
        audioMixer.SetFloat("Effect", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("EffectVolume", volume);
    }
    public void SetCharacter()
    {
        float volume = CharacterSlider.value;
        audioMixer.SetFloat("Character", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("CharacterVolume", volume);
    }
    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        BackgroundSlider.value = PlayerPrefs.GetFloat("BackgroundVolume");
        EffectSlider.value = PlayerPrefs.GetFloat("EffectVolume");
        CharacterSlider.value = PlayerPrefs.GetFloat("CharacterVolume");
    }
    public void PlayEffect(AudioClip clip)
    {
        EffectSource.PlayOneShot(clip);
    }
    public void PlayCharacter(AudioClip clip)
    {
        CharacterSource.PlayOneShot(clip);
    }

    public void Play(string name)
    {
        //Sound s = Array.Find(sounds, sound => sound.name == name);
        //if(s == null)
        //{
        //    Debug.Log("no sound name");
        //    return;
        //}
        //s.source.Play();
    }
    //public void EffectPlay(string name)
    //{
    //    Sound s = Array.Find(sounds, sound => sound.name == name);
    //    if (s == null)
    //    {
    //        Debug.Log("no sound name");
    //        return;
    //    }
    //    //s.source.PlayOneShot();
    //}

    //public void StopAll()
    //{
    //    foreach(Sound s in sounds)
    //    {
    //        s.source.Stop();
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundSetting : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider masterSlider;
    public Slider BackgroundSlider;
    public Slider EffectSlider;
    public Slider CharacterSlider;

    private void Start()
    {
        //if (PlayerPrefs.HasKey("masterVolume"))
        //{
        //    LoadVolume();
        //}
        //else
        //{
        //    SetMaster();
        //    SetBackground();
        //    SetEffect();
        //    SetCharacter();
        //}
        //Play("background");
    }

    public void SetMaster()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        //PlayerPrefs.SetFloat("masterVolume", volume);
    }
    public void SetBackground()
    {
        float volume = BackgroundSlider.value;
        audioMixer.SetFloat("Background", Mathf.Log10(volume) * 20);
        //PlayerPrefs.SetFloat("BackgroundVolume", volume);
    }
    public void SetEffect()
    {
        float volume = EffectSlider.value;
        audioMixer.SetFloat("Effect", Mathf.Log10(volume) * 20);
        //PlayerPrefs.SetFloat("EffectVolume", volume);
    }
    public void SetCharacter()
    {
        float volume = CharacterSlider.value;
        audioMixer.SetFloat("Character", Mathf.Log10(volume) * 20);
        //PlayerPrefs.SetFloat("CharacterVolume", volume);
    }
    //private void LoadVolume()
    //{
    //    masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
    //    BackgroundSlider.value = PlayerPrefs.GetFloat("BackgroundVolume");
    //    EffectSlider.value = PlayerPrefs.GetFloat("EffectVolume");
    //    CharacterSlider.value = PlayerPrefs.GetFloat("CharacterVolume");
    //    SetMaster();
    //    SetBackground();
    //    SetEffect();
    //    SetCharacter();
    //}
}

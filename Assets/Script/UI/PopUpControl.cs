using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpControl : MonoBehaviour
{
    private Animator _ani;
    private AudioSource _audio;
    // Start is called before the first frame update
    private void Awake()
    {
        _ani = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SoundManager.instance.PlayOneShot(_audio,"Pop");
    }
    public async void DisableEvent()
    {
        SoundManager.instance.PlayOneShot(_audio,"Pop2");
        await System.Threading.Tasks.Task.Delay(100);
        gameObject.SetActive(false);
    }
}

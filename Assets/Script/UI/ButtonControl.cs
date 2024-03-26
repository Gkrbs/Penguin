using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    protected AudioSource _audio;
    protected Animator _ani;
    private readonly int _press_trigger = Animator.StringToHash("Pressed");

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _ani = GetComponent<Animator>();
    }
    private void Start()
    {
    }
    public virtual void OnClickedEvent()
    {
        SoundManager.instance.PlayOneShot(_audio, "ButtonPress");
        if (_ani == null)
            return;
        if(!_ani.GetCurrentAnimatorStateInfo(0).IsName("Pressed"))
            _ani.SetTrigger(_press_trigger);

    }
}

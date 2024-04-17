using Lightbug.CharacterControllerPro.Core;
using Lightbug.CharacterControllerPro.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    private float _fall_time = 0.0f;
    [SerializeField]
    private string[] _sound_names;
    [SerializeField]
    private CharacterActor _actor;
    [SerializeField]
    private NormalMovement _nm;
    [SerializeField]
    private AudioSource _audio;
    private Rigidbody _rd;
    private void Start()
    {
        _rd = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("JumpGround"))
        {
            _fall_time = -2.0f;
        }
        else if (collision.gameObject.CompareTag("StageGround"))
        {
            if (_fall_time >= 1.5f)
            {
                int idx = (int)_fall_time - 1;
                if (idx > _sound_names.Length)
                    idx = _sound_names.Length - 1;
                SoundManager.instance.PlayOneShot(_audio, _sound_names[idx]);
            }
            _fall_time = 0.0f;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")||
            collision.gameObject.layer == LayerMask.NameToLayer("DynamicGround") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Can Hook Ground"))
        {
            if (_fall_time >= 2.0f)
            {
                int idx = (int)_fall_time - 2;
                if (idx > _sound_names.Length)
                    idx = _sound_names.Length - 1;
                SoundManager.instance.PlayOneShot(_audio, _sound_names[idx]);
            }
            _fall_time = 0.0f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (_nm.verticalMovementParameters.isGrappled || _nm.DO_JETPACK)
        {
            _fall_time = 0.0f;
        }
        if (!_actor.IsGrounded && !_nm.verticalMovementParameters.isGrappled && !_nm.DO_JETPACK)
        { 
            _fall_time += Time.deltaTime;
        }

    }
}

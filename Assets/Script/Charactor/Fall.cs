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

    private void jump_event()
    {
        if (!SteamManager.instance.achieve.isThisAchievementUnlocked((int)AchievementManager.IDS.TOTAL_JUMP_COUNT))
            SteamManager.instance.achieve.AchievementCount((int)AchievementManager.IDS.TOTAL_JUMP_COUNT);
    }
    private void jetpack_event()
    {
        if (!SteamManager.instance.achieve.isThisAchievementUnlocked((int)AchievementManager.IDS.TOTAL_JECTPACK_COUNT))
            SteamManager.instance.achieve.AchievementCount((int)AchievementManager.IDS.TOTAL_JECTPACK_COUNT);
    }
    private void create_wall_event()
    {
        if (!SteamManager.instance.achieve.isThisAchievementUnlocked((int)AchievementManager.IDS.TOTAL_CREATE_WALL_COUNT))
            SteamManager.instance.achieve.AchievementCount((int)AchievementManager.IDS.TOTAL_CREATE_WALL_COUNT);
    }
    private void Start()
    {
        _rd = GetComponent<Rigidbody>();
        _nm.JumpEvent += jump_event;
        _nm.JetpackEvent += jetpack_event;
        _nm.CreateWallEvent += create_wall_event;
    }
    private void set_achievements()
    {
        if (GameManager.instance == null || SteamManager.instance == null) return;


        if (!SteamManager.instance.achieve.isThisAchievementUnlocked((int)AchievementManager.IDS.LONGEST_DROP_TIME))
        {
            SteamManager.instance.achieve.UnlockedAchievement((int)AchievementManager.IDS.LONGEST_DROP_TIME);
        }
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
                if (_fall_time >= SteamManager.instance.achieve.longest_drop_time)
                    set_achievements();
                //도전과제
                if (!SteamManager.instance.achieve.isThisAchievementUnlocked((int)AchievementManager.IDS.TOTAL_ACTIVE_VOICE_COUNT))
                    SteamManager.instance.achieve.AchievementCount((int)AchievementManager.IDS.TOTAL_ACTIVE_VOICE_COUNT);

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
                if (idx >= _sound_names.Length)
                    idx = _sound_names.Length - 1;
                SoundManager.instance.PlayOneShot(_audio, _sound_names[idx]);
                if (_fall_time >= SteamManager.instance.achieve.longest_drop_time)
                    set_achievements();
                //도전과제
                if (!SteamManager.instance.achieve.isThisAchievementUnlocked((int)AchievementManager.IDS.TOTAL_ACTIVE_VOICE_COUNT))
                    SteamManager.instance.achieve.AchievementCount((int)AchievementManager.IDS.TOTAL_ACTIVE_VOICE_COUNT);
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

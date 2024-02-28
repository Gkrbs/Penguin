using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Demo;
using Lightbug.CharacterControllerPro.Core;
public class HookGun : MonoBehaviour
{
    public CharacterActor characterActor;
    enum GUN_STATES
    {
        IDLE,
        SHOOT,
        GRAPPLE,
        RELOAD
    }
    [SerializeField]
    private GUN_STATES state = GUN_STATES.IDLE;


    private float _max_distance = 15.0f;
    private float _min_distance = 2.5f;

    float _bullet_force = 1000.0f;
    [SerializeField]
    float _gun_rot_speed = 10.0f;

    [SerializeField]
    private Transform _cam_tr;

    [SerializeField]
    private AudioClip _flareShotSound;
    [SerializeField]
    private AudioClip _noAmmoSound;
    [SerializeField]
    private Transform _fire_point;
    [SerializeField]
    public GameObject _muzzleParticles;
    private Animation _ani;
    private AudioSource _audio;

    [SerializeField]
    LayerMask _target_layer;

    [SerializeField]
    Transform _aim_tr;
    [SerializeField]
    private Hook _hook;

    public float MAX_DISTANCE
    {
        get { return _max_distance; }
    }

    public float MIN_DISTANCE
    {
        get { return _min_distance; }
    }

    public Vector3 FIRE_POINT
    {
        get { return _fire_point.position; }
    }
    public float BULLET_DISTANCE
    {
        get
        {
            float res = Vector3.Distance(_fire_point.position, _hook.transform.position);
            return res;
        }
    }

    private void Awake()
    {
        _ani = GetComponent<Animation>();
        _audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Max " + MAX_DISTANCE);
        Debug.Log("Bul " + BULLET_DISTANCE);
        switch (state)
        {
            case GUN_STATES.IDLE:
                idle();
                break;
            case GUN_STATES.SHOOT:
                shoot();
                break;
            case GUN_STATES.GRAPPLE:
                graple();
                break;
            case GUN_STATES.RELOAD:
                Reloading();
                break;
        }
    }

    private void GunMove()
    {
        float rot_x = -Input.GetAxis("Mouse Y");
        transform.Rotate(Vector3.right * rot_x * Time.deltaTime * _gun_rot_speed);
    }

    private void idle()
    {
        if (_hook.IS_SHOOTING || _hook.IS_GRAPPLE) return;

        if (Input.GetButtonDown("Fire1") && !_ani.isPlaying)
        {
            state = GUN_STATES.SHOOT;
        }
    }

    private void shoot()
    {
        if (_hook.IS_GRAPPLE)
        {
            return;
        }

        if (!_ani.isPlaying)
        {
            _hook.transform.parent = null;
            _ani.CrossFade("Shoot");
            _audio.PlayOneShot(_flareShotSound);

            RaycastHit hit;
            Vector3 dir;
            float cam_dist = Vector3.Distance(_cam_tr.position, _aim_tr.position);
            if (Physics.Raycast(_cam_tr.position, _cam_tr.forward, out hit, cam_dist, _target_layer))
            {
                if (Physics.Linecast(_fire_point.position, hit.point, out hit, _target_layer))
                {
                    dir = (hit.point - _fire_point.position).normalized;
                }
                else
                {
                    dir = (_aim_tr.position - _fire_point.position).normalized;
                }
            }
            else
            {
                dir = (_aim_tr.position - _fire_point.position).normalized;
            }

            _hook.Shoot(dir, _bullet_force);

            _muzzleParticles.transform.position = _fire_point.position;
            _muzzleParticles.transform.rotation = _fire_point.rotation;
            _muzzleParticles.SetActive(true);
            state = GUN_STATES.GRAPPLE;
        }
    }
    private void graple()
    {
        if (!_hook.IS_GRAPPLE && BULLET_DISTANCE > MAX_DISTANCE)
        {
            Reloading();
        }

        if (_hook.IS_SHOOTING) return;

        SpringJoint joint = transform.parent.GetComponent<SpringJoint>();
        if (joint != null)
        {
            if (joint.maxDistance > BULLET_DISTANCE)
            {
                characterActor.;
            }
            elses
            {
                characterActor.IsGrounded = true;
            }
        }

        //if (MAX_DISTANCE > BULLET_DISTANCE)
        //{
        //    characterActor.alwaysNotGrounded = true;
        //}
        //else
        //{
        //    characterActor.alwaysNotGrounded = false;
        //}
        if (Input.GetButtonDown("Fire1"))
        {
            Reloading();
        }
    }

    private void Reloading()
    {
        _hook.Reloading();
        state = GUN_STATES.IDLE;
        characterActor.alwaysNotGrounded = false;
    }

}

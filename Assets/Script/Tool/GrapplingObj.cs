using Lightbug.CharacterControllerPro.Core;
using Lightbug.CharacterControllerPro.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingObj : MonoBehaviour
{
    private bool _is_delay = false;


    [Header("Grappling")]
    [SerializeField]
    private float _force = 20.0f;
    [SerializeField]
    private float _max_dist = 5f;
    [SerializeField]
    private float _min_dist = 1f;
    [SerializeField]
    private float _anc_speed = 8f;
    [SerializeField]
    private float _spring = 500f;
    [SerializeField]
    private float _damper = 750f;
    [SerializeField]
    private float _mass_scale = 7f;
    [SerializeField]
    private float _jump_force = 800f;
    
    [Header("Shoot")]
    [SerializeField]
    private int _cool_time = 500;

    [SerializeField]
    private LayerMask defaultLayer;
    [SerializeField]
    LayerMask _target_layer;

    [SerializeField]
    private Transform _cam_tr;
    [SerializeField]
    private Transform _fire_point;

    [Header("Audio")]
    [SerializeField]
    private AudioClip _ShotSound;
    private AudioSource _audio;

    [Header("Roop")]
    private LineRenderer _ir;

    [Header("Object")]
    [SerializeField]
    private Tongue _bullet;
    [SerializeField]
    private GameObject _player;

    private SpringJoint _joint = null;
    public NormalMovement normalMovement;
    public CharacterActor characterActor;

    public float BULLET_DISTANCE
    {
        get { return Vector3.Distance(_fire_point.position, _bullet.gameObject.transform.position); }
    }
    public void Shoot()
    {
        if (UIManager.instance != null && UIManager.instance.OPEN_MENU)
            return;

        if (_is_delay) return;

        _is_delay = true;

        RaycastHit hit;
        Vector3 dir;
        float cam_dist = Vector3.Distance(_cam_tr.position, AimObject.instanse.AIM);
        if (Physics.Raycast(_cam_tr.position, _cam_tr.forward, out hit, cam_dist, _target_layer))
        {
              dir = (hit.point - _fire_point.position).normalized;
        }
        else
        {
            dir = (AimObject.instanse.AIM - _fire_point.position).normalized;
        }

        _audio.PlayOneShot(_ShotSound);
        _bullet.Shoot(_fire_point, dir, _force);

        delay();
    }

    private void StartGrappling(Vector3 pos)
    {
        normalMovement.Grappling(true);
        if (_joint == null)
            _joint = _player.gameObject.AddComponent<SpringJoint>();
        _joint.autoConfigureConnectedAnchor = false;
        _joint.connectedAnchor = pos;
        float distanceFromPoint = Vector3.Distance(_player.transform.position, pos);
        _joint.maxDistance = distanceFromPoint;
        _joint.minDistance = 0.0f;

        _joint.spring = _spring;
        _joint.damper = _damper;
        _joint.massScale = _mass_scale;
    }

    private async void delay()
    {
        await System.Threading.Tasks.Task.Delay(_cool_time);

        _is_delay = false;
    }
    private void StopGrappling()
    {
        if (_joint != null)
        {
            MonoBehaviour.Destroy(_joint);
            normalMovement.Grappling(false);
            _joint = null;
        }
    }
    private void Awake()
    {
        _ir = GetComponent<LineRenderer>();
        _audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _bullet.StartGrapplingEvent += StartGrappling;
        characterActor = GameObject.Find("Player").GetComponent<CharacterActor>();
        defaultLayer = characterActor.stableLayerMask;
        AimObject.instanse.Init(_max_dist);
    }
    private void draw_line()
    {
        if (!_bullet.IS_SHOOT && !_bullet.IS_ACTIVE)
        {
            _ir.enabled = false;
            _ir.startWidth = 0.0f;
            _ir.endWidth = 0.0f;
            return;
        }
        _ir.enabled = true;
        _ir.startWidth = 0.025f;
        _ir.endWidth = 0.025f;
        _ir.SetPosition(0, _fire_point.position);
        _ir.SetPosition(1, _bullet.gameObject.transform.position);
    }
    private void ReloadControl()
    {
        if (UIManager.instance != null && UIManager.instance.OPEN_MENU)
            return;

        if (!_bullet.IS_ACTIVE) return;


        if (Input.GetMouseButtonDown(0))
        {
            if (GetComponentInParent<SpringJoint>() != null)
            {
                _bullet.Stop();
                StopGrappling();

            }
        }
    }
    public void GrapplingControl()
    {
        if (!_bullet.IS_ACTIVE) return;

        SpringJoint joint = transform.GetComponentInParent<SpringJoint>();

        if (joint == null)
            return;

        if (joint.maxDistance < BULLET_DISTANCE && characterActor.IsGrounded)
        {
            characterActor.alwaysNotGrounded = true;
            characterActor.stableLayerMask = 0;
        }
        else
        {
            characterActor.alwaysNotGrounded = false;
            characterActor.stableLayerMask = defaultLayer;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            joint.maxDistance -= Time.deltaTime * _anc_speed;
            if (joint.maxDistance < _min_dist)
                joint.maxDistance = _min_dist;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            joint.maxDistance += Time.deltaTime * _anc_speed;
            if (joint.maxDistance > _max_dist)
                joint.maxDistance = _max_dist;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }
    private void LateUpdate()
    {
        draw_line();
        GrapplingControl();
        ReloadControl();
    }
    private void Update()
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            if (!_bullet.IS_SHOOT)
                Shoot();
        }
  
    }
}

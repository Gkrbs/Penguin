using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    enum INFO
    {
        FORCE,
        COOL_TIME
    }

    private const string GUN_DATA_PATH = "ScriptableOBjects\\GunData";
    private const string MAGAZINE_PREFAB_PATH = "ScriptableOBjects\\GunData";

    public bool is_tool_control = false;
    private float _delay_time = 0.0f;
    private float _default_delay_time = 0.0f;


    [SerializeField]
    private ToolInfo _data;
    [SerializeField]
    private Magazine _magazine;
    private GameObject _magazine_obj;

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
    private ActiveTool _tool;
    private Vector3 _hit_position = Vector3.zero;
    public Vector3 HIT_POS
    {
        get { return _hit_position; }
    }
    private void Awake()
    {
        _data = _data == null ? Resources.Load<ToolInfo>(GUN_DATA_PATH) : _data;
        _ani = GetComponent<Animation>();
        _audio = GetComponent<AudioSource>();
    }

    public void ActiveTool()
    {
        if (_tool != null)
            _tool.ActiveAction();
    }

    public void Reload(string magazine_path, string bullet_name)
    {
        if (_tool != null)
            _tool.StopAction();
        _magazine.Init(gameObject, magazine_path, bullet_name);
    }

    public void Shoot()
    {
        GameObject bullet = _magazine.GetBullet();

        if (bullet == null)
        {
            if (!_ani.isPlaying)
            {
                _ani.CrossFade("Shoot");
                _audio.PlayOneShot(_flareShotSound);
            }
            return;
        }
        _hit_position = Vector3.zero;
        RaycastHit hit;
        Vector3 dir;
        float cam_dist = Vector3.Distance(_cam_tr.position, _aim_tr.position);
        if (Physics.Raycast(_cam_tr.position, _cam_tr.forward, out hit, cam_dist, _target_layer))
        {
            dir = (hit.point - _fire_point.position).normalized;
            _hit_position = hit.point;
        }
        else
        {
            dir = (_aim_tr.position - _fire_point.position).normalized;
        }

        _ani.CrossFade("Shoot");
        _audio.PlayOneShot(_flareShotSound);

        if (bullet == null)
            return;

        if (_tool != null)
        {
            _tool.Init(_fire_point.gameObject);
            _tool.ActiveAction(bullet);
        }
        Rigidbody bullet_rd = bullet.GetComponent<Rigidbody>();
        bullet.transform.position = _fire_point.position;
        bullet.transform.rotation = _fire_point.rotation;
        float force = _data.f_datas[(int)INFO.FORCE];
        bullet_rd.velocity = dir * force;     //AddForce(dir * force);
        bullet.SetActive(true);
        bullet.transform.parent = null;

        _muzzleParticles.transform.position = _fire_point.position;
        _muzzleParticles.transform.rotation = _fire_point.rotation;
        _muzzleParticles.SetActive(true);

    }



    private void Start()
    {
        _tool = GetComponent<ActiveTool>();
        _magazine ??= GetComponentInChildren<Magazine>();
        _magazine.Init(gameObject, "GrapplingBulletMagazine", "Grappling Bullet");
        _default_delay_time = _data.f_datas[(int)INFO.COOL_TIME];
        _delay_time = _default_delay_time;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (is_tool_control)
            {
                if (!_tool.IS_TRIGGER && !_ani.isPlaying)
                    Shoot();
            }
            else
            {
                if (_delay_time == _default_delay_time && !_ani.isPlaying)
                {
                    Shoot();
                    _delay_time -= Time.deltaTime;
                }
            }
        }
        if (_delay_time != _default_delay_time)
        {
            _delay_time -= Time.deltaTime;
            if (_delay_time <= 0)
            {
                _delay_time = _default_delay_time;
            }
        }
    }

}

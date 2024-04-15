using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    private bool _is_delay_stop = false;
    private bool _is_active = false;
    private bool _is_shoot = false;
    [SerializeField]
    [Range(0.0f, 10.0f)] private float _cast_max_dist = 0.5f;
    [SerializeField]
    private float _max_bullet_dist = 5.0f;
    [SerializeField]
    private int _stop_delay_millisec = 100;
    [SerializeField]
    private Transform parents;
    [SerializeField]
    private LayerMask _layer_mask;

    private Vector3 _hit_point = Vector3.zero;
    private Vector3 default_valocity = Vector3.zero;

    private Rigidbody _rd;
    public delegate void StartGrapplingDelegate(Vector3 pos);
    public event StartGrapplingDelegate StartGrapplingEvent;
    public bool IS_ACTIVE
    {
        get { return _is_active; }
    }
    public bool IS_SHOOT
    {
        get { return _is_shoot; }
    }
    public Vector3 HIT_POINT
    {
        get { return _hit_point; }
    }

    public float MAX_DISTANCE
    {
        get
        {
            return _max_bullet_dist;
        }
    }

    private void Awake()
    {
        _rd = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        default_valocity = _rd.velocity;
    }

    public void Shoot(Transform tire_point, Vector3 dir, float force)
    {
        _is_shoot = true;
        Rigidbody bullet_rd = GetComponent<Rigidbody>();
        transform.position = tire_point.position;
        transform.rotation = tire_point.rotation;
        bullet_rd.velocity = dir * force;
        gameObject.SetActive(true);
        transform.parent = null;
    }

    public void CollisionObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, GetComponent<Rigidbody>().velocity, out hit, _cast_max_dist, _layer_mask))
        {
            _hit_point = hit.point;
            _rd.isKinematic = true;
            transform.position = hit.point;
            _is_active = true;
            StartGrapplingEvent(_hit_point);
            return;
        }

        if (Physics.Raycast(transform.position, GetComponent<Rigidbody>().velocity, out hit, _cast_max_dist))
        {
            StopDelay();
        }
    }
    public async void StopDelay()
    {
        if (_is_delay_stop) return;
        _is_delay_stop = true;
        await System.Threading.Tasks.Task.Delay(_stop_delay_millisec);
        Stop();
        _is_delay_stop = false;
    }
    public void Stop()
    {
        if (_is_delay_stop) return;
        transform.position = parents.position;
        transform.rotation = parents.rotation;
        transform.parent = parents;
        _is_active = false;
        _is_shoot = false;
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        default_valocity = Vector3.zero;
        if (_rd.velocity != Vector3.zero)
            _rd.velocity = Vector3.zero;
        if (_rd.isKinematic) _rd.isKinematic = false;
    }
    private void FixedUpdate()
    {
        float max_dist = Vector3.Distance(parents.transform.position, transform.position);

        float limit_dist = _max_bullet_dist;
        if (!_is_active && max_dist > limit_dist)
        {
            Stop();
        }

        if (!_is_active && !_rd.isKinematic && _rd.velocity != default_valocity)
        {
            Stop();
        }
    }
    // Update is called once per frame
    void Update()
    {
        CollisionObject();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private bool _is_grapple = false;
    private bool _is_shooting = false;
    private bool _is_reloading = false;
    [SerializeField]
    private float _reload_speed = 0.5f;

    private Vector3 _grapple_point;
    private LineRenderer _ir;

    [SerializeField]
    private HookGun _gun;
    [SerializeField]
    private GameObject _parents;
    [SerializeField]
    private ActionInterface _active;
    [SerializeField]
    private ActiveObjectsScriptable _data;

    private Rigidbody _rd;

    public bool IS_GRAPPLE
    {
        get { return _is_grapple; }
    }

    public bool IS_SHOOTING
    {
        get { return _is_shooting; }
    }

    public bool IS_RELOADING
    {
        get { return _is_reloading; }
    }

    private void Awake()
    {
        _ir = GetComponent<LineRenderer>();
        _rd = GetComponent<Rigidbody>();
        _active = new Grappling();
    }
    private void Start()
    {
        _active.InitObjectInfo(_data);
        _active.Init(_gun.transform.parent.gameObject);
    }
    public async void Reloading()
    {
        _rd.isKinematic = false;
        _rd.velocity = Vector3.zero;
        _is_grapple = false;
        _is_shooting = false;
        transform.parent = _parents.transform;
        transform.position = _parents.transform.position;
        transform.rotation = _parents.transform.rotation;
        await System.Threading.Tasks.Task.Delay(100);
        _active.StopAction();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (_rd.velocity != Vector3.zero)
                _rd.velocity = Vector3.zero;
            _rd.isKinematic = true;
            _grapple_point = other.transform.position;
            _is_grapple = true;
            _is_shooting = false;
            if(_active != null)
                _active.ActiveAction(other.gameObject);
        }
    }
    public void Shoot(Vector3 dir, float force)
    {
        GetComponent<Rigidbody>().AddForce(dir * force);
        _is_shooting = true;
    }
    private void Update()
    {
        DrawRope();
        if(_active!= null)
            _active.ActiveAction();
        if (!IS_SHOOTING && !IS_GRAPPLE)
        {
            transform.position = _parents.transform.position;
            transform.rotation = _parents.transform.rotation;
        }
    }

    private void DrawRope()
    {
        if (!_is_shooting && !_is_grapple)
        {
            _ir.startWidth = 0.0f;
            _ir.endWidth = 0.0f;
            return;
        }

        _ir.startWidth = 0.1f;
        _ir.endWidth = 0.1f;
        _ir.SetPosition(0, _parents.transform.position);
        _ir.SetPosition(1, transform.position);
    }
}
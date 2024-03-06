using Lightbug.CharacterControllerPro.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallTypeBullet : ActiveTool
{

    private Rigidbody _rd;

    [SerializeField]
    private Vector3 _start_point = Vector3.zero;
    private Magazine _magazine;
    private GrapplingTypeGun _gpgun;
    public override void ActiveAction()
    {
        base.ActiveAction();
    }

    public override void ActiveAction(GameObject target)
    {
        if (_rd.velocity != Vector3.zero)
            _rd.velocity = Vector3.zero;
        _rd.isKinematic = true;
        _is_trigger = true;
        Vector3 grapple_point = target.transform.position;

    }
    private void OnEnable()
    {
        _start_point = transform.position;
    }
    private void OnDisable()
    {
        _start_point = Vector3.zero;
        if (_rd.velocity != Vector3.zero)
            _rd.velocity = Vector3.zero;
    }
    public override void Init(GameObject obj)
    {
        //_target and obj is magazine object 
        base.Init(obj);
        _magazine = _target.GetComponent<Magazine>();
        _gpgun = _user.GetComponentInChildren<GrapplingTypeGun>();

    }
    public override void StopAction()
    {

    }
    private void Awake()
    {
        _rd = GetComponent<Rigidbody>();
        //_parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        float max_dist = _gpgun.BULLET_DISTANCE;
        float limit_dist = _gpgun.MAX_DISTANCE;
        if (!_is_trigger && max_dist > limit_dist)
        {
            StopAction();
            _gpgun.StopAction();

        }
    }
}

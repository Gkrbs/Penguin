using Lightbug.CharacterControllerPro.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingTypeBullet : ActiveTool
{
    enum INFO
    {
        SPRING,
        DAMPER,
        MIN_DISTANCE,
        MAX_DISTANCE,
        MASS_SCALE,
    }

    private Rigidbody _rd;
    private SpringJoint _joint;
    [SerializeField]
    private Vector3 _start_point = Vector3.zero;
    private Magazine _magazine;
    private GrapplingTypeGun _gpgun;
    //[SerializeField]
    //private Transform _parent;
    public NormalMovement normalMovement;
    public override void ActiveAction()
    {
        base.ActiveAction();
    }

    public override void ActiveAction(GameObject target)
    {
        Vector3 grapple_point = _gpgun.HIT_POS;
        if (grapple_point == Vector3.zero)
        {
            return;
        }
        if (_rd.velocity != Vector3.zero)
            _rd.velocity = Vector3.zero;
        _rd.isKinematic = true;
        _is_trigger = true;
        normalMovement.Grappling(true);
        transform.position = grapple_point;
        if (_joint == null)
            _joint = _user.gameObject.AddComponent<SpringJoint>();
        _joint.autoConfigureConnectedAnchor = false;
        _joint.connectedAnchor = grapple_point;
        float distanceFromPoint = Vector3.Distance(_user.transform.position, grapple_point);
        _joint.maxDistance = distanceFromPoint;//_data.f_datas[(int)INFO.MAX_DISTANCE];//distanceFromPoint * 0.25f;
        _joint.minDistance = 0.0f;

        _joint.spring = _data.f_datas[(int)INFO.SPRING];
        _joint.damper = _data.f_datas[(int)INFO.DAMPER];
        _joint.massScale = _data.f_datas[(int)INFO.MASS_SCALE];


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
        if (_rd.isKinematic) _rd.isKinematic = false;
    }
    public override void Init(GameObject obj)
    {
        //_target and obj is magazine object 
        base.Init(obj);
        _magazine = _target.GetComponent<Magazine>();
        _gpgun = _user.GetComponentInChildren<GrapplingTypeGun>();
        normalMovement = _user.GetComponentInChildren<NormalMovement>();

    }
    public override void StopAction()
    {
        _is_trigger = false;
        if (_joint != null)
        {
            MonoBehaviour.Destroy(_joint);
            normalMovement.Grappling(false);
            _joint = null;
        }
        _magazine.SetBullet(gameObject);
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

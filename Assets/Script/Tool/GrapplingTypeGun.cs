using Lightbug.CharacterControllerPro.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingTypeGun : ActiveTool
{
    private const string GRAPPLING_TYPE_GUN_DATA_PATH = "ScriptableOBjects\\GrapplingToolData";

    enum INFO
    {
        MIN_DISTINCE,
        MAX_DISTINCE,
        ANC_SPEED,
        JUMP_FORCE
    }

    private float _max_dist = 0f;
    private float _min_dist = 0f;
    private float _anc_speed = 0f;
    private float _jump_force = 0f;

    public CharacterActor characterActor;
    [SerializeField]
    private Transform _fire_point, _bullet;
    [SerializeField]
    private LineRenderer _ir;
    private GrapplingTypeBullet _glapple_bullet;
    private Gun _gun;
    [SerializeField]
    public float BULLET_DISTANCE
    {
        get
        {
            float res = 0f;
            if (_bullet != null)
            {
                res = Vector3.Distance(_fire_point.position, _bullet.position);
            }
            return res;

        }
    }

    public float MAX_DISTANCE
    {
        get
        {
            return _max_dist;
        }
    }
    public float MIN_DISTANCE
    {
        get
        {
            return _max_dist;
        }
    }
    private void Start()
    {
        if (_data == null)
        {
            _data = Resources.Load<ToolInfo>(GRAPPLING_TYPE_GUN_DATA_PATH);

        }
        _max_dist = _data.f_datas[(int)INFO.MAX_DISTINCE];
        _min_dist = _data.f_datas[(int)INFO.MIN_DISTINCE];
        _anc_speed = _data.f_datas[(int)INFO.ANC_SPEED];
        _jump_force = _data.f_datas[(int)INFO.JUMP_FORCE];
    }
    public override void Init(GameObject obj)
    {
        base.Init(obj);
        _is_trigger = true;
        if (_ir == null)
            _ir = GetComponent<LineRenderer>();
        if (_fire_point == null)
            _fire_point = _target.transform;
        if (_gun == null)
            _gun = GetComponent<Gun>();
        characterActor = _user.GetComponent<CharacterActor>();
        _gun.is_tool_control = true;
    }

    public override void ActiveAction()
    {
        if (!_is_trigger) return;

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
            characterActor.stableLayerMask = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Wall");
        }

        if (Input.GetKey(KeyCode.Q))
        {
            joint.maxDistance -= Time.deltaTime * _data.f_datas[(int)INFO.ANC_SPEED];
            if (joint.maxDistance < _min_dist)
                joint.maxDistance = _min_dist;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            joint.maxDistance += Time.deltaTime * _data.f_datas[(int)INFO.ANC_SPEED];
            if (joint.maxDistance > _max_dist)
                joint.maxDistance = _max_dist;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    public override void StopAction()
    {
        if (!_is_trigger) return;
        _is_trigger = false;
        _bullet = null;
        _glapple_bullet = null;
    }

    public override void ActiveAction(GameObject target)
    {
        if (!_is_trigger) return;
        _bullet = target.transform;
        _glapple_bullet = target.GetComponent<GrapplingTypeBullet>();
    }

    private void ReloadControl()
    {
        if (!_is_trigger) return;
        if (_glapple_bullet.IS_TRIGGER)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _glapple_bullet.StopAction();
                StopAction();
            }
        }
    }
    private void draw_line()
    {
        if (!_is_trigger)
        {
            _ir.enabled = false;
            _ir.startWidth = 0.0f;
            _ir.endWidth = 0.0f;
            return;
        }
        _ir.enabled = true;
        _ir.startWidth = 0.1f;
        _ir.endWidth = 0.1f;
        _ir.SetPosition(0, _fire_point.position);
        _ir.SetPosition(1, _bullet.position);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        draw_line();
        ActiveAction();
        ReloadControl();
    }

}

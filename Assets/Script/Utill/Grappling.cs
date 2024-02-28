using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : ActionInterface
{
    enum F_PARAMS
    {
        SPRING,
        DAMPER,
        MASS_SCALE
    }

    //
    //Parameter Default Setting
    //[SerializeField]
    //private float _max_distance = 7.0f;
    //[SerializeField]
    //private float _spring = 0.0f;
    //[SerializeField]
    //private float _damper = 7.0f;
    //[SerializeField]
    //private float _massScale = 4.5f;
    //
    private float max_dist = 0.0f;
    private float min_dist = 0.0f;
    private float _h_anc_speed = 3.0f;
    private ActiveObjectsScriptable _hook_info;
    private Vector3 _grapple_point;
    private SpringJoint _s_joint = null;
    private HingeJoint _h_joint = null;
    private bool _do_spring = false;
    [SerializeField]
    private Transform _player;
    public Grappling(bool do_spring = true)
    {
        _do_spring = do_spring;
    }
    public void Init(GameObject obj)
    {
        _player = obj.transform;
        max_dist = _player.gameObject.GetComponentInChildren<HookGun>().MAX_DISTANCE;
        min_dist = _player.gameObject.GetComponentInChildren<HookGun>().MIN_DISTANCE;
    }

    public void InitObjectInfo(ActiveObjectsScriptable data)
    {
        _hook_info = data;
    }

    public void ActiveAction()
    {
        if (_s_joint == null)
            return;
        if (Input.GetKey(KeyCode.Q))
        {
            _s_joint.maxDistance -= Time.deltaTime * _h_anc_speed;
            if (_s_joint.maxDistance < min_dist)
                _s_joint.maxDistance = min_dist;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            _s_joint.maxDistance += Time.deltaTime * _h_anc_speed;
            if (_s_joint.maxDistance > max_dist)
                _s_joint.maxDistance = max_dist;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    public void ActiveAction(GameObject target)
    {
        _grapple_point = target.transform.position;
        if (_do_spring)
        {

            if (_s_joint == null)
                _s_joint = _player.gameObject.AddComponent<SpringJoint>();
            _s_joint.autoConfigureConnectedAnchor = false;
            _s_joint.connectedAnchor = _grapple_point;

            float distanceFromPoint = Vector3.Distance(_player.position, _grapple_point);
            _s_joint.maxDistance = 2.5f;//distanceFromPoint * 0.25f;
            _s_joint.minDistance = 0.0f;

            _s_joint.spring = _hook_info.FLOAT_PARAMS_ARR[(int)F_PARAMS.SPRING];
            _s_joint.damper = _hook_info.FLOAT_PARAMS_ARR[(int)F_PARAMS.DAMPER];
            _s_joint.massScale = _hook_info.FLOAT_PARAMS_ARR[(int)F_PARAMS.MASS_SCALE];
        }
        else
        {
            if (_h_joint == null)
                _h_joint = _player.gameObject.AddComponent<HingeJoint>();
            _h_joint.autoConfigureConnectedAnchor = false;
            _h_joint.connectedAnchor = _grapple_point;

            float distanceFromPoint = Vector3.Distance(_player.position, _grapple_point);

            _h_joint.anchor = _grapple_point;

            //_h_joint.spring = _hook_info.FLOAT_PARAMS_ARR[(int)F_PARAMS.SPRING];
            //_h_joint.damper = _hook_info.FLOAT_PARAMS_ARR[(int)F_PARAMS.DAMPER];
            //_h_joint.massScale = _hook_info.FLOAT_PARAMS_ARR[(int)F_PARAMS.MASS_SCALE];

        }
    }

    public void StopAction()
    {
        if (_do_spring)
        {
            if (_s_joint != null)
            {
                MonoBehaviour.Destroy(_s_joint);
                _s_joint = null;
            }
        }
        else
        {
            if (_h_joint != null)
            {
                MonoBehaviour.Destroy(_h_joint);
                _h_joint = null;
            }
        }
    }
}

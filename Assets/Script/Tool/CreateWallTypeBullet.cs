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
    [SerializeField]
    private GameObject _wall;
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
        Vector3 col_point = target.transform.position;
        GameObject wall = Instantiate(_wall, col_point, transform.rotation);
        StopAction();

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
        _wall = Resources.Load<GameObject>("Wall");


    }
    public override void StopAction()
    {
        GetComponent<Bullet>().DelayDestroyBullet(1.0f);
    }
    private void Awake()
    {
        _rd = GetComponent<Rigidbody>();
        //_parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        float max_dist = Vector3.Distance(_start_point, transform.position);
        float limit_dist = GetComponent<Bullet>().MAX_DISTANCE;
        if (!_is_trigger && max_dist > limit_dist)
        {
            StopAction();
        }
    }
}

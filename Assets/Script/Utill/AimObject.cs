using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimObject : MonoBehaviour
{
    public static AimObject instanse;
    //[SerializeField]
    //float _move_speed = 10.0f;
    [SerializeField]
    private Transform _fire_point, _cam_tr;
    private float _max_distance = 15.0f;
    private float _default_max_distance = 15.0f;
    private Vector3 _aim_pos;
    public Vector3 AIM
    {
        get { return _aim_pos; }
    }
    public void Init(float dist)
    {
        _default_max_distance = dist;
        _max_distance = _default_max_distance;
    }

    public void SetMaxDistance(float dist)
    {
        if (dist != _max_distance)
            _max_distance = dist;
    }
    private void Awake()
    {
        if (instanse == null)
            instanse = GetComponent<AimObject>();
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target_pos = _cam_tr.position + _cam_tr.forward * _max_distance;
        float dist = Vector3.Distance(target_pos, _fire_point.transform.position);
        if (dist != _max_distance)
        {
            target_pos += _cam_tr.forward * (_max_distance - dist);
        }

        _aim_pos = target_pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDiskWall : MonoBehaviour
{
    private bool is_active = false;
    private bool is_complete = false;
    private float speed = 10.0f;
    //[SerializeField]
    //private float _max_distanse = 15.0f;
    private Vector3 targetPos = Vector3.zero;
    [SerializeField]
    private Transform _cam_tr, _aim_tr;
    private Vector3 start_pos = Vector3.zero;
    [SerializeField]
    LayerMask _target_layer;
    [SerializeField]
    [Range(0.0f, 5.0f)] private float radian = 0.5f;
    private void OnEnable()
    {
        start_pos = transform.position;
        if (_cam_tr == null)
            _cam_tr = Camera.main.transform;
        if (_aim_tr == null)
            _aim_tr = _cam_tr.GetChild(0);
        RaycastHit hit;
        float cam_dist = Vector3.Distance(_cam_tr.position, _aim_tr.position);
        print("cam : " + cam_dist);
        if (Physics.Raycast(_cam_tr.position, _cam_tr.forward, out hit, cam_dist, _target_layer))
        {
            targetPos = hit.point;
            is_complete = true;
        }
        else
        {
            targetPos = _aim_tr.position;
        }
        is_active = true;
    }

    private void Update()
    {
        if (!is_active) return;
        Vector3 dir = targetPos - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(dir.normalized);

        if (Vector3.Distance(targetPos, transform.position) <= radian)
        {
            is_active = false;
            if (!is_complete)
            {
                Destroy(gameObject);
            }
            is_complete = false;
        }        
    }
}

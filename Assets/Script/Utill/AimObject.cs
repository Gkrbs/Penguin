using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimObject : MonoBehaviour
{
    [SerializeField]
    float _move_speed = 10.0f;
    [SerializeField]
    private Transform _fire_point_tr;
    [SerializeField]
    private GrapplingTypeGun _gun;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _fire_point_tr.position) < (_gun.MAX_DISTANCE - 0.5f))
        {
            transform.Translate(Vector3.forward*Time.deltaTime * _move_speed);
        }
        else if(Vector3.Distance(transform.position, _fire_point_tr.position) > (_gun.MAX_DISTANCE + 0.5f))
        {
            transform.Translate(Vector3.forward*Time.deltaTime * -_move_speed);

        }
    }
}

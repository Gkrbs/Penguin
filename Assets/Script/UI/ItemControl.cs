using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    [SerializeField]
    private float _rot_speed = 10.0f;
   
    void Update()
    {
        transform.Rotate(0f, 0f, _rot_speed * Time.deltaTime);    
    }
}

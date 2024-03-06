using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AimCheck : MonoBehaviour
{
    public Image aimImage;
    public Transform firePoint;
    public Transform targetPoint;
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(firePoint.position,targetPoint.position,out hit, 15.0f))
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                aimImage.color = Color.blue;
        }
        else
            aimImage.color = Color.red;
    }
}

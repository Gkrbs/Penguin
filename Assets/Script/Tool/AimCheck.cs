using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AimCheck : MonoBehaviour
{
    public Image aimImage;
    public Transform firePoint;
    public Transform targetPoint;
    [SerializeField]
    LayerMask _target_layer;
    void Update()
    {
        aimImage.color = Physics.Linecast(firePoint.position, targetPoint.position, _target_layer) ? Color.blue : Color.red;
    }
}

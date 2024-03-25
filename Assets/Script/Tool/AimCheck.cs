using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AimCheck : MonoBehaviour
{
    public Image aimImage;
    public Transform camTr;
    [SerializeField]
    LayerMask _target_layer;
    void Update()
    {
        float cam_dist = Vector3.Distance(camTr.position, AimObject.instanse.AIM);//targetTr.position);
        aimImage.color = Physics.Raycast(camTr.position, camTr.forward, cam_dist + 0.5f, _target_layer) ? Color.blue : Color.red;
    }
}

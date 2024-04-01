using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCheck : MonoBehaviour
{
    public Animation anim;
    public bool isUp = true;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(isUp)
            {
                anim.CrossFade("GoDown");
                isUp = false;
            }
        }
    }
}

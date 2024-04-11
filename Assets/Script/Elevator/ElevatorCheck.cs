using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ElevatorCheck : MonoBehaviour
{
    public Animation anim;
    public ElevatorIsUpCheck elevatorIsUpCheck;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(elevatorIsUpCheck.isUp)
            {
                anim.CrossFade("GoDown");
            }
        }
    }
}

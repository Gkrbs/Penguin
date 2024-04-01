using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInCheck : MonoBehaviour
{
    public Animation anim;
    public ElevatorCheck elevatorCheck;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.CrossFade("GoUp");
            elevatorCheck.isUp = true;
        }
    }
}

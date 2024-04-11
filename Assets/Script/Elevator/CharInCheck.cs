using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInCheck : MonoBehaviour
{
    public Animation anim;
    public ElevatorIsUpCheck elevatorIsUpCheck;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!elevatorIsUpCheck.isUp)
            {
                anim.CrossFade("GoUp");
            }
        }
    }
}

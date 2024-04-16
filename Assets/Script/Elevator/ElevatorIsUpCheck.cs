using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ElevatorIsUpCheck : MonoBehaviour
{
    public bool isUp = true;
    public void IsUpCheck(int check)
    {
        isUp = Convert.ToBoolean(check);
    }
}

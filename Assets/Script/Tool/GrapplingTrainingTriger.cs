using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingTrainingTriger : MonoBehaviour
{
    public bool is_trigger = false;
    public string equals_tag = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(equals_tag))
            is_trigger = true;
    }
}

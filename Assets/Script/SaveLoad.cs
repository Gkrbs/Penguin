using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Core;

public class SaveLoad : MonoBehaviour
{
    Vector3 position;
    public CharacterActor characterActor;
    // Update is called once per frame
    void Update()
    {
        //if(GameManager.gamemode == hard) return;

        if(Input.GetKeyDown(KeyCode.T) && characterActor.IsGrounded)
        {
            position = transform.position;
            Debug.Log("save" + position);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.SetActive(false);
            transform.position = position;
            Debug.Log("load" + position);
            gameObject.SetActive(true);
        }
    }
}

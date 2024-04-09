using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Core;

public class SaveLoad : MonoBehaviour
{
    Vector3 position;
    public CharacterActor characterActor;
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.SELECTED_LEVEL == GameManager.LEVELS.NORMAL) gameObject.SetActive(false);

        if(Input.GetKeyDown(KeyCode.T) && characterActor.IsGrounded)
        {
            position = player.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            player.gameObject.SetActive(false);
            player.transform.position = position;
            player.gameObject.SetActive(true);
        }
    }
}

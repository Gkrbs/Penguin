using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Core;

public class SaveLoad : MonoBehaviour
{
    Vector3 position;
    public CharacterActor characterActor;
    public GameObject player;
    public Animator anim;

    private void Start()
    {
        if (GameManager.instance == null) return;
        if (GameManager.instance.SELECTED_LEVEL == GameManager.LEVELS.NORMAL)
        {
            gameObject.SetActive(false);
            return;
        }
        if(PlayerPrefs.HasKey("autoSaveX"))
        {
            GameManager.instance.autoSavePosition = new(PlayerPrefs.GetFloat("autoSaveX"),
                                                        PlayerPrefs.GetFloat("autoSaveY"),
                                                        PlayerPrefs.GetFloat("autoSaveZ"));
            LoadPosition(GameManager.instance.autoSavePosition);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance == null) return;
        if (characterActor.IsGrounded) AutoSave();
        else return;
        if (Input.GetKeyDown(KeyCode.T) && characterActor.IsGrounded)
        {
            anim.SetTrigger("Save");
            position = player.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadPosition(position);
        }
    }
    void LoadPosition(Vector3 position)
    {
        player.gameObject.SetActive(false);
        player.transform.position = position;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.gameObject.SetActive(true);
    }
    void AutoSave()
    {
        if (characterActor.IsGrounded)
        {
            GameManager.instance.autoSavePosition = player.transform.position;
            PlayerPrefs.SetFloat("autoSaveX", player.transform.position.x);
            PlayerPrefs.SetFloat("autoSaveY", player.transform.position.y);
            PlayerPrefs.SetFloat("autoSaveZ", player.transform.position.z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingPanal : MonoBehaviour
{
    private bool _end_credit = false;
    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private GameObject _credit_text_obj, _ending_text_obj;
    [SerializeField]
    private TMPro.TMP_Text _play_time_text;
    [SerializeField]
    private string SceneName;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("CresditText"))
        {
            other.gameObject.SetActive(false);
            _end_credit = true;
            if(Timer.instance != null)
                _play_time_text.text = "PLAY TIME : " + Timer.instance.PLAY_TIME;
            _ending_text_obj.SetActive(true);
        }
    }

    void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

    }

    // Update is called once per frame
    void Update()
    {
        float current_speed = _speed;
        if (Input.GetMouseButton(0))
        {
            current_speed *= 3;
        }

        if (_end_credit)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                bl_SceneLoaderManager.LoadScene(SceneName);
            }
        }
        else
        {
            _credit_text_obj.transform.Translate(0f, Time.deltaTime * current_speed, 0f);
        }
    }
}

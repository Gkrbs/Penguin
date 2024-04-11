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
    private AudioSource _audio;
    [SerializeField]
    private KeyCode _code;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("CresditText"))
        {
            credit_end();
        }
    }

    void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        _audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        if (_audio != null)
        {
            _audio.loop = true;
            _audio.Play();
        }
    }

    private void credit_end()
    {
        _credit_text_obj.gameObject.SetActive(false);
        _end_credit = true;
        if (Timer.instance != null)
            _play_time_text.text = "PLAY TIME : " + Timer.instance.PLAY_TIME;
        _ending_text_obj.SetActive(true);
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
            if (Input.GetKeyDown(_code))
            {
                credit_end();
            }
        }
    }
}

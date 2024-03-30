using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private bool _collision_player = false;

    [SerializeField]
    private string SceneName = "";
    [SerializeField]
    private GameObject _fadeout_panel;
    
    private Animation _fadeout_ani;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_fadeout_panel != null)
            {
                _fadeout_panel.SetActive(true);
                _fadeout_ani = _fadeout_panel.GetComponent<Animation>();
                _fadeout_ani.Play();
                _collision_player = true;
            }
            else
            {
                bl_SceneLoaderManager.LoadScene(SceneName);
            }
        }
    }
    private void Update()
    {
        if (_fadeout_panel)
        {
            if (_fadeout_ani != null&& !_fadeout_ani.isPlaying)
            {
                bl_SceneLoaderManager.LoadScene(SceneName);
            }
        }
    }
}

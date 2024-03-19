using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButton : ButtonControl
{
    [SerializeField]
    private Slider _loading_bar;
    [SerializeField]
    private GameObject _loading_panel;
    // Start is called before the first frame update
    void Start()
    {
    }
    public override void OnClickedEvent()
    {
        base.OnClickedEvent();
        SoundManager.instance.PlayOneShot(_audio, "StartAnimation");
        Time.timeScale = 1.0f;
        StartCoroutine(LoadingScene());

    }

    IEnumerator LoadingScene()
    {
        AsyncOperation operation =  SceneManager.LoadSceneAsync("TitleScene");
        _loading_panel.SetActive(true);
        
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        while (!operation.isDone)
        {
            float progress_value = Mathf.Clamp01(operation.progress / 0.9f);
            _loading_bar.value = progress_value;

            yield return null;
                
        }

    }


}

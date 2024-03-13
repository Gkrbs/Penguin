using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : ButtonControl
{
    // Start is called before the first frame update
    void Start()
    {
    }
    public override void OnClickedEvent()
    {
        base.OnClickedEvent();
        SoundManager.instance.PlayOneShot(_audio, "StartAnimation");
        SceneManager.LoadScene("PlayScen");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

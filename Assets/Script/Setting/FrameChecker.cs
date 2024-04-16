using UnityEngine;
using System.Collections;

public class FrameChecker : MonoBehaviour
{
    float deltaTime = 0.0f;
    GUIStyle style; Rect rect; float msec; float fps; float worstFps = 100f; string text;
    void Awake()
    {
        int w = Screen.width, h = Screen.height;
        rect = new Rect(0, 0, w, h * 4 / 150);
        style = new GUIStyle(); style.alignment = TextAnchor.UpperLeft; style.fontSize = h * 4 / 100; style.normal.textColor = Color.cyan;
        StartCoroutine("worstReset");
    }
    IEnumerator worstReset() 
    {    
        while (true) 
        {      
            yield return new WaitForSeconds(15f);
            worstFps = 100f;
        }
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }
    void OnGUI()
    {
        fps = 1.0f / deltaTime;  //초당 프레임 - 1초에 
        if (Time.deltaTime == 0)
            fps = 0;
        if (fps<worstFps) worstFps = fps;
            text = "fps " + fps.ToString ("F0");
        GUI.Label(rect, text, style);
    }
} 

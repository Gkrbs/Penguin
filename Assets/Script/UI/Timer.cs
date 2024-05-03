using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    private bool _start_timer = false;
    private float _time_sec = 0.0f;
    public float F_PLAY_TIME
    {
        get { return _time_sec; }
    }
    public string PLAY_TIME
    {
        get
        {
            int hour = (int)(_time_sec / 3600f);
            int minute = (int)(_time_sec / 60f);
            int second = (int)(_time_sec % 60f);
            string text = "";

            if (hour < 10)
                text = hour.ToString("D2") + ":" + minute.ToString("D2") + ":" + second.ToString("D2");
            else
                text = hour.ToString() + ":" + minute.ToString("D2") + ":" + second.ToString("D2");

            return text;
        }
    }
    private void Awake()
    {
        if (instance == null)
        { 
            instance = GetComponent<Timer>();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    private void Start()
    {
        _time_sec = 0.0f;
    }
    public void StartTimer()
    {
        _start_timer = true;
    }

    public void StopTimer()
    {
        _start_timer = false;
    }

    public void ResetTimer()
    {
        _time_sec = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(_start_timer)
            _time_sec += Time.deltaTime;
    }
}

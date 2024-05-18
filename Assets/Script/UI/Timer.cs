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
            var sec = System.TimeSpan.FromSeconds(_time_sec);
            var time = sec.ToString(@"hh\:mm\:ss");

            return time;
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

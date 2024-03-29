using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Timer : MonoBehaviour
{
    private GameObject daemon;

    //private TimeSpan dropDuration = new TimeSpan(0,0,0,0,800);
    // 800ms
    private float dropDuration = 0.8f;

    private float dropTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        daemon = GameObject.Find("Daemon");
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > dropTime)
        {
            ExecuteEvents.Execute<IBlockMovement>(daemon, null, (x, y) => x.Gravity());
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        dropTime = Time.time + dropDuration;
    }
}

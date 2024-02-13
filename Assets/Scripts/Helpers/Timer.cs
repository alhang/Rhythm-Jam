using UnityEngine;
using System.Collections;
using System;

public class Timer
{
    float time;
    Action callback;

    public Timer(float time, Action callback)
    {
        this.time = time;
        this.callback = callback;
    }

    public IEnumerator Start()
    {
        float waitUntil = Time.time + time;
        yield return new WaitUntil(() => SongManager.time >= waitUntil);
        callback.Invoke();
    }
}
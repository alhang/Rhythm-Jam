using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class BeatListener : MonoBehaviour
{
    // Look into UnityEvents, they're pretty helpful, they allow you to determine how methods are called in the editor
    public UnityEvent onBeatEvent;

    // Number of beats to trigger OnBeatHandler
    public int beatInterval = 4;

    // Number of beats since last trigger of OnBeatHandler
    private int beatCount = 0;

    private void OnEnable()
	{
        SongManager.OnBeat += OnBeatHandler;
	}

    private void OnDisable()
    {
        SongManager.OnBeat -= OnBeatHandler;
    }

    // SongManager will invoke the OnBeat event every 1/4 beat, so if beatInterval is 4, OnBeatEvent will invoke every beat
    public void OnBeatHandler()
    {
        beatCount++;
        if (beatCount >= beatInterval)
        {
            //Debug.Log("Beat");
            beatCount = 0;
            onBeatEvent?.Invoke();
        }
    }
}

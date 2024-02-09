using UnityEngine;
using System.Collections;

public class BeatListener : MonoBehaviour
{
	private void OnEnable()
	{
        SongManager.OnBeat += OnBeatHandler;
	}

    private void OnDisable()
    {
        SongManager.OnBeat -= OnBeatHandler;
    }

    public virtual void OnBeatHandler()
	{

	}
}


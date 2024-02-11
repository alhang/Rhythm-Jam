using UnityEngine;
using System.Collections;
using System;

public class SyncedTimer
{
	float time;
	Action callback;

	public SyncedTimer(float time, Action callback)
	{
		this.time = time;
		this.callback = callback;
	}

	public IEnumerator Start()
	{
		float waitUntil = SongManager.time + time;
		yield return new WaitUntil(() => SongManager.time >= waitUntil);
		callback.Invoke();
	}
}


using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Pulser : MonoBehaviour
{
    private int beatInterval = 4;
    public int beatCount = 0;

    private void OnEnable()
    {
        SongManager.OnBeat += OnBeatHandler;
    }

    private void OnDisable()
    {
        SongManager.OnBeat -= OnBeatHandler;
    }

    public void OnBeatHandler()
    {
        beatCount++;
        if (beatCount >= beatInterval)
        {
            beatCount = 0;
            Pulse();
        }
    }

    // Scale percentage
    public float scaleBy = 1.1f;
	private Vector2 originalScale;
	private Vector2 modifiedScale;

	// Animation length in seconds
	public float pulseTime = 0.1f;

    private void Start()
    {
		originalScale = transform.localScale;
        modifiedScale = originalScale * scaleBy;
    }

    public void Pulse()
	{
		StopAllCoroutines();
		StartCoroutine(PulseCoroutine());
	}

	IEnumerator PulseCoroutine()
	{
        transform.localScale = modifiedScale;

		float percentDone = 0;
		float timeElapsed = 0;
		while (timeElapsed < pulseTime)
		{
            timeElapsed += Time.deltaTime;
			percentDone = timeElapsed / pulseTime;
			transform.localScale = Vector2.Lerp(modifiedScale, originalScale, percentDone);
			yield return null;
		}

        transform.localScale = originalScale;
    }
}


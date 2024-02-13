using UnityEngine;
using System.Collections;

public class Pulser : MonoBehaviour
{
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


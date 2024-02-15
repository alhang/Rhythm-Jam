using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Animations;

public class Swayer : MonoBehaviour
{
    private float beatLength => SongManager.beatInterval;
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
            Sway();
        }
    }

    private void Start()
    {
        int rng = Random.Range(0, 2);
        if(rng == 0)
        {
            transform.eulerAngles = minRotation;
            //startRight = false;
            targetAngle = minRotation;
            currentAngle = maxRotation;
        }
        else
        {
            transform.eulerAngles = maxRotation;
            //startRight = true;
            targetAngle = maxRotation;
            currentAngle = minRotation;
        }
    }

    public void Sway()
    {
        Vector3 temp = currentAngle;
        currentAngle = targetAngle;
        targetAngle = temp;

        StopAllCoroutines();
        StartCoroutine(SwayCoroutine());
    }

    [SerializeField] private Vector3 minRotation;
    [SerializeField] private Vector3 maxRotation;
    [SerializeField] private Vector3 currentAngle;
    [SerializeField] private Vector3 targetAngle;

    IEnumerator SwayCoroutine()
    {
        AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        float percentDone = 0;
        float timeElapsed = 0;
        while (timeElapsed < beatLength)
        {
            timeElapsed += Time.deltaTime;
            percentDone = timeElapsed / beatLength;

            transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(currentAngle.z, targetAngle.z, curve.Evaluate(percentDone)));
            yield return null;
        }
    }
}


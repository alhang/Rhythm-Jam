using UnityEngine;
using System.Collections;
using TMPro;
using System;
using Unity.VisualScripting;

public class SongManager : Singleton<SongManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] TextMeshProUGUI sampledTimeDisplay;
    [SerializeField] AudioClip song;
    [SerializeField] float bpm;

    public static float time;
    public static float deltaTime;

    public static event Action OnBeat;
    // test commit
    public void Play(AudioClip audioClip)
    {
        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.Play();
        StartCoroutine(CalculateDeltaTime());
        StartCoroutine(InvokeOnBeat(bpm));
    }

    private IEnumerator InvokeOnBeat(float bpm)
    {
        while (true)
        {
            yield return new WaitUntil(() => audioSource.isPlaying);

            OnBeat?.Invoke();
            yield return new WaitForSeconds(60/bpm);
        }
    }

    private IEnumerator CalculateDeltaTime()
    {
        while (true)
        {
            yield return new WaitUntil(() => audioSource.isPlaying);

            float sampleTime = (float)audioSource.timeSamples / audioSource.clip.frequency;
            deltaTime = sampleTime - time;
            time = sampleTime;
            sampledTimeDisplay.text = time.ToString();

            yield return null;
        }
    }

    public void Stop()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            StopAllCoroutines();
        }

    }

    public static IEnumerator WaitForSeconds(float time)
    {
        float curTime = SongManager.time;
        yield return new WaitUntil(() => SongManager.time >= curTime + time);
    }
}


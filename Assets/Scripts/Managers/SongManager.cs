using UnityEngine;
using System.Collections;
using TMPro;
using System;

public class SongManager : Singleton<SongManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] TextMeshProUGUI sampledTimeDisplay;
    [SerializeField] AudioClip song;
    public float bpm;

    public static float time;
    public static float deltaTime;
    public static float timeSinceLastQuarterBeat;
    public static float quarterBeatInterval;

    public static event Action OnBeat;

    public void ChangeSong(AudioClip song)
    {
        this.song = song;
    }

    public void Play()
    {
        audioSource.Stop();
        audioSource.clip = song;
        audioSource.Play();
        StartCoroutine(CalculateDeltaTime());
    }

    private IEnumerator CalculateDeltaTime()
    {
        quarterBeatInterval = 60 / (bpm * 4);
        timeSinceLastQuarterBeat = 0;

        while (true)
        {
            // Wait until audioSource is playing
            yield return new WaitUntil(() => audioSource.isPlaying);

            // Calculate the number of timeSamples that have passed
            float sampleTime = (float) audioSource.timeSamples / audioSource.clip.frequency;

            // Assign variables
            deltaTime = sampleTime - time;
            time = sampleTime;
            timeSinceLastQuarterBeat += deltaTime;

            // Display number of samples
            //sampledTimeDisplay.text = time.ToString();

            // If on quarterBeat invoke event
            if(timeSinceLastQuarterBeat > quarterBeatInterval)
            {
                OnBeat?.Invoke();
                timeSinceLastQuarterBeat -= quarterBeatInterval;
                // Debug.Log("Quarter beat");
            }

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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmbientSound : MonoBehaviour
{
    [Min(0.5f)] public float playTimer;
    [Min(0.5f)] public float randomTimerRange;
    [Min(0)] public int audioIndex = 0;

    public bool useRandomTimer = true;
    public bool useAudioManager = true;

    public AudioSource audioSource = null;

    float timer;

    private void Start()
    {
        timer = playTimer;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && !audioSource.isPlaying)
        {
            if (useAudioManager)
            {
                AudioManager.Instance.PlayAudio(audioIndex);
            }
            else
            {
                audioSource?.Play();
            }
            if (useRandomTimer)
            {
                float minRange = Mathf.Max((playTimer - randomTimerRange), 0.5f);
                float maxRange = Mathf.Max((playTimer + randomTimerRange), 1);
                timer = Random.Range(minRange, maxRange);
            }
            else
            {
                timer = playTimer;
            }
        }
    }
}

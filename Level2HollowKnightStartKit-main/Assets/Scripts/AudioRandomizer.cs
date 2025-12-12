using System.Collections.Generic;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour
{
    public float pitchMin = 1f;
    public float pitchMax = 1f;

    public List<AudioClip> possibleSounds;

    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlayRandomizedSound()
    {
        if(audioSource == null || possibleSounds.Count == 0)
        {
            return;
        }

        audioSource.pitch = Random.Range(pitchMin, pitchMax);

        int randomSoundIndex = Random.Range(0, possibleSounds.Count);

        audioSource.PlayOneShot(possibleSounds[randomSoundIndex]);
    }
}

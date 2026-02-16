using System.Collections.Generic;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour
{
    [Header("Pitch Settings")]
    public float pitchMin = 1f;
    public float pitchMax = 1f;

    [Header("Volume Settings")]
    [Range(0f, 1f)]
    public float volume = 1f; // This is your new volume slider

    [Header("Possible Sounds")]
    public List<AudioClip> possibleSounds;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found, adding one automatically.");
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Call this whenever you want to play a random sound
    public void PlayRandomizedSound()
    {
        if (audioSource == null || possibleSounds == null || possibleSounds.Count == 0)
            return;

        // Randomize pitch
        audioSource.pitch = Random.Range(pitchMin, pitchMax);

        // Pick a random clip
        int randomIndex = Random.Range(0, possibleSounds.Count);
        AudioClip clip = possibleSounds[randomIndex];

        // Play with volume slider applied
        audioSource.PlayOneShot(clip, volume);

        // Reset pitch for other sounds
        audioSource.pitch = 1f;
    }
}

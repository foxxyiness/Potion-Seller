using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Dictionary to store audio clips and their corresponding AudioSource components
    private Dictionary<string, AudioSource> _audioDictionary = new Dictionary<string, AudioSource>();

    // Play audio method
    public void PlayAudio(string audioName, AudioClip audioClip)
    {
        // Check if audio clip exists in the dictionary
        if (!_audioDictionary.ContainsKey(audioName))
        {
            // Create a new GameObject for audio
            GameObject audioObject = new GameObject(audioName);
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();

            // Add the audio source to the dictionary
            _audioDictionary.Add(audioName, audioSource);

            // Play the audio clip
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            // If the audio clip is already playing, don't play another instance
            Debug.Log("Audio clip " + audioName + " is already playing.");
        }
    }
}

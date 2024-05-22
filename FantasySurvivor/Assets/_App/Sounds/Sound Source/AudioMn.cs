using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMn : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioClip musicClip;

    void Start()
    {
        // Ki?m tra xem musicAudioSource ?ã ???c gán ch?a tr??c khi s? d?ng
        if (musicAudioSource != null)
        {
            // Ki?m tra xem musicClip ?ã ???c gán ch?a
            if (musicClip != null)
            {
                musicAudioSource.clip = musicClip;
                musicAudioSource.Play();
            }
            else
            {
                Debug.LogError("B?n ch?a gán AudioClip cho musicClip!");
            }
        }
        else
        {
            Debug.LogError("B?n ch?a gán AudioSource cho musicAudioSource!");
        }
    }
}
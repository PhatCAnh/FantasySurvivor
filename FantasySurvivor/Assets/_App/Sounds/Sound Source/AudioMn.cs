using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMn : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioClip musicClip;

    void Start()
    {
        // Ki?m tra xem musicAudioSource ?� ???c g�n ch?a tr??c khi s? d?ng
        if (musicAudioSource != null)
        {
            // Ki?m tra xem musicClip ?� ???c g�n ch?a
            if (musicClip != null)
            {
                musicAudioSource.clip = musicClip;
                musicAudioSource.Play();
            }
            else
            {
                Debug.LogError("B?n ch?a g�n AudioClip cho musicClip!");
            }
        }
        else
        {
            Debug.LogError("B?n ch?a g�n AudioSource cho musicAudioSource!");
        }
    }
}
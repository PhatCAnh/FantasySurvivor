using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic backgroundMusic;

    [SerializeField] private AudioClip backgroundClip;
    private AudioSource audioSource;

    void Awake()
    {
        if (backgroundMusic == null)
        {
            backgroundMusic = this;
            

            // Add AudioSource component if it does not exist
            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            // Assign the AudioClip and start playing
            audioSource.clip = backgroundClip;
            audioSource.loop = true;
            audioSource.playOnAwake = false;  // Ensure it does not play automatically on Awake
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
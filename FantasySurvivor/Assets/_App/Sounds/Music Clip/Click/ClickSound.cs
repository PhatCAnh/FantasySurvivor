using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioSource audioSource; // Tham chi?u ??n AudioSource
    public AudioClip clickSound; // Tham chi?u ??n AudioClip

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Ph??ng th?c này s? ???c g?i khi game object b? nh?n
    public void OnClick()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}

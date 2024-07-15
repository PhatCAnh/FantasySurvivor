using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxEffect : AudioManager
{
    // Add specific effect-related sound clips here if needed
    public Sound[] effectSounds;

    private void Start()
    {
        // Example: Play effect sound on effect activation
        PlaySFX("FireballCast");
    }

    // Example method to play an effect sound by name
    public void PlayEffectSound(string name)
    {
        Sound s = Array.Find(effectSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Effect sound not found: " + name);
            return;
        }
        sfxSource.PlayOneShot(s.clip);
    }

    // Example method to toggle effect sound
    public void ToggleEffectSfx()
    {
        ToggleSfx();
    }

    // Example method to set effect sound volume
    public void SetEffectVolume(float volume)
    {
        SfxVolume(volume);
    }
}

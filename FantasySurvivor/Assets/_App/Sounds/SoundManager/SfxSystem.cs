using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxSystem : AudioManager
{
    // Add specific system-related sound clips here if needed
    public Sound[] systemSounds;

    private void Start()
    {
        // Example: Play system startup sound on start
        PlaySFX("SystemStartup");
    }

    // Example method to play a system sound by name
    public void PlaySystemSound(string name)
    {
        Sound s = Array.Find(systemSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("System sound not found: " + name);
            return;
        }
        sfxSource.PlayOneShot(s.clip);
    }

    // Example method to toggle system sound
    public void ToggleSystemSfx()
    {
        ToggleSfx();
    }

    // Example method to set system sound volume
    public void SetSystemVolume(float volume)
    {
        SfxVolume(volume);
    }
}
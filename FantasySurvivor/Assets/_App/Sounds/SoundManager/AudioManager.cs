using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SfxVolumeKey = "SfxVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // Lấy giá trị âm lượng đã lưu trữ
        float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 0.5f); // Giá trị mặc định là 0.5 nếu chưa lưu
        float sfxVolume = PlayerPrefs.GetFloat(SfxVolumeKey, 0.5f); // Giá trị mặc định là 0.5 nếu chưa lưu

        // Đặt âm lượng
        MusicVolume(musicVolume);
        SfxVolume(sfxVolume);
        PlayMusic("Theme");
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null )
        {
            Debug.Log("Sound not found");

        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("sfx not found");

        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
    public void ToggleMusic()
    {
        musicSource.mute= !musicSource.mute;
    }
    public void ToggleSfx()
    {
        sfxSource.mute= !sfxSource.mute;
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }
    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat(SfxVolumeKey, volume); 
    }
    public bool IsMusicOn()
    {
        return !musicSource.mute;
    }

    public bool IsSfxOn()
    {
        return !sfxSource.mute;
    }
}

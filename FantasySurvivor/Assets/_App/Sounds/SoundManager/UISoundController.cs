using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundController : MonoBehaviour
{
    public Slider _musicslider, _sfxSlider;
    [SerializeField] Image OnIconMusic;
    [SerializeField] Image OffIconMusic;
    [SerializeField] Image OnIconSfx;
    [SerializeField] Image OffIconSfx;

    private void Start()
    {
        _musicslider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        _sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 0.5f);

        UpdateMusicIcon();
        UpdateSfxIcon();
    }
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        UpdateMusicIcon();

    }
    public void ToggleSfx()
    {
        AudioManager.Instance.ToggleSfx();
        UpdateSfxIcon();

    }
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicslider.value);
      

    }
    public void SFXVolume()
    {
        AudioManager.Instance.SfxVolume(_sfxSlider.value);
       
    }


    private void UpdateMusicIcon()
    {
        bool isMusicOn = AudioManager.Instance.IsMusicOn(); 
        OnIconMusic.gameObject.SetActive(isMusicOn);
        OffIconMusic.gameObject.SetActive(!isMusicOn);
    }

    private void UpdateSfxIcon()
    {
        bool isSfxOn = AudioManager.Instance.IsSfxOn(); 
        OnIconSfx.gameObject.SetActive(isSfxOn);
        OffIconSfx.gameObject.SetActive(!isSfxOn);
    }
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Enums;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using FantasySurvivor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISoundController : View<GameApp>, IPopup
{
    public Slider _musicslider, _sfxSlider;
    [SerializeField] Image OnIconMusic;
    [SerializeField] Image OffIconMusic;
    [SerializeField] Image OnIconSfx;
    [SerializeField] Image OffIconSfx;
    [SerializeField] private Button _btnClose;
    [SerializeField] private Transform _goMainContent;

    private GameObject currentPopup;

    private void OnClickBtnClose()
    {
        Close();
    }



    protected override void OnViewInit()
    {
        base.OnViewInit();
        Open();
    }

    public void Open()
    {
        // Initialize sliders and icons
        _musicslider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        _sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 0.5f);

        UpdateMusicIcon();
        UpdateSfxIcon();

        // Initialize button listeners
        _btnClose.onClick.AddListener(OnClickBtnClose);

        _goMainContent.localScale = Vector3.zero;
        _goMainContent.DOScale(Vector3.one, 0.15f);
    }

    public void Close()
    {
        _goMainContent.DOScale(Vector3.zero, 0.15f)
            .OnComplete(() => { Destroy(gameObject); });
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
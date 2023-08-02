using ArbanFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : AudioManagerBase<AudioType>
{
    private AudioType _currentMusicKey;
    private GameApp _app => Singleton<GameApp>.instance;
    [SerializeField] SoundData soundData;
    private void Awake()
    {
        Singleton<AudioManager>.Set(this);
    }

    private void OnDestroy()
    {
        Singleton<AudioManager>.Unset(this);
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        //Add to here
        RegisterAudio(AudioType.Background, new AudioConfig(soundData.backgroundMusic, 1f));
    }

    public override void PlayMusic(AudioType type)
    {
        _currentMusicKey = type;
        if (!_app.models.settingModel.isMusic)
            return;

        base.PlayMusic(type);
    }

    public override void PlayEffect(AudioType type)
    {
        if (!_app.models.settingModel.isSound)
            return;

        base.PlayEffect(type);
    }

    public void SetEnableMusic(bool isEnable)
    {
        if (isEnable)
            PlayMusic(_currentMusicKey);
        else
            StopMusic();
    }
}

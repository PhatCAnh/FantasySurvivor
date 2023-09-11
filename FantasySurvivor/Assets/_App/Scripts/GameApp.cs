using ArbanFramework;
using ArbanFramework.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : AppBase
{
    public ModelManager models => Singleton<ModelManager>.instance;
    public ConfigManager configs => Singleton<ConfigManager>.instance;
    public ResourceManager resourceManager => Singleton<ResourceManager>.instance;
    public AudioManager audioManager => Singleton<AudioManager>.instance;
    public AdsController adsController => Singleton<AdsController>.instance;

    public override void OnInit()
    {
        Singleton<GameApp>.Set(this);
        Singleton<ModelManager>.Set(new ModelManager());
        Singleton<ConfigManager>.Set(new ConfigManager());
        Singleton<AdsController>.Set(new AdsController());

        configs.Init();
        models.Init();
        Application.targetFrameRate = 60;
        
#if UNITY_STANDALONE
         Screen.SetResolution(720, 1600, false);
#endif
    }
}

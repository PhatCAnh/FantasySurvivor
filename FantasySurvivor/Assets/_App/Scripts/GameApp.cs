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
	public AnalyticsController analytics => Singleton<AnalyticsController>.instance;

	public override void OnInit()
	{
		Singleton<GameApp>.Set(this);
		Singleton<ModelManager>.Set(new ModelManager());
		Singleton<ConfigManager>.Set(new ConfigManager());
		

		configs.Init();
		models.Init();

#if UNITY_ANDROID
		Singleton<AdsController>.Set(new AdsController());
		Singleton<AnalyticsController>.Set(new AnalyticsController());

        analytics.Init();
        adsController.Init(analytics);
#endif
		Application.targetFrameRate = 120;

#if UNITY_STANDALONE
		Screen.SetResolution(440, 960, false);
#endif
	}
}
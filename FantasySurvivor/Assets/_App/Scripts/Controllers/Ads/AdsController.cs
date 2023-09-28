using ArbanFramework;
using ArbanFramework.MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdsController: Controller<GameApp>, IUnityAdsInitializationListener
{
	public string androidGameId;
	public string iosGameId;

	public bool isTestingMode = true;

	private string _gameId;

	private void Awake()
	{
		Init();
	}
	public void Init()
	{
		InitializeInterstitialAds();
	}

	private void InitializeInterstitialAds()
	{

#if UNITY_ANDROID
        _gameId = androidGameId;
#elif UNITY_IOS
        _gameId = iosGameId;
#elif UNITY_Editor
		_gameId = androidGameId;
#endif
		if(!Advertisement.isInitialized && Advertisement.isSupported)
		{
			Advertisement.Initialize(_gameId, isTestingMode, this);
		}
	}
	public void OnInitializationComplete()
	{
		Debug.Log("Ads initialized");
	}
	public void OnInitializationFailed(UnityAdsInitializationError error, string message)
	{
		Debug.Log("Failed to init");
	}

	
	
}
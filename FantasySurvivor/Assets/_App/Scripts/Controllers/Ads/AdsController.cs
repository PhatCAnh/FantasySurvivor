using ArbanFramework;
using ArbanFramework.MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using FantasySurvivor;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdsController : IUnityAdsInitializationListener
{
	private string androidGameId = "5429857";
	private string iosGameId = "5429856";

	private string _androidRewardId = "Rewarded_Android";
	private string _iosRewardId = "Rewarded_iOS";

	private string _androidInterstitialId = "Interstitial_Android";
	private string _iosInterstitialId = "Interstitial_iOS";

	private string _androidBannerId = "Banner_Android";
	private string _iosBannerId = "Banner_iOS";

	private AnalyticsController _analyticsController;

	public bool isTestingMode = false;

	private string _gameId;

	private LoadReward _reward;
	private LoadInterstitial _interstitial;
	public void Init(AnalyticsController analyticsController)
	{
		InitializeInterstitialAds();
		_analyticsController = analyticsController;
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

		_reward = new LoadReward(_androidRewardId, _iosRewardId);
		_interstitial = new LoadInterstitial(_androidInterstitialId, _iosInterstitialId);
	}
	public void OnInitializationComplete()
	{
		Debug.Log("Ads initialized");
	}
	public void OnInitializationFailed(UnityAdsInitializationError error, string message)
	{
		Debug.Log("Failed to init");
	}

	public void ShowReward(Action callBack = null)
	{
		_reward.ShowAd(callBack);
		_analyticsController.TrackAds(TypeAds.Reward);
	}

	public void ShowInterstitial(Action callBack = null)
	{
		_interstitial.LoadAd(callBack);
		_analyticsController.TrackAds(TypeAds.Interstitial);
	}


}
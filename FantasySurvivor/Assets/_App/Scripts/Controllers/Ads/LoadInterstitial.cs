using System;
using UnityEngine;
using UnityEngine.Advertisements;
namespace FantasySurvivor
{
	public class LoadInterstitial : IUnityAdsLoadListener, IUnityAdsShowListener
	{
		public string androidGameId;
		public string iosGameId;

		private string _gameId;

		private Action _callBack;
		
		public LoadInterstitial(string androidId, string iosId)
		{
			this.androidGameId = androidId;
			this.iosGameId = iosId;
			
#if UNITY_ANDROID
			_gameId = this.androidGameId;
#elif UNITY_IOS
			_gameId = iosGameId;
#elif UNITY_Editor
			_gameId = androidGameId;
#endif
		}
		
		public void LoadAd(Action callback)
		{
			Debug.Log("Loading interstitial!!");
			Advertisement.Load(_gameId, this);
			_callBack = callback;
		}
		
		public void ShowAd()
		{
			Debug.Log("Showing ad!");
			Advertisement.Show(_gameId, this);
		}
		
		
		public void OnUnityAdsAdLoaded(string placementId)
		{
			Debug.Log("interstitial loaded!!");
			ShowAd();
		}
		public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
		{
			Debug.Log("interstitial failed to load!!");
		}
		public void OnUnityAdsShowClick(string placementId)
		{
			Debug.Log("interstitial clicked!!");
		}
		public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
		{
			Debug.Log("interstitial show failure!!");
		}
		public void OnUnityAdsShowStart(string placementId)
		{
			Debug.Log("interstitial show start!!");
		}
		
		public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
		{
			Debug.Log("interstitial show complete!!");
			_callBack?.Invoke();
		}
	}
}
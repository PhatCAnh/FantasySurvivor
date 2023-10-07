using System;
using UnityEngine;
using UnityEngine.Advertisements;
namespace FantasySurvivor
{
	public class LoadReward : IUnityAdsLoadListener, IUnityAdsShowListener
	{
		private string _androidGameId;
		private string _iosGameId;

		private string _gameId;

		private Action _callBack;

		public LoadReward(string androidGameId, string iosGameId)
		{
			this._androidGameId = androidGameId;
			this._iosGameId = iosGameId;
			
			#if UNITY_ANDROID
			_gameId = _androidGameId;
#elif UNITY_IOS
			_gameId = iosGameId;
#elif UNITY_Editor
			_gameId = androidGameId;
#endif
			
			LoadAd();
		}
		public void LoadAd()
		{
			Debug.Log("Loading Reward!!");
			Advertisement.Load(_gameId, this);
		}

		public void ShowAd(Action callback)
		{
			Debug.Log("Showing ad!");
			Advertisement.Show(_gameId, this);
			_callBack = callback;
		}


		public void OnUnityAdsAdLoaded(string placementId)
		{
			if(placementId.Equals(_gameId))
			{
				Debug.Log("Reward loaded!!");
			}
		}
		public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
		{
			Debug.Log("Reward failed to load!!");
		}
		public void OnUnityAdsShowClick(string placementId)
		{
			Debug.Log("Reward clicked!!");
		}
		public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
		{
			Debug.Log("Reward show failure!!");
		}
		public void OnUnityAdsShowStart(string placementId)
		{
			Debug.Log("Reward show start!!");
		}

		public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
		{
			if(placementId.Equals(_gameId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
			{
				Debug.Log("Reward show complete!!");
				_callBack?.Invoke();
			}
		}
	}
}
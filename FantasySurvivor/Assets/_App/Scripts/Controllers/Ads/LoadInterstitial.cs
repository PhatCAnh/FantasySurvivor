using UnityEngine;
using UnityEngine.Advertisements;
namespace _App.Scripts.Controllers.Ads
{
	public class LoadInterstitial : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
	{
		public string androidGameId;
		public string iosGameId;

		private string _gameId;

		private void Awake()
		{
#if UNITY_ANDROID
			_gameId = androidGameId;
#elif UNITY_IOS
			_gameId = iosGameId;
#elif UNITY_Editor
			_gameId = androidGameId;
#endif
		}
		public void LoadAd()
		{
			Debug.Log("Loading interstitial!!");
			Advertisement.Load(_gameId, this);
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
		}
	}
}
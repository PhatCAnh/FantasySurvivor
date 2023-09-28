using UnityEngine;
using UnityEngine.Advertisements;
namespace _App.Scripts.Controllers.Ads
{
	public class LoadReward : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
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
			Debug.Log("Loading Reward!!");
			Advertisement.Load(_gameId, this);
		}

		public void ShowAd()
		{
			Debug.Log("Showing ad!");
			Advertisement.Show(_gameId, this);
		}


		public void OnUnityAdsAdLoaded(string placementId)
		{
			if(placementId.Equals(_gameId))
			{
				Debug.Log("Reward loaded!!");
				ShowAd();
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
			if(placementId.Equals(_gameId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
			{
				Debug.Log("Reward show complete!!");
			}
		}
	}
}
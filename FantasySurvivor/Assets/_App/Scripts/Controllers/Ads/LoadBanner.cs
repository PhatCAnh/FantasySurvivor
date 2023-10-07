using System;
using UnityEngine;
using UnityEngine.Advertisements;
namespace FantasySurvivor
{
	public class LoadBanner : MonoBehaviour
	{
		public string androidGameId;
		public string iosGameId;
		
		private string _gameId;
		
		private BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

		private void Start()
		{
			#if UNITY_ANDROID
			_gameId = androidGameId;
#elif UNITY_IOS
        _gameId = iosGameId;
#elif UNITY_Editor
		_gameId = androidGameId;
#endif
			Advertisement.Banner.SetPosition(_bannerPosition);
		}
		
		public void LoadBannerInGame()
		{
			BannerLoadOptions options = new BannerLoadOptions()
			{
				loadCallback = OnBannerLoaded,
				errorCallback = OnBannerLoadError
			};
			Advertisement.Banner.Load(_gameId, options);
		}
		
		public void ShowBanner()
		{
			BannerOptions options = new BannerOptions
			{
				showCallback = OnBannerShow,
				clickCallback = OnBannerClicked,
				hideCallback = OnBannerHidden,
			};
			
			Advertisement.Banner.Show(_gameId, options);
		}
		
		public void HiddenBanner()
		{
			Advertisement.Banner.Hide();
		}
		
		private void OnBannerLoaded()
		{
			Debug.Log("Banner Loaded!!");
			ShowBanner();
		}
		
		private void OnBannerLoadError(string error)
		{
			Debug.Log("Banner failed to load " + error);
		}
		
		private void OnBannerShow()
		{
			
		}
		
		private void OnBannerClicked()
		{
			
		}
		
		private void OnBannerHidden()
		{
			
		}
		
	}
}
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Popup
{
	public class ChoiceMapPopup : View<GameApp>, IPopup
	{
		[Required, SerializeField] private Button _btnPlay;

		private GameController gameController => Singleton<GameController>.instance;

		protected override void OnViewInit()
		{
			base.OnViewInit();
			
			_btnPlay.onClick.AddListener(OnClickBtnPlay);
			
			
			Open();
		}

		public void Open()
		{
		}
		public void Close()
		{
			Destroy(gameObject);
		}

		private void OnClickBtnPlay()
		{
			var load = SceneManager.LoadSceneAsync("scn_Game", LoadSceneMode.Single);
			load.completed += o => gameController.StartGame();
			Close();
		}
	}
}

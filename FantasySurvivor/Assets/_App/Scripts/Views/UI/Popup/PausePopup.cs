using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Popup
{
	public class PausePopup : View<GameApp>, IPopup
	{
		[Required, SerializeField] private Button _btnExit;
		[Required, SerializeField] private Button _btnResume;
		
		private GameController gameController => Singleton<GameController>.instance;
		
		protected override void OnViewInit()
		{
			base.OnViewInit();
			_btnExit.onClick.AddListener(OnClickExit);
			_btnResume.onClick.AddListener(OnClickResume);

			Open();
		}
		
		public void Open()
		{
			gameController.isStopGame = true;
		}
		public void Close()
		{
			gameController.isStopGame = false;
			Destroy(gameObject);
		}

		private void OnClickExit()
		{
			gameController.LoseGame();
			Close();
		}

		private void OnClickResume()
		{
			Close();
		}
	}
}

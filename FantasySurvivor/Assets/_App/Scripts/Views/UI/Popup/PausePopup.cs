using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.UI;

namespace FantasySurvivor
{
	public class PausePopup : View<GameApp>, IPopup
	{
		[SerializeField] private Button _btnExit;
		[SerializeField] private Button _btnResume;
		
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

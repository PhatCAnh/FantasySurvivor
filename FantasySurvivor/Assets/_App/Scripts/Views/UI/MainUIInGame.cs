using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Popup
{
	public class MainUIInGame : View<GameApp>, IPopup
	{
		[Required, SerializeField] private Button _btnSetting;


		private GameController gameController => Singleton<GameController>.instance;

		public void Open()
		{
		}
		public void Close()
		{
		}


		protected override void OnViewInit()
		{
			base.OnViewInit();
			_btnSetting.onClick.AddListener(OnClickBtnSetting);
		}

		private void OnClickBtnSetting()
		{
			gameController.isStopGame = true;
		}
	}
}


using System;
using FantasySurvivor;
using UnityEngine;
namespace FantasySurvivor
{
	
	public class InteractBulletMonster : BulletMonster
	{
		protected override void OnViewInit()
		{
			base.OnViewInit();
			if(!app.models.dataPlayerModel.firstSeeBulletInteract)
			{
				app.resourceManager.ShowPopup(PopupType.ClickBulletTutorial);
				app.models.dataPlayerModel.firstSeeBulletInteract = true;
			}
		}

		private void OnMouseDown()
		{
			if(gameController.isStop) return;
			
			Touch();
		}
	}
}
using System;
using MR.CharacterState.Unit;
using UnityEngine;
namespace _App.Scripts.Views.InGame.Monsters
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
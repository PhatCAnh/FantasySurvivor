using System;
using MR.CharacterState.Unit;
using UnityEngine;
namespace _App.Scripts.Views.InGame.Monsters
{
	
	public class InteractBulletMonster : BulletMonster
	{

		private void OnMouseDown()
		{
			if(gameController.isStop) return;
			
			Touch();
		}
	}
}
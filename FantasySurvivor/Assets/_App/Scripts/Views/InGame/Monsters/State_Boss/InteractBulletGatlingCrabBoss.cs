using System;
using FantasySurvivor;
using UnityEngine;
namespace FantasySurvivor
{
	
	public class InteractBulletGatlingCrabBoss : BulletBossGatlingCrab
	{
		private void OnMouseDown()
		{
			if(gameController.isStop) return;
			
			Touch();
		}
	}
}
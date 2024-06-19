using System;
using FantasySurvivor;
using UnityEngine;
namespace FantasySurvivor
{
	
	public class InteractBulletBoss : BulletBossGatlingCrab
	{
		private void OnMouseDown()
		{
			if(gameController.isStop) return;
			
			Touch();
		}
	}
}
using System;
using System.Linq;
using _App.Scripts.Controllers;
using ArbanFramework;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
namespace FantasySurvivor
{
	public class SkillBombActive : SkillActive
	{
		public float sizeExplosion;
		
		
		[SerializeField] private float _time;
		
		[SerializeField] private GameObject _explosionEffect;

		private readonly Cooldown _cdTime = new Cooldown();
		
		public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
		{
			base.Init(data, target, level, type);

			if(target == null) return;
			
			_cdTime.Restart(_time);
		}

		private void Update()
		{
			if(gameController.isStop) return;
			_cdTime.Update(Time.deltaTime);
			if(_cdTime.isFinished)
			{
				TouchUnit();
			}
		}

		private void FixedUpdate()
		{
			if(gameController.isStop) return;
			CheckAoeMons();
		}

		protected override void TouchUnit(Monster mons = null)
		{
			foreach(var unit in gameController.listMonster.ToList())
			{
				if(gameController.CheckTouch(unit.transform.position, transform.position, sizeExplosion + unit.size))
				{
					base.TouchUnit(unit);
				}
			}
			var explosion = Instantiate(_explosionEffect, transform.position, quaternion.identity);
			explosion.transform.localScale = sizeExplosion * Vector3.one;
			Singleton<PoolController>.instance.ReturnObject(this.type, gameObject);
		}

		protected override void OnDrawGizmosSelected()
		{
			base.OnDrawGizmosSelected();
			Gizmos.DrawWireSphere(transform.position, sizeExplosion);
		}
	}
}
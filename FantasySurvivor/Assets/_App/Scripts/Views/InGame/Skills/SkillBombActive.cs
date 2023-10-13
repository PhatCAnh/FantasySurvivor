using System;
using System.Linq;
using ArbanFramework;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
namespace FantasySurvivor
{
	public class SkillBombActive : SkillActive
	{
		public float sizeExplosion;
		
		[SerializeField] private SpawnPos spawnPos;
		
		[SerializeField] private float _time;
		
		[SerializeField] private GameObject _explosionEffect;

		private readonly Cooldown _cdTime = new Cooldown();
		
		public override void Init(float damage, Monster target, int level)
		{
			base.Init(damage, target, level);

			if(target == null) return;

			transform.position = spawnPos == SpawnPos.Character ? origin.transform.position : base.target.transform.position;
			
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
				if(Vector2.Distance(unit.transform.position, transform.position) < sizeExplosion + unit.size)
				{
					base.TouchUnit(unit);
				}
			}
			var explosion = Instantiate(_explosionEffect, transform.position, quaternion.identity);
			explosion.transform.localScale = sizeExplosion * Vector3.one;
			Destroy(gameObject);
		}

		protected override void OnDrawGizmosSelected()
		{
			base.OnDrawGizmosSelected();
			Gizmos.DrawWireSphere(transform.position, sizeExplosion);
		}
	}
}
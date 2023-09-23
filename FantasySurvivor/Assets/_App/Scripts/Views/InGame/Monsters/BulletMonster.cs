﻿using System;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
namespace MR.CharacterState.Unit
{
	public class BulletMonster : View<GameApp>
	{
		public SpriteRenderer skin;

		public Rigidbody2D rigidbody2d;
		
		[SerializeField] protected GameObject deadEffect;

		[SerializeField] private float _speedBullet = 15;

		private Monster _origin;

		private TowerView _target;
		
		protected GameController gameController => Singleton<GameController>.instance;

		public void Init(Monster origin)
		{
			_origin = origin;
			_target = origin.target;
			var spawnPos = _origin.firePoint;
			transform.SetPositionAndRotation(spawnPos.position, spawnPos.rotation);
			rigidbody2d.velocity = spawnPos.up * _speedBullet;
			Destroy(gameObject, 3f);
		}

		protected void Touch()
		{
			if(deadEffect != null)
			{
				Instantiate(deadEffect, transform.position, Quaternion.identity);
			}
			
			Destroy(gameObject);
		}
		
		private void Update()
		{
			if(Vector2.Distance(transform.position, _target.transform.position) < 0.25f)
			{
				_target.TakeDamage(_origin.model.attackDamage);
				Touch();
			}
		}
	}
}
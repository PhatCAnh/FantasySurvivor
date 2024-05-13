using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BulletView : View<GameApp>
{
	[SerializeField] protected float moveSpeed;
	
	private Character _origin;

	protected GameObject callBackEffect;

	protected Monster target;

	protected Vector3 targetPos;

	protected bool isCritical;

	protected float damage;
	
	protected GameController gameController => Singleton<GameController>.instance;

	public void Init(Character character, Monster target, GameObject effect)
	{
		_origin = character;
		
		this.target = target;

		callBackEffect = effect;
		
		damage = _origin.model.attackDamage;
	}
	
	protected void FixedUpdate()
	{
		//sua lai
		if(gameController.isStop) return;

		if(target != null)
		{
			targetPos = target.transform.position;
			transform.up = targetPos - transform.position;
		}
		
		transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
		
		if(gameController.CheckTouch(transform.position, targetPos, 0.1f))
		{
			target.TakeDamage(damage, isCritical);
			TouchUnit(target);
		}
	}

	protected virtual void TouchUnit(Monster unit)
	{
		if(callBackEffect != null)
		{
			Instantiate(callBackEffect, unit.transform.position, quaternion.identity);
		}
		
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.TryGetComponent(out Monster monster))
		{
			monster.TakeDamage(damage, isCritical);
			TouchUnit(monster);
		}
	}

	private void IsCritical(int percent)
	{
		// _isCritical = Random.Range(0, 100) < percent;
		// if(_isCritical)
		// {
		// 	skin.color = new Color(1, 0.75f, 0);
		// 	_moveSpeed *= 1.25f;
		// 	_damage = _damage * _origin.model.criticalDamage / 100;
		// }
		// else
		// {
		// 	skin.color = Color.white;
		// }
	}
}
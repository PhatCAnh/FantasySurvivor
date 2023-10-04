using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BulletView : View<GameApp>
{
	[SerializeField] protected float moveSpeed;
	
	private Character _origin;

	protected Monster target;

	protected bool isCritical;

	protected float damage;
	
	protected GameController gameController => Singleton<GameController>.instance;

	public void Init(Character origin, Monster target)
	{
		_origin = origin;
		this.target = target;
		transform.up = target.transform.position - transform.position;
		damage = _origin.model.attackDamage;
		//IsCritical(origin.model.criticalRate);
	}
	
	protected void FixedUpdate()
	{
		//sua lai
		if(gameController.isStop) return;
		transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
		if(Vector2.Distance(transform.position, target.transform.position) < 0.1f)
		{
			target.TakeDamage(damage, isCritical);
			TouchUnit();
		}
	}

	protected virtual void TouchUnit()
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.TryGetComponent(out Monster monster))
		{
			monster.TakeDamage(damage, isCritical);
			TouchUnit();
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
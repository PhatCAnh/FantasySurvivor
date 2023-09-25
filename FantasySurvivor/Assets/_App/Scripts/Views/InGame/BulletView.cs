using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletView : MonoBehaviour
{
	public SpriteRenderer skin;

	public Rigidbody2D rigidbody2d;

	private TowerView _origin;

	private Vector3 _targetPos;

	private float _moveSpeed;

	private bool _isCritical;

	private int _damage;
	
	protected GameController gameController => Singleton<GameController>.instance;

	public void Init(TowerView origin)
	{
		_origin = origin;
		var spawnPos = _origin.firePoint;
		transform.SetPositionAndRotation(spawnPos.position, spawnPos.rotation);
		_targetPos = origin.target.transform.position;
		_moveSpeed = Mathf.Clamp(15 * origin.model.attackSpeed / 2, 10, 100);
		_damage = _origin.model.attackDamage;
		IsCritical(origin.model.criticalRate);
		Destroy(gameObject, 3f);
	}
	
	protected void FixedUpdate()
	{
		if(gameController.isStop) return;
		transform.position = Vector2.MoveTowards(transform.position, _targetPos, _moveSpeed * Time.deltaTime);
	}

	private void TouchUnit()
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.TryGetComponent(out Monster monster))
		{
			var damage = _damage;
			monster.TakeDamage(damage);
			TouchUnit();
		}
	}

	private void IsCritical(int percent)
	{
		_isCritical = Random.Range(0, 100) < percent;
		if(_isCritical)
		{
			skin.color = new Color(1, 0.75f, 0);
			_moveSpeed *= 1.25f;
			_damage = _damage * _origin.model.criticalDamage / 100;
		}
		else
		{
			skin.color = Color.white;
		}
	}
}
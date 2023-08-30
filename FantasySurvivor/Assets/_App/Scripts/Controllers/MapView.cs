using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using Cooldown = ArbanFramework.Cooldown;
using Random = UnityEngine.Random;

public class MapView : View<AppBase>
{
	[SerializeField] private Vector2 _size;

	[SerializeField] private float _startTime;
	
	[SerializeField] private float _cooldown;

	private readonly Cooldown _cooldownTime = new Cooldown();

	private GameController gameController => Singleton<GameController>.instance;

	protected override void Start()
	{
		base.Start();
		_cooldownTime.Restart(_startTime);
	}

	private void Update()
	{
		_cooldownTime.Update(Time.deltaTime);
		if(_cooldownTime.isFinished)
		{
			gameController.SpawnMonster(GetRandomPositionSpawn());
			_cooldownTime.Restart(_cooldown);
		}
	}

	private Vector2 GetRandomPositionSpawn()
	{
		var randomX = (Random.Range(0, 2) * 2 - 1) * Random.Range(0, _size.x / 2 + 1);
		var randomY = (Random.Range(0, 2) * 2 - 1) * Random.Range(0, _size.y / 2 + 1);
		return new Vector2(randomX, randomY);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireCube(transform.position, _size);
	}
}

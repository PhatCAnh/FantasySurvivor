using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using FantasySurvivor;
using UnityEngine;
using Cooldown = ArbanFramework.Cooldown;
using Random = UnityEngine.Random;

public class MapView : View<AppBase>
{
	public MapModel model { get; private set; }
	
	[SerializeField] private Vector2 _size;

	[SerializeField] private float _startTime;
	
	[SerializeField] private float _cooldown;

	private readonly Cooldown _cooldownTime = new Cooldown();
	private GameController gameController => Singleton<GameController>.instance;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		_cooldownTime.Restart(_startTime);
	}

	public void Init()
	{
		model = new();
	}
	
	private void Update()
	{
		if(gameController.isStop) return;
		_cooldownTime.Update(Time.deltaTime);
		if(_cooldownTime.isFinished)
		{
			gameController.SpawnMonster();
			_cooldownTime.Restart(_cooldown);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireCube(transform.position, _size);
	}
}

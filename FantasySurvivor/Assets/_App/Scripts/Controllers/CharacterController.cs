using System;
using ArbanFramework;
using ArbanFramework.MVC;
using MR;
using UnityEngine;
using Cooldown = ArbanFramework.Cooldown;

public class CharacterController : Controller<GameApp>
{
	public TowerView Tower => _gameController.tower;
	private GameController _gameController => Singleton<GameController>.instance;

	private void Awake()
	{
		Singleton<CharacterController>.Set(this);
	}

	private void Update()
	{
		if(_gameController.isStop) return;

		ControlMove();
	}
	private void ControlMove()
	{
		//Tower.Controlled(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
	}
}
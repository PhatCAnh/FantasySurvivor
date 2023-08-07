using System;
using ArbanFramework;
using ArbanFramework.MVC;
using MR;
using UnityEngine;
using Cooldown = ArbanFramework.Cooldown;

public class CharacterController : Controller<GameApp>
{
	public Character character => _gameController.character;
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
		character.Controlled(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
	}
}
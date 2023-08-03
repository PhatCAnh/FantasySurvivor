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

		ControlMove(Time.deltaTime);
	}
	private void ControlMove(float deltaTime)
	{
		var posX = Input.GetAxis("Horizontal");
		var posY = Input.GetAxis("Vertical");
		var position = new Vector2(posX, posY);
		character.Controlled(deltaTime, position);
	}
}
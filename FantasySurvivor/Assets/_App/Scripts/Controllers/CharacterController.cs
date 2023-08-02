using System;
using ArbanFramework;
using ArbanFramework.MVC;
using MR;
using UnityEngine;
using Cooldown = ArbanFramework.Cooldown;

public class CharacterController : Controller<GameApp>
{
	public Character character => _gameController.character;

	public CustomJoystick joystick;

	private GameController _gameController => Singleton<GameController>.instance;

	private Vector2 _oldJoystickPos;
	private Cooldown _check = new Cooldown();

	private bool _isReadyDash;

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
		if(joystick == null || character == null) return;
		var posX = joystick.Horizontal;
		var posY = joystick.Vertical;
		var joystickPosition = new Vector2(posX, posY);
		var charPos = character.transform.position;


		Camera.main.transform.position = new Vector3(charPos.x, charPos.y, Camera.main.transform.position.z);
		character.Controlled(deltaTime, joystickPosition);
	}

	public void ControlDash()
	{
		character.DashState();
	}
}
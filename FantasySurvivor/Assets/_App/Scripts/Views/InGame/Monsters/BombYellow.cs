using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class BombYellow : Monster
{
	[SerializeField] private GameObject _deadEffect;
	
	public override void Attack()
	{
		base.Attack();
		
		Die(true);
	}

	protected override void OnDestroy()
	{
		Instantiate(_deadEffect, transform.position, quaternion.identity);
		
		base.OnDestroy();
	}
}

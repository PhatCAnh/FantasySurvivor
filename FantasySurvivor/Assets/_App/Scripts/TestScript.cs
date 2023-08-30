using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
	[SerializeField] private Transform _target;
	
	private void FixedUpdate()
	{
		transform.up = _target.position - transform.position;
	}
}

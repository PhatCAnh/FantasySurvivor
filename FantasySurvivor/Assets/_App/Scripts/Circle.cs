using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
	private Rigidbody2D _theRb;

	private Vector3 _lastVelocity;

	private void Start()
	{
		_theRb = GetComponent<Rigidbody2D>();
		_theRb.velocity = new Vector2(15, 15);
		Invoke("tachra", 2f);
	}

	private void Update()
	{
		_lastVelocity = _theRb.velocity;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		var speed = new Vector2(15, 15).magnitude;
		var direction = Vector3.Reflect(_lastVelocity.normalized, other.contacts[0].normal);
		_theRb.velocity = direction * Mathf.Max(speed, 0f);
	}
}
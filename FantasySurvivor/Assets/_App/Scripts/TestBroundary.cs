using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestBroundary : MonoBehaviour
{
	private Camera cam;
	private float width;
	private float height;
	public EdgeCollider2D edge;

	// Start is called before the first frame update
	void Start()
	{
		cam = Camera.main;
		FindBoundaries();
		SetBoundary();
	}

	// Update is called once per frame
	void Update()
	{
		// FindBoundaries();
		// SetBoundary();
	}

	void SetBoundary()
	{
		Vector2 pointA = new Vector2(width / 2, height / 2);
		Vector2 pointB = new Vector2(width / 2, -height / 2);
		Vector2 pointC = new Vector2(-width / 2, -height / 2);
		Vector2 pointD = new Vector2(-width / 2, height / 2);
		Vector2[] tempArray = {pointA, pointB, pointC, pointD, pointA};
		edge.points = tempArray;
	}

	void FindBoundaries()
	{
		width = 1 / (cam.WorldToViewportPoint(new Vector3(1, 1, 0)).x - 0.5f);
		height = 1 / (cam.WorldToViewportPoint(new Vector3(1, 1, 0)).y - 0.5f);
	}
}
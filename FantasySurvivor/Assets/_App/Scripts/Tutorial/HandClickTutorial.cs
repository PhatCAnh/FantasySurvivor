using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using UnityEngine;

public class HandClickTutorial : View<GameApp>
{
	private void OnMouseEnter()
	{
		Destroy(gameObject);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;

public class HandClickTutorial : View<GameApp>
{
	private void OnMouseEnter()
	{
		if(Singleton<GameController>.instance.isStop) return;
		Destroy(gameObject);
	}
}

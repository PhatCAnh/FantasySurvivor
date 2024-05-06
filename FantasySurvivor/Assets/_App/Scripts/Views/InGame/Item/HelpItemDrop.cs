using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using DG.Tweening;
using UnityEngine;

public class HelpItemDrop : View<GameApp>
{
	[SerializeField] private Transform inCircle;
	[SerializeField] private Transform outCircle;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		SetRotate(inCircle);
		SetRotate(outCircle);
	}

	private void SetRotate(Transform trans)
	{
		trans.DORotate(new Vector3(0, 0, 360), 2f, RotateMode.FastBeyond360)
			.SetEase(Ease.Linear)
			.SetLoops(-1, LoopType.Restart);
	}

	private void Update()
	{
		if(Input.GetMouseButton(0) && inCircle.localScale.x < outCircle.localScale.x)
		{
			inCircle.localScale += new Vector3(0.01f, 0.01f, 0.01f);
		}
		if(Input.GetMouseButton(1))
		{
			inCircle.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		}
	}

}
using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class HelpItemDrop : View<GameApp>
{
	[SerializeField] private Transform inCircle;
	[SerializeField] private Transform outCircle;
	[SerializeField] private Transform questionMark;
	[SerializeField] private GameObject rewardEffectPrefab;

	private float _duration = 10;
	private float speed;

	protected override void OnViewInit()
	{
		base.OnViewInit();

		// ReSharper disable once PossibleLossOfFraction
		speed = 1 / _duration;
		
		SetRotate(inCircle);
		SetRotate(outCircle);
		questionMark.DOMoveY(1.5f, 1f).SetLoops(-1, LoopType.Yoyo);
		questionMark.DOScale(1, 1f).SetLoops(-1, LoopType.Yoyo);
	}

	private void SetRotate(Transform trans)
	{
		trans.DORotate(new Vector3(0, 0, 360), 2f, RotateMode.FastBeyond360)
			.SetEase(Ease.Linear)
			.SetLoops(-1, LoopType.Restart);
	}

	private void Update()
	{
		if(Input.GetMouseButton(0))
		{
			inCircle.localScale += speed * Time.deltaTime * Vector3.one ;
			if(inCircle.localScale.x >= outCircle.localScale.x)
			{
				Collected();
				Instantiate(rewardEffectPrefab, transform.position, quaternion.identity);
				Destroy(gameObject);
				return;
			}
		}
		if(Input.GetMouseButton(1))
		{
			inCircle.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		}
	}

	private void Collected()
	{
		inCircle.DOKill();
		outCircle.DOKill();
		questionMark.DOKill();
	}
}
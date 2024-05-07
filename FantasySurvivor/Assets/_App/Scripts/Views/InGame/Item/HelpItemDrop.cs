using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArbanFramework;
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
	
	private GameController gameController => Singleton<GameController>.instance;

	private float _duration = 10;
	private float _speed;
	private float _size = 5;

	protected override void OnViewInit()
	{
		base.OnViewInit();

		// ReSharper disable once PossibleLossOfFraction
		
		outCircle.localScale = Vector3.one * _size;
		_speed = _size / _duration;
		
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

	private void FixedUpdate()
	{
		if(gameController.isStop) return;
		if(gameController.GetDistanceCharacter(transform.position) < _size)
		{
			inCircle.localScale += _speed * Time.deltaTime * Vector3.one ;
			if(inCircle.localScale.x >= outCircle.localScale.x)
			{
				Collected();
				Instantiate(rewardEffectPrefab, transform.position, quaternion.identity);
				Destroy(gameObject);
				return;
			}
		}
	}

	private void Collected()
	{
		foreach(var mob in gameController.listMonster.ToList())
		{
			if(Vector2.Distance(transform.position, mob.transform.position) < _size + mob.size)
			{
				gameController.MonsterDie(mob);
			}
		}
		inCircle.DOKill();
		outCircle.DOKill();
		questionMark.DOKill();
		
	}
}
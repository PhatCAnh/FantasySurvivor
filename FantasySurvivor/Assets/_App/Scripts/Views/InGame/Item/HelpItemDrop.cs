using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class HelpItemDrop : View<GameApp>
{
	[SerializeField] private Transform inCircle;
	[SerializeField] private Transform outCircle;
	[SerializeField] private Transform questionMark;
	[SerializeField] private GameObject rewardEffectPrefab;
	[SerializeField] private SpriteRenderer originSprite;
	[SerializeField] private Sprite bombSprite;
	[SerializeField] private Sprite magnetSprite;
	[SerializeField] private Sprite foodSprite;
	
	
	private GameController gameController => Singleton<GameController>.instance;

	private float _duration;
	private float _speed;
	private int _size;
	private DropItemType _type;

	protected override void OnViewInit()
	{
		base.OnViewInit();

		// ReSharper disable once PossibleLossOfFraction
		_size = Random.Range(5, 11);
		_duration = Random.Range(2, 5) * 5;
		outCircle.localScale = Vector3.one * _size;
		_speed = _size / _duration;
		_type = (DropItemType) Random.Range(0, 3);
		switch (_type)
		{
			case DropItemType.Bomb:
				originSprite.sprite = bombSprite;
			break;
			case DropItemType.Food:
				originSprite.sprite = foodSprite;
				break;
			case DropItemType.Magnet:
				originSprite.sprite = magnetSprite;
				break;
		}
		
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
		if(gameController.CheckTouchCharacter(transform.position, _size))
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
		gameController.CollectedItemSpecial(_type);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		inCircle.DOKill();
		outCircle.DOKill();
		questionMark.DOKill();
	}
}
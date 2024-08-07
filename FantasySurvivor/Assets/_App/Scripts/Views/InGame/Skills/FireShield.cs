using System;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FireShield : SkillActive
{
	public Transform centerPoint; // Điểm trung tâm của vòng tròn
	public float radius = 3f; // Bán kính của vòng tròn
	public float duration = 2f; // Thời gian để di chuyển một vòng
	private List<Monster> monsters = new List<Monster>();
	private Tween _tween;
	private float _angleStep;
	private float angle;
	protected override void OnViewInit()
	{
		base.OnViewInit();
        AudioManager.Instance.PlaySFX("FireShield");
        MoveInCircle();
		centerPoint = gameController.character.transform;
	}

	public void UpdateAngle(float number)
	{
		_angleStep = number;
	}


	private void FixedUpdate()
	{
		if (gameController.isStop) return;
		HandleTouch();
	}

	void Update()
	{
		//Fix lại cái này
		// Kiểm tra giá trị của gameController.isStop
		if (gameController.isStop)
		{
		    // Dừng Tween nếu isStop là true
		    if (_tween.IsPlaying())
		    {
			    _tween.Pause();
		    }
		}
		else
		{
		    // Nếu isStop là false và Tween chưa được tạo hoặc không chạy, khởi động lại Tween
		    if (!_tween.IsPlaying())
		    {
			    _tween.Play();
		    }
		}
	}

	protected virtual void HandleTouch()
	{
		foreach (var mob in gameController.listMonster.ToList())
		{
		    if (!monsters.Contains(mob) && gameController.CheckTouch(transform.position, mob.transform.position, 0.5f + mob.size))
		    {
		        TakeDamage(mob);
		        monsters.Add(mob);
		    }
		}
	}

	private void MoveInCircle()
	{
		_tween = DOTween.To(() => 0f, value => UpdatePositions((value * -360f / duration) + _angleStep), 2, duration)
			.SetEase(Ease.Linear)
			.SetLoops(-1, LoopType.Restart);
	}
	public void UpdatePositions(float initialAngle)
	{
		float radian1 = (initialAngle + radius) * Mathf.Deg2Rad;
		Vector3 offset1 = new Vector3(
			radius * Mathf.Cos(radian1),
			radius * Mathf.Sin(radian1),
			0
		);
		transform.position = centerPoint.position + offset1;
	}

	protected void OnDisable()
	{
		_tween.Pause();
		_tween.Kill();
	}
}
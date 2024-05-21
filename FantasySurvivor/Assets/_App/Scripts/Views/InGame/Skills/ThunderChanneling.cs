using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ThunderChanneling : SkillBulletActive
{
	[SerializeField] private GameObject _ChannelingInstantiate;
	[SerializeField] private float maxRange;
	private int channelingTime = 3;

	public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
	{
		base.Init(data, target, level, type);

		if(target == null) return;

		this.direction = target.transform.position - transform.position;

		skin.up = direction;
		if(channelingTime > 0)
		{
			callBackDamaged += SpawnChanneling;
		}
	}


	private void SpawnChanneling()
	{

		// channelingTime -= 1;
		// Monster nearestMonster = gameController.FindNearestMonster(this.transform.position, maxRange, target);
		//
		// if(nearestMonster != null)
		// {
		// 	var bullet = Instantiate(_ChannelingInstantiate, transform.position, Quaternion.identity);
		// 	bullet.GetComponent<ThunderChanneling>().Init(damage, nearestMonster, level);
		// }
		//
		// Destroy(gameObject);
	}
}
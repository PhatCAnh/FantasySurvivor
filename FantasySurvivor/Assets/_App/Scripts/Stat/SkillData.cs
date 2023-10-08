﻿using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SkillData
{
	public SkillName name;
	public SkillType type;
	public SkillElementalType typeElemental;
	public Sprite imgUI;
	public string description;
	public GameObject skillPrefab;
	public GameObject effectPrefab;
	public Dictionary<int, LevelSkillData> levelSkillData;

	public void Init(Dictionary<int, LevelSkillData> data)
	{
		levelSkillData = data;
	}

	public LevelSkillData GetData(int level)
	{
		return levelSkillData[level];
	}
}

public class LevelSkillData
{
	public int value;
	public float cooldown;
	public string description;

	public LevelSkillData(int value, float cooldown, string description)
	{
		this.value = value;
		this.cooldown = cooldown;
		this.description = description;
	}
}
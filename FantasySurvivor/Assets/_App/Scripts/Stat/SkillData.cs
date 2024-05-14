using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SkillData
{
	public SkillName name;
	public SkillType type;
	public SpawnPos spawnPos;
	public ItemPrefab normalSkillType;
	public ItemPrefab evolveSkillType;
	public SkillElementalType typeElemental;
	public Sprite imgUI;
	public GameObject skillEvolvePrefab;
	public bool canAppear = true;
	
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
	public float value;
	public float cooldown;
	public string description;

	public LevelSkillData(float value, float cooldown, string description)
	{
		this.value = value;
		this.cooldown = cooldown;
		this.description = $"{description}";
	}
}
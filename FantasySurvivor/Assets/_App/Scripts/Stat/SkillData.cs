using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SkillData
{
	public SkillId id;
	public SkillName name;
	public SkillType type;
	public SpawnPos spawnPos;
	public ItemPrefab normalSkillType;
	public ItemPrefab evolveSkillType;
	public SkillElementalType typeElemental;
	public Sprite imgUI;
	public bool canAppear = true;
	public bool ChoiceSkill = false;
	
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
	public float valueSpecial1;
	public float valueSpecial2;
	public float valueSpecial3;

	public LevelSkillData(float value, float cooldown, string description, float vs1, float vs2, float vs3)
	{
		this.value = value;
		this.cooldown = cooldown;
		this.description = $"{description}";
		this.valueSpecial1 = vs1;
		this.valueSpecial2 = vs2;
		this.valueSpecial3 = vs3;
	}
}
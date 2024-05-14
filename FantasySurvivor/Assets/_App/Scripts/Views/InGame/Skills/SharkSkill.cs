using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _App.Scripts.Controllers;
using ArbanFramework;
using FantasySurvivor;
using UnityEngine;
using Cooldown = ArbanFramework.Cooldown;
using Random = UnityEngine.Random;

public class Skill
{
	public SkillName skillName;

	public int level;

	protected Character origin;

	protected GameController gameController => ArbanFramework.Singleton<GameController>.instance;

	protected SpawnPos spawnPos;
	
	protected ItemPrefab skillPrefab;

	protected ItemPrefab normalSkillPrefab;

	protected ItemPrefab evolveSkillPrefab;

	protected Dictionary<int, LevelSkillData> levelData;

	public readonly Cooldown cooldownSkill = new Cooldown();

	public virtual void Active()
	{

	}

	public virtual void Init(SkillData data)
	{
		this.skillName = data.name;

		this.origin = gameController.character;

		level = 1;

		spawnPos = data.spawnPos;

		normalSkillPrefab = data.normalSkillType;

		evolveSkillPrefab = data.evolveSkillType;

		skillPrefab = normalSkillPrefab;

		levelData = data.levelSkillData;
	}

	public virtual void UpLevel()
	{
		level++;
		if(level == 6 && normalSkillPrefab != evolveSkillPrefab)
		{
			skillPrefab = evolveSkillPrefab;
		}
	}

	public virtual void CoolDownSkill(float deltaTime)
	{
		cooldownSkill.Update(deltaTime);
		if(cooldownSkill.isFinished)
		{
			Active();
			cooldownSkill.Restart(levelData[level].cooldown);
		}
	}
}

public class ProactiveSkill : Skill
{
	protected int numberProjectile = 1;
	protected int timeDelaySkill = 1;

	public override void Init(SkillData data)
	{
		base.Init(data);
		AddCooldown();
	}

	public override async void Active()
	{
		base.Active();
		var mons = gameController.GetAllMonsterInAttackRange();
		if(mons != null)
		{
			for(int i = 0; i < numberProjectile; i++)
			{
				var randomMob = mons[Random.Range(0, mons.Count)];
				Singleton<PoolController>.instance.GetObject(skillPrefab, SetPositionSpawn(spawnPos, randomMob)).TryGetComponent(out SkillActive skill);
				skill.Init(origin.model.attackDamage * levelData[level].value / 100, randomMob, level, skillPrefab);
				UpdatePrefab(skill);
				await Task.Delay(timeDelaySkill);
			}
		}
	}

	protected Vector3 SetPositionSpawn(SpawnPos type, Monster mob)
	{
		return type switch
		{
			SpawnPos.Character => gameController.character.transform.position,
			SpawnPos.Monster => mob.transform.position,
		};
	}

	protected virtual void AddCooldown()
	{
		origin.listSkillCooldown.Add(this);
	}

	protected virtual void UpdatePrefab(SkillActive prefab)
	{

	}
}
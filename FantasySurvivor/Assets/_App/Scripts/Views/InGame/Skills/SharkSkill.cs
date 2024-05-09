using System.Collections.Generic;
using System.Threading.Tasks;
using FantasySurvivor;
using UnityEngine;
using Cooldown = ArbanFramework.Cooldown;

public class Skill
{
	public SkillName skillName;
	
	public int level;
	
	protected Character origin;
	protected GameController gameController => ArbanFramework.Singleton<GameController>.instance;

	protected GameObject skillPrefab, skillEvolvePrefab, skillIns;
	
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
		
		skillPrefab = data.skillPrefab;

		skillEvolvePrefab = data.skillEvolvePrefab;

		skillIns = skillPrefab;

		levelData = data.levelSkillData;
	}

	public virtual void UpLevel()
	{
		level++;
		if(level == 6 && skillEvolvePrefab != null)
		{
			skillIns = skillEvolvePrefab;
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
				var skill = GameObject.Instantiate(skillIns).GetComponent<SkillActive>();
				skill.Init(origin.model.attackDamage * levelData[level].value / 100, mons, level);
				UpdatePrefab(skill);
				await Task.Delay(timeDelaySkill);
			}
		}
	}

	protected virtual void AddCooldown()
	{
		origin.listSkillCooldown.Add(this);
	}

	protected virtual void UpdatePrefab(SkillActive prefab)
	{
		
	}
}


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

	protected GameObject skillPrefab;
	
	protected GameObject effectPrefab;


	protected Dictionary<int, LevelSkillData> levelData;

	protected readonly Cooldown cooldownSkill = new Cooldown();
	
	public virtual void Active()
	{

	}

	public virtual void Init(SkillData data)
	{
		this.skillName = data.name;
		
		this.origin = gameController.character;

		level = 1;
		
		skillPrefab = data.skillPrefab;
		effectPrefab = data.effectPrefab;

		levelData = data.levelSkillData;
	}

	public virtual void UpLevel()
	{
		level++;
	}
	
	public void CoolDownSkill(float deltaTime)
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
	
	public override async void Active()
	{
		base.Active();
		var mons = gameController.GetRandomMonster();
		if(mons != null)
		{
			for(int i = 0; i < numberProjectile; i++)
			{
				var skill = gameController.SpawnBullet(skillPrefab);
				skill.GetComponent<SkillActive>().Init(origin.model.attackDamage * levelData[level].value / 100, mons, effectPrefab, level);
				await Task.Delay(timeDelaySkill);
			}
		}
	}
}



public class BuffSkill : Skill
{
	
}
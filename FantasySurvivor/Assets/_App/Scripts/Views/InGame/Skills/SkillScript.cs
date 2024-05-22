using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
namespace FantasySurvivor
{
    public class FireBall : ProactiveSkill
    {
        public override void Init(SkillData data)
        {
            base.Init(data);
            timeDelaySkill = Mathf.RoundToInt(levelData.Last().Value.cooldown / 2 * 1000);
        }

        public override void UpLevel()
        {
            base.UpLevel();
            if (level == 3 || level == 5)
            {
                numberProjectile++;
            }
            if (level == 6)
            {
                numberProjectile = 1;
            }
        }
    }

    public class EarthPunch : ProactiveSkill
    {
        public override void Init(SkillData data)
        {
            base.Init(data);
        }
    }

    public class Poisonball : ProactiveSkill
    {
        public override void Init(SkillData data)
        {
            base.Init(data);
        }

    }

    /*public class ThunderBird : ProactiveSkill
	{
        public override void Init(SkillData data)
        {
            base.Init(data);
            timeDelaySkill = Mathf.RoundToInt(levelData.Last().Value.cooldown / 2 * 1000);
        }
        public override void UpLevel()
        {
            base.UpLevel();
            if (level == 3)
            {
                numberProjectile++;
            }
        }
    }*/

    /*public class Twin : ProactiveSkill
	{
		public override void Init(SkillData data)
		{
			base.Init(data);
			timeDelaySkill = Mathf.RoundToInt(1000);
		}

		public override void UpLevel()
		{
			base.UpLevel();
			if(level == 3)
			{
				numberProjectile++;
			}
		}
	}*/

    public class Shark : ProactiveSkill
    {
        protected override void UpdatePrefab(SkillActive prefab)
        {
            base.UpdatePrefab(prefab);
            prefab.transform.localScale = Vector3.one * levelData[level].valueSpecial1;
            prefab.size = levelData[level].valueSpecial1;

        }
    }

    /*public class ZoneOfJudgment : ProactiveSkill
	{
		private float _sizeTouch = 2.5f;

		private List<Monster> _monstersTouched = new List<Monster>();

		private GameObject _circle;

		public override void Init(SkillData data)
		{	
			base.Init(data);
			var prefab = GameObject.Instantiate(skillPrefab, origin.transform.position, quaternion.identity, origin.transform);
			prefab.transform.DORotate(new Vector3(0, 0, -360), 7.5f, RotateMode.FastBeyond360)
				.SetLoops(-1, LoopType.Incremental)
				.SetEase(Ease.Linear);
			prefab.GetComponent<SpriteRenderer>().DOColor(new Color(1, 0.9f, 0.3f), 2)
				.SetLoops(-1, LoopType.Yoyo)
				.SetEase(Ease.Linear);
			_circle = prefab;
		}


		public override void Active()
		{
			_monstersTouched.Clear();
		}

		public override void CoolDownSkill(float deltaTime)
		{
			base.CoolDownSkill(deltaTime);
			foreach(var monster in gameController.listMonster.ToList())
			{
				if(_monstersTouched.Contains(monster)) continue;

				if(Vector2.Distance(monster.transform.position, origin.transform.position) < _sizeTouch)
				{
					monster.TakeDamage(origin.model.attackDamage * levelData[level].value / 100);
					_monstersTouched.Add(monster);
				}
			}
		}

		public override void UpLevel()
		{
			base.UpLevel();
			_sizeTouch += 0.5f;
			_circle.transform.localScale = Vector2.one * _sizeTouch;
		}
	}*/

    public class ThunderStrike : ProactiveSkill
    {
        public override void Init(SkillData data)
        {
            base.Init(data);
        }
        public override void UpLevel()
        {
            base.UpLevel();
            if (level == 3 || level == 5)
            {
                numberProjectile++;
            }
        }
    }

    public class Skyboom : ProactiveSkill
    {
        public override void Init(SkillData data)
        {
            base.Init(data);
        }
        protected override void UpdatePrefab(SkillActive prefab)
        {
            base.UpdatePrefab(prefab);
            prefab.transform.localScale = Vector3.one * levelData[level].valueSpecial1;
            prefab.size = levelData[level].valueSpecial1;
        }
    }
    public class waterball : ProactiveSkill
    {
        public override void Init(SkillData data)
        {
            base.Init(data);
            timeDelaySkill = Mathf.RoundToInt(levelData.Last().Value.cooldown / 2 * 1000);

        }


        public override void UpLevel()
        {
            base.UpLevel();
            if (level == 3 || level == 5)
            {

                numberProjectile++;
            }
            if (level == 6)
            {
                numberProjectile = 1;
            }
        }

    }
}


/*public class BlackDrum : ProactiveSkill
{
	protected override void UpdatePrefab(SkillActive prefab)
	{
		base.UpdatePrefab(prefab);
		prefab.GetComponent<SkillBombActive>().sizeExplosion += 0.5f * level;
	}
}*/
﻿using System.Collections.Generic;
using System.Linq;
using ArbanFramework;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;
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

    public class ThunderChannelingFl : ProactiveSkill
    {
        public override void Init(SkillData data)
        {
            base.Init(data);
            base.UpLevel();
            timeDelaySkill = Mathf.RoundToInt(levelData.Last().Value.cooldown / 2 * 1000);
        }
        public override void UpLevel()
        {
            base.UpLevel();

        }
    }

    public class FireShieldLv : ProactiveSkill
    {

        private List<Monster> _monstersTouched = new List<Monster>();
        public float speed = 1f;
        private List<FireShield> fireBalls = new List<FireShield>();
        private Cooldown cdExist = new Cooldown();
        private float radius = 3;
        public override void Init(SkillData data)
        {
            base.Init(data);
            var skill = GameObject.Instantiate(skillIns).GetComponent<FireShield>();
            skill.Init(origin.model.attackDamage * levelData[level].value / 100, null, level);
            fireBalls.Add(skill);
            level = 3;
        }
        public override void UpLevel()
        {
            base.UpLevel();
        }

        public override void Active()
        {
            _monstersTouched.Clear();
            float radius = 3f; // Bán kính của vòng tròn mà các quả cầu lửa sẽ xoay quanh
            float angleStep = 360f / level; // Góc giữa các quả cầu lửa

            for (int i = 0; i < level; i++)
            {
                float currentAngle = i * angleStep;
                float radian = currentAngle * Mathf.Deg2Rad;
                float x = radius * Mathf.Cos(radian);
                float y = radius * Mathf.Sin(radian);
                var skill = GameObject.Instantiate(skillIns, new Vector3(x, y, 0), Quaternion.identity).GetComponent<FireShield>();
                if (skill != null)
                {
                    skill.Init(origin.model.attackDamage * levelData[level].value / 100, null, level);
                    fireBalls.Add(skill);
                    Debug.Log(skill.transform.position);
                    /*DOTween.To(() => 0f, value =>
                        skill.UpdatePosition(value * currentAngle)
                    , 1f, 4)
                        .SetEase(Ease.Linear)
                        .SetLoops(-1, LoopType.Restart);*/
                }
            }

            cdExist.Restart(4);
        }
        public override void CoolDownSkill(float deltaTime)
        {
            /*if (cdExist.isFinished)
            {
                foreach (var item in fireBalls.ToList())
                {
                    GameObject.Destroy(item.gameObject);
                }
                fireBalls.Clear();

                cooldownSkill.Update(deltaTime);
                if (cooldownSkill.isFinished)
                {
                    Active();
                    cooldownSkill.Restart(levelData[level].cooldown);
                }
            }
            else
            {
                cdExist.Update(deltaTime);

            }*/
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

    /*public class Shark : ProactiveSkill
	{
		protected override void UpdatePrefab(SkillActive prefab)
		{
			base.UpdatePrefab(prefab);
			if(level == 6)
			{
				prefab.transform.localScale = Vector3.one * 2;
				prefab.size = 2;
			}
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

        public override void Active()
        {
            var mons = gameController.GetAllMonsterInAttackRange();
            if (mons != null)
            {
                for (int i = 0; i < numberProjectile; i++)
                {
                    var skill = GameObject.Instantiate(skillIns).GetComponent<SkillActive>();
                    skill.Init(origin.model.attackDamage * levelData[level].value / 100, mons[Random.Range(0, mons.Count)], level);
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
}
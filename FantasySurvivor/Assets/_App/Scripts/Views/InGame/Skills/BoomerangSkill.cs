using ArbanFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FantasySurvivor
{
    public class BoomerangSkill  : SkillActive
    {

        [SerializeField] protected Transform boomerang;

        public SpawnPos spawnPos;

        public TargetType targetType;

        public bool canBlock;

        public float moveSpeed;

        protected Monster oldTarget;

        protected Vector3 targetPos;

        protected Vector3 direction;

        protected Vector3 playerPos;

        protected Vector3 endPos;

        protected Character character;

        protected Vector3 characterPos;

        private List<Monster> _listMonsterTouched;
        private List<Monster> _listMonsterTouchedReturn;

        enum FlyState
        {
            bumShot,
            bumReturn
        }

        private FlyState flyState;


        public override void Init(float damage, Monster target, int level)
        {
            base.Init(damage, target, level);

            if (target == null) return;

            transform.position = spawnPos == SpawnPos.Character ? origin.transform.position : target.transform.position;

            this.direction = target.transform.position - transform.position;

            boomerang.up = direction;

            character = gameController.character;

            _listMonsterTouched = new List<Monster>();

            _listMonsterTouchedReturn = new List<Monster>();

            flyState = FlyState.bumShot;
        }

        private void FixedUpdate()
        {
            if (gameController.isStop) return;

            if (target != null && skillDamagedType == SkillDamagedType.Single)
            {
                targetPos = target.transform.position;
            }

            characterPos = character.transform.position;

            if ((Vector2.Distance(characterPos, transform.position) < 8f))
            {
                switch (targetType)
                {
                    case TargetType.Shot:
                        transform.Translate(moveSpeed * Time.fixedDeltaTime * direction.normalized);
                        break;
                        //case TargetType.Target:
                        //    transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
                        //    boomerang.up = direction;
                        //    if (Vector2.Distance(targetPos, transform.position) < 0.1f)
                        //    {
                        //        Destroy(gameObject);
                        //    }
                        //    break;
                }
            } else
            {
                switch (targetType)
                {
                    case TargetType.Shot:
                        boomerang.up = - direction.normalized;
                        flyState = FlyState.bumReturn;
                        transform.Translate(moveSpeed * Time.fixedDeltaTime * direction.normalized);
                        if (Vector2.Distance(characterPos, transform.position) > 9f)
                        {
                            Destroy(gameObject);
                        }
                        break;
                        //case TargetType.Target:
                        //    transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
                        //    boomerang.up = direction;
                        //    if (Vector2.Distance(targetPos, transform.position) < 0.1f)
                        //    {
                        //        Destroy(gameObject);
                        //    }
                        //    break;
                }
            }
            HandleTouch();
            if (Vector2.Distance(targetPos, transform.position) < 0.1f)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void HandleTouch()
        {
            if (!canBlock)
            {
                switch (skillDamagedType)
                {
                    case SkillDamagedType.Single:
                        if (Vector2.Distance(targetPos, transform.position) < sizeTouch)
                        {
                            TakeDamage();
                            Destroy(gameObject);
                        }
                        break;
                    case SkillDamagedType.AreaOfEffect:
                        foreach (var mons in gameController.listMonster.ToList())
                        {
                            if (flyState == FlyState.bumReturn)
                            {
                                if (!_listMonsterTouchedReturn.FirstOrDefault(mon => mon == mons) && CheckTouchMonsters(mons))
                                {
                                    _listMonsterTouchedReturn.Add(mons);
                                }
                            } else
                            {
                                if (!_listMonsterTouched.FirstOrDefault(mon => mon == mons) && CheckTouchMonsters(mons))
                                {
                                    _listMonsterTouched.Add(mons);
                                }
                            }
                        }
                        break;

                }
            }
            else
            {
                if (gameController.listMonster.ToList().Any(CheckTouchMonsters))
                {
                    Destroy(gameObject);
                    return;
                }
            }
            if (Vector2.Distance(origin.transform.position, transform.position) > 30)
            {
                Destroy(gameObject);
            }
        }
    }
}

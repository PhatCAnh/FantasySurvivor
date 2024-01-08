using ArbanFramework;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace FantasySurvivor
{

    public class RocketActive : SkillActive
    {
        [SerializeField] protected Transform skin;

        public SpawnPos spawnPos;

        public TargetType targetType;

        public bool canBlock;

        public float moveSpeed;

        protected Monster oldTarget;

        protected Vector3 targetPos;

        protected Vector3 direction;

        public float sizeExplosion;

        [SerializeField] private float _time;

        [SerializeField] private GameObject _explosionEffect;

         private float _cdTime ;

        protected Vector3 explosionTarget;

        public override void Init(float damage, Monster target, int level)
        {
            base.Init(damage, target, level);

            if (target == null) return;
            if (origin != null)
            {
                transform.position = spawnPos == SpawnPos.Character ? origin.transform.position : target.transform.position;
                this.direction = target.transform.position - transform.position;
                explosionTarget = target.transform.position;
                skin.up = direction;
            }
        }

        private void FixedUpdate()
        {
            if (gameController.isStop) return;

            if (gameController != null)
            {
                if (target != null && skillDamagedType == SkillDamagedType.Single)
                {
                    targetPos = target.transform.position;
                }
                transform.Translate(moveSpeed * Time.fixedDeltaTime * direction.normalized);

                HandleTouch();  

                _cdTime += Time.deltaTime;
            }
        }
        protected virtual void HandleTouch()
        {
            if (origin != null)
            {
                if (Vector2.Distance(origin.transform.position, transform.position) > 30 || _cdTime >= _time || Vector2.Distance(explosionTarget, origin.transform.position) < 5 )
                {
                    CheckAoeMons();
                    var explosion = Instantiate(_explosionEffect, transform.position, quaternion.identity);
                    explosion.transform.localScale = sizeExplosion * Vector3.one;
                    foreach (var mons in gameController.listMonster.ToList())
                    {
                        CheckTouchMonsters(mons);
                    }
                    Destroy(gameObject);

                    _cdTime = 0;
                }
            }
        }
        protected override void OnDrawGizmosSelected()
		{
			base.OnDrawGizmosSelected();
			Gizmos.DrawWireSphere(transform.position, sizeExplosion);
		}
    }
}
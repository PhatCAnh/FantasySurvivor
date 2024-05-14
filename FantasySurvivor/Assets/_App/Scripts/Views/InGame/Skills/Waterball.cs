using System.Linq;
using UnityEditor;
using UnityEngine;
namespace FantasySurvivor
{
    public class Waterball : SkillBulletActive
    {
        [SerializeField] protected GameObject explosionEffectPrefab;  // Prefab hiệu ứng nổ
        private float sizee = 5.0f;       // Bán kính nổ
        private float Damage = 6.0F;      // Sát thương gây ra bởi vụ nổ
        
        public override void Init(float damage, Monster target, int level)
        {
            base.Init(damage, target, level);

        }

        private void FixedUpdate()
        {
            if (gameController.isStop) return;

            if (target != null && skillDamagedType == SkillDamagedType.Single)
            {
                targetPos = target.transform.position;
            }

            switch (targetType)
            {
                case TargetType.Shot:
                    transform.Translate(moveSpeed * Time.fixedDeltaTime * direction.normalized);
                    break;
                case TargetType.Target:
                    transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
                    skin.up = direction;
                    if (gameController.CheckTouch(targetPos, transform.position, 0.1f))
                    {
                        //fix it
                        Destroy(gameObject);
                    }
                    break;
            }
            HandleTouch();
        }

        protected override void CreateExplo()
        {
            base.CreateExplo();
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

            foreach (var mob in gameController.GetAllMonsterInRange(transform.position, 3))
            {
                if (mob == target) continue;
                mob.TakeDamage(damage/2);
            }
        }

    }
}
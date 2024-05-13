using System.Linq;
using UnityEditor;
using UnityEngine;
namespace FantasySurvivor
{
    public class Waterball : SkillActive
    {
        [SerializeField] protected Transform skin;
        [SerializeField] protected Transform explosionEffectPrefab;  // Đổi thành GameObject để đảm bảo prefab có thể được instantiate
        [SerializeField] private float explosionRadius = 2.0f;         // Bán kính nổ
        [SerializeField] private float explosionDamage = 4.0f;        // Sát thương gây ra bởi vụ nổ

        public SpawnPos spawnPos;
        public TargetType targetType;
        public bool canBlock;
        public float moveSpeed;

        protected Monster oldTarget;
        protected Vector3 targetPos;
        protected Vector3 direction;

        public override void Init(float damage, Monster target, int level)
        {
            base.Init(damage, target, level);
            if (target == null) return;

            transform.position = spawnPos == SpawnPos.Character ? origin.transform.position : target.transform.position;
            this.direction = target.transform.position - transform.position;
            skin.up = direction;
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
                        Explode();
                        Destroy(gameObject);
                    }
                    break;
            }
            HandleTouch();
        }

        protected virtual void HandleTouch()
        {
            if (!canBlock)
            {
                if (gameController.CheckTouch(targetPos, transform.position, sizeTouch))
                {
                    TakeDamage();
                    Explode();
                    Destroy(gameObject);
                }
            }
            else
            {
                if (gameController.listMonster.ToList().Any(CheckTouchMonsters))
                {
                    Explode();
                    Destroy(gameObject);
                }
            }

            if (!gameController.CheckTouch(targetPos, transform.position, 30))
            {
                Destroy(gameObject);
            }
        }

        protected void Explode()
        {
            // Lấy tất cả các quái vật trong phạm vi nổ
            var monstersInRange = gameController.listMonster
                .Where(m => Vector3.Distance(transform.position, m.transform.position) <= explosionRadius)
                .ToList();

            foreach (var monster in monstersInRange)
            {
                // Gây sát thương cho mỗi quái vật trong phạm vi
                monster.TakeDamage(explosionDamage);
            }

            // Thêm hiệu ứng nổ tại vị trí đạn nổ (nếu có)
            //if (explosionEffectPrefab != null)
            //{

            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            //}
        }
    }
}

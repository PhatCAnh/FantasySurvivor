using System;
using System.Linq;
using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
namespace FantasySurvivor
{
    public class TwinBorn : SkillBulletActive
    {
        [SerializeField] private SkillActive _twinInstantiate;

        private bool check = false;

        public void InitTwin(Vector2 direction, float moveSpeed, float damage, Monster oldTarget, int level)
        {
            this.level = level;
            this.origin = gameController.character;
            this.direction = direction;
            this.moveSpeed = moveSpeed;
            skin.up = -direction;
            this.damage = damage;
            this.oldTarget = oldTarget;
            check = false;
        }

        private void SpawnTwinBorn()
        {
            var bullet = Instantiate(_twinInstantiate, transform.position, new Quaternion(0, 0, 90, 0));
            bullet.GetComponent<TwinBorn>().InitTwin(new Vector2(-direction.y, direction.x), moveSpeed, damage / 2, target, level);
            var bullet2 = Instantiate(_twinInstantiate, transform.position, new Quaternion(0, 0, -90, 0));
            bullet2.GetComponent<TwinBorn>().InitTwin(new Vector2(direction.y, -direction.x), moveSpeed, damage / 2, target, level);
            Singleton<PoolController>.instance.ReturnObject(this.type, gameObject);
            check = true;
        }

        protected override void HandleTouch()
        {
            foreach (var mob in gameController.listMonster.ToList())
            {
                if (CheckTouchMonsters(mob))
                    return;
            }

            if (!gameController.CheckTouch(origin.transform.position, transform.position, 30))
            {
                Destroy(gameObject);
            }
        }
        protected override bool CheckTouchMonsters(Monster monster)
        {
            if (monster.Equals(oldTarget)) return false;
            sizeTouch = size + monster.size;

            if (gameController.CheckTouch(monster.transform.position, transform.position, sizeTouch))
            {
                TakeDamage(monster);
                if (monster.isDead && this.level == 6)
                {
                    SpawnTwinBorn();
                }
                Destroy(gameObject);

                return true;
            }
            return false;
        }
    }
}
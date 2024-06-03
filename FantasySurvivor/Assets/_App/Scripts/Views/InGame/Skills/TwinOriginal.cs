using _App.Scripts.Controllers;
using ArbanFramework;
using FantasySurvivor;
using Unity.Mathematics;
using UnityEngine;
namespace _App.Scripts.Views.InGame.Skills
{
    public class TwinOriginal : SkillBulletActive
    {
        [SerializeField] private SkillActive _twinInstantiate;

        private bool check = false;

        public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
        {
            base.Init(data, target, level, type);
        }
        protected override bool CheckTouchMonsters(Monster monster)
        {
            sizeTouch = size + monster.size;
            if (!gameController.CheckTouch(monster.transform.position, transform.position, sizeTouch)) return false;

            TakeDamage(monster);
            if (monster.isDead && this.level == 6)
            {
                SpawnTwinBorn();
                check = false;
            }
            return true;
        }

        private void SpawnTwinBorn()
        {
            if (check) return;
            var bullet = Instantiate(_twinInstantiate, transform.position, new Quaternion(0, 0, 90, 0));
            bullet.GetComponent<TwinBorn>().InitTwin(new Vector2(-direction.y, direction.x), moveSpeed, damage / 2, target, level);
            var bullet2 = Instantiate(_twinInstantiate, transform.position, new Quaternion(0, 0, -90, 0));
            bullet2.GetComponent<TwinBorn>().InitTwin(new Vector2(direction.y, -direction.x), moveSpeed, damage / 2, target, level);
            Singleton<PoolController>.instance.ReturnObject(this.type, gameObject);
            check = true;
        }
    }
}
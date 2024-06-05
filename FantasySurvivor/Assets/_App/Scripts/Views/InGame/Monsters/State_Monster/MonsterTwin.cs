using _App.Scripts.Controllers;
using ArbanFramework;
using UnityEngine;
namespace FantasySurvivor
{
    public class MonsterTwin : RangedMonster
    {
        private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

        public override void Die(bool selfDie = false)
        {
            base.Die(selfDie);
            spawnMon();
        }

        private void spawnMon()
        {
            var mob = gameController.SpawnMonster("Goblin", 10, stat.attackDamage.BaseValue);

            Vector2 spawnPosition = new Vector2(transform.position.x,
                                                 transform.position.y);

            mob.transform.position = spawnPosition;

        }
    }
}

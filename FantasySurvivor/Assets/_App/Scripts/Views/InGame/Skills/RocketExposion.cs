using ArbanFramework.MVC;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace FantasySurvivor
{
    public class RocketExposion : SkillBulletActive
    {
        public float sizeExplosion;
        private void Update()
        {
            TouchUnit();
        }

        private void FixedUpdate()
        {
            if (gameController.isStop) return;
            CheckAoeMons();
        }

        protected override void TouchUnit(Monster mons = null)
        {
            foreach (var unit in gameController.listMonster.ToList())
            {
                if (Vector2.Distance(unit.transform.position, transform.position) < sizeExplosion + unit.size)
                {
                    base.TouchUnit(unit);
                }
            }
            Destroy(gameObject);
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.DrawWireSphere(transform.position, sizeExplosion);
        }
    }
}
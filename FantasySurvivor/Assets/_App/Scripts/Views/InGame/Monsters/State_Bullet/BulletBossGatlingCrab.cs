using System;
using System.Collections.Generic;
using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;
namespace FantasySurvivor
{
    public class BulletBossGatlingCrab : View<GameApp>
    {
        public SpriteRenderer skin;

        [SerializeField] protected float size;
        [SerializeField] protected ItemPrefab type;
        [SerializeField] private float _speedBullet = 15f;


        private Monster _origin;
        private Vector2 _directionToTarget;

        private Character _character => gameController.character;
        protected GameController gameController => Singleton<GameController>.instance;

        public void Init(Monster origin)
        {
            /*_origin = origin;
            _directionToTarget = (origin.target.transform.position - origin.transform.position).normalized;*/
            _origin = origin;
            _directionToTarget = (transform.position - origin.target.transform.position).normalized;
            /*float angle = Mathf.Atan2(_directionToTarget.y, _directionToTarget.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);*/
        }

        // Thêm phương thức mới để thiết lập hướng di chuyển
        public void SetDirection(Vector2 direction)
        {
            _directionToTarget = direction.normalized;
        }

        protected void FixedUpdate()
        {
            if (gameController.isStop) return;

            // Di chuyển viên đạn theo hướng được thiết lập
            Vector3 directionToTarget = _directionToTarget.normalized;
            var position = transform.position;
            position = Vector2.MoveTowards(position, position + directionToTarget, _speedBullet * Time.deltaTime);

            transform.position = position;

            var characterPosition = _character.transform.position;
            var x = position.x - characterPosition.x;
            var y = position.y - characterPosition.y;
            float distance = x * x + y * y;
            var sizeTotal = size + _character.sizeBase;

            if (distance <= sizeTotal * sizeTotal) Touch();
            else if (distance > 900) Singleton<PoolController>.instance.ReturnObject(type, gameObject);
        }

        
        protected void Touch()
        {
            _character.TakeDamage(_origin.model.attackDamage);
            Singleton<PoolController>.instance.ReturnObject(type, gameObject);
        }

        protected void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, size);

        }
    }
}
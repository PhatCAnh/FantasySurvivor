using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    public SpriteRenderer skin;

    public Rigidbody2D rigidbody2d;

    private TowerView _origin;

    public void Init(TowerView origin, float speed)
    {
        _origin = origin;
        var spawnPos = _origin.firePoint;
        transform.SetPositionAndRotation(spawnPos.position, spawnPos.rotation);
        rigidbody2d.velocity = spawnPos.up * speed;
        Destroy(gameObject, 3f);
    }

    private void TouchUnit()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out Monster monster))
        {
            var damage = _origin.model.attackDamage;
            monster.TakeDamage(damage);
            Singleton<PoolDamagePoint>.instance
                .GetObjectFromPool(other.transform.position, damage, false);
            TouchUnit();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    public SpriteRenderer skin;

    public Rigidbody2D rigidbody2d;

    public void Init(Transform spawnPos, float speed)
    {
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
            monster.TakeDamage(10);
            TouchUnit();
        }
    }
}

using ArbanFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMonster : Monster
{
    private Collider2D monsCollider;
    private GameController gameController => Singleton<GameController>.instance;
    public override void Die(bool selfDie = false)
    {
        isDead = true;
        monsCollider = GetComponent<Collider2D>();
        monsCollider.isTrigger = true;
        animator.SetBool("Dead", isDead);
        gameController.MonsterDestroy(this);
    }

}

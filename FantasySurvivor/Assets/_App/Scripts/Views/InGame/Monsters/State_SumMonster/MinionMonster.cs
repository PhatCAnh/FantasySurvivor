using ArbanFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMonster : Monster
{
    private GameController gameController => Singleton<GameController>.instance;
    public override void Die(bool selfDie = false)
    {
        isDead = true;
        animator.SetBool("Dead", isDead);
        gameController.MonsterDestroy(this);
    }

}

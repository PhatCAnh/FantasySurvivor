using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSummoner : Monster
{


    private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

    protected override void HandlePhysicUpdate()
    {




        if (moveDirection.magnitude > 25)
        {
            transform.position = gameController.RandomPositionSpawnMonster(20);
        }
        else
        {
            MoveState();
        }

        SetAnimation(idleDirection);
    }


}

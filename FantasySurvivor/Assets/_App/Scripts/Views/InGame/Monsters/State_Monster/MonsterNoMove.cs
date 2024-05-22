using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasySurvivor
{
    public class MonsterNoMove : RangedMonster
    {

        public override void MoveState()
        {
            isNoMove = true;
            base.MoveState();
        }
        public override void AttackState()
        {
            isNoMove = true;
            base.AttackState();
        }

    }

}

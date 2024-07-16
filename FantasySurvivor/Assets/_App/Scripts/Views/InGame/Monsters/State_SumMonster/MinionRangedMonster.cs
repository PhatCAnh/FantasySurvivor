using ArbanFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace FantasySurvivor
{
    public class MinionRangedMonster : RangedMonster
    {
        private GameController gameController => Singleton<GameController>.instance;


        public override void Die(bool selfDie = true)
        {
            base.Die(true);
        }
    }
}

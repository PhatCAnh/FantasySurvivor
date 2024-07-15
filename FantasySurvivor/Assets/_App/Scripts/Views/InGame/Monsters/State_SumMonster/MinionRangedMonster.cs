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


        public override void TakeDamage(float damage, TextPopupType type, bool isCritical = false, Action callBackDamaged = null, Action callBackKilled = null)
        {
            base.TakeDamage(damage, type, isCritical, callBackDamaged, callBackKilled);
            this.Die(true);
        }

        public override void Die(bool selfDie = true)
        {
            base.Die(true);
        }
    }
}

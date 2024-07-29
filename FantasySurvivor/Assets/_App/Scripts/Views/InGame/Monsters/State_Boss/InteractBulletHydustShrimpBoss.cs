using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasySurvivor
{
    public class InteractBulletHydustShrimpBoss : BulletBossHydustShrimp
    {
        private void OnMouseDown()
        {
            if (gameController.isStop) return;

            Touch();
        }
    }
}

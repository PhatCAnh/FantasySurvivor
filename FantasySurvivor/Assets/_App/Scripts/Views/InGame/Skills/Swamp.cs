using System;
using System.Collections.Generic;
using System.Linq;
using ArbanFramework;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;

namespace FantasySurvivor
{
    public class Swamp : SkillActive
    {

        [SerializeField] private SpawnPos spawnPos;
        private static List<Vector3> usedPositions = new List<Vector3>();
        public override void Init(float damage, Monster target, int level)
        {
        
            base.Init(damage, target, level);

            if (target == null) return;
            //if (level == 1)
            //{
            //    transform.position = target.transform.position;
            //}
            //else
            //{
            //    Vector3 randomOffset = new Vector3(
            //   UnityEngine.Random.Range(-5.0f, 5.0f),
            //   UnityEngine.Random.Range(-10.0f, 10.0f),
            //   0);
            //    transform.position = spawnPos == SpawnPos.Monster ? origin.transform.position + randomOffset : base.target.transform.position + randomOffset;
            //}
            Vector3 newPosition;

            if (level == 1)
            {
                newPosition = target.transform.position;
            }
            else
            {
                Vector3 randomOffset;
                do
                {
                    randomOffset = new Vector3(
                        UnityEngine.Random.Range(-5.0f, 5.0f),
                        UnityEngine.Random.Range(-10.0f, 10.0f),
                        0);

                    newPosition = spawnPos == SpawnPos.Monster ? origin.transform.position + randomOffset : base.target.transform.position + randomOffset;
                } while (usedPositions.Contains(newPosition));
            }

            transform.position = newPosition;
            usedPositions.Add(newPosition);
        

    }
    

        private void FixedUpdate()
        {
            if (gameController.isStop) return;

            foreach (var item in    gameController.listMonster.ToList())
            {
                if (gameController.CheckTouch(transform.position, item.transform.position, size + item.size))
                {

                    item.TakeDamage(damage); 
                }
            }
        }
    }
}


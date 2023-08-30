using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace FantasySurvivor
{
    public class CharacterModel : UnitModel
    {
        public CharacterModel(EventTypeBase eventType) : base(dataChangedEvent)
        {
        }

        public CharacterModel() : base(dataChangedEvent)
        {
        }

        public CharacterModel(float moveSpeed, int maxHp) : base(dataChangedEvent)
        {
            this.moveSpeed = moveSpeed;
            this.currentHealthPoint = maxHp;
            this.maxHealthPoint = maxHp;
        }
    }
}
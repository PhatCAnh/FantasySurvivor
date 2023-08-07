using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace MR
{
    public class CharacterModel : Model<GameApp>
    {
        public static EventTypeBase dataChangedEvent = new EventTypeBase(nameof(CharacterModel) + ".dataChanged");

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

        private float _moveSpeed;

        private int _currentHealthPoint;

        private int _maxHealthPoint;
        
        public float moveSpeed
        {
            get => _moveSpeed;
            set {
                if(moveSpeed.Equals(value)) return;
                _moveSpeed = value;
                RaiseDataChanged(nameof(moveSpeed));
            }
        }

        public int currentHealthPoint
        {
            get => _currentHealthPoint;
            set {
                if(currentHealthPoint == value) return;
                _currentHealthPoint = value;
                RaiseDataChanged(nameof(currentHealthPoint));
            }
        }
        
        public int maxHealthPoint
        {
            get => _maxHealthPoint;
            set {
                if(maxHealthPoint == value) return;
                _maxHealthPoint = value;
                RaiseDataChanged(nameof(maxHealthPoint));
            }
        }
    }
}
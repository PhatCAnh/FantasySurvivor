using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using UnityEngine;

namespace MR
{
    public class CharacterModel : Model<GameApp>
    {
        public static EventTypeBase dataChangedEvent = new EventTypeBase(nameof(SettingModel) + ".dataChanged");

        public CharacterModel(EventTypeBase eventType) : base(dataChangedEvent)
        {
        }

        public CharacterModel() : base(dataChangedEvent)
        {
        }

        public CharacterModel(float moveSpeed, float amountOil) : base(dataChangedEvent)
        {
            this.moveSpeed = moveSpeed;
            this.amountOil = amountOil;
            this.currentOil = amountOil;
        }

        private float _moveSpeed;

        private float _amountOil;

        private float _currentOil;

        public float moveSpeed
        {
            get => _moveSpeed;
            set
            {
                if (moveSpeed != value)
                {
                    _moveSpeed = value;
                    RaiseDataChanged(nameof(moveSpeed));
                }
            }
        }

        public float amountOil
        {
            get { return _amountOil; }
            set
            {
                if (amountOil != value)
                {
                    _amountOil = value;
                    RaiseDataChanged(nameof(amountOil));
                }
            }
        }

        public float currentOil
        {
            get { return _currentOil; }
            set
            {
                if (currentOil != value)
                {
                    _currentOil = Mathf.Clamp(value, 0, _amountOil);
                    RaiseDataChanged(nameof(currentOil));
                }
            }
        }
    }
}
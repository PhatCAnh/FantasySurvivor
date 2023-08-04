using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
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

        public CharacterModel(float moveSpeed) : base(dataChangedEvent)
        {
            this.moveSpeed = moveSpeed;
        }

        private float _moveSpeed;

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
    }
}
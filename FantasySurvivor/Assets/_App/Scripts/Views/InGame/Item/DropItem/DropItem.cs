using System;
using System.Collections;
using System.Collections.Generic;

using ArbanFramework;
using ArbanFramework.StateMachine;
using FantasySurvivor;
using UnityEngine;


namespace FantasySurvivor
{
    public class DropItem : ObjectRPG
    {
        [SerializeField] private Sprite _spriteExp;
        [SerializeField] private Sprite _spriteMagnet;
        [SerializeField] private Sprite _spriteBomb;
        [SerializeField] private Sprite _spriteFood;
        
        public SpriteRenderer skin;

        public DropItemType type;

        public int value;
        //item type
        public bool isSpawn => _stateMachine.currentState == _spawnSm;
        public bool isIdle => _stateMachine.currentState == _idleSm;
        public bool isCollect => _stateMachine.currentState == _collectSm;
        public bool isComplete => _stateMachine.currentState == _completeSm;

        private StateMachine _stateMachine;
        private DropItemSpawn _spawnSm;
        private DropItemIdle _idleSm;
        private DropItemCollect _collectSm;
        private DropItemComplete _completeSm;
        
        public Character character { get; private set; }

        private GameController gameController => Singleton<GameController>.instance;

        protected override void OnViewInit() {
            base.OnViewInit();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            character = gameController.character;
            if( _stateMachine == null ) {
                _stateMachine = new StateMachine();
                _spawnSm = new DropItemSpawn(this, _stateMachine);
                _idleSm = new DropItemIdle(this, _stateMachine);
                _collectSm = new DropItemCollect(this, _stateMachine);
                _completeSm = new DropItemComplete(this, _stateMachine);
                _stateMachine.Init(_spawnSm);
            } else {
                Spawn();
            }
        }
        public void Init(int valueInit, DropItemType itemType)
        {
            type = itemType;
            switch (itemType)
            {
                case DropItemType.Exp:
                    value = valueInit;
                    skin.sprite = _spriteExp;
                    break;
                case DropItemType.Food:
                    value = valueInit;
                    skin.sprite = _spriteFood;
                    break;
                case DropItemType.Magnet:
                    skin.sprite = _spriteMagnet;
                    break;
                case DropItemType.Bomb:
                    value = valueInit;
                    skin.sprite = _spriteBomb;
                    break;
            }
        }

        public void Update()
        {
            if(gameController.isStop) return;
            _stateMachine.currentState.LogicUpdate(Time.deltaTime);
        }

        public void FixedUpdate()
        {
            if(gameController.isStop) return;
            _stateMachine.currentState.PhysicUpdate(Time.fixedDeltaTime);
        }


        #region StateMachine Method

        public void Spawn()
        {
            if (isSpawn)
                return;

            _stateMachine.ChangeState(_spawnSm);
        }
        
        public void Idle()
        {
            if (isIdle)
                return;

            _stateMachine.ChangeState(_idleSm);
        }

        public void Collect()
        {
            if (isCollect)
                return;

            _stateMachine.ChangeState(_collectSm);
        }

        public void Complete()
        {
            if (isComplete)
                return;

            _stateMachine.ChangeState(_completeSm);
        }

        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using ArbanFramework.StateMachine;
using MR.CharacterState.Tower;
using Sirenix.OdinInspector;
using UnityEngine;

public class TowerView : View<GameApp>
{
    public SpriteRenderer skinBase;

    public Animator animator;

    public Transform weapon;

    public Transform firePoint;

    public BulletView arrow;

    public float sizeBase;

    public float attackSpeed = 1;
    
    public Monster target { set; get; }

    public GameController gameController => Singleton<GameController>.instance;

    public bool isIdle => _stateMachine.currentState == _idleState;
    public bool isAttack => _stateMachine.currentState == _attackState;

    private StateMachine _stateMachine;
    private TowerIdle _idleState;
    private TowerAttack _attackState;
    
    protected override void OnViewInit()
    {
        base.OnViewInit();
        if(_stateMachine == null)
        {
            _stateMachine = new StateMachine();
            _idleState = new TowerIdle(this, _stateMachine);
            _attackState = new TowerAttack(this, _stateMachine);
            _stateMachine.Init(_idleState);
        }
        else
        {
            IdleState();
        }
        animator.SetFloat("AttackSpeed", attackSpeed);
    }
    
    protected virtual void Update()
    {
        var time = Time.deltaTime;
        _stateMachine.currentState.LogicUpdate(time);
    }
	
    protected virtual void FixedUpdate()
    {
        if(gameController.isStop) return;
        _stateMachine.currentState.PhysicUpdate(Time.fixedTime);
    }

    public void Attack()
    {
        var arrowIns = Instantiate(arrow);
        arrowIns.Init(firePoint, 15f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, sizeBase);
    }
    
    public void IdleState()
    {
        if(isIdle) return;
        _stateMachine.ChangeState(_idleState);
    }

    public void AttackState()
    {
        if(isAttack) return;
        _stateMachine.ChangeState(_attackState);
    }
    
}

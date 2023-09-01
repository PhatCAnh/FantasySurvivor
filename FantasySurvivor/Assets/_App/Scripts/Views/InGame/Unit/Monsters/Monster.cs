using ArbanFramework;
using ArbanFramework.MVC;
using ArbanFramework.StateMachine;
using FantasySurvivor;
using UnityEngine;

public class Monster : View<GameApp>
{
	#region Fields
	
	public Rigidbody2D myRigid;

	public Animator animator;
	
	private Vector2 _direction = Vector2.zero;
	
	public MonsterModel model { get; protected set; }
	
	public MonsterStat stat { get; protected set; }
	
	public Vector2 idleDirection { get; private set; } = Vector2.down;

	public float speedMul { get; set; } = 1;
	
	public Vector2 moveDirection
	{
		get => _direction;
		set {
			if(value != Vector2.zero)
			{
				idleDirection = value;
			}
			_direction = value;
		}
	}
	public bool isIdle => _stateMachine.currentState == _idleState;

	public bool isMove => _stateMachine.currentState == _moveState;
	
	public bool isAlive => model.currentHealthPoint > 0;
	
	private StateMachine _stateMachine;
	private GameController gameController => Singleton<GameController>.instance;
	
	private MonsterIdle _idleState;
	
	private MonsterMove _moveState;	
	
	public float size;
	
	private TowerView target => gameController.tower;

	private float _sizeDirection;
	
	#endregion

	#region Base Methods
	
	public virtual void Init(MonsterModel monsterModel)
	{
		this.model = monsterModel;
	}
	
	protected override void OnViewInit()
	{
		base.OnViewInit();
		if(_stateMachine == null)
		{
			_stateMachine = new StateMachine();
			_idleState = new MonsterIdle(this, _stateMachine);
			_moveState =  new MonsterMove(this, _stateMachine);
			_stateMachine.Init(_idleState);
		}
		else
		{
			IdleState();
		}
		_sizeDirection = 0.1f + target.sizeBase + size;
	}

	private void Update()
	{
		var time = Time.deltaTime;
		_stateMachine.currentState.LogicUpdate(time);
		HandlePhysicUpdate();
	}
	
	private void FixedUpdate()
	{
		if(gameController.isStop) return;
		_stateMachine.currentState.PhysicUpdate(Time.fixedTime);
	}

	#endregion

	private void HandlePhysicUpdate()
	{
		moveDirection = target.transform.position - transform.position;
		
		if(moveDirection.magnitude < _sizeDirection)
			IdleState();
		else
		{
			MoveState();
		}
		SetAnimation(idleDirection);
	}
	
	protected virtual void SetAnimation(Vector2 directionMove)
	{
		animator.SetFloat("SpeedMul", speedMul);
		animator.SetFloat("Horizontal", directionMove.x);
		animator.SetFloat("Vertical", directionMove.y);
	}

	public void AttackDamage()
	{
		target.TakeDamage(model.attackDamage);
	}
	
	public void TakeDamage(int damage)
	{
		model.currentHealthPoint -= damage;
		if(!isAlive) Die();
	}

	private void Die()
	{
		gameController.MonsterDie(this);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, size);
	}
	
	#region State Machine Method

	public virtual void IdleState()
	{
		if(isIdle) return;
		_stateMachine.ChangeState(_idleState);
	}

	public virtual void MoveState()
	{
		if(isMove) return;
		_stateMachine.ChangeState(_moveState);
	}

	#endregion
}
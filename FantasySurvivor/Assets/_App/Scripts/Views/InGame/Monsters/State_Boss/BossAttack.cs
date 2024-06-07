using ArbanFramework.StateMachine;
using UnityEngine;
namespace FantasySurvivor
{
    public class BossAttack : State<Monster>
    {

        public BossAttack(Monster agent, StateMachine stateMachine) : base(agent, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            agent.animator.SetFloat("Speed", 0f);
            agent.firePoint.up = agent.target.transform.position - agent.firePoint.position;
            agent.Attack();
        }
    }
}
using ArbanFramework.StateMachine;
using DG.Tweening;
using UnityEngine;

namespace MR.CharacterState
{
    public class CharacterDash : State<Character>
    {
        public CharacterDash(Character agent, StateMachine stateMachine) : base(agent, stateMachine)
        {
        }
        
        public override void Enter()
        {
            var directionUnit = agent.moveDirection.normalized;
            Dash(directionUnit, Time.fixedDeltaTime);
        }

        private void Dash(Vector2 dir, float deltaTime)
        {
            var movement = GameConst.MOVE_SPEED_ANIMATION_RATIO * 1f * dir;
            var newPosition = agent.myRigid.position + movement;
            agent.myRigid.DOMove(newPosition, 0.125f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    agent.IdleState();
                });
        }

        public override void Exit()
        {
        }
    }
}
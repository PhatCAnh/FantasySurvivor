using ArbanFramework.StateMachine;

using UnityEngine;
namespace MR.State_DropItem {
    public class DropItemIdle : State<DropItem> {
        public DropItemIdle(DropItem agent, StateMachine stateMachine) : base( agent, stateMachine )
        {
        }

        public override void LogicUpdate(float deltaTime) {
            base.LogicUpdate(deltaTime);

            if (agent.character == null)
                return;

            var distanceToTarget = Vector2.Distance(agent.transform.position, agent.character.transform.position);
            if (distanceToTarget > 1f)
                return;

            agent.Collect();
        }
    }
}
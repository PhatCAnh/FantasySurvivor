using ArbanFramework.StateMachine;
using DG.Tweening;
using UnityEngine;
namespace FantasySurvivor {
    public class DropItemIdle : State<DropItem> {
        public DropItemIdle(DropItem agent, StateMachine stateMachine) : base( agent, stateMachine )
        {
        }

        public override void LogicUpdate(float deltaTime) {
            base.LogicUpdate(deltaTime);

            if (agent.character == null)
                return;

            var distanceToTarget = Vector2.Distance(agent.transform.position, agent.character.transform.position);
            if (distanceToTarget > agent.character.model.ItemAttractionRange)
                return;
            agent.Collect();
        }
    }
}
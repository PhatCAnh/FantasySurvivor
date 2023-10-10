using FantasySurvivor;
using ArbanFramework.StateMachine;

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

            if (GameLogic.CheckDistance(agent.transform.position, agent.character.transform.position, 0.1f))
                return;

            agent.Collect();
        }
    }
}
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
            if(agent.gameController.CheckTouchCharacter(agent.transform.position, agent.character.model.itemAttractionRange))
            {
                agent.Collect();
            }
        }
    }
}
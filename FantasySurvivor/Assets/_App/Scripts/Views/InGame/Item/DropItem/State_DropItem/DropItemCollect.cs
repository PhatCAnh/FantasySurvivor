using ArbanFramework;
using ArbanFramework.StateMachine;

using UnityEngine;
namespace FantasySurvivor {
    public class DropItemCollect : State<DropItem> {
        public DropItemCollect(DropItem agent, StateMachine stateMachine) : base( agent, stateMachine )
        {
        }

        public override void LogicUpdate(float deltaTime) {
            base.LogicUpdate(deltaTime);
            
            if (agent.gameController.CheckTouchCharacter(agent.transform.position, agent.character.sizeBase))
            {
                Singleton<GameController>.instance.Collected(agent);
                agent.Complete();
                return;
            }

            agent.transform.position = Vector3.MoveTowards(agent.transform.position, agent.character.transform.position, 10 * Time.deltaTime);
        }
    }
}
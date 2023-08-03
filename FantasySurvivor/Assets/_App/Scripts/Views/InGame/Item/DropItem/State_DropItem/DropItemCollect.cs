using ArbanFramework;
using ArbanFramework.StateMachine;

using UnityEngine;
namespace MR.State_DropItem {
    public class DropItemCollect : State<DropItem> {
        public DropItemCollect(DropItem agent, StateMachine stateMachine) : base( agent, stateMachine )
        {
        }

        public override void LogicUpdate(float deltaTime) {
            base.LogicUpdate(deltaTime);
            
            var distanceToTarget = Vector2.Distance(agent.transform.position, agent.character.transform.position);
            if (distanceToTarget < 0.15f)
            {
                //Singleton<GameController>.instance.CollectedItem(agent.type);
                agent.Complete();
                return;
            }

            agent.transform.position = Vector3.MoveTowards(agent.transform.position, agent.character.transform.position, 10 * Time.deltaTime);
        }
    }
}
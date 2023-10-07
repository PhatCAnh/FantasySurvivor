using ArbanFramework.StateMachine;

using UnityEngine;
namespace FantasySurvivor {
    public class DropItemComplete : State<DropItem> {
        public DropItemComplete(DropItem agent, StateMachine stateMachine) : base( agent, stateMachine )
        {
        }

        public override void Enter() {
            base.Enter();
            
            GameObject.Destroy(agent.gameObject);
        }
    }
}
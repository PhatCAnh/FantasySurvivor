using ArbanFramework;
using ArbanFramework.StateMachine;
using StateMachine = ArbanFramework.StateMachine.StateMachine;
namespace FantasySurvivor {
    public class DropItemComplete : State<DropItem> {
        public DropItemComplete(DropItem agent, StateMachine stateMachine) : base( agent, stateMachine )
        {
        }

        public override void Enter() {
            base.Enter();
            
            Singleton<PoolDropItem>.instance.RemoveObjectToPool(agent);
        }
    }
}
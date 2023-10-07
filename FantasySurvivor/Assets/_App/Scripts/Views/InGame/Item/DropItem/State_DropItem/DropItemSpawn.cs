using ArbanFramework.StateMachine;
namespace FantasySurvivor {
    public class DropItemSpawn : State<DropItem> {
        public DropItemSpawn(DropItem agent, StateMachine stateMachine) : base( agent, stateMachine )
        {
        }

        public override void Enter() {
            base.Enter();
            agent.Idle();
        }
    }
}
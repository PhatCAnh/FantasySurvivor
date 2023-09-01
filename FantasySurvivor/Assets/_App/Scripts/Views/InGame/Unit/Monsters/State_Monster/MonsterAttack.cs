using ArbanFramework.StateMachine;
namespace FantasySurvivor
{
	public class MonsterAttack : State<Monster>
	{

		public MonsterAttack(Monster agent, StateMachine stateMachine) : base(agent, stateMachine)
		{
		}
		
		
	}
}
using ArbanFramework.StateMachine;

namespace FantasySurvivor
{
    public class CharacterIdle : State<Character>
    {
        public CharacterIdle(Character agent, StateMachine stateMachine) : base(agent, stateMachine)
        {
        }
    }
}
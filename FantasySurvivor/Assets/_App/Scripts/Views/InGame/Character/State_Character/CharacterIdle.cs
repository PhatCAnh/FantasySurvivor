using ArbanFramework.StateMachine;

namespace MR.CharacterState
{
    public class CharacterIdle : State<Character>
    {
        public CharacterIdle(Character agent, StateMachine stateMachine) : base(agent, stateMachine)
        {
        }
    }
}
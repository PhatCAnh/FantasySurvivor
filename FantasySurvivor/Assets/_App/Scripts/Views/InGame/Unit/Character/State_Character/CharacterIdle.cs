using ArbanFramework.StateMachine;

namespace MR.CharacterState
{
    public class CharacterIdle : UnitIdle
    {
        public CharacterIdle(Character agent, StateMachine stateMachine) : base(agent, stateMachine)
        {
        }
    }
}
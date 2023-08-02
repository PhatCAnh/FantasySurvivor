using ArbanFramework;
namespace MR {
    public class CharacterStat
    {
        public StatFloat moveSpeed = new();

        public CharacterStat(float moveSpeed)
        {
            this.moveSpeed.BaseValue = moveSpeed;
        }
    }
}
using _App.Scripts.Controllers;
using ArbanFramework;
namespace FantasySurvivor
{
    public class Shark : SkillEffect
    {
        private SkillActive _skillActive;

        protected override void OnViewInit()
        {
            base.OnViewInit();
          
            _skillActive = GetComponentInParent<SkillActive>();
        }
        protected override void Attack()
        {
            _skillActive.TakeDamage();

            foreach (var mons in _skillActive.gameController.listMonster)
            {
                if (_skillActive.data.valueSpecial2 != 0)
                {
                    _skillActive.origin.AddHealth(_skillActive.data.valueSpecial2 * _skillActive.data.value);
                }
            }
        }
        protected override void DoneEffect()
        {
            Singleton<PoolController>.instance.ReturnObject(_skillActive.type, _skillActive.gameObject);
        }
    }
}
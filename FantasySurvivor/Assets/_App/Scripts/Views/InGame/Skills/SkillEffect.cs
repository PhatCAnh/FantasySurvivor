using _App.Scripts.Controllers;
using ArbanFramework;
namespace FantasySurvivor
{
    public class SkillEffect : Effect
    {
        private SkillActive _skillActive;

        protected override void OnViewInit()
        {
            base.OnViewInit();
            _skillActive = GetComponentInParent<SkillActive>();
        }

        protected virtual void Attack()
        {
            _skillActive.TakeDamage();
        }
        protected override void DoneEffect()
        {
            Singleton<PoolController>.instance.ReturnObject(_skillActive.type, _skillActive.gameObject);
        }
    }
}
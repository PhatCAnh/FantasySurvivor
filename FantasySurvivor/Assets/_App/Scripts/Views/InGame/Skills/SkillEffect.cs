using _App.Scripts.Controllers;
using ArbanFramework;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
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

        protected virtual void ThunderStrike()
        {
            AudioManager.Instance.PlaySFX("Lightning strikes");
        }
        protected virtual void Shark()
        {
            AudioManager.Instance.PlaySFX("Hydro Shark");
        }
        protected virtual void Earthpunch() 
        {
            AudioManager.Instance.PlaySFX("Earthpunch");
        }
        protected virtual void FireBall()
        {
            AudioManager.Instance.PlaySFX("FireBall");
        }
    }
    
}
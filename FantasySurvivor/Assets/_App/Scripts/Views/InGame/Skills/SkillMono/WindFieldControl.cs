using FantasySurvivor;
using _App.Scripts.Controllers;
using ArbanFramework;
using Vector3 = UnityEngine.Vector3;
namespace _App.Scripts.Views.InGame.Skills.SkillMono
{
    public class WindFieldControl : ProactiveSkill
    {
        public override void Active()
        {
            var skill = Singleton<PoolController>.instance.GetObject(skillPrefab, Vector3.zero).GetComponent<SkillActive>();
            skill.Init(levelData[level], null, level, skillPrefab);
            var script = skill.GetComponent<WindField>();
        }

    }
}
using ArbanFramework.MVC;
namespace _App.Scripts.Views.InGame.Skills
{
	public class Skill : View<GameApp>
	{
		private SkillAttack _skill;
		
		protected override void OnViewInit()
		{
			base.OnViewInit();
			_skill = GetComponentInParent<SkillAttack>();
		}

		private void Attack()
		{
			_skill.Attack();
		}

		private void DoneAnim()
		{
			Destroy(gameObject);
		}
	}
}
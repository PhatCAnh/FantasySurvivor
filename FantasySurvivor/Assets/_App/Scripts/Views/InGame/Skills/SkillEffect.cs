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

		private void Attack()
		{
			_skillActive.TakeDamage();
		}

		protected override void DoneEffect()
		{
			Destroy(_skillActive.gameObject);			
		}
	}
}
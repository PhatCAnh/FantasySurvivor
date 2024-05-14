using _App.Scripts.Controllers;
using ArbanFramework;
using FantasySurvivor;
using Unity.Mathematics;
using UnityEngine;
namespace _App.Scripts.Views.InGame.Skills
{
	public class TwinOriginal : SkillBulletActive
	{
		[SerializeField] private SkillActive _twinInstantiate;

		public override void Init(float damage, Monster target, int level, ItemPrefab type)
		{
			base.Init(damage, target, level, type);
			callBackDamaged += SpawnTwinBorn;
		}

		private void SpawnTwinBorn()
		{
			var bullet = Instantiate(_twinInstantiate, transform.position, new Quaternion(0, 0, 90, 0));
			bullet.GetComponent<TwinBorn>().InitTwin(new Vector2(-direction.y, direction.x), moveSpeed, damage/2, target, level);
			var bullet2 = Instantiate(_twinInstantiate, transform.position, new Quaternion(0, 0, -90, 0));
			bullet2.GetComponent<TwinBorn>().InitTwin(new Vector2(direction.y, -direction.x), moveSpeed, damage/2, target, level);
			Singleton<PoolController>.instance.ReturnObject(this.type, gameObject);
		}
	}
}
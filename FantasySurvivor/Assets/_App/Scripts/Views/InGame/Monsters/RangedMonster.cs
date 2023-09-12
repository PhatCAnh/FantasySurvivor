using Sirenix.OdinInspector;
using UnityEngine;
namespace MR.CharacterState.Unit.Monsters
{
	public class RangedMonster : Monster
	{
		public BulletMonster bulletPrefab;

		public override void Attack()
		{
			var arrowIns = Instantiate(bulletPrefab);
			arrowIns.Init(this, 15f);
		}
	}
}
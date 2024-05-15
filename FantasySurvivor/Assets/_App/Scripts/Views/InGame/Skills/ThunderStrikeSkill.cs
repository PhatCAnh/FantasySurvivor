using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FantasySurvivor;
using UnityEngine;

public class ThunderStrikeSkill : SkillFallActive
{
	protected override void TouchUnit(Monster mons)
	{
		mons.TakeDamage(mons == target ? damage : damage * data.valueSpecial1 / 100, isCritical);
	}
}
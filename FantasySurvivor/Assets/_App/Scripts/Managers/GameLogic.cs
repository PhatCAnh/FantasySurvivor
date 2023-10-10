using System.Collections.Generic;
using UnityEngine;
namespace FantasySurvivor
{
	public static class GameLogic
	{
		public static bool CheckDistance(Vector2 vector1, Vector2 vector2, float distance)
		{
			return (vector1 - vector2).sqrMagnitude < distance * distance;
		}
		
		public static Vector2 RandomPositionSpawnMonster(float radius, Vector3 characterPos)
		{
			var angle = Random.Range(0, 2 * Mathf.PI);
			var x = radius * Mathf.Cos(angle);
			var y = radius * Mathf.Sin(angle);
			return new Vector2(x + characterPos.x, y + characterPos.y);
		}
	}
	
}
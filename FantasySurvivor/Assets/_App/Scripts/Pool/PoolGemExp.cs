using System;
using System.Linq;
using ArbanFramework;
using UnityEngine;
namespace _App.Scripts.Pool
{
	public class PoolGemExp : ObjectPool
	{
		public Action<GemExp> onSpawnGemExp;
		
		private Character character => Singleton<GameController>.instance.character;
		
		protected override void Awake()
		{
			base.Awake();
			Singleton<PoolGemExp>.Set(this);
		}

		protected void OnDestroy()
		{
			Singleton<PoolGemExp>.Unset(this);
		}

		public void GetObjectFromPool(Vector3 position, int value)
		{
			var gemExp = GetObject(position).GetComponent<GemExp>();
			gemExp.Init(value);
			onSpawnGemExp?.Invoke(gemExp);
		}

		public void RemoveObjectToPool(GemExp theGameObject)
		{
			ReturnObject(theGameObject.gameObject);
		}
	}
}
using System;
using ArbanFramework;
using UnityEngine;
namespace _App.Scripts.Pool
{
	public class PoolGemExp : ObjectPool
	{
		public Action<GemExp> onSpawnGemExp;
		
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
			theGameObject.isTouched = false;
			ReturnObject(theGameObject.gameObject);
		}
	}
}
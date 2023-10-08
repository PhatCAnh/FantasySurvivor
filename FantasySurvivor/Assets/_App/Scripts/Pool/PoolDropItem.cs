using System;
using System.Linq;
using ArbanFramework;
using UnityEngine;
namespace FantasySurvivor
{
	public class PoolDropItem : ObjectPool
	{
		protected override void Awake()
		{
			base.Awake();
			Singleton<PoolDropItem>.Set(this);
		}

		protected void OnDestroy()
		{
			Singleton<PoolDropItem>.Unset(this);
		}

		public void GetObjectFromPool(Vector3 position, int value, DropItemType type)
		{
			var gemExp = GetObject(position).GetComponent<DropItem>();
			gemExp.Init(value, type);
		}

		public void RemoveObjectToPool(DropItem theGameObject)
		{
			ReturnObject(theGameObject.gameObject);
		}
	}
}
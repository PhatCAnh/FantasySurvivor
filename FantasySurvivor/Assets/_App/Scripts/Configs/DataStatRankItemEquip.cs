using ArbanFramework.Config;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FantasySurvivor
{
	public class DataStatRankItemEquip : IConfigItem
	{
		public string id { get; private set; }
		public int levelLimit { get;private set; }
		public int atk { get;private set; }
		public int health { get;private set; }

		public string GetId()
		{
			return id;
		}

		public void OnReadImpl(IConfigReader reader)
		{
			id = reader.ReadString();
			levelLimit = reader.ReadInt();
			atk = reader.ReadInt();
			health = reader.ReadInt();
		}
	}

	public class DataStatRankItemEquipTable : Configs<DataStatRankItemEquip>
	{
		public override string FileName => nameof(DataStatRankItemEquip);

		public DataStatRankItemEquip GetConfig(ItemRank rank)
		{
			return GetConfig(rank.ToString());
		}
	}
}
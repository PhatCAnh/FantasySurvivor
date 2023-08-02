using ArbanFramework.Config;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MR
{
    public class ConstConfig : IConfigItem
    {
        public string key;
        public string valueStr;

        public string GetId()
        {
            return key;
        }

        public void OnReadImpl(IConfigReader reader)
        {
            key = reader.ReadString();
            valueStr = reader.ReadString();
        }
    }


    public class ConstConfigTable : Configs<ConstConfig>
    {
        public override string FileName => nameof(ConstConfig);

        public int GetValueInt(string key)
        {
            int val = 0;
            var cfg = GetConfig(key);
            if (cfg == null)
            {
                Debug.AssertFormat(false, "Key {0} not found", key);
                return val;
            }

            int.TryParse(cfg.valueStr, out val);
            return val;
        }
    }
}
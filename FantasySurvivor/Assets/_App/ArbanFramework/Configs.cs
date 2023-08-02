using UnityEngine;
using System.Collections.Generic;

namespace ArbanFramework.Config
{
    public interface IConfigs
    {
        void Load(string text = null);
    }

    public abstract class Configs<_ConfigItem> : IConfigs where _ConfigItem : IConfigItem, new()
    {
        public abstract string FileName { get; }
        public List<_ConfigItem> itemList { get; private set; }
        public Dictionary<string, _ConfigItem> itemDic { get; private set; }

        public void Load(string text = null)
        {
            IConfigReader reader = null;

            if(reader == null)
            {
                try
                {
                    //Load local
                    if(string.IsNullOrEmpty(text))
                    {
                        var fullPath = "Configs/" + FileName;
                        var textAsset = Resources.Load<TextAsset>(fullPath);
                        if (!textAsset)
                        {
                            Debug.LogErrorFormat("Config file missing: \'{0}\'!", fullPath);
                            return;
                        }

                        reader = new CsvConfigReader(textAsset.text);
                    }    
                    //load from text
                    else
                    {
                        reader = new CsvConfigReader(text);
                    }    
                }
                catch { }
            }            
                        
            itemList = new List<_ConfigItem>();
            itemDic = new Dictionary<string, _ConfigItem>();
            try
            {
                while (reader.HasNext())
                {
                    var item = new _ConfigItem();
                    item.OnReadImpl(reader);
                    itemList.Add(item);
                    itemDic.Add(item.GetId(), item);
                }
            }
            catch (System.Exception e)
            {
                if(reader is CsvConfigReader)
                {
                    var csvReader = reader as CsvConfigReader;
                    Debug.LogErrorFormat("Csv reader error! \'{0}\' col:{1} row{2}", FileName, csvReader.currentIdPosition, csvReader.currentFieldPosition);
                }
                Debug.LogException(e);
            }
        }

        public _ConfigItem GetConfig(string configId)
        {
            var res = itemDic.TryGetValue(configId, out _ConfigItem config);
            Debug.AssertFormat(res, "{0} Id missing: \'{1}\'!", this.GetType().Name, configId);

            return config;
        }

        public _ConfigItem GetConfigAt(int index)
        {
            Debug.AssertFormat(index >= 0 && index < itemList.Count, "{0} index outof range: \'{1}\'!", this.GetType().Name, index);
            return itemList[index];
        }
    }
}
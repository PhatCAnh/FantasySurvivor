using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArbanFramework.Tutorial
{
    public enum TutorialNodeType
    {
        Mission2,
        Mission3,
        GunShop,
        TabGun2,
        Upgrade,
        BuyGun,
        YesBuy,
        Back,
        StartBattle,
    }

    public enum TutorialTriggerType
    {
        WorldMap,
        GunShop,
        UpgradeDone,
        ClickBuyGun,
        YesBuyGun,
        BattleInfo,
        Count,
    }

    public struct TriggerValue
    {
        public bool isTrigger;
        public int customValue;
    }

    public class TutorialConfig
    {
        public int step;
        public TutorialNodeType nodeType;
        public TutorialTriggerType triggerType;
        public int customId;
        public int requiredAmount;
    }

    public class TutorialController
    {
        private static TutorialController _instance;

        public static TutorialController instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TutorialController();
                    _instance.Init();
                }

                return _instance;
            }
        }

        public Dictionary<TutorialNodeType, TutorialNode> tutorialNodeDic = new Dictionary<TutorialNodeType, TutorialNode>();
        public Dictionary<TutorialTriggerType, TriggerValue> tutorialTriggerDic = new Dictionary<TutorialTriggerType, TriggerValue>();
        public List<TutorialConfig> tutorialConfigs = new List<TutorialConfig>();
        public int requiredClick { get; private set; } = 0;
        public int currentStep { get; private set; }
        public bool isForceNext { get; private set; } = false;
        public TutorialNode currentNode { get; private set; }
        private void Init()
        {
            for (var i = (TutorialTriggerType)0; i < TutorialTriggerType.Count; i++)
            {
                tutorialTriggerDic[i] = new TriggerValue() { customValue = -1, isTrigger = false };
            }
            InitConfigs();
        }

        private void InitConfigs()
        {
            tutorialConfigs.Add(new TutorialConfig()
            {
                step = 1,
                nodeType = TutorialNodeType.GunShop,
                triggerType = TutorialTriggerType.WorldMap,
                customId = -1,
                requiredAmount = 1,
            });

            tutorialConfigs.Add(new TutorialConfig()
            {
                step = 2,
                nodeType = TutorialNodeType.Upgrade,
                triggerType = TutorialTriggerType.GunShop,
                customId = -1,
                requiredAmount = 10,
            });

            tutorialConfigs.Add(new TutorialConfig()
            {
                step = 3,
                nodeType = TutorialNodeType.TabGun2,
                triggerType = TutorialTriggerType.GunShop,
                customId = 1,
                requiredAmount = 1,
            });

            tutorialConfigs.Add(new TutorialConfig()
            {
                step = 4,
                nodeType = TutorialNodeType.BuyGun,
                triggerType = TutorialTriggerType.GunShop,
                customId = 2,
                requiredAmount = 1,
            });

            tutorialConfigs.Add(new TutorialConfig()
            {
                step = 5,
                nodeType = TutorialNodeType.YesBuy,
                triggerType = TutorialTriggerType.ClickBuyGun,
                customId = -1,
                requiredAmount = 1,
            });

            tutorialConfigs.Add(new TutorialConfig()
            {
                step = 6,
                nodeType = TutorialNodeType.Back,
                triggerType = TutorialTriggerType.GunShop,
                customId = -1,
                requiredAmount = 1,
            });

            tutorialConfigs.Add(new TutorialConfig()
            {
                step = 7,
                nodeType = TutorialNodeType.Mission2,
                triggerType = TutorialTriggerType.WorldMap,
                customId = -1,
                requiredAmount = 1,
            });

            tutorialConfigs.Add(new TutorialConfig()
            {
                step = 8,
                nodeType = TutorialNodeType.StartBattle,
                triggerType = TutorialTriggerType.BattleInfo,
                customId = -1,
                requiredAmount = 1,
            });

            tutorialConfigs.Add(new TutorialConfig()
            {
                step = 9,
                nodeType = TutorialNodeType.GunShop,
                triggerType = TutorialTriggerType.WorldMap,
                customId = -1,
                requiredAmount = 1,
            });

            tutorialConfigs.Add(new TutorialConfig()
            {
                step = 10,
                nodeType = TutorialNodeType.Upgrade,
                triggerType = TutorialTriggerType.GunShop,
                customId = -1,
                requiredAmount = -1,
            });

            tutorialConfigs.Add(new TutorialConfig()
            {
                step = 11,
                nodeType = TutorialNodeType.Back,
                triggerType = TutorialTriggerType.GunShop,
                customId = -1,
                requiredAmount = 1,
            });

            tutorialConfigs.Add(new TutorialConfig()
            {
                step = 12,
                nodeType = TutorialNodeType.Mission3,
                triggerType = TutorialTriggerType.WorldMap,
                customId = -1,
                requiredAmount = 1,
            });

            tutorialConfigs.Add(new TutorialConfig()
            {
                step = 13,
                nodeType = TutorialNodeType.StartBattle,
                triggerType = TutorialTriggerType.BattleInfo,
                customId = -1,
                requiredAmount = 1,
            });
        }

        public void AddNode(TutorialNode node)
        {
            tutorialNodeDic[node.nodeType] = node;
        }

        public IEnumerator RunTutorial()
        {
            ResetNodeValue();
            foreach (var tutorialConfig in tutorialConfigs)
            {
                if (tutorialConfig.step < currentStep)
                    continue;

                var triggerRequired = tutorialConfig.triggerType;
                var triggerCustomId = tutorialConfig.customId;
                yield return new WaitUntil(() => IsTrigger(triggerRequired, triggerCustomId));
                var tutorialNode = tutorialConfig.nodeType;
                currentNode = tutorialNodeDic[tutorialNode];
                currentNode.Show();
                yield return new WaitUntil(() => IsDoneStep(tutorialConfig.requiredAmount));
                ResetNodeValue();
                currentStep++;
            }
        }

        private bool IsDoneStep(int requiredAmount)
        {
            if (requiredAmount < 0)
                return isForceNext;

            return requiredClick >= requiredAmount;
        }

        private void ResetNodeValue()
        {
            isForceNext = false;
            requiredClick = 0;
        }

        public void TutorialNodeClick(TutorialNode tutorialNode)
        {
            if (tutorialNode != currentNode)
                return;

            requiredClick++;
        }

        public void ForceNextTutorial()
        {
            isForceNext = true;
        }

        public void SetTrigger(TutorialTriggerType tutorialTriggerType, int customId, bool isTrigger)
        {
            tutorialTriggerDic[tutorialTriggerType] = new TriggerValue() { customValue = customId, isTrigger = isTrigger };
        }

        public bool IsTrigger(TutorialTriggerType tutorialTriggerType, int customId)
        {
            var value = tutorialTriggerDic[tutorialTriggerType];
            if (customId == -1)
                return value.isTrigger;

            return customId == value.customValue && value.isTrigger;
        }
    }
}

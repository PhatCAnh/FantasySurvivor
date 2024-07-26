using ArbanFramework;
using ArbanFramework.MVC;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace FantasySurvivor
{
    public class TransferItemPopup : View<GameApp>, IPopup
    {
        [SerializeField] private Button _btnTranf, _btnBack;

        [SerializeField] private Transform _slotItemEquipContainer;

        [SerializeField] private GameObject _slotItemEquipPrefab;

        [SerializeField] private ItemSlotChosenTranfUI _slotNone1, _slotNone2;

        private Dictionary<ItemType, ItemSlotChosenTranfUI> _dicItemEquip1;
        private Dictionary<ItemType, ItemSlotChosenTranfUI> _dicItemEquip2;

        private ItemController itemController => Singleton<ItemController>.instance;

        private List<GameObject> _listItemSlot = new List<GameObject>();

        private int _numberValue;
        protected override void OnViewInit()
        {
            base.OnViewInit();
            _slotNone1.Init(null, this);
            _slotNone2.Init(null, this);

            _dicItemEquip1 = new Dictionary<ItemType, ItemSlotChosenTranfUI>
        {
            {ItemType.Weapon, _slotNone1},
            {ItemType.Ring, _slotNone1},
            {ItemType.Gloves, _slotNone1},
            {ItemType.Hat, _slotNone1},
            {ItemType.Shoes, _slotNone1},
            {ItemType.Armor, _slotNone1},
        };
            _dicItemEquip2 = new Dictionary<ItemType, ItemSlotChosenTranfUI>
        {
            {ItemType.Weapon, _slotNone2},
            {ItemType.Ring, _slotNone2},
            {ItemType.Gloves, _slotNone2},
            {ItemType.Hat, _slotNone2},
            {ItemType.Shoes, _slotNone2},
            {ItemType.Armor, _slotNone2},
        };
            
            _btnTranf.onClick.AddListener(ConvertItem);
            _btnBack.onClick.AddListener(Close);
            var dataPlayerModel = app.models.dataPlayerModel;

            foreach (var item in app.models.dataPlayerModel.BagItem)
            {
                var go = Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer);
                go.TryGetComponent(out ItemSlotTranfUI item1);
                item1.Init(item, this);
                _listItemSlot.Add(go);
            }
            var sortedItemSlots = _listItemSlot
               .Select(go => go.GetComponent<ItemSlotTranfUI>())
               .OrderBy(itemSlot => GetItemTypeOrder(itemSlot.itemData.dataConfig.type))
               .ToList();
            for (int i = 0; i < sortedItemSlots.Count; i++)
            {
                sortedItemSlots[i].transform.SetSiblingIndex(i);
                sortedItemSlots[i].gameObject.SetActive(true);
            }
        }
        public void ChosenItem(ItemType type, ItemInBag data)
        {
            if (_slotNone1.itemData == null)
            {
                _slotNone1.ItemType = type;
                var slot = _dicItemEquip1[type];
                if (slot.isChosen) UnChosenItem(type, data);
                slot.Init(data);
            }
            else
                    if (_slotNone1.itemData != null && _slotNone2.itemData == null)
            {
                _slotNone2.ItemType = type;
                var slot = _dicItemEquip2[type];
                if (slot.isChosen) UnChosenItem(type, data);
                slot.Init(data);
            }
        }

        public void UnChosenItem(ItemType type, ItemInBag data)
        {
            if (data == _slotNone1.itemInBag)
            {
                _dicItemEquip1[type].ResetData();
                Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer)
                    .TryGetComponent(out ItemSlotTranfUI item);
                item.Init(data, this);
            }
            if (data == _slotNone2.itemInBag)
            {
                _dicItemEquip2[type].ResetData();
                Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer)
                    .TryGetComponent(out ItemSlotTranfUI item);
                item.Init(data, this);
            }
        }

        public bool UnableChosen()
        {
            if (_slotNone1.itemData != null
             && _slotNone2.itemData != null)
            {
                return false;
            }
            return true;
        }
        public void Open()
        {

        }
        public void Close()
        {
            Destroy(gameObject);
        }
        protected int GetItemTypeOrder(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Weapon: return 0;
                case ItemType.Armor: return 1;
                case ItemType.Shoes: return 2;
                case ItemType.Gloves: return 3;
                case ItemType.Hat: return 4;
                case ItemType.Ring: return 5;
                default: return int.MaxValue;
            }

        }
        public void ConvertItem()
        {
            if (UnableChosen()) return;
            if (_slotNone1.ItemType == _slotNone2.ItemType
                && _slotNone1.itemInBag.level > _slotNone2.itemInBag.level)
            {
                if (Earthpunch.IsActionSuccessful(1))
                {
                    _slotNone2.itemInBag.level = _slotNone1.itemInBag.level-1;
                    _slotNone1.itemInBag.level = 1;
                }
                else
                {
                    app.resourceManager.ShowPopup(PopupType.ConvertItemPopup).TryGetComponent(out Popup_Noty convertItemPopUp);
                }
                _slotNone1.ResetData();
                _slotNone2.ResetData();
                ReloadItem();
            }
        }
        public void ReloadItem()
        {
            foreach (var itemSlot in _listItemSlot)
            {
                Destroy(itemSlot);
            }
            _listItemSlot.Clear();

            // Reinstantiate items
            foreach (var item in app.models.dataPlayerModel.BagItem)
            {
                var go = Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer);
                go.TryGetComponent(out ItemSlotTranfUI item1);
                item1.Init(item, this);
                _listItemSlot.Add(go);
            }
        }
    }
}
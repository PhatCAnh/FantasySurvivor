using ArbanFramework.MVC;
using ArbanFramework;
using UnityEngine.UI;
using DG.Tweening;
using FantasySurvivor;
using UnityEngine;
using TMPro;

public class SuccessfulCombinePopupDetail : View<GameApp>, IPopup
{
    [SerializeField] private Image _imgSkin, _imgRank, _imgIcon;

    [SerializeField] private TextMeshProUGUI _txtName, _txtDescription;

    [SerializeField] private Button _btnClose;

    [SerializeField] private Sprite _spriteWeapon, _spriteArmor, _spriteShoes, _spriteGloves, _spriteHat, _spriteRing;

    [SerializeField] private Sprite _spriteAtk, _spriteHealth;

    [SerializeField] private Transform  _goMainContent;

    [SerializeField] private GameObject _prefabStat, _statContainer;

    private ItemSlotUI _itemSlotUI;

    private ItemInBag _dataInBag;

    private ItemData _itemData;

    private DataStatRankItemEquip _dataStatRank; 

    private int _numberValue;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        _btnClose.onClick.AddListener(Close);
    }
    public void Init(ItemSlotUI itemSlotEquipUI, ItemInBag dataInBag, ItemData itemData, Image imgSkin, Image imgRank, bool isEquip = false)
    {
        _itemSlotUI = itemSlotEquipUI;
        _dataInBag = dataInBag;
        _itemData = itemData;
        _dataStatRank = app.configs.dataStatRankItemEquip.GetConfig(dataInBag.rank);
        _imgSkin.sprite = imgSkin.sprite;
        _imgRank.sprite = imgRank.sprite;
        _txtName.text = $"{itemData.dataConfig.name} - Level: {dataInBag.level} / {_dataStatRank.levelLimit}";
        var nameStat = "";
        switch (itemData.dataConfig.type)
        {
            case ItemType.Weapon:
                _imgIcon.sprite = _spriteWeapon;
                nameStat = "Atk";
                break;
            case ItemType.Armor:
                _imgIcon.sprite = _spriteArmor;
                nameStat = "Health";
                break;
            case ItemType.Shoes:
                _imgIcon.sprite = _spriteShoes;
                nameStat = "Health";
                break;
            case ItemType.Gloves:
                _imgIcon.sprite = _spriteGloves;
                nameStat = "Atk";
                break;
            case ItemType.Hat:
                _imgIcon.sprite = _spriteHat;
                nameStat = "Health";
                break;
            case ItemType.Ring:
                _imgIcon.sprite = _spriteRing;
                nameStat = "Atk";
                break;
        }

        Instantiate(_prefabStat, _statContainer.transform).TryGetComponent(out StatInPopupDescription statInPopupDescription);
        var data = Singleton<GameController>.instance.GetDataStat(nameStat, dataInBag.rank);
        _numberValue = _itemData.dataConfig.baseValue + data.Item2 * (dataInBag.level - 1);
        Sprite sprite = null;
        switch (data.Item3)
        {
            case StatId.Atk:
                sprite = _spriteAtk;
                break;
            case StatId.Health:
                sprite = _spriteHealth;
                break;

        }
        statInPopupDescription.Init(data.Item1, _numberValue, sprite);
        Open();
    }

    public void Action()
    {
        _itemSlotUI.Action(_numberValue);
        Close();
    }

    public void Open()
    {
        _goMainContent.localScale = Vector3.zero;

        _goMainContent.DOScale(Vector3.one, 0.15f);
    }
    public void Close()
    {
        Destroy(gameObject);
    }
}

using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterController : Controller<GameApp>
{
    public int numberLimitBagEquip;
    public int numberLimitBagETC;

    [SerializeField] private CharacterDataTable _charDataTable;

    private Dictionary<CharacterId, CharacterData> _dicCharData;

    private ItemController itemController => Singleton<ItemController>.instance;

    private void Awake()
    {
        Singleton<CharacterController>.Set(this);
    }

    private void Start()
    {
        var charId = app.models.dataPlayerModel.mainChar;

        _dicCharData = new Dictionary<CharacterId, CharacterData>
        {
            { CharacterId.Char1, NewCharacterData(CharacterId.Char1) },
            { CharacterId.Char2, NewCharacterData(CharacterId.Char2) }
        };

        var data = GetDataCharacter(charId).dataStat;

        app.models.characterModel = new CharacterModel(
            data.hp,
            data.moveSpeed,
            data.damage,
            data.itemAttractionRange,
            data.attackRange,
            data.armor,
            data.regen,
            data.shield
        );
    }

    public void ChangeCharacterData(CharacterId id)
    {
        var data = GetDataCharacter(id).dataStat;

        var dataCurrent = GetDataCharacter(app.models.dataPlayerModel.mainChar).dataStat;

        app.models.characterModel.ChangeCharacterStat(
            data.hp - dataCurrent.hp,
            data.moveSpeed - dataCurrent.moveSpeed,
            data.damage - dataCurrent.damage,
            data.itemAttractionRange - dataCurrent.itemAttractionRange,
            data.attackRange - dataCurrent.attackRange,
            data.armor - dataCurrent.armor,
            data.regen - dataCurrent.regen,
            data.shield - dataCurrent.shield
        );
    }

    public CharacterData GetDataCharacter(CharacterId id)
    {
        if (_dicCharData.TryGetValue(id, out CharacterData item))
        {
            return item;
        }

        return null;
    }

    private CharacterData NewCharacterData(CharacterId id)
    {
        var ui = _charDataTable.characterData.Find(item => item.id == id);
        var data = app.configs.dataCharacter.GetConfig(id);
        return new CharacterData(id, ui, data);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Singleton<CharacterController>.Unset(this);
    }
}
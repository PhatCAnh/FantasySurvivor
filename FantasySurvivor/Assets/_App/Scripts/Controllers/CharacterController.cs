using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.Serialization;
public class CharacterController: Controller<GameApp>
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
		_dicCharData = new Dictionary<CharacterId, CharacterData>
		{
			{CharacterId.Char1, NewCharacterData(CharacterId.Char1)}
		};
		
		//chỗ này mốt sửa lại khi có chọn player

		var data = GetDataCharacter(CharacterId.Char1).dataStat;
		
		app.models.characterModel = new CharacterModel(
			data.moveSpeed,
			data.hp,
			data.attackRange,
			data.damage,
			data.itemAttractionRange,
			data.armor,
			1
		);
	}

	public CharacterData GetDataCharacter(CharacterId id)
	{
		if(_dicCharData.TryGetValue(id, out CharacterData item))
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
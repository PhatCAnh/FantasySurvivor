using System;
using System.Collections.Generic;
using FantasySurvivor;
using UnityEngine;
[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 3)]
public class CharacterDataTable : ScriptableObject
{
	public List<CharacterDataUI> characterData;
}

[Serializable]
public class CharacterDataUI
{
	public CharacterId id;
	public string characterName;
	public GameObject prefab;
}

public class CharacterData
{
	public CharacterId id;
	public readonly CharacterDataUI dataUI;
	public readonly DataCharacterConfig dataStat;

	public CharacterData(CharacterId id, CharacterDataUI dataUI, DataCharacterConfig dataStat)
	{
		this.id = id;
		this.dataUI = dataUI;
		this.dataStat = dataStat;
	}
}
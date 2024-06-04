using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private PlayerData _playerData;

	[ContextMenu("Save")]
	public void Save()
	{
		_playerData.playerPosition.x = transform.position.x;
		_playerData.playerPosition.y = transform.position.y;
		
		CloudSaving.SaveSomeData(_playerData, "PlayerData");
		Debug.Log("Saved Data");
	}
	
	[ContextMenu("Load")]
	public async void Load()
	{
		var data = await CloudSaving.LoadSomeData<PlayerData>("PlayerData");

		transform.position = new Vector3(data.playerPosition.x, data.playerPosition.y);

		_playerData.XP = data.XP;
		
		Debug.Log("Loaded Data");
	}
}

[Serializable]
public struct PlayerData
{
	public int XP;
	public PlayerPosition playerPosition;
}

[Serializable]
public struct PlayerPosition
{
	public float x, y;
}

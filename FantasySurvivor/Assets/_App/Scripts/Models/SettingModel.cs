using Newtonsoft.Json;
using ArbanFramework.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingModel : Model<GameApp>
{
	public static EventTypeBase dataChangedEvent = new EventTypeBase( nameof(SettingModel) + ".dataChanged" );

	[JsonProperty] private bool _isSound;
	[JsonProperty] private bool _isMusic;

	public SettingModel() : base( dataChangedEvent )
	{
	}

	public override void InitBaseData()
	{
		_isSound = true;
		_isMusic = true;
	}

	public bool isSound
	{
		get => _isSound;
		set
		{
			if( _isSound == value )
				return;
			_isSound = value;
			RaiseDataChanged( nameof(isSound) );
		}
	}

	public bool isMusic
	{
		get => _isMusic;
		set
		{
			if( _isMusic == value )
				return;
			_isMusic = value;
			RaiseDataChanged( nameof(isMusic) );
		}
	}
}
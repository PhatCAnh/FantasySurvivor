using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class AnalyticsController
{
	public async void Init()
	{
		return;
		try
		{
			await UnityServices.InitializeAsync();
			AnalyticsService.Instance.StartDataCollection();
		}
		catch(ConsentCheckException e)
		{
			Debug.Log(e.ToString());
		}
	}

	private void LogEvent(string key, Dictionary<string, object> value)
	{
		return;
		AnalyticsService.Instance.CustomData(key, value);
		AnalyticsService.Instance.Flush();
	}

	public void TrackPlay(LevelResult result, int level)
	{
		return;
		Dictionary<string, object> parameters = new Dictionary<string, object>()
		{
			{TrackParamsKey.LevelResult, result.ToString()},
			{TrackParamsKey.Level, level}
		};
		LogEvent(TrackEventName.TRACK_PLAY, parameters);
	}
	
	public void TrackAds(TypeAds type)
	{
		return;
		Dictionary<string, object> parameters = new Dictionary<string, object>()
		{
			{TrackParamsKey.Category, type.ToString()},
		};
		LogEvent(TrackEventName.TRACK_AD, parameters);
	}

	public void TrackUpStat(bool isInGame, TypeStatTower type, int level)
	{
		return;
		Dictionary<string, object> parameters = new Dictionary<string, object>()
		{
			{TrackParamsKey.Category, isInGame ? "inGame" : "outGame"},
			{TrackParamsKey.StatType, type.ToString()},
			{TrackParamsKey.Level, level}
		};
		LogEvent(TrackEventName.TRACK_UPSTAT, parameters);
	}
}

public static class TrackEventName
{
	public const string TRACK_IAP = "track_iap";
	public const string TRACK_AD = "track_ad";
	public const string TRACK_PLAY = "track_play";
	public const string TRACK_UPSTAT = "track_upstat";
	public const string TRACK_SOFT_CURRENCY = "track_soft_currency";
	public const string TRACK_BOOSTER = "track_booster";
}

public static class TrackParamsKey
{
	public const string Category = "category";
	public const string TrackId = "id";
	public const string IapResult = "iap_result";
	public const string AdResult = "ad_result";
	public const string LevelResult = "level_result";
	public const string LevelName = "levelName";
	public const string StatType = "stat_type";
	public const string BoosterResult = "booster_result";
	public const string SinkType = "sink_type";
	public const string SourceType = "source_type";
	public const string Value = "value";
	public const string AdFormat = "ad_format";
	public const string Level = "level";
	public const string BoosterName = "booster_name";
	public const string ProductId = "product_id";
	public const string Reason = "reason";
}
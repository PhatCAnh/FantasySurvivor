using System;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;
namespace FantasySurvivor
{
	public class Tracking : MonoBehaviour
	{
		async void Start()
		{
			try
			{
				await UnityServices.InitializeAsync();
				AnalyticsService.Instance.StartDataCollection();
			}
			catch (ConsentCheckException e)
			{
				Debug.Log(e.ToString());
			}
		}

		public void Test()
		{
			int currentLevel = Random.Range(1, 4); //Gets a random number from 1-3
//Define Custom Parameters
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "level_result", "level" + currentLevel.ToString()}
			};
// The ‘levelCompleted’ event will get cached locally 
			//and sent during the next scheduled upload, within 1 minute
			AnalyticsService.Instance.CustomData("track_play", parameters);
// You can call Events.Flush() to send the event immediately
			AnalyticsService.Instance.Flush();
		}
	}
}
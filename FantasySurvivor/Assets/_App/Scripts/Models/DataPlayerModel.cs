using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using ArbanFramework.MVC;
using Newtonsoft.Json;
using Unity.Collections.LowLevel.Unsafe;
namespace FantasySurvivor
{
	public class DataPlayerModel : Model<GameApp>
	{
		public static EventTypeBase dataChangedEvent = new EventTypeBase(nameof(DataPlayerModel) + ".dataChanged");

		public DataPlayerModel(EventTypeBase eventType) : base(dataChangedEvent)
		{
		}

		public DataPlayerModel() : base(dataChangedEvent)
		{

		}

		public override void InitBaseData()
		{
			this.coin = 100;
			levelAd = 1;
			levelAr = 1;
			levelAs = 1;
			levelHealth = 1;
		}

		private int _coin;
		[JsonProperty] private int _levelAd;
		[JsonProperty] private int _levelAr;
		[JsonProperty] private int _levelAs;
		[JsonProperty] private int _levelHealth;

		public int coin
		{
			get => _coin;
			set {
				if(coin == value) return;
				_coin = value;
				RaiseDataChanged(nameof(coin));
			}
		}

		public int levelAd
		{
			get => _levelAd;
			set {
				if(levelAd == value) return;
				_levelAd = value;
				RaiseDataChanged(nameof(levelAd));
			}
		}

		public int levelAr
		{
			get => _levelAr;
			set {
				if(levelAr == value) return;
				_levelAr = value;
				RaiseDataChanged(nameof(levelAr));
			}
		}

		public int levelAs
		{
			get => _levelAs;
			set {
				if(levelAs == value) return;
				_levelAs = value;
				RaiseDataChanged(nameof(levelAs));
			}
		}
		public int levelHealth
		{
			get => _levelHealth;
			set {
				if(levelHealth == value) return;
				_levelHealth = value;
				RaiseDataChanged(nameof(levelHealth));
			}
		}
	}
}
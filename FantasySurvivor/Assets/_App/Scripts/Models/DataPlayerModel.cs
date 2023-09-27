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
			levelAd = 0;
			levelAr = 0;
			levelAs = 0;
			levelHealth = 0;
			levelCr = 0;
			levelCd = 0;

			firstTouchHand = true;
			firstTutorialHandUi = true;
			firstSeeBulletInteract = true;
			app.models.WriteModel<DataPlayerModel>();
		}

		[JsonProperty] private int _coin;
		[JsonProperty] private int _levelAd;
		[JsonProperty] private int _levelAr;
		[JsonProperty] private int _levelAs;
		[JsonProperty] private int _levelCr;
		[JsonProperty] private int _levelCd;
		
		[JsonProperty] private int _levelHealth;
		
		[JsonProperty] public bool firstTouchHand;
		[JsonProperty] public bool firstTutorialHandUi;
		[JsonProperty] public bool firstSeeBulletInteract;
		
		

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
		
		public int levelCr
		{
			get => _levelCr;
			set {
				if(levelCr == value) return;
				_levelCr = value;
				RaiseDataChanged(nameof(levelCr));
			}
		}
		
		public int levelCd
		{
			get => _levelCd;
			set {
				if(levelCd == value) return;
				_levelCd = value;
				RaiseDataChanged(nameof(levelCd));
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
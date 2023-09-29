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
			this.Coin = 100;

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
		[JsonProperty] private int _levelRegenHp;
		
		[JsonProperty] public bool firstTouchHand;
		[JsonProperty] public bool firstTutorialHandUi;
		[JsonProperty] public bool firstSeeBulletInteract;
		
		public int Coin
		{
			get => _coin;
			set {
				if(Coin == value) return;
				_coin = value;
				app.models.WriteModel<DataPlayerModel>();
				RaiseDataChanged(nameof(Coin));
			}
		}

		public int LevelAd
		{
			get => _levelAd;
			set {
				if(LevelAd == value) return;
				_levelAd = value;
				app.models.WriteModel<DataPlayerModel>();
				RaiseDataChanged(nameof(LevelAd));
			}
		}

		public int LevelAr
		{
			get => _levelAr;
			set {
				if(LevelAr == value) return;
				_levelAr = value;
				app.models.WriteModel<DataPlayerModel>();
				RaiseDataChanged(nameof(LevelAr));
			}
		}

		public int LevelAs
		{
			get => _levelAs;
			set {
				if(LevelAs == value) return;
				_levelAs = value;
				app.models.WriteModel<DataPlayerModel>();
				RaiseDataChanged(nameof(LevelAs));
			}
		}
		
		public int LevelCr
		{
			get => _levelCr;
			set {
				if(LevelCr == value) return;
				_levelCr = value;
				app.models.WriteModel<DataPlayerModel>();
				RaiseDataChanged(nameof(LevelCr));
			}
		}
		
		public int LevelCd
		{
			get => _levelCd;
			set {
				if(LevelCd == value) return;
				_levelCd = value;
				app.models.WriteModel<DataPlayerModel>();
				RaiseDataChanged(nameof(LevelCd));
			}
		}
		
		public int LevelHealth
		{
			get => _levelHealth;
			set {
				if(LevelHealth == value) return;
				_levelHealth = value;
				app.models.WriteModel<DataPlayerModel>();
				RaiseDataChanged(nameof(LevelHealth));
			}
		}
		
		public int LevelRegenHp
		{
			get => _levelRegenHp;
			set {
				if(LevelRegenHp == value) return;
				_levelRegenHp = value;
				app.models.WriteModel<DataPlayerModel>();
				RaiseDataChanged(nameof(LevelRegenHp));
			}
		}
	}
}
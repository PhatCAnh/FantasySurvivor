using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using ArbanFramework.MVC;
using Newtonsoft.Json;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
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
			this.Coin = 99;
            this.Gem = 10;
            this.Health = 100;
            this.Atkdamage = 20;
			this.Armor = 50;
            this.Movespeed = 2.5f; 
			firstTouchHand = true;
			firstTutorialHandUi = true;
			firstSeeBulletInteract = true;
			app.models.WriteModel<DataPlayerModel>();
		}

		[JsonProperty] private int _coin;
        [JsonProperty] private int _gem;
        [JsonProperty] private float _movespeed;
        [JsonProperty] private int _health;
        [JsonProperty] private int _atkdamage;
        [JsonProperty] private int _armor;
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
        public int Armor
        {
            get => _armor;
            set
            {
                if (Armor == value) return;
                _armor = Mathf.Clamp(value, 0, 100); // Giữ giá trị giáp trong phạm vi hợp lệ (0-100)
                app.models.WriteModel<DataPlayerModel>();
                RaiseDataChanged(nameof(Armor));
            }
        }
        public int Atkdamage
        {
            get => _atkdamage;
            set
            {
                if (Atkdamage == value) return;
                _atkdamage = value;
                app.models.WriteModel<DataPlayerModel>();
                RaiseDataChanged(nameof(Atkdamage));
            }
        }
        public int Gem
        {
            get => _gem;
            set
            {
                if (Gem == value) return;
                _gem = value;
                app.models.WriteModel<DataPlayerModel>();
                RaiseDataChanged(nameof(Gem));
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
        public int Health
        {
            get => _health;
            set
            {
                if (Health == value) return;
                _health = value;
                app.models.WriteModel<DataPlayerModel>();
                RaiseDataChanged(nameof(Health));
            }
        }
        public float Movespeed
        {
            get => _movespeed;
            set
            {
                if (Movespeed == value) return;
                _movespeed = value;
                app.models.WriteModel<DataPlayerModel>();
                RaiseDataChanged(nameof(Movespeed));
            }
        }
    
    }
}
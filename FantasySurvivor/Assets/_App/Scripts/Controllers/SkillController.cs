
using System.Collections.Generic;
using System.Linq;
using _App.Datas.DataScript;
using _App.Scripts.Views.InGame.Skills.SkillMono;
using ArbanFramework;
using ArbanFramework.MVC;
using FantasySurvivor;
using UnityEngine;
namespace _App.Scripts.Controllers
{
	public class SkillController : Controller<GameApp>
	{
		[SerializeField] private SkillDataTable _skillDataTable;
		
		private List<SkillDataTotal> _listSkillTotal;

		private List<SkillId> _listSkillSelected;
		
		private List<SkillId> _listSkillChoose;

		public List<Skill> listSkills = new List<Skill>();
		
		private int _limitSkill = 2;
		private void Awake()
		{
			Singleton<SkillController>.Set(this);
		}
		
		private void Start()
		{
			_listSkillTotal = new List<SkillDataTotal>();
			_listSkillSelected = app.models.dataPlayerModel.SkillSet;
			_listSkillChoose = new List<SkillId>();

			var listSkillUI = _skillDataTable.listSkillData;

			foreach(var item in listSkillUI)
			{
				var id = item.id;
				var dataStat = app.configs.dataLevelSkill.GetConfig(id);
				var skill = new SkillDataTotal(id, item, dataStat);
				_listSkillTotal.Add(skill);
			}
		}
		
		public List<SkillData> GetListSkillDataTable()
		{
			return _skillDataTable.listSkillData.Where(skill => skill.name != SkillName.Food).ToList();
		}

      
        public SkillData GetSkillDataTable(SkillName name)
		{
			return _skillDataTable.listSkillData.FirstOrDefault(skill => skill.name == name);
		}

		public List<SkillDataTotal> GetListSkillTotal()
		{
			return _listSkillTotal;
		}
		
		public List<SkillDataTotal> GetListSkill()
		{
			return _listSkillTotal.Where(skill => skill.skillDataUI.canAppear).ToList();
		}

		public List<SkillId> GetListSkillInGame()
		{
			return _listSkillSelected;
		}

		public void ResetDataSkillInGame()
		{
			_listSkillChoose.Clear();
			listSkills.Clear();
		}
		
		public List<SkillDataTotal> GetRandomSkill()
		{
			int count = 3;
			List<SkillDataTotal> newList = new List<SkillDataTotal>();
			if (_listSkillSelected.Count < 3)
			{
				count = _listSkillSelected.Count;
			}
			for (int i = 0; i < count; i++)
			{
				SkillDataTotal skill;
				do
				{
					skill = GetDataSkill(_listSkillSelected[Random.Range(0, _listSkillSelected.Count)]);
				} while (newList.Contains(skill));
				newList.Add(skill);
			}

			if (_listSkillSelected.Count == 0)
			{
				//newList.Add(app.resourceManager.GetSkill(SkillName.Food));
			}

			return newList;
		}

		public void RemoveSkill(SkillId id)
		{
			foreach (var skill in _listSkillSelected.ToList())
			{
				if (skill.Equals(id))
				{
					_listSkillSelected.Remove(skill);
				}
			}
		}

		public void ChoiceSkillInGame(SkillId id)
		{
			var data = GetDataSkill(id);
			var dataUI = data.skillDataUI;
			foreach(var skill in listSkills)
			{
				if(skill.skillId == id)
				{
					skill.UpLevel();
					if(skill.level >= 6) RemoveSkill(skill.skillId);
					return;
				}
			}

			switch (dataUI.type)
			{
				case SkillType.Active:
				{
					Skill skillIns;
					switch (dataUI.name)
                    {
                        case SkillName.Fireball:
                            skillIns = new FireBallControl();
                            break;
                        case SkillName.ThunderStrike:
                            skillIns = new ThunderStrikeControl();
                            break;
                        case SkillName.Waterball:
                            skillIns = new WaterBallControl();
                            break;
                        // case SkillName.ThunderChanneling:
                        // 	skillIns = new ThunderChanneling();
                        // 	break;
                        case SkillName.FireShield:
                            skillIns = new FireShieldControl();
                            break;
                        case SkillName.Shark:
                            skillIns = new SharkControl();
                            break;
                        case SkillName.PoisonBullet:
                            skillIns = new PoisonballControl();
                            break;
                        case SkillName.Earthpunch:
                            skillIns = new EarthPunchControl();
                            break;
                        case SkillName.SkyBoom:
                            skillIns = new SkyboomControl();
                            break;
                        case SkillName.Boomerang:
                            skillIns = new BoomerangControl();
                            break;
                        case SkillName.SmilingFace:
                            skillIns = new SmilingFaceControl();
                            break;
                        case SkillName.IceSpear:
                            skillIns = new IceSpearControl();
                            break;
                        case SkillName.Twin: 
                            skillIns = new TwinControl(); 
                            break;
                        default:
                            skillIns = new ProactiveSkill();
                            break;
                    }
					skillIns.Init(data);
					listSkills.Add(skillIns);
					_listSkillChoose.Add(id);
					break;
				}
				case SkillType.Buff:
					/*if(dataUI.name == SkillName.Food)
					{
						model.currentHealthPoint += model.currentHealthPoint * 20 / 100;
					}*/
					break;
			}
			if(_listSkillChoose.Count == _limitSkill)
			{
				_listSkillSelected = _listSkillChoose;
			}
		}

		public SkillDataTotal GetDataSkill(SkillId id)
		{
			return GetListSkill().FirstOrDefault(skill => skill.id == id);
		}

		public Skill GetSkillChoose(SkillId id)
		{
			return listSkills.FirstOrDefault(skill => skill.skillId == id);
		}

		public void AddSkillSet(SkillId id)
		{
			_listSkillSelected.Add(id);
			app.models.dataPlayerModel.AddSkillSet(id);
		}

		public void RemoveSkillSet(SkillId id)
		{
			_listSkillSelected.Remove(id);
			app.models.dataPlayerModel.RemoveSkillSet(id);
		}
		
		protected override void OnDestroy()
		{
			base.OnDestroy();
			Singleton<SkillController>.Unset(this);
		}
	}
}
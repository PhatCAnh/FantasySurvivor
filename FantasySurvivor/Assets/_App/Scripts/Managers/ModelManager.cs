using ArbanFramework.MVC;
using FantasySurvivor;
using MR;

public class ModelManager : ModelManagerBase
{
	
	//Models
	public SettingModel settingModel;
	public MonsterModel monsterModel;
	public TowerModel towerModel;
	public MapModel mapModel;
	public void Init()
	{
		//Register
		RegisterModel(out settingModel);
		RegisterModel(out monsterModel);
		RegisterModel(out towerModel);
		RegisterModel(out mapModel);
		LoadData();
	}
}
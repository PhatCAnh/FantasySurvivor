using ArbanFramework.MVC;
using FantasySurvivor;
using MR;

public class ModelManager : ModelManagerBase
{
	public MonsterModel monsterModel;
	public TowerModel towerModel;
	//Models
	public SettingModel settingModel;
	public void Init()
	{
		//Register
		RegisterModel(out settingModel);
		RegisterModel(out monsterModel);
		RegisterModel(out towerModel);
		LoadData();
	}
}
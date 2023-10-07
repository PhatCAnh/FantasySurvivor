using ArbanFramework.MVC;
using FantasySurvivor;

public class ModelManager : ModelManagerBase
{
	//Models
	public SettingModel settingModel;
	public DataPlayerModel dataPlayerModel;
	public CharacterModel characterModel;
	public MonsterModel monsterModel;
	public TowerModel towerModel;
	public MapModel mapModel;
	public void Init()
	{
		//Register
		RegisterModel(out characterModel);
		RegisterModel(out dataPlayerModel);
		RegisterModel(out settingModel);
		RegisterModel(out monsterModel);
		RegisterModel(out towerModel);
		RegisterModel(out mapModel);
		LoadData();
	}
}
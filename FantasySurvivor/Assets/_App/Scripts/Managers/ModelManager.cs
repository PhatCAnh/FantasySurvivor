using ArbanFramework.MVC;
using MR;

public class ModelManager : ModelManagerBase
{
	public CharacterModel characterModel;
	//Models
	public SettingModel settingModel;
	public void Init()
	{
		//Register
		RegisterModel(out settingModel);
		RegisterModel(out characterModel);
		LoadData();
	}
}
using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine.UI;

public class Icon_ChoiceSkill : View<GameApp>
{
    public SkillId id;
    public Image image;
    public Toggle toggle;
    private PopupChoiceSkill parent;
    public TextMeshProUGUI _txtName;
    private SkillData _skillData;
    private SkillController skillController => Singleton<SkillController>.instance;

    //public void Init(SkillId id, PopupChoiceSkill parent, bool checkSKillSet)
    //{
    //    this.id = id;
    //    this.parent = parent;
    //    _skillData = skillController.GetDataSkill(id).skillDataUI;
    //     toggle.isOn= checkSKillSet ;
    //    _txtName.text = skillController.GetDataSkill(id).skillDataUI.name.ToString();
    //    toggle.interactable = false;
    //    // if(checkSKillSet)
    //    // {
    //    //     skillController.GetListSkillInGame().Add(id);
    //    //     parent.UpdateTextNumberChoiceSkill(true);
    //    // }
    //}
    public void ShowList(SkillId id, PopupChoiceSkill parent)
    {
        this.id = id;
        this.parent = parent;
        _skillData = skillController.GetDataSkill(id).skillDataUI;
        _txtName.text = skillController.GetDataSkill(id).skillDataUI.name.ToString();
        toggle.isOn = false;
        toggle.interactable = false;
    }
    protected override void OnViewInit()
    {
        base.OnViewInit();
        image.sprite = _skillData.imgUI;
        //     toggle.onValueChanged.AddListener(OnClickTgl);

    }
    //private void OnClickTgl(bool value)
    //{
    //    toggle.isOn = parent.UpdateTextNumberChoiceSkill(id);
    //}
}
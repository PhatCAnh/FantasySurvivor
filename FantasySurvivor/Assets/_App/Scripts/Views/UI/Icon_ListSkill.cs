using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Icon_ListSkill : View<GameApp>
{
    public SkillId id;
    public Image image;
    public Toggle toggle;
    public SkillName SkillName;
    public TextMeshProUGUI _txtName;
    private Showlistskill parent;
    private SkillData _skillData;
    private SkillController skillController => Singleton<SkillController>.instance;

    //public void Init(SkillId id, PopupChoiceSkill parent, bool checkSKillSet)
    //{
    //    this.id = id;
    //    this.parent = parent;
    //    _skillData = skillController.GetDataSkill(id).skillDataUI;
    //    toggle.isOn = checkSKillSet;
    //    // if(checkSKillSet)
    //    // {
    //    //     skillController.GetListSkillInGame().Add(id);
    //    //     parent.UpdateTextNumberChoiceSkill(true);
    //    // }
    //}
    public void Init(SkillId id,Showlistskill parent, bool checkSKillSet)
    {
        this.id = id;
        this.parent = parent;
        _txtName.text = skillController.GetDataSkill(id).skillDataUI.name.ToString();
        //_txtName.enableWordWrapping = true;
        //_txtName.overflowMode = TextOverflowModes.Overflow;
        _skillData = skillController.GetDataSkill(id).skillDataUI;
        toggle.isOn = checkSKillSet;
    }
    public void ShowList(SkillId id, Showlistskill parent)
    {
        this.id = id;
       this.parent = parent;
        _skillData = skillController.GetDataSkill(id).skillDataUI;
        _txtName.text = skillController.GetDataSkill(id).skillDataUI.name.ToString();
        toggle.isOn = false;
        toggle.interactable = false;
    }
    public void SkillDetail(SkillId id, Showlistskill parent)
    {
        this.id = id;
        this.parent = parent;
        _skillData = skillController.GetDataSkill(id).skillDataUI;
        toggle.isOn = false;
    }
    protected override void OnViewInit()
    {
        base.OnViewInit();
        image.sprite = _skillData.imgUI;
        toggle.onValueChanged.AddListener(OnClickTgl);

    }
    private void OnClickTgl(bool value)
    {

        toggle.isOn = parent.UpdateTextNumberChoiceSkill(id);
     

    }


}
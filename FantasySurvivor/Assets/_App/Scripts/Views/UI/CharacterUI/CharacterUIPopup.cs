using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharacterUIPopup : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnClose, _btnSelect, _btnSelected;

    [SerializeField] private Sprite _spriteChar1, _spriteChar2;

    [SerializeField] private TextMeshProUGUI _txtMainName, _txtMainDescription, _txtAtk, _txtHealth;

    [SerializeField] private Toggle _toggleChar1, _toggleChar2;

    [SerializeField] private GameObject _goFocusChar1, _goFocusChar2, _goSelectChar1, _goSelectChar2;

    [SerializeField] private Image _imgMain;

    private CharacterId characterId;

    protected override void OnViewInit()
    {
        _btnClose.onClick.AddListener(Close);

        _toggleChar1.onValueChanged.AddListener(OnClickToggleChar1);
        _toggleChar2.onValueChanged.AddListener(OnClickToggleChar2);

        _btnSelect.onClick.AddListener(OnClickBtnSelect);
        _btnSelected.onClick.AddListener(OnClickBtnSelected);
        _btnClose.onClick.AddListener(Close);

        if (app.models.dataPlayerModel.mainChar == CharacterId.Char1)
        {
            OnClickToggleChar1(true);
            _goSelectChar1.SetActive(true);
            _goSelectChar2.SetActive(false);
        }
        else
        {
            OnClickToggleChar2(true);
            _goSelectChar1.SetActive(false);
            _goSelectChar2.SetActive(true);
        }
        Open();
    }

    public void Open()
    {
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    private void OnClickToggleChar1(bool value)
    {
        if (app.models.dataPlayerModel.mainChar == CharacterId.Char1)
        {
            _btnSelected.gameObject.SetActive(true);
            _btnSelect.gameObject.SetActive(false);
        }
        else
        {
            _btnSelected.gameObject.SetActive(false);
            _btnSelect.gameObject.SetActive(true);
        }

        characterId = CharacterId.Char1;

        _goFocusChar1.SetActive(value);
        _goFocusChar2.SetActive(!value);
        if (value)
        {
            var data = app.configs.dataCharacter.GetConfig(CharacterId.Char1);
            _imgMain.sprite = _spriteChar1;
            _txtMainName.text = data.name;
            _txtAtk.text = data.damage.ToString();
            _txtHealth.text = data.hp.ToString();
        }
    }

    private void OnClickToggleChar2(bool value)
    {
        if (app.models.dataPlayerModel.mainChar == CharacterId.Char2)
        {
            _btnSelected.gameObject.SetActive(true);
            _btnSelect.gameObject.SetActive(false);
        }
        else
        {
            _btnSelected.gameObject.SetActive(false);
            _btnSelect.gameObject.SetActive(true);
        }

        _goFocusChar2.SetActive(value);
        _goFocusChar1.SetActive(!value);

        characterId = CharacterId.Char2;

        if (value)
        {
            var data = app.configs.dataCharacter.GetConfig(CharacterId.Char2);
            _imgMain.sprite = _spriteChar2;
            _txtMainName.text = data.name;
            _txtAtk.text = data.damage.ToString();
            _txtHealth.text = data.hp.ToString();
        }
    }

    private void OnClickBtnSelect()
    {
        Singleton<CharacterController>.instance.ChangeCharacterData(characterId);
        app.models.dataPlayerModel.mainChar = characterId;
        if (characterId == CharacterId.Char1)
        {
            _goSelectChar1.SetActive(true);
            _goSelectChar2.SetActive(false);
        }
        else
        {
            _goSelectChar1.SetActive(false);
            _goSelectChar2.SetActive(true);
        }
    }

    private void OnClickBtnSelected()
    {
    }
}
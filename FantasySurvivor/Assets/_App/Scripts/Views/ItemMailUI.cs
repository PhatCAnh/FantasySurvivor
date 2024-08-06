using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMailUI : View<GameApp>
{
    [SerializeField] private TextMeshProUGUI _txtTitle;

    [SerializeField] private Button _btnRead;

    public void Init(ItemMail itemMail)
    {
        _txtTitle.text = itemMail.title;

        _btnRead.onClick.AddListener(() =>
        {
            app.resourceManager.ShowPopup(PopupType.ReadMail).TryGetComponent(out ReadMailPopup popup);
            popup.Init(itemMail);
        });
    }
}
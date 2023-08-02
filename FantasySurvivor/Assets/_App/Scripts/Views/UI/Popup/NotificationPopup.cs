using ArbanFramework.MVC;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MR
{
    public class NotificationPopup : View<GameApp>, IPopup
    {
        public TextMeshProUGUI contentNotifi;

        protected override void OnViewInit()
        {
            base.OnViewInit();
            Open();
        }

        public void Close()
        {
            transform.DOScale(0.25f, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        public void Open()
        {
            transform.localScale = Vector3.one * 0.25f;
            transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
        }
    }
}

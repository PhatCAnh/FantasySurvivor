using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MR
{
    public class ChoiceMazePopup : View<GameApp>, IPopup
    {
        [SerializeField] private Button huntBtn;
        [SerializeField] private Button cancelBtn;

        protected override void OnViewInit()
        {
            base.OnViewInit();

            huntBtn.onClick.AddListener(Hunt);

            cancelBtn.onClick.AddListener(Close);
        }

        public void Close()
        {
            transform.DOScale(0.25f, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
            {
                Singleton<GameController>.instance.StartGame();
                Destroy(gameObject);
            });
        }

        public void Open()
        {
            transform.localScale = Vector3.one * 0.25f;
            transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
        }

        private void Hunt()
        {
            
            Close();
        }
    }
}

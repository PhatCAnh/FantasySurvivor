using ArbanFramework.MVC;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FantasySurvivor
{
    public class NotificationPopup : View<GameApp>, IPopup
    {
        [SerializeField] private Transform _goMainContent;
        
        [SerializeField] private TextMeshProUGUI _txtContent;


        private Sequence sequence;
        public void Init(string content)
        {
            _txtContent.text = content;
            sequence = DOTween.Sequence();
            Open();
        }
        
        public void Open()
        {
            sequence
                .Append(_goMainContent.DOScale(Vector3.one, 0.15f))
                .Append( _goMainContent.DOScale(Vector3.zero, 0.15f).SetDelay(1))
                .OnComplete(() => { Destroy(gameObject); });
        }

        public void Close()
        {
            sequence.Kill();
            Destroy(gameObject);
        }
    }
}
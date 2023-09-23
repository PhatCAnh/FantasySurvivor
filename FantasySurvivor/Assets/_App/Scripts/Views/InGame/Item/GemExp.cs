using _App.Scripts.Pool;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using Popup;
using UnityEngine;
using UnityEngine.Serialization;

public class GemExp : View<GameApp>
{
    [SerializeField] private SpriteRenderer _skin;
    [SerializeField] private int _valueExp;

    private Sequence _sequence;

    [FormerlySerializedAs("_isTouched")]
    public bool isTouched = false;

    private GameController gameController => Singleton<GameController>.instance;

    public void Init(int valueExp)
    {
        _valueExp = valueExp;
    }
    
    private void OnMouseEnter()
    {
        if(gameController.isStop) return;
        
        if(isTouched) return;
        isTouched = true;
        
        var endPoint = 1.5f * transform.position + (transform.position - gameController.tower.transform.position) .normalized;

        _sequence = DOTween.Sequence();

        _sequence
            .Append(transform.DOLocalMove(endPoint, 0.5f))
            .Append(DOTween.To(() => transform.position, x => transform.position = x, gameController.tower.transform.position, 0.25f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    gameController.map.model.expInGame += _valueExp;
                    Singleton<PoolGemExp>.instance.RemoveObjectToPool(this);
                }));
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _sequence.Kill();
    }
}

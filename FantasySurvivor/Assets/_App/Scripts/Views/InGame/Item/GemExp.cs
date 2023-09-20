using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using Popup;
using UnityEngine;

public class GemExp : View<GameApp>
{
    [SerializeField] private SpriteRenderer _skin;
    [SerializeField] private int _valueExp;

    private Sequence _sequence;

    private bool _isTouched = false;

    private GameController gameController => Singleton<GameController>.instance;

    public void Init(int valueExp)
    {
        _valueExp = valueExp;
    }
    
    private void OnMouseEnter()
    {
        if(_isTouched) return;
        _isTouched = true;
        
        var endPoint = 1.5f * transform.position + (transform.position - gameController.tower.transform.position) .normalized;

        _sequence = DOTween.Sequence();

        _sequence
            .Append(transform.DOLocalMove(endPoint, 0.5f))
            .Append(DOTween.To(() => transform.position, x => transform.position = x, gameController.tower.transform.position, 0.25f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    gameController.map.model.expInGame += _valueExp;
                    Destroy(gameObject);
                }));
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _sequence.Kill();
    }
}

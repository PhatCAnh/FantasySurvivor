using System;
using _App.Scripts.Pool;
using ArbanFramework.MVC;
using DG.Tweening;
using Popup;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Sequence = DG.Tweening.Sequence;

public class GemExp : View<GameApp>
{
    [SerializeField] private SpriteRenderer _skin;
    public int valueExp;
    private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

    private State state = State.Waiting;
    
    private enum State
    {
        Waiting,
        Follow,
    }

    public void Init(int valueExp)
    {
        this.valueExp = valueExp;
    }

    private void Update()
    {
        //chuyen cai nay state machine
        if(gameController.isStop) return;
        if(state == State.Waiting && Vector2.Distance(transform.position, gameController.character.transform.position) < 3f)
        {
            state = State.Follow;
        }
        else if(state == State.Follow)
        {
            transform.position = Vector2.MoveTowards(transform.position, gameController.character.transform.position, 10f * Time.deltaTime);
            if(Vector2.Distance(transform.position, gameController.character.transform.position) < 0.1f)
            {
                gameController.Collected(this);
                ArbanFramework.Singleton<PoolGemExp>.instance.RemoveObjectToPool(this);
                state = State.Waiting;
            }
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        transform.DOKill();
    }
}

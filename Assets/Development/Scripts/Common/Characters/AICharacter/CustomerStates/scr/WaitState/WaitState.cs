using System;
using System.Collections;
using GameHandler;
using UnityEngine;
using Zenject;

public abstract class WaitState : CustomerState
{
    protected abstract Transform WaitPoint { get; }

    public event Action OnLeaveState;

    [Inject]
    private UpdateHandler _updateHandler;

    public WaitState(CustomerStateType stateType, AICharacter parent) : base(stateType, parent)
    {
    }

    protected override void onEnter()
    {
        Debug.Log("Ждет, когда обслужат");

        _movement.Enable();
        Move(WaitPoint);
        Subscribe();
    }

    protected override void onExit() => Unsubscribe();

    protected void Move(Transform waitPoint)
    {
        _movement.MoveAI(waitPoint.position).OnComplete(() =>
        {
            _movement.Look(-waitPoint.forward);
            _movement.Stop();
        });
    }

    protected void OnCompliteOrder()
    {
        Unsubscribe();
        StartLeave();
    }

    protected void StartLeave() => _updateHandler.startCoroutine(Leave());
    protected void StopLeave() => _updateHandler.stopCoroutine(Leave());
    protected void LeaveState() => OnLeaveState?.Invoke();

    protected abstract IEnumerator Leave();

    protected abstract void Subscribe();
    protected abstract void Unsubscribe();
}

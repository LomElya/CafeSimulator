using System.Collections;
using UnityEngine;

public class WaitTakingOrderState : WaitState
{
    private CashDesk _desk => _parent.References.CashDesk;

    protected override Transform WaitPoint => _desk.WaitAndTake(_customer);

    public WaitTakingOrderState(CustomerStateType stateType, AICharacter parent) : base(stateType, parent)
    {
    }

    public override Transition SetTransition() => new WantToBeServed(CustomerStateType.JoinDeliveryTable, this);

    protected override IEnumerator Leave()
    {
        yield return new WaitForSeconds(0.1f);

        LeaveState();
        _desk.Leave();
        StopLeave();
    }

    protected override void Subscribe() => _desk.Complite += OnCompliteOrder;
    protected override void Unsubscribe() => _desk.Complite -= OnCompliteOrder;
}

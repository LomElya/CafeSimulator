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
        _desk.EndTransit();
        yield return new WaitForSeconds(0.1f);

        _desk.Leave();
        StopLeave();
        LeaveState();
    }

    protected override void Subscribe() => _desk.Complite += OnCompliteOrder;
    protected override void Unsubscribe() => _desk.Complite -= OnCompliteOrder;
}

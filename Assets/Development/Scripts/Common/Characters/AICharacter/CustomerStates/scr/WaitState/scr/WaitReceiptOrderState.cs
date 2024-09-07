using System.Collections;
using UnityEngine;

public class WaitReceiptOrderState : WaitState
{
    private DeliveryTable _table => _customer.References.DeliveryTable;
    private StackPresenter _stackPresenter => _customer.Stack;

    protected override Transform WaitPoint => _table.WaitAndTake(_customer);

    public WaitReceiptOrderState(CustomerStateType stateType, AICharacter parent) : base(stateType, parent)
    {
    }

    public override Transition SetTransition() => new WantToBeServed(CustomerStateType.LeaveCafe, this);

    protected override IEnumerator Leave()
    {
        yield return new WaitForSeconds(1.5f);

        foreach (Stackable stackable in _table.TakeAllItems())
        {
            _stackPresenter.AddToStack(stackable);
            yield return new WaitForSeconds(0.25f);
        }

        LeaveState();
        _table.Leave();
        StopLeave();
    }

    protected override void Subscribe() => _customer.PurchaseList.BecameEmpty += OnCompliteOrder;
    protected override void Unsubscribe() => _customer.PurchaseList.BecameEmpty -= OnCompliteOrder;
}

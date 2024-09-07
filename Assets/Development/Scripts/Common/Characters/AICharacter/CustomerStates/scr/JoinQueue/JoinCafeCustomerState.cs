using System.Collections.Generic;

public class JoinCafeCustomerState : JoinQueueState
{
    protected override IEnumerable<CustomerQueue> Queues => _parent.References.ShopQueues.Queues;

    public JoinCafeCustomerState(CustomerStateType stateType, AICharacter parent) : base(stateType, parent)
    {
    }

    public override Transition SetTransition() => new WaitFreeCashDeskTransition(CustomerStateType.WaitTakingOrder, _parent);
}

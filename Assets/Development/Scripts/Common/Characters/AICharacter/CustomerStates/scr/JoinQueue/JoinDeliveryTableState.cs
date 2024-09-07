using System.Collections.Generic;

public class JoinDeliveryTableState : JoinQueueState
{
    protected override IEnumerable<CustomerQueue> Queues => _parent.References.DeliveryQueues.Queues;

    public JoinDeliveryTableState(CustomerStateType stateType, AICharacter parent) : base(stateType, parent)
    {
    }


    public override Transition SetTransition()=> new WaitFreeDeliveryTableTransition(CustomerStateType.WaitReceiptOrder, _parent);
}

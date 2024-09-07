using System.Collections.Generic;

public class WaitFreeDeliveryTableTransition : WaitTransition
{
    protected override WaitingCustomerPoint Target => _customer.References.DeliveryTable;

    protected override IEnumerable<CustomerQueue> Queues => _customer.References.DeliveryQueues.Queues;

    public WaitFreeDeliveryTableTransition(CustomerStateType traterState, AICharacter parent) : base(traterState, parent)
    {
    }
}

using System.Collections.Generic;

public class WaitFreeCashDeskTransition : WaitTransition
{
    protected override WaitingCustomerPoint Target => _customer.References.CashDesk;
    protected override IEnumerable<CustomerQueue> Queues => _customer.References.ShopQueues.Queues;

    public WaitFreeCashDeskTransition(CustomerStateType traterState, AICharacter parent) : base(traterState, parent)
    {
    }
}

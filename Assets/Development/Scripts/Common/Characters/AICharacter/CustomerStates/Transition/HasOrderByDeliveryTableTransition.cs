using System.Linq;

public class HasOrderByDeliveryTableTransition : Transition
{
    private readonly AICharacter _parent;
    private readonly Customer _customer;

    public HasOrderByDeliveryTableTransition(CustomerStateType targetStateType, AICharacter parent) : base(targetStateType)
    {
        _parent = parent;
        _customer = _parent.Customer;
    }

    protected override void onEnable()
    {
        Subscribe();
        NeedTransit = CanTransit();
    }

    protected override void onDisable() => Unsubscribe();

    private bool CanTransit() =>
        _customer.References.DeliveryTable.Free;

    private void OnBecameEmpty()
    {
        if (_customer.References.ShopQueues.Queues.Any(queue => queue.Peek() != _customer))
            return;

        Unsubscribe();
        NeedTransit = true;
    }

    private void Subscribe() => _customer.References.DeliveryTable.BecameFree += OnBecameEmpty;
    private void Unsubscribe() => _customer.References.DeliveryTable.BecameFree -= OnBecameEmpty;
}

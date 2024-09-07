using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class WaitTransition : Transition
{
    protected readonly AICharacter _parent;
    protected readonly Customer _customer;

    protected abstract WaitingCustomerPoint Target { get; }
    protected abstract IEnumerable<CustomerQueue> Queues { get; }

    public WaitTransition(CustomerStateType traterState, AICharacter parent) : base(traterState)
    {
        _parent = parent;
        _customer = _parent.Customer;
    }

    protected override void onEnable()
    {
        NeedTransit = CanTransit();
        Subscribe();
    }

    protected override void onDisable() => Unsubscribe();

    private bool CanTransit() => Target.Free;

    private void OnBecameEmpty()
    {
        if (Queues.Any(queue => queue.Peek() != _customer))
            return;

        Unsubscribe();
        NeedTransit = true;
    }

    private void Subscribe() => Target.BecameFree += OnBecameEmpty;
    private void Unsubscribe() => Target.BecameFree -= OnBecameEmpty;
}

using System.Collections.Generic;
using UnityEngine;

public abstract class JoinQueueState : CustomerState
{
    private CustomerQueue _customerQueue;

    protected abstract IEnumerable<CustomerQueue> Queues { get; }

    public JoinQueueState(CustomerStateType stateType, AICharacter parent) : base(stateType, parent)
    {
    }

    protected override void onEnter()
    {
        Debug.Log("Встал в очередь");
        _movement.Enable();

        foreach (CustomerQueue queue in Queues)
        {
            if (queue.NotFull)
            {
                _customerQueue = queue;
                _customerQueue.Enqueue(_customer);
                GoToPositionInQueue();

                if (_customerQueue.Peek() != _customer)
                    _customerQueue.FirstCustomerChanged += OnFirstCustomerChanged;

                break;
            }
        }

        OnEnter();
    }

    protected override void onExit()
    {
        if (_customerQueue == null)
            return;

        if (_customerQueue.Peek() == _customer)
            _customerQueue.Dequeue();
        else
            _customerQueue.Remove(_customer);

        _movement.Disable();
        _customer.CreateNewPurchaseList();
        _customerQueue.FirstCustomerChanged -= OnFirstCustomerChanged;

        OnExit();
    }

    private void OnFirstCustomerChanged(Customer customer) => GoToPositionInQueue();

    private void GoToPositionInQueue()
    {
        _movement.Move(_customerQueue.GetPosition(_customer));
        _movement.OnComplete(OnCompleteMove);
    }

    private void OnCompleteMove()
    {
        _movement.Stop();
        _movement.Look(-_customerQueue.transform.forward);
    }

    protected virtual void OnEnter() { }
    protected virtual void OnExit() { }
}

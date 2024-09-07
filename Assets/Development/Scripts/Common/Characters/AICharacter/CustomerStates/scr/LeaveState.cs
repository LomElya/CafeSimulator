using UnityEngine;
using Zenject;

public class LeaveState : CustomerState
{
    private CustomerSpawner _customerSpawner;

    public LeaveState(CustomerStateType stateType, AICharacter parent) : base(stateType, parent) { }

    [Inject]
    private void Construct(CustomerSpawner customerSpawner)
    {
        _customerSpawner = customerSpawner;
    }

    public override Transition SetTransition() => new NoneTransition(CustomerStateType.None);

    protected override void onEnter()
    {
        Debug.Log("Уходит");
        _movement.Enable();
        _movement.MoveAI(_parent.References.ExitPoint.position).OnComplete(OnCompleteMove);
    }

    private void OnCompleteMove()
    {
        _customer.Leave();
        _customerSpawner.DestroyCustomer(_customer);
    }

    protected override void onExit() { }
}

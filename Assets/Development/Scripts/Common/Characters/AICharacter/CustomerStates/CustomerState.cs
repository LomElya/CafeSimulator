public abstract class CustomerState
{
    public Transition Transition;

    protected readonly AICharacter _parent;
    protected readonly AIMovement _movement;
    protected readonly Customer _customer;
    private readonly CustomerStateType _stateType;

    public CustomerStateType StateType => _stateType;

    public CustomerState(CustomerStateType stateType, AICharacter parent)
    {
        _parent = parent;
        _movement = _parent.Movement;
        _customer = _parent.Customer;

        _stateType = stateType;
        Transition = SetTransition();
    }

    public void Enter()
    {
        Transition?.Enable();
        onEnter();
    }

    public void Exit()
    {
        Transition?.Disable();
        onExit();
    }

    protected abstract void onEnter();
    protected abstract void onExit();

    public CustomerStateType GetNextState()
    {
        if (Transition == null)
            return CustomerStateType.None;

        if (Transition.NeedTransit)
            return Transition.TargetStateType;

        return CustomerStateType.None;
    }

    public abstract Transition SetTransition();

}

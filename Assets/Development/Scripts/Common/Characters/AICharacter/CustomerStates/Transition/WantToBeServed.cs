public class WantToBeServed : Transition
{
    private readonly WaitState _stateParent;

    public WantToBeServed(CustomerStateType traterState, WaitState stateParent) : base(traterState)
    {
        _stateParent = stateParent;
    }

    protected override void onEnable() => Subscribe();
    protected override void onDisable() => Unsubscribe();

    protected void OnLeaveState()
    {
        Unsubscribe();
        NeedTransit = true;
    }

    private void Subscribe() => _stateParent.OnLeaveState += OnLeaveState;
    private void Unsubscribe() => _stateParent.OnLeaveState -= OnLeaveState;
}

public class NoneCustomerSate : CustomerState
{
    public NoneCustomerSate(CustomerStateType stateType, AICharacter parent) : base(stateType, parent) { }
    protected override void onEnter() { }
    protected override void onExit() { }

    public override Transition SetTransition() => null;
}

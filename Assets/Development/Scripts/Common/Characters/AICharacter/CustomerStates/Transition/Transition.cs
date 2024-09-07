public class Transition
{
    public readonly CustomerStateType _targetStateType;

    public Transition(CustomerStateType targetStateType)
    {
        _targetStateType = targetStateType;
    }

    public CustomerStateType TargetStateType => _targetStateType;

    public bool NeedTransit { get; protected set; } = false;

    public void Enable()
    {
        NeedTransit = false;
        onEnable();
    }

    public void Disable()
    {
        onDisable();
    }

    protected virtual void onEnable() { }
    protected virtual void onDisable() { }
}

using System.Collections.Generic;
using GameHandler;
using UnityEngine;
using Zenject;

public class AICharacter : Character
{
    [SerializeField] private CustomerStateType _firstState;
    [SerializeField] private AIMovement _movement;
    [SerializeField] private Customer _customer;
    [SerializeField] private StackPresenter _stack;

    private Dictionary<CustomerStateType, CustomerState> _states = new();

    private CustomerState _currentState;
    private Transition _currentTransition;

    protected UpdateHandler _updateHandler;
    protected CustomerStateeFabric _fabric;

    private CharacterReferences _characterReferences;

    public CharacterReferences References => _characterReferences;
    public AIMovement Movement => _movement;
    public Customer Customer => _customer;
    public StackPresenter Stack => _stack;

    [Inject]
    private void Construct(UpdateHandler updateHandler, CustomerStateeFabric stateFabric)
    {
        _updateHandler = updateHandler;
        _fabric = stateFabric;
    }

    public void Init(CharacterReferences characterReferences)
    {
        _updateHandler.AddUpdate(OnUpdate);
        _characterReferences = characterReferences;
    }

    private void OnUpdate()
    {
        _view.SetMove(_movement.IsRun);

        if (_currentState == null)
            return;

        CustomerStateType nextState = _currentState.GetNextState();

        if (nextState != CustomerStateType.None)
            SetState(nextState);
    }

    public void SetState(CustomerStateType stateType)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        CustomerState newState;

        if (_states.ContainsKey(stateType))
            newState = _states[stateType];
        else
        {
            newState = _fabric.CreateState(stateType, this);

            _currentTransition = newState.Transition;

            AddStates(stateType, newState);
        }

        // _prevGameState = _currentGameState.GameStateType;
        _currentState = newState;
        _currentState.Enter();
    }

    private void AddStates(CustomerStateType stateType, CustomerState state)
    {
        _states.Add(stateType, state);
    }

    public void Run() => SetState(_firstState);

    private void OnDestroy()
    {
        _updateHandler.RemoveUpdate(OnUpdate);
    }
}

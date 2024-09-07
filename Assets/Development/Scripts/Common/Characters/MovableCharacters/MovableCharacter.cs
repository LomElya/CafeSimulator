using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovableCharacter : Character, IMovable
{
    [SerializeField] protected float _speed;

    protected Rigidbody _rigidbody;
    protected float _speedRate = 1f;

    public bool IsMoving { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        OnAwake();
    }

    public virtual void Move(Vector3 direction)
    {
        MoveVelocity(direction);
        ChangeMove(true);
    }

    public void Stop()
    {
        if (_rigidbody != null)
            _rigidbody.velocity = Vector3.zero;

        ChangeMove(false);
    }

    private void ChangeMove(bool isMoving)
    {
        IsMoving = isMoving;
        _view.SetMove(IsMoving);
    }

    protected void MoveVelocity(Vector3 direction)
    {
        _rigidbody.velocity = direction * _speed * _speedRate;
        ChangeMove(true);
    }
    protected virtual void OnAwake() { }
}
